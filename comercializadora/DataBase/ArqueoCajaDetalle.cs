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
    
    public partial class ArqueoCajaDetalle
    {
        public int ArqueoDetalleID { get; set; }
        public string ArqueoCaja { get; set; }
        public Nullable<int> EfectivoArqueoID { get; set; }
        public Nullable<int> ChequeArqueoID { get; set; }
        public Nullable<decimal> Monto { get; set; }
    
        public virtual ArqueoCaja ArqueoCaja1 { get; set; }
        public virtual ChequeArqueo ChequeArqueo { get; set; }
        public virtual EfectivoArqueo EfectivoArqueo { get; set; }
    }
}
