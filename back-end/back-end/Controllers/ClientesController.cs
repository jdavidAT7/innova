using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using back_end.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "clientes";

        public ClientesController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }
/*
        [HttpGet]
        public async Task<ActionResult<LandingPageDTO>> Get()
        {
            var top = 6;
            var hoy = DateTime.Today;

            var proximosEstrenos = await context.Peliculas
                .Where(x => x.FechaLanzamiento > hoy)
                .OrderBy(x => x.FechaLanzamiento)
                .Take(top)
                .ToListAsync();

            var enCines = await context.Peliculas
                .Where(x => x.EnCines)
                .OrderBy(x => x.FechaLanzamiento)
                .Take(top)
                .ToListAsync();

            var resultado = new LandingPageDTO();
            resultado.ProximosEstrenos = mapper.Map<List<PeliculaDTO>>(proximosEstrenos);
            resultado.EnCines = mapper.Map<List<PeliculaDTO>>(enCines);

            return resultado;
        }
*/
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            var cliente = await context.Clientes
                .Include(x => x.ClientesAdendums).ThenInclude(x => x.Adendum)
                .Include(x => x.CContratos).ThenInclude(x => x.Contrato)
                .Include(x => x.ClientesServicios).ThenInclude(x => x.Servicio)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null) { return NotFound(); }

            var dto = mapper.Map<ClienteDTO>(cliente);
            dto.Contratos = dto.Contratos.OrderBy(x => x.Orden).ToList();
            return dto;
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<List<ClienteDTO>>> Filtrar([FromQuery] ClientesFiltrarDTO clientesFiltrarDTO)
        {
            var clientesQueryable = context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(clientesFiltrarDTO.NombreComercial))
            {
                clientesQueryable = clientesQueryable.Where(x => x.NombreComercial.Contains(clientesFiltrarDTO.NombreComercial));
            }
            /*
                        if (clientesFiltrarDTO.Estado)
                        {
                            clientesQueryable = clientesQueryable.Where(x => x.Estado);
                        }

                        if (clientesFiltrarDTO.SiguientesRegistros)
                        {
                            var hoy = DateTime.Today;
                            clientesQueryable = clientesQueryable.Where(x => x.FechaVencimiento> hoy);
                        }
            */
            if (clientesFiltrarDTO.AdendumId != 0)
            {
                clientesQueryable = clientesQueryable
                    .Where(x => x.ClientesAdendums.Select(y => y.AdendumId)
                    .Contains(clientesFiltrarDTO.AdendumId));
            }

            await HttpContext.InsertarParametrosPaginacionEnCabecera(clientesQueryable);

            var clientes = await clientesQueryable.Paginar(clientesFiltrarDTO.PaginacionDTO).ToListAsync();
            return mapper.Map<List<ClienteDTO>>(clientes);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromForm] ClienteCreacionDTO clienteCreacionDTO)
        {
            var cliente = mapper.Map<Cliente>(clienteCreacionDTO);

            //if (clienteCreacionDTO.Documento != null)
            //{
            //    cliente.Poster = await almacenadorArchivos.GuardarArchivo(contenedor, clienteCreacionDTO.Poster);
            //}

            EscribirOrdenActores(cliente);

            context.Add(cliente);
            await context.SaveChangesAsync();
            return cliente.Id;
        }

        [HttpGet("PostGet")]
        public async Task<ActionResult<ClientesPostGetDTO>> PostGet()
        {
            var servicios = await context.Servicios.ToListAsync();
            var adendums = await context.Adendums.ToListAsync();

            var serviciosDTO = mapper.Map<List<ServicioDTO>>(servicios);
            var adendumsDTO = mapper.Map<List<ClienteAdendumDTO>>(adendums);

            return new ClientesPostGetDTO() { Servicios = serviciosDTO, Adendums = adendumsDTO };
        }

        [HttpGet("PutGet/{id:int}")]
        public async Task<ActionResult<ClientesPutGetDTO>> PutGet(int id)
        {
            var clienteActionResult = await Get(id);
            if (clienteActionResult.Result is NotFoundResult) { return NotFound(); }

            var cliente = clienteActionResult.Value;

            var adendumsSeleccionadosIds = cliente.Adendums.Select(x => x.Id).ToList();
            var adendumsNoSeleccionados = await context.Adendums
                .Where(x => !adendumsSeleccionadosIds.Contains(x.Id))
                .ToListAsync();

            var serviciosSeleccionadosIds = cliente.Servicios.Select(x => x.Id).ToList();
            var serviciosNoSeleccionados = await context.Servicios
                .Where(x => !serviciosSeleccionadosIds.Contains(x.Id))
                .ToListAsync();

            var adendumsNoSeleccionadosDTO = mapper.Map<List<ClienteAdendumDTO>>(adendumsNoSeleccionados);
            var serviciosNoSeleccionadosDTO = mapper.Map<List<ServicioDTO>>(serviciosNoSeleccionados);

            var respuesta = new ClientesPutGetDTO();
            respuesta.Cliente = cliente;
            respuesta.AdendumsSeleccionados = cliente.Adendums;
            respuesta.AdendumsNoSeleccionados = adendumsNoSeleccionadosDTO;
            respuesta.ServiciosSeleccionados = cliente.Servicios;
            respuesta.ServiciosNoSeleccionados = serviciosNoSeleccionadosDTO;
            respuesta.Contratos = cliente.Contratos;
            return respuesta;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ClienteCreacionDTO clienteCreacionDTO)
        {
            var cliente = await context.Clientes
                .Include(x => x.CContratos)
                .Include(x => x.ClientesAdendums)
                .Include(x => x.ClientesServicios)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente = mapper.Map(clienteCreacionDTO, cliente);

            //if (peliculaCreacionDTO.Poster != null)
            //{
            //    pelicula.Poster = await almacenadorArchivos.EditarArchivo(contenedor, peliculaCreacionDTO.Poster, pelicula.Poster);
            //}

            EscribirOrdenActores(cliente);

            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            context.Remove(cliente);
            await context.SaveChangesAsync();

            //await almacenadorArchivos.BorrarArchivo(cliente.Poster, contenedor);

            return NoContent();
        }


        private void EscribirOrdenActores(Cliente cliente)
        {
            if (cliente.CContratos != null)
            {
                for (int i = 0; i < cliente.CContratos.Count; i++)
                {
                    cliente.CContratos[i].Orden = i;
                }
            }
        }

    }
}


