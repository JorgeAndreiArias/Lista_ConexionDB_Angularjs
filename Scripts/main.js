
    $(document).ready(function () {
        $('#btnJoin').click(function () {
            var Nombre = $('#txtName').val();
            var Apellido = $('#txtLName').val();
            var nick = $('#txtNick').val();
            var Contrasena = $('#txtPassword').val();

            if (Nombre != '' && Apellido != '' && nick != '' && Contrasena != '') {

                if (checkPassword(Contrasena)) {
                    FuncCook(Nombre, Apellido, nick, Contrasena);
                    MiFuncionRegistro(Nombre, Apellido, nick, Contrasena);

                } else { alert('Su contraseña no cumple nada') }
            }
            else {
                alert('Tienes algun espacio vacio')
            }

        });
                
    });


        function FuncCook(Nombre, Apellido, nick, Contrasena) {
        alert('Entra')
            var objJson = {
        "Nombre": Nombre,
                "Nick": nick,
                "Pass": Contrasena,
                "Apellido": Apellido
                //"Password": password
            }
            var stringJson = JSON.stringify(objJson)

            $.cookie('Micookie1', stringJson, {expires: 7 });
            alert('Valor de la cookie =>' + $.cookie('Micookie1'))
        }

        function LeerCookie() {
            if ($.cookie('Micookie1') != null || $.cookie('Micookie1') != '') {
                var objCook = $.parseJSON($.cookie('Micookie1'));
                $('#txtNick').val(objCook.Nick);
                $('#txtPassword').val(objCook.Pass);
                $('#txtLName').val(objCook.Apellido);
                $('#txtName').val(objCook.Nombre);
            }
        }


        function MiFuncionRegistro(Nombre, Apellido, nick, Contrasena) {
        sha256(Contrasena);
    var hashPwd = sha256.create();

            var objJson = {
                "Name": Nombre,
                "LastName": Apellido,
                "Nick": nick,
                "Password": Contrasena
            }

            var stringJson = JSON.stringify(objJson);
            //alert(stringJson);
            $.ajax({
        url: "WSLogin.asmx/Registrar",
                data: "{'user':" + stringJson + "}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; utf-8",
                success: function (msg) {
                   
        window.location.href = "Login.html";
    
                    //alert(msg.d.Name)
                },
                error: function (result) {
        alert("ERROR " + result.status + ' ' + result.statusText);
    }
            });
        }

        function checkPassword(str) {
            var re = /(?=.*\d)(?=.*[a-z])(?=.*[A-Z].{6,})/;
            return re.test(str);
        }
