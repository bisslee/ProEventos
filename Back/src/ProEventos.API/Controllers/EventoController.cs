using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {

        private readonly ILogger<EventoController> _logger;

        public EventoController()
        {
         
        }

        public IEnumerable<Evento> eventos = new Evento[]{
            new Evento(){
               EventoId=1,
               Tema= "Angular e .Net 5",
               Local="Sampa",
               Lote="1o. lote",
               QtdPessoas=250,
               DataEvento=DateTime.Now.AddDays(3).ToString()
           },
            new Evento(){
               EventoId=2,
               Tema= "Angular  gaeasd",
               Local="Sampa",
               Lote="3o. lote",
               QtdPessoas=440,
               DataEvento=DateTime.Now.AddDays(10).ToString()
           }

           };

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
           return eventos; 
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> Get(int id)
        {
           return eventos.Where(e=> e.EventoId==id); 
        }
        
        [HttpPost]
        public string Post()
        {
           return "exemplo de Post";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
           return $"exemplo de Put {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
           return $"exemplo de Delete {id}";
        }
    }
}
