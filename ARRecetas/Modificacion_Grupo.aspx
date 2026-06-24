<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Modificacion_Grupo.aspx.vb" Inherits="ARRecetas.Modificacion_Grupo" %>

<!DOCTYPE html>

<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>ARRecetas</title>

    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Bootstrap Icons -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" rel="stylesheet" />
    <!-- Bootstrap 5 JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>


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

</head>

<body>

<form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="container-fluid px-4">

        <!-- HEADER -->
        <div class="main-header mb-4">
            <div class="header-title">
                <i class="bi bi-journal-text"></i>
                <h1>Gestión de Manuales</h1>
            </div>
            <p>Administración y control de los manuales de recetas</p>
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

        <!-- Formulario -->
        <!-- HEADER FORMULARIO -->

        <div class="mb-3 mt-4">
            <div class="d-flex align-items-center gap-2 mb-1">
                <i class="bi bi-file-earmark-text"
                   style="color:#ba0c5c; font-size:1.2rem;"></i>

                <h5 class="mb-0 fw-semibold" style="color:#540f4a;">
                    Modificación de Manual
                </h5>
            </div>

            <p class="text-muted mb-0" style="font-size:0.8rem;">
                Modificar los campos del Manual.
            </p>
        </div>

         <!-- BOTONES -->
         <%--<div class="d-flex justify-content- gap-2 mt-3">
             
             <asp:Button
                 ID="Button2"
                 runat="server"
                 Text="Nuevo Grupo"
                 CssClass="btn btn-sm text-white"
                 Style="background-color:#ba0c5c;" />
         </div>

        <p></p>--%>
        <!-- FORMULARIO -->
            <!-- FORMULARIO -->
            <div class="card border-0 shadow-sm rounded-4">
                <div class="card-body p-3">

                    <div class="row g-3">

                        <!-- Descripción Documento -->
                        <div class="col-md-6">
                            <label class="form-label fw-semibold" style="font-size:0.8rem;">
                                Descripción del Manual
                            </label>
                            <asp:TextBox
                                ID="txtDescripcionDocumento"
                                runat="server"
                                CssClass="form-control form-control-sm"
                                placeholder="Ej: Grupo de Recetas Tanta Miraflores 1er Semestre 2026" />
                        </div>

                        <!-- Selección Insumo / Receta -->
                        <%--<div class="col-md-6">
                            <label class="form-label fw-semibold" style="font-size:0.8rem;">
                                Centro de Costo
                            </label>

                            <div class="input-group input-group-sm">
                            
                                <asp:DropDownList ID="cbocentro_costos" runat="server" CssClass="form-select banner-dropdown"></asp:DropDownList>

                            </div>
                        </div>--%>

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
                            Text="Guardar Manual"
                            CssClass="btn btn-sm text-white"
                            Style="background-color:#ba0c5c;" />


                    </div>

                </div>
            </div>

         <p></p>
        <div class="card border-0 shadow-sm rounded-4">
            <div class="card-body p-3">

                <!-- Título -->
               <div class="row mb-3">
                    <div class="col d-flex justify-content-between align-items-center">
                        <h5 class="fw-semibold mb-0">
                            Recetas asociadas al Manual
                        </h5>

                        <%--<button type="button"
                                class="btn btn-sm btn-success"
                                data-bs-toggle="modal"
                                data-bs-target="#modalSeleccionManuales">
                            <i class="bi bi-plus-lg"></i> Agregar Manual
                        </button>--%>
                        <asp:Button 
                        ID="btnAgregarManual" 
                        runat="server"
                        Text="Agregar Receta"
                        CssClass="btn btn-sm btn-success"
                        OnClick="btnAgregarManual_Click"
                        UseSubmitBehavior="false" />

                    </div>
                </div>

                <!-- Grid -->
                <div class="row">

<%--                    <div class="col-6 table-responsive">
                        <div class="row mb-3">
                            <div class="col">
                                <h6 class="mb-0">Listado de Manuales - Maestro</h6>
                            </div>
                        </div>
                        <asp:GridView ID="grvListaManuales_Maestro"
                            runat="server"
                            AutoGenerateColumns="False"
                            CssClass="table table-hover align-middle table-bordered mb-0"
                            DataKeyNames="id_manual,cod_manual,desc_manual,cod_centro_sap_manual,cod_sap_rb_manual,desc_sap_rb_manual"
                            >

                            <Columns>
                                 <asp:TemplateField HeaderText="" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                     <ItemTemplate>
                                         <asp:LinkButton
                                             ID="btnSeleccionar"
                                             runat="server"
                                             CommandName="quitar_A_Grupo"
                                             CommandArgument="<%# Container.DataItemIndex %>"
                                             CssClass="btn btn-sm btn-outline-primary"
                                             ToolTip="Quitar Manual relacionado al Grupo">
                                             <i class="bi-plus-lg"></i>
                                         </asp:LinkButton>

                                     </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:BoundField DataField="cod_manual"
                                    HeaderText="Cod. Manual"/>

                                <asp:BoundField DataField="desc_manual"
                                    HeaderText="Descripción" />

                                <asp:BoundField DataField="cod_centro_sap_manual"
                                    HeaderText="Centro" />

                                <asp:BoundField DataField="cod_sap_rb_manual"
                                    HeaderText="Cod. SAP" />

                                <asp:BoundField DataField="desc_sap_rb_manual"
                                    HeaderText="Desc. SAP" />

                            </Columns>

                        </asp:GridView>
                    </div>--%>

                    <div class="col-12 table-responsive">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grvListaManuales"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    CssClass="table table-hover align-middle table-bordered mb-0"
                                    DataKeyNames="id_manual,cod_manual,desc_manual,cod_centro_sap_manual,cod_sap_rb_manual,desc_sap_rb_manual"
                                    >

                                    <Columns>
                                         <asp:TemplateField HeaderText="" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:LinkButton
                                                     ID="btnSeleccionar"
                                                     runat="server"
                                                     CommandName="eliminar"
                                                     CommandArgument="<%# Container.DataItemIndex %>"
                                                     CssClass="btn btn-sm btn-outline-primary"
                                                     ToolTip="Quitar Receta relacionado al Manual">
                                                     <i class="bi bi-x-lg"></i>
                                                 </asp:LinkButton>

                                             </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:BoundField DataField="cod_manual"
                                            HeaderText="Cod. Receta"/>

                                        <asp:BoundField DataField="desc_manual"
                                            HeaderText="Descripción" />

                                        <asp:BoundField DataField="cod_centro_sap_manual"
                                            HeaderText="Centro" />

                                        <asp:BoundField DataField="cod_sap_rb_manual"
                                            HeaderText="Cod. SAP" />

                                        <asp:BoundField DataField="desc_sap_rb_manual"
                                            HeaderText="Desc. SAP" />

                                    </Columns>

                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </div>
                </div>

            </div>
        </div>





    </div>

    <!-- BOTÓN FLOTANTE -->
  <%--  <button type="button"
        class="btn btn-footer"
        data-bs-toggle="modal"
        data-bs-target="#infoModal">
    <i class="bi bi-info-circle me-1"></i> InformaciónX</button>

    <button type="button"
            class="btn btn-footer"
            data-bs-toggle="modal"
            data-bs-target="#infoModal">
        <i class="bi bi-info-circle me-1"></i> Información</button>--%>
    
    <div class="position-fixed bottom-0 end-0 p-3 d-flex gap-2">
    <button type="button"
            class="btn btn-footer"
            data-bs-toggle="modal"
            data-bs-target="#infoModal">
        <i class="bi bi-info-circle me-1"></i> Información
    </button>

   <button type="button"
        class="btn btn-footer"
        onclick="window.location.href='main.aspx';">
    <i class="bi bi-house me-1"></i> Principal
    </button>

        <button type="button"
class="btn btn-footer"
onclick="window.location.href='main.aspx';">

<i class="bi bi-box-arrow-right me-1"></i> Cerrar sesión

</button>

</div>

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


    <div class="modal fade" id="InformativoModal" tabindex="-1" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-sm">
        <div class="modal-content border-0 shadow rounded-4">

            <!-- Header -->
            <div class="modal-header border-0 justify-content-center pb-0">
                <i class="bi bi-check-circle-fill text-success"
                   style="font-size:3rem;"></i>
            </div>

            <!-- Body -->
            <div class="modal-body text-center px-4">
                <h5 class="fw-bold mt-2">Operación exitosa</h5>

                <p class="text-muted mt-2 mb-4">
                    El grupo de manuales de recetas se creó correctamente.
                </p>

                <button type="button"
                        class="btn btn-success px-4"
                        data-bs-dismiss="modal">
                    Aceptar
                </button>
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

    <div class="modal fade" id="modalSeleccionManuales" tabindex="-1">
    <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content rounded-4 border-0 shadow">

            <!-- HEADER -->
            <div class="modal-header" style="background:#540f4a;">
                <h6 class="modal-title text-white">
                    <i class="bi bi-search me-2"></i>
                    Seleccionar Receta
                </h6>
                <button type="button"
                        class="btn-close btn-close-white"
                        data-bs-dismiss="modal"></button>
            </div>

            <!-- BODY -->
            <div class="modal-body">

                <asp:UpdatePanel ID="upModalReceta" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <!-- FILTROS -->
                        <div class="row g-3 mb-4">

                            <div class="col-md-6">
                                <label class="form-label fw-semibold text-secondary">
                                    Código de receta 
                                </label>
                                <asp:TextBox ID="txtcod_manual"
                                    runat="server"
                                    CssClass="form-control"
                                    placeholder="Ej: RCT0000001" />
                            </div>

                            <div class="col-md-6">
                                <label class="form-label fw-semibold text-secondary">
                                    Descripción de receta
                                </label>

                                <div class="input-group">
                                    <asp:TextBox ID="txtdesc_manual"
                                        runat="server"
                                        CssClass="form-control"
                                        placeholder="Ej: Arroz grano largo" />

                                    <asp:Button ID="btnBuscarManual"
                                        runat="server"
                                        CssClass="btn btn-outline-primary"
                                        Text="Buscar"
                                        OnClick="btnBuscarManual_Click" />
                                </div>
                            </div>

                        </div>

                        <!-- GRID -->
                        <div class="table-responsive">
                            <asp:GridView ID="grvManualSelect"
                                runat="server"
                                AutoGenerateColumns="False"
                                CssClass="table table-hover align-middle table-bordered"
                                DataKeyNames="id_manual,cod_manual,desc_manual,cod_centro_sap_manual,cod_sap_rb_manual,desc_sap_rb_manual"
                                OnRowCommand="grvmanualSelect_RowCommand">

                                <Columns>

                                    <asp:TemplateField HeaderText="Sel">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSeleccionar"
                                                runat="server"
                                                CssClass="btn btn-sm btn-outline-primary"
                                                CommandName="Seleccionar"
                                                CommandArgument='<%# Container.DataItemIndex %>'>
                                                <i class="bi bi-check-circle"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="cod_manual"
                                        HeaderText="Código" />

                                    <asp:BoundField DataField="desc_manual"
                                        HeaderText="Descripción" />

                                    <asp:BoundField DataField="cod_centro_sap_manual"
                                        HeaderText="Centro" />
                                    
                                    <asp:BoundField DataField="cod_sap_rb_manual"
                                        HeaderText="Código SAP" />

                                    <asp:BoundField DataField="desc_sap_rb_manual"
                                        HeaderText="Descripción SAP" />

                                    
                                    
                                </Columns>

                            </asp:GridView>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

            <!-- FOOTER -->
            <div class="modal-footer">
                <button type="button"
                        class="btn btn-outline-secondary"
                        data-bs-dismiss="modal">
                    Cerrar
                </button>
            </div>

        </div>
    </div>
</div>



    <div class="modal fade" id="modalListaManuales" tabindex="-1">
        <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content rounded-4 border-0 shadow">

                <!-- HEADER -->
                <div class="modal-header" style="background:#540f4a;">
                    <h5 class="modal-title text-white">
                        <i class="bi bi-search me-2"></i>
                        Listado de Manuales asociados al Grupo
                    </h5>
                    <button type="button"
                            class="btn-close btn-close-white"
                            data-bs-dismiss="modal"></button>
                </div>

                <!-- BODY -->
                <div class="modal-body">
                    <%-- <div class="mb-3">
                         <h6 class="fw-bold text-uppercase text-secondary mb-1">
                             <i class="bi bi-journal-text me-2"></i>
                             Lista de Manuales para asignar
                         </h6>
                         <hr class="mt-1 mb-0" />
                     </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="table-responsive">
                            <asp:GridView ID="grvListaManuales_libres"
                                runat="server"
                                AutoGenerateColumns="False"
                                CssClass="table table-hover align-middle table-bordered mb-0"
                                DataKeyNames="cod_manual,desc_manual,cod_centro_sap_manual,cod_sap_rb_manual,desc_sap_rb_manual"
                                >

                                <Columns>
                                       <asp:TemplateField HeaderText="" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                           <ItemTemplate>
                                               <asp:LinkButton
                                                   ID="btnSeleccionar"
                                                   runat="server"
                                                   CommandName="Asociar_A_Grupo"
                                                   CommandArgument="<%# Container.DataItemIndex %>"
                                                   CssClass="btn btn-sm btn-outline-primary"
                                                   ToolTip="Agregar Manual al Grupo">
                                                   <i class="bi bi-plus-lg"></i>
                                               </asp:LinkButton>

                                           </ItemTemplate>
                                       </asp:TemplateField>

                                    <asp:BoundField DataField="cod_manual"
                                        HeaderText="Cod. Manual"/>

                                    <asp:BoundField DataField="desc_manual"
                                        HeaderText="Descripción" />

                                    <asp:BoundField DataField="cod_centro_sap_manual"
                                        HeaderText="Centro" />

                                    <asp:BoundField DataField="cod_sap_rb_manual"
                                        HeaderText="Cod. SAP" />

                                    <asp:BoundField DataField="desc_sap_rb_manual"
                                        HeaderText="Desc. SAP" />

                                </Columns>

                            </asp:GridView>
                        </div>

                    </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    

                  <%--  <div class="mb-3">
                        <h6 class="text-secondary mb-1">
                            <i class="bi bi-journal-text me-2"></i>
                            Manuales que fueron asignados al Grupo al momento de su registro:
                        </h6>
                        <hr class="mt-1 mb-0" />
                    </div>--%>

                    <asp:UpdatePanel ID="upModalGrupo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="table-responsive">
                                <asp:GridView ID="grvListaManualesx"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    CssClass="table table-hover align-middle table-bordered mb-0"
                                    DataKeyNames="id_manual,cod_manual,desc_manual,cod_centro_sap_manual,cod_sap_rb_manual,desc_sap_rb_manual"
                                    >

                                    <Columns>
                                         <%--<asp:TemplateField HeaderText="" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:LinkButton
                                                     ID="btnSeleccionar"
                                                     runat="server"
                                                     CommandName="quitar_A_Grupo"
                                                     CommandArgument="<%# Container.DataItemIndex %>"
                                                     CssClass="btn btn-sm btn-outline-primary"
                                                     ToolTip="Quitar Manual relacionado al Grupo">
                                                     <i class="bi bi-dash-lg"></i>
                                                 </asp:LinkButton>

                                             </ItemTemplate>
                                         </asp:TemplateField>--%>
                                        <asp:BoundField DataField="cod_manual"
                                            HeaderText="Cod. Manual"/>

                                        <asp:BoundField DataField="desc_manual"
                                            HeaderText="Descripción" />

                                        <asp:BoundField DataField="cod_centro_sap_manual"
                                            HeaderText="Centro" />

                                        <asp:BoundField DataField="cod_sap_rb_manual"
                                            HeaderText="Cod. SAP" />

                                        <asp:BoundField DataField="desc_sap_rb_manual"
                                            HeaderText="Desc. SAP" />

                                    </Columns>

                                </asp:GridView>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

                <!-- FOOTER -->
                <div class="modal-footer">
                    <button type="button"
                            class="btn btn-outline-secondary"
                            data-bs-dismiss="modal">
                        Cerrar</button>
                </div>

            </div>
        </div>
    </div>


    <div class="modal fade" id="modalModificarGrupo" tabindex="-1">
    <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content rounded-4 border-0 shadow">

            <!-- HEADER -->
            <div class="modal-header" style="background:#540f4a;">
                <h5 class="modal-title text-white">
                    <i class="bi bi-search me-2"></i>
                    Modificar Grupo
                </h5>
                <button type="button"
                        class="btn-close btn-close-white"
                        data-bs-dismiss="modal"></button>
            </div>

            <!-- BODY -->
            <div class="modal-body">

              

            </div>

            <!-- FOOTER -->
            <div class="modal-footer">
                <button type="button"
                        class="btn btn-outline-secondary"
                        data-bs-dismiss="modal">
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
