using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bienvenido : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                long p = long.Parse(HttpContext.Current.Session["Identificador"].ToString());
                if ( p > 0)
                {
                    Response.Write("Bienvenido");
                }
                else
                {
                    Response.Redirect("Login.html");
                }
            }
            catch (Exception)
            {
                Response.Redirect("Login.html");
            }
        }
    }

    protected void LinkCerrar_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session["Identificador"] = 0;
        Response.Redirect("Login.html");
    }
}
    

