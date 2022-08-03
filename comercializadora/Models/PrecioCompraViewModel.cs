using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using comercializadora.DataBase;

namespace comercializadora.Models
{
    public class PrecioCompraViewModel
    {
        public List<vPrecioCompraInsumos> vPrecioCompraInsumos { get; set; }
        public List<vPrecioCompraProductos> vPrecioCompraProductos { get; set; }
    }
}