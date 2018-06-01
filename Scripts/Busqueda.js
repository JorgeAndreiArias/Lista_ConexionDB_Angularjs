$(document).ready(function ()
{   
    $("#bListaUsuarios").click(function ()
    {        
        ListaUsuarios();
    });

    $('#txtBuscar').keyup(/*"change keyup paste",*/ function () {
        ListaUsuarios();
    });
    //UTILIZANDO UI DE JAVASCRIPT
    var ListaUsuarios = function ()
    {
       Nombre = $('#txtBuscar').val();
        var objJson = {
            "Nick": Nombre
        }
        var stringJson = JSON.stringify(objJson);
        $.ajax(
        {
            url: "WSLogin.asmx/GetUsers",
            data: "{\"user\":" + stringJson + "}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (response)
            {
                //OPERADOR TERNARIO
                var datos_usuarios = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                $("#tbody").remove();
                
                var name = $('#txtBuscar').val();
                for (var i = 0; i < datos_usuarios.length; i++)
                {
                    var id = datos_usuarios[i].Id;
                    var nombre = datos_usuarios[i].Name;
                    var lastname = datos_usuarios[i].LastName;
                    var nick = datos_usuarios[i].Nick;
                    var password = datos_usuarios[i].Password;
                    //var p = moment(datos_usuarios[i].FechaNacimiento).toString();
                    

                    if (nick.indexOf(name) > -1) {
                        $("#tbl-usuarios").append("<tbody id=" + "tbody" + "></tbody>");
                        $("#tbody").append("<tr><td>" + id + "</td>" +
                            "<td>" + nombre + "</td>" +
                            "<td>" + lastname + "</td>" +
                            "<td>" + nick + "</td>" +
                            "<td>" + password + "</td>");
                           // "<td>" + p + "</td></tr> ");
                    }
                }
            }
        });
    }
});
