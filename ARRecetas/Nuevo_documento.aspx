<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Nuevo_documento.aspx.vb" Inherits="ARRecetas.Nuevo_documento" %>

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
            position: fixed;
            bottom: 22px;
            right: 22px;
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

        <!-- Formulario -->
        <!-- HEADER FORMULARIO -->

        <div class="mb-3 mt-4">
            <div class="d-flex align-items-center gap-2 mb-1">
                <i class="bi bi-file-earmark-text"
                   style="color:#ba0c5c; font-size:1.2rem;"></i>

                <h5 class="mb-0 fw-semibold" style="color:#540f4a;">
                    Registro de Documento de Receta
                </h5>
            </div>

            <p class="text-muted mb-0" style="font-size:0.8rem;">
                Complete la información requerida para registrar una nueva versión del documento
            </p>
        </div>


        <!-- FORMULARIO -->
            <!-- FORMULARIO -->
            <div class="card border-0 shadow-sm rounded-4">
                <div class="card-body p-3">

                    <div class="row g-3">

                        <!-- Descripción Documento -->
                        <div class="col-md-6">
                            <label class="form-label fw-semibold" style="font-size:0.8rem;">
                                Descripción del Documento
                            </label>
                            <asp:TextBox
                                ID="txtDescripcionDocumento"
                                runat="server"
                                CssClass="form-control form-control-sm"
                                placeholder="Ej: Actualización receta base - Q2 2026" />
                        </div>

                        <!-- Selección Insumo / Receta -->
                        <div class="col-md-6">
                            <label class="form-label fw-semibold" style="font-size:0.8rem;">
                                Insumo o Receta Asociada
                            </label>

                            <div class="input-group input-group-sm">
                                <asp:TextBox
                                    ID="txtReceta"
                                    runat="server"
                                    CssClass="form-control"
                                    ReadOnly="true"
                                    placeholder="Seleccione un insumo o receta" />

                                <button type="button"
                                    class="btn btn-outline-secondary"
                                    data-bs-toggle="modal"
                                    data-bs-target="#modalSeleccionReceta">
                                    <i class="bi bi-search"></i>
                                </button>

                            </div>
                        </div>

                        <!-- Otras Opciones -->
                        <div class="col-md-6">
                            <label class="form-label fw-semibold" style="font-size:0.8rem;">
                                Observaciones / Otras Opciones
                            </label>
                            <asp:TextBox
                                ID="txtObservaciones"
                                runat="server"
                                CssClass="form-control form-control-sm"
                                TextMode="MultiLine"
                                Rows="3"
                                placeholder="Ingrese observaciones adicionales (opcional)" />
                        </div>

                    </div>

                    <!-- BOTONES -->
                    <div class="d-flex justify-content-end gap-2 mt-3">
                        <asp:Button
                            ID="btnCancelar"
                            runat="server"
                            Text="Cancelar"
                            CssClass="btn btn-light btn-sm" />

                        <asp:Button
                            ID="btnGuardar"
                            runat="server"
                            Text="Guardar Documento"
                            CssClass="btn btn-sm text-white"
                            Style="background-color:#ba0c5c;" />
                    </div>

                </div>
            </div>




    </div>

    <!-- BOTÓN FLOTANTE -->
    <button type="button"
            class="btn btn-footer"
            data-bs-toggle="modal"
            data-bs-target="#infoModal">
        <i class="bi bi-info-circle me-1"></i> Información
    </button>

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


    <!-- MODAL SELECCIÓN RECETA -->
    <%--<div class="modal fade" id="modalSeleccionReceta" tabindex="-1">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content rounded-4 border-0">

                <div class="modal-header" style="background:#540f4a;">
                    <h5 class="modal-title text-white">
                        <i class="bi bi-list-ul me-2"></i>
                        Seleccionar Insumo o Receta
                    </h5>
                    <button type="button"
                        class="btn-close btn-close-white"
                        data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">
                    <!-- Aquí puedes colocar un GridView o buscador -->
                    <p class="text-muted mb-0">
                        Aquí se listarán los insumos o recetas disponibles.
                    </p>
                </div>

                <div class="modal-footer">
                    <button type="button"
                        class="btn btn-outline-secondary"
                        data-bs-dismiss="modal">
                        Cerrar
                    </button>
                </div>

            </div>
        </div>
    </div>--%>

    <div class="modal fade" id="modalSeleccionReceta" tabindex="-1">
    <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content rounded-4 border-0 shadow">

            <!-- HEADER -->
            <div class="modal-header" style="background:#540f4a;">
                <h5 class="modal-title text-white">
                    <i class="bi bi-search me-2"></i>
                    Seleccionar Insumo o Receta
                </h5>
                <button type="button"
                        class="btn-close btn-close-white"
                        data-bs-dismiss="modal"></button>
            </div>

            <!-- BODY -->
            <div class="modal-body">

                <!-- FILTROS DE BÚSQUEDA -->
                <div class="row g-3 mb-4">

                    <!-- Código Insumo -->
                    <div class="col-md-6">
                        <label class="form-label fw-semibold text-secondary">
                            Código de Insumo
                        </label>
                        <div class="input-group">
                            <span class="input-group-text bg-light">
                                <i class="bi bi-upc-scan"></i>
                            </span>
                            <input type="text"
                                   class="form-control"
                                   id="txtCodigoInsumo"
                                   placeholder="Ej: INS-00045">
                            <button class="btn btn-outline-primary"
                                    type="button"
                                    id="btnBuscarCodigo">
                                <i class="bi bi-search"></i>
                            </button>
                        </div>
                    </div>

                    <!-- Descripción Insumo -->
                    <div class="col-md-6">
                        <label class="form-label fw-semibold text-secondary">
                            Descripción de Insumo
                        </label>
                        <div class="input-group">
                            <span class="input-group-text bg-light">
                                <i class="bi bi-card-text"></i>
                            </span>
                            <input type="text"
                                   class="form-control"
                                   id="txtDescripcionInsumo"
                                   placeholder="Ej: Arroz grano largo">
                            <button class="btn btn-outline-primary"
                                    type="button"
                                    id="btnBuscarDescripcion">
                                <i class="bi bi-search"></i>
                            </button>
                        </div>
                    </div>

                </div>

                <!-- RESULTADOS -->
                <div class="border rounded-3 p-3 bg-light">
                    <p class="text-muted mb-0">
                        Aquí se listarán los insumos o recetas disponibles según la búsqueda.
                    </p>
                    <!-- Aquí luego puedes meter un GridView, tabla o repeater -->
                </div>

            </div>

            <!-- FOOTER -->
            <div class="modal-footer">
                <button type="button"
                        class="btn btn-outline-secondary"
                        data-bs-dismiss="modal">
                    <i class="bi bi-x-circle me-1"></i>
                    Cerrar
                </button>
            </div>

        </div>
    </div>
</div>




</form>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

</body>
</html>


