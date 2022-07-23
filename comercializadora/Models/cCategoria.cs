using System;
using System.ComponentModel.DataAnnotations;

namespace comercializadora.Models
{
    [MetadataType(typeof(cCategoriasMetaData))]
    public partial class Categorias
    {

    }
    public class cCategoriasMetaData
    {
        public int categoriaId { get; set; }


        [Display(Name = "Nombre Categoria")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*El campo {0} es requerido")]
        public string descripcion { get; set; }
    }
}