using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repository;
        public EventoController(IProAgilRepository repository)
        {
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllEventosAsync(true);
                return Ok(results);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falou {ex.Message}");
            }
        }

        // GET api/values
        [HttpGet("{EventoId}")]
        public async Task<ActionResult> Get(int EventoId)
        {
            try
            {
                var results = await _repository.GetAllEventosAsyncById(EventoId, true);
                return Ok(results);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falou");
            }
        }

        // GET api/values
        [HttpGet("getByTema/{tema}")]
        public async Task<ActionResult> Get(string tema)
        {
            try
            {
                var results = await _repository.GetAllEventosAsyncByTema(tema, true);
                return Ok(results);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falou");
            }
        }

        // GET api/values
        [HttpPost]
        public async Task<ActionResult> Post(Evento model)
        {
            try
            {
                _repository.Add(model);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
                }
                return BadRequest();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falou");
            }
        }

        // GET api/values
        [HttpPut("{EventoId}")]
        public async Task<ActionResult> Put(int EventoId, Evento model)
        {
            try
            {
                var evento = await _repository.GetAllEventosAsyncById(EventoId,false);
                if(evento == null) return NotFound();
                _repository.Update(model);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falou. Erro {ex.Message}");
            }
        }

         // GET api/values
        [HttpDelete("{EventoId}")]
        public async Task<ActionResult> Delete(int EventoId)
        {
            try
            {
                var evento = await _repository.GetAllEventosAsyncById(EventoId,false);
                if(evento == null) return NotFound();
                _repository.Delete(evento);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falou");
            }
        }
    }
}