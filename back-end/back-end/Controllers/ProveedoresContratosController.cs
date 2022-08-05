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

    [Route("api/contratosp")]
    [ApiController]
    public class ProveedoresContratosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "contratosp";

        public ProveedoresContratosController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProveedorContratoDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Contratosp.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var contratos = await queryable.OrderBy(x => x.CodigoContrato).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<ProveedorContratoDTO>>(contratos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProveedorContratoDTO>> Get(int id)
        {
            var contrato = await context.Contratosp.FirstOrDefaultAsync(x => x.Id == id);

            if (contrato == null)
            {
                return NotFound();
            }

            return mapper.Map<ProveedorContratoDTO>(contrato);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ProveedorContratoCreacionDTO proveedorcontratoCreacionDTO)
        {
            var contrato = mapper.Map<ProveedorContrato>(proveedorcontratoCreacionDTO);

            if (proveedorcontratoCreacionDTO.Documento != null)
            {
                contrato.Documento = await almacenadorArchivos.GuardarArchivo(contenedor, proveedorcontratoCreacionDTO.Documento);
            }

            context.Add(contrato);
            await context.SaveChangesAsync();
            return NoContent();
        }

        /*
        [HttpPost("buscarPorNombre")]
        public async Task<ActionResult<List<CContratoDTO>>> BuscarPorNombre([FromBody] string codigoContrato)
        {
            if (string.IsNullOrWhiteSpace(codigoContrato)) { return new List<CContratoDTO>(); }
            return await context.Contratos
                .Where(x => x.CodigoContrato.Contains(codigoContrato))
                .Select(x => new CContratoDTO { Id = x.Id, CodigoContrato = x.CodigoContrato, Documento = x.Documento })
                .Take(5)
                .ToListAsync();
        }
        */

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ProveedorContratoCreacionDTO ContratoCreacionDTO)
        {
            var contrato = await context.Contratosp.FirstOrDefaultAsync(x => x.Id == id);

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
            var contrato = await context.Contratosp.FirstOrDefaultAsync(x => x.Id == id);

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
