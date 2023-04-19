using Microsoft.AspNetCore.Mvc;

using AP002.Models;
using AP002.DataLayer;
using System.Data;

namespace AP002.Controllers
{
    /*actua como interfaz de usuario y logica de negocio*/
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            ProductoDAL _productoDal = new ProductoDAL();

            List<Producto> lista = new List<Producto>();

            lista = _productoDal.ProductoListarLS();

            return View(lista);
        }

        public IActionResult IndexDS()
        {
            ProductoDAL _productoDal = new ProductoDAL();

            DataSet tabla = _productoDal.ProductosListarDS();

            List<Producto> lista = new List<Producto>();

            /*tables[0] se refiere a la primera tabla*/
            /*la consulta sql puede devolver muchas tablas.. pero con este codigo
             estamos asumiendo que solo hay una*/
            /*en cada iteracion la fila contiene los datos..*/
            /*un datarow contiene una fila de datos de un datatable*/
            foreach(DataRow fila in tabla.Tables[0].Rows){
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
