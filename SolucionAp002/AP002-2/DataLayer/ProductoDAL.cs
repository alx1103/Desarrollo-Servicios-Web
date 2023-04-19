using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AP002_2.DataLayer
{
    public class ProductoDAL
    {
        /*conectarnos con la base de datos*/
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Negocios"].ConnectionString);
        
        public DataSet ProductoListar()
        {
            /*nos permite llamar al prodcemineto almacenado, le damos lo que ahremos, y la conexion*/
            SqlDataAdapter da = new SqlDataAdapter("usp_ProductoListar", cn);
            /*le estamos avisando que sera un procedimiento almacenado*/
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            /*creamos un ds para que se guarden los datos*/
            DataSet ds = new DataSet();
            /*de la clase adapter da le mandamos lo que obtenemos a ds*/
            da.Fill(ds);
            /*retornamos los datos en ds*/
            return ds;
        }
    
    }
}