using p1ACrud13.Clases.CapaDatos;
using p1ACrud13.Clases.entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1ACrud13.Clases.Servicio
{
    public class ServicioAlumno
    {
        ClsConexion cone = new ClsConexion();
        //public void promedio(string instruccion)
        //{

        //    DataTable respuesta = cone.consultaTablaDirecta(instruccion);
        //    int acumNotas = 0;

        //    foreach (DataRow dr in respuesta.Rows)
        //    {
        //        acumNotas = acumNotas + Convert.ToInt32(dr["parcial1"]);

        //    }

        //    int promedio = acumNotas / respuesta.Rows.Count;
        //    Console.WriteLine("Promedio=" + promedio);


        //}



        public void lee(string instruccion)
        {

            DataTable respuesta = cone.consultaTablaDirecta(instruccion);

            foreach (DataRow dr in respuesta.Rows)
            {
                Console.WriteLine($"Carnet={dr["carnet"]} nombre={dr["nombre"]} Seccion {dr["seccion"]}  P1={dr["parcial1"]}");
            }

        }




        public DataTable ConsultaSQL(string instruccion)
        {
            DataTable respuesta = cone.consultaTablaDirecta(instruccion);
            return respuesta;
        }








        //public void inserta(string nombre, string carnet, string correo, string clase, string seccion)
        //{

        //    ClsConexion conn = new ClsConexion();
        //    string instruccion = $"insert into tb_alumnos (carnet,nombre,correo,clase,seccion) values('{carnet}','{nombre}','{correo}','{clase}','{seccion}')";
        //    conn.EjecutarSQLDirecto(instruccion);
        //}



        //public void cambia()
        //{
        //    string instruccion = $"update tb_alumnos set parcial1 = 20 where carnet='21-11951'";
        //    cone.EjecutarSQLDirecto(instruccion);
        //}



        public int CrearAlumno(MdAlumnos alu)
        {
            string instruccion = $"insert into tb_alumnos (carnet,nombre,correo,clase,seccion, parcial1, parcial2, parcial3) values('{alu.carnet}','{alu.nombre}','{alu.correo}','{alu.clase}','{alu.seccion}',{alu.parcial1},{alu.parcial2},{alu.parcial3})";
            return cone.EjecutarSQLDirecto(instruccion);
        }


        public MdAlumnos ObtenerAlumno(string carnet)
        {
           
            MdAlumnos alu = new MdAlumnos();

            DataTable resp = cone.consultaTablaDirecta($"select * from tb_alumnos where carnet ='{carnet}'");
            if (resp.Rows.Count > 0)
            {
                foreach (DataRow item in resp.Rows)
                {
                    alu.idAlumno = Convert.ToInt32(item["idAlumno"]);
                    alu.carnet = item["carnet"].ToString();
                    alu.nombre = item["nombre"].ToString();
                    alu.seccion = item["seccion"].ToString();
                    alu.clase = item["clase"].ToString();
                    alu.correo = item["correo"].ToString();
                    alu.parcial1 = Convert.ToInt32(item["parcial1"]);
                    alu.parcial2 = Convert.ToInt32(item["parcial2"]);
                    alu.parcial3 = Convert.ToInt32(item["parcial3"]);

                }
                return alu;
            }
            else
            {
                return null;
            }
        }



        public int actualizarAlumno(MdAlumnos alu)
        {
            string instruccion = $"update tb_alumnos set nombre = '{alu.nombre}',correo = '{alu.correo}',clase = '{alu.clase}',seccion= '{alu.seccion}',parcial1= '{alu.parcial1}',parcial2= '{alu.parcial2}',parcial3= '{alu.parcial3}' where carnet='{alu.carnet}'";
            return cone.EjecutarSQLDirecto(instruccion);
        }
        public void EliminarAlumno(string carnet) 
        {
            string instruccion = $"Delete tb_alumnos where carnet='{carnet}'";
            cone.EjecutarSQLDirecto(instruccion);


        }


    }
}
