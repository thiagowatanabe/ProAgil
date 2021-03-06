using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //Geral
         void Add <T>(T entity) where T : class;

         void Update <T>(T entity) where T : class;

         void Delete <T>(T entity) where T : class;

         Task<bool> SaveChangesAsync();

         //Eventos
         Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes);

         Task<Evento[]> GetAllEventosAsync( bool includePalestrantes);
         Task<Evento> GetAllEventosAsyncById(int EventoId, bool includePalestrantes);

          Task<Palestrante[]> GetAllPalestrantesAsyncByName(string nome, bool includeEventos);
         Task<Palestrante> GetAllPalestrantesAsyncById(int PalestrantesId, bool includeEventos);
    }
}