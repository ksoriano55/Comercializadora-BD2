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
    
    public partial class Pagos
    {
        public int PagoId { get; set; }
        public Nullable<int> ProductorId { get; set; }
        public Nullable<int> ProveedorId { get; set; }
        public int cosechaId { get; set; }
        public Nullable<int> CompraId { get; set; }
        public string Concepto { get; set; }
        public int TipoPagoId { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
    
        public virtual Productor Productor { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual TipoPago TipoPago { get; set; }
        public virtual Compra Compra { get; set; }
        public virtual Cosecha Cosecha { get; set; }
    }
}
