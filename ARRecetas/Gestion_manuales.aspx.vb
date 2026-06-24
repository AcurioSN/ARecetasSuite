Public Class Gestion_manuales
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


				Listar_Grupos_recetas()
				Listar_CentroCostos()

				'Registro Auditoria
				registro_acceso_pagina(Session("ACTIVACION_GENERAL").ToString(), Session("SistemaAcceso").ToString(), Session("user").ToString())


			End If
		Catch ex As Exception
			If (Session("user") Is Nothing) Then
				Response.Redirect("~/login.aspx")
			End If
		End Try

	End Sub
	Public Sub Listar_CentroCostos()
		Dim ds As New DataSet()
		ds = obj.Listar_CentroCostos()
		cbocentro_costos.DataSource = ds.Tables(0)
		cbocentro_costos.DataTextField = "name1"
		cbocentro_costos.DataValueField = "werks"
		cbocentro_costos.DataBind()
	End Sub

	Public Sub Listar_Grupos_recetas()
		Dim ds As New DataSet
		Dim id_usuario As Integer
		id_usuario = Integer.Parse(Session("id_usuario").ToString())

		ds = obj.Listar_Grupos_Recetas(id_usuario)

		grvGrupo.DataSource = ds.Tables(0)
		grvGrupo.DataBind()

		ds.Dispose()

	End Sub

	Protected Sub grvGrupo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grvGrupo.RowCommand
		If e.CommandName = "Seleccionar" Then

			Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

			Dim idGrupo As String = grvGrupo.DataKeys(rowIndex)("id_grupo").ToString()
			Dim descGrupo As String = grvGrupo.DataKeys(rowIndex)("desc_grupo").ToString()
			lblidgrupo.Text = idGrupo.ToString()

			Dim valor As String = descGrupo.Replace("'", "\'")

			ScriptManager.RegisterStartupScript(
				Me,
				Me.GetType(),
				"setGrupo",
				"
            document.getElementById('" & hfIdGrupo.ClientID & "').value = '" & idGrupo & "';
            document.getElementById('" & txtgruposeleccionado.ClientID & "').value = '" & valor & "';

            var modalEl = document.getElementById('modalSeleccionGrupo');
            var modal = bootstrap.Modal.getOrCreateInstance(modalEl);
            modal.hide();

            document.body.classList.remove('modal-open');
            var backdrops = document.getElementsByClassName('modal-backdrop');
            while(backdrops.length > 0){
                backdrops[0].parentNode.removeChild(backdrops[0]);
            }
            ",
				True)

		End If
		'If e.CommandName = "Seleccionar" Then


		'	' Índice de la fila
		'	Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

		'	' Obtener valores desde DataKeys (FORMA CORRECTA)
		'	Dim idGrupo As String = grvGrupo.DataKeys(rowIndex)("id_grupo").ToString()
		'	Dim descGrupo As String = grvGrupo.DataKeys(rowIndex)("desc_grupo").ToString()
		'	Dim fechaGrupo As DateTime = Convert.ToDateTime(grvGrupo.DataKeys(rowIndex)("fec_reg_grupo"))

		'	' Pintar valores
		'	hfIdGrupo.Value = idGrupo
		'	txtgruposeleccionado.Text = descGrupo


		'	' Cerrar modal Bootstrap
		'	ScriptManager.RegisterStartupScript(Me, Me.GetType(),
		'	"CerrarModalGrupo",
		'	"$('#modalSeleccionGrupo').modal('hide');",
		'	True)


		'End If

	End Sub



	Protected Sub btnBuscarReceta_Click(sender As Object, e As EventArgs) Handles btnBuscarReceta.Click
		Buscar_productos()
	End Sub

	Public Sub Buscar_productos()
		If Session("user") Is Nothing Then
			Response.Redirect("~/login.aspx", True)
			Exit Sub
		End If

		Dim unidad As String
		'Dim zona As String
		Dim cod_producto As String
		Dim desc_producto As String
		Dim ds As New DataSet

		unidad = cbocentro_costos.SelectedValue.ToString()

		cod_producto = txtcod_buscar.Text
		desc_producto = txtdesc_buscar.Text

		'Nuevo filtro de busqueda Reservar
		ds = obj.Lista_Productos_centro_almacen_Reservar(unidad, cod_producto, desc_producto)

		If ds.Tables(0).Rows.Count <> 0 Then
			grvrecetasSelect.DataSource = ds.Tables(0)
			grvrecetasSelect.DataBind()
			ds.Dispose()

			' Vuelves a mostrar el modal
			'ScriptManager.RegisterStartupScript(
			'Me,
			'Me.GetType(),
			'"modal",
			'"var myModal = new bootstrap.Modal(document.getElementById('modalSeleccionReceta')); myModal.show();",
			'True)

			ScriptManager.RegisterStartupScript(
		   Me,
		   Me.GetType(),
		   "modal",
		   "bootstrap.Modal.getOrCreateInstance(document.getElementById('modalSeleccionReceta')).show();",
		   True)


		End If




	End Sub

	Protected Sub grvrecetasSelect_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grvrecetasSelect.RowCommand
		If e.CommandName = "Seleccionar" Then

			Dim index As Integer = Convert.ToInt32(e.CommandArgument)

			Dim material As String = grvrecetasSelect.DataKeys(index).Values("material").ToString()
			Dim descripcion As String = grvrecetasSelect.DataKeys(index).Values("descripcion_material").ToString()

			Dim valor As String = material & "-" & descripcion


			'Buscar los insumos de insumos del material seleccionado

			Dim ds As New DataSet()
			ds = obj.Lista_detalle_insumos_seleccionado(material, cbocentro_costos.SelectedValue.ToString())
			grvdetalle_receta_seleccionada.DataSource = ds.Tables(0)
			grvdetalle_receta_seleccionada.DataBind()
			ds.Dispose()


			'ScriptManager.RegisterStartupScript(
			'	Me,
			'	Me.GetType(),
			'	"setReceta",
			'	"
			'         document.getElementById('" & txtReceta.ClientID & "').value = '" & valor.Replace("'", "\'") & "';

			'         var modalEl = document.getElementById('modalSeleccionReceta');
			'         var modal = bootstrap.Modal.getOrCreateInstance(modalEl);
			'         modal.hide();

			'         document.body.classList.remove('modal-open');
			'         var backdrops = document.getElementsByClassName('modal-backdrop');
			'         while(backdrops.length > 0){
			'             backdrops[0].parentNode.removeChild(backdrops[0]);
			'         }
			'         ",
			'	True)

			ScriptManager.RegisterStartupScript(
				Me,
				Me.GetType(),
				"setReceta",
				"
			var txt = document.getElementById('" & txtReceta.ClientID & "');
			txt.value = '" & valor.Replace("'", "\'") & "';
			txt.setAttribute('value','" & valor.Replace("'", "\'") & "'); 

			var modalEl = document.getElementById('modalSeleccionReceta');
			var modal = bootstrap.Modal.getOrCreateInstance(modalEl);
			modal.hide();

			document.body.classList.remove('modal-open');
			var backdrops = document.getElementsByClassName('modal-backdrop');
			while(backdrops.length > 0){
				backdrops[0].parentNode.removeChild(backdrops[0]);
			}
			",
				True)



		End If

		'If e.CommandName = "Seleccionar" Then
		'	Dim index As Integer = Convert.ToInt32(e.CommandArgument)

		'	' 2️⃣ Obtener las DataKeys
		'	Dim material As String = grvrecetasSelect.DataKeys(index).Values("material").ToString()
		'	Dim descripcion As String = grvrecetasSelect.DataKeys(index).Values("descripcion_material").ToString()
		'	Dim unidad As String = grvrecetasSelect.DataKeys(index).Values("unidad_medida").ToString()

		'	txtReceta.Text = material + "-" + descripcion

		'	ScriptManager.RegisterStartupScript(
		'	Me,
		'	Me.GetType(),
		'	"cerrarModal",
		'	"
		'	var modalEl = document.getElementById('modalSeleccionReceta');
		'	var modal = bootstrap.Modal.getOrCreateInstance(modalEl);
		'	modal.hide();

		'	document.body.classList.remove('modal-open');
		'	var backdrops = document.getElementsByClassName('modal-backdrop');
		'	while(backdrops.length > 0){
		'		backdrops[0].parentNode.removeChild(backdrops[0]);
		'	}
		'	",
		'	True)


		'End If

	End Sub

	Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
		Try



			Dim id_grupo As Integer
			Dim centro_costos As String
			Dim preparacion As String

			'DescripcionDocumento = txtDescripcionDocumento.Text.ToString() 'Descripcion del documento

			Dim DescripcionDocumento As String = txtDescripcionDocumento.Text.Trim()

			If String.IsNullOrEmpty(DescripcionDocumento) Then

				txtDescripcionDocumento.CssClass = "form-control border border-danger"
				txtDescripcionDocumento.Focus()
				Exit Sub

			End If

			If lblidgrupo.Text = "" Then
				txtgruposeleccionado.CssClass = "form-control border border-danger"
				txtgruposeleccionado.Focus()
				Exit Sub

			Else
				id_grupo = Integer.Parse(lblidgrupo.Text.ToString()) 'Id Grupo seleccionado
			End If


			If cbocentro_costos.SelectedIndex = 0 Then
				cbocentro_costos.CssClass = "form-control border border-danger"
				cbocentro_costos.Focus()
				Exit Sub

			Else
				centro_costos = cbocentro_costos.SelectedValue.ToString() ' Centro de Costos

			End If


			Dim raw As String = Request.Form(txtReceta.UniqueID)
			Dim cod_sap As String = ""
			Dim desc_sap As String = ""

			If Not String.IsNullOrEmpty(raw) AndAlso raw.Contains("-") Then

				Dim partes() As String = raw.Split(New Char() {"-"c}, 2)
				cod_sap = partes(0).Trim()
				desc_sap = partes(1).Trim()

			Else
				txtReceta.CssClass = "form-control border border-danger"
				txtReceta.Focus()
				Exit Sub

			End If

			preparacion = txtObservaciones.Text

			Dim id_usuario As Integer
			id_usuario = Integer.Parse(Session("id_usuario").ToString())

			'Guardar datos del manual de la receta
			Dim id_manual As Integer
			id_manual = obj.Registra_manual_receta(DescripcionDocumento, centro_costos, cod_sap, id_usuario, id_grupo, preparacion, desc_sap)

			If id_manual <> 0 Then
				'Guardar el detalle del manual de la receta

				For Each row As GridViewRow In grvdetalle_receta_seleccionada.Rows

					If row.RowType = DataControlRowType.DataRow Then

						Dim id As String = row.Cells(0).Text
						Dim codigoPadre As String = row.Cells(1).Text
						Dim descripcionPadre As String = row.Cells(2).Text
						Dim codigo As String = row.Cells(3).Text
						Dim descripcion As String = row.Cells(4).Text
						Dim unidadMedida As String = row.Cells(5).Text
						Dim cantidad As String = row.Cells(6).Text
						Dim unidadMedida_cabecera As String = row.Cells(7).Text
						Dim cantidad_cabecera As String = row.Cells(8).Text

						obj.Registra_detalle_manual_receta(id_manual, codigoPadre, descripcionPadre, codigo, descripcion, unidadMedida, cantidad, unidadMedida_cabecera, cantidad_cabecera)

					End If

				Next

				'Registro de la imagen
				If fileImagen.HasFile Then

					Dim extension As String = System.IO.Path.GetExtension(fileImagen.FileName).ToLower()

					If extension <> ".jpg" And extension <> ".jpeg" Then
						Exit Sub
					End If

					Dim ruta As String = Server.MapPath("~/adjunto_manual/")
					Dim nombre As String = System.IO.Path.GetFileName(fileImagen.FileName)

					fileImagen.SaveAs(ruta & id_manual.ToString() + "_" + nombre)

					Dim nombre_archivo_principal As String
					nombre_archivo_principal = id_manual.ToString() + "_" + nombre

					obj.Registra_archivo_manual(id_manual, nombre_archivo_principal)

				End If

				limpiar()

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


			End If


		Catch ex As Exception

		End Try





	End Sub

	Public Sub limpiar()
		txtDescripcionDocumento.Text = ""
		lblidgrupo.Text = ""
		txtgruposeleccionado.Text = ""
		cbocentro_costos.SelectedIndex = 0
		txtReceta.Text = ""
		txtObservaciones.Text = ""
		grvdetalle_receta_seleccionada.DataSource = Nothing
		grvdetalle_receta_seleccionada.DataBind()


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
		Response.Redirect("Gestion_manuales.aspx")
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
		obj.Registro_SesionSistema(TokenGlobal, user, sistema, "Gestion_manuales.aspx")
	End Sub

End Class