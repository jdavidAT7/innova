using back_end.Utilidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace back_end.DTOs
{
    public class ProveedorCreacionDTO
    {
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string ContactoComercial { get; set; }
        public string PaginaWeb { get; set; }

        [Phone(ErrorMessage = "No es un número de teléfono válido")]
        public string Telefono { get; set; }
        public string Nit { get; set; }
        [EmailAddress(ErrorMessage = "No es una dirección de correo electrónico válida.")]
        public string Email { get; set; }


    }
}
