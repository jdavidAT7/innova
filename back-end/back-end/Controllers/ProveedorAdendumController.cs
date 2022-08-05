using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using back_end.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Controllers
{
    [Route("api/adendumsp")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class ProveedorAdendumController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "adendumsp";

        public ProveedorAdendumController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProveedorAdendumDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Adendumsp.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var adendums = await queryable.OrderBy(x => x.CodigoAdendum).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<ProveedorAdendumDTO>>(adendums);
        }
/*
        [HttpGet("todos")]
        public async Task<ActionResult<List<ProveedorAdendumDTO>>> Todos()
        {
            var adendums = await context.Adendumsp.ToListAsync();
            return mapper.Map<List<ProveedorAdendumDTO>>(adendums);
        }
*/
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProveedorAdendumDTO>> Get(int id)
        {
            var adendum = await context.Adendumsp.FirstOrDefaultAsync(x => x.Id == id);

            if (adendum == null)
            {
                return NotFound();
            }

            return mapper.Map<ProveedorAdendumDTO>(adendum);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ProveedorAdendumCreacionDTO proveedorAdendumCreacionDTO)
        {
            var adendum = mapper.Map<ProveedorAdendum>(proveedorAdendumCreacionDTO);

            if (proveedorAdendumCreacionDTO.Documento != null)
            {
                adendum.Documento = await almacenadorArchivos.GuardarArchivo(contenedor, proveedorAdendumCreacionDTO.Documento);
            }

            context.Add(adendum);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ProveedorAdendumCreacionDTO proveedorAdendumCreacionDTO)
        {
            var adendum = await context.Adendumsp.FirstOrDefaultAsync(x => x.Id == id);

            if (adendum == null)
            {
                return NotFound();
            }

            adendum = mapper.Map(proveedorAdendumCreacionDTO, adendum);

            if (proveedorAdendumCreacionDTO.Documento != null)
            {
                adendum.Documento = await almacenadorArchivos.EditarArchivo(contenedor, proveedorAdendumCreacionDTO.Documento, adendum.Documento);
            }

            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var adendum = await context.Adendumsp.FirstOrDefaultAsync(x => x.Id == id);

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
