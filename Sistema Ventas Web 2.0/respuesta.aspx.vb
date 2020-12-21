Imports System.Data
Imports PLogic
Imports PEntity
Imports System.Collections.Generic
Imports System.Xml
Imports System.IO
Imports System.Linq
Imports PDataAccess
Imports System.Net
Imports PUtilitario
Imports System.Globalization
Imports System.Runtime.Serialization.Json

Partial Class respuesta

    Inherits System.Web.UI.Page
    Dim objPDAGlobals As New ClsGobals

#Region "Metodos Privados con Eventos"
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Me.Title = Functions.NombreTituloPagina(Me.Title)
        Try

            Dim p As String = String.Empty
            If Functions.ValidarPageTransaction(p) = False Then
                Response.Redirect(p, False)
                Exit Sub
            End If
            If Functions.UserLogueo(Me) Then
                WUCViewUser1.UserName = SessionManager.PerfilName
                WUCViewUser1.LogueoEnd = False
                WUCViewUser1.LogueoStart = True
            Else
                WUCViewUser1.LogueoEnd = True
                WUCViewUser1.LogueoStart = False
            End If

            If Not IsPostBack Then
                If SessionManager.isPagando = False Then
                    SessionManager.isPagando = True

                    'RETORNO VISA
                    objPDAGlobals.ActualizarWebOrdersEstadoId(SessionManager.Id_WebOrders, 9)

                    ''Linea de Prueba
                    ''ProcesarVentaWebLocal()

                    ''Linea de Produccion
                    ''ProcesarVentaWeb()
                    AutorizacionToken()
                End If
            End If

            Functions.EliminarAsientobySession()
            Functions.EliminarAsientosCaducados()
        Catch ex As Exception
            ShowError("Error Desconocido, por favor comuniquese con el administrador y con el banco proveedor de su tarjeta", ex.Message)
            Log.Instance(GetType(respuesta)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)

        End Try
    End Sub


    Protected Sub cmdSalirsis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSalirsis.Click
        Response.Redirect("destinos.aspx", False)
    End Sub

    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        ScriptUser.JQueryMensaje(Me, Message.EPage)
        Functions.ClearPostTransaccion()
    End Sub
#End Region


#Region "Metodos Privados"

    Private Sub AutorizacionToken()
        Dim merchantId As String = AppSettings.valueString("merchantId")
        Dim transactionToken As String = Request.Form("transactionToken")
        Dim channel As String = Request.Form("channel")
        Dim sessionPay As String = SessionManager.VisaNet.SessionPay
        Dim order As String = SessionManager.VisaNet.Orden
        Dim urlAuthorize As String = (AppSettings.valueString("UrlAPIAutorizacion") + merchantId)
        Dim service As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(Order))
        Dim memory As MemoryStream = New MemoryStream(Encoding.UTF8.GetBytes(SessionManager.VisaNet.Orden))
        Dim oOrder As Order = CType(service.ReadObject(memory), Order)
        Dim objOrder As New Order

        objOrder.tokenId = transactionToken
        objOrder.purchaseNumber = oOrder.purchaseNumber
        objOrder.amount = oOrder.amount
        objOrder.currency = oOrder.currency

        Dim oSponsored As New Sponsored

        oSponsored.merchantId = merchantId
        oSponsored.name = "VisaNet Peru"
        oSponsored.mcci = ""
        oSponsored.address = "Lima Peru"
        oSponsored.phoneNumber = ""

        Dim oAuthorizationRequest As New AuthorizationRequest
        oAuthorizationRequest.channel = channel
        oAuthorizationRequest.captureType = "manual"
        oAuthorizationRequest.countable = True
        oAuthorizationRequest.order = objOrder

        Dim body As String = JsonHelper.JsonSerializer(Of AuthorizationRequest)(oAuthorizationRequest)
        Log.Instance(GetType(respuesta)).LogInformacion("Request Autorizacion: " + body)
        Dim requestAutorization As HttpWebRequest
        requestAutorization = CType(WebRequest.Create(urlAuthorize), HttpWebRequest)
        requestAutorization.Method = "POST"
        requestAutorization.ContentType = "application/json"
        requestAutorization.Headers.Add("Authorization", sessionPay)
        Dim writer As StreamWriter
        writer = New StreamWriter(requestAutorization.GetRequestStream())
        writer.Write(body)
        writer.Close()

        Dim response As HttpWebResponse
        Dim reader As StreamReader
        Dim respuestaAutorizacion As String

        Try
            response = CType(requestAutorization.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())
            respuestaAutorizacion = reader.ReadToEnd()
            Log.Instance(GetType(respuesta)).LogInformacion("Response Autorizacion: " + respuestaAutorizacion)
            reader.Close()
        Catch ex As WebException
            reader = New StreamReader(ex.Response.GetResponseStream, True)
            respuestaAutorizacion = reader.ReadToEnd()
            reader.Close()
            Log.Instance(GetType(respuesta)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
            Log.Instance(GetType(respuesta)).LogInformacion("Response Autorizacion Error: " + respuestaAutorizacion)
            Dim oPLGlobals As New PLGlobals
            oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
        End Try


        Try

            If String.IsNullOrEmpty(respuestaAutorizacion) = False Then
                Dim oVisaNetAutorizacion As VisaNetAutorizacion
                oVisaNetAutorizacion = JsonHelper.JsonDeserialize(Of VisaNetAutorizacion)(respuestaAutorizacion)
                If Not oVisaNetAutorizacion Is Nothing Then
                    If Not oVisaNetAutorizacion.dataMap Is Nothing AndAlso String.IsNullOrWhiteSpace(oVisaNetAutorizacion.dataMap.TRANSACTION_DATE) = False Then
                        Dim provider As CultureInfo = CultureInfo.InvariantCulture
                        Dim oETb_Info_TransaccionVisaNet As New ETb_Info_TransaccionVisaNet
                        SessionManager.Respuesta = oVisaNetAutorizacion.dataMap.ACTION_CODE
                        SessionManager.Cod_Tienda = oVisaNetAutorizacion.dataMap.MERCHANT
                        SessionManager.NroOrdenTienda = oVisaNetAutorizacion.order.purchaseNumber
                        SessionManager.Cod_Accion = oVisaNetAutorizacion.dataMap.ACTION_CODE
                        SessionManager.MountVisa = oVisaNetAutorizacion.dataMap.AMOUNT
                        SessionManager.FechaTransaccionVisa = DateTime.ParseExact(oVisaNetAutorizacion.dataMap.TRANSACTION_DATE, "yyMMddHHmmss", provider).ToString("dd/MM/yyyy HH:mm:ss")
                        SessionManager.TarjetaTransaccion = oVisaNetAutorizacion.dataMap.CARD
                        SessionManager.Cod_Desc_Accion = oVisaNetAutorizacion.dataMap.ACTION_DESCRIPTION
                        SessionManager.NomTarjeta = oVisaNetAutorizacion.dataMap.BRAND
                        SessionManager.Transaccion_Id = oVisaNetAutorizacion.order.transactionId
                        oETb_Info_TransaccionVisaNet.Respuesta = SessionManager.Respuesta
                        oETb_Info_TransaccionVisaNet.Nro_Orden = SessionManager.NroOrdenTienda
                        oETb_Info_TransaccionVisaNet.Cod_Accion = SessionManager.Cod_Accion
                        oETb_Info_TransaccionVisaNet.Importe_Autorizado = SessionManager.MountVisa
                        oETb_Info_TransaccionVisaNet.Fecha_Compra = DateTime.Parse(SessionManager.FechaTransaccionVisa)
                        oETb_Info_TransaccionVisaNet.NumTarjetaHabiente = SessionManager.TarjetaTransaccion
                        oETb_Info_TransaccionVisaNet.Mensaje_Error = SessionManager.Cod_Desc_Accion
                        oETb_Info_TransaccionVisaNet.NomTarjeta = SessionManager.NomTarjeta
                        oETb_Info_TransaccionVisaNet.Transaccion_Id = SessionManager.Transaccion_Id
                        If String.IsNullOrEmpty(SessionManager.Cod_Tienda) = False AndAlso String.IsNullOrEmpty(SessionManager.NroOrdenTienda) = False Then
                            If merchantId = SessionManager.Cod_Tienda AndAlso SessionManager.NumOrden = SessionManager.NroOrdenTienda Then
                                SessionManager.IsProcesable = True
                                ' GENERA PAGADO OK
                                objPDAGlobals.ActualizarWebOrdersEstadoId(SessionManager.Id_WebOrders, 6)
                                ValidardProcesodePagos(oETb_Info_TransaccionVisaNet)
                            Else
                                ShowError(Message.EPagoTransaccionPasarela, Message.ECodTiendaNumOrdenDiferecs)
                                Exit Sub
                            End If
                        Else
                            ShowError(Message.EPagoTransaccionPasarela, Message.EVisaNetCodTiendaNumOrdenRetornoNulo)
                            Exit Sub
                        End If
                    ElseIf Not oVisaNetAutorizacion.data Is Nothing AndAlso String.IsNullOrWhiteSpace(oVisaNetAutorizacion.data.TRANSACTION_DATE) = False Then
                        Dim provider As CultureInfo = CultureInfo.InvariantCulture
                        Dim oETb_Info_TransaccionVisaNet As New ETb_Info_TransaccionVisaNet
                        SessionManager.Respuesta = oVisaNetAutorizacion.data.ACTION_CODE
                        SessionManager.Cod_Tienda = oVisaNetAutorizacion.data.MERCHANT
                        SessionManager.NroOrdenTienda = SessionManager.NumOrden
                        SessionManager.Cod_Accion = oVisaNetAutorizacion.data.ACTION_CODE
                        SessionManager.MountVisa = oVisaNetAutorizacion.data.AMOUNT
                        SessionManager.FechaTransaccionVisa = DateTime.ParseExact(oVisaNetAutorizacion.data.TRANSACTION_DATE, "yyMMddHHmmss", provider).ToString("dd/MM/yyyy HH:mm:ss")
                        SessionManager.TarjetaTransaccion = oVisaNetAutorizacion.data.CARD
                        SessionManager.Cod_Desc_Accion = oVisaNetAutorizacion.data.ACTION_DESCRIPTION
                        SessionManager.NomTarjeta = oVisaNetAutorizacion.data.BRAND
                        SessionManager.Transaccion_Id = oVisaNetAutorizacion.data.TRANSACTION_ID
                        oETb_Info_TransaccionVisaNet.Respuesta = SessionManager.Respuesta
                        oETb_Info_TransaccionVisaNet.Nro_Orden = SessionManager.NroOrdenTienda
                        oETb_Info_TransaccionVisaNet.Cod_Accion = SessionManager.Cod_Accion
                        oETb_Info_TransaccionVisaNet.Importe_Autorizado = SessionManager.MountVisa
                        Log.Instance(GetType(respuesta)).LogInformacion(SessionManager.FechaTransaccionVisa)
                        oETb_Info_TransaccionVisaNet.Fecha_Compra = DateTime.Parse(SessionManager.FechaTransaccionVisa)
                        oETb_Info_TransaccionVisaNet.NumTarjetaHabiente = SessionManager.TarjetaTransaccion
                        oETb_Info_TransaccionVisaNet.Mensaje_Error = SessionManager.Cod_Desc_Accion
                        oETb_Info_TransaccionVisaNet.NomTarjeta = SessionManager.NomTarjeta
                        oETb_Info_TransaccionVisaNet.Transaccion_Id = SessionManager.Transaccion_Id
                        If String.IsNullOrEmpty(SessionManager.Cod_Tienda) = False AndAlso String.IsNullOrEmpty(SessionManager.NroOrdenTienda) = False Then
                            If merchantId = SessionManager.Cod_Tienda AndAlso SessionManager.NumOrden = SessionManager.NroOrdenTienda Then
                                SessionManager.IsProcesable = True
                                ValidardProcesodePagos(oETb_Info_TransaccionVisaNet)
                            Else
                                ShowError(Message.EPagoTransaccionPasarela, Message.ECodTiendaNumOrdenDiferecs)
                                Exit Sub
                            End If
                        Else
                            ShowError(Message.EPagoTransaccionPasarela, Message.EVisaNetCodTiendaNumOrdenRetornoNulo)
                            Exit Sub
                        End If
                    Else
                        Dim oPLGlobals As New PLGlobals
                        oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
                        Functions.EliminarAsientobySession()
                        Functions.EliminarAsientosCaducados()
                        ShowError(Message.EPagoTransaccionPasarela, Message.ENuloTramaJsonAutorizacion)
                    End If

                Else
                    Dim oPLGlobals As New PLGlobals
                    oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
                    Functions.EliminarAsientobySession()
                    Functions.EliminarAsientosCaducados()
                    ShowError(Message.EPagoTransaccionPasarela, Message.ETramaJsonAutorizacion)
                    Exit Sub
                End If
            Else
                ShowError(Message.EPagoTransaccionPasarela, Message.ENuloTramaJsonAutorizacion)
                Exit Sub
            End If
        Catch ex As Exception
            Dim oPLGlobals As New PLGlobals
            oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
            Log.Instance(GetType(respuesta)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Sub AnulacionTransaccion()
        Dim merchantId As String = AppSettings.valueString("merchantId")
        Dim urlAnulation As String = (AppSettings.valueString("UrlAPIAnulacion") + merchantId)
        Dim sessionPay As String = SessionManager.VisaNet.SessionPay

        Dim order As String = SessionManager.VisaNet.Orden

        Dim service As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(Order))
        Dim memory As MemoryStream = New MemoryStream(Encoding.UTF8.GetBytes(SessionManager.VisaNet.Orden))
        Dim oOrder As Order = CType(service.ReadObject(memory), Order)
        Dim objOrder As New Order

        objOrder.purchaseNumber = oOrder.purchaseNumber
        objOrder.transactionDate = DateTime.Parse(SessionManager.FechaTransaccionVisa).ToString("yyyyMMdd")

        Dim oAuthorizationRequest As New AuthorizationRequest
        oAuthorizationRequest.order = objOrder

        Dim body As String = JsonHelper.JsonSerializer(Of AuthorizationRequest)(oAuthorizationRequest)
        Log.Instance(GetType(respuesta)).LogInformacion("Request Anulacion: " + body)
        Dim requestAutorization As HttpWebRequest
        requestAutorization = CType(WebRequest.Create(urlAnulation), HttpWebRequest)
        requestAutorization.Method = "POST"
        requestAutorization.ContentType = "application/json"
        requestAutorization.Headers.Add("Authorization", sessionPay)
        Dim writer As StreamWriter
        writer = New StreamWriter(requestAutorization.GetRequestStream())
        writer.Write(body)
        writer.Close()

        Dim response As HttpWebResponse
        Dim reader As StreamReader
        Dim respuestaAnulacion As String = String.Empty
        Dim oPLGlobals As New PLGlobals
        Try
            response = CType(requestAutorization.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())
            If response.StatusCode = HttpStatusCode.OK Then
                respuestaAnulacion = reader.ReadToEnd()
                reader.Close()
            End If
            Log.Instance(GetType(respuesta)).LogInformacion("Response Anulacion: " & respuestaAnulacion)
        Catch ex As WebException
            reader = New StreamReader(ex.Response.GetResponseStream, True)
            respuestaAnulacion = reader.ReadToEnd()
            reader.Close()
            Log.Instance(GetType(respuesta)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
            Log.Instance(GetType(respuesta)).LogInformacion("Response Anulacion Error: " & respuestaAnulacion)
            oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
        End Try


        Try

            If String.IsNullOrEmpty(respuestaAnulacion) = False Then
                Dim oVisaNetAnulacion As AnnulmentResponse
                oVisaNetAnulacion = JsonHelper.JsonDeserialize(Of AnnulmentResponse)(respuestaAnulacion)
                If Not oVisaNetAnulacion Is Nothing Then
                    If Not oVisaNetAnulacion.dataMap Is Nothing AndAlso oVisaNetAnulacion.dataMap.STATUS = "Voided" Then
                        Log.Instance(GetType(respuesta)).LogInformacion("Se anulo la orden en el servicio de VisaNet")
                        Log.Instance(GetType(respuesta)).LogInformacion("Se anulo la orden en el servicio de VisaNet el N° Pedido : " & oOrder.purchaseNumber)

                        oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
                        Functions.EliminarAsientobySession()
                        Functions.EliminarAsientosCaducados()
                    Else
                        Log.Instance(GetType(respuesta)).LogInformacion("No se anulo la orden en el servicio de VisaNet")
                        Log.Instance(GetType(respuesta)).LogInformacion("No se anulo la orden en el servicio de VisaNet el N° Pedido : " & oOrder.purchaseNumber)
                        oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
                        Functions.EliminarAsientobySession()
                        Functions.EliminarAsientosCaducados()
                    End If

                Else
                    Log.Instance(GetType(respuesta)).LogInformacion("No se anulo la orden en el servicio de VisaNet")
                    Log.Instance(GetType(respuesta)).LogInformacion("No se anulo la orden en el servicio de VisaNet el N° Pedido : " & oOrder.purchaseNumber)
                    oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
                    Functions.EliminarAsientobySession()
                    Functions.EliminarAsientosCaducados()
                    Exit Sub
                End If
            Else
                Log.Instance(GetType(respuesta)).LogInformacion("No se anulo la orden en el servicio de VisaNet")
                Log.Instance(GetType(respuesta)).LogInformacion("No se anulo la orden en el servicio de VisaNet el N° Pedido : " & oOrder.purchaseNumber)
                oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
                Functions.EliminarAsientobySession()
                Functions.EliminarAsientosCaducados()
                Exit Sub
            End If
        Catch ex As Exception
            Log.Instance(GetType(respuesta)).LogInformacion("No se anulo la orden en el servicio de VisaNet")
            Log.Instance(GetType(respuesta)).LogInformacion("No se anulo la orden en el servicio de VisaNet el N° Pedido : " & oOrder.purchaseNumber)
            oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
            Functions.EliminarAsientobySession()
            Functions.EliminarAsientosCaducados()
            Log.Instance(GetType(respuesta)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Sub ValidardProcesodePagos(oETb_Info_TransaccionVisaNet As ETb_Info_TransaccionVisaNet)

        Try
            Dim oPLGlobals As New PLGlobals

            If SessionManager.Respuesta = "000" Then
                ''Registrar Transaccion
                ''Inicio
                oETb_Info_TransaccionVisaNet.Id_WebOrders = SessionManager.Id_WebOrders
                oPLGlobals.RegistrarTransaccionVisaNet(oETb_Info_TransaccionVisaNet)
                ''Fin
                Dim oWebOrder As PLGlobals = New PLGlobals()
                Dim oListaWebOrders As ListaWebOrders = oWebOrder.ListarOrdenesInfobyNumOrden(SessionManager.NumOrden)
                If oListaWebOrders.Count > 0 Then

                    If oListaWebOrders.Item(0).Estado_WebOrders <> 7 Then
                        If CDbl(SessionManager.MountOrder) <> CDbl(SessionManager.MountVisa) Then
                            AnulacionTransaccion()
                            objPDAGlobals.ActualizarWebOrdersEstadoId(SessionManager.Id_WebOrders, 5)
                            ErrorTransaccionMonto(SessionManager.NroOrdenTienda, SessionManager.MountOrder, SessionManager.MountVisa)
                            ShowError(Message.EVisaNetMontoNumOrdenDiferecsCliente, Message.EVisaNetMontoNumOrdenDiferecs)
                            Exit Sub
                        Else
                            objPDAGlobals.ActualizarWebOrdersEstadoId(SessionManager.Id_WebOrders, 3)
                            VenderPasaje()
                        End If
                    Else
                        AnulacionTransaccion()
                        objPDAGlobals.ActualizarWebOrdersEstadoId(SessionManager.Id_WebOrders, 11)
                        ShowError(Message.ENunOrdeNoGenerado, Message.ENumOrdeNulo)
                        Exit Sub
                    End If
                Else
                    AnulacionTransaccion()
                    objPDAGlobals.ActualizarWebOrdersEstadoId(SessionManager.Id_WebOrders, 11)
                    ShowError(Message.ENunOrdeNoGenerado, Message.ENumOrdeNulo)
                    Exit Sub
                End If
            Else
                ''Registrar Transaccion
                ''Inicio
                oETb_Info_TransaccionVisaNet.Id_WebOrders = SessionManager.Id_WebOrders
                oPLGlobals.RegistrarTransaccionVisaNet(oETb_Info_TransaccionVisaNet)
                ''Fin

                objPDAGlobals.ActualizarWebOrdersEstadoId(SessionManager.Id_WebOrders, 11)
                oETb_Info_TransaccionVisaNet.Id_WebOrders = SessionManager.Id_WebOrders
                oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
                Functions.EliminarAsientobySession()
                Functions.EliminarAsientosCaducados()
                MostrarErrorTransactional()
            End If
        Catch ex As Exception
            Log.Instance(GetType(respuesta)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Sub VenderPasaje()
        Try
            Dim oPLGlobals As New PLGlobals
            Dim Rst As String = SaveVentaWeb()
            If Rst.ToUpper = "OK" Then
                smCompletTransaction(SessionManager.NroOrdenTienda, Now().ToString)
                If SessionManager.Id_VentaWebIda.Length > 0 OrElse SessionManager.Id_VentaWebRetorno.Length > 0 Then
                    oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
                    Functions.EliminarAsientobySession()
                    Functions.EliminarAsientosCaducados()
                    Functions.ClearPostTransaccion()
                    SessionManager.SelectionOKTransaction = True
                    Response.Redirect("detallecompra.aspx", False)
                    Exit Sub
                End If
            Else
                If Rst <> "OK" Then
                    AnulacionTransaccion()
                    oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
                    Functions.EliminarAsientobySession()
                    Functions.EliminarAsientosCaducados()
                    Rst = Message.EPagoTransaccionSystem
                End If
                ShowError(Rst, Rst)
            End If

        Catch ex As Exception
            Log.Instance(GetType(respuesta)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub
    Private Function SaveVentaWeb() As String

        Try

            Dim Notes As String = "<b>Usuario:</b> " & SessionManager.Name & "; <b>Identificado:</b> " & User.Identity.IsAuthenticated.ToString _
                    & ", <b>IP:</b> " & Util.IpUser(Me) & ", <b>Pais:</b> " & Util.PaisUser(Me) _
                    & ", <b>Nota:</b> Esta orden fue procesada por el formulario de pagos."
            Dim oVentas As PLGlobals = New PLGlobals()
            Dim respuesta As String = String.Empty
            Dim FlagFactElectronica As String = AppSettings.valueString("FactElectronica").ToUpper
            Dim ListaVentaAnuladoIda As New ListaVenta
            Dim ListaVentaAnuladoRetorno As New ListaVenta
            Dim ListaAsientos As New ListaAsientos

            If SessionManager.ViajeRetorno = True Then
                If FlagFactElectronica = "TRUE" Then
                    respuesta = oVentas.RegistroVentaWeb(SessionManager.Id_WebOrders, Notes, True, ListaAsientos)
                    Dim ListaAsientoIda As New List(Of EAsiento)
                    ListaAsientoIda = ListaAsientos.Where(Function(i) i.OrderVenta = 1).ToList()

                    Dim ListaAsientoRetorno As New List(Of EAsiento)
                    ListaAsientoRetorno = ListaAsientos.Where(Function(i) i.OrderVenta = 2).ToList()

                    SessionManager.ListaAsientoWebIda = New ListaAsientos()
                    SessionManager.ListaAsientoWebRetorno = New ListaAsientos()
                    If Not ListaAsientoIda Is Nothing AndAlso ListaAsientoIda.Count > 0 Then
                        SessionManager.Id_VentaWebIda = ListaAsientoIda.Item(0).Id_VentaWeb
                        For i As Integer = 0 To ListaAsientoIda.Count - 1
                            SessionManager.ListaAsientoWebIda.Add(ListaAsientoIda.Item(i))
                        Next
                    End If
                    If Not ListaAsientoRetorno Is Nothing AndAlso ListaAsientoRetorno.Count > 0 Then
                        SessionManager.Id_VentaWebRetorno = ListaAsientoRetorno.Item(0).Id_VentaWeb
                        For i As Integer = 0 To ListaAsientoRetorno.Count - 1
                            SessionManager.ListaAsientoWebRetorno.Add(ListaAsientoRetorno.Item(i))
                        Next
                    End If
                Else
                    respuesta = oVentas.RegistroVentaWeb(SessionManager.Id_WebOrders, Notes, False, ListaAsientos)
                    Dim ListaAsientoIda As New List(Of EAsiento)
                    ListaAsientoIda = ListaAsientos.Where(Function(i) i.OrderVenta = 1).ToList()

                    Dim ListaAsientoRetorno As New List(Of EAsiento)
                    ListaAsientoRetorno = ListaAsientos.Where(Function(i) i.OrderVenta = 2).ToList()

                    SessionManager.ListaAsientoWebIda = New ListaAsientos()
                    SessionManager.ListaAsientoWebRetorno = New ListaAsientos()
                    If Not ListaAsientoIda Is Nothing AndAlso ListaAsientoIda.Count > 0 Then
                        SessionManager.Id_VentaWebIda = ListaAsientoIda.Item(0).Id_VentaWeb
                        For i As Integer = 0 To ListaAsientoIda.Count - 1
                            SessionManager.ListaAsientoWebIda.Add(ListaAsientoIda.Item(i))
                        Next
                    End If
                    If Not ListaAsientoRetorno Is Nothing AndAlso ListaAsientoRetorno.Count > 0 Then
                        SessionManager.Id_VentaWebRetorno = ListaAsientoRetorno.Item(0).Id_VentaWeb
                        For i As Integer = 0 To ListaAsientoRetorno.Count - 1
                            SessionManager.ListaAsientoWebRetorno.Add(ListaAsientoRetorno.Item(i))
                        Next
                    End If
                End If
            Else
                If FlagFactElectronica = "TRUE" Then
                    respuesta = oVentas.RegistroVentaWeb(SessionManager.Id_WebOrders, Notes, True, ListaAsientos)
                    Dim ListaAsientoIda As New List(Of EAsiento)
                    ListaAsientoIda = ListaAsientos.Where(Function(i) i.OrderVenta = 1).ToList()

                    SessionManager.ListaAsientoWebIda = New ListaAsientos()
                    If Not ListaAsientoIda Is Nothing AndAlso ListaAsientoIda.Count > 0 Then
                        SessionManager.Id_VentaWebIda = ListaAsientoIda.Item(0).Id_VentaWeb
                        For i As Integer = 0 To ListaAsientoIda.Count - 1
                            SessionManager.ListaAsientoWebIda.Add(ListaAsientoIda.Item(i))
                        Next
                    End If

                Else
                    respuesta = oVentas.RegistroVentaWeb(SessionManager.Id_WebOrders, Notes, False, ListaAsientos)
                    Dim ListaAsientoIda As New List(Of EAsiento)
                    ListaAsientoIda = ListaAsientos.Where(Function(i) i.OrderVenta = 1).ToList()

                    SessionManager.ListaAsientoWebIda = New ListaAsientos()
                    If Not ListaAsientoIda Is Nothing AndAlso ListaAsientoIda.Count > 0 Then
                        SessionManager.Id_VentaWebIda = ListaAsientoIda.Item(0).Id_VentaWeb
                        For i As Integer = 0 To ListaAsientoIda.Count - 1
                            SessionManager.ListaAsientoWebIda.Add(ListaAsientoIda.Item(i))
                        Next
                    End If
                End If

            End If
            SessionManager.Cliente.ListaAsientosIda = SessionManager.ListaAsientoWebIda
            SessionManager.Cliente.ListaAsientosRetorno = SessionManager.ListaAsientoWebRetorno
            If respuesta <> "OK" Then
                respuesta = Message.ERegisterVentaNulo
                Log.Instance(GetType(respuesta)).LogError("Se anularon los boletos grabados")
                SendersMail.NotificacionErrorSale(SessionManager.NumOrden, SessionManager.CantidadAsientos, SessionManager.CorreoCliente)
            End If
            Return respuesta
        Catch ex As Exception
            Log.Instance(GetType(respuesta)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Function

    Private Sub ShowError(ByVal message As String, ByVal messagesystem As String)

        Try
            If String.IsNullOrEmpty(message) = False Then
                If String.IsNullOrEmpty(SessionManager.Cod_Accion) = False Then
                    message += "<br/><b>Nº Order:</b> " & SessionManager.NumOrden
                    message += "<br/><b>Fecha Transaccion:</b> " & SessionManager.FechaTransaccionVisa

                End If
                DivErrsave.InnerHtml = message
                RegistroVisorErrores(messagesystem)
                Functions.ClearPostTransaccion()
            End If
        Catch ex As Exception
            Log.Instance(GetType(respuesta)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Sub MostrarErrorTransactional()

        Try
            Dim oPLGlobals As New PLGlobals
            Dim respuesta As String = String.Empty

            If String.IsNullOrEmpty(SessionManager.Cod_Desc_Accion) = False Then
                respuesta = SessionManager.Cod_Desc_Accion
            Else
                respuesta = oPLGlobals.ObtenerTransactionPay(SessionManager.ProveedorPasarela, SessionManager.Cod_Accion, SessionManager.NroOrdenTienda)
            End If
            If String.IsNullOrEmpty(respuesta) = False Then
                smErrosTransaction()
            Else
                respuesta = "Error Desconocido, por favor comuniquese con el administrador y con el banco proveedor de su tarjeta"
                ErrorSender("Error al mostrar el tipo de error de la venta al usuario")
            End If
            ShowError(respuesta, respuesta)
        Catch ex As Exception
            Log.Instance(GetType(respuesta)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub RegistroVisorErrores(ByVal message As String)


        Try
            Dim oPLGlobals As New PLGlobals
            oPLGlobals.RegistraErroresTransaccionSales(message, Now, SessionManager.IdUser, SessionManager.NumOrden, 1)
        Catch ex As Exception
            Log.Instance(GetType(respuesta)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub


    Private Sub ProcesarVentaWebLocal()

        Dim eTicket As String = "jbscjkbasjbaj"
        SessionManager.ETicket = eTicket
        Dim codTienda As String = AppSettings.valueString("merchantId")
        Try
            If String.IsNullOrEmpty(eTicket) = False Then
                If eTicket = SessionManager.ETicket Then
                    Dim xmlReq As String = "jbscajcab"
                    If String.IsNullOrEmpty(xmlReq) = False Then
                        Dim xmlRes As String = "ksdknlbdsnkl"
                        ''Response.Write(xmlRes.ToString)
                        'Dim xmlDocument As New XmlDocument()
                        'xmlDocument.LoadXml(xmlRes)
                        If String.IsNullOrEmpty(xmlRes) = False Then
                            Dim oETb_Info_TransaccionVisaNet As New ETb_Info_TransaccionVisaNet
                            SessionManager.Respuesta = "000"
                            SessionManager.Cod_Tienda = "522591303"
                            SessionManager.NroOrdenTienda = SessionManager.NumOrden
                            SessionManager.Cod_Accion = "956"
                            SessionManager.FechaTransaccionVisa = Now.ToShortDateString & " " & Now.ToShortTimeString
                            SessionManager.TarjetaTransaccion = "16461****41664646"
                            SessionManager.MountVisa = SessionManager.MountOrder
                            SessionManager.Cod_Desc_Accion = "Se cancelo el pago"
                            oETb_Info_TransaccionVisaNet.Respuesta = SessionManager.Respuesta
                            oETb_Info_TransaccionVisaNet.Nro_Orden = SessionManager.NroOrdenTienda
                            oETb_Info_TransaccionVisaNet.Cod_Accion = SessionManager.Cod_Accion
                            oETb_Info_TransaccionVisaNet.Importe_Autorizado = SessionManager.MountVisa
                            oETb_Info_TransaccionVisaNet.Fecha_Compra = DateTime.Parse(SessionManager.FechaTransaccionVisa)
                            oETb_Info_TransaccionVisaNet.NumTarjetaHabiente = SessionManager.TarjetaTransaccion
                            oETb_Info_TransaccionVisaNet.Mensaje_Error = SessionManager.Cod_Desc_Accion
                            If String.IsNullOrEmpty(SessionManager.Cod_Tienda) = False AndAlso String.IsNullOrEmpty(SessionManager.NroOrdenTienda) = False Then
                                If codTienda = SessionManager.Cod_Tienda AndAlso SessionManager.NumOrden = SessionManager.NroOrdenTienda Then
                                    SessionManager.IsProcesable = True
                                    ' GENERA PAGADO OK
                                    objPDAGlobals.ActualizarWebOrdersEstadoId(SessionManager.Id_WebOrders, 6)
                                    ValidardProcesodePagos(oETb_Info_TransaccionVisaNet)
                                Else
                                    ShowError(Message.EPagoTransaccionPasarela, Message.ECodTiendaNumOrdenDiferecs)
                                    Exit Sub
                                End If
                            Else
                                ShowError(Message.EPagoTransaccionPasarela, Message.EVisaNetCodTiendaNumOrdenRetornoNulo)
                                Exit Sub
                            End If
                        Else
                            ShowError(Message.EPagoTransaccionPasarela, Message.EXMLRespondQueryWSVisaNetNulo)
                            Exit Sub
                        End If

                    Else
                        ShowError(Message.EPagoTransaccionSystem, Message.EXMLRequestQueryWSVisaNetNulo)
                        Exit Sub
                    End If
                Else
                    ShowError(Message.EPagoTransaccionPasarela, Message.EEtcketDiferecs)
                    Exit Sub
                End If

            Else
                ShowError(Message.EPagoTransaccionPasarela, Message.ERequesETicketNuloFormulario)
                Exit Sub
            End If
        Catch ex As Exception
            Log.Instance(GetType(respuesta)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

#End Region

















End Class

