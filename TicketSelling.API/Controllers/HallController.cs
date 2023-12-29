using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TicketSelling.API.Exceptions;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ServicesContracts;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с залами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Hall")]
    public class HallController : ControllerBase
    {
        private readonly IHallService hallService;
        private readonly IMapper mapper;

        public HallController(IHallService hallService, IMapper mapper)
        {
            this.hallService = hallService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список залов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<HallResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await hallService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<HallResponse>(x)));
        }

        /// <summary>
        /// Получить зал по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(HallResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await hallService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<HallResponse>(item));
        }

        /// <summary>
        /// Добавить зал
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(HallResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateHallRequest model, CancellationToken cancellationToken)
        {
            var hallModel = mapper.Map<HallModel>(model);
            var result = await hallService.AddAsync(hallModel, cancellationToken);
            return Ok(mapper.Map<HallResponse>(result));
        }

        /// <summary>
        /// Изменить зал по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(HallResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(HallRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<HallModel>(request);
            var result = await hallService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<HallResponse>(result));
        }

        /// <summary>
        /// Удалить зал по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await hallService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
