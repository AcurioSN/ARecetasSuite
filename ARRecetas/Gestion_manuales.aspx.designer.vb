'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class Gestion_manuales

	'''<summary>
	'''Control form1.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents form1 As Global.System.Web.UI.HtmlControls.HtmlForm

	'''<summary>
	'''Control ScriptManager1.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents ScriptManager1 As Global.System.Web.UI.ScriptManager

	'''<summary>
	'''Control lblUsuario.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents lblUsuario As Global.System.Web.UI.WebControls.Label

	'''<summary>
	'''Control btnCerrarSesion.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents btnCerrarSesion As Global.System.Web.UI.WebControls.Button

	'''<summary>
	'''Control txtDescripcionDocumento.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents txtDescripcionDocumento As Global.System.Web.UI.WebControls.TextBox

	'''<summary>
	'''Control hfIdGrupo.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents hfIdGrupo As Global.System.Web.UI.WebControls.HiddenField

	'''<summary>
	'''Control lblidgrupo.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents lblidgrupo As Global.System.Web.UI.WebControls.Label

	'''<summary>
	'''Control txtgruposeleccionado.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents txtgruposeleccionado As Global.System.Web.UI.WebControls.TextBox

	'''<summary>
	'''Control cbocentro_costos.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents cbocentro_costos As Global.System.Web.UI.WebControls.DropDownList

	'''<summary>
	'''Control txtReceta.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents txtReceta As Global.System.Web.UI.WebControls.TextBox

	'''<summary>
	'''Control txtObservaciones.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents txtObservaciones As Global.System.Web.UI.WebControls.TextBox

	'''<summary>
	'''Control fileImagen.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents fileImagen As Global.System.Web.UI.WebControls.FileUpload

	'''<summary>
	'''Control btnCancelar.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents btnCancelar As Global.System.Web.UI.WebControls.Button

	'''<summary>
	'''Control btnGuardar.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents btnGuardar As Global.System.Web.UI.WebControls.Button

	'''<summary>
	'''Control UpdatePanel1.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents UpdatePanel1 As Global.System.Web.UI.UpdatePanel

	'''<summary>
	'''Control grvdetalle_receta_seleccionada.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents grvdetalle_receta_seleccionada As Global.System.Web.UI.WebControls.GridView

	'''<summary>
	'''Control upModalReceta.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents upModalReceta As Global.System.Web.UI.UpdatePanel

	'''<summary>
	'''Control txtcod_buscar.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents txtcod_buscar As Global.System.Web.UI.WebControls.TextBox

	'''<summary>
	'''Control txtdesc_buscar.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents txtdesc_buscar As Global.System.Web.UI.WebControls.TextBox

	'''<summary>
	'''Control btnBuscarReceta.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents btnBuscarReceta As Global.System.Web.UI.WebControls.Button

	'''<summary>
	'''Control grvrecetasSelect.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents grvrecetasSelect As Global.System.Web.UI.WebControls.GridView

	'''<summary>
	'''Control upModalGrupo.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents upModalGrupo As Global.System.Web.UI.UpdatePanel

	'''<summary>
	'''Control grvGrupo.
	'''</summary>
	'''<remarks>
	'''Campo generado automáticamente.
	'''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
	'''</remarks>
	Protected WithEvents grvGrupo As Global.System.Web.UI.WebControls.GridView
End Class
