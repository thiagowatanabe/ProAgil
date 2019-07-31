using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Api.Dtos;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repository;
        private readonly IMapper _mapper;
        public EventoController(IProAgilRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var eventos = await _repository.GetAllEventosAsync(true);

                var results = _mapper.Map<EventoDto[]>(eventos);

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falou {ex.Message}");
            }
        }

        // Post api/upload
        [HttpPost("upload")]
        public async Task<ActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;

                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", "").Trim());

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return Ok();
                }
                return BadRequest("Erro ao tentar realizar upload");
            }
            catch (Exception ex)
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
                var evento = await _repository.GetAllEventosAsyncById(EventoId, true);
                var results = _mapper.Map<EventoDto>(evento);
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
                var evento = await _repository.GetAllEventosAsyncByTema(tema, true);
                var results = _mapper.Map<List<EventoDto>>(evento);
                return Ok(results);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falou");
            }
        }

        // GET api/values
        [HttpPost]
        public async Task<ActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);

                _repository.Add(evento);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{evento.Id}", _mapper.Map<EventoDto>(evento));
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
                var evento = await _repository.GetAllEventosAsyncById(EventoId, false);
                if (evento == null) return NotFound();

                evento = _mapper.Map(model, evento);

                _repository.Update(evento);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{evento.Id}", _mapper.Map<EventoDto>(evento));
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
                var evento = await _repository.GetAllEventosAsyncById(EventoId, false);
                if (evento == null) return NotFound();
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