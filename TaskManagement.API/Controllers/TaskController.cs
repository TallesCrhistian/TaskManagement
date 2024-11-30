using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.ViewModels.Task;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskServices _iTaskServices;

        public TaskController(ITaskServices iTaskServices)
        {
            _iTaskServices = iTaskServices;
        }

        /// <summary>
        /// Cria uma Tarefa com base nos dados fornecidos no <paramref name="taskCreateViewModel"/>.
        /// </summary>
        /// <param name="taskCreateViewModel">Os dados da Tarefa a ser criada.</param>
        /// <returns>Um objeto <see cref="IActionResult"/> que representa o resultado da operação.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServiceResponseDTO<TaskViewModel>))]      
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateViewModel taskCreateViewModel)
        {         
            ServiceResponseDTO<TaskViewModel> serviceResponseDTO = await this._iTaskServices.Create(taskCreateViewModel);

            return this.StatusCode(serviceResponseDTO.StatusCode, serviceResponseDTO);
        }

        /// <summary>
        /// Busca uma Tarefa com base no <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Filtro para buscar uma Tarefa.</param>
        /// <returns>Um objeto <see cref="IActionResult"/> que representa o resultado da operação.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServiceResponseDTO<TaskViewModel>))]       
        [HttpGet("{id}")]
        public async Task<IActionResult> Read(Guid id)
        {
            ServiceResponseDTO<TaskViewModel> serviceResponseDTO = await this._iTaskServices.Read(id);

            return this.StatusCode(serviceResponseDTO.StatusCode, serviceResponseDTO);
        }

        /// <summary>
        /// Atualiza uma Tarefa com base nos dados fornecidos no <paramref name="taskUpdateViewModel"/>.
        /// </summary>
        /// <param name="taskUpdateViewModel">Objeto para atualização da Tarefa.</param>
        /// <returns>Um objeto <see cref="IActionResult"/> que representa o resultado da operação.</returns>        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServiceResponseDTO<TaskViewModel>))]       
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskUpdateViewModel taskUpdateViewModel)
        {
            ServiceResponseDTO<TaskViewModel> serviceResponseViewModel = await this._iTaskServices.Update(taskUpdateViewModel);

            return this.StatusCode(serviceResponseViewModel.StatusCode, serviceResponseViewModel);
        }

        /// <summary>
        /// Deleta uma Tarefa com no <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Filtro para validar a Tarefa a ser deletado.</param>
        /// <returns>Um objeto <see cref="IActionResult"/> que representa o resultado da operação.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServiceResponseDTO<TaskViewModel>))]       
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {         
            ServiceResponseDTO<TaskViewModel> serviceResponseViewModel = await this._iTaskServices.Delete(id);

            return this.StatusCode(serviceResponseViewModel.StatusCode, serviceResponseViewModel);
        }

        /// <summary>
        /// Lista Tarefas com o filtro <paramref name="taskFilterViewModel"/>.
        /// </summary>
        /// <param name="taskFilterViewModel">Objeto para filtrar e buscar os Tarefas.</param>
        /// <returns>Um objeto <see cref="IActionResult"/> que representa o resultado da operação.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ServiceResponseDTO<TaskViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ServiceResponseDTO<TaskViewModel>))]       
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] TaskFilterViewModel taskFilterViewModel, [FromQuery] int pageIndex)
        {
            ServiceResponseDTO<ListResponseDTO<TaskViewModel>> serviceResponseViewModel = await this._iTaskServices.List(taskFilterViewModel, pageIndex);

            return this.StatusCode(serviceResponseViewModel.StatusCode, serviceResponseViewModel);
        }
    }
}