﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AP002_2.Models
{
    public class Producto
    {
        public int idProducto { get; set; }
        public string nomProducto { get; set; }

        public decimal precioProducto { get; set; }
        public string nomCategoria { get; set; }
        public string nomProveedor { get; set; }
        public int stockProducto { get; set; }
    }
}