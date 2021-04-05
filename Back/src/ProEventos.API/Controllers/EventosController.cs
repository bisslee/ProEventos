using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence.Data;
using ProEventos.Domain;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {

        private readonly ILogger<EventosController> _logger;
        private readonly IEventoService _service;

        public EventosController(IEventoService service)
        {
            _service = service;
        }

      
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           try
           {
               var eventos = await _service.GetAllEventosAsync(true);
               if (eventos==null) return NotFound("Nenhum Evento Encontrado");

               return Ok(eventos);
           }
           catch (Exception ex)
           {
               return this.StatusCode(StatusCodes.Status500InternalServerError, 
                        $"Erro ao recuperar eventos. Erro: {ex.Message}") ;           
           }
        }

        [HttpGet("{id}")]
        public  async Task<IActionResult> GetById(int id)
        {
           try
           {
               var evento = await _service.GetEventoByIdAsync(id, true);
               if (evento==null) return NotFound("Nenhum Evento Encontrado");

               return Ok(evento);
           }
           catch (Exception ex)
           {
               return this.StatusCode(StatusCodes.Status500InternalServerError, 
                        $"Erro ao recuperar eventos. Erro: {ex.Message}");            
           }
        }

        [HttpGet("tema/{tema}")]
        public  async Task<ActionResult> GetByTema(string tema)
        {
           try
           {
               var eventos = await _service.GetAllEventosByTemaAsync(tema, true);
               if (eventos==null) return NotFound("Nenhum Evento Encontrado");

               return Ok(eventos);
           }
           catch (Exception ex)
           {
               return this.StatusCode(StatusCodes.Status500InternalServerError, 
                        $"Erro ao recuperar eventos. Erro: {ex.Message}");            
           }
        }
        
        [HttpPost]
        public  async Task<ActionResult> Post(Evento model)
        {
           try
           {
               if (model==null) return BadRequest("Evento obrigatorio");
               
               var eventos = await _service.Add(model);
               if (eventos==null) return BadRequest("erro ao cadastrar ");

               return Ok(eventos);
           }
           catch (Exception ex)
           {
               return this.StatusCode(StatusCodes.Status500InternalServerError, 
                        $"Erro ao Cadastrar eventos. Erro: {ex.Message}");            
           }
        }

        [HttpPut("{id}")]
         public  async Task<ActionResult> Put(int id, Evento model)
         {
           try
           {
               if (model==null) return BadRequest("Evento obrigatorio");
               
               var evento = await _service.Update(id, model);
               if (evento==null) return BadRequest("erro ao cadastrar ");

               return Ok(evento);
           }
           catch (Exception ex)
           {
               return this.StatusCode(StatusCodes.Status500InternalServerError, 
                        $"Erro ao Atualizar eventos. Erro: {ex.Message}");            
           }
        }

        [HttpDelete("{id}")]
        public  async Task<ActionResult> Delete(int id)
         {
           try
           {
              if (await _service.Delete(id))
              {
                  return Ok();
              }
              else
              {
                  return BadRequest();
              }
               
            
               
           }
           catch (Exception ex)
           {
               return this.StatusCode(StatusCodes.Status500InternalServerError, 
                        $"Erro ao Deletar eventos. Erro: {ex.Message}");            
           }
        }
    }
}
