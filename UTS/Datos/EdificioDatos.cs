using UTS.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Identity.Client;
using System.Transactions;
using System.Drawing.Imaging;

namespace UTS.Datos
{
    public class EdificioDatos
    {
        public List<EdificioModel> Lista()
        {
            //crear una lista vacia
            var oLista = new List<EdificioModel>();
            // crear una instancia de la clase conexion
            var cn = new Conexion();
            //utilizar using para establecer la cadena de conexion
            using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
            {
                //abrir la conexion
                conexion.Open();
                //Comando a ejecutar
                SqlCommand cmd = new SqlCommand("SP_listar_edificios", conexion);
                //decir el tipo de comando
                cmd.CommandType = CommandType.StoredProcedure;
                //Leer el resultado de la ejecucion del procedimiento almacenado
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        //una ves se esten leyendo tambien los guardaremos en la lista
                        oLista.Add(new EdificioModel()
                        { //se utilizan las propiedades de la clase
                            numedificio = Convert.ToInt32(dr["numedificio"]),
                            cantaula = Convert.ToInt32(dr["cantaula"]),
                            encargado = dr["encargado"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }

        public EdificioModel ConsultarEdificio(int numedificio)
        {
            var oEdificio = new EdificioModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_consultar_edificio", conexion);
                cmd.Parameters.AddWithValue("numedificio", numedificio);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oEdificio.numedificio = Convert.ToInt32(dr["numedificio"]);
                        oEdificio.cantaula = Convert.ToInt32(dr["cantaula"]);
                        oEdificio.encargado = dr["encargado"].ToString();

                    }
                }
            }
            return oEdificio;
        }


        public bool GuardarEdificio(EdificioModel model)
        {
            //creo una variable boolean
            bool respuesta;
            try
            {
                var cn = new Conexion();
                //utlizar la cadena para establecer la cadena de conexion
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_insertar_edificios", conexion);
                    //enviado un parametro al procedimiento almacenado
                    cmd.Parameters.AddWithValue("numedificio", model.numedificio);
                    cmd.Parameters.AddWithValue("cantaula", model.cantaula);
                    cmd.Parameters.AddWithValue("encargado", model.encargado);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //ejecutar el procedimiento almacennado
                    cmd.ExecuteNonQuery();
                }
                //si no ocurre un error la variable respuesta sera true
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }

        public bool EditarEdificio(EdificioModel model)
        {
            //creo una variable boolean
            bool respuesta;
            try
            {
                var cn = new Conexion();
                //utlizar la cadena para establecer la cadena de conexion
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_actualizar_edificio", conexion);
                    //enviado un parametro al procedimiento almacenado
                    cmd.Parameters.AddWithValue("numedificio", model.numedificio);
                    cmd.Parameters.AddWithValue("cantaula", model.cantaula);
                    cmd.Parameters.AddWithValue("encargado", model.encargado);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //ejecutar el procedimiento almacennado
                    cmd.ExecuteNonQuery();
                }
                //si no ocurre un error la variable respuesta sera true
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }

        public bool EliminarEdificio(int numedificio)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_eliminar_edificio", conexion);
                    cmd.Parameters.AddWithValue("numedificio", numedificio);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }

    }

}

