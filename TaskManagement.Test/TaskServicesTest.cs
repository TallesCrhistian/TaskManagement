using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;
using TaskManagement.Application.ViewModels.Task;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Business;
using TaskManagement.Domain.Interfaces.Repository;

namespace TaskManagement.Test
{
    public class TaskServicesTest
    {
        [Test]
        public async Task Create_ShouldCreateTask_WhenValidDataProvided()
        {            
            var taskCreateViewModel = new TaskCreateViewModel { Title = "New Task" };
            var taskDTO = new TaskDTO { Title = "New Task" };
            var taskEntity = new TaskEntity { Title = "New Task" };
            var taskViewModel = new TaskViewModel { Title = "New Task" };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<TaskDTO>(taskCreateViewModel)).Returns(taskDTO);
            mockMapper.Setup(m => m.Map<TaskEntity>(taskDTO)).Returns(taskEntity);
            mockMapper.Setup(m => m.Map<TaskViewModel>(taskEntity)).Returns(taskViewModel);          

            var mockTaskBusiness = new Mock<ITaskBusiness>();
            var mockTaskRepository = new Mock<ITaskRepository>();
            mockTaskBusiness.Setup(b => b.Create(It.IsAny<TaskEntity>())).Returns(taskEntity);

            var mockBaseRepository = new Mock<IBaseRepository>();
            mockBaseRepository.Setup(r => r.Create(It.IsAny<TaskEntity>())).ReturnsAsync(taskEntity);

            var mockWorkUnit = new Mock<IWorkUnit>();
            var mockLogger = new Mock<ILogger<TaskServices>>();

            var service = new TaskServices(mockMapper.Object, mockLogger.Object, mockBaseRepository.Object, mockWorkUnit.Object, mockTaskBusiness.Object, mockTaskRepository.Object);

            // Act
            var response = await service.Create(taskCreateViewModel);

            // Assert
            Assert.AreEqual(StatusCodes.Status201Created, response.StatusCode);
            Assert.IsNotNull(response.GenericData);
            Assert.AreEqual(taskViewModel.Title, response.GenericData.Title);

            mockWorkUnit.Verify(w => w.SaveChangesAsync(), Times.Once);
            mockWorkUnit.Verify(w => w.CommitAsync(), Times.Once);
        }

        [Test]
        public async Task Create_ShouldReturnBadRequest_WhenTitleIsInvalid()
        {
            // Arrange
            var taskCreateViewModel = new TaskCreateViewModel { Title = null };

            var mockMapper = new Mock<IMapper>();
            var mockTaskBusiness = new Mock<ITaskBusiness>();
            var mockBaseRepository = new Mock<IBaseRepository>();
            var mockWorkUnit = new Mock<IWorkUnit>();
            var mockLogger = new Mock<ILogger<TaskServices>>();
            var mockTaskRepository = new Mock<ITaskRepository>();

            var taskDTO = new TaskDTO();

            mockMapper.Setup(m => m.Map<TaskDTO>(taskCreateViewModel)).Returns(taskDTO);

            var service = new TaskServices(mockMapper.Object, mockLogger.Object, mockBaseRepository.Object, mockWorkUnit.Object, mockTaskBusiness.Object, mockTaskRepository.Object);             

            // Act
            var response = await service.Create(taskCreateViewModel);

            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
            Assert.IsNull(response.GenericData);
           
            mockWorkUnit.Verify(w => w.Rollback(), Times.Once);
        }

        [Test]
        public async Task Create_ShouldCallRollback_WhenExceptionOccurs()
        {
            // Arrange
            var taskCreateViewModel = new TaskCreateViewModel { Title = "New Task" };

            var mockMapper = new Mock<IMapper>();
            var mockTaskBusiness = new Mock<ITaskBusiness>();
            var mockBaseRepository = new Mock<IBaseRepository>();
            mockBaseRepository.Setup(r => r.Create(It.IsAny<TaskEntity>())).ThrowsAsync(new Exception("Repository error"));

            var mockWorkUnit = new Mock<IWorkUnit>();
            var mockLogger = new Mock<ILogger<TaskServices>>();
            var mockTaskRepository = new Mock<ITaskRepository>();

            var service = new TaskServices(mockMapper.Object, mockLogger.Object, mockBaseRepository.Object, mockWorkUnit.Object, mockTaskBusiness.Object, mockTaskRepository.Object);

            // Act
            var response = await service.Create(taskCreateViewModel);

            // Assert
            mockWorkUnit.Verify(w => w.Rollback(), Times.Once);
        }

        [Test]
        public async Task Create_ShouldReturnInternalServerError_WhenCommitAsyncFails()
        {
            // Arrange
            var taskCreateViewModel = new TaskCreateViewModel { Title = "Valid Task" };

            var taskDTO = new TaskDTO { Title = "Valid Task" };
            var taskEntity = new TaskEntity { Title = "Valid Task" };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<TaskDTO>(taskCreateViewModel)).Returns(taskDTO);
            mockMapper.Setup(m => m.Map<TaskEntity>(taskDTO)).Returns(taskEntity);

            var mockTaskBusiness = new Mock<ITaskBusiness>();
            mockTaskBusiness.Setup(b => b.Create(It.IsAny<TaskEntity>())).Returns(taskEntity);

            var mockBaseRepository = new Mock<IBaseRepository>();
            mockBaseRepository.Setup(r => r.Create(It.IsAny<TaskEntity>())).ReturnsAsync(taskEntity);

            var mockWorkUnit = new Mock<IWorkUnit>();
            mockWorkUnit.Setup(w => w.CommitAsync()).ThrowsAsync(new Exception("Commit failed"));

            var mockLogger = new Mock<ILogger<TaskServices>>();
            var mockTaskRepository = new Mock<ITaskRepository>();

            var service = new TaskServices(mockMapper.Object, mockLogger.Object, mockBaseRepository.Object, mockWorkUnit.Object, mockTaskBusiness.Object, mockTaskRepository.Object);

            // Act
            var response = await service.Create(taskCreateViewModel);

            // Assert
            Assert.AreEqual(StatusCodes.Status500InternalServerError, response.StatusCode);
            mockWorkUnit.Verify(w => w.Rollback(), Times.Once);            
        }

        [Test]
        public async Task Update_ShouldReturnNotFound_WhenTaskDoesNotExist()
        {
            // Arrange
            var taskUpdateViewModel = new TaskUpdateViewModel { Id = new Guid(), Title = "Updated Task" };

            var taskDTO = new TaskDTO() { Title = "Updated Task" };

            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(m => m.Map<TaskDTO>(taskUpdateViewModel)).Returns(taskDTO);

            var mockTaskBusiness = new Mock<ITaskBusiness>();
            var mockBaseRepository = new Mock<IBaseRepository>();
            var mockWorkUnit = new Mock<IWorkUnit>();
            var mockLogger = new Mock<ILogger<TaskServices>>();
            var mockTaskRepository = new Mock<ITaskRepository>();

            var service = new TaskServices(mockMapper.Object, mockLogger.Object, mockBaseRepository.Object, mockWorkUnit.Object, mockTaskBusiness.Object, mockTaskRepository.Object);
          
            // Act
            var response = await service.Update(taskUpdateViewModel);

            // Assert
            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
            Assert.IsNull(response.GenericData);
          
            mockWorkUnit.Verify(w => w.Rollback(), Times.Once);
        }

        [Test]
        public async Task Update_ShouldReturnBadRequest_WhenTitleIsInvalid()
        {
            // Arrange
            var taskUpdateViewModel = new TaskUpdateViewModel { Id = new Guid(), Title = "" };

            var taskDTO = new TaskDTO() { Title = ""};

            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(m => m.Map<TaskDTO>(taskUpdateViewModel)).Returns(taskDTO);

            var mockTaskBusiness = new Mock<ITaskBusiness>();
            var mockBaseRepository = new Mock<IBaseRepository>();
            var mockWorkUnit = new Mock<IWorkUnit>();
            var mockLogger = new Mock<ILogger<TaskServices>>();
            var mockTaskRepository = new Mock<ITaskRepository>();

            var service = new TaskServices(mockMapper.Object, mockLogger.Object, mockBaseRepository.Object, mockWorkUnit.Object, mockTaskBusiness.Object, mockTaskRepository.Object);           

            // Act
            var response = await service.Update(taskUpdateViewModel);

            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
            Assert.IsNull(response.GenericData);
           
            mockWorkUnit.Verify(w => w.Rollback(), Times.Once);
        }

        [Test]
        public async Task Update_ShouldUpdateTaskSuccessfully()
        {
            // Arrange
            var taskUpdateViewModel = new TaskUpdateViewModel { Id = new Guid(), Title = "Updated Task" };

            var taskDTO = new TaskDTO { Id = new Guid(), Title = "Updated Task" };
            var oldTaskDTO = new TaskDTO { Id = new Guid(), Title = "Old Task" };
            var taskEntity = new TaskEntity { Id = new Guid(), Title = "Updated Task" };
            var taskViewModel = new TaskViewModel { Id = new Guid(), Title = "Updated Task" };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<TaskDTO>(taskUpdateViewModel)).Returns(taskDTO);
            mockMapper.Setup(m => m.Map<TaskEntity>(taskDTO)).Returns(taskEntity);
            mockMapper.Setup(m => m.Map<TaskViewModel>(taskEntity)).Returns(taskViewModel);

            var mockTaskBusiness = new Mock<ITaskBusiness>();
            mockTaskBusiness.Setup(b => b.Update(It.IsAny<TaskEntity>())).Returns(taskEntity);

            var mockBaseRepository = new Mock<IBaseRepository>();
            mockBaseRepository.Setup(r => r.Update(It.IsAny<TaskEntity>())).ReturnsAsync(taskEntity);
            mockBaseRepository.Setup(r => r.Read<TaskEntity>(It.IsAny<Guid>())).ReturnsAsync(taskEntity);

            var mockWorkUnit = new Mock<IWorkUnit>();
            var mockLogger = new Mock<ILogger<TaskServices>>();
            var mockTaskRepository = new Mock<ITaskRepository>();

            var service = new TaskServices(mockMapper.Object, mockLogger.Object, mockBaseRepository.Object, mockWorkUnit.Object, mockTaskBusiness.Object, mockTaskRepository.Object);          

            // Act
            var response = await service.Update(taskUpdateViewModel);

            // Assert
            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
            Assert.IsNotNull(response.GenericData);

            mockWorkUnit.Verify(w => w.SaveChangesAsync(), Times.Once);
            mockWorkUnit.Verify(w => w.CommitAsync(), Times.Once);
        }
    }
}
