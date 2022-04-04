using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace universidades.shared
{
    public partial class Universidade
    {
        [Key]
        [Column("IdUniversidade")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Display(Name = "Pais")]
        [Required(ErrorMessage = "Pais es Requerido")]
        public string Country { get; set; }
        [Display(Name = "Dominio")]
        public string Domains { get; set; }
        [Display(Name = "Codigo Regional")]
        public string AlphaTwoCode { get; set; }
        [Display(Name = "Pagina Web")]
        public string WebPages { get; set; }
        [Display(Name = "Imagen")]
        public string Imagen { get; set; }
        [Display(Name = "Universidad")]
        [Required(ErrorMessage = "Nombre de Universidad es Requerido")]
        public string Name { get; set; }

        public bool favorito { get; set; }
    }
}
