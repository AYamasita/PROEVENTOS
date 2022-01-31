using System.Threading.Tasks;
using ProEventos.Persistence.Contextos;

namespace ProEventos.Persistence.Contratos
{
    public class GeralPersist : IGeralPersist
    {
        private readonly ProEventosContext _context;
        public GeralPersist(ProEventosContext context)
        {
            _context = context;
        }

        //Geral
        void IGeralPersist.Add<T>(T entity)
        {
            _context.Add(entity);
        }


        void IGeralPersist.Update<T>(T entity)
        {
            _context.Update(entity);
        }

        void IGeralPersist.Delete<T>(T entity)
        {
            _context.Remove(entity);
        }

        void IGeralPersist.DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        async Task<bool> IGeralPersist.SaveChangesAsync() {

               return (await _context.SaveChangesAsync()) > 0;
        }

       

    }
}