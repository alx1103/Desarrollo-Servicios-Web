using System.Data.SqlClient;
using AP002.Models;
using System.Data;

/*agregar*/
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AP002.DataLayer

{
    /*DataLayer es la capa de acceso de datos, que se usa para interactuar con la base de datos
     Es responsable de realiar las operacionesCrud*/
    public class ProductoDAL
    {
        /*el valor inicial de la cadena es vacía*/
        public String cnn = String.Empty;

        /*constructor*/
        public ProductoDAL(){
            
            /*carga la configuracion de la aplicacion desde un archivo JSON*/
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            
            cnn = builder.GetSection("ConnectionStrings:DefaultConnection").Value;
        }

        /*esta funcion devolverá un DataSet*/
        /*un DataSet es una colección de DataTable*/
        /*DataSet es un conjunto de Datos*/
        public DataSet ProductosListarDS()
        {
            /*Para conectarse a la base de datos. Para la conexión*/
            SqlConnection cn = new SqlConnection(cnn);

            /*se utiliza para ejecutar consultas de la base de datos*/
            /*nos permite llamar a un procedimiento almacenado*/
            SqlDataAdapter da = new SqlDataAdapter("usp_ProductoListar", cn);

            /*le dices al adaptador que es un procedimiento almacenado*/
            /*da debe ejecutar un procedimiento almacenado*/
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            /*contenedor que se utiliza para almacenar el resultado de la consulta*/
            /*un conjunto de datos*/
            DataSet ds = new DataSet();

            /*llena el objeto Ds, Dt con los resultados obtenidos*/
            /*fill pertence a la clase SqlDataAdapter*/
            /*el adaptador llena ese dataSet con la informacion que le dio el procedimiento almacenado*/
            da.Fill(ds);

            /*retorna el dataSet con la informacion*/
            return ds;
        }

        /*nos devolverá una lista de productos*/
        public List<Producto> ProductoListarLS()
        {
            /*para conectarnos con la bd*/
            SqlConnection cn = new SqlConnection(cnn);

            /*lo que nos va a retonar*/
            /*ES UNA LISTA VACIA*/
            List<Producto> lista = new List<Producto>();

            /*también permite ejecutar procedimiento almacenados*/
            SqlCommand cmd = new SqlCommand("usp_ProductoListar", cn);

            /*se ejecuta el procedimiento almacenado*/
            /*indicas que es un procedimiento almacenado*/
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();

            /*leer la colección, la informacion que trae el procediminetoAlmacenado
             y la pondras en la lista*/
            /*en este dr ya tengo la información*/
            IDataReader dr = cmd.ExecuteReader();

            /*mientras encuentre lectura, filas*/
            while (dr.Read())
            {
                lista.Add(new Producto()
                {
                    idProducto = int.Parse(dr[0].ToString()),
                    nomProducto = dr[1].ToString(),
                    precioProducto = decimal.Parse(dr[2].ToString()),
                    nomCategoria = dr[3].ToString(),
                    nomProveedor = dr[4].ToString(),
                    stockProducto = int.Parse(dr[5].ToString())
                });
            }
            cn.Close();
            return lista;
        }







    }
}  