<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bienvenido.aspx.cs" Inherits="Bienvenido" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:LinkButton ID="LinkCerrar" runat="server" OnClick="LinkCerrar_Click">Cerrar Session</asp:LinkButton>
        </div>
    </form>
</body>
</html>
