Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.Security
Imports Negocio
Imports System.Net

Imports System.Security.Cryptography
Imports System.IO
Imports System.Text

Public Class login
	Inherits System.Web.UI.Page
	Dim obj As New Negocio.NRecetas
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then

            '1. Capturo el token desde la Suite: Token de usuario y clave y token de activacion general (DE BD)
            Dim token As String = Request.QueryString("t")
            Dim tokenGlobal As String = Request.QueryString("tg")
            Session("ACTIVACION_GENERAL") = tokenGlobal


            '2. Registro de Logs en el sistema ARRECETAS
            UtilLog.EscribirLog(
                "Ingreso Login ARRECETAS. URL=" &
                Request.Url.ToString())

            UtilLog.EscribirLog(
                "Host=" &
                Request.Url.Host)

            UtilLog.EscribirLog(
                "token recibido=" &
                If(token, "(NULL)"))

            UtilLog.EscribirLog(
                "TokenGlobal recibido=" &
                If(tokenGlobal, "(NULL)"))



            If Not String.IsNullOrEmpty(token) Then

                Dim datos As String = Desencriptar(token)

                Dim partes() As String = datos.Split("|")

                Dim usuario As String = partes(0)
                Dim clave As String = partes(1)

                '3.Registros de Ingreso al sistema ARRECETAS y Acceso
                Session("SistemaAcceso") = "ARRCT"
                obj.Registro_SesionSistema(tokenGlobal, usuario, Session("SistemaAcceso").ToString(), "login.aspx")

                '4.================ PROCESO COOKIE =================
                Dim resultado_cookie As Boolean
                resultado_cookie = CargarActivacionGeneral()

                '5.================ PROCESO COOKIE =================
                If resultado_cookie Then
                    validar_ingreso_sistema_suite(usuario, clave) 'Proceso del boton Login (El de antes)
                End If

            Else
                Response.Redirect("~/SesionExpirada.aspx")
            End If


        End If


    End Sub
    Public Function CargarActivacionGeneral() As Boolean

        Dim token As String = Session("ACTIVACION_GENERAL")
        Dim ds As New DataSet
        ds = obj.ExisteTokenActivo(token)

        If ds.Tables(0).Rows.Count > 0 Then

            Return True
        Else
            Response.Redirect("~/SesionExpirada.aspx", False)
            Return False
        End If

        'Dim cookie As HttpCookie = Request.Cookies("ACTIVACION_GENERAL")

        'If cookie IsNot Nothing AndAlso Not String.IsNullOrEmpty(cookie.Value) Then

        '    Session("ACTIVACION_GENERAL") = cookie.Value
        '    UtilLog.EscribirLog(
        '        "login.aspx: COOKIE OK. Valor=" & cookie.Value)

        '    Return True

        'Else
        '    UtilLog.EscribirLog(
        '        "login.aspx: COOKIE NO EXISTE")
        '    Session.Clear()
        '    Session.Abandon()

        '    Response.Redirect("~/SesionExpirada.aspx", False)
        '    HttpContext.Current.ApplicationInstance.CompleteRequest()

        '    Return False
        'End If

    End Function
    Private Function Desencriptar(textoEncriptado As String) As String

        Dim clave As String = "ACURIO2026SUPERKEY"

        Dim aes As New AesManaged()

        Dim pdb As New Rfc2898DeriveBytes(
        clave,
        New Byte() {
            &H49, &H76, &H61, &H6E,
            &H20, &H4D, &H65, &H64,
            &H76, &H65, &H64, &H65,
            &H76
        })

        aes.Key = pdb.GetBytes(32)
        aes.IV = pdb.GetBytes(16)

        Dim bytes As Byte() =
        Convert.FromBase64String(textoEncriptado)

        Dim ms As New MemoryStream(bytes)

        Dim cs As New CryptoStream(
        ms,
        aes.CreateDecryptor(),
        CryptoStreamMode.Read)

        Dim sr As New StreamReader(cs, Encoding.UTF8)

        Return sr.ReadToEnd()

    End Function

    Public Function IsAuthenticated(ByVal Domain As String, ByVal username As String, ByVal pwd As String) As Boolean
        Dim Success As Boolean = False
        Dim Entry As New System.DirectoryServices.DirectoryEntry("LDAP://" & Domain, username, pwd)
        Dim Searcher As New System.DirectoryServices.DirectorySearcher(Entry)
        Searcher.SearchScope = DirectoryServices.SearchScope.OneLevel
        Try
            Dim Results As System.DirectoryServices.SearchResult = Searcher.FindOne
            Success = Not (Results Is Nothing)
        Catch
            Success = False
        End Try
        Return Success
    End Function

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim user As String
            Dim pwd As String
            Dim ds As New DataSet
            Dim dsUnidades As New DataSet

            user = txtUsuario.Text.ToString()
            pwd = txtPassword.Text.ToString()

            '------------------reCaptcha-------------------
            Dim result As String
            Dim success As Boolean

            'Dim respuestaRecaptcha As String = Request.Form("g-recaptcha-response")
            'Dim secretKey As String = Session("clave_secreta")
            'Dim client As New WebClient()

            'Dim result As String = client.DownloadString($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={respuestaRecaptcha}")

            'Dim jsonResult As JObject = JObject.Parse(result)
            'Dim success As Boolean = jsonResult.Value(Of Boolean)("success")

            Dim can As Integer
            Dim usuario As String
            Dim clave As String
            Dim tipo_usuario As String = ""
            Dim id_usuario As String
            Dim id_perfil As Integer
            Dim resultado As Boolean

            Dim i As Integer = 6
            usuario = user
            clave = pwd

            'Validación AD
            resultado = IsAuthenticated("acurio.net", usuario, clave)

            'Validar en Tabla - Administrador de Usuarios
            ds = obj.validar_acceso(user)
            can = ds.Tables(0).Rows.Count

            If resultado And can > 0 Then

                'Validamos la existencia del usuario en arsysusers

                id_usuario = ds.Tables(0).Rows(0)(0).ToString()
                usuario = ds.Tables(0).Rows(0)(4).ToString()

                Session("user") = usuario '"imartinez"
                Session("id_usuario") = id_usuario 'de la BD de accesos

                Response.Redirect("~/main.aspx")
                'Dim strRedirect As String
                'strRedirect = Request("ReturnUrl")
                'If strRedirect Is Nothing Then
                '    strRedirect = "main.aspx"
                'End If

                'Dim vRecuerdame = False 'cuando pongas tu chek de recordar tu pagina 
                'Dim tkt = New FormsAuthenticationTicket(1, user, DateTime.Now, DateTime.Now.AddMinutes(30), vRecuerdame, user)
                'Dim cookiestr = FormsAuthentication.Encrypt(tkt)
                'Dim ck = New HttpCookie("appNameAuth", cookiestr)
                'If vRecuerdame Then
                '    ck.Expires = tkt.Expiration
                'End If
                'ck.Path = FormsAuthentication.FormsCookiePath
                'Response.Cookies.Add(ck)
                'Response.Redirect(strRedirect, True)
            Else
                Response.Redirect("login.aspx")
                'lblMensajeError.Text = "Contraseña o nombre de usuario incorrecto."

            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub validar_ingreso_sistema_suite(ByVal user As String, ByVal pwd As String)
        Try
            Dim ds As New DataSet
            Dim dsUnidades As New DataSet


            Dim can As Integer
            Dim usuario As String
            Dim clave As String
            Dim tipo_usuario As String = ""
            Dim id_usuario As String
            Dim id_perfil As Integer
            Dim resultado As Boolean

            Dim i As Integer = 6
            usuario = user
            clave = pwd

            'Validar en Tabla - Administrador de Usuarios
            ds = obj.validar_acceso(user)
            can = ds.Tables(0).Rows.Count
            resultado = True 'Siempre acceder - La validacion es desde la SUITE

            If resultado And can > 0 Then
                'Validamos la existencia del usuario en arsysusers

                id_usuario = ds.Tables(0).Rows(0)(0).ToString()
                usuario = ds.Tables(0).Rows(0)(4).ToString()

                Session("user") = usuario '"imartinez"
                Session("id_usuario") = id_usuario 'de la BD de accesos


                Dim strRedirect As String
                strRedirect = Request("ReturnUrl")
                If strRedirect Is Nothing Then
                    strRedirect = "main.aspx"
                End If

                Dim vRecuerdame = False 'cuando pongas tu chek de recordar tu pagina 
                Dim tkt = New FormsAuthenticationTicket(1, user, DateTime.Now, DateTime.Now.AddMinutes(30), vRecuerdame, user)
                Dim cookiestr = FormsAuthentication.Encrypt(tkt)
                Dim ck = New HttpCookie("appNameAuth", cookiestr)
                If vRecuerdame Then
                    ck.Expires = tkt.Expiration
                End If
                ck.Path = FormsAuthentication.FormsCookiePath
                Response.Cookies.Add(ck)
                Response.Redirect(strRedirect, True)

            Else
                Response.Redirect("login.aspx")
                'lblMensajeError.Text = "Contraseña o nombre de usuario incorrecto."

            End If
        Catch ex As Exception

        End Try

    End Sub
End Class