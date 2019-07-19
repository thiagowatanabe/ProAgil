using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using System.Linq;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;
        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

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


        public async Task<bool> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync() > 0;
        }



        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedeSociais);

            if(includePalestrantes)
            {
                query = query.Include(c => c.PalestranteEventos).ThenInclude(c => c.Palestrante);
            }

            query = query.OrderByDescending(c => c.Data);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAllEventosAsyncById(int EventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedeSociais);

            if(includePalestrantes)
            {
                query = query.Include(c => c.PalestranteEventos).ThenInclude(c => c.Palestrante);
            }

            query = query.OrderByDescending(c => c.Data).Where(q => q.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedeSociais);

            if(includePalestrantes)
            {
                query = query.Include(c => c.PalestranteEventos).ThenInclude(c => c.Palestrante);
            }

            query = query.OrderByDescending(c => c.Data).Where(q => q.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestrantesAsyncById(int PalestrantesId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(c => c.RedeSociais);

            if(includeEventos)
            {
                query = query.Include(c => c.PalestranteEventos).ThenInclude(c => c.Evento);
            }

            query = query.OrderBy(q => q.Nome).Where(q => q.Id == PalestrantesId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string nome,bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(c => c.RedeSociais);

            if(includeEventos)
            {
                query = query.Include(c => c.PalestranteEventos).ThenInclude(c => c.Evento);
            }

            query = query.OrderBy(q => q.Nome).Where(q => q.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

    }
}