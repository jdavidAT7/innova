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
    [Route("api/servicios")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ServiciosController(
            //ILogger<ClientesController> logger,
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServicioDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Servicios.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var servicios = await queryable.OrderBy(x => x.TipoServicio).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<ServicioDTO>>(servicios);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServicioDTO>> Get(int id)
        {
            var servicio = await context.Servicios.FirstOrDefaultAsync(x => x.Id == id);

            if (servicio == null)
            {
                return NotFound();
            }

            return mapper.Map<ServicioDTO>(servicio);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ServicioCreacionDTO servicioCreacionDTO)
        {
            var servicio = mapper.Map<Servicio>(servicioCreacionDTO);
            context.Add(servicio);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ServicioCreacionDTO servicioCreacionDTO)
        {
            var servicio = await context.Servicios.FirstOrDefaultAsync(x => x.Id == id);

            if (servicio == null)
            {
                return NotFound();
            }

            servicio = mapper.Map(servicioCreacionDTO, servicio);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Servicios.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Servicio() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
