 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/*porque aqui esta la estructura que voy a utilizar*/
using AP002_2.Models;
using AP002_2.DataLayer;
using System.Data;

namespace AP002_2.Controllers
{
    public class ProductoController : Controller
    {
        ProductoDAL _productoDal = new ProductoDAL();
        // GET: Producto
        public ActionResult Index()
        {
            ProductoDAL _productoDal = new ProductoDAL();

            DataSet tabla = _productoDal.ProductoListar();

            List<Producto> lista = new List<Producto>();

            /*tables[0] se refiere a la primera tabla*/
            /*la consulta sql puede devolver muchas tablas.. pero con este codigo
             estamos asumiendo que solo hay una*/
            /*en cada iteracion la fila contiene los datos..*/
            /*un datarow contiene una fila de datos de un datatable*/
            foreach (DataRow fila in tabla.Tables[0].Rows)
            {
                lista.Add(new Producto
                {
                    idProducto = int.Parse(fila[0].ToString()),
                    nomProducto = fila[1].ToString(),
                    precioProducto = decimal.Parse(fila[2].ToString()),
                    nomCategoria = fila[3].ToString(),
                    nomProveedor = fila[4].ToString(),
                    stockProducto = int.Parse(fila[5].ToString())
                });
            }
            return View(lista);
        }
    }
}