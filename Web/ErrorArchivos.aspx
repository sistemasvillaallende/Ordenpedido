<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorArchivos.aspx.cs" Inherits="Web.ErrorArchivos" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Error - Archivo demasiado grande</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            color: #333;
        }
        .container {
            margin: 100px auto;
            max-width: 500px;
            padding: 30px;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0px 0px 10px rgba(0,0,0,0.1);
            text-align: center;
        }
        h2 {
            color: #c00;
        }
        a {
            display: inline-block;
            margin-top: 20px;
            padding: 10px 15px;
            background: #0078d7;
            color: #fff;
            text-decoration: none;
            border-radius: 5px;
        }
        a:hover {
            background: #005fa3;
        }
    </style>
</head>
<body>
    <div class="container">
        <h2>⚠ Archivo demasiado grande</h2>
        <p>El archivo que intentaste subir supera el tamaño máximo permitido (50 MB).</p>
        <p>Por favor, selecciona un archivo más liviano e inténtalo nuevamente.</p>
        <a href="~/Default.aspx">Volver al inicio</a>
    </div>
</body>
</html>
