using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.ViewModels.Task;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Business;
using TaskManagement.Domain.Interfaces.Repository;
using TaskManagement.Utils;
using TaskManagement.Utils.CustomMaths;
using TaskManagement.Utils.Exceptions;
using TaskManagement.Utils.Messages;

namespace TaskManagement.Application.Services
{
    public class TaskServices : ITaskServices
    {
        private readonly IMapper _iMapper;
        private readonly ILogger<TaskServices> _iLogger;
        private readonly IBaseRepository _iBaseRepository;
        private readonly IWorkUnit _iWorkUnit;
        private readonly ITaskBusiness _iTaskBusiness;
        private readonly ITaskRepository _iTaskRepository;

        public TaskServices(
            IMapper iMapper,
            ILogger<TaskServices> logger, 
            IBaseRepository iBaseRepository, 
            IWorkUnit iWorkUnit,
            ITaskBusiness iTaskBusiness,
            ITaskRepository iTaskRepository)
        {
            _iMapper = iMapper;
            _iLogger = logger;
            _iBaseRepository = iBaseRepository;
            _iWorkUnit = iWorkUnit;
            _iTaskBusiness = iTaskBusiness;
            _iTaskRepository = iTaskRepository;
        }

        private const string EntityName = "Task";

        public async Task<ServiceResponseDTO<TaskViewModel>> Create(TaskCreateViewModel taskCreateViewModel)
        {
            ServiceResponseDTO<TaskViewModel> serviceResponseDTO = new ServiceResponseDTO<TaskViewModel>();

            try
            {
                TaskDTO taskDTO = _iMapper.Map<TaskDTO>(taskCreateViewModel);

                ValidateTitle(taskDTO);                

                TaskEntity taskEntity = _iMapper.Map<TaskEntity>(taskDTO);

                taskEntity = _iTaskBusiness.Create(taskEntity);

                taskEntity = await _iBaseRepository.Create(taskEntity);

                serviceResponseDTO.GenericData = _iMapper.Map<TaskViewModel>(taskEntity);
                serviceResponseDTO.StatusCode = StatusCodes.Status201Created;

                await this._iWorkUnit.SaveChangesAsync();
                await this._iWorkUnit.CommitAsync();

                this._iLogger.LogInformation(Messages.Created(EntityName));
            }
            catch (CustomException ex)
            {
                this._iWorkUnit.Rollback();

                this._iLogger.LogError(ex.Message, ex.StatusCode, ex.InnerException);

                serviceResponseDTO = CatchFunctions.ServiceResponse<CustomException, TaskViewModel>(ex, ex.StatusCode);
            }
            catch (Exception ex)
            {
                this._iWorkUnit.Rollback();

                this._iLogger.LogCritical(ex.Message, ex.InnerException);

                serviceResponseDTO = CatchFunctions.ServiceResponse<Exception, TaskViewModel>(ex, HttpStatusCode.InternalServerError);
            }

            return serviceResponseDTO;
        }

        public async Task<ServiceResponseDTO<TaskViewModel>> Read(Guid id)
        {
            ServiceResponseDTO<TaskViewModel> serviceResponseDTO = new ServiceResponseDTO<TaskViewModel>();

            try
            {
                TaskDTO taskDTO = await ValidateIfTaskExistsAsync(id);

                serviceResponseDTO.GenericData = _iMapper.Map<TaskViewModel>(taskDTO);                
            }
            catch (CustomException ex)
            {
                this._iLogger.LogError(ex.Message, ex.StatusCode, ex.InnerException);

                serviceResponseDTO = CatchFunctions.ServiceResponse<CustomException, TaskViewModel>(ex, ex.StatusCode);
            }
            catch (Exception ex)
            {            
                this._iLogger.LogCritical(ex.Message, ex.InnerException);

                serviceResponseDTO = CatchFunctions.ServiceResponse<Exception, TaskViewModel>(ex, HttpStatusCode.InternalServerError);
            }

            return serviceResponseDTO;
        }

        public async Task<ServiceResponseDTO<TaskViewModel>> Update(TaskUpdateViewModel taskUpdateViewModel)
        {
            ServiceResponseDTO<TaskViewModel> serviceResponseDTO = new ServiceResponseDTO<TaskViewModel>();

            try
            {
                TaskDTO oldTaskDTO =  await ValidateIfTaskExistsAsync(taskUpdateViewModel.Id);

                TaskDTO taskDTO = _iMapper.Map<TaskDTO>(taskUpdateViewModel);

                ValidateTitle(taskDTO);

                TaskEntity newTaskEntity = _iMapper.Map<TaskEntity>(taskDTO);
                TaskEntity oldTaskEntity = _iMapper.Map<TaskEntity>(oldTaskDTO);

                newTaskEntity = _iTaskBusiness.Update(newTaskEntity);

                newTaskEntity = await _iBaseRepository.Update(newTaskEntity);

                serviceResponseDTO.GenericData = _iMapper.Map<TaskViewModel>(newTaskEntity);                

                await this._iWorkUnit.SaveChangesAsync();
                await this._iWorkUnit.CommitAsync();

                this._iLogger.LogInformation(Messages.Created(EntityName));
            }
            catch (CustomException ex)
            {
                this._iWorkUnit.Rollback();

                this._iLogger.LogError(ex.Message, ex.StatusCode, ex.InnerException);

                serviceResponseDTO = CatchFunctions.ServiceResponse<CustomException, TaskViewModel>(ex, ex.StatusCode);
            }
            catch (Exception ex)
            {
                this._iWorkUnit.Rollback();

                this._iLogger.LogCritical(ex.Message, ex.InnerException);

                serviceResponseDTO = CatchFunctions.ServiceResponse<Exception, TaskViewModel>(ex, HttpStatusCode.InternalServerError);
            }

            return serviceResponseDTO;
        }

        public async Task<ServiceResponseDTO<TaskViewModel>> Delete(Guid id)
        {
            ServiceResponseDTO<TaskViewModel> serviceResponseDTO = new ServiceResponseDTO<TaskViewModel>();

            try
            {
                TaskDTO taskDTO =  await ValidateIfTaskExistsAsync(id);

                TaskEntity taskEntity = await _iBaseRepository.Delete<TaskEntity>(id);

                serviceResponseDTO.GenericData = _iMapper.Map<TaskViewModel>(taskEntity);                

                await this._iWorkUnit.SaveChangesAsync();
                await this._iWorkUnit.CommitAsync();

                this._iLogger.LogInformation(Messages.Deleted(EntityName));
            }
            catch (CustomException ex)
            {
                this._iWorkUnit.Rollback();

                this._iLogger.LogError(ex.Message, ex.StatusCode, ex.InnerException);

                serviceResponseDTO = CatchFunctions.ServiceResponse<CustomException, TaskViewModel>(ex, ex.StatusCode);
            }
            catch (Exception ex)
            {
                this._iWorkUnit.Rollback();

                this._iLogger.LogCritical(ex.Message, ex.InnerException);

                serviceResponseDTO = CatchFunctions.ServiceResponse<Exception, TaskViewModel>(ex, HttpStatusCode.InternalServerError);
            }

            return serviceResponseDTO;
        }

        public async Task<ServiceResponseDTO<ListResponseDTO<TaskViewModel>>> List(TaskFilterViewModel taskFilterViewModel, int pageForIndex)
        {
            ServiceResponseDTO<ListResponseDTO<TaskViewModel>> serviceResponseDTO = new ServiceResponseDTO<ListResponseDTO<TaskViewModel>>();

            try
            {
                TaskEntity taskEntity = this._iMapper.Map<TaskEntity>(taskFilterViewModel);

                List<TaskEntity> taskEntities = await this._iTaskRepository.List(taskEntity, pageForIndex);               

                List<TaskViewModel> taskDTOList = taskEntities.Count != 0 ? this._iMapper.Map<List<TaskViewModel>>(taskEntities)
                     : throw new CustomException(HttpStatusCode.NotFound, Messages.NotFound(EntityName), new HttpRequestException());                          

                ListResponseDTO<TaskViewModel> dataResponse = new ListResponseDTO<TaskViewModel>()
                {
                    Data = taskDTOList,
                    TotalPages = await GetTotalPages(taskEntity),
                };

                serviceResponseDTO.GenericData = dataResponse;                
            }
            catch (CustomException ex)
            {
                this._iLogger.LogError(ex.Message, ex.StatusCode, ex.InnerException);

                serviceResponseDTO = CatchFunctions.ServiceResponse<CustomException, ListResponseDTO<TaskViewModel>>(ex, ex.StatusCode);
            }
            catch (Exception ex)
            {
                this._iLogger.LogCritical(ex.Message, ex.InnerException);

                serviceResponseDTO = CatchFunctions.ServiceResponse<Exception, ListResponseDTO<TaskViewModel>>(ex, HttpStatusCode.InternalServerError);
            }

            return serviceResponseDTO;
        }             

        private void ValidateTitle(TaskDTO taskDTO)
        {           
            if (string.IsNullOrWhiteSpace(taskDTO.Title))
                throw new CustomException(HttpStatusCode.BadRequest, Messages.RequiredProperty("title"), new HttpRequestException());           

            if (taskDTO.Title.Length > 100)
                throw new CustomException(HttpStatusCode.BadRequest, Messages.CharacterLimit("title", "100"), new HttpRequestException());                    
        }

        private async Task<TaskDTO> ValidateIfTaskExistsAsync(Guid id)
        {
            TaskEntity taskEntity = await _iBaseRepository.Read<TaskEntity>(id);

            return taskEntity is not null ? _iMapper.Map<TaskDTO>(taskEntity)
               : throw new CustomException(HttpStatusCode.BadRequest, Messages.NotFound(EntityName), new HttpRequestException());
        }

        private async Task<int> GetTotalPages(TaskEntity taskEntity)
        {
            try
            {
                int itensByPage = ConfigurationHelper.GetItemsPerPage();

                int quantityOfTasks = await _iTaskRepository.GetQuantityOfItens(taskEntity);

                int totalPages = MathAdvanced.DivideAndRoundUp(quantityOfTasks, itensByPage);

                return totalPages;
            }
            catch
            {
                throw;
            }
        }
    }
}
