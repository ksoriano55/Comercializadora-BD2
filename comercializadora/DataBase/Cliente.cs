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
    
    public partial class Cliente
    {
        public Cliente()
        {
            this.Factura = new HashSet<Factura>();
        }
    
        public int ClienteID { get; set; }
        public string Nombre { get; set; }
        public string RTN { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    
        public virtual ICollection<Factura> Factura { get; set; }
    }
}
