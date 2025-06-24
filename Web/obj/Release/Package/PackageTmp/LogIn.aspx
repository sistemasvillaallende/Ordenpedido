<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="Web.logonin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MesaWeb - Inicio de Sesion</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport' />
    <!-- Bootstrap 3.3.4 -->
    <link href="App_Themes/bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome Icons -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="styles/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <!-- iCheck -->
    <link href="styles/skin-blue.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .sombra {
            -webkit-box-shadow: 10px 10px 5px 0px rgba(0,0,0,0.75);
            -moz-box-shadow: 10px 10px 5px 0px rgba(0,0,0,0.75);
            box-shadow: 10px 10px 5px 0px rgba(0,0,0,0.75);
            border-radius: 10px 10px 10px 10px;
            -moz-border-radius: 10px 10px 10px 10px;
            -webkit-border-radius: 10px 10px 10px 10px;
            border: 0px solid #000000;
        }
    </style>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>
<body class="login-page">
    <form id="form1" runat="server">

        <script type="text/javascript">
</script>
        <div class="login-box">
            <div class="box box-warning" id="divError" runat="server" visible="false">
                <div class="box-header">
                </div>
                <div class="box-body">
                    <div class="alert alert-dismissible" role="alert" style="border-color: red;">
                        <strong style="font-size: 18px;">Sr. Usuario:</strong><br />
                        <p style="font-size: 16px;" id="lblError" runat="server"></p>
                        <div style="text-align: center; padding-top: 30px;">
                            <asp:Button ID="btnOkError" CssClass="btn btn-danger" OnClick="btnOkError_Click"
                                runat="server" Text="Aceptar" />
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                </div>
            </div>
            <div class="login-logo">
                <a href="#" style="color: #3C8DBC;"><b>Ordenes</b> de Pedido</a>
            </div>
            <!-- /.login-logo -->
            <div id="divLogIn" class="login-box-body sombra" runat="server">
                <h2 class="login-box-msg">Inicie su sesion</h2>
                <div class="form-group">
                    <asp:TextBox ID="txtUser" CssClass="form-control" placeholder="Nombre de Usuario" runat="server"
                        AutoCompleteType="Disabled" OnTextChanged="txtUser_TextChanged" AutoPostBack="True"></asp:TextBox>
                </div>
                <div class="form-group">
                    <input type="password" class="form-control" placeholder="Contraseña" id="txtPass" runat="server" />
                </div>
                <div class="form-group">
                    <asp:DropDownList ID="ddlOficina" runat="server" CssClass="form-control" AppendDataBoundItems="True" Enabled="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlOficina"
                        ErrorMessage="Debe Seleccionar Oficina" ForeColor="#FF3300" InitialValue="0"
                        ValidationGroup="ValidacionDatos" Display="Dynamic">*</asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <div class="col-xs-8">
                    </div>
                    <!-- /.col -->
                    <div class="col-xs-4">
                        <button type="button" runat="server" id="btnIngresar" onserverclick="btnIngresar_ServerClick" class="btn btn-primary btn-block btn-flat">Ingresar</button>
                    </div>
                    <!-- /.col -->
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <!-- Bootstrap Pop-up Box -->
                        <div class="modal fade" id="myModalMessage" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false" aria-labelledby="myModalLabel">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" id="btnModelX" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                        <h4 class="modal-title" id="myModalMessageTitle">Aviso Importante!</h4>
                                    </div>
                                    <div class="modal-body" id="myModalMessageContent">
                                        <h4>
                                            <strong>
                                                <asp:Label ID="lblPregunta" runat="server" Text="Mensaje Pregunta">
                                                </asp:Label>
                                            </strong></h4>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnModalYes" runat="server" class="btn btn-primary" data-dismiss="modal" onserverclick="btnModalYes_ServerClick">Cerrar</button>
                                        <%--<button type="button" id="btnModalClose" class="btn btn-default" data-dismiss="modal">No</button>--%>
                                    </div>
                                    <%--<div class="modal-footer">
                                    <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                                </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.social-auth-links -->


            </div>
            <!-- /.login-box-body -->
        </div>
        <!-- /.login-box -->

        <!-- jQuery 2.1.4 -->
        <script src="App_Themes/bower_components/jquery/dist/jquery.min.js"></script>
        <!-- Bootstrap 3.3.2 JS -->
        <script src="App_Themes/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>

        <script>
            $(function () {
                $('input').iCheck({
                    checkboxClass: 'icheckbox_square-blue',
                    radioClass: 'iradio_square-blue',
                    increaseArea: '20%' // optional
                });
            });
        </script>
    </form>
</body>
</html>
