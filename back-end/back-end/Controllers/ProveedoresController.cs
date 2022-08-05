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
    [Route("api/proveedores")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public ProveedoresController(
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProveedorDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Proveedores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var proveedores = await queryable.OrderBy(x => x.Id).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<ProveedorDTO>>(proveedores);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProveedorDTO>> Get(int id)
        {
            var proveedor = await context.Proveedores.FirstOrDefaultAsync(x => x.Id == id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return mapper.Map<ProveedorDTO>(proveedor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProveedorCreacionDTO proveedorCreacionDTO)
        {
            var proveedor = mapper.Map<Proveedor>(proveedorCreacionDTO);
            context.Add(proveedor);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProveedorCreacionDTO proveedorCreacionDTO)
        {
            var proveedor = await context.Proveedores.FirstOrDefaultAsync(x => x.Id == id);

            if (proveedor == null)
            {
                return NotFound();
            }

            proveedor = mapper.Map(proveedorCreacionDTO, proveedor);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Proveedores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Proveedor() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
