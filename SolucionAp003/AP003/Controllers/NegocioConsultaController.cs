using Microsoft.AspNetCore.Mvc;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using AP003.Models;


namespace AP003.Controllers
{
    public class NegocioConsultaController : Controller
    {
        /*este tipo de configuracion es solo lectura y solo puede tener un valor inicialmente unico*/
        public readonly IConfiguration? _config;

        public NegocioConsultaController(IConfiguration? config)
        {
            _config = config;
        }

        public async Task<IActionResult> IndexBuscarProductos(string nombre="queso")
        {
            return View(await Task.Run(() => buscaProductos(nombre)));
        }

        IEnumerable<ProductoModel> buscaProductos(string nombre)
        {
            List<ProductoModel> productos = new List<ProductoModel>();

            if (string.IsNullOrEmpty(nombre))
            {
                return productos;
            }

            using(SqlConnection cn = new(_config["ConnectionStrings:cnDB"]))
            {
                SqlCommand cmd = new SqlCommand("usp_Producto_Buscar", cn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cn.Open();

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
    }
}
