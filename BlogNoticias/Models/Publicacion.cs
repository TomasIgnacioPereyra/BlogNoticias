using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNoticias.Models
{
    public class Publicacion
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100 ,MinimumLength = 4)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Subtitulo { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Autor { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 10)]
        public string Cuerpo { set; get; }

    }
}
