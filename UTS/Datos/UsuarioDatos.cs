using UTS.Models;
using System.Data.SqlClient;
using System.Data;
using UTS.Datos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UTS.Models
{
    public class UsuarioDatos
    {
        public List<UsuarioModel> Listar()
        {
            // Creamos una lista para almacenar los usuarios recuperados.
            var oLista = new List<UsuarioModel>();

            var cn = new Conexion();

            // Establecemos una conexión a la base de datos utilizando 'cn.getAulasUTSContext()'
            using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
            {
                conexion.Open();

                // llamar el procedimiento almacenado "SP_consultar_un_usuario" en la base de datos.
                SqlCommand cmd = new SqlCommand("SP_listar_usuarios", conexion);

                // Se ejecuta un procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                //  Se utiliza un lector de datos (DataReader === dr)
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // Asignamos los valores obtenidos
                        oLista.Add(new UsuarioModel()
                        {
                            clave_empleado = Convert.ToInt32(dr["clave_empleado"]),
                            nombre = dr["nombre"].ToString(),
                            apellidos = dr["apellidos"].ToString(),
                            contraseña = dr["contraseña"].ToString(),
                            tipo = dr["tipo"].ToString()
                        });
                    }
                }
            }

            // Se devuelve la lista de los usuarios
            return oLista;
        }


        public UsuarioModel ObtenerUsuario(int clave_empleado)
        {
            // Creamos una instancia de 'UsuarioModel' para almacenar la información del usuario.
            var oUsuario = new UsuarioModel();

            // Creamos una instancia de la clase 'Conexion'
            var cn = new Conexion();

            // Establecemos una conexión a la base de datos utilizando 'cn.getAulasUTSContext()'
            using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
            {
                conexion.Open();

                // llamar el procedimiento almacenado "SP_consultar_un_usuario" en la base de datos.
                SqlCommand cmd = new SqlCommand("SP_consultar_un_usuario", conexion);

                // El valor es obtenido de la instancia 'model' de la clase 'UsuarioModel'.
                cmd.Parameters.AddWithValue("clave_empleado", clave_empleado);

                // Se ejecuta un procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                //  Se utiliza un lector de datos (DataReader === dr)
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // Asignamos los valores obtenidos
                        oUsuario.clave_empleado = Convert.ToInt32(dr["clave_empleado"]);
                        oUsuario.nombre = dr["nombre"].ToString();
                        oUsuario.apellidos = dr["apellidos"].ToString();
                        oUsuario.contraseña = dr["contraseña"].ToString();
                        oUsuario.tipo = dr["tipo"].ToString();
                    }
                }
            }

            // Se devuelve la información del usuario
            return oUsuario;
        }


        public bool GuardarUsuario(UsuarioModel model)
        {
            bool respuesta;

            try
            {
                // Creamos una nueva instancia de la clase 'Conexion'.
                var cn = new Conexion();

                // Establecemos una conexión a la base de datos utilizando 'cn.getAulasUTSContext()'
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();

                    // llamar el procedimiento almacenado "SP_insertar_usuario" en la base de datos.
                    SqlCommand cmd = new SqlCommand("SP_insertar_usuario", conexion);

                    // Los valores son obtenidos de la instancia 'model' de la clase 'UsuarioModel'.
                    cmd.Parameters.AddWithValue("clave_empleado", model.clave_empleado);
                    cmd.Parameters.AddWithValue("nombre", model.nombre);
                    cmd.Parameters.AddWithValue("apellidos", model.apellidos);
                    cmd.Parameters.AddWithValue("contraseña", model.contraseña);
                    cmd.Parameters.AddWithValue("tipo", model.tipo);

                    // Se ejecuta un Procedimiento almacenado
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                respuesta = true;
            }
            catch (Exception e)
            {
                // En caso de algo inesperado, se captura el error y retorna false
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }


        public bool EditarUsuario(UsuarioModel model)
        {
            bool respuesta;

            try
            {
                // Creamos una nueva instancia de la clase 'Conexion'.
                var cn = new Conexion();

                // Establecemos una conexión a la base de datos utilizando 'cn.getAulasUTSContext()'
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();

                    // llamar el procedimiento almacenado "SP_actualizar_usuario" en la base de datos.
                    SqlCommand cmd = new SqlCommand("SP_actualizar_usuario", conexion);

                    // Los valores son obtenidos de la instancia 'model' de la clase 'UsuarioModel'.
                    cmd.Parameters.AddWithValue("clave_empleado", model.clave_empleado);
                    cmd.Parameters.AddWithValue("nombre", model.nombre);
                    cmd.Parameters.AddWithValue("apellidos", model.apellidos);
                    cmd.Parameters.AddWithValue("contraseña", model.contraseña);
                    cmd.Parameters.AddWithValue("tipo", model.tipo);

                    // Se ejecuta un Procedimiento almacenado
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                respuesta = true;
            }
            catch (Exception e)
            {
                // En caso de algo inesperado, se captura el error y retorna false
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }


        public bool EliminarUsuario(int clave_empleado)
        {
            bool respuesta;

            try
            {
                // Creamos una nueva instancia de la clase 'Conexion'.
                var cn = new Conexion();

                // Establecemos una conexión a la base de datos utilizando 'cn.getAulasUTSContext()'
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();

                    // llamar el procedimiento almacenado "SP_eliminar_usuario" en la base de datos.
                    SqlCommand cmd = new SqlCommand("SP_eliminar_usuario", conexion);

                    // El valor es obtenido de la instancia 'model' de la clase 'UsuarioModel'.
                    cmd.Parameters.AddWithValue("clave_empleado", clave_empleado);

                    // Se ejecuta un Procedimiento almacenado
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                respuesta = true;
            }
            catch (Exception e)
            {
                // En caso de algo inesperado, se captura el error y retorna false
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }

    }
}
