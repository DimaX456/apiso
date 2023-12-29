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
    /// CRUD контроллер по работе с кинотеатрами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Cinema")]
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaService cinemaService;
        private readonly IMapper mapper;

        public CinemaController(ICinemaService cinemaService, IMapper mapper)
        {
            this.cinemaService = cinemaService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список кинотеатров
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CinemaResponse>), StatusCodes.Status200OK)]      
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await cinemaService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<CinemaResponse>(x)));
        }

        /// <summary>
        /// Получить кинотетар по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CinemaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id,CancellationToken cancellationToken)
        {
            var item = await cinemaService.GetByIdAsync(id,cancellationToken);
            return Ok(mapper.Map<CinemaResponse>(item));
        }

        /// <summary>
        /// Добавить кинотеатр
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CinemaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateCinemaRequest model, CancellationToken cancellationToken)
        {
            var cinemaModel = mapper.Map<CinemaModel>(model);
            var result = await cinemaService.AddAsync(cinemaModel, cancellationToken);
            return Ok(mapper.Map<CinemaResponse>(result));
        }

        /// <summary>
        /// Изменить кинотеатр
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(CinemaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(CinemaRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<CinemaModel>(request);
            var result = await cinemaService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<CinemaResponse>(result));
        }

        /// <summary>
        /// Удалить Кинотеатр по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await cinemaService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
