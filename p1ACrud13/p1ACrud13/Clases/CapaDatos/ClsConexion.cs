using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1ACrud13.Clases.CapaDatos
{
    public class ClsConexion
    {

        private string NombreServidor = "UMG-LAB\\SQLEXPRESS";
        private string NombreBaseDatos = "programacion1";
        
        public SqlConnection conexion;
        private String CadenaConexion { get; }



        public ClsConexion()
        {
            CadenaConexion = $"Data Source={NombreServidor};Initial Catalog={NombreBaseDatos};Integrated Security=True";
        }




        public DataTable consultaTablaDirecta(String sqll)
        {
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                try
                {
                    conexion.Open();
                    SqlDataReader dr;
                    SqlCommand cmd = new SqlCommand(sqll,conexion);
                    dr = cmd.ExecuteReader();

                    var dataTable = new DataTable();
                    dataTable.Load(dr);
                    return dataTable;

                } catch (Exception ex)
                {
                    Console.WriteLine($"ups hubo un clavo en tabla directa {ex.Message}");
                    return null;
                }
            }
        }




        public int EjecutarSQLDirecto(String sqll)
        {
            int status = -1;
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                try
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(sqll, conexion);
                     status = cmd.ExecuteNonQuery();
                     return status;

                }
                catch (Exception ex)
                {
                    return status;
                    //Console.WriteLine($"ups hubo un clavo sqldirecto {ex.Message}");
                }
            }
        }







    }
}
