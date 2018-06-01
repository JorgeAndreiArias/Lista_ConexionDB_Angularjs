using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;

/// <summary>
/// Descripción breve de WSLogin
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
[System.Web.Script.Services.ScriptService]
public class WSLogin : System.Web.Services.WebService
{

   public WSLogin()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public User Login(User user)
    {//oahdpiubpiufpqiuwebcpiuqwbcpiasdbcpuy
        SQLConexion _conexion = new SQLConexion();
        List<SqlParameter> _Parametros = new List<SqlParameter>();
        DataTableReader _dtr = null;
        try
        {
            _conexion.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
            _Parametros.Add(new SqlParameter("@nick", user.Nick));
            _Parametros.Add(new SqlParameter("@pass", user.Password));
            _conexion.PrepararProcedimiento("[dbo].[sp_conf]", _Parametros);
            _dtr = _conexion.EjecutarTableReader();

            if (_dtr.HasRows) {
                _dtr.Read();
                User _user = new global::User()
                {
                    
                    Id = long.Parse(_dtr["Id"].ToString()),
                    Name = _dtr["Nombre"].ToString(),
                    LastName = _dtr["LastName"].ToString(),
                    Nick = user.Nick,
                    Password = user.Password
                };
                _dtr.Close();
                HttpContext.Current.Session["Identificador"] = _user.Id;
                return _user;
            }else
            {
                throw new Exception("User not found");
            }
        }
        catch (Exception msg)
        {
            throw new Exception(msg.Message);
        }
        finally
        {
            _conexion.Desconectar();
            _conexion = null;
            _dtr = null;
        }
        //Aqui iría la logica a base de datos
        //if (user.Nick == "andreiarias53" && user.Password == "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855")
        //{
        
            //Creamos session con el Id del Usuario
            
        //}
        //else
        
    }

    [WebMethod(EnableSession = true)]
    public User Registrar(User user)
    {
        SQLConexion _conexion = new SQLConexion();
        List<SqlParameter> _Parametros = new List<SqlParameter>();
        DataTableReader _dtr = null;
        try
        {
            _conexion.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
            _Parametros.Add(new SqlParameter("@Name", user.Name));
            _Parametros.Add(new SqlParameter("@LastName", value: user.LastName));
            _Parametros.Add(new SqlParameter("@Nick", user.Nick));
            _Parametros.Add(new SqlParameter("@Password", user.Password));
            _conexion.PrepararProcedimiento("sp_SetUsers", _Parametros);
            _conexion.EjecutarProcedimiento();
            return user;
           
        }
        catch (Exception msg)
        {
            throw new Exception(msg.Message);
        }
        finally
        {
            _conexion.Desconectar();
            _conexion = null;
            _dtr = null;
        }
    }

    [WebMethod]
    public List<User> GetUsers(string user)
    {
        SQLConexion _conexion = new SQLConexion();
        List<SqlParameter> _Parametros = new List<SqlParameter>();
        DataTableReader _dtr = null;    
        List<User> _list = new List<User>();
        try
        {
            //se abre conexion
            _conexion.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
            _Parametros.Add(new SqlParameter("@nick", user.ToString()));
            _conexion.PrepararProcedimiento("sp_ListUsers", _Parametros);
            _dtr = _conexion.EjecutarTableReader();
            if (_dtr.HasRows)
            {
                while (_dtr.Read())
                {
                    User _user = new User()
                    {
                        //Se recuperan los valores de acuerdo al alias que se definio en el procedimiento almacenado
                        Id = long.Parse(_dtr["Id"].ToString()),
                        Name = _dtr["Nombre"].ToString(),
                        LastName = _dtr["LastName"].ToString(),
                        Nick = _dtr["Nick"].ToString(),
                        Password = _dtr["Password"].ToString(),
                        Img = _dtr["Img"].ToString()
                    };
                    _list.Add(_user); //Se agrega elemento 2
                    
                    //HttpContext.Current.Session["Identificador"] = _user.Id;
               
                }
                //JavaScriptSerializer js = new JavaScriptSerializer();
                //Context.Response.Write(js.Serialize(_list));
                _dtr.Close();
                return _list;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            _conexion.Desconectar();
            _conexion = null;
            _dtr = null;
        }
        return _list;
    }

    [WebMethod]
    public void Updates(string user)
    {
        SQLConexion _conexion = new SQLConexion();
        List<SqlParameter> _Parametros = new List<SqlParameter>();
        DataTableReader _dtr = null;
        List<User> _list = new List<User>();
        try
        {
            //se abre conexion
            _conexion.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
            _Parametros.Add(new SqlParameter("@id", user.ToString()));
            _conexion.PrepararProcedimiento("sp_UdtEst", _Parametros);
            _dtr = _conexion.EjecutarTableReader();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            _conexion.Desconectar();
            _conexion = null;
            _dtr = null;
        }
    }
}


