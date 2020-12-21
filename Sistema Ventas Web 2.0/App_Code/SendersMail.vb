Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Data
Imports PDataAccess
Imports PLogic
Imports PEncry
Imports PEntity
Imports System.Collections.Generic
Imports System.Net.Mail
Imports System.Web.Mail
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Net.Configuration
Imports System.IO
Imports PUtilitario
Imports System.Net.Mime

Public Module SendersMail
    Private HtmBr As String = "<br>"
    Private SendOnCreateUsers As String = AppSettings.valueString("SendOnCreateUsers").ToUpper
    Private SendOnCreateOrders As String = AppSettings.valueString("SendOnCreateOrders").ToUpper
    Private SendOnProcessPayOk As String = AppSettings.valueString("SendOnProcessPayOk").ToUpper
    ''Private SendOnProcessPayCancel As String = AppSettings.valueString("SendOnProcessPayCancel").ToUpper
    'Private SendOnConfirProcessPayOk As String = AppSettings.valueString("SendOnConfirProcessPayOk").ToUpper
    Private SendOnProcessPayError As String = AppSettings.valueString("SendOnProcessPayError").ToUpper
    Private SendOnErrorSystemAdmin As String = AppSettings.valueString("SendErrorSystemConfiguration").ToUpper
    Private SendOnErrorSystem As String = AppSettings.valueString("SendErrorSystem").ToUpper
    Private SendOnErrorSender As String = AppSettings.valueString("SendErrorWeb").ToUpper
    Private FactElectronica As String = AppSettings.valueString("FactElectronica").ToUpper




#Region "IsMail"
    Public Function IsMail(ByVal strEmail As String) As Boolean
        Dim l_reg As New System.Text.RegularExpressions.Regex("^(([^<;>;()[\]\\.,;:\s@\""]+" & _
        "(\.[^<;>;()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@" & _
        "((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}" & _
        "\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+" & _
        "[a-zA-Z]{2,}))$")
        Return (l_reg.IsMatch(strEmail))
    End Function

    Function IsValidEmail(ByVal strIn As String) As Boolean
        Return Regex.IsMatch(strIn, ("^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
    End Function
#End Region



#Region "Metodos Privados"

    Private Function SendMail(ByVal strTo As String, ByVal strSubj As String, ByVal strBody As String, ListaPasajeros As ListaAsientos, Optional ByVal CCO As Boolean = False, Optional ByVal IsHtml As Boolean = False, Optional ByVal Firma As Boolean = True, Optional ByVal IsMsg As Boolean = False, Optional ByVal replyTo As String = "") As String
        Dim correo As New System.Net.Mail.MailMessage
        Dim smtp As New System.Net.Mail.SmtpClient
        Dim mailData() As String = ReadMailConfig()
        Dim strFrom As String
        Dim mailNameFrom As String = String.Empty

        If Not AppSettings.valueString("fromMail") Is Nothing Then
            strFrom = AppSettings.valueString("fromMail")
        Else
            strFrom = mailData(1)
        End If
        If Not AppSettings.valueString("fromMailName") Is Nothing Then
            mailNameFrom = AppSettings.valueString("fromMailName").Replace("[NombreEmpresa]", AppSettings.valueString("NombreEmpresa"))
        End If

        If Firma Then
            strBody = strBody & HtmBr & HtmBr & HtmBr & _
                    "<b> " & AppSettings.valueString("NombreEmpresa") & " </b>" & HtmBr & _
                    "<a href='" & AppSettings.valueString("UrlWebSiteEmpresa") & "'><b>" & AppSettings.valueString("UrlWebSiteEmpresa") & "</b></a> <br> (" & AppSettings.valueString("MensajeFirmaCompra") & ") " & HtmBr & _
                   "<br><br>email procesado:" & Now.ToString
        End If

        correo.From = New System.Net.Mail.MailAddress(mailNameFrom & "<" & strFrom & ">")
        correo.To.Add(strTo)

        correo.Subject = strSubj
        correo.HeadersEncoding = Encoding.Unicode
        correo.BodyEncoding = Encoding.UTF8
        correo.IsBodyHtml = True
        ''correo.BodyEncoding = System.Text.Encoding.UTF8
        ''correo.Body = strBody
        Dim htmlView As System.Net.Mail.AlternateView = AlternateView.CreateAlternateViewFromString(strBody, New ContentType("text/html"))

        htmlView.ContentType.CharSet = Encoding.UTF8.WebName
        correo.AlternateViews.Add(htmlView)

        ''correo.SubjectEncoding = Encoding.UTF8
        ''Adjuntar PDF
        If Not ListaPasajeros Is Nothing AndAlso ListaPasajeros.Count > 0 Then
            For Each entidad As EAsiento In ListaPasajeros

                If entidad.DocumentoPDF <> String.Empty Then
                    Dim oMemoryStream As New MemoryStream(Convert.FromBase64String(entidad.DocumentoPDF))
                    If Convert.FromBase64String(entidad.DocumentoPDF).Length <> 0 Then
                        correo.Attachments.Add(New System.Net.Mail.Attachment(oMemoryStream, entidad.NomFileXmlCertificado + ".pdf"))
                    End If
                End If
            Next
        End If


        ''Adjuntar Certificado XML
        If Not ListaPasajeros Is Nothing AndAlso ListaPasajeros.Count > 0 Then
            For Each entidad As EAsiento In ListaPasajeros

                If entidad.XmlCertificado <> String.Empty Then
                    Dim oMemoryStream As New MemoryStream(Convert.FromBase64String(entidad.XmlCertificado))
                    If Convert.FromBase64String(entidad.XmlCertificado).Length <> 0 Then
                        correo.Attachments.Add(New System.Net.Mail.Attachment(oMemoryStream, entidad.NomFileXmlCertificado + ".zip"))
                    End If
                End If
                If entidad.XmlCertificadoSUNAT <> String.Empty Then
                    Dim oMemoryStream As New MemoryStream(Convert.FromBase64String(entidad.XmlCertificadoSUNAT))
                    If Convert.FromBase64String(entidad.XmlCertificado).Length <> 0 Then
                        correo.Attachments.Add(New System.Net.Mail.Attachment(oMemoryStream, "CDR-" & entidad.NomFileXmlCertificado + ".zip"))
                    End If
                End If

            Next
        End If


        If CCO = True Then
            Dim bcc As New MailAddress(AppSettings.valueString("EmailCCO"))
            correo.Bcc.Add(bcc)
        End If

        Dim html As AlternateView
        html = AlternateView.CreateAlternateViewFromString(strBody, Nothing, "text/html")

        correo.AlternateViews.Add(html)
        correo.IsBodyHtml = IsHtml
        correo.Priority = System.Net.Mail.MailPriority.High
        smtp.Host = mailData(1)
        If Len(Trim(mailData(2))) > 0 And Len(Trim(mailData(2))) > 0 Then
            smtp.Credentials = New System.Net.NetworkCredential(mailData(3), mailData(4))
        End If
        If Len(Trim(mailData(2))) > 0 Then
            smtp.Port = mailData(2)
        End If

        Try

            If AppSettings.valueString("SSLMail") = "true" Then
                smtp.EnableSsl = True
            End If
            smtp.Send(correo)
            Return ""

        Catch ex As Exception
            Log.Instance(GetType(SendersMail)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
            Return "(Error al enviar correo: " & ex.Message & " " & ex.Source & ")"
        End Try
    End Function
    Private Function ReadMailConfig() As String()
        Try
            ''Prueba
            ''Dim MailConfig(4) As String
            ''MailConfig(1) = "smtp.gmail.com"
            ''MailConfig(2) = "587"
            ''MailConfig(3) = "control.web.2016@gmail.com"
            ''MailConfig(4) = "chapulin2016"
            ''Producción
            Dim cngF As Configuration = WebConfigurationManager.OpenWebConfiguration("~/")
            Dim mailSettings As MailSettingsSectionGroup = cngF.GetSectionGroup("system.net/mailSettings")
            Dim MailConfig(4) As String
            If Not mailSettings Is Nothing Then
                MailConfig(1) = mailSettings.Smtp.Network.Host
                MailConfig(2) = mailSettings.Smtp.Network.Port
                MailConfig(3) = mailSettings.Smtp.Network.UserName
                MailConfig(4) = mailSettings.Smtp.Network.Password
            End If
            ReadMailConfig = MailConfig
        Catch ex As Exception
            Log.Instance(GetType(SendersMail)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function

    Function OpenSendMails(ByVal strNameTxt As String) As String
        Try
            Dim FileItem As New FileInfo(Variables.sPathDir & "/mailtmpl/" & strNameTxt)
            Dim strRet As String = String.Empty
            If FileItem.Exists Then
                Dim r As StreamReader = FileItem.OpenText()
                strRet = Replace(r.ReadToEnd, Chr(10), "<br>")
                r.Close()
            End If
            Return strRet
        Catch ex As Exception
            Log.Instance(GetType(SendersMail)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function
#End Region


#Region "Metodos Publicos"

    Public Sub NotificacionErrorSale(NroPedido As String, Cantidad As String, EmailCliente As String)
        Try

            Dim strTo As String = AppSettings.valueString("SendErrorSale")
            Dim strSubj As String = Functions.NombreAsuntoMensajeEmail(AppSettings.valueString("sbjSendErrorSale"))
            Dim strBody As New StringBuilder
            strBody.Append(OpenSendMails("saleError.txt"))
            strBody.Replace("[NRO_PEDIDO]", NroPedido)
            strBody.Replace("[CANTIDAD]", Cantidad)
            strBody.Replace("[EMAIL]", EmailCliente)
            strBody.Replace("[FECHATRANSACCION]", Now.ToString("dd/MM/yyyy"))
            If strTo.Contains(",") Then
                Dim correos As String() = strTo.Split(",")
                For i As Integer = 0 To correos.Length - 1
                    If String.IsNullOrWhiteSpace(correos(i)) = False Then
                        SendMail(correos(i), strSubj, strBody.ToString, Nothing, True, True, False)
                    End If
                Next
            Else
                SendMail(strTo, strSubj, strBody.ToString, Nothing, True, True, False)
            End If
        Catch ex As Exception
            Log.Instance(GetType(SendersMail)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Public Sub NotificacionErrorDeveloper(ByVal strMsg As String, ByVal sistema As String, ByVal metodo As String)
        Try
            If SendOnErrorSystem <> "TRUE" Then
                Exit Sub
            End If
            Dim strTo As String = AppSettings.valueString("SendErrorSystemMail")
            Dim strSubj As String = Functions.NombreAsuntoMensajeEmail(AppSettings.valueString("sbjSendErrorSystem")) & " FH: " & Now.Date.ToString & " - " & Now.TimeOfDay.ToString
            Dim strBody As New StringBuilder
            If SessionManager.ViajeRetorno Then
                strBody.Append(OpenSendMails("systemerrorretorno.txt"))
                strBody.Replace("[SYSTEM]", sistema)
                strBody.Replace("[METODO]", metodo)
                strBody.Replace("[MESSAGE]", strMsg)
                strBody.Replace("[RUTA]", SessionManager.Origen_Ida & " - " & SessionManager.Destino_Ida)
                strBody.Replace("[SERVICIO]", SessionManager.ServicioIda)
                strBody.Replace("[FECHA-HORA-VIAJE]", SessionManager.FechaDeViajeIda & " - " & SessionManager.HoraViajeIda)
                strBody.Replace("[RUTA-RETORNO]", SessionManager.Origen_Retorno & " - " & SessionManager.Destino_Retorno)
                strBody.Replace("[SERVICIO-RETORNO]", SessionManager.ServicioRetorno)
                strBody.Replace("[FECHA-HORA-VIAJE-RETORNO]", SessionManager.FechaDeViajeRetorno & " - " & SessionManager.HoraViajeRetorno)
                strBody.Replace("[USERNAME]", SessionManager.Name)
            Else
                strBody.Append(OpenSendMails("systemerror.txt"))
                strBody.Replace("[SYSTEM]", sistema)
                strBody.Replace("[METODO]", metodo)
                strBody.Replace("[MESSAGE]", strMsg)
                strBody.Replace("[RUTA]", SessionManager.Origen_Ida & " - " & SessionManager.Destino_Ida)
                strBody.Replace("[SERVICIO]", SessionManager.ServicioIda)
                strBody.Replace("[FECHA-HORA-VIAJE]", SessionManager.FechaDeViajeIda & " - " & SessionManager.HoraViajeIda)
                strBody.Replace("[USERNAME]", SessionManager.Name)
            End If

            SendMail(strTo, strSubj, strBody.ToString, Nothing, True)
        Catch ex As Exception
            Log.Instance(GetType(SendersMail)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Public Sub NotificacionErrorSystemAdmin(ByVal strMsg As String)

        Try
            If SendOnErrorSystemAdmin <> "TRUE" Then
                Exit Sub
            End If
            Dim strTo As String = AppSettings.valueString("SendErrorSystemConfigurationMail")
            Dim strSubj As String = Functions.NombreAsuntoMensajeEmail(AppSettings.valueString("sbjSendErrorSystemConfiguration"))
            Dim strBody As New StringBuilder
            If SessionManager.ViajeRetorno Then
                strBody.Append(OpenSendMails("systemconfigurationerrorretorno.txt"))
                strBody.Replace("[MESSAGE]", strMsg)
                strBody.Replace("[RUTA]", SessionManager.Origen_Ida & " - " & SessionManager.Destino_Ida)
                strBody.Replace("[SERVICIO]", SessionManager.ServicioIda)
                strBody.Replace("[FECHA-HORA-VIAJE]", SessionManager.FechaDeViajeIda & " - " & SessionManager.HoraViajeIda)
                strBody.Replace("[RUTA-RETORNO]", SessionManager.Origen_Retorno & " - " & SessionManager.Destino_Retorno)
                strBody.Replace("[SERVICIO-RETORNO]", SessionManager.ServicioRetorno)
                strBody.Replace("[FECHA-HORA-VIAJE-RETORNO]", SessionManager.FechaDeViajeRetorno & " - " & SessionManager.HoraViajeRetorno)

                strBody.Replace("[USERNAME]", SessionManager.Name)
            Else
                strBody.Append(OpenSendMails("systemconfigurationerror.txt"))
                strBody.Replace("[MESSAGE]", strMsg)
                strBody.Replace("[RUTA]", SessionManager.Origen_Ida & " - " & SessionManager.Destino_Ida)
                strBody.Replace("[SERVICIO]", SessionManager.ServicioIda)
                strBody.Replace("[FECHA-HORA-VIAJE]", SessionManager.FechaDeViajeIda & " - " & SessionManager.HoraViajeIda)
                strBody.Replace("[USERNAME]", SessionManager.Name)
            End If


            SendMail(strTo, strSubj, strBody.ToString, Nothing, True)
        Catch ex As Exception
            Log.Instance(GetType(SendersMail)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Public Function smErrosTransaction() As String

        Try
            If SendOnProcessPayError <> "TRUE" OrElse String.IsNullOrEmpty(SessionManager.Correo_Users) Then
                Return "False"
            End If

            Dim strTo As String = ""
            Dim strSubj As String = Functions.NombreAsuntoMensajeEmail(AppSettings.valueString("sbjTransationError"))
            Dim strBody As String = String.Empty
            strTo = SessionManager.Correo_Users
            strBody = OpenSendMails("payError.txt")
            strBody = Replace(strBody, "[NAME_USER]", SessionManager.PerfilName)

            Return SendMail(strTo, strSubj, strBody, Nothing, True)
        Catch ex As Exception
            Log.Instance(GetType(SendersMail)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function

    Public Function smNewPassword(ByVal Usuario As String) As String

        Try
            Dim oPLGlobals As New PLGlobals
            Dim respuesta As String = String.Empty
            Dim lista As ListaUsers
            Dim strTo As String = ""
            Dim strBody As String = ""
            Dim strSubj As String = Functions.NombreAsuntoMensajeEmail(AppSettings.valueString("sbjNewPassword"))
            lista = oPLGlobals.Logueo_Usuario_Web(Usuario)
            If lista.Count > 0 Then
                Dim oEncry As New Crypto()
                Dim nPws As String = Util.CharRandom(10)
                oPLGlobals.Users_Change_Password(Usuario, oEncry.Encrypt(nPws))
                strBody = OpenSendMails("newPasswords.txt")
                If String.IsNullOrEmpty(lista.Item(0).Nombres_Users.ToString) AndAlso String.IsNullOrEmpty(lista.Item(0).Apellidos_Users.ToString) Then
                    strBody = Replace(strBody, "[NAME_USER]", "Cliente")
                Else
                    strBody = Replace(strBody, "[NAME_USER]", lista.Item(0).Nombres_Users.ToString & " " & lista.Item(0).Apellidos_Users.ToString)
                End If
                strBody = Replace(strBody, "[USERNAME]", Usuario)
                strBody = Replace(strBody, "[PASSWORD]", nPws)
                strTo = lista.Item(0).Correo_Users.ToString
                respuesta = SendMail(strTo, strSubj, strBody, Nothing, True)
            Else
                respuesta = "El usuario ingresado no existe"
            End If
            Return respuesta
        Catch ex As Exception
            Log.Instance(GetType(SendersMail)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Function

    Public Function smNewUser(ByVal strUserName As String, ByVal Passwords As String, ByVal email As String) As String
        Try
            If SendOnCreateUsers <> "TRUE" OrElse String.IsNullOrEmpty(email) Then
                Return ""
            End If
            Dim strTo As String = ""
            Dim strSubj As String = ""
            Dim strBody As String = ""

            strBody = OpenSendMails("createUsers.txt")
            strBody = Replace(strBody, "[USERNAME]", strUserName)
            strBody = Replace(strBody, "[PASSWORD]", Passwords)
            strTo = email
            strSubj = Functions.NombreAsuntoMensajeEmail(AppSettings.valueString("sbjCreateUser"))
            Return SendMail(strTo, strSubj, strBody, Nothing, True)
        Catch ex As Exception
            Log.Instance(GetType(SendersMail)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Function

    Public Function smCreateOrderConfirm(ByVal strOrder As String, ByVal ctAsientos As Integer) As String

        Try
            If SendOnCreateOrders <> "TRUE" OrElse String.IsNullOrEmpty(SessionManager.Correo_Users) Then
                Return ""
            End If
            Dim strTo As String = ""
            Dim strSubj As String = Functions.NombreAsuntoMensajeEmail(AppSettings.valueString("sbjCreateOrderConfirm"))
            Dim strBody As String = ""
            strTo = SessionManager.Correo_Users
            strBody = OpenSendMails("createOrders.txt")
            strBody = Replace(strBody, "[NAME_USER]", SessionManager.PerfilName)
            strBody = Replace(strBody, "[NUMORDER]", strOrder)
            strBody = Replace(strBody, "[CTASIENTOS]", Str(ctAsientos))

            Return SendMail(strTo, strSubj, strBody, Nothing, True)
        Catch ex As Exception
            Log.Instance(GetType(SendersMail)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function

    Public Function smCompletTransaction(ByVal strNumOrder As String, ByVal strFechaHora As String) As String


        Try
            If SendOnProcessPayOk <> "TRUE" OrElse String.IsNullOrEmpty(SessionManager.Correo_Users) Then
                Return ""
            End If
            Dim strTo As String = ""
            Dim strSubj As String = Functions.NombreAsuntoMensajeEmail(AppSettings.valueString("sbjTransationOk"))
            Dim strBody As String = ""
            ''strTo = "williams.morales@bitsolutions.com.pe"
            strTo = SessionManager.Cliente.Correo_Users

            strBody = OpenSendMails("payOk.txt")
            Dim ListaPasajeros As New ListaAsientos
            'strBody = Replace(strBody, "[NAME_USER]", SessionManager.Correo_Users)
            'strBody = Replace(strBody, "[DATEPAY]", SessionManager.FechaTransaccionVisa)
            strBody = Replace(strBody, "[NRO_ORDEN]", strNumOrder)
            Dim HTML_HD_IDA As New StringBuilder
            Dim HTML_HD_RETORNO As New StringBuilder
            If Not SessionManager.Cliente.ListaAsientosIda Is Nothing AndAlso SessionManager.Cliente.ListaAsientosIda.Count > 0 Then
                ListaPasajeros = SessionManager.Cliente.ListaAsientosIda
                HTML_HD_IDA.Append("<tr style='border-spacing: 0;'>")
                HTML_HD_IDA.AppendLine("<td style='border-spacing: 0; border-left:1px solid #dab369;border-right:1px solid #dab369; background:#76d376;padding:10px 0px;color:#fff'> ")
                HTML_HD_IDA.AppendLine("<table><tbody><tr>")
                HTML_HD_IDA.AppendLine("<th style='padding-left:20px; padding-right:100px;'>" & SessionManager.Origen_Ida & " - " & SessionManager.Destino_Ida & " (ida)</th>")
                HTML_HD_IDA.AppendLine("<th> " & CDate(SessionManager.FechaDeViajeIda).ToString("dd MMMM, yyyy") & " " & SessionManager.HoraViajeIda & "</th>")
                HTML_HD_IDA.AppendLine("</tr></tbody></table></td></tr>")

                Dim HTML_DT_IDA As New StringBuilder
                HTML_DT_IDA.Append("<tr style='border-spacing: 0;'><td style='border-spacing: 0; border:1px solid #d3d3d3; '> <table><tbody> ")
                HTML_DT_IDA.AppendLine("<tr><th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>Doc.</th>")
                HTML_DT_IDA.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>Numero</th>")
                HTML_DT_IDA.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>Pasajero</th>")
                HTML_DT_IDA.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>Servicio</th>")
                HTML_DT_IDA.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>Asiento</th>")
                HTML_DT_IDA.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>Precio</th></tr>")
                For n As Integer = 0 To SessionManager.Cliente.ListaAsientosIda.Count - 1
                    HTML_DT_IDA.AppendLine("<tr>")
                    With SessionManager.Cliente.ListaAsientosIda.Item(n)
                        HTML_DT_IDA.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>" & Util.DescripcionDoc(.Tipo_Doc) & "</th>")
                        HTML_DT_IDA.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>" & .DNI & "</th>")
                        HTML_DT_IDA.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>" & .Nombre & " " & .ApePaterno & " " & .ApeMaterno & "</th>")
                        HTML_DT_IDA.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>" & .Servicio & "</th>")
                        HTML_DT_IDA.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>" & .Nume_Asiento & "</th>")
                        HTML_DT_IDA.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>" & .Costo.ToString("#.#0") & "</th>")
                    End With

                    HTML_DT_IDA.AppendLine("</tr>")
                Next
                HTML_DT_IDA.AppendLine("</tbody></table></td></tr>  ")
                HTML_HD_IDA.AppendLine(HTML_DT_IDA.ToString)

            Else
                HTML_HD_IDA.Append("")
            End If
            If Not SessionManager.Cliente.ListaAsientosRetorno Is Nothing AndAlso SessionManager.Cliente.ListaAsientosRetorno.Count > 0 Then
                ListaPasajeros.AddRange(SessionManager.Cliente.ListaAsientosRetorno)
                HTML_HD_RETORNO.Append("<tr style='border-spacing: 0;'>")
                HTML_HD_RETORNO.AppendLine("<td style='border-spacing: 0; border-left:1px solid #dab369;border-right:1px solid #dab369; background:#76d376;padding:10px 0px;color:#fff;'> ")
                HTML_HD_RETORNO.AppendLine("<table><tbody><tr>")
                HTML_HD_RETORNO.AppendLine("<th style='padding-left:20px; padding-right:100px;'>" & SessionManager.Origen_Retorno & " - " & SessionManager.Destino_Retorno & " (retorno)</th>")
                HTML_HD_RETORNO.AppendLine("<th> " & CDate(SessionManager.FechaDeViajeRetorno).ToString("dd MMMM, yyyy") & " " & SessionManager.HoraViajeRetorno & "</th>")
                HTML_HD_RETORNO.AppendLine("</tr></tbody></table></td></tr>")

                Dim HTML_DT_RETORNO As New StringBuilder
                HTML_DT_RETORNO.Append("<tr style='border-spacing: 0;'><td style='border-spacing: 0; border:1px solid #d3d3d3; '> <table><tbody> ")
                HTML_DT_RETORNO.AppendLine("<tr><th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>Doc.</th>")
                HTML_DT_RETORNO.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>Numero</th>")
                HTML_DT_RETORNO.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>Pasajero</th>")
                HTML_DT_RETORNO.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>Servicio</th>")
                HTML_DT_RETORNO.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>Asiento</th>")
                HTML_DT_RETORNO.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>Precio</th></tr>")
                For n As Integer = 0 To SessionManager.Cliente.ListaAsientosRetorno.Count - 1
                    HTML_DT_RETORNO.AppendLine("<tr>")
                    With SessionManager.Cliente.ListaAsientosRetorno.Item(n)
                        HTML_DT_RETORNO.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>" & Util.DescripcionDoc(.Tipo_Doc) & "</th>")
                        HTML_DT_RETORNO.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>" & .DNI & "</th>")
                        HTML_DT_RETORNO.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>" & .Nombre & " " & .ApePaterno & " " & .ApeMaterno & "</th>")
                        HTML_DT_RETORNO.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>" & .Servicio & "</th>")
                        HTML_DT_RETORNO.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>" & .Nume_Asiento & "</th>")
                        HTML_DT_RETORNO.AppendLine("<th style='padding-left:20px;padding-bottom:10px;padding-top:10px;'>" & .Costo.ToString("#.#0") & "</th>")
                    End With

                    HTML_DT_RETORNO.AppendLine("</tr>")
                Next
                HTML_DT_RETORNO.AppendLine("</tbody></table></td></tr>  ")
                HTML_HD_RETORNO.AppendLine(HTML_DT_RETORNO.ToString)
            Else
                HTML_HD_RETORNO.Append("")
            End If


            strBody = Replace(strBody, "[HD_IDA]", HTML_HD_IDA.ToString)
            strBody = Replace(strBody, "[HD_RETORNO]", HTML_HD_RETORNO.ToString)
            strBody = strBody.Replace("<br>", "")
            If FactElectronica <> "TRUE" Then
                ListaPasajeros = Nothing
            End If
            Return SendMail(strTo, strSubj, strBody, ListaPasajeros, True)
        Catch ex As Exception
            Log.Instance(GetType(SendersMail)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Function

    Public Function ErrorTransaccionMonto(ByVal NOrden As String, ByVal montoreal As String, ByVal montovisa As String) As String

        Try
            Dim mailData() As String = ReadMailConfig()
            Dim strTo As String = AppSettings.valueString("SendErrorWebMail")
            Dim strSubj As String = Functions.NombreAsuntoMensajeEmail(AppSettings.valueString("sbjTransationErrorMonto"))
            Dim strBody As String = ""
            strBody = OpenSendMails("MontoErrorTrasaccion.txt")
            strBody = Replace(strBody, "[NAME_USER]", SessionManager.PerfilName)
            strBody = Replace(strBody, "[USER]", SessionManager.Correo_Users)
            strBody = Replace(strBody, "[NUMORDER]", NOrden)
            strBody = Replace(strBody, "[MONTOREAL]", montoreal)
            strBody = Replace(strBody, "[MONTOVISA]", montovisa)

            Return SendMail(strTo, strSubj, strBody, Nothing, True)
        Catch ex As Exception
            Log.Instance(GetType(SendersMail)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function

    Public Function ErrorSender(ByVal strMsg As String) As String

        Try
            If SendOnErrorSender <> "TRUE" Then
                Return ""
            End If
            Dim strTo As String = AppSettings.valueString("SendErrorWebMail")
            Dim strSubj As String = "Web Error"
            Dim strBody As String = strMsg
            Return SendMail(strTo, strSubj, strBody, Nothing, False)
        Catch ex As Exception
            Log.Instance(GetType(SendersMail)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function
#End Region

End Module