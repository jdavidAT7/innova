using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProveedorAdendum, ProveedorAdendumDTO>().ReverseMap();
            CreateMap<ProveedorAdendumCreacionDTO, ProveedorAdendum>()
                .ForMember(x => x.Documento, options => options.Ignore());

            CreateMap<Proveedor, ProveedorDTO>().ReverseMap();
            CreateMap<ProveedorCreacionDTO, Proveedor>();

            CreateMap<ProveedorContrato, ProveedorContratoDTO>().ReverseMap();
            CreateMap<ProveedorContratoCreacionDTO, ProveedorContrato>()
                .ForMember(x => x.Documento, options => options.Ignore());

            CreateMap<ClienteAdendum, ClienteAdendumDTO>().ReverseMap();
            CreateMap<ClienteAdendumCreacionDTO, ClienteAdendum>()
                .ForMember(x => x.Documento, options => options.Ignore());


            CreateMap<ClienteContrato, ClienteContratoDTO>().ReverseMap();
            CreateMap<ClienteContratoCreacionDTO, ClienteContrato>()
                .ForMember(x => x.Documento, options => options.Ignore());


            CreateMap<ServicioCreacionDTO, Servicio>().ReverseMap();
            CreateMap<Servicio, ServicioDTO>();


            CreateMap<ProveedorCreacionDTO, Proveedor>();
            CreateMap<Proveedor, ProveedorDTO>().ReverseMap();




            CreateMap<ClienteCreacionDTO, Cliente>()
                .ForMember(x => x.ClientesAdendums, opciones => opciones.MapFrom(MapearClientesAdendums))
                .ForMember(x => x.ClientesServicios, opciones => opciones.MapFrom(MapearClientesServicios))
                .ForMember(x => x.CContratos, opciones => opciones.MapFrom(MapearCContratos));

            CreateMap<Cliente, ClienteDTO>()
                .ForMember(x => x.Adendums, options => options.MapFrom(MapearClientesAdendums))
                .ForMember(x => x.Contratos, options => options.MapFrom(MapearCContratos))
                .ForMember(x => x.Servicios, options => options.MapFrom(MapearClientesServicios));

            CreateMap<IdentityUser, UsuarioDTO>();

        }


        private List<ServicioDTO> MapearClientesServicios(Cliente cliente, ClienteDTO clienteDTO)
        {
            var resultado = new List<ServicioDTO>();

            if (cliente.ClientesServicios != null)
            {
                foreach (var clientesServicios in cliente.ClientesServicios)
                {
                    resultado.Add(new ServicioDTO()
                    {
                        Id = clientesServicios.ServicioId,
                        CodigoServicio = clientesServicios.Servicio.CodigoServicio,
                        TipoServicio = clientesServicios.Servicio.TipoServicio,
                        FechaActivacion = clientesServicios.Servicio.FechaActivacion,
                        FechaExpiracion = clientesServicios.Servicio.FechaExpiracion
                    });
                }
            }

            return resultado;
        }

        private List<CContratoDTO> MapearCContratos(Cliente cliente, ClienteDTO clienteDTO)
        {
            var resultado = new List<CContratoDTO>();

            if (cliente.CContratos != null)
            {
                foreach (var contratoClientes in cliente.CContratos)
                {
                    resultado.Add(new CContratoDTO()
                    {
                        Id = contratoClientes.ContratoId,
                        CodigoContrato = contratoClientes.Contrato.CodigoContrato,
                        Documento = contratoClientes.Contrato.Documento,
                        Orden = contratoClientes.Orden,
                        Personaje = contratoClientes.Personaje
                    });
                }
            }

            return resultado;
        }

        private List<ClienteAdendumDTO> MapearClientesAdendums(Cliente cliente, ClienteDTO clienteDTO)
        {
            var resultado = new List<ClienteAdendumDTO>();

            if (cliente.ClientesAdendums != null)
            {
                foreach (var adendum in cliente.ClientesAdendums)
                {
                    resultado.Add(new ClienteAdendumDTO()
                    {
                        Id = adendum.AdendumId,
                        CodigoAdendum = adendum.Adendum.CodigoAdendum,
                        FechaAdendum = adendum.Adendum.FechaAdendum,
                        FechaActivacion = adendum.Adendum.FechaActivacion,
                        FechaExpiracion = adendum.Adendum.FechaExpiracion,
                        Observacion = adendum.Adendum.Observacion,
                        Documento = adendum.Adendum.Documento
                    }) ;
                }
            }

            return resultado;
        }

        private List<CContratos> MapearCContratos(ClienteCreacionDTO clienteCreacionDTO,
            Cliente cliente)
        {
            var resultado = new List<CContratos>();

            if (clienteCreacionDTO.Contratos == null) { return resultado; }

            foreach (var actor in clienteCreacionDTO.Contratos)
            {
                resultado.Add(new CContratos() 
                { 
                    ContratoId = actor.Id, 
                    Personaje = actor.Personaje 
                });
            }

            return resultado;
        }

        private List<ClientesAdendums> MapearClientesAdendums(ClienteCreacionDTO clienteCreacionDTO,
            Cliente cliente)
        {
            var resultado = new List<ClientesAdendums>();

            if (clienteCreacionDTO.AdendumsIds == null) { return resultado; }

            foreach (var id in clienteCreacionDTO.AdendumsIds)
            {
                resultado.Add(new ClientesAdendums() { AdendumId = id });
            }

            return resultado;
        }

        private List<ClientesServicios> MapearClientesServicios(ClienteCreacionDTO clienteCreacionDTO,
           Cliente cliente)
        {
            var resultado = new List<ClientesServicios>();

            if (clienteCreacionDTO.ServiciosIds == null) { return resultado; }

            foreach (var id in clienteCreacionDTO.ServiciosIds)
            {
                resultado.Add(new ClientesServicios() { ServicioId = id });
            }

            return resultado;
        }
    }
}
