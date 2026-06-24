<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="main.aspx.vb" Inherits="ARRecetas.main" %>


<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>ARRecetas</title>

    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Bootstrap Icons -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" rel="stylesheet" />

    <style>
        body {
            background: #f6f6f8;
            font-family: "Segoe UI", sans-serif;
            font-size: 14px;
        }

        /* HEADER */
        .main-header {
            background: linear-gradient(135deg, #540f4a, #ba0c5c);
            color: #fff;
            padding: 1.4rem 2rem;
            border-radius: 0 0 1.25rem 1.25rem;
        }

        .header-title {
            display: flex;
            align-items: center;
            gap: .6rem;
        }

        .header-title h1 {
            font-size: 1.2rem;
            margin: 0;
            font-weight: 700;
        }

        .header-title i {
            font-size: 1.4rem;
        }

        .main-header p {
            font-size: .85rem;
            opacity: .85;
            margin-top: .25rem;
        }

        /* CARD LINK (CLICKEABLE SEGURA) */
        .card-link {
            display: block;
            text-decoration: none;
            color: inherit;
            height: 100%;
        }

        .card-link:hover {
            color: inherit;
        }

        /* CARD */
        .main-card {
            background: #fff;
            border-radius: 1rem;
            padding: 1.25rem;
            height: 100%;
            box-shadow: 0 6px 20px rgba(0,0,0,.08);
            transition: transform .25s ease, box-shadow .25s ease, border .25s ease;
        }

        /* HOVER REAL */
        .card-link:hover .main-card {
            transform: translateY(-6px);
            box-shadow: 0 18px 42px rgba(186,12,92,.30);
            border: 1px solid rgba(186,12,92,.25);
        }

        .main-card h5 {
            font-size: .95rem;
            font-weight: 600;
            transition: color .25s ease;
        }

        .card-link:hover h5 {
            color: #ba0c5c;
        }

        .main-card p {
            font-size: .85rem;
        }

        /* ICONOS */
        .icon-circle {
            width: 48px;
            height: 48px;
            border-radius: 50%;
            background: #ba0c5c;
            color: #fff;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 1.25rem;
            margin-bottom: .75rem;
            transition: transform .25s ease, background-color .25s ease;
        }

        .card-link:hover .icon-circle {
            transform: scale(1.08);
            background-color: #540f4a;
        }

        .icon-secondary {
            background: #975a89;
        }

        /* BOTÓN FLOTANTE INFERIOR */
        .btn-footer {
            /*position: fixed;
            bottom: 22px;
            right: 22px;*/
            z-index: 1050;
            border-radius: 50px;
            padding: .55rem 1.2rem;
            background: #540f4a;
            color: #fff;
            border: none;
            box-shadow: 0 6px 18px rgba(0,0,0,.25);
        }

        .btn-footer:hover {
            background: #ba0c5c;
        }

        /* MODAL 2 COLUMNAS */
        .modal-left {
            background: linear-gradient(135deg, #540f4a, #ba0c5c);
            color: #fff;
            padding: 2rem;
        }

        .modal-left i {
            font-size: 2rem;
            margin-bottom: .75rem;
        }

        .modal-left h4 {
            font-weight: 700;
        }

        .modal-right {
            background: #fff;
            padding: 2rem;
        }

        @media (max-width: 767px) {
            .modal-left,
            .modal-right {
                padding: 1.5rem;
            }
        }

        html, body {
    height: 100%;
}

.container-fluid {
    height: calc(100vh - 20px);
    overflow-y: auto;
}
    </style>

    <%--CARD DELGADO USER--%>
    <style>
    /* CARD SUPERIOR USUARIO */

.user-card{
background:white;
border-radius:12px;
padding:14px 18px;
box-shadow:0 6px 18px rgba(0,0,0,.08);
transition:all .25s ease;
}

.user-card:hover{
box-shadow:0 12px 28px rgba(0,0,0,.12);
}

/* avatar */

.user-avatar{
width:38px;
height:38px;
border-radius:50%;
background:linear-gradient(135deg,#540f4a,#ba0c5c);
color:white;
display:flex;
align-items:center;
justify-content:center;
font-size:18px;
}

/* nombre usuario */

.user-name{
font-weight:600;
font-size:.9rem;
}

.user-role{
font-size:.75rem;
color:#777;
}

/* botón logout */

.btn-logout{
background:#540f4a;
color:white;
border-radius:8px;
padding:6px 14px;
font-size:.8rem;
text-decoration:none;
transition:.2s;
}

.btn-logout:hover{
background:#ba0c5c;
color:white;
}

    </style>

<style>
  /* =========================================
   🔥 SUITE AR BADGE
   Estilo corporativo Acurio
========================================= */

.suite-ar-badge {
    position: fixed;
    bottom: 16px;
    right: 16px;
    z-index: 9999;

    display: flex;
    align-items: center;
    gap: 8px;

    padding: 10px 18px;

    /* Degradado corporativo */
    background: linear-gradient(
        135deg,
        #540f4a 0%,
        #975a89 55%,
        #ba0c5c 100%
    );

    color: #ffffff;

    border-radius: 26px;

    font-size: 12px;
    font-weight: 600;
    letter-spacing: .4px;

    /* Transparencia elegante */
    opacity: .92;

    /* Glass effect */
    backdrop-filter: blur(8px);

    /* Sombra premium */
    box-shadow:
        0 6px 18px rgba(84, 15, 74, .35),
        0 2px 4px rgba(0,0,0,.15);

    /* Borde elegante */
    border: 1px solid rgba(255,255,255,.10);

    transition: all .25s ease;
}

/* Hover */
.suite-ar-badge:hover {
    transform: translateY(-2px);
    opacity: 1;

    box-shadow:
        0 8px 22px rgba(186, 12, 92, .35),
        0 3px 6px rgba(0,0,0,.18);
}

/* Icono */
.suite-ar-badge i {
    font-size: 14px;
    color: rgba(255,255,255,.95);
}
</style>

</head>

<body>

<form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="container-fluid px-4">

        <!-- HEADER -->
        <div class="main-header mb-4">
            <div class="header-title">
                <i class="bi bi-journal-text"></i>
                <h1>Gestión de Versiones de Recetas</h1>
            </div>
            <p>Administración y control del versionado documental</p>
        </div>


        <%--CARD DELGADO USER--%>
      <div class="row mb-4">

<div class="col-12">

<div class="user-card d-flex justify-content-between align-items-center">

<!-- Usuario -->

<div class="d-flex align-items-center gap-3">

<div class="user-avatar">
<i class="bi bi-person"></i>
</div>

<div>

<div class="user-name">
<asp:Label ID="lblUsuario" runat="server" Text="Iván"></asp:Label>
</div>

<div class="user-role">
Sesión activa en ARecetas
</div>

</div>

</div>

<!-- Botón cerrar sesión -->

<%--<a href="main.aspx" class="btn btn-logout">

<i class="bi bi-box-arrow-right me-1"></i>
Cerrar sesión

</a>--%>
    <asp:Button
    ID="btnCerrarSesion"
    runat="server"
    CssClass="btn btn-logout"
    Text="Cerrar sesión"
   
    UseSubmitBehavior="false" />


</div>

</div>

</div>


        <!-- CARDS -->

        <div class="row g-4">

            <div class="col-md-4">
                <a href="Gestion_grupos.aspx" class="card-link">
                    <div class="main-card text-center">
                        <div class="icon-circle mx-auto">
                            <i class="bi bi-collection-fill me-1"></i>
                        </div>
                        <h5>Gestión de Manuales</h5>
                        <p class="text-muted">Gestionar los manuales del conjunto de recetas</p>
                    </div>
                </a>
            </div>

            <div class="col-md-4">
                <a href="Gestion_manuales_p.aspx" class="card-link">
                    <div class="main-card text-center">
                        <div class="icon-circle icon-secondary mx-auto">
                            <i class="bi bi-book me-1"></i>
                        </div>
                        <h5>Gestión de Recetas</h5>
                        <p class="text-muted">Gestionar las recetas de los diversos platos</p>
                    </div>
                </a>
            </div>

            <div class="col-md-4">
                <a href="main.aspx" class="card-link">
                    <div class="main-card text-center">
                        <div class="icon-circle mx-auto">
                            <i class="bi bi-graph-up-arrow"></i>
                        </div>
                        <h5>Reportes</h5>
                        <p class="text-muted">Análisis y auditoría</p>
                    </div>
                </a>
            </div>

        </div>

    </div>

    <!-- BOTÓN FLOTANTE -->
   <%--  <div class="position-fixed bottom-0 end-0 p-3 d-flex flex-row gap-2 align-items-center">

<button type="button"
class="btn btn-footer"
data-bs-toggle="modal"
data-bs-target="#infoModal">

<i class="bi bi-info-circle me-1"></i> Información

</button>


</div>--%>

    <!-- MODAL -->
    <div class="modal fade" id="infoModal" tabindex="-1">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body p-0">
                    <div class="row g-0">

                        <div class="col-md-5 modal-left">
                            <i class="bi bi-journal-bookmark-fill"></i>
                            <h4>ARecetas</h4>
                            <p class="opacity-75">
                                Sistema corporativo para la gestión, control y versionado
                                de documentos de recetas.
                            </p>
                        </div>

                        <div class="col-md-7 modal-right">
                            <h6 class="fw-bold mb-3">Funcionalidades</h6>
                            <ul class="text-muted ps-3">
                                <li>Registro de versiones</li>
                                <li>Trazabilidad documental</li>
                                <li>Control y auditoría</li>
                            </ul>

                            <div class="text-end mt-4">
                                <button type="button"
                                    class="btn btn-outline-secondary"
                                    data-bs-dismiss="modal">
                                    Cerrar
                                </button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="suite-ar-badge">

    <i class="bi-grid-3x3-gap-fill"></i>

    <span>Suite AR</span>

    </div>

</form>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

</body>
</html>

