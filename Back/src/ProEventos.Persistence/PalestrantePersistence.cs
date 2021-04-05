using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Data;

namespace ProEventos.Persistence
{
    public class PalestrantePersistence : IPalestrantePersistence
    {
        private readonly ProEventosContext _context;

        public PalestrantePersistence(ProEventosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos=false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes                     
            .Include(p=>p.RedesSociais);

            if(includeEventos)
            {
                query= query
                        .Include(p=>p.PalestrantesEventos)
                        .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p=>p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos=false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Where(p=>p.Nome.ToLower().Contains(nome.ToLower()))    
            .Include(p=>p.RedesSociais);

            if(includeEventos)
            {
                query= query
                        .Include(p=>p.PalestrantesEventos)
                        .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p=>p.Id);

            return await query.ToArrayAsync();
        }



        public async Task<Palestrante> GetPalestranteByIdAsync(int id, bool includeEventos=false)
        {
           IQueryable<Palestrante> query = _context.Palestrantes
            .Where(p=>p.Id == id)    
            .Include(p=>p.RedesSociais);

            if(includeEventos)
            {
                query= query
                        .Include(p=>p.PalestrantesEventos)
                        .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p=>p.Id);

            return await query.FirstOrDefaultAsync();
        }

    }
}