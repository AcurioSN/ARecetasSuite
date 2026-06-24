Imports System.Drawing
Imports System.Drawing.Image
Imports System.Globalization
Imports System.IO
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports OfficeOpenXml
Imports OfficeOpenXml.Drawing
Imports OfficeOpenXml.Style


Public Class Gestion_grupos
	Inherits System.Web.UI.Page
	Dim obj As New Negocio.NRecetas
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

		'verificacion de que la Cookie exista
		CargarActivacionGeneral()

		If Not Page.IsPostBack Then

			If Session("user") IsNot Nothing Then 'El usuario tiene su sesion activa
				lblUsuario.Text = Session("user").ToString()
				Listar_Grupos_recetas()

				'Registro Auditoria
				registro_acceso_pagina(Session("ACTIVACION_GENERAL").ToString(), Session("SistemaAcceso").ToString(), Session("user").ToString())


			End If

		End If


	End Sub
	'Public Sub Listar_CentroCostos()
	'	Dim ds As New DataSet()
	'	ds = obj.Listar_CentroCostos()
	'	cbocentro_costos.DataSource = ds.Tables(0)
	'	cbocentro_costos.DataTextField = "name1"
	'	cbocentro_costos.DataValueField = "werks"
	'	cbocentro_costos.DataBind()
	'End Sub

	Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
		Dim cod_centro As String
		Dim desc_grupo As String
		Dim id_usuario As Integer

		'cod_centro = cbocentro_costos.SelectedValue.ToString()
		desc_grupo = txtDescripcionDocumento.Text.ToString()
		id_usuario = Integer.Parse(Session("id_usuario").ToString())

		Dim resultado As Boolean
		resultado = obj.Registra_grupo("", desc_grupo, id_usuario)
		Listar_Grupos_recetas()

		If resultado Then
			ScriptManager.RegisterStartupScript(
				Me,
				Me.GetType(),
				"AbrirInformativoModal",
				"var modal = new bootstrap.Modal(document.getElementById('InformativoModal')); modal.show();",
				True
				)
		End If




	End Sub

	Public Sub Listar_Grupos_recetas()
		Dim ds As New DataSet
		Dim id_usuario As Integer
		id_usuario = Integer.Parse(Session("id_usuario").ToString())

		ds = obj.Listar_Grupos_Recetas(id_usuario)

		grvgrupo_receta.DataSource = ds.Tables(0)
		grvgrupo_receta.DataBind()

		ds.Dispose()

	End Sub

	Private Sub grvgrupo_receta_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grvgrupo_receta.RowCommand
		If e.CommandName = "Seleccionar" Then

			Dim index As Integer = Convert.ToInt32(e.CommandArgument)

			' Obtener id_grupo desde DataKeys
			Dim idGrupo As Integer = Convert.ToInt32(grvgrupo_receta.DataKeys(index).Value)
			Dim ds_datos As New DataSet()
			ds_datos = obj.Lista_datos_excel(idGrupo)
			ExportarExcel(ds_datos)

		End If
		If e.CommandName = "VerManuales" Then
			Dim index As Integer = Convert.ToInt32(e.CommandArgument)

			' Obtener id_grupo desde DataKeys
			Dim idGrupo As Integer = Convert.ToInt32(grvgrupo_receta.DataKeys(index).Value)


			' (Opcional) cargar datos del modal aquí
			'CargarGrupos() ' si necesitas llenar grvGrupo
			Dim ds As New DataSet
			ds = obj.Listar_Manual_Grupo(idGrupo)
			grvListaManuales.DataSource = ds.Tables(0)
			grvListaManuales.DataBind()


			'grvListaManuales_libres.DataSource = ds.Tables(0)
			'grvListaManuales_libres.DataBind()
			'ds.Dispose()

			' Abrir modal con Bootstrap 5
			ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal",
			"var myModal = new bootstrap.Modal(document.getElementById('modalListaManuales')); myModal.show();",
			True)



		End If
		If e.CommandName = "Modificar" Then

			Dim index As Integer = Convert.ToInt32(e.CommandArgument)
			' Obtener id_grupo desde DataKeys
			Dim idGrupo As Integer = Convert.ToInt32(grvgrupo_receta.DataKeys(index).Value)
			Response.Redirect("Modificacion_Grupo.aspx?id=" & idGrupo)
			'' Abrir modal con Bootstrap 5
			'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal",
			'"var myModal = new bootstrap.Modal(document.getElementById('modalModificarGrupo')); myModal.show();",
			'True)
		End If

		If e.CommandName = "Eliminar" Then
			Dim index As Integer = Convert.ToInt32(e.CommandArgument)
			' Obtener id_manual desde DataKeys
			Dim id_grupo As Integer = Convert.ToInt32(grvgrupo_receta.DataKeys(index).Value)
			Dim desc_grupo As String = grvgrupo_receta.DataKeys(index).Values("desc_grupo").ToString()

			lblDescManualEliminar.Text = desc_grupo
			lblidGrupoEliminar.Text = id_grupo

			'Dim ds As New DataSet()
			'ds = obj.Lista_manuales_grupo_eliminar(id_grupo)
			'grvListaGruposManuales.DataSource = ds.Tables(0)
			'grvListaGruposManuales.DataBind()


			' Abrir modal con Bootstrap 5
			ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal",
			"var myModal = new bootstrap.Modal(document.getElementById('modalListaGrupos_eliminar')); myModal.show();",
			True)



		End If
	End Sub

	Public Sub creacion_Excel(ByVal idGrupo As Integer)

		'Using ms As New MemoryStream()

		'    Using document As SpreadsheetDocument = SpreadsheetDocument.Create(ms, SpreadsheetDocumentType.Workbook)

		'        Dim workbookPart As WorkbookPart = document.AddWorkbookPart()
		'        workbookPart.Workbook = New Workbook()

		'        Dim worksheetPart As WorksheetPart = workbookPart.AddNewPart(Of WorksheetPart)()
		'        Dim sheetData As New SheetData()
		'        worksheetPart.Worksheet = New Worksheet()

		'        ' =============================
		'        ' DEFINIR ANCHO DE COLUMNAS
		'        ' =============================
		'        Dim columns As New Columns()

		'        columns.Append(New Column() With {.Min = 2, .Max = 2, .Width = 35, .CustomWidth = True}) ' INGREDIENTES
		'        columns.Append(New Column() With {.Min = 3, .Max = 3, .Width = 15, .CustomWidth = True}) ' CANT.
		'        columns.Append(New Column() With {.Min = 4, .Max = 4, .Width = 18, .CustomWidth = True}) ' U.M.

		'        worksheetPart.Worksheet.Append(columns)
		'        worksheetPart.Worksheet.Append(sheetData)

		'        Dim sheets As Sheets = workbookPart.Workbook.AppendChild(New Sheets())

		'        sheets.Append(New Sheet() With {
		'            .Id = workbookPart.GetIdOfPart(worksheetPart),
		'            .SheetId = 1,
		'            .Name = "Receta"
		'        })

		'        ' =============================
		'        ' ESTILOS
		'        ' =============================
		'        Dim stylesPart As WorkbookStylesPart = workbookPart.AddNewPart(Of WorkbookStylesPart)()
		'        stylesPart.Stylesheet = New Stylesheet()

		'        Dim fonts As New Fonts()

		'        fonts.Append(New Font()) ' 0 default

		'        ' 1 - Título
		'        fonts.Append(New Font(
		'            New FontName() With {.Val = "Clarendon"},
		'            New FontSize() With {.Val = 22},
		'            New Bold()
		'        ))

		'        ' 2 - Subtítulo
		'        fonts.Append(New Font(
		'            New FontName() With {.Val = "Clarendon"},
		'            New FontSize() With {.Val = 10},
		'            New Italic()
		'        ))

		'        ' 3 - Tabla normal
		'        fonts.Append(New Font(
		'            New FontName() With {.Val = "Calibri"},
		'            New FontSize() With {.Val = 11}
		'        ))

		'        ' 4 - Cabecera blanca
		'        fonts.Append(New Font(
		'            New FontName() With {.Val = "Calibri"},
		'            New FontSize() With {.Val = 11},
		'            New Bold(),
		'            New Color() With {.Rgb = "FFFFFF"}
		'        ))

		'        Dim fills As New Fills()
		'        fills.Append(New Fill(New PatternFill() With {.PatternType = PatternValues.None}))
		'        fills.Append(New Fill(New PatternFill() With {.PatternType = PatternValues.Gray125}))

		'        ' 2 - Azul cabecera
		'        fills.Append(New Fill(
		'            New PatternFill(
		'                New ForegroundColor() With {.Rgb = "1F4E78"}
		'            ) With {.PatternType = PatternValues.Solid}
		'        ))

		'        Dim borders As New Borders()
		'        borders.Append(New Border()) ' 0 default

		'        ' 1 - borde exterior grueso
		'        borders.Append(New Border(
		'            New LeftBorder() With {.Style = BorderStyleValues.Thick},
		'            New RightBorder() With {.Style = BorderStyleValues.Thick},
		'            New TopBorder() With {.Style = BorderStyleValues.Thick},
		'            New BottomBorder() With {.Style = BorderStyleValues.Thick},
		'            New DiagonalBorder()
		'        ))

		'        Dim cellFormats As New CellFormats()

		'        cellFormats.Append(New CellFormat()) ' 0 default

		'        ' 1 - Título
		'        cellFormats.Append(New CellFormat() With {
		'            .FontId = 1,
		'            .ApplyFont = True,
		'            .Alignment = New Alignment() With {
		'                .Horizontal = HorizontalAlignmentValues.Center,
		'                .Vertical = VerticalAlignmentValues.Center
		'            },
		'            .ApplyAlignment = True
		'        })

		'        ' 2 - Subtítulo
		'        cellFormats.Append(New CellFormat() With {
		'            .FontId = 2,
		'            .ApplyFont = True,
		'            .Alignment = New Alignment() With {
		'                .Horizontal = HorizontalAlignmentValues.Center
		'            },
		'            .ApplyAlignment = True
		'        })

		'        ' 3 - Tabla normal
		'        cellFormats.Append(New CellFormat() With {
		'            .FontId = 3,
		'            .BorderId = 1,
		'            .ApplyBorder = True,
		'            .Alignment = New Alignment() With {
		'                .Horizontal = HorizontalAlignmentValues.Center,
		'                .Vertical = VerticalAlignmentValues.Center
		'            },
		'            .ApplyAlignment = True
		'        })

		'        ' 4 - Cabecera azul
		'        cellFormats.Append(New CellFormat() With {
		'            .FontId = 4,
		'            .FillId = 2,
		'            .BorderId = 1,
		'            .ApplyFill = True,
		'            .ApplyFont = True,
		'            .ApplyBorder = True,
		'            .Alignment = New Alignment() With {
		'                .Horizontal = HorizontalAlignmentValues.Center,
		'                .Vertical = VerticalAlignmentValues.Center
		'            },
		'            .ApplyAlignment = True
		'        })

		'        stylesPart.Stylesheet.Append(fonts)
		'        stylesPart.Stylesheet.Append(fills)
		'        stylesPart.Stylesheet.Append(borders)
		'        stylesPart.Stylesheet.Append(cellFormats)

		'        stylesPart.Stylesheet.Save()

		'        ' =============================
		'        ' TÍTULO Y SUBTÍTULO
		'        ' =============================

		'        Dim titleRow As New Row() With {.RowIndex = 5, .Height = 35, .CustomHeight = True}
		'        titleRow.Append(New Cell() With {
		'            .CellReference = "B5",
		'            .DataType = CellValues.String,
		'            .CellValue = New CellValue("RIÑONCITOS AL PISCO"),
		'            .StyleIndex = 1
		'        })
		'        sheetData.Append(titleRow)

		'        Dim subRow As New Row() With {.RowIndex = 6}
		'        subRow.Append(New Cell() With {
		'            .CellReference = "B6",
		'            .DataType = CellValues.String,
		'            .CellValue = New CellValue("Receta: El Bodegón Lima"),
		'            .StyleIndex = 2
		'        })
		'        sheetData.Append(subRow)

		'        ' =============================
		'        ' TABLA
		'        ' =============================

		'        Dim ingredientes = New List(Of Object()) From {
		'            New Object() {"INGREDIENTES", "CANT.", "U.M."},
		'            New Object() {"RIÑON LIMPIO", 240.0, "Gramos"},
		'            New Object() {"VINAGRE BLANCO", 10.0, "Mililitros"},
		'            New Object() {"AJO PELADO", 10.0, "Gramos"},
		'            New Object() {"ACEITE VEGETAL", 30.0, "Mililitros"},
		'            New Object() {"CEBOLLA BLANCA", 55.0, "Gramos"},
		'            New Object() {"CHAMPIÑONES ENTEROS", 70.0, "Gramos"},
		'            New Object() {"PISCO DE LA CASA", 30.0, "Mililitros"},
		'            New Object() {"AJI AMARILLO EN JULIANA", 20.0, "Gramos"},
		'            New Object() {"RB MEZCLA PARA SALTADO", 20.0, "Gramos"},
		'            New Object() {"RB SALSA DEMIGLACE", 40.0, "Gramos"},
		'            New Object() {"CREMA DE LECHE", 80.0, "Mililitros"},
		'            New Object() {"PEREJIL", 5.0, "Gramos"},
		'            New Object() {"RB PURE DE PAPAS", 220.0, "Gramos"}
		'        }

		'        Dim startRow As Integer = 8

		'        For i = 0 To ingredientes.Count - 1

		'            Dim rowIndex = startRow + i
		'            Dim row As New Row() With {.RowIndex = rowIndex}

		'            Dim styleUsed As UInt32Value = If(i = 0, 4UI, 3UI)

		'            row.Append(New Cell() With {
		'                .CellReference = "B" & rowIndex,
		'                .DataType = CellValues.String,
		'                .CellValue = New CellValue(ingredientes(i)(0).ToString()),
		'                .StyleIndex = styleUsed
		'            })

		'            row.Append(New Cell() With {
		'                .CellReference = "C" & rowIndex,
		'                .CellValue = New CellValue(ingredientes(i)(1).ToString()),
		'                .StyleIndex = styleUsed
		'            })

		'            row.Append(New Cell() With {
		'                .CellReference = "D" & rowIndex,
		'                .DataType = CellValues.String,
		'                .CellValue = New CellValue(ingredientes(i)(2).ToString()),
		'                .StyleIndex = styleUsed
		'            })

		'            sheetData.Append(row)

		'        Next

		'        ' =============================
		'        ' MERGE
		'        ' =============================
		'        Dim mergeCells As New MergeCells()
		'        mergeCells.Append(New MergeCell() With {.Reference = "B5:G5"})
		'        mergeCells.Append(New MergeCell() With {.Reference = "B6:G6"})

		'        worksheetPart.Worksheet.InsertAfter(mergeCells,
		'            worksheetPart.Worksheet.Elements(Of SheetData)().First())

		'        workbookPart.Workbook.Save()

		'    End Using

		'    Response.Clear()
		'    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
		'    Response.AddHeader("content-disposition", "attachment;filename=Receta_Rinon.xlsx")
		'    Response.BinaryWrite(ms.ToArray())
		'    Response.Flush()
		'    HttpContext.Current.ApplicationInstance.CompleteRequest()

		'End Using




	End Sub

	Public Sub ExportarExcel(ByVal ds_datos As DataSet)
		Dim opcion As Boolean = True
		Dim Unidad As String
		Dim fase As String
		Dim Datos As New DataTable

		Try
			Dim i1 As Integer = 0

			Using package = New ExcelPackage
				package.Workbook.Properties.Author = "Acurio Restaurantes"
				package.Workbook.Properties.Title = "Manual de Recetas"


				For i1 = 0 To ds_datos.Tables(0).Rows.Count - 1 'Creacion de cada hoja excel
					'Datos
					Dim codigo_padre_principal_receta As String = ds_datos.Tables(0).Rows(i1)(9).ToString()
					Dim desc_receta_titulo As String = ds_datos.Tables(0).Rows(i1)(10).ToString()
					Dim id_manual_receta As Integer = ds_datos.Tables(0).Rows(i1)(4).ToString() ' El id manual
					Dim desc_manual As String = ds_datos.Tables(0).Rows(i1)(6).ToString()
					Dim preparacion_receta As String = ds_datos.Tables(0).Rows(i1)(13).ToString()
					Dim codigo_manual_generado As String = ds_datos.Tables(0).Rows(i1)(8).ToString() 'Para jalar la imagen o foto
					Dim nombre_archivo_foto As String = ds_datos.Tables(0).Rows(i1)(14).ToString()  'Foto
					Dim desc_grupo_o_manual As String = ds_datos.Tables(0).Rows(i1)(1).ToString()
					Dim cod_centro_sap As String = ds_datos.Tables(0).Rows(i1)(7).ToString()
					'Traer el detalle del manual receta

					Dim dtOrigen As DataTable = ds_datos.Tables(1)
					Dim dv As New DataView(dtOrigen)
					dv.RowFilter = "id_manual = " & id_manual_receta

					'Dim dtFiltrado As DataTable = dv.ToTable()
					'Dim dsDetalleResultado As New DataSet()
					'dsDetalleResultado.Tables.Add(dtFiltrado) 'El detalle del manual de receta

					'Corregido
					' Aplicar filtro al DataView
					dv.RowFilter = "codigo_padre = '" & codigo_padre_principal_receta.ToString() & "'"

					' Convertir a DataTable ya filtrado
					Dim dtFiltrado As DataTable = dv.ToTable()

					' Pasar a DataSet
					Dim dsDetalleResultado As New DataSet()
					dsDetalleResultado.Tables.Add(dtFiltrado)


					'Creacion de hoja Excel
					Dim HojaExcel = package.Workbook.Worksheets.Add("Manual1")
					HojaExcel.Name = desc_receta_titulo + "_" + i1.ToString() ' "Manual de Recetas"

					' 🔥 CONFIGURACIÓN DE MÁRGENES
					With HojaExcel.PrinterSettings
						.TopMargin = 2.5 / 2.54
						.BottomMargin = 2.5 / 2.54
						.LeftMargin = 3 / 2.54
						.RightMargin = 1.8 / 2.54
						.HeaderMargin = 1 / 2.54

						' 🔥 RECOMENDADO PARA PDF
						.Orientation = eOrientation.Portrait
						.FitToPage = True
						.FitToWidth = 1
						.FitToHeight = False
					End With

					'Foto de la receta

					' Ruta desde web.config
					Dim rutaBase As String = ConfigurationManager.AppSettings("Adjunto_manual")
					'Dim rutaImagen As String = Path.Combine(rutaBase, codigo_manual_generado + ".jpg")
					Dim rutaImagen As String = Path.Combine(rutaBase, nombre_archivo_foto)


					'Imagen de Logo Acurio Restaurantes
					Dim rutaImagenLogo As String = Path.Combine("C:\inetpub\wwwroot\ARecetas\images", "logo_acurio_restaurantes.png")

					'Imagen de Logo de Marca
					Dim primeros2 As String = cod_centro_sap.Substring(0, 2)
					Dim imagen_marca As String = ""
					If primeros2 = "BC" Then
						imagen_marca = "barrachalaca_logo.png"
					End If
					If primeros2 = "PH" Then
						imagen_marca = "panchita_logo.png"
					End If
					If primeros2 = "LM" Then
						imagen_marca = "lamar_logo.png"
					End If
					If primeros2 = "TA" Then
						imagen_marca = "tanta_logo.png"
					End If
					If primeros2 = "EB" Then
						imagen_marca = "bodegon_logo.png"
					End If
					Dim rutaImagenLogo_marca As String = Path.Combine("C:\inetpub\wwwroot\ARecetas\images", imagen_marca)

					'If File.Exists(rutaImagen) Then

					'	Dim imagen As Image = Image.FromFile(rutaImagen)

					'	Dim picture = HojaExcel.Drawings.AddPicture("ImagenReceta", imagen)

					'	' Posicionar en F8 (fila 8, columna 6)
					'	picture.SetPosition(7, 0, 5, 0)
					'	' (EPPlus usa base 0 → fila 7 = fila 8 real, columna 5 = columna 6 real)

					'	' Calcular tamaño del rango F8:H21
					'	Dim ancho As Integer = 0
					'	For col As Integer = 6 To 8   ' F=6, G=7, H=8
					'		ancho += HojaExcel.Column(col).Width * 7
					'	Next

					'	Dim alto As Integer = 0
					'	For fil As Integer = 8 To 21
					'		alto += HojaExcel.Row(fil).Height
					'	Next

					'	picture.SetSize(CInt(ancho), CInt(alto))

					'End If

					'Comentado - arriba
					If File.Exists(rutaImagen) Then

						Dim anchoPx As Integer = CInt(6.3 * 37.8)
						Dim altoPx As Integer = CInt(5.4 * 37.8)

						Using imgOriginal As Image = Image.FromFile(rutaImagen)

							' 🔥 Crear imagen EXACTA (rompe proporción aquí)
							Using bmp As New Bitmap(anchoPx, altoPx)

								Using g As Graphics = Graphics.FromImage(bmp)
									g.DrawImage(imgOriginal, 0, 0, anchoPx, altoPx)
								End Using

								'Dim picture = HojaExcel.Drawings.AddPicture("ImagenReceta", bmp)
								Using ms As New MemoryStream()
									bmp.Save(ms, Imaging.ImageFormat.Png)
									ms.Position = 0

									'Dim picture = HojaExcel.Drawings.AddPicture("ImagenReceta", ms)

									Dim picture As ExcelPicture

									Using img As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
										picture = HojaExcel.Drawings.AddPicture("ImagenReceta", img)
									End Using

									' Posicionar en F8
									picture.SetPosition(7, 0, 5, 0)

									' (Opcional pero recomendable)
									picture.SetSize(anchoPx, altoPx)

								End Using



							End Using
						End Using

					End If


					'If File.Exists(rutaImagenLogo) Then

					'	Dim imagen As Image = Image.FromFile(rutaImagenLogo)

					'	Dim picture = HojaExcel.Drawings.AddPicture("LogoAcurio", imagen)

					'	' Posición en D1
					'	'picture.SetPosition(0, 2, 1, 2)
					'	picture.SetPosition(0, 2, 0, 2)

					'	' Más ancho que alto (formato horizontal)
					'	'picture.SetSize(90, 40)
					'	'picture.SetSize(120, 50)
					'	picture.SetSize(120, 40)


					'End If


					'If File.Exists(rutaImagenLogo_marca) Then

					'	Dim imagen As Image = Image.FromFile(rutaImagenLogo_marca)

					'	Dim picture = HojaExcel.Drawings.AddPicture("LogoMarca", imagen)

					'	' Posición en D1
					'	'picture.SetPosition(0, 2, 3, 2)
					'	picture.SetPosition(0, 2, 6, 2)

					'	' Más ancho que alto (formato horizontal)
					'	picture.SetSize(120, 50)

					'End If

					'Hoja 1 : EVALUACIONES

					Dim formatRango As ExcelRange
					' Título principal en B5:G5
					'formatRango = HojaExcel.Cells("B5:G5")

					'formatRango.Merge = True
					'formatRango.Value = desc_receta_titulo.ToString()


					'' Estilos
					'formatRango.Style.Font.Name = "Clarendon"
					'formatRango.Style.Font.Size = 22
					'formatRango.Style.Font.Bold = True
					'formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
					'formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
					'formatRango.Style.WrapText = True

					' Título principal en B5:G5

					formatRango = HojaExcel.Cells("B5:F5")
					formatRango.Merge = True
					HojaExcel.Row(5).Height = 29.5
					formatRango.Value = desc_manual.ToString()


					' Estilos
					formatRango.Style.Font.Name = "Clarendon"
					formatRango.Style.Font.Size = 22
					formatRango.Style.Font.Bold = False
					formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
					formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
					formatRango.Style.WrapText = True

					' 🔥 Borde exterior grueso
					With formatRango.Style.Border
						.Top.Style = ExcelBorderStyle.Thick
						.Bottom.Style = ExcelBorderStyle.Thick
						.Left.Style = ExcelBorderStyle.Thick
						.Right.Style = ExcelBorderStyle.Thick
					End With

					' Altura dinámica según longitud del texto
					If desc_receta_titulo.Length <= 40 Then
						HojaExcel.Row(5).Height = 29.5 '45
					ElseIf desc_receta_titulo.Length <= 80 Then
						HojaExcel.Row(5).Height = 35 '65
					Else
						HojaExcel.Row(5).Height = 40 '85
					End If


					' Título principal en B6:G6
					formatRango = HojaExcel.Cells("B6:F6")
					formatRango.Merge = True
					formatRango.Value = "Receta:" + desc_grupo_o_manual 'desc_manual

					' Estilos
					formatRango.Style.Font.Name = "Arial"
					formatRango.Style.Font.Size = 10
					formatRango.Style.Font.Italic = True
					formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
					formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
					formatRango.Style.WrapText = True



					formatRango = HojaExcel.Cells(8, 2, 8, 4)
					formatRango.Style.Font.Bold = True
					formatRango.Style.Font.Size = 7
					formatRango.Style.Fill.PatternType = ExcelFillStyle.Solid
					formatRango.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(130, 36, 112))
					formatRango.Style.Font.Color.SetColor(System.Drawing.Color.White)

					HojaExcel.Column(2).Width = 15.86

					formatRango = HojaExcel.Cells(8, 2)
					formatRango.Style.WrapText = True
					formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
					formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
					formatRango.Style.Border.Top.Style = 4
					formatRango.Style.Border.Left.Style = 4
					formatRango.Style.Border.Right.Style = 4
					formatRango.Style.Border.Bottom.Style = 4
					formatRango.Style.Font.Name = "Arial"
					formatRango.Style.Font.Size = 10
					formatRango.Value = "INGREDIENTES"

					With formatRango.Style.Border
						.Top.Style = ExcelBorderStyle.Thin
						.Bottom.Style = ExcelBorderStyle.Thin
						.Left.Style = ExcelBorderStyle.Thin
						.Right.Style = ExcelBorderStyle.None
					End With

					' CANT.
					formatRango = HojaExcel.Cells(8, 3)
					formatRango.Style.WrapText = True
					formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
					formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
					formatRango.Style.Border.Top.Style = 4
					formatRango.Style.Border.Left.Style = 4
					formatRango.Style.Border.Right.Style = 4
					formatRango.Style.Border.Bottom.Style = 4
					formatRango.Style.Font.Name = "Arial"
					formatRango.Style.Font.Size = 10
					formatRango.Value = "CANT."

					With formatRango.Style.Border
						.Top.Style = ExcelBorderStyle.Thin
						.Bottom.Style = ExcelBorderStyle.Thin
						.Left.Style = ExcelBorderStyle.None
						.Right.Style = ExcelBorderStyle.None
					End With

					' U.M.
					formatRango = HojaExcel.Cells(8, 4)
					formatRango.Style.WrapText = True
					formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
					formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
					formatRango.Style.Border.Top.Style = 4
					formatRango.Style.Border.Left.Style = 4
					formatRango.Style.Border.Right.Style = 4
					formatRango.Style.Border.Bottom.Style = 4
					formatRango.Style.Font.Name = "Arial"
					formatRango.Style.Font.Size = 10
					formatRango.Value = "U.M."

					With formatRango.Style.Border
						.Top.Style = ExcelBorderStyle.Thin
						.Bottom.Style = ExcelBorderStyle.Thin
						.Left.Style = ExcelBorderStyle.None
						.Right.Style = ExcelBorderStyle.Thin
					End With

					Dim filaInicio As Integer = 9
					Dim filaFin As Integer

					If dsDetalleResultado IsNot Nothing AndAlso dsDetalleResultado.Tables.Count > 0 AndAlso dsDetalleResultado.Tables(0).Rows.Count > 0 Then

						'Dim dt As DataTable = dsDetalleResultado.Tables(0)

						'Dim totalFilas As Integer = System.Math.Max(dt.Rows.Count, 15)

						'For i As Integer = 0 To totalFilas - 1

						'	' Si hay data, la usamos
						'	If i < dt.Rows.Count Then
						'		HojaExcel.Cells(filaInicio + i, 2).Value = dt.Rows(i)("descripcion_item")
						'		HojaExcel.Cells(filaInicio + i, 3).Value = dt.Rows(i)("cantidad")
						'		HojaExcel.Cells(filaInicio + i, 4).Value = dt.Rows(i)("unidad_medida")

						'	Else
						'		' Si no hay data, dejamos en blanco
						'		HojaExcel.Cells(filaInicio + i, 2).Value = ""
						'		HojaExcel.Cells(filaInicio + i, 3).Value = ""
						'		HojaExcel.Cells(filaInicio + i, 4).Value = ""
						'	End If

						'	' Quitar bordes
						'	Dim rango = HojaExcel.Cells(filaInicio + i, 2, filaInicio + i, 4)

						'	rango.Style.Border.Top.Style = ExcelBorderStyle.None
						'	rango.Style.Border.Left.Style = ExcelBorderStyle.None
						'	rango.Style.Border.Right.Style = ExcelBorderStyle.None
						'	rango.Style.Border.Bottom.Style = ExcelBorderStyle.None

						'Next

						'filaFin = filaInicio + totalFilas - 1


						Dim dt As DataTable = dsDetalleResultado.Tables(0)

						Dim totalFilas As Integer = System.Math.Max(dt.Rows.Count, 15)

						For i As Integer = 0 To totalFilas - 1

							' Si hay data, la usamos
							If i < dt.Rows.Count Then
								HojaExcel.Cells(filaInicio + i, 2).Value = dt.Rows(i)("descripcion_item")
								HojaExcel.Cells(filaInicio + i, 3).Value = dt.Rows(i)("cantidad")
								HojaExcel.Cells(filaInicio + i, 4).Value = dt.Rows(i)("unidad_medida")
							Else
								' Si no hay data, dejamos en blanco
								HojaExcel.Cells(filaInicio + i, 2).Value = ""
								HojaExcel.Cells(filaInicio + i, 3).Value = ""
								HojaExcel.Cells(filaInicio + i, 4).Value = ""
							End If

							' Quitar bordes
							Dim rango = HojaExcel.Cells(filaInicio + i, 2, filaInicio + i, 4)

							rango.Style.Border.Top.Style = ExcelBorderStyle.None
							rango.Style.Border.Left.Style = ExcelBorderStyle.None
							rango.Style.Border.Right.Style = ExcelBorderStyle.None
							rango.Style.Border.Bottom.Style = ExcelBorderStyle.None

						Next

						filaFin = filaInicio + totalFilas - 1

						' 🔥 FORMATO (lo que pediste)

						' DESCRIPCIÓN → Arial 10, izquierda
						With HojaExcel.Cells(filaInicio, 2, filaFin, 2).Style
							.Font.Name = "Arial"
							.Font.Size = 10
							.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left
						End With

						' CANTIDAD → Arial 10, centrado
						With HojaExcel.Cells(filaInicio, 3, filaFin, 3).Style
							.Font.Name = "Arial"
							.Font.Size = 10
							.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center
						End With

						' UNIDAD_MEDIDA → Arial 10, centrado
						With HojaExcel.Cells(filaInicio, 4, filaFin, 4).Style
							.Font.Name = "Arial"
							.Font.Size = 10
							.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center
						End With



					End If


					' Rango completo
					Dim rangoTabla = HojaExcel.Cells(8, 2, filaFin, 4)

					' Borde superior (fila 8)
					HojaExcel.Cells(8, 2, 8, 4).Style.Border.Top.Style = ExcelBorderStyle.Thin

					' Borde inferior (última fila)
					HojaExcel.Cells(filaFin, 2, filaFin, 4).Style.Border.Bottom.Style = ExcelBorderStyle.Thin

					' Borde izquierdo (columna 2)
					HojaExcel.Cells(8, 2, filaFin, 2).Style.Border.Left.Style = ExcelBorderStyle.Thin

					' Borde derecho (columna 4)
					HojaExcel.Cells(8, 4, filaFin, 4).Style.Border.Right.Style = ExcelBorderStyle.Thin


					'-- ' PREPARACIÓN DEL PLATO
					Dim filaPreparacion As Integer = filaFin + 2

					Dim rango_dinamico1 As String = "B" & filaPreparacion.ToString() & ":F" & filaPreparacion.ToString()
					formatRango = HojaExcel.Cells(rango_dinamico1)
					formatRango.Merge = True
					formatRango.Value = "PREPARACIÓN DEL PLATO"
					formatRango.Style.Font.Name = "Arial"
					formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
					formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
					formatRango.Style.WrapText = True
					formatRango.Style.Font.Bold = True
					formatRango.Style.Font.Size = 11
					formatRango.Style.Fill.PatternType = ExcelFillStyle.Solid
					formatRango.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(130, 36, 112))
					formatRango.Style.Font.Color.SetColor(System.Drawing.Color.White)

					'-- Preparacion del plato Detalle --
					Dim fila As Integer = filaPreparacion + 1
					Dim rango_dinamico2 As String = "B" & fila.ToString() & ":F" & fila.ToString()

					formatRango = HojaExcel.Cells(rango_dinamico2)

					formatRango.Merge = True
					'formatRango.Value = preparacion_receta.ToString()

					Dim texto As String = preparacion_receta.ToString()
					texto = texto.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf)
					formatRango.Value = texto
					formatRango.Style.WrapText = True


					' ===== FUENTE =====
					formatRango.Style.Font.Name = "Arial"
					formatRango.Style.Font.Size = 10
					formatRango.Style.Font.Bold = False
					formatRango.Style.Font.Color.SetColor(System.Drawing.Color.Black)

					' ===== ALINEACIÓN =====
					formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
					formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Top
					formatRango.Style.WrapText = True
					formatRango.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center

					' ===== FONDO =====
					formatRango.Style.Fill.PatternType = ExcelFillStyle.Solid
					formatRango.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White)

					' ===== BORDE GENERAL =====
					With formatRango.Style.Border
						.Top.Style = ExcelBorderStyle.Thin
						.Bottom.Style = ExcelBorderStyle.Thin
						.Left.Style = ExcelBorderStyle.Thin
						.Right.Style = ExcelBorderStyle.Thin
					End With

					' ===== AJUSTE MANUAL DE ALTURA =====
					Dim cantidadCaracteres As Integer = preparacion_receta.Length
					Dim alturaEstimada As Double = (cantidadCaracteres / 4)   ' Ajusta divisor si quieres más compacto

					If alturaEstimada < 30 Then
						alturaEstimada = 30
					End If

					HojaExcel.Row(fila).Height = alturaEstimada


					'Llenar el detalle de las recetas - tablas - no considerar los registros padres iniciales

					'Dim con As Integer = 0

					''Detalle de las RB de las RB.
					Dim dt_detalle As DataTable = ds_datos.Tables(2)

					Dim dtOrigen1 As DataTable = ds_datos.Tables(2)
					Dim dv1 As New DataView(dtOrigen1)
					dv1.RowFilter = "codigo_padre <> " & codigo_padre_principal_receta & " and id_manual = " & id_manual_receta

					Dim dtFiltrado1 As DataTable = dv1.ToTable()
					Dim dsDetalleResultado1 As New DataSet()
					dsDetalleResultado1.Tables.Add(dtFiltrado1)

					Dim filaFinx As Integer = 0

					For j As Integer = 0 To dsDetalleResultado1.Tables(0).Rows.Count - 1
						Dim titulo_receta_detalle As String
						Dim codigo_padre_detalle As String
						Dim unidad_medida_cabecera As String
						Dim cantidad_cabecera As String
						Dim UM As String
						Dim cantidad As String


						titulo_receta_detalle = dsDetalleResultado1.Tables(0).Rows(j)(2).ToString()
						codigo_padre_detalle = dsDetalleResultado1.Tables(0).Rows(j)(1).ToString()
						unidad_medida_cabecera = dsDetalleResultado1.Tables(0).Rows(j)(3).ToString()
						cantidad_cabecera = dsDetalleResultado1.Tables(0).Rows(j)(4).ToString()
						'UM = dsDetalleResultado1.Tables(0).Rows(j)(5).ToString()
						'cantidad = dsDetalleResultado1.Tables(0).Rows(j)(6).ToString()
						Dim filaRB As Integer
						If filaFinx = 0 Then
							filaRB = filaFin + 5
						Else
							filaRB = filaFinx + 1
						End If

						'cabeceras de la tabla
						Dim rango_dinamico3 As String = "B" & filaRB.ToString() & ":D" & filaRB.ToString()

						formatRango = HojaExcel.Cells(rango_dinamico3)
						formatRango.Merge = True
						formatRango.Value = titulo_receta_detalle
						formatRango.Style.Font.Name = "Arial"
						formatRango.Style.Font.Size = 11
						formatRango.Style.Font.Bold = True
						formatRango.Style.Font.Color.SetColor(System.Drawing.Color.Black)
						formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
						formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
						formatRango.Style.WrapText = True
						formatRango.Style.Fill.PatternType = ExcelFillStyle.Solid
						formatRango.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White)
						With formatRango.Style.Border
							.Top.Style = ExcelBorderStyle.Thin
							.Bottom.Style = ExcelBorderStyle.Thin
							.Left.Style = ExcelBorderStyle.Thin
							.Right.Style = ExcelBorderStyle.Thin
						End With

						Dim rango_dinamico4 As String = "E" & filaRB.ToString() '& ":F" & filaRB.ToString()

						formatRango = HojaExcel.Cells(rango_dinamico4)
						formatRango.Merge = True
						formatRango.Value = "RENDIMIENTO"
						formatRango.Style.Font.Name = "Arial"
						formatRango.Style.Font.Size = 11
						formatRango.Style.Font.Bold = True
						'formatRango.Style.Font.Color.SetColor(System.Drawing.Color.Black)
						'formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
						'formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
						'formatRango.Style.WrapText = True
						'formatRango.Style.Fill.PatternType = ExcelFillStyle.Solid
						'formatRango.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White)

						formatRango.Style.Font.Color.SetColor(System.Drawing.Color.White)
						formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
						formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
						formatRango.Style.WrapText = True
						formatRango.Style.Fill.PatternType = ExcelFillStyle.Solid
						formatRango.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(166, 96, 150))

						'With formatRango.Style.Border
						'	.Top.Style = ExcelBorderStyle.Thin
						'	.Bottom.Style = ExcelBorderStyle.Thin
						'	.Left.Style = ExcelBorderStyle.Thin
						'	.Right.Style = ExcelBorderStyle.Thin
						'End With
						With formatRango.Style.Border
							.Top.Style = ExcelBorderStyle.Thin
							.Bottom.Style = ExcelBorderStyle.None
							.Left.Style = ExcelBorderStyle.None
							.Right.Style = ExcelBorderStyle.None
						End With

						'Dim rango_dinamico5 As String = "G" & filaRB.ToString() & ":G" & filaRB.ToString()
						Dim rango_dinamico5 As String = "F" & filaRB.ToString() & ":F" & filaRB.ToString()

						formatRango = HojaExcel.Cells(rango_dinamico5)
						formatRango.Merge = True
						formatRango.Value = cantidad_cabecera + " " + unidad_medida_cabecera '"0 UM" 'cantidad.ToString() + " " + UM.ToString()
						formatRango.Style.Font.Name = "Arial"
						formatRango.Style.Font.Size = 11
						formatRango.Style.Font.Bold = True
						'formatRango.Style.Font.Color.SetColor(System.Drawing.Color.Black)
						'formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
						'formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
						'formatRango.Style.WrapText = True
						'formatRango.Style.Fill.PatternType = ExcelFillStyle.Solid
						'formatRango.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White)


						formatRango.Style.Font.Color.SetColor(System.Drawing.Color.White)
						formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
						formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
						formatRango.Style.WrapText = True
						formatRango.Style.Fill.PatternType = ExcelFillStyle.Solid
						formatRango.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(166, 96, 150))



						With formatRango.Style.Border
							.Top.Style = ExcelBorderStyle.Thin
							.Bottom.Style = ExcelBorderStyle.Thin
							.Left.Style = ExcelBorderStyle.None
							.Right.Style = ExcelBorderStyle.Thin
						End With

						'Creando cabeceras de las tablas del detalle de las RB

						Dim rango_dinamico6 As String = "B" & (filaRB + 1).ToString() & ":B" & (filaRB + 1).ToString()
						formatRango = HojaExcel.Cells(rango_dinamico6)
						formatRango.Style.WrapText = True
						formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
						formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
						formatRango.Style.Border.Top.Style = 4
						formatRango.Style.Border.Left.Style = 4
						formatRango.Style.Border.Right.Style = 4
						formatRango.Style.Border.Bottom.Style = 4
						formatRango.Style.Font.Bold = True
						formatRango.Style.Font.Size = 11
						formatRango.Style.Font.Name = "Arial"
						formatRango.Style.Fill.PatternType = ExcelFillStyle.Solid
						formatRango.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(130, 36, 112))
						formatRango.Style.Font.Color.SetColor(System.Drawing.Color.White)
						formatRango.Value = "INGREDIENTES"

						With formatRango.Style.Border
							.Top.Style = ExcelBorderStyle.Thin
							.Bottom.Style = ExcelBorderStyle.Thin
							.Left.Style = ExcelBorderStyle.Thin
							.Right.Style = ExcelBorderStyle.None
						End With

						' CANT.
						Dim rango_dinamico7 As String = "C" & (filaRB + 1).ToString() & ":C" & (filaRB + 1).ToString()
						formatRango = HojaExcel.Cells(rango_dinamico7)
						formatRango.Style.WrapText = True
						formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
						formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
						formatRango.Style.Border.Top.Style = 4
						formatRango.Style.Border.Left.Style = 4
						formatRango.Style.Border.Right.Style = 4
						formatRango.Style.Border.Bottom.Style = 4
						formatRango.Style.Font.Bold = True
						formatRango.Style.Font.Size = 11
						formatRango.Style.Font.Name = "Arial"
						formatRango.Style.Fill.PatternType = ExcelFillStyle.Solid
						formatRango.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(130, 36, 112))
						formatRango.Style.Font.Color.SetColor(System.Drawing.Color.White)
						formatRango.Value = "CANT."

						With formatRango.Style.Border
							.Top.Style = ExcelBorderStyle.Thin
							.Bottom.Style = ExcelBorderStyle.Thin
							.Left.Style = ExcelBorderStyle.None
							.Right.Style = ExcelBorderStyle.None
						End With

						' U.M.
						Dim rango_dinamico8 As String = "D" & (filaRB + 1).ToString() & ":D" & (filaRB + 1).ToString()
						formatRango = HojaExcel.Cells(rango_dinamico8)
						formatRango.Style.WrapText = True
						formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
						formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
						formatRango.Style.Border.Top.Style = 4
						formatRango.Style.Border.Left.Style = 4
						formatRango.Style.Border.Right.Style = 4
						formatRango.Style.Border.Bottom.Style = 4
						formatRango.Style.Font.Bold = True
						formatRango.Style.Font.Size = 11
						formatRango.Style.Font.Name = "Arial"
						formatRango.Style.Fill.PatternType = ExcelFillStyle.Solid
						formatRango.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(130, 36, 112))
						formatRango.Style.Font.Color.SetColor(System.Drawing.Color.White)
						formatRango.Value = "U.M."

						With formatRango.Style.Border
							.Top.Style = ExcelBorderStyle.Thin
							.Bottom.Style = ExcelBorderStyle.Thin
							.Left.Style = ExcelBorderStyle.None
							.Right.Style = ExcelBorderStyle.None
						End With

						' PREPARACIÓN
						Dim rango_dinamico9 As String = "E" & (filaRB + 1).ToString() & ":F" & (filaRB + 1).ToString()
						formatRango = HojaExcel.Cells(rango_dinamico9)
						formatRango.Merge = True
						formatRango.Style.WrapText = True
						formatRango.Style.VerticalAlignment = ExcelVerticalAlignment.Center
						formatRango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
						formatRango.Style.Border.Top.Style = 4
						formatRango.Style.Border.Left.Style = 4
						formatRango.Style.Border.Right.Style = 4
						formatRango.Style.Border.Bottom.Style = 4
						formatRango.Style.Font.Bold = True
						formatRango.Style.Font.Size = 11
						formatRango.Style.Font.Name = "Arial"
						formatRango.Style.Fill.PatternType = ExcelFillStyle.Solid
						formatRango.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(130, 36, 112))
						formatRango.Style.Font.Color.SetColor(System.Drawing.Color.White)
						formatRango.Value = "PREPARACIÓN"

						With formatRango.Style.Border
							.Top.Style = ExcelBorderStyle.Thin
							.Bottom.Style = ExcelBorderStyle.Thin
							.Left.Style = ExcelBorderStyle.None
							.Right.Style = ExcelBorderStyle.Thin
						End With

						'El detalle del detalle
						'traer los insumos de la RB detalle
						Dim dt_detalle_x As DataTable = ds_datos.Tables(1)

						Dim dtOrigenx As DataTable = ds_datos.Tables(1)
						Dim dv1x As New DataView(dtOrigenx)
						dv1x.RowFilter = "codigo_padre = " & codigo_padre_detalle & " and id_manual = " & id_manual_receta

						Dim dtFiltradox As DataTable = dv1x.ToTable()
						Dim dsDetalleResultado1x As New DataSet()
						dsDetalleResultado1x.Tables.Add(dtFiltradox)

						'Imprimimos el listado del detalle del detalle
						Dim filaIniciox As Integer = filaRB + 2


						If dsDetalleResultado1x IsNot Nothing AndAlso dsDetalleResultado1x.Tables.Count > 0 Then

							'Dim dt As DataTable = dsDetalleResultado1x.Tables(0)

							'For i As Integer = 0 To dt.Rows.Count - 1

							'	HojaExcel.Cells(filaIniciox + i, 2).Value = dt.Rows(i)("descripcion_item")
							'	HojaExcel.Cells(filaIniciox + i, 3).Value = dt.Rows(i)("cantidad")
							'	HojaExcel.Cells(filaIniciox + i, 4).Value = dt.Rows(i)("unidad_medida")

							'	' Quitar bordes
							'	Dim rango = HojaExcel.Cells(filaIniciox + i, 2, filaIniciox + i, 4)

							'	rango.Style.Border.Top.Style = ExcelBorderStyle.None
							'	rango.Style.Border.Left.Style = ExcelBorderStyle.None
							'	rango.Style.Border.Right.Style = ExcelBorderStyle.None
							'	rango.Style.Border.Bottom.Style = ExcelBorderStyle.None

							'Next
							'filaFinx = filaIniciox + dt.Rows.Count - 1

							Dim dt As DataTable = dsDetalleResultado1x.Tables(0)

							For i As Integer = 0 To dt.Rows.Count - 1

								HojaExcel.Cells(filaIniciox + i, 2).Value = dt.Rows(i)("descripcion_item")
								HojaExcel.Cells(filaIniciox + i, 3).Value = dt.Rows(i)("cantidad")
								HojaExcel.Cells(filaIniciox + i, 4).Value = dt.Rows(i)("unidad_medida")

								' Quitar bordes
								Dim rango = HojaExcel.Cells(filaIniciox + i, 2, filaIniciox + i, 4)

								rango.Style.Border.Top.Style = ExcelBorderStyle.None
								rango.Style.Border.Left.Style = ExcelBorderStyle.None
								rango.Style.Border.Right.Style = ExcelBorderStyle.None
								rango.Style.Border.Bottom.Style = ExcelBorderStyle.None

							Next

							filaFinx = filaIniciox + dt.Rows.Count - 1

							' 🔥 FORMATO

							' DESCRIPCIÓN → Arial 11, izquierda
							With HojaExcel.Cells(filaIniciox, 2, filaFinx, 2).Style
								.Font.Name = "Arial"
								.Font.Size = 11
								.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left
								.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
							End With

							' CANTIDAD → Arial 11, centrado
							With HojaExcel.Cells(filaIniciox, 3, filaFinx, 3).Style
								.Font.Name = "Arial"
								.Font.Size = 11
								.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center
								.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
							End With

							' UNIDAD_MEDIDA → Arial 11, centrado
							With HojaExcel.Cells(filaIniciox, 4, filaFinx, 4).Style
								.Font.Name = "Arial"
								.Font.Size = 11
								.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center
								.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center
							End With




						End If

					Next





					HojaExcel.Column(1).Width = 3.09
					HojaExcel.Column(2).Width = 32.36
					HojaExcel.Column(3).Width = 10.36
					HojaExcel.Column(4).Width = 10.82
					HojaExcel.Column(5).Width = 16.82
					HojaExcel.Column(6).Width = 10.27

					formatRango.Worksheet.View.ShowGridLines = False

				Next



				Response.BinaryWrite(package.GetAsByteArray())
				Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
				Response.AddHeader("Content-Disposition", "attachment; filename=ListaManualRecetas.xlsx")
				'Response.AddHeader("Content-Disposition", "attachment; filename=LibroReclamaciones_RGeneral.csv")
			End Using
		Catch ex As Exception
			mensajeError(ex.Message & " - ExportarExcel")
		Finally
			If opcion Then
				Response.End()
				Response.Flush()
			End If
		End Try
	End Sub
	Public Sub mensajeError(ByVal msj As String)
		ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertIns", "alert('" & msj & "');", True)
	End Sub

	Private Sub grvListaManuales_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grvListaManuales.RowCommand
		If e.CommandName = "quitar_A_Grupo" Then

			Dim index As Integer = Convert.ToInt32(e.CommandArgument)

			' Obtener id_grupo desde DataKeys
			Dim idManual As Integer = Convert.ToInt32(grvListaManuales.DataKeys(index).Value)

			' Abrir modal con Bootstrap 5
			ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal",
			"var myModal = new bootstrap.Modal(document.getElementById('modalListaManuales')); myModal.show();",
			True)

		End If
	End Sub

	Protected Sub btnEliminarGrupo_Click(sender As Object, e As EventArgs) Handles btnEliminarGrupo.Click
		Dim id_grupo As Integer
		Dim id_usuario As Integer
		Dim resultado As Boolean

		id_usuario = Integer.Parse(Session("id_usuario").ToString())
		id_grupo = Integer.Parse(lblidGrupoEliminar.Text.ToString())

		resultado = obj.Eliminar_grupo(id_grupo, id_usuario)
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
			Listar_Grupos_recetas()
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
		Response.Redirect("Gestion_grupos.aspx")
	End Sub

	Public Function CargarActivacionGeneral() As Boolean
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
		obj.CerrarSesionGlobal(Session("ACTIVACION_GENERAL").ToString())
	End Sub
	Public Sub registro_acceso_pagina(ByVal TokenGlobal As String, ByVal sistema As String, ByVal user As String)
		obj.Registro_SesionSistema(TokenGlobal, user, sistema, "Gestion_grupos.aspx")
	End Sub
End Class