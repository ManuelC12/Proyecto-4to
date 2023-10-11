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
            var oLista = new List<EdificioModel>();

            var cn = new Conexion();

            // Establecemos una conexión a la base de datos utilizando la cadena de conexión proporcionada por 'cn.getAulasUTSContext()'.
            using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
            {
                conexion.Open();

                // Creamos un nuevo comando SQL que ejecutará un procedimiento almacenado llamado "SP_listar_edificios" en la base de datos.
                SqlCommand cmd = new SqlCommand("SP_listar_edificios", conexion);

                // Se ejecuta un procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                // Utilizamos un lector de datos (DataReader) para obtener los resultados de la consulta almacenada en 'dr'.
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // Creamos una instancia de 'EdificioModel' y la agregamos a la lista 'oLista' con los valores recuperados del lector de datos.
                        oLista.Add(new EdificioModel()
                        {
                            numedificio = Convert.ToInt32(dr["numedificio"]),
                            cantaula = Convert.ToInt32(dr["cantaula"]),
                            encargado = dr["encargado"].ToString()
                        });
                    }
                }
            }

            // Se retorna la lista de edificios
            return oLista;
        }

        public EdificioModel ConsultarEdificio(int numedificio)
        {
            var oEdificio = new EdificioModel();

            var cn = new Conexion();

            // Establecemos una conexión a la base de datos utilizando la cadena de conexión proporcionada por 'cn.getAulasUTSContext()'.
            using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
            {
                conexion.Open();

                // Creamos un nuevo comando SQL que ejecutará un procedimiento almacenado llamado "SP_consultar_edificio" en la base de datos.
                SqlCommand cmd = new SqlCommand("SP_consultar_edificio", conexion);

                // Agregamos un parámetro al comando SQL para especificar el número de edificio que deseamos consultar.
                cmd.Parameters.AddWithValue("numedificio", numedificio);

                // Se ejecuta un procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                // Utilizamos un lector de datos (DataReader) para obtener los resultados de la consulta almacenada en 'dr'.
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // Asignamos los valores recuperados del lector de datos a la instancia 'oEdificio' de la clase 'EdificioModel'.
                        oEdificio.numedificio = Convert.ToInt32(dr["numedificio"]);
                        oEdificio.cantaula = Convert.ToInt32(dr["cantaula"]);
                        oEdificio.encargado = dr["encargado"].ToString();
                    }
                }
            }

            // Devolvemos la información del edificio.
            return oEdificio;
        }

        public bool GuardarEdificio(EdificioModel model)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                // Establecemos una conexión a la base de datos utilizando la cadena de conexión proporcionada por 'cn.getAulasUTSContext()'.
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();

                    // Creamos un nuevo comando SQL que ejecutará un procedimiento almacenado llamado "SP_insertar_edificios" en la base de datos.
                    SqlCommand cmd = new SqlCommand("SP_insertar_edificios", conexion);

                    // Agregamos parámetros al comando SQL con los valores obtenidos de la instancia 'model' de la clase 'EdificioModel'.
                    cmd.Parameters.AddWithValue("numedificio", model.numedificio);
                    cmd.Parameters.AddWithValue("cantaula", model.cantaula);
                    cmd.Parameters.AddWithValue("encargado", model.encargado);

                    // Se ejecuta un procedimiento almacenado
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

        public bool EditarEdificio(EdificioModel model)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                // Establecemos una conexión a la base de datos utilizando la cadena de conexión proporcionada por 'cn.getAulasUTSContext()'.
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();

                    // Creamos un nuevo comando SQL que ejecutará un procedimiento almacenado llamado "SP_actualizar_edificio" en la base de datos.
                    SqlCommand cmd = new SqlCommand("SP_actualizar_edificio", conexion);

                    // Agregamos parámetros al comando SQL con los valores obtenidos de la instancia 'model' de la clase 'EdificioModel'.
                    cmd.Parameters.AddWithValue("numedificio", model.numedificio);
                    cmd.Parameters.AddWithValue("cantaula", model.cantaula);
                    cmd.Parameters.AddWithValue("encargado", model.encargado);

                    // Se ejecuta un porcedimiento almacenado
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

        public bool EliminarEdificio(int numedificio)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                // Establecemos una conexión a la base de datos utilizando la cadena de conexión proporcionada por 'cn.getAulasUTSContext()'.
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();

                    // Creamos un nuevo comando SQL que ejecutará un procedimiento almacenado llamado "SP_eliminar_edificio" en la base de datos.
                    SqlCommand cmd = new SqlCommand("SP_eliminar_edificio", conexion);

                    // Agregamos un parámetro al comando SQL para especificar el número de edificio que deseamos eliminar.
                    cmd.Parameters.AddWithValue("numedificio", numedificio);

                    // Se ejecuta un procedimiento almacenado
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

