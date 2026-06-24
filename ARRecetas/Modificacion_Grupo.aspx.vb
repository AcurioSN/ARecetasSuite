Public Class Modificacion_Grupo
	Inherits System.Web.UI.Page
    Dim obj As New Negocio.NRecetas
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'verificacion de que la Cookie exista
        CargarActivacionGeneral()

            If Not IsPostBack Then
                If Session("user") Is Nothing Then
                    Response.Redirect("~/login.aspx", True)
                    Exit Sub
                End If
                If Request.QueryString("id") IsNot Nothing Then
                    Dim idGrupo As String = Request.QueryString("id")

                    Session("id_grupo") = idGrupo
                CargarManuales(idGrupo)

                'Registro Auditoria
                registro_acceso_pagina(Session("ACTIVACION_GENERAL").ToString(), Session("SistemaAcceso").ToString(), Session("user").ToString())

            End If
            End If


    End Sub
    Public Sub CargarManuales(ByVal idGrupo As Integer)
        Dim ds As New DataSet
        ds = obj.Listar_Manual_Grupo(idGrupo)
        grvListaManuales.DataSource = ds.Tables(0)
        grvListaManuales.DataBind()

        txtDescripcionDocumento.Text = ds.Tables(1).Rows(0)(0).ToString()

        'guardo los manuales originales del grupo
        Dim dt As DataTable = ds.Tables(0)
        Session("dtManualesGrupo") = dt

    End Sub

    Protected Sub grvmanualSelect_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grvManualSelect.RowCommand

        If e.CommandName = "Seleccionar" Then
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id_manual As String = grvManualSelect.DataKeys(rowIndex)("id_manual").ToString()
            Dim cod_manual As String = grvManualSelect.DataKeys(rowIndex)("cod_manual").ToString()
            Dim desc_manual As String = grvManualSelect.DataKeys(rowIndex)("desc_manual").ToString()
            Dim cod_centro_sap_manual As String = grvManualSelect.DataKeys(rowIndex)("cod_centro_sap_manual").ToString()
            Dim cod_sap_rb_manual As String = grvManualSelect.DataKeys(rowIndex)("cod_sap_rb_manual").ToString()
            Dim desc_sap_rb_manual As String = grvManualSelect.DataKeys(rowIndex)("desc_sap_rb_manual").ToString()


            Dim dt As DataTable = CType(Session("dtManualesGrupo"), DataTable)

            Dim filas() As DataRow = dt.Select("id_manual = " & id_manual)

            If filas.Length = 0 Then

                Dim nuevaFila As DataRow = dt.NewRow()

                nuevaFila("id_grupo") = CInt(Session("id_grupo"))
                nuevaFila("id_manual") = id_manual
                nuevaFila("cod_manual") = cod_manual
                nuevaFila("desc_manual") = desc_manual
                nuevaFila("cod_centro_sap_manual") = cod_centro_sap_manual
                nuevaFila("cod_sap_rb_manual") = cod_sap_rb_manual
                nuevaFila("desc_sap_rb_manual") = desc_sap_rb_manual

                dt.Rows.Add(nuevaFila)

            End If

            Session("dtManualesGrupo") = dt

            grvListaManuales.DataSource = dt
            grvListaManuales.DataBind()



            ScriptManager.RegisterStartupScript(
    Me,
    Me.GetType(),
    "modalClose",
    "
    var modalElement = document.getElementById('modalSeleccionManuales');

    if (modalElement) {

        // Forzar ocultar visualmente
        modalElement.classList.remove('show');
        modalElement.style.display = 'none';
        modalElement.setAttribute('aria-hidden', 'true');

        // Eliminar backdrop
        document.querySelectorAll('.modal-backdrop').forEach(function(el) {
            el.remove();
        });

        // Restaurar body
        document.body.classList.remove('modal-open');
        document.body.style.removeProperty('padding-right');
    }
    ",
    True)

        End If



    End Sub

    Protected Sub btnBuscarManual_Click(sender As Object, e As EventArgs) Handles btnBuscarManual.Click
        Dim cod_manual As String
        Dim desc_manual As String
        Dim ds As New DataSet()
        cod_manual = txtcod_manual.Text
        desc_manual = txtdesc_manual.Text

        ds = obj.Busqueda_Manuales(cod_manual, desc_manual)

        grvManualSelect.DataSource = ds.Tables(0)
        grvManualSelect.DataBind()

        ' Vuelves a mostrar el modal
        'ScriptManager.RegisterStartupScript(
        '    Me,
        '    Me.GetType(),
        '    "modal",
        '    "var myModal = new bootstrap.Modal(document.getElementById('modalSeleccionManuales')); myModal.show();",
        '    True)

        ScriptManager.RegisterStartupScript(
    Me,
    Me.GetType(),
    "modal",
    "bootstrap.Modal.getOrCreateInstance(document.getElementById('modalSeleccionManuales')).show();",
    True)


    End Sub

    Protected Sub grvListaManualesx_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grvListaManualesx.RowCommand

    End Sub

    Private Sub grvListaManuales_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grvListaManuales.RowCommand
        If e.CommandName = "eliminar" Then

            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id_manual As String = grvListaManuales.DataKeys(rowIndex)("id_manual").ToString()

            Dim dt As DataTable = CType(Session("dtManualesGrupo"), DataTable)

            Dim filas() As DataRow = dt.Select("id_manual = " & id_manual)

            If filas.Length > 0 Then
                dt.Rows.Remove(filas(0))
            End If

            Session("dtManualesGrupo") = dt


            grvListaManuales.DataSource = dt
            grvListaManuales.DataBind()

        End If
    End Sub

    Protected Sub btnAgregarManual_Click(sender As Object, e As EventArgs) Handles btnAgregarManual.Click
        ScriptManager.RegisterStartupScript(
        Me,
        Me.GetType(),
        "modalOpen",
        "bootstrap.Modal.getOrCreateInstance(document.getElementById('modalSeleccionManuales')).show();",
        True)
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim descripcion_grupo As String
        Dim id_grupo As Integer
        Dim id_usuario As Integer

        descripcion_grupo = txtDescripcionDocumento.Text.ToString()
        id_grupo = Integer.Parse(Session("id_grupo").ToString())
        id_usuario = Integer.Parse(Session("id_usuario").ToString())

        'Modificacion de la descripcion del grupo / reinicio de manuales del grupo a 0
        obj.Modifica_Grupo(id_grupo, descripcion_grupo, id_usuario)

        'Manuales asociados al grupo
        Dim dt As DataTable = CType(Session("dtManualesGrupo"), DataTable)
        Dim idGrupo As Integer = CInt(Session("id_grupo"))

        For Each row As DataRow In dt.Rows

            Dim idManual As Integer = CInt(row("id_manual"))

            ' Aquí llamas tu SP de inserción
            obj.Modifica_Insertar_Manual_Grupo(idGrupo, idManual)

        Next


        CargarManuales(Session("id_grupo"))

        ' Si todo salió bien, mostrar modal
        ScriptManager.RegisterStartupScript(
        Me,
        Me.GetType(),
        "mostrarModal",
        "
					window.addEventListener('load', function () {
						var modal = new bootstrap.Modal(document.getElementById('InformativoModal'));
						modal.show();
					});
					",
                    True
                )


    End Sub
    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        CerrarSesion_ActivacionGeneral()

        Dim cogeCookie = Request.Cookies.Get("appNameAuth")
        If Not cogeCookie Is Nothing Then
            Request.Cookies.Remove("appNameAuth")
        End If

        FormsAuthentication.SignOut() 'ahora si cierras session!!!
        Session.Abandon()
        Session.Clear()
        Response.Redirect("Modificacion_Grupo.aspx")
    End Sub

    Public Function CargarActivacionGeneral() As Boolean
        '================ PROCESO COOKIE =================

        Dim token As String = String.Empty
        Dim ds As New DataSet
        If Session("ACTIVACION_GENERAL") IsNot Nothing Then
            token = Session("ACTIVACION_GENERAL").ToString()

            ds = obj.ExisteTokenActivo(token)
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Response.Redirect("~/SesionExpirada.aspx", False)
                Return False
            End If
        Else
            Response.Redirect("~/SesionExpirada.aspx")
            Return False
        End If


    End Function
    Public Sub CerrarSesion_ActivacionGeneral()
        '================ PROCESO COOKIE =================
        obj.CerrarSesionGlobal(Session("ACTIVACION_GENERAL").ToString())

    End Sub

    Public Sub registro_acceso_pagina(ByVal TokenGlobal As String, ByVal sistema As String, ByVal user As String)
        obj.Registro_SesionSistema(TokenGlobal, user, sistema, "Modificacion_Grupo.aspx")
    End Sub

End Class