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
    
    public partial class CuentaBancaria
    {
        public CuentaBancaria()
        {
            this.Productor = new HashSet<Productor>();
        }
    
        public int CuantaID { get; set; }
        public string Banco { get; set; }
        public string NumeroCuenta { get; set; }
    
        public virtual ICollection<Productor> Productor { get; set; }
    }
}
