using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGenericPersistence _generic;
        private readonly IEventoPersistence _evento;
        public EventoService(IGenericPersistence generic, IEventoPersistence evento)
        {
            _evento = evento;
            _generic = generic;

        }
        public async Task<Evento> Add(Evento model)
        {
            try
            {
                _generic.Add<Evento>(model);
                if (await _generic.SaveChangesAsync())
                {
                    return await _evento.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            

        }

        public async Task<Evento> Update(int id, Evento model)
        {
            try
            {
               var evento = await _evento.GetEventoByIdAsync(id, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                _generic.Update<Evento>(model);
                if (await _generic.SaveChangesAsync())
                {
                    return await _evento.GetEventoByIdAsync(model.Id, false);
                }
                return null;
               
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        public async Task<bool> Delete(int id)
        {
           try
            {
               var evento = await _evento.GetEventoByIdAsync(id, false);

                _generic.Delete<Evento>(evento);
               if (evento == null) throw new Exception("EVENTNOTFOUND");

                return await _generic.SaveChangesAsync();
               
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes=false)
        {
              try
              {
                  return await _evento.GetAllEventosAsync(includePalestrantes);
              }
              catch (Exception ex)
              {
                  
                  throw ex;
              }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            try
              {
                  return await _evento.GetAllEventosByTemaAsync(tema, includePalestrantes);
              }
              catch (Exception ex)
              {
                  
                  throw ex;
              }
        }

        public async Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes)
        {
            try
              {
                  return await _evento.GetEventoByIdAsync(id, includePalestrantes);
              }
              catch (Exception ex)
              {                  
                  throw ex;
              }
        }

       
    }
}