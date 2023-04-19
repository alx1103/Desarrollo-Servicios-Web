using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
/*importar el modelo*/
using AP004.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AP004.Controllers
{
    public class NegociosController : Controller
    {
        /*esta es una variable de solo lectura*/
        public readonly IConfiguration? _configuration;

        public NegociosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        /*IEnumerable es una interfaz*/
        IEnumerable<Cliente> listarClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            using (SqlConnection cn = new(_configuration["ConnectionStrings:cnDB"]))
            {
                /*esto se utiliza para ejecutar consultas sql de una base de datos*/
                SqlCommand cmd = new SqlCommand("usp_ClienteListar", cn);
                cn.Open();

                /*lee los resultados de una consulta; en este caso en cmd ejecuta la consulta, y reader
                 va a leer el resultado de esa consulta*/
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Cliente
                    {
                        idCliente = reader.GetString(0),
                        nomCliente = reader.GetString(1),
                    }
                    );
                };
                cn.Close();
            }
            return lista;
        }

        IEnumerable<Pedidos> listaPedidosCliente(String idCliente)
        {
            if (string.IsNullOrEmpty(idCliente)) idCliente = String.Empty;

            List<Pedidos> lista = new List<Pedidos>();

            using (SqlConnection cn = new(_configuration["ConnectionStrings:cnDB"]))
            {
                /*SqlComand es para que se ejecute el procedimiento almacenado*/
                SqlCommand cmd = new SqlCommand("usp_PedidosXCliente", cn);
                
                /*para darle el parametro*/
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcliente", idCliente);
                cn.Open();

                /*para que lea lo que arrojo la ejecucion*/
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Pedidos
                    {
                        idPedido = reader.GetInt32(0),
                        fechaPedido = reader.GetDateTime(1),
                        nomCliente = reader.GetString(2),
                        cuidadDestinatario = reader.GetString(3),
                        paisDestinatario= reader.GetString(4)
                    });
                    
                };
                cn.Close();

            }
            return lista;
        }


        public async Task<IActionResult> Index(string idcliente)
        {

            /*esa informacion de los parametros vienen del procedure*/
            ViewBag.clientes = new SelectList(listarClientes(), "idCliente","nomCliente");

            return View(await Task.Run(()=> listaPedidosCliente(idcliente)));
        }
           
        


    }
}
