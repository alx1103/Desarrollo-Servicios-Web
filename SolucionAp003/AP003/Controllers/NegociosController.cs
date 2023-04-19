using Microsoft.AspNetCore.Mvc;
using AP003.Models;
using Microsoft.Data;
using Microsoft.Data.SqlClient;

namespace AP003.Controllers
{
    public class NegociosController : Controller
    {
        /*readonly significa que es una variable de lectura*/
        public readonly IConfiguration? _config;

        /*constructor*/
        public NegociosController(IConfiguration IConfig)
        {
            _config = IConfig;
        }


        IEnumerable<ProductoModel> listadoProductos()
        {
            List<ProductoModel> productos = new List<ProductoModel>();
            
            using (SqlConnection cn = new(_config["ConnectionStrings:cnDB"]))
            {
                /*una consulta que se va a ejecutar en la base de datos*/
                SqlCommand cmd = new SqlCommand("usp_ProductoListar", cn);
                cn.Open();
                /*se ejecuta la consulta*/
                /*SqlDataReader lee el resultado de la consulta*/
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productos.Add(new ProductoModel
                    {
                        IdProducto = reader.GetInt32(0),
                        NombreProducto = reader.GetString(1),
                        PrecioUnidad = reader.GetDecimal(2),
                        NombreCategoria = reader.GetString(3),
                        NombreProveedor = reader.GetString(4),
                        stcokProducto = reader.GetInt16(5)
                    });
                }
            }
            return productos;
        }

        /*asy asincrono*/
        /*Task.run crea una tarea en segundo plano para ejecutar el metodo listado..*/
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => listadoProductos()));
        }

        /*Paginacion*/
        public async Task<IActionResult> Paginacion(int p)
        {
            int f = 12;
            int c = listadoProductos().Count();

            int paginas = c % f > 0 ? c / f + 1 : c / f;

            ViewBag.paginas = paginas;

            /*el metodo skip es para ignorar los primeros productos de ese resultado de la multiplicacion
             y despues de eso tomo los 12*/
            return View(await Task.Run(() => listadoProductos().Skip(p * f).Take(f)));
        }












    }
}
