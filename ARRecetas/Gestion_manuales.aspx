<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Gestion_manuales.aspx.vb" Inherits="ARRecetas.Gestion_manuales" %>

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

        /*adjuntar imagen*/
        .upload-area{
        border:2px dashed #ba0c5c;
        border-radius:10px;
        padding:25px;
        text-align:center;
        cursor:pointer;
        background:#fafafa;
        transition:all .25s ease;
        }

        .upload-area:hover{
        background:#f3f0f3;
        border-color:#540f4a;
        }

        .upload-icon{
        font-size:28px;
        color:#ba0c5c;
        margin-bottom:6px;
        }

        .upload-text{
        font-size:0.8rem;
        margin:0;
        color:#555;
        }

        .upload-area.dragover{
        background:#f5e9f1;
        border-color:#540f4a;
        }

        /*lista desplegable*/

        .modern-select{
position:relative;
}

.select-icon{
position:absolute;
left:10px;
top:50%;
transform:translateY(-50%);
color:#975a89;
font-size:14px;
z-index:2;
}

.modern-dropdown{
padding-left:32px;
border-radius:8px;
border:1px solid #ddd;
box-shadow:0 2px 6px rgba(0,0,0,.05);
transition:all .2s ease;
font-size:.85rem;
}

.modern-dropdown:focus{
border-color:#ba0c5c;
box-shadow:0 0 0 0.15rem rgba(186,12,92,.15);
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
                <h1>Registro de Receta</h1>
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

        <!-- Formulario -->
        <!-- HEADER FORMULARIO -->

        <div class="mb-3 mt-4">
            <div class="d-flex align-items-center gap-2 mb-1">
                <i class="bi bi-file-earmark-text"
                   style="color:#ba0c5c; font-size:1.2rem;"></i>

                <h5 class="mb-0 fw-semibold" style="color:#540f4a;">
                    Ingresar la información
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
                                Ingrese Descripción del Documento de la Receta
                            </label>
                  
                                    <asp:TextBox
                                    ID="txtDescripcionDocumento"
                                    runat="server"
                                    CssClass="form-control form-control-sm"
                                    placeholder="Ej: Actualización receta base - Q2 2026" />
                          
                            
                        </div>


                        <asp:HiddenField ID="hfIdGrupo" runat="server" />
                        <asp:Label ID="lblidgrupo" runat="server" Text="" Visible="False"></asp:Label>
                        <!--Selección de Grupo-->
                        <div class="col-md-6">
                            <label class="form-label fw-semibold" style="font-size:0.8rem;">
                                Seleccione el Manual
                            </label>

                            <div class="input-group input-group-sm">
                           
                                <asp:TextBox
                                ID="txtgruposeleccionado"
                                runat="server"
                                CssClass="form-control"
                                ReadOnly="true"
                                placeholder="Seleccione el Manual" />
                          

                                <button type="button"
                                    class="btn btn-outline-secondary"
                                    data-bs-toggle="modal"
                                    data-bs-target="#modalSeleccionGrupo">
                                    <i class="bi bi-search"></i>
                                </button>

                            </div>
                        </div>


                        <%--<div class="col-md-6">
                            <label class="form-label fw-semibold" style="font-size:0.8rem;">
                                Seleccione el Centro de Costo
                            </label>

                            <div class="input-group input-group-sm">
                              
                                <asp:DropDownList ID="cbocentro_costos" runat="server" CssClass="form-select banner-dropdown"></asp:DropDownList>
                         

                            </div>
                        </div>--%>
                        <div class="col-md-6">

                        <label class="form-label fw-semibold" style="font-size:0.8rem;">
                        Seleccione el Centro
                        </label>

                        <div class="modern-select">

                        <i class="bi bi-building select-icon"></i>

                        <asp:DropDownList
                        ID="cbocentro_costos"
                        runat="server"
                        CssClass="form-select modern-dropdown">
                        </asp:DropDownList>

                        </div>

                        </div>


                        <!-- Selección Insumo / Receta -->
                        <div class="col-md-6">
                            <label class="form-label fw-semibold" style="font-size:0.8rem;">
                                Seleccione Insumo o Receta Asociada
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
                        <div class="col-md-12">
                            <label class="form-label fw-semibold" style="font-size:0.8rem;">
                                Detalle la Preparación de la receta
                            </label>
                           
                                    <asp:TextBox
                                    ID="txtObservaciones"
                                    runat="server"
                                    CssClass="form-control form-control-sm"
                                    TextMode="MultiLine"
                                    Rows="3"
                                    placeholder="Preparación" />
                       

                        </div>

                        <%------Adjuntar archivo ----%>


                        <%--<div class="col-md-6">

                        <label class="form-label fw-semibold" style="font-size:0.8rem;">
                        Adjuntar Imagen de la Receta
                        </label>

                        <div id="uploadArea" class="upload-area">

                        <i class="bi bi-cloud-arrow-up upload-icon"></i>

                        <p class="upload-text">
                        Arrastre una imagen JPG o haga clic para seleccionarla
                        </p>

                        <asp:FileUpload
                        ID="fileImagen"
                        runat="server"
                        CssClass="d-none"
                        accept=".jpg,.jpeg"
                        />

                        </div>

                        <!-- Nombre del archivo -->
                        <div id="fileNamePreview" class="small text-muted mt-2"></div>

                        <!-- Vista previa de la imagen -->
                        <img id="imagePreview"
                                style="max-width:140px;
                                    margin-top:8px;
                                    display:none;
                                    border-radius:6px;
                                    box-shadow:0 4px 10px rgba(0,0,0,.15);" />

                        </div>--%>

                        <div class="row">

<!-- COLUMNA 1 : SUBIR IMAGEN -->

<div class="col-md-6">

<label class="form-label fw-semibold" style="font-size:0.8rem;">
Adjuntar Imagen de la Receta
</label>

<div id="uploadArea" class="upload-area">

<i class="bi bi-cloud-arrow-up upload-icon"></i>

<p class="upload-text">
Arrastre una imagen JPG o haga clic para seleccionarla
</p>

<asp:FileUpload
ID="fileImagen"
runat="server"
CssClass="d-none"
accept=".jpg,.jpeg"
/>

</div>

<!-- Nombre del archivo -->
<div id="fileNamePreview" class="small text-muted mt-2"></div>

</div>


            <!-- COLUMNA 2 : PREVIEW -->

            <div class="col-md-6">

            <label class="form-label fw-semibold" style="font-size:0.8rem;">
            Vista previa de la imagen
            </label>

            <div class="preview-container text-center">

            <img id="imagePreview"
                 style="max-width:220px;
                        display:none;
                        border-radius:8px;
                        box-shadow:0 6px 18px rgba(0,0,0,.2);" />

            </div>

            <!-- BOTÓN ELIMINAR -->

            <button type="button"
                    id="btnEliminarImagen"
                    class="btn btn-sm btn-outline-danger mt-2"
                    style="display:none;">
            <i class="bi bi-trash"></i> Eliminar imagen
            </button>




            </div>

</div>



                    </div>

                    <!-- BOTONES -->
                    <div class="d-flex justify-content-end gap-2 mt-3">
                        <asp:Button
                            ID="btnCancelar"
                            runat="server"
                            Text="Cancelar"
                            CssClass="btn btn-light btn-sm" PostBackUrl="~/Gestion_Manuales_p.aspx" />

                        <asp:Button
                            ID="btnGuardar"
                            runat="server"
                            Text="Guardar Receta"
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
                        <div class="col">
                            <h6 class="fw-semibold mb-0">Detalle de la receta seleccionada - SAP</h6>
                        </div>
                    </div>

                    <!-- Grid -->
                    <div class="row">
                        <div class="col-12">
                            <div style="max-height:400px; overflow-y:auto;" class="table-responsive">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>

                               
                                        <asp:GridView
                                            ID="grvdetalle_receta_seleccionada"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            CssClass="table table-sm table-hover align-middle mb-0"
                                            GridLines="None"
                                            EmptyDataText="No existen grupos registrados."
                                            DataKeyNames="id">

                                            <Columns>

                    
                                               <%-- <asp:TemplateField HeaderText="" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton
                                                            ID="btnSeleccionar"
                                                            runat="server"
                                                            CommandName="Seleccionar"
                                                            CssClass="btn btn-sm btn-outline-primary"
                                                            ToolTip="Seleccionar grupo">
                                                            <i class="bi bi-check2-circle"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                   
                                                <asp:BoundField
                                                    DataField="id"
                                                    HeaderText="ID" />

                                                 <asp:BoundField
                                                     DataField="codigo_padre"
                                                     HeaderText="Código Padre" />

                                                <asp:BoundField
                                                    DataField="descripcion_padre"
                                                    HeaderText="Descriçión" />

                                                <asp:BoundField
                                                    DataField="codigo"
                                                    HeaderText="Código" />

                                                <asp:BoundField
                                                    DataField="descripcion"
                                                    HeaderText="Descripción" />


                                                <asp:BoundField
                                                    DataField="unidad_medida"
                                                    HeaderText="UM" />
               
                                                 <asp:BoundField
                                                    DataField="cantidad"
                                                    HeaderText="Cantidad" />

                                                <asp:BoundField
                                                    DataField="unidad_medida_cabecera"
                                                    HeaderText="UM C."/>

                                                <asp:BoundField
                                                    DataField="cantidad_cabecera"
                                                    HeaderText="cantidad C."/>


                                            </Columns>

                                            <HeaderStyle CssClass="table-light fw-semibold" />

                                        </asp:GridView>


                                </ContentTemplate>
                            </asp:UpdatePanel>
                             </div>
                        </div>
                    </div>

                </div>
            </div>








    </div>

    <!-- BOTÓN FLOTANTE -->
   <%-- <button type="button"
            class="btn btn-footer"
            data-bs-toggle="modal"
            data-bs-target="#infoModal">
        <i class="bi bi-info-circle me-1"></i> Información
    </button>--%>
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

         <%--   <button type="button"
class="btn btn-footer"
onclick="window.location.href='main.aspx';">

<i class="bi bi-box-arrow-right me-1"></i> Cerrar sesión

</button>--%>

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
                    La receta se creó correctamente.
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


 


<div class="modal fade" id="modalSeleccionReceta" tabindex="-1">
    <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content rounded-4 border-0 shadow">

            <!-- HEADER -->
            <div class="modal-header" style="background:#540f4a;">
                <h6 class="modal-title text-white">
                    <i class="bi bi-search me-2"></i>
                    Seleccionar Insumo o Receta
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
                                    Código de Insumo
                                </label>
                                <asp:TextBox ID="txtcod_buscar"
                                    runat="server"
                                    CssClass="form-control"
                                    placeholder="Ej: INS-00045" />
                            </div>

                            <div class="col-md-6">
                                <label class="form-label fw-semibold text-secondary">
                                    Descripción de Insumo
                                </label>

                                <div class="input-group">
                                    <asp:TextBox ID="txtdesc_buscar"
                                        runat="server"
                                        CssClass="form-control"
                                        placeholder="Ej: Arroz grano largo" />

                                    <asp:Button ID="btnBuscarReceta"
                                        runat="server"
                                        CssClass="btn btn-outline-primary"
                                        Text="Buscar"
                                        OnClick="btnBuscarReceta_Click" />
                                </div>
                            </div>

                        </div>

                        <!-- GRID -->
                        <div class="table-responsive">
                            <asp:GridView ID="grvrecetasSelect"
                                runat="server"
                                AutoGenerateColumns="False"
                                CssClass="table table-hover align-middle table-bordered"
                                DataKeyNames="material,descripcion_material,unidad_medida"
                                OnRowCommand="grvrecetasSelect_RowCommand">

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

                                    <asp:BoundField DataField="material"
                                        HeaderText="Cod. Material" />

                                    <asp:BoundField DataField="descripcion_material"
                                        HeaderText="Descripción" />

                                    <asp:BoundField DataField="unidad_medida"
                                        HeaderText="UM" />

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


    <div class="modal fade" id="modalSeleccionGrupo" tabindex="-1">
    <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content rounded-4 border-0 shadow">

            <!-- HEADER -->
            <div class="modal-header" style="background:#540f4a;">
                <h6 class="modal-title text-white">
                    <i class="bi bi-search me-2"></i>
                    Seleccionar el Manual
                </h6>
                <button type="button"
                        class="btn-close btn-close-white"
                        data-bs-dismiss="modal"></button>
            </div>

            <!-- BODY -->
            <div class="modal-body">

                <asp:UpdatePanel ID="upModalGrupo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div class="table-responsive">
                            <asp:GridView ID="grvGrupo"
                                runat="server"
                                AutoGenerateColumns="False"
                                CssClass="table table-hover align-middle table-bordered mb-0"
                                DataKeyNames="id_grupo,desc_grupo,fec_reg_grupo"
                                OnRowCommand="grvGrupo_RowCommand">

                                <Columns>

                                    <asp:TemplateField HeaderText="Sel" ItemStyle-Width="60px">
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

                                    <asp:BoundField DataField="id_grupo"
                                        HeaderText="ID"
                                        Visible="False" />

                                    <asp:BoundField DataField="desc_grupo"
                                        HeaderText="Descripción del Manual" />

                                    <asp:BoundField DataField="fec_reg_grupo"
                                        HeaderText="Fecha Registro"
                                        DataFormatString="{0:dd/MM/yyyy}"
                                        ItemStyle-Width="140px" />

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



</form>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

<%--adjuntar imagen--%>

    <%--<script>

		const uploadArea = document.getElementById("uploadArea");
		const fileInput = document.getElementById("<%= fileImagen.ClientID %>");
		const preview = document.getElementById("fileNamePreview");
		const imagePreview = document.getElementById("imagePreview");

		/* evitar que el navegador abra el archivo */

		document.addEventListener("dragover", function (e) {
			e.preventDefault();
		});

		document.addEventListener("drop", function (e) {
			e.preventDefault();
		});

		/* abrir selector al hacer click */

		uploadArea.addEventListener("click", function () {
			fileInput.click();
		});

		/* cuando se selecciona archivo */

		fileInput.addEventListener("change", function () {

			if (this.files.length > 0) {

				preview.innerText = "Archivo seleccionado: " + this.files[0].name;

				const reader = new FileReader();

				reader.onload = function (e) {

					imagePreview.src = e.target.result;
					imagePreview.style.display = "block";

				}

				reader.readAsDataURL(this.files[0]);

			}

		});

		/* cuando se arrastra archivo */

		uploadArea.addEventListener("dragover", function (e) {
			e.preventDefault();
			uploadArea.classList.add("dragover");
		});

		uploadArea.addEventListener("dragleave", function () {
			uploadArea.classList.remove("dragover");
		});

		/* cuando se suelta archivo */

		uploadArea.addEventListener("drop", function (e) {

			e.preventDefault();
			uploadArea.classList.remove("dragover");

			const files = e.dataTransfer.files;

			if (files.length > 0) {

				fileInput.files = files;

				preview.innerText = "Archivo seleccionado: " + files[0].name;

				const reader = new FileReader();

				reader.onload = function (ev) {

					imagePreview.src = ev.target.result;
					imagePreview.style.display = "block";

				}

				reader.readAsDataURL(files[0]);

			}

		});

	</script>--%>

<script>

	const uploadArea = document.getElementById("uploadArea");
	const fileInput = document.getElementById("<%= fileImagen.ClientID %>");
	const preview = document.getElementById("fileNamePreview");
	const imagePreview = document.getElementById("imagePreview");
	const btnEliminar = document.getElementById("btnEliminarImagen"); // ← FALTABA

	/* evitar que el navegador abra el archivo */

	document.addEventListener("dragover", function (e) {
		e.preventDefault();
	});

	document.addEventListener("drop", function (e) {
		e.preventDefault();
	});

	/* abrir selector al hacer click */

	uploadArea.addEventListener("click", function () {
		fileInput.click();
	});

	/* cuando se selecciona archivo */

	fileInput.addEventListener("change", function () {

		if (this.files.length > 0) {

			preview.innerText = "Archivo seleccionado: " + this.files[0].name;

			const reader = new FileReader();

			reader.onload = function (e) {

				imagePreview.src = e.target.result;
				imagePreview.style.display = "block";

				btnEliminar.style.display = "inline-block"; // ← MOSTRAR BOTÓN

			}

			reader.readAsDataURL(this.files[0]);

		}

	});

	/* cuando se arrastra archivo */

	uploadArea.addEventListener("dragover", function (e) {
		e.preventDefault();
		uploadArea.classList.add("dragover");
	});

	uploadArea.addEventListener("dragleave", function () {
		uploadArea.classList.remove("dragover");
	});

	/* cuando se suelta archivo */

	uploadArea.addEventListener("drop", function (e) {

		e.preventDefault();
		uploadArea.classList.remove("dragover");

		const files = e.dataTransfer.files;

		if (files.length > 0) {

			fileInput.files = files;

			preview.innerText = "Archivo seleccionado: " + files[0].name;

			const reader = new FileReader();

			reader.onload = function (ev) {

				imagePreview.src = ev.target.result;
				imagePreview.style.display = "block";

				btnEliminar.style.display = "inline-block"; // ← MOSTRAR BOTÓN

			}

			reader.readAsDataURL(files[0]);

		}

	});

	/* ELIMINAR IMAGEN */

	btnEliminar.addEventListener("click", function () {

		fileInput.value = "";

		imagePreview.src = "";
		imagePreview.style.display = "none";

		preview.innerText = "";

		btnEliminar.style.display = "none";

	});

</script>


</body>
</html>


