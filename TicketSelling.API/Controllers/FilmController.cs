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
    /// CRUD контроллер по работе с фильмами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Film")]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService filmService;
        private readonly IMapper mapper;

        public FilmController(IFilmService filmService, IMapper mapper)
        {
            this.filmService = filmService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список фильмов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<FilmResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await filmService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<FilmResponse>(x)));
        }

        /// <summary>
        /// Получить фильм по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(FilmResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await filmService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<FilmResponse>(item));
        }

        /// <summary>
        /// Добавить фильм
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(FilmResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateFilmRequest model, CancellationToken cancellationToken)
        {
            var filmModel = mapper.Map<FilmModel>(model);
            var result = await filmService.AddAsync(filmModel, cancellationToken);
            return Ok(mapper.Map<FilmResponse>(result));
        }

        /// <summary>
        /// Изменить фильм по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(FilmResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(FilmRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<FilmModel>(request);
            var result = await filmService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<FilmResponse>(result));
        }

        /// <summary>
        /// Удалить фильм по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await filmService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
