using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using comercializadora.DataBase;
namespace comercializadora.Models
{
    public class InventariosViewModel
    {
        public List<vInventarioInsumos> vInventarioInsumos { get; set; }
        public List<vInventarioProductos> vInventarioProductos { get; set; }
    }
}