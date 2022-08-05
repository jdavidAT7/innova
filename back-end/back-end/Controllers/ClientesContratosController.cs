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
    [Route("api/contratos")]
    [ApiController]
    public class ClientesContratosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "contratos";

        public ClientesContratosController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteContratoDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Contratos.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var contratos = await queryable.OrderBy(x => x.CodigoContrato).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<ClienteContratoDTO>>(contratos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteContratoDTO>> Get(int id)
        {
            var contrato = await context.Contratos.FirstOrDefaultAsync(x => x.Id == id);

            if (contrato == null)
            {
                return NotFound();
            }

            return mapper.Map<ClienteContratoDTO>(contrato);
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<List<ClienteContratoDTO>>> Filtrar([FromQuery] CContratosFiltrarDTO contratosFiltrarDTO)
        {
            var contratosQueryable = context.Contratos.AsQueryable();

            if (!string.IsNullOrEmpty(contratosFiltrarDTO.CodigoContrato))
            {
                contratosQueryable = contratosQueryable.Where(x => x.CodigoContrato.Contains(contratosFiltrarDTO.CodigoContrato));
            }
/*
            if (clientesFiltrarDTO.AdendumId != 0)
            {
                clientesQueryable = clientesQueryable
                    .Where(x => x.ClientesAdendums.Select(y => y.AdendumId)
                    .Contains(clientesFiltrarDTO.AdendumId));
            }
*/
            await HttpContext.InsertarParametrosPaginacionEnCabecera(contratosQueryable);

            var contratos = await contratosQueryable.Paginar(contratosFiltrarDTO.PaginacionDTO).ToListAsync();
            return mapper.Map<List<ClienteContratoDTO>>(contratos);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ClienteContratoCreacionDTO clientecontratoCreacionDTO)
        {
            var contrato = mapper.Map<ClienteContrato>(clientecontratoCreacionDTO);

            if (clientecontratoCreacionDTO.Documento != null)
            {
                contrato.Documento = await almacenadorArchivos.GuardarArchivo(contenedor, clientecontratoCreacionDTO.Documento);
            }

            context.Add(contrato);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("buscarPorNombre")]
        public async Task<ActionResult<List<CContratoDTO>>> BuscarPorNombre([FromBody] string codigoContrato)
        {
            if (string.IsNullOrWhiteSpace(codigoContrato)) { return new List<CContratoDTO>(); }
            return await context.Contratos
                .Where(x => x.CodigoContrato.Contains(codigoContrato))
                .Select(x => new CContratoDTO { Id = x.Id, CodigoContrato = x.CodigoContrato, Documento = x.Documento})
                .Take(5)
                .ToListAsync();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ClienteContratoCreacionDTO ContratoCreacionDTO)
        {
            var contrato = await context.Contratos.FirstOrDefaultAsync(x => x.Id == id);

            if (contrato == null)
            {
                return NotFound();
            }

            contrato = mapper.Map(ContratoCreacionDTO, contrato);

            if (ContratoCreacionDTO.Documento != null)
            {
                contrato.Documento = await almacenadorArchivos.EditarArchivo(contenedor, ContratoCreacionDTO.Documento, contrato.Documento);
            }

            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var contrato = await context.Contratos.FirstOrDefaultAsync(x => x.Id == id);

            if (contrato == null)
            {
                return NotFound();
            }

            context.Remove(contrato);
            await context.SaveChangesAsync();

            await almacenadorArchivos.BorrarArchivo(contrato.Documento, contenedor);

            return NoContent();
        }
    }
}
