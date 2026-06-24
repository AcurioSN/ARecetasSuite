<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="ARRecetas.login" %>

<!DOCTYPE html>
<html lang="es">

<head runat="server">

<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1">

<title>ARECETAS</title>

<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
<link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet">

<style>

body{
margin:0;
height:100vh;
font-family:'Segoe UI',sans-serif;
overflow:hidden;
}

/* contenedor */

.login-container{
height:100vh;
padding:0;
margin:0;
}

/* columnas */

.login-left,
.login-right{
padding:0;
margin:0;
}

/* lado imagen */

.login-left{
background:url('https://images.unsplash.com/photo-1504674900247-0877df9cc836') center/cover no-repeat;
position:relative;
color:white;
}

.overlay{
position:absolute;
width:100%;
height:100%;

background:linear-gradient(140deg,rgba(84,15,74,0.85),rgba(186,12,92,0.85));

display:flex;
flex-direction:column;
align-items:center;
justify-content:center;
text-align:center;

padding:40px;
}

.overlay h1{
font-size:46px;
font-weight:700;
letter-spacing:2px;
}

.overlay p{
font-size:18px;
opacity:0.9;
}

/* lado login */

.login-right{
background:white;
display:flex;
align-items:center;
justify-content:center;
}

/* contenedor login */

.login-box{
width:340px;
}

/* logo */

.logo{
font-size:32px;
font-weight:700;
color:#540f4a;
}

.logo span{
color:#ba0c5c;
}

/* inputs */

.form-control{
padding:12px;
border-radius:10px;
}

.input-group-text{
background:#f4f4f4;
}

/* botón */

.btn-login{
background:linear-gradient(135deg,#ba0c5c,#975a89);
border:none;
color:white;
padding:12px;
font-weight:600;
border-radius:10px;
transition:0.3s;
}

.btn-login:hover{
transform:translateY(-2px);
box-shadow:0 6px 18px rgba(0,0,0,0.25);
}

.footer{
font-size:12px;
color:#888;
margin-top:30px;
}

@media(max-width:900px){

.login-left{
display:none;
}

.login-right{
background:linear-gradient(135deg,#540f4a,#975a89);
color:white;
}

.logo{
color:white;
}

.footer{
color:#ddd;
}

}

</style>

</head>

<body>
	
<form id="form1" runat="server">
	<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div class="container-fluid login-container">

<div class="row h-100 g-0">

<!-- PANEL VISUAL -->

<div class="col-lg-7 login-left">

<div class="overlay">

<div>

<h1><i class="bi bi-journal-richtext"></i> ARECETAS</h1>

<p class="mt-3">
Sistema de administración de recetas  
para restaurantes
</p>

</div>

</div>

</div>

<!-- LOGIN -->

<div class="col-lg-5 login-right">

<div class="login-box">

<div class="text-center mb-4">

<div class="logo">
AR<span>ECETAS</span>
</div>

<div class="text-muted">
Ingreso al sistema
</div>

</div>

<!-- USUARIO -->

<div class="mb-3">

<label class="form-label">Usuario</label>

<div class="input-group">

<%--<span class="input-group-text">
<i class="bi bi-person"></i>
</span>--%>

<asp:TextBox 
ID="txtUsuario" 
runat="server" 
CssClass="form-control" 
placeholder="Ingrese su usuario" />

</div>

</div>

<!-- PASSWORD -->

<div class="mb-3">

<label class="form-label">Contraseña</label>

<div class="input-group">

<%--<span class="input-group-text">
<i class="bi bi-lock"></i>
</span>--%>

<asp:TextBox 
ID="txtPassword" 
runat="server" 
CssClass="form-control" 
TextMode="Password"
placeholder="Ingrese su contraseña" />

</div>

</div>

<div class="d-grid mt-4">

<asp:Button 
ID="btnLogin" 
runat="server" 
Text="Ingresar" 
CssClass="btn btn-login" />

</div>

<div class="text-center footer">
© 2026 ARECETAS v1.1.0 (2026-04-29) - Soluciones de Negocio - Desarrollo e Innovación
</div>

</div>

</div>

</div>

</div>

</form>

</body>

</html>