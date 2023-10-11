using UTS.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Identity.Client;
using System.Transactions;
using System.Drawing.Imaging;

namespace UTS.Datos
{
    public class HorarioDatos
    {
        public List<HorarioModel> Listar()
        {
            var oLista = new List<HorarioModel>();

            var cn = new Conexion();

            // Establecemos una conexión a la base de datos utilizando la cadena de conexión proporcionada por 'cn.getAulasUTSContext()'.
            using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
            {
                conexion.Open();

                // Creamos un nuevo comando SQL que ejecutará un procedimiento almacenado llamado "Sp_ListarHorario" en la base de datos.
                SqlCommand cmd = new SqlCommand("Sp_ListarHorario", conexion);

                // Se ejecuta un procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                // Utilizamos un lector de datos (DataReader) para obtener los resultados de la consulta almacenada en 'dr'.
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // Creamos una instancia de 'HorarioModel' y la agregamos a la lista 'oLista' con los valores recuperados del lector de datos.
                        oLista.Add(new HorarioModel()
                        {
                            idhorario = Convert.ToInt32(dr["idhorario"]),
                            refInstalacion = new InstalacionModel
                            {
                                idaula = Convert.ToInt32(dr["idaula2"]),
                            },
                            clave_empleado2 = Convert.ToInt32(dr["clave_empleado2"]),
                            Fecha = (DateTime)dr["Fecha"],
                            HoraInicio = (TimeSpan)dr["HoraInicio"],
                            HoraFin = (TimeSpan)dr["HoraFin"]
                        });
                    }
                }
            }

            // Devolvemos la lista que contiene los horarios recuperados.
            return oLista;
        }

        public HorarioModel ConsultarHorario(int idhorario)
        {
            var oHorario = new HorarioModel();

            var cn = new Conexion();

            // Establecemos una conexión a la base de datos utilizando la cadena de conexión proporcionada por 'cn.getAulasUTSContext()'.
            using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
            {
                conexion.Open();

                // Creamos un nuevo comando SQL que ejecutará un procedimiento almacenado llamado "horario_agenda_consultar" en la base de datos.
                SqlCommand cmd = new SqlCommand("horario_agenda_consultar", conexion);

                // Agregamos un parámetro al comando SQL para especificar el ID del horario que deseamos consultar.
                cmd.Parameters.AddWithValue("idhorario", idhorario);

                // Se ejecuta un procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                // Utilizamos un lector de datos (DataReader) para obtener los resultados de la consulta almacenada en 'dr'.
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // Asignamos los valores recuperados del lector de datos a la instancia 'oHorario' de la clase 'HorarioModel'.
                        oHorario.idhorario = Convert.ToInt32(dr["idhorario"]);
                        oHorario.refInstalacion = new InstalacionModel
                        {
                            idaula = Convert.ToInt32(dr["idaula2"]),
                        };
                        oHorario.clave_empleado2 = Convert.ToInt32(dr["clave_empleado2"]);
                        oHorario.Fecha = (DateTime)dr["Fecha"];
                        oHorario.HoraInicio = (TimeSpan)dr["HoraInicio"];
                        oHorario.HoraFin = (TimeSpan)dr["HoraFin"];
                    }
                }
            }

            // Devolvemos la información del horario.
            return oHorario;
        }

        public bool InsertarApartado(HorarioModel model)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                // Establecemos una conexión a la base de datos utilizando la cadena de conexión proporcionada por 'cn.getAulasUTSContext()'.
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();

                    // Creamos un nuevo comando SQL que ejecutará un procedimiento almacenado llamado "Sp_InsertarHorario" en la base de datos.
                    SqlCommand cmd = new SqlCommand("Sp_InsertarHorario", conexion);

                    // Agregamos parámetros al comando SQL para especificar los valores del nuevo horario que deseamos insertar.
                    cmd.Parameters.AddWithValue("idaula2", model.refInstalacion.idaula);
                    cmd.Parameters.AddWithValue("clave_empleado2", model.clave_empleado2);
                    cmd.Parameters.AddWithValue("Fecha", model.Fecha);
                    cmd.Parameters.AddWithValue("HoraInicio", model.HoraInicio);
                    cmd.Parameters.AddWithValue("HoraFin", model.HoraFin);

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

        public bool EditarApartado(HorarioModel model)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                // Establecemos una conexión a la base de datos utilizando la cadena de conexión proporcionada por 'cn.getAulasUTSContext()'.
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();

                    // Creamos un nuevo comando SQL que ejecutará un procedimiento almacenado llamado "Sp_EditarHorario" en la base de datos.
                    SqlCommand cmd = new SqlCommand("Sp_EditarHorario", conexion);

                    // Agregamos parámetros al comando SQL para especificar los valores actualizados del horario.
                    cmd.Parameters.AddWithValue("idhorario", model.idhorario);
                    cmd.Parameters.AddWithValue("idaula2", model.refInstalacion.idaula);
                    cmd.Parameters.AddWithValue("clave_empleado2", model.clave_empleado2);
                    cmd.Parameters.AddWithValue("Fecha", model.Fecha);
                    cmd.Parameters.AddWithValue("HoraInicio", model.HoraInicio);
                    cmd.Parameters.AddWithValue("HoraFin", model.HoraFin);

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

        public bool EliminarHorario(int idhorario)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                // Establecemos una conexión a la base de datos utilizando la cadena de conexión proporcionada por 'cn.getAulasUTSContext()'.
                using (var conexion = new SqlConnection(cn.getAulasUTSContext()))
                {
                    conexion.Open();

                    // Creamos un nuevo comando SQL que ejecutará un procedimiento almacenado llamado "dbo.horario_agenda_eliminar" en la base de datos.
                    SqlCommand cmd = new SqlCommand("dbo.horario_agenda_eliminar", conexion);

                    // Agregamos un parámetro al comando SQL para especificar el ID del horario que deseamos eliminar.
                    cmd.Parameters.AddWithValue("idhorario", idhorario);

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

