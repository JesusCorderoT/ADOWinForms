using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using ADOWinForms.Entidades;

namespace ADOWinForms.ADO
{
    internal class ADOEstatusAlumno :ICRUD
    {
        List<EstatusAlumno> _listEs;
        string _cnnString = ConfigurationManager.ConnectionStrings["InstitutoConnection"].ConnectionString;
        SqlCommand comando;
        public List<EstatusAlumno> Consultar()
        {
            string query = $"select * from EstatusAlumno";
            using (SqlConnection con = new SqlConnection(_cnnString))
            {
                _listEs = new List<EstatusAlumno>();
                comando = new SqlCommand(query, con);
                comando.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    _listEs.Add(
                        new EstatusAlumno()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            clave = reader["clave"].ToString(),
                            nombre = reader["nombre"].ToString(),
                        }
                        );
                }
                con.Close();
            }
            Console.WriteLine("Consultaste todos");
            foreach (var es in _listEs)
            {
                Console.WriteLine($"ID:{es.id} Clave:{es.clave} Nombre:{es.nombre}");
            }
            return _listEs;
        }
        //-----------------------------------------------------------------------------------------------------


        public EstatusAlumno Consultar(int id)
        {

            EstatusAlumno es = new EstatusAlumno();

            string queryu = $"select * from EstatusAlumno where id ={id}";
            using (SqlConnection con = new SqlConnection(_cnnString))
            {

                comando = new SqlCommand(queryu, con);
                comando.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader adpter = comando.ExecuteReader();
                while (adpter.Read())
                {

                    es = new EstatusAlumno()
                    {
                        id = Convert.ToInt32(adpter["id"]),
                        clave = adpter["clave"].ToString(),
                        nombre = adpter["nombre"].ToString(),
                    };


                }
                con.Close();

            }
            return es;
        }
        //-----------------------------------------------------------------------------------------------------
        public void Actualizar(EstatusAlumno estatus)
        {
            string queryAc = $"PO_actualizarEstatus";
            using (SqlConnection con = new SqlConnection(_cnnString))
            {

                comando = new SqlCommand(queryAc, con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", estatus.id);
                comando.Parameters.AddWithValue("@clave", estatus.clave);
                comando.Parameters.AddWithValue("@nombre", estatus.nombre);
                con.Open();
                comando.ExecuteNonQuery();
                con.Close();
            }
            Console.WriteLine("Actualizaste a" + estatus.id);
        }

        //-----------------------------------------------------------------------------------------------------
        public int Agregar(EstatusAlumno estatus)
        {
            string queryA = $"PO_agregarEstatus";
            using (SqlConnection con = new SqlConnection(_cnnString))
            {

                comando = new SqlCommand(queryA, con);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@clave", estatus.clave);
                comando.Parameters.AddWithValue("@nombre", estatus.nombre);
                con.Open();
                int a = (Int32)comando.ExecuteScalar();
                estatus.id = a;
                con.Close();
            }
            Console.WriteLine("Nuevo ID" + estatus.id);
            return estatus.id;

        }



        public void Eliminar(EstatusAlumno estatus)
        {
            string queryd = $"delete from EstatusAlumno where id={estatus.id}";
            using (SqlConnection con = new SqlConnection(_cnnString))
            {
                comando = new SqlCommand(queryd, con);
                comando.CommandType = CommandType.Text;
                con.Open();
                comando.ExecuteNonQuery();
                con.Close();
            }

            Console.WriteLine("Elimino al id:" + estatus.id);
        }
    }
}
