using System.Threading.Tasks;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application.Contratos
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;

        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            _geralPersist = geralPersist;
            _eventoPersist = eventoPersist;

        }

        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersist.Add<Evento>(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    //recupera o evento adicionado
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                }
                return null;

            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);

                if (evento is null) return null;

                _geralPersist.Update(model);
                model.Id = eventoId;

                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                 var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);

                 if (evento is null) throw new System.Exception("Evento para deletar não foi encontrado.");

                 _geralPersist.Delete<Evento>(evento);

                 return await _geralPersist.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);

                if (eventos is null) return null;

                return eventos;
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema);

                if (eventos is null) return null;

                return eventos;
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
           try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);

                if (evento is null) throw new System.Exception("Evento não foi encontrado.");
                
                return evento;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }


    }
}