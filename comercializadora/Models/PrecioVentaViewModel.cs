using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using comercializadora.DataBase;

namespace comercializadora.Models
{
    public class PrecioVentaViewModel
    {
        public List<vPrecioVentaInsumos> vPrecioVentaInsumos { get; set; }
        public List<vPrecioVentaProductos> vPrecioVentaProductos { get; set; }
    }
}