using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EquipoQ22.Dominio;

namespace EquipoQ22.Datos
{
    internal class helperDao
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private static helperDao instancia;

        public helperDao()
        {
            conn = new SqlConnection(@"Data Source=DESKTOP-169QEF9\SQLEXPRESS;Initial Catalog=Qatar2022;Integrated Security=True");
            cmd = new SqlCommand();
        }
        public static helperDao obtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new helperDao();
            }
            return instancia;
        }
        public bool cargarMaestroDetalle(string insertSP, string insertSPDet, Equipos equipo)
        {
            bool aux = false;
            SqlTransaction t = null;
            try
            {
                conn.Open();
                t = conn.BeginTransaction();
                SqlCommand comando = new SqlCommand(insertSP, conn, t);
                comando.CommandType = CommandType.StoredProcedure;
                //comando.Parameters.AddWithValue("nombreColumnaSQL", parametroDeDato primeraTabla)
                comando.Parameters.AddWithValue("@pais", equipo.pais);
                comando.Parameters.AddWithValue("@director_tecnico", equipo.directorTecnico);

                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@id";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                comando.Parameters.Add(pOut);

                comando.ExecuteNonQuery();
                int identificador = (int)pOut.Value;
                SqlCommand comandoD = null;
                foreach (Jugadores J in equipo.lJugador)
                {
                    comandoD = new SqlCommand(insertSPDet,conn,t);
                    comandoD.CommandType = CommandType.StoredProcedure;
                    //INGRESO LAS COLUMNAS QUE ESTAN EN EL DETALLE
                    //comandoD.Parameters.AddWithValue("nombreColumnaSQLDetalles", parametroDato);
                    comandoD.Parameters.AddWithValue("id_equipo",identificador);
                    comandoD.Parameters.AddWithValue("id_persona", J.persona.IDPersona);
                    comandoD.Parameters.AddWithValue("camiseta", J.nroCamiseta);
                    comandoD.Parameters.AddWithValue("posicion", J.posicion);
                    comandoD.ExecuteNonQuery();
                }
                t.Commit();
                aux = true;
            }
            catch (Exception ex)
            {
                if (t != null)
                   t.Rollback();
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return aux;
        }
        public DataTable combo(string nombreSP)
        {
            DataTable dt = new DataTable();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = nombreSP;
            cmd.Parameters.Clear();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            return dt;
        }
    }
}
