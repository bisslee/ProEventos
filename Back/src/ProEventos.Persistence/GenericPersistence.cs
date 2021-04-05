using System.Threading.Tasks;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Data;

namespace ProEventos.Persistence
{
    public class GenericPersistence : IGenericPersistence
    {
        private readonly ProEventosContext _context;

        public GenericPersistence(ProEventosContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entities) where T : class
        {
             _context.RemoveRange(entities);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) >0;
        }
       
    }
}