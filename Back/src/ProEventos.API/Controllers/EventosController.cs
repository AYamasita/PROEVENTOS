using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos is null)
                {
                    return NotFound("Nenhum evento encontrado.");
                }
                return Ok(eventos);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar recuperar eventos. Erro {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoByIdAsync(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento is null)
                {
                    return NotFound("Evento por Id não encontrado.");
                }
                return Ok(evento);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar recuperar evento por Id. Erro {ex.Message}");
            }

        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetEventoByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync(tema);
                if (eventos is null)
                {
                    return NotFound("Evento por tema não encontrado.");
                }
                return Ok(eventos);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar recuperar evento por tema. Erro {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
               var evento =  await _eventoService.AddEventos(model);
               if (evento is null)
                    return BadRequest("Erro ao adicionar evento.");
               return Ok(evento);
            }
            catch (System.Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar adicionar evento. Erro {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
               var evento =  await _eventoService.UpdateEvento(id,model);
               if (evento is null)
                    return BadRequest("Erro ao atualizar evento.");
               return Ok(evento);
            }
            catch (System.Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar atualizar evento. Erro {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete(int id)
        {
               try
            {
              
               return await _eventoService.DeleteEvento(id) ? Ok("Deletado") :
                    BadRequest("Erro ao deletar evento.");               
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                $"Erro ao tentar deletar evento. Erro {ex.Message}");
            }
        }
    }
}
