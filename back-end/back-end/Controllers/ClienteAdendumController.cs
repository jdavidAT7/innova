using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using back_end.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Controllers
{
    [Route("api/adendums")]
    [ApiController]
    public class ClienteAdendumController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "adendums";

        public ClienteAdendumController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteAdendumDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Adendums.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var adendums = await queryable.OrderBy(x => x.CodigoAdendum).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<ClienteAdendumDTO>>(adendums);
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<ClienteAdendumDTO>>> Todos()
        {
            var adendums = await context.Adendums.ToListAsync();
            return mapper.Map<List<ClienteAdendumDTO>>(adendums);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteAdendumDTO>> Get(int id)
        {
            var adendum = await context.Adendums.FirstOrDefaultAsync(x => x.Id == id);

            if (adendum == null)
            {
                return NotFound();
            }

            return mapper.Map<ClienteAdendumDTO>(adendum);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ClienteAdendumCreacionDTO clienteAdendumCreacionDTO)
        {
            var adendum = mapper.Map<ClienteAdendum>(clienteAdendumCreacionDTO);

            if (clienteAdendumCreacionDTO.Documento != null)
            {
                adendum.Documento = await almacenadorArchivos.GuardarArchivo(contenedor, clienteAdendumCreacionDTO.Documento);
            }

            context.Add(adendum);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ClienteAdendumCreacionDTO clienteAdendumCreacionDTO)
        {
            var adendum = await context.Adendums.FirstOrDefaultAsync(x => x.Id == id);

            if (adendum == null)
            {
                return NotFound();
            }

            adendum = mapper.Map(clienteAdendumCreacionDTO, adendum);

            if (clienteAdendumCreacionDTO.Documento != null)
            {
                adendum.Documento = await almacenadorArchivos.EditarArchivo(contenedor, clienteAdendumCreacionDTO.Documento, adendum.Documento);
            }

            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var adendum = await context.Adendums.FirstOrDefaultAsync(x => x.Id == id);

            if (adendum == null)
            {
                return NotFound();
            }

            context.Remove(adendum);
            await context.SaveChangesAsync();

            await almacenadorArchivos.BorrarArchivo(adendum.Documento, contenedor);

            return NoContent();
        }
    }
}
