Public Class Gestion_Manuales_p
	Inherits System.Web.UI.Page
	Dim obj As New Negocio.NRecetas
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Try
			'verificacion de que la Cookie exista
			CargarActivacionGeneral()


			If Not Page.IsPostBack Then
				If Session("user") Is Nothing Then
					Response.Redirect("~/login.aspx", True)
					Exit Sub
				End If
				lblUsuario.Text = Session("user").ToString()


				Listar_Manuales_recetas()

				'Registro Auditoria
				registro_acceso_pagina(Session("ACTIVACION_GENERAL").ToString(), Session("SistemaAcceso").ToString(), Session("user").ToString())

			End If
		Catch ex As Exception
			If (Session("user") Is Nothing) Then
				Response.Redirect("~/login.aspx")
			End If
		End Try

	End Sub
	Public Sub Listar_Manuales_recetas()
		Dim ds As New DataSet
		Dim id_usuario As Integer
		id_usuario = Integer.Parse(Session("id_usuario").ToString())

		ds = obj.Listar_Manual_Recetas(id_usuario)

		grvManual_Recetas.DataSource = ds.Tables(0)
		grvManual_Recetas.DataBind()

		ds.Dispose()

	End Sub

	Protected Sub grvManual_Recetas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grvManual_Recetas.RowCommand
		If e.CommandName = "Eliminar" Then
			Dim index As Integer = Convert.ToInt32(e.CommandArgument)
			' Obtener id_manual desde DataKeys
			Dim id_manual As Integer = Convert.ToInt32(grvManual_Recetas.DataKeys(index).Value)
			Dim desc_manual As String = grvManual_Recetas.DataKeys(index).Values("desc_manual").ToString()

			lblDescManualEliminar.Text = desc_manual
			lblidmanualEliminar.Text = id_manual

			Dim ds As New DataSet()
			ds = obj.grupos_manuales_eliminar(id_manual)
			grvListaGruposManuales.DataSource = ds.Tables(0)
			grvListaGruposManuales.DataBind()


			' Abrir modal con Bootstrap 5
			ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal",
			"var myModal = new bootstrap.Modal(document.getElementById('modalListaManuales')); myModal.show();",
			True)



		End If
	End Sub

	Protected Sub btnEliminarManual_Click(sender As Object, e As EventArgs) Handles btnEliminarManual.Click
		Dim id_manual As Integer
		Dim id_usuario As Integer
		Dim resultado As Boolean

		id_usuario = Integer.Parse(Session("id_usuario").ToString())
		id_manual = Integer.Parse(lblidmanualEliminar.Text.ToString())

		resultado = obj.Eliminar_manual(id_manual, id_usuario)
		If resultado Then   ' Si todo salió bien, mostrar modal
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
			Listar_Manuales_recetas()
		End If


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
		Response.Redirect("Gestion_manuales_p.aspx")
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
		obj.Registro_SesionSistema(TokenGlobal, user, sistema, "Gestion_Manuales_p.aspx")
	End Sub

End Class