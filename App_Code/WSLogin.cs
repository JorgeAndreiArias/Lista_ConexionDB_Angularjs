using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

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

    [WebMethod]
    public string HelloWorld()
    {
        return "Hola a todos";
    }

    [WebMethod(EnableSession = true)]
    public User Login(User user)
    {
        //Aqui iría la logica a base de datos
        if (user.Nick == "andreiarias53" && user.Password == "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855")
        {
            User _user = new global::User()
            {
                Id = 1001,
                Name = "Jorge Andrei",
                LastName = "Arias"
            };
            //Creamos session con el Id del Usuario
            HttpContext.Current.Session["Identificador"] = _user.Id;
            return _user;
        }
        else
        {
            throw new Exception("User not found");
        }
    }

    [WebMethod(EnableSession = true)]
    public User Registrar(User user)
    {
        
            User _user = new global::User()
            {
                Id = 002,
                Name = user.Name,
                LastName = user.LastName,
                Nick = user.Nick,
                Password = user.Password
            };
            HttpContext.Current.Session["Identificador"] = _user.Id;
            return _user;
        }
    }

