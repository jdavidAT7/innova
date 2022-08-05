using back_end.Utilidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace back_end.DTOs
{
    public class ClienteCreacionDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        public string NombreComercial { get; set; }

        //public bool Estado { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 25)]
        public string ContactoCliente { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 10)]
        [Phone(ErrorMessage = "No es un número de teléfono válido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 15)]
        public string Nit { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 30)]
        [EmailAddress(ErrorMessage = "No es una dirección de correo electrónico válida.")]
        public string Email { get; set; }


        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> AdendumsIds { get; set; }


        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> ServiciosIds { get; set; }


        [ModelBinder(BinderType = typeof(TypeBinder<List<ContratoClienteCreacionDTO>>))]
        public List<ContratoClienteCreacionDTO> Contratos { get; set; }
    }
}
