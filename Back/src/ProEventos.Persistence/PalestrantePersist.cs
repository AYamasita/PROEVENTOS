using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;

namespace ProEventos.Persistence.Contratos
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _context;
        public PalestrantePersist(ProEventosContext context)
        {
            _context = context;
        }

       
        //Palestrantes
       async Task<Palestrante[]> IPalestrantePersist.GetAllPalestrantesAsync(bool includeEventos)
        {
            
             IQueryable<Palestrante> query = _context.Palestrantes
             .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos)
                .ThenInclude(e=>e.Evento);
            }

            query = query.OrderBy(p => p.Id);
            return await query.AsNoTracking().ToArrayAsync();
        }
        async Task<Palestrante[]> IPalestrantePersist.GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
               
             IQueryable<Palestrante> query = _context.Palestrantes
             .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos)
                .ThenInclude(e=>e.Evento);
            }

            query = query.OrderBy(p => p.Id)
                .Where(p=>p.Nome.ToLower().Contains(nome.ToLower()));
            return await query.AsNoTracking().ToArrayAsync();
        }
        async Task<Palestrante> IPalestrantePersist.GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
             IQueryable<Palestrante> query = _context.Palestrantes
             .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos)
                .ThenInclude(e=>e.Evento);
            }

            query = query.OrderBy(p => p.Id)
                .Where(p=>p.Id == palestranteId);
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

    }
}