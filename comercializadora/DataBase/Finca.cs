//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace comercializadora.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Finca
    {
        public Finca()
        {
            this.Lotes = new HashSet<Lotes>();
        }
    
        public int FincaID { get; set; }
        public string Nombre { get; set; }
        public int ProductorID { get; set; }
    
        public virtual ICollection<Lotes> Lotes { get; set; }
        public virtual Productor Productor { get; set; }
    }
}
