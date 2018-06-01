using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de SQLConection
/// </summary>
public class SQLConexion
{
    private SqlConnection _conn = null;
    string _ConnectonString = "";
    bool _Conectado = false;

    string _NombreProcedimiento = "";
    List<SqlParameter> _Parametros = new List<SqlParameter>();
    bool _Preparado = false;


    public SQLConexion()
    {
           //
    }

    public bool Conectar(string ConnectionString) {
        bool _Rsp = false;
        _conn = new SqlConnection(ConnectionString);
        try
        {
            _conn.Open();
            _Conectado = true;
            _Rsp = true;
        }
        catch (SqlException SqlEx)
        {
            string MensajeError = "ERROR : " + SqlEx.Message + ". " + "LINEA : " + SqlEx.LineNumber + ".";
            throw new Exception(MensajeError, SqlEx);
        }
        catch {
            _Rsp = false;
        }
        return _Rsp;
    }
    public void Desconectar() {
        try {
            _conn.Close();
        } catch {

        }
    }
    public void PrepararProcedimiento(string NombreProcedimiento, List<SqlParameter> Parametros)
    {
        if (_Conectado)
        {
            _NombreProcedimiento = "";
            _Parametros.Clear();

            _NombreProcedimiento = NombreProcedimiento;
            _Parametros = Parametros;
            _Preparado = true;
        }
        else
        {
            throw new Exception("No hay conexión con la bd");
        }
    }

    public DataTableReader EjecutarTableReader()
    {
        if (_Preparado)
        {
            DataTable dt = new DataTable();
            SqlCommand cmm = new SqlCommand(_NombreProcedimiento, _conn);
            cmm.CommandType = System.Data.CommandType.StoredProcedure;
            //cmm.CommandTimeout = 120; //SEGUNDOS DE ESPERA PARA EJECUTAR UNA CONSULTA EN SQL(60 SEGUNDOS)
            cmm.Parameters.AddRange(_Parametros.ToArray());
            SqlDataAdapter adt = new SqlDataAdapter(cmm);
            adt.Fill(dt);
            _Preparado = false;
            return dt.CreateDataReader();
        }
        else
        {
            _Preparado = false;
            throw new Exception("Procedimiento no prepradado");
        }
    }

    public int EjecutarProcedimiento()
    {
        if (_Preparado)
        {
            SqlCommand cmm = new SqlCommand(_NombreProcedimiento, _conn);
            cmm.CommandType = System.Data.CommandType.StoredProcedure;
            cmm.CommandTimeout = 120;
            cmm.Parameters.AddRange(_Parametros.ToArray());
            _Preparado = false;
            return cmm.ExecuteNonQuery();
        }
        else {
            _Preparado = false;
            throw new Exception("Procedimiento no preparado");
        }
    }
}
