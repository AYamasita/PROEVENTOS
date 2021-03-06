using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext _context;
        public EventoPersist(ProEventosContext context)
        {
            _context = context;
           // _context.ChangeTracker.QueryTrackingBehavior =  QueryTrackingBehavior.NoTracking;
           
        }       

        //Eventos
        async Task<Evento[]> IEventoPersist.GetAllEventosAsync(bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos)
                .ThenInclude(pe=>pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id);
            return await query.AsNoTracking().ToArrayAsync();
        }
        
        async Task<Evento[]> IEventoPersist.GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
             IQueryable<Evento> query = _context.Eventos
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos)
                .ThenInclude(pe=>pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id)
                    .Where(e=>e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.AsNoTracking().ToArrayAsync();
        }
        async Task<Evento> IEventoPersist.GetEventoByIdAsync(int eventoId, bool includePalestrantes)
        {
             IQueryable<Evento> query = _context.Eventos
            .Include(e => e.Lotes)
            .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos)
                .ThenInclude(pe=>pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id)
                    .Where(e=>e.Id == eventoId);

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

    }
}