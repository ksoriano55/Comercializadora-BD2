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
    
    public partial class Inventario
    {
        public int InventarioId { get; set; }
        public int BodegaId { get; set; }
        public Nullable<int> ProductoId { get; set; }
        public Nullable<int> InsumoId { get; set; }
        public int Disponible { get; set; }
    
        public virtual Bodega Bodega { get; set; }
        public virtual Insumo Insumo { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
