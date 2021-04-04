﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {

        private readonly ILogger<EventoController> _logger;
        private readonly DataContext _context;

        public EventoController(DataContext context)
        {
            _context = context;
        }

      
        [HttpGet]
        public IEnumerable<Evento> Get()
        {
           return _context.Eventos; 
        }

        [HttpGet("{id}")]
        public Evento Get(int id)
        {
           return _context.Eventos.FirstOrDefault(e=> e.EventoId==id); 
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
