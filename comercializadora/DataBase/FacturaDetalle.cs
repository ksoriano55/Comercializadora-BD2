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
    
    public partial class FacturaDetalle
    {
        public int FacturaDetalleId { get; set; }
        public int FacturaID { get; set; }
        public Nullable<int> ProductoID { get; set; }
        public Nullable<int> InsumoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    
        public virtual Insumo Insumo { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Factura Factura { get; set; }
    }
}
