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
    
    public partial class Compra
    {
        public Compra()
        {
            this.CompraDetalle = new HashSet<CompraDetalle>();
            this.Pagos = new HashSet<Pagos>();
        }
    
        public int CompraID { get; set; }
        public int ProveedorID { get; set; }
        public string CodigoCompra { get; set; }
        public decimal ValorCompra { get; set; }
        public Nullable<decimal> SaldoPendiente { get; set; }
        public System.DateTime Fecha { get; set; }
        public Nullable<System.DateTime> FechaVencimiento { get; set; }
    
        public virtual ICollection<CompraDetalle> CompraDetalle { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual ICollection<Pagos> Pagos { get; set; }
    }
}
