using UTS.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Identity.Client;
using System.Transactions;
using System.Drawing.Imaging;

namespace UTS.Datos
{
    public class InstalacionDatos
    {
        public List<InstalacionModel> Lista()
        {
            var oLista = new List<InstalacionModel>();

            var cn = new Conexion();

            // Utilizar 'using' para establecer la cadena de conexión y garantizar su liberación.
            using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
            {
                conexion.Open();

                // Crear un comando SQL que ejecutará el procedimiento almacenado "SP_listar_instalaciones" en la base de datos.
                SqlCommand cmd = new SqlCommand("SP_listar_instalaciones", conexion);

                // Se ejecuta un procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                // Leer el resultado de la ejecución del procedimiento almacenado.
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // Una vez que se estén leyendo los datos, se guardan en la lista.
                        oLista.Add(new InstalacionModel()
                        {
                            // Se utilizan las propiedades de la clase para asignar los valores.
                            idaula = Convert.ToInt32(dr["idaula"]),
                            capacidad = Convert.ToInt32(dr["capacidad"]),
                            nombre = dr["nombre"].ToString(),
                            refEdificio = new EdificioModel()
                            {
                                numedificio = Convert.ToInt32(dr["numedificio1"])
                            }
                        });
                    }
                }
            }

            // Se devuelve la lista de instalaciones.
            return oLista;
        }

        public InstalacionModel ConsultarInstalacion(int idaula)
        {
            var oInstalacion = new InstalacionModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
            {
                conexion.Open();

                // Crear un comando SQL que ejecutará el procedimiento almacenado "SP_consulta_instalacion" en la base de datos.
                SqlCommand cmd = new SqlCommand("SP_consulta_instalacion", conexion);

                // Agregar un parámetro al comando SQL para especificar el ID de la instalación que se desea consultar.
                cmd.Parameters.AddWithValue("idaula", idaula);

                // Se ejecuta un procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                // Leer el resultado de la ejecución del procedimiento almacenado.
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // Asignar los valores obtenidos a la instancia de 'InstalacionModel'.
                        oInstalacion.idaula = Convert.ToInt32(dr["idaula"]);
                        oInstalacion.capacidad = Convert.ToInt32(dr["capacidad"]);
                        oInstalacion.nombre = dr["nombre"].ToString();
                        oInstalacion.refEdificio = new EdificioModel()
                        {
                            numedificio = Convert.ToInt32(dr["numedificio1"])
                        };
                    }
                }
            }

            // Se devuelve la información de la instalación consultada.
            return oInstalacion;
        }

        public bool GuardarInstalacion(InstalacionModel model)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                // Establecer una conexión a la base de datos utilizando la cadena de conexión proporcionada por 'cn.getAulasUTSContext()'.
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();

                    // Crear un comando SQL que ejecutará el procedimiento almacenado "SP_insertar_instalacion" en la base de datos.
                    SqlCommand cmd = new SqlCommand("SP_insertar_instalacion", conexion);

                    // Agregar parámetros al comando SQL para especificar los valores de la nueva instalación que se desea insertar.
                    cmd.Parameters.AddWithValue("capacidad", model.capacidad);
                    cmd.Parameters.AddWithValue("nombre", model.nombre);
                    cmd.Parameters.AddWithValue("numedificio1", model.refEdificio.numedificio);

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
