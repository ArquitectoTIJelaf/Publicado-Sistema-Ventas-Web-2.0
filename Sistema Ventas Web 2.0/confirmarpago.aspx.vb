Imports System.Data
Imports PLogic
Imports PEntity
Imports System.Collections.Generic
Imports PEncry
Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports PUtilitario
Imports System.Runtime.Serialization.Json
Imports PDataAccess

Partial Class confirmarpago
    Inherits System.Web.UI.Page

#Region "Metodos Privados"
    Public Shared Function ObtenerIP() As String
        Try
            Dim Url As String = "http://bot.whatismyipaddress.com/"
            Dim client As New WebClient
            Return client.DownloadString(Url)
        Catch ex As Exception
            Return "127.0.0.1"
        End Try
    End Function
    Private Sub PermisosObjetos()

        Try
            If SessionManager.ViajeRetorno Then
                dataitinerarioretornotitle.Visible = True
                dataitinerarioretornohead.Visible = True
                'dataitinerarioretorno.Visible = True
                'dataitinerarioidatitle.Visible = True
                dataitinerarioidahead.Visible = True
                dataitinerarioida.Visible = True
                'lbldatosida.Text = Message.TitleRutaIda
                lbldatosretorno.Text = Message.TitleRutaRetorno

            Else
                dataitinerarioretornotitle.Visible = False
                dataitinerarioretornohead.Visible = False
                'dataitinerarioretorno.Visible = False
                'dataitinerarioidatitle.Visible = True
                dataitinerarioidahead.Visible = True
                dataitinerarioida.Visible = True
                'lbldatosida.Text = Message.TitleRuta
                lbldatosretorno.Text = String.Empty

            End If
        Catch ex As Exception
            Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Public Sub CargarInformaciondeViajes()

        Try
            If SessionManager.ViajeRetorno Then
                DetalleViajeIda()
                DetalleViajeRetorno()
            ElseIf SessionManager.ViajeRetorno = False Then
                DetalleViajeIda()
            End If
        Catch ex As Exception
            Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub DetalleViajeIda()

        Try
            lblrutaida.Text = SessionManager.Origen_Ida & "-" & SessionManager.Destino_Ida
            lblfechaviajeida.Text = SessionManager.FechaDeViajeIda
            lblhorasalidaida.Text = SessionManager.HoraViajeIda
            lblhorallegadaida.Text = SessionManager.HoraLlegadaViajeIda
            lblservicioida.Text = SessionManager.ServicioIda
            lblmontosolesida.Text = Format(SessionManager.MontoIda, "0.00")

            ''Datos Mobile

            ''IDA


            lblrutaidamobile.Text = SessionManager.Origen_Ida & " - " & SessionManager.Destino_Ida
            lblfechaviajeidamobile.Text = SessionManager.FechaDeViajeIda
            lblhorasalidaidamobile.Text = SessionManager.HoraViajeIda
            lblhorallegadaidamobile.Text = SessionManager.HoraLlegadaViajeIda
            lblservicioidamobile.Text = SessionManager.ServicioIda
            lblmontosolesidamobile.Text = Format(SessionManager.MontoIda, "0.00")

            lblrutaidamobile.Text = "IDA: " & SessionManager.Origen_Ida & "-" & SessionManager.Destino_Ida
            lblfechaviajeidamobile.Text = DateTime.Parse(SessionManager.FechaDeViajeIda).ToString("dddd") & " " & SessionManager.FechaDeViajeIda
        Catch ex As Exception
            Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub DetalleViajeRetorno()

        Try
            lblrutaretorno.Text = SessionManager.Origen_Retorno & "-" & SessionManager.Destino_Retorno
            lblfechaviajeretorno.Text = SessionManager.FechaDeViajeRetorno
            lblhorasalidaretorno.Text = SessionManager.HoraViajeRetorno
            lblhorallegadaretorno.Text = SessionManager.HoraLlegadaViajeRetorno
            lblservicioretorno.Text = SessionManager.ServicioRetorno
            lblmontosolesretorno.Text = Format(SessionManager.MontoRetorno, "0.00")

            ''Datos Mobile
            lblrutaretornomobile.Text = SessionManager.Origen_Retorno & " - " & SessionManager.Destino_Retorno
            lblfechaviajeretornomobile.Text = SessionManager.FechaDeViajeRetorno
            lblhorasalidaretornomobile.Text = SessionManager.HoraViajeRetorno
            lblhorallegadaretornomobile.Text = SessionManager.HoraLlegadaViajeRetorno
            lblservicioretornomobile.Text = SessionManager.ServicioRetorno
            lblmontosolesretornomobile.Text = Format(SessionManager.MontoRetorno, "0.00")
            ''RETORNO
            lblrutaretornomobile.Text = "RET: " & SessionManager.Origen_Retorno & "-" & SessionManager.Destino_Retorno
            lblfechaviajeretornomobile.Text = DateTime.Parse(SessionManager.FechaDeViajeRetorno).ToString("dddd") & " " & SessionManager.FechaDeViajeRetorno

        Catch ex As Exception
            Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub



    Private Sub OrderGenerate(ByVal Type As Integer)

        Try
            Dim typetransacion As String = "T"
            If SelectionPasarela(typetransacion) Then
                If GenerarOrden(Type) Then
                    If typetransacion = "T" Then
                        btncontinuar.Enabled = False
                        Functions.ClearPreviewTransaccion()
                        SessionManager.SelectionPago = True

                        Dim oEUsers As New EUsers
                        oEUsers.Correo_Users = SessionManager.CorreoCliente
                        oEUsers.ListaAsientosIda = SessionManager.ListaAsientoWebIda
                        oEUsers.ListaAsientosRetorno = SessionManager.ListaAsientoWebRetorno
                        SessionManager.Cliente = oEUsers

                        Dim fechareservavence As String = String.Empty
                        Dim horareservavence As String = String.Empty
                        Dim FechaReserva As DateTime
                        Dim TimeExpirationSafetypay As Integer = CInt(AppSettings.valueString("TiempoSession"))
                        FechaReserva = DateAdd(DateInterval.Minute, CInt(TimeExpirationSafetypay), Now)
                        fechareservavence = FechaReserva.ToString("dd/MM/yyyy")
                        horareservavence = FechaReserva.ToString("hh:mm tt").Replace(".", "").Replace(" ", "").ToUpper

                        Dim respuesta As String = RegistrarReserva(fechareservavence, horareservavence)
                        If respuesta = "OK" Then
                            Functions.LiberarAsientobySession()
                            Log.Instance(GetType(confirmarpago)).LogError("Se registro la reserva temporal")
                        Else
                            Dim oPLGlobals As New PLGlobals
                            oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
                            Log.Instance(GetType(confirmarpago)).LogError("No se registro la reserva temporal")
                            Response.Redirect("destinos.aspx?reserva=0", False)
                        End If
                        Exit Sub
                    Else
                        btncontinuar.Enabled = False
                        Functions.ClearPreviewTransaccion()
                        Dim oPLGlobals As New PLGlobals
                        Dim oETb_Hora_Reserva_Web As ETb_Hora_Reserva_Web = oPLGlobals.TiempoReservaWeb()
                        If oETb_Hora_Reserva_Web Is Nothing OrElse (oETb_Hora_Reserva_Web.tiempo_reserva_web < 1 OrElse oETb_Hora_Reserva_Web.tiempo_reserva_web_Prev < 1 OrElse oETb_Hora_Reserva_Web.Hora_Inicio_Act Is Nothing OrElse oETb_Hora_Reserva_Web.Hora_Max_Act Is Nothing) Then
                            ScriptUser.JQueryMensaje(Me, "La forma de pago presencial no se encuentran configuradas.", 1)
                            Exit Sub
                        End If
                        Dim fechareservavence As String = String.Empty
                        Dim horareservavence As String = String.Empty

                        If DateDiff(DateInterval.Day, CDate(Now.ToShortDateString), CDate(SessionManager.FechaDeViajeIda)) = 1 Then
                            If DateDiff(DateInterval.Minute, CDate(Now.ToShortTimeString), CDate(oETb_Hora_Reserva_Web.Hora_Max_Act)) > 0 Then
                                fechareservavence = Now.ToShortDateString
                                horareservavence = DateAdd(DateInterval.Minute, oETb_Hora_Reserva_Web.tiempo_reserva_web, CDate(Now.ToShortTimeString)).ToString("hh:mm tt").Replace(".", "").Replace(" ", "").ToUpper
                            Else
                                fechareservavence = DateAdd(DateInterval.Day, 1, CDate(Now.ToShortDateString)).ToString
                                horareservavence = DateAdd(DateInterval.Minute, oETb_Hora_Reserva_Web.tiempo_reserva_web, CDate(oETb_Hora_Reserva_Web.Hora_Inicio_Act)).ToString("hh:mm tt").Replace(".", "").Replace(" ", "").ToUpper
                                If DateDiff(DateInterval.Minute, CDate(horareservavence), CDate(SessionManager.HoraViajeIda)) < 0 Then
                                    ScriptUser.JQueryMensaje(Me, "No se puede realizar un pago presencial al turno de la fecha seleccionada. Intente con otro turno y/o fecha de viaje.", 1)
                                    Exit Sub
                                End If

                            End If
                        ElseIf DateDiff(DateInterval.Day, CDate(Now.ToShortDateString), CDate(SessionManager.FechaDeViajeIda)) > 1 Then
                            fechareservavence = DateAdd(DateInterval.Day, 1, CDate(Now.ToShortDateString)).ToString
                            ''horareservavence = DateAdd(DateInterval.Minute, oETb_Hora_Reserva_Web.tiempo_reserva_web_Prev, CDate(oETb_Hora_Reserva_Web.Hora_Inicio_Act)).ToString("hh:mm tt").Replace(".", "").Replace(" ", "").ToUpper
                            horareservavence = DateAdd(DateInterval.Minute, oETb_Hora_Reserva_Web.tiempo_reserva_web_Prev, CDate(fechareservavence)).ToString("hh:mm tt").Replace(".", "").Replace(" ", "").ToUpper

                        Else

                            ScriptUser.JQueryMensaje(Me, "La forma de pago presencial no se encuentran configuradas.", 1)
                            Exit Sub

                        End If
                        ''Dim tiempo_reserva As Integer = oETb_Hora_Reserva_Web.tiempo_reserva_web
                        ''Dim fechahorareserva As DateTime = Util.FormatTimeLong(Now.Year, Now.Month, Now.Day, Now.Hour, Now.Minute, 0)
                        ''Dim fechahorareservavence As DateTime = DateAdd(DateInterval.Minute, tiempo_reserva, fechahorareserva)
                        ''Dim horareservavence As DateTime = fechahorareservavence
                        ''Dim horareservavence As String = fechahorareservavence.ToString("hh:mm tt").Replace(".", "").Replace(" ", "").ToUpper
                        SessionManager.Reservacion = True
                        RegistrarReserva(fechareservavence, horareservavence)
                        Functions.LiberarAsientobySession()
                    End If
                End If


            End If

        Catch ex As Exception
            Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Function GenerarOrden(ByVal Type As Integer) As Boolean

        Try
            Dim oPLGlobals As New PLGlobals()
            Dim OrderSave As Boolean = False
            If Functions.ValidoMyOrders Then
                Dim estado As Boolean = True
                Dim respuesta As String = String.Empty
                SessionManager.ListaAsientosbyOrderIda = Nothing
                SessionManager.ListaProgramacionIda = Nothing
                SessionManager.ListaAsientosbyOrderRetorno = Nothing
                SessionManager.ListaProgramacionRetorno = Nothing

                If SessionManager.ViajeRetorno = True Then

                    If SessionManager.ListaAsientoWebIda.Count < 1 Then
                        ScriptUser.JQueryMensaje(Me, Message.EAsientoEliminadosViajeIda, 0)
                    End If
                    If SessionManager.ListaAsientoWebRetorno.Count < 1 Then
                        ScriptUser.JQueryMensaje(Me, Message.EAsientoEliminadosViajeRetorno, 0)
                    End If

                    ''If SessionManager.ListaAsientoWebIda.Count > 0 Then
                    ''    If oPLGlobals.FormatearPreciosAsientos(respuesta, SessionManager.Cod_ProgramacionIda, SessionManager.ListaAsientoWebIda, SessionManager.ListaProgramacionIda, SessionManager.RutaConcatenadaIda, SessionManager.OrigenId_Ida, SessionManager.DestinoId_Ida, SessionManager.IdPrecioNacionalidadIda, SessionManager.Codi_Sucursal_ida, SessionManager.Codi_Destino_Ida, SessionManager.ServicioIdIda, SessionManager.IdEmpresaIda, SessionManager.IdBusIda, CDate(SessionManager.Fecha_Programacion_Ida), SessionManager.Hora_Programacion_Ida, SessionManager.Nro_ViajeIda, SessionManager.HoraViajeIda, SessionManager.FechaDeViajeIda, SessionManager.TransactionPromociones) = False Then
                    ''        Functions.NotificarErrorConfiguracionSistema(respuesta)
                    ''        ScriptUser.JQueryMensaje(Me, Message.WNoVender, 1)
                    ''        OrderSave = False
                    ''        estado = False
                    ''        Exit Function
                    ''    End If
                    ''Else

                    ''    ScriptUser.JQueryMensaje(Me, Message.EAsientoEliminadosViajeIda, 0)
                    ''End If
                    ''If SessionManager.ListaAsientoWebRetorno.Count > 0 Then
                    ''    If oPLGlobals.FormatearPreciosAsientos(respuesta, SessionManager.Cod_ProgramacionRetorno, SessionManager.ListaAsientoWebRetorno, SessionManager.ListaProgramacionRetorno, SessionManager.RutaConcatenadaRetorno, SessionManager.OrigenId_Retorno, SessionManager.DestinoId_Retorno, SessionManager.IdPrecioNacionalidadRetorno, SessionManager.Codi_Sucursal_Retorno, SessionManager.Codi_Destino_Retorno, SessionManager.ServicioIdRetorno, SessionManager.IdEmpresaRetorno, SessionManager.IdBusRetorno, CDate(SessionManager.Fecha_Programacion_Retorno), SessionManager.Hora_Programacion_Retorno, SessionManager.Nro_ViajeRetorno, SessionManager.HoraViajeRetorno, SessionManager.FechaDeViajeRetorno, SessionManager.TransactionPromociones) = False Then
                    ''        Functions.NotificarErrorConfiguracionSistema(respuesta)
                    ''        ScriptUser.JQueryMensaje(Me, Message.WNoVender, 1)
                    ''        OrderSave = False
                    ''        estado = False
                    ''        Exit Function
                    ''    End If
                    ''Else
                    ''    ScriptUser.JQueryMensaje(Me, Message.EAsientoEliminadosViajeRetorno, 0)
                    ''End If
                Else
                    ''SessionManager.ListaAsientosbyOrderIda = oPLGlobals.ListarAsientosbyIDCID( SessionManager.Id_WebOrders, SessionManager.IdUser, "1" & SessionManager.IDCID, SessionManager.IdBusIda)
                    If SessionManager.ListaAsientoWebIda.Count < 1 Then
                        ScriptUser.JQueryMensaje(Me, Message.EAsientoEliminadosViajeIda, 0)
                    End If
                    ''If SessionManager.ListaAsientoWebIda.Count > 0 Then
                    ''    If oPLGlobals.FormatearPreciosAsientos(respuesta, SessionManager.Cod_ProgramacionIda, SessionManager.ListaAsientoWebIda, SessionManager.ListaProgramacionIda, SessionManager.RutaConcatenadaIda, SessionManager.OrigenId_Ida, SessionManager.DestinoId_Ida, SessionManager.IdPrecioNacionalidadIda, SessionManager.Codi_Sucursal_ida, SessionManager.Codi_Destino_Ida, SessionManager.ServicioIdIda, SessionManager.IdEmpresaIda, SessionManager.IdBusIda, CDate(SessionManager.Fecha_Programacion_Ida), SessionManager.Hora_Programacion_Ida, SessionManager.Nro_ViajeIda, SessionManager.HoraViajeIda, SessionManager.FechaDeViajeIda, SessionManager.TransactionPromociones) = False Then
                    ''        Functions.NotificarErrorConfiguracionSistema(respuesta)
                    ''        ScriptUser.JQueryMensaje(Me, Message.WNoVender, 1)
                    ''        estado = False
                    ''        Exit Function
                    ''    End If
                    ''Else
                    ''    ScriptUser.JQueryMensaje(Me, Message.EAsientoEliminadosViaje, 0)
                    ''End If
                End If
                Dim Rst As String = oPLGlobals.GenerarNumOrden(SessionManager.IDCID, SessionManager.IdUser, Util.IpUser(Me), Util.HostUser(Me), Mid(Util.HeadVars(Me, "HTTP_ACCEPT_LANGUAGE"), 1, 5), 1, SessionManager.CorreoCliente)
                If Rst.Substring(0, 2) = "OK" Then
                    OrderSave = True
                    Dim arraydata() As String = Rst.Split("|")
                    SessionManager.NumOrden = arraydata(0).Substring(2, 9).Trim
                    SessionManager.Id_WebOrders = CInt(arraydata(1).Trim.ToString)
                    SessionManager.MountOrder = (SessionManager.MontoIda + SessionManager.MontoRetorno).ToString("#.00")
                    smCreateOrderConfirm(SessionManager.NumOrden, SessionManager.CantidadAsientos)
                    ''SessionManager.Type = Type

                Else
                    OrderSave = False
                End If

            Else
                ScriptUser.JQueryMensaje(Me, Message.WAsientoEliminados, 1)
            End If

            Return OrderSave
        Catch ex As Exception
            Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try


    End Function

    Private Function SelectionPasarela(ByRef typetransacion As String) As Boolean

        Try
            Dim estado As Boolean = False

            If String.IsNullOrEmpty(hivcomision.Value) = False AndAlso String.IsNullOrEmpty(hivflagventa.Value) = False AndAlso String.IsNullOrEmpty(hivproveedor.Value) = False AndAlso String.IsNullOrEmpty(hivformapago.Value) = False Then
                SessionManager.ProveedorPasarela = hivproveedor.Value
                SessionManager.FormaPagoWeb = hivformapago.Value
                estado = True
                typetransacion = hivflagventa.Value
            Else
                ScriptUser.JQueryMensaje(Me, Message.WSelectFormaPago)
            End If

            For i As Int16 = 0 To SessionManager.ListaAsientoWebIda.Count - 1

                SessionManager.ListaAsientoWebIda.Item(i).Flag_Venta = typetransacion
                SessionManager.ListaAsientoWebIda.Item(i).Comision_Web = Val(hivcomision.Value)
                SessionManager.ListaAsientoWebIda.Item(i).ProveedorWeb = Val(hivproveedor.Value)
                ''Agregar Datos de Empresa---Facturacion Electronica
                SessionManager.ListaAsientoWebIda.Item(i).Codi_Empresa = SessionManager.IdEmpresaIda
                SessionManager.ListaAsientoWebIda.Item(i).Ruc_Empresa = SessionManager.RUCEmpresaIda
                SessionManager.ListaAsientoWebIda.Item(i).UserWebSUNAT = AppSettings.valueString("UserSUNATWS")
                SessionManager.ListaAsientoWebIda.Item(i).DescripcionCosto = Util.Convertir(SessionManager.ListaAsientoWebIda.Item(i).Costo, True)
                SessionManager.ListaAsientoWebIda.Item(i).Origen = SessionManager.Origen_Ida
                SessionManager.ListaAsientoWebIda.Item(i).Destino = SessionManager.Destino_Ida
                SessionManager.ListaAsientoWebIda.Item(i).FechaViaje = SessionManager.FechaDeViajeIda
                SessionManager.ListaAsientoWebIda.Item(i).HoraViaje = SessionManager.HoraViajeIda
                If typetransacion <> "T" Then
                    SessionManager.ListaAsientoWebIda.Item(i).Vale_Remoto = "1"
                End If

            Next

            If SessionManager.ViajeRetorno Then

                For i As Int16 = 0 To SessionManager.ListaAsientoWebRetorno.Count - 1
                    SessionManager.ListaAsientoWebRetorno.Item(i).Flag_Venta = typetransacion
                    SessionManager.ListaAsientoWebRetorno.Item(i).Comision_Web = Val(hivcomision.Value)
                    SessionManager.ListaAsientoWebRetorno.Item(i).ProveedorWeb = Val(hivproveedor.Value)
                    ''Agregar Datos de Empresa---Facturacion Electronica
                    SessionManager.ListaAsientoWebRetorno.Item(i).Codi_Empresa = SessionManager.IdEmpresaRetorno
                    SessionManager.ListaAsientoWebRetorno.Item(i).Ruc_Empresa = SessionManager.RUCEmpresaRetorno
                    SessionManager.ListaAsientoWebRetorno.Item(i).UserWebSUNAT = AppSettings.valueString("UserSUNATWS")
                    SessionManager.ListaAsientoWebRetorno.Item(i).DescripcionCosto = Util.Convertir(SessionManager.ListaAsientoWebRetorno.Item(i).Costo, True)
                    SessionManager.ListaAsientoWebRetorno.Item(i).Origen = SessionManager.Origen_Retorno
                    SessionManager.ListaAsientoWebRetorno.Item(i).Destino = SessionManager.Destino_Retorno
                    SessionManager.ListaAsientoWebRetorno.Item(i).FechaViaje = SessionManager.FechaDeViajeRetorno
                    SessionManager.ListaAsientoWebRetorno.Item(i).HoraViaje = SessionManager.HoraViajeRetorno
                    If typetransacion <> "T" Then
                        SessionManager.ListaAsientoWebRetorno.Item(i).Vale_Remoto = "1"
                    End If
                Next

            End If
            Return estado
        Catch ex As Exception
            Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function

    Private Function RegistrarReserva(ByVal fechareservavence As String, ByVal horavencereserva As String) As String
        Try
            Dim respuestaida, respuestaretorno, respuesta As String
            Dim Notes As String = "<b>Usuario:</b> " & SessionManager.Name & "; <b>Identificado:</b> " & User.Identity.IsAuthenticated.ToString _
        & ", <b>IP:</b> " & Util.IpUser(Me) & ", <b>Pais:</b> " & Util.PaisUser(Me) _
        & ", <br/><b>Nota:</b> Esta orden fue procesado por el formulario de pagos."
            Dim oVentas As PLGlobals = New PLGlobals()
            If SessionManager.ViajeRetorno = True Then
                Dim ProgramacionIda As New ETb_Programacion
                Dim ProgramacionRetorno As New ETb_Programacion
                ProgramacionIda.NroViaje = SessionManager.Nro_ViajeIda
                ProgramacionIda.Fech_programacion = SessionManager.Fecha_Programacion_Ida
                ProgramacionIda.Codi_Empresa = SessionManager.IdEmpresaIda
                ProgramacionIda.Codi_Origen = SessionManager.Codi_Sucursal_ida
                ProgramacionIda.Codi_Destino = SessionManager.Codi_Destino_Ida
                ProgramacionIda.OrigenId = SessionManager.OrigenId_Ida
                ProgramacionIda.DestinoId = SessionManager.DestinoId_Ida
                ProgramacionIda.Codi_Servicio = SessionManager.ServicioIdIda
                ProgramacionIda.Bus = SessionManager.IdBusIda
                ProgramacionIda.Hora_Programacion = SessionManager.Hora_Programacion_Ida
                ProgramacionIda.HoraViaje = SessionManager.HoraViajeIda
                ProgramacionIda.FechaViaje = SessionManager.FechaDeViajeIda


                ProgramacionRetorno.NroViaje = SessionManager.Nro_ViajeRetorno
                ProgramacionRetorno.Fech_programacion = SessionManager.Fecha_Programacion_Retorno
                ProgramacionRetorno.Codi_Empresa = SessionManager.IdEmpresaRetorno
                ProgramacionRetorno.Codi_Origen = SessionManager.Codi_Sucursal_Retorno
                ProgramacionRetorno.Codi_Destino = SessionManager.Codi_Destino_Retorno
                ProgramacionRetorno.OrigenId = SessionManager.OrigenId_Retorno
                ProgramacionRetorno.DestinoId = SessionManager.DestinoId_Retorno
                ProgramacionRetorno.Codi_Servicio = SessionManager.ServicioIdRetorno
                ProgramacionRetorno.Bus = SessionManager.IdBusRetorno
                ProgramacionRetorno.Hora_Programacion = SessionManager.Hora_Programacion_Retorno
                ProgramacionRetorno.HoraViaje = SessionManager.HoraViajeRetorno
                ProgramacionRetorno.FechaViaje = SessionManager.FechaDeViajeRetorno



                respuestaida = oVentas.RegistroReservaWeb(SessionManager.Cod_ProgramacionIda, SessionManager.Id_WebOrders, Notes, SessionManager.IdUser, "1" & SessionManager.IDCID, SessionManager.CantidadAsientosIda, SessionManager.CantidadAsientos, 1, SessionManager.MountOrder, ProgramacionIda, SessionManager.ListaAsientoWebIda, "", fechareservavence, horavencereserva)
                respuestaretorno = oVentas.RegistroReservaWeb(SessionManager.Cod_ProgramacionRetorno, SessionManager.Id_WebOrders.ToString, Notes, SessionManager.IdUser, "2" & SessionManager.IDCID, SessionManager.CantidadAsientosRetorno, SessionManager.CantidadAsientos, 2, SessionManager.MountOrder, ProgramacionRetorno, SessionManager.ListaAsientoWebRetorno, SessionManager.NumOrden, fechareservavence, horavencereserva)
                If respuestaida.Substring(0, 2) = "OK" AndAlso respuestaretorno.Substring(0, 2) = "OK" Then
                    respuesta = "OK"
                    SessionManager.Id_VentaWebIda = respuestaida.Substring(2, respuestaretorno.Length - 2)
                    SessionManager.Id_VentaWebRetorno = respuestaretorno.Substring(2, respuestaretorno.Length - 2)
                Else
                    If respuestaida.Length > 1 AndAlso respuestaretorno.Length > 1 Then
                        If respuestaida.Substring(0, 2) = "OK" AndAlso respuestaida.Substring(0, 2) <> "OK" Then
                            respuesta = Message.ERegisterVentaIda
                            SessionManager.Id_VentaWebIda = respuestaida.Substring(2, respuestaida.Length - 3)
                        ElseIf respuestaretorno.Substring(0, 2) = "OK" AndAlso respuestaretorno.Substring(0, 2) <> "OK" Then
                            respuesta = Message.ERegisterVentaRetorno
                            SessionManager.Id_VentaWebRetorno = respuestaretorno.Substring(2, respuestaretorno.Length - 2)
                        Else
                            respuesta = Message.ENoRegisterReserva

                        End If
                    Else
                        respuesta = Message.ENoRegisterReserva

                    End If

                End If
            Else
                Dim ProgramacionIda As New ETb_Programacion
                ProgramacionIda.NroViaje = SessionManager.Nro_ViajeIda
                ProgramacionIda.Fech_programacion = SessionManager.Fecha_Programacion_Ida
                ProgramacionIda.Codi_Empresa = SessionManager.IdEmpresaIda
                ProgramacionIda.Codi_Origen = SessionManager.Codi_Sucursal_ida
                ProgramacionIda.Codi_Destino = SessionManager.Codi_Destino_Ida
                ProgramacionIda.OrigenId = SessionManager.OrigenId_Ida
                ProgramacionIda.DestinoId = SessionManager.DestinoId_Ida
                ProgramacionIda.Codi_Servicio = SessionManager.ServicioIdIda
                ProgramacionIda.Bus = SessionManager.IdBusIda
                ProgramacionIda.Hora_Programacion = SessionManager.Hora_Programacion_Ida
                ProgramacionIda.HoraViaje = SessionManager.HoraViajeIda
                ProgramacionIda.FechaViaje = SessionManager.FechaDeViajeIda
                respuestaida = oVentas.RegistroReservaWeb(SessionManager.Cod_ProgramacionIda, SessionManager.Id_WebOrders, Notes, SessionManager.IdUser, "1" & SessionManager.IDCID, SessionManager.CantidadAsientosIda, SessionManager.CantidadAsientos, 1, SessionManager.MountOrder, ProgramacionIda, SessionManager.ListaAsientoWebIda, "", fechareservavence, horavencereserva)
                If respuestaida.Length > 1 Then
                    If respuestaida.Substring(0, 2) = "OK" Then
                        respuesta = "OK"
                        SessionManager.Id_VentaWebIda = respuestaida.Substring(2, respuestaida.Length - 2)
                    Else
                        respuesta = Message.ENoRegisterReserva

                    End If
                Else
                    respuesta = Message.ENoRegisterReserva
                End If

            End If
            If respuesta <> "OK" Then
                Dim oPLGlobals As New PLGlobals
                oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
                Log.Instance(GetType(confirmarpago)).LogError("No se registro la reserva temporal")
                Response.Redirect("destinos.aspx?reserva=0", False)
            End If
            Return respuesta

        Catch ex As Exception
            Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function

    Private Sub ListarMediosPagos()
        Dim oPLGlobals As New PLGlobals
        If SessionManager.ListarMediosPagosTCredito Is Nothing Then
            SessionManager.ListarMediosPagosTCredito = oPLGlobals.ListarMediosPagos()
        End If

        If AppSettings.valueString("PasarelaMultiTarjeta") = "1" Then
            dlpagostcredito.DataSource = SessionManager.ListarMediosPagosTCredito
            dlpagostcredito.DataBind()
        End If


        If SessionManager.ListarMediosPagosTCredito.Count > 0 Then
            hivflagventa.Value = SessionManager.ListarMediosPagosTCredito.Item(0).Flag_Venta
            hivflagpasarela.Value = SessionManager.ListarMediosPagosTCredito.Item(0).Flag_Pasarela
            hivcomision.Value = SessionManager.ListarMediosPagosTCredito.Item(0).Comision
            hivproveedor.Value = SessionManager.ListarMediosPagosTCredito.Item(0).WebTarjetaPagoTipo_Id
            hivformapago.Value = SessionManager.ListarMediosPagosTCredito.Item(0).Id_FormaPagoWeb
        End If
    End Sub

    Private Sub CreaUsuario()
        Try

            Dim oPLGlobals As New PLGlobals
            Dim oCrypto As New Crypto
            Dim oListaUsers As ListaUsers = oPLGlobals.Logueo_Usuario_Web_Auto(SessionManager.CorreoCliente)
            If oListaUsers.Count > 0 Then
                SessionUser.UserLoaData(oListaUsers)
            Else
                If oPLGlobals.RegistroUsuario_One(SessionManager.CorreoCliente, oCrypto.Encrypt("123456789"), SessionManager.CorreoCliente) Then
                    SessionManager.Name = SessionManager.CorreoCliente
                    Dim Rpta As String = String.Empty
                    Rpta = smNewUser(SessionManager.CorreoCliente, "123456789", SessionManager.CorreoCliente)
                    oListaUsers = oPLGlobals.Logueo_Usuario_Web_Auto(SessionManager.CorreoCliente)
                    SessionUser.UserLoaData(oListaUsers)
                    'Me.User.Identity. = txtCorreo.Text
                    'Exit Sub
                Else
                    'flerror.Text = "Ocurrio un error, por favor comunicarse con el Administrador"
                End If
            End If

            FormsAuthentication.SetAuthCookie(SessionManager.Name, True)


        Catch ex As Exception
            Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try


    End Sub


    Private Sub GenerarTokenLocal()


        Try

            Try

                Dim oVisaNetEntidad As New VisaNetEntidad
                oVisaNetEntidad.CodigoComercio = AppSettings.valueString("merchantId")
                oVisaNetEntidad.Monto = SessionManager.MountOrder
                oVisaNetEntidad.NumeroPedido = SessionManager.NumOrden
                oVisaNetEntidad.Channel = "web"
                oVisaNetEntidad.UrlTimeout = AppSettings.valueString("UrlTimeout")
                oVisaNetEntidad.ExpirationMinutes = AppSettings.valueString("ExpirationTime")
                SessionManager.VisaNet = oVisaNetEntidad
                SessionManager.SelectionTransaction = True
                Response.Redirect("respuesta.aspx")

            Catch ex As WebException
                Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
            End Try

        Catch ex As WebException
            Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub GenerarToken()
        If CInt(SessionManager.MountOrder) < 1 Then
            Log.Instance(GetType(confirmarpago)).LogInformacion("Monto menor a 1 sol")
            Response.Redirect("destinos.aspx?error=0")
        End If
        Dim oPLGlobals As New PLGlobals

        Dim merchantId As String = AppSettings.valueString("merchantId")
        Dim User As String = AppSettings.valueString("User")
        Dim Password As String = AppSettings.valueString("Password")
        Dim Credentials As String = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes((User + (":" + Password))))

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
        ServicePointManager.DefaultConnectionLimit = 9999

        ''Crear Token Seguridad
        Dim urlCreateTokenSecurity As String = AppSettings.valueString("UrlAPITokenSecurity")
        Dim request As HttpWebRequest
        request = CType(WebRequest.Create(urlCreateTokenSecurity), HttpWebRequest)
        request.Method = "POST"
        request.Headers.Add("Authorization", "Basic " + Credentials)
        Dim writer = New StreamWriter(request.GetRequestStream())
        writer.Close()

        Dim responseToken As HttpWebResponse
        Dim reader As StreamReader
        Dim respuesta As String = String.Empty

        Try
            responseToken = CType(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(responseToken.GetResponseStream())
            respuesta = reader.ReadToEnd()
            reader.Close()


            ''Crear Token de Session
            Dim urlCreateTokenSession As String = (AppSettings.valueString("UrlAPITokenSession") & merchantId)
            Dim oMerchantDefineData As New MerchantDefineData
            oMerchantDefineData.MDD1 = AppSettings.valueString("merchantId")
            oMerchantDefineData.MDD2 = AppSettings.valueString("NombreComercio")
            oMerchantDefineData.MDD4 = SessionManager.CorreoCliente
            oMerchantDefineData.MDD6 = "1" ''Horas entre la compra y entrega
            oMerchantDefineData.MDD18 = SessionManager.Origen_Ida.Substring(0, 3) & "-" & SessionManager.Destino_Ida.Substring(0, 3) ''Ruta
            oMerchantDefineData.MDD19 = SessionManager.Origen_Ida.Substring(0, 3) ''Punto de Origen
            oMerchantDefineData.MDD20 = SessionManager.Destino_Ida.Substring(0, 3) ''Punto de Destino
            oMerchantDefineData.MDD30 = SessionManager.CorreoCliente
            oMerchantDefineData.MDD32 = SessionManager.CorreoCliente
            If SessionManager.ListaAsientoWebIda.Item(0).Tipo_Doc = 1 Then
                oMerchantDefineData.MDD63 = "DNI"
            ElseIf SessionManager.ListaAsientoWebIda.Item(0).Tipo_Doc = 4 Then
                oMerchantDefineData.MDD63 = "CEX"
            ElseIf SessionManager.ListaAsientoWebIda.Item(0).Tipo_Doc = 7 Then
                oMerchantDefineData.MDD63 = "PAS"
            End If
            oMerchantDefineData.MDD65 = SessionManager.ListaAsientoWebIda.Item(0).DNI
            oMerchantDefineData.MDD75 = "Registrado"
            oMerchantDefineData.MDD77 = DateDiff(DateInterval.Day, SessionManager.FechaReg_Users, Now).ToString()

            Dim oAntifraud As New Antifraud
            oAntifraud.clientIp = ObtenerIP()
            oAntifraud.merchantDefineData = oMerchantDefineData

            Dim oSesionTokenRequest As New SesionTokenRequest
            oSesionTokenRequest.channel = "web"
            oSesionTokenRequest.amount = SessionManager.MountOrder
            oSesionTokenRequest.recurrenceMaxAmount = 0
            oSesionTokenRequest.antifraud = oAntifraud

            Dim body As String = JsonHelper.JsonSerializer(Of SesionTokenRequest)(oSesionTokenRequest)
            Log.Instance(GetType(confirmarpago)).LogInformacion("Request Token Session: " + body)
            request = CType(WebRequest.Create(urlCreateTokenSession), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.Headers.Add("Authorization", respuesta)
            writer = New StreamWriter(request.GetRequestStream())
            writer.Write(body)
            writer.Close()

            Dim resultado As String = String.Empty
            Try
                responseToken = CType(request.GetResponse(), HttpWebResponse)
                reader = New StreamReader(responseToken.GetResponseStream())
                resultado = reader.ReadToEnd()
                reader.Close()
                Log.Instance(GetType(confirmarpago)).LogInformacion("Response Token Session: " + resultado)
                Dim service As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(SesionTokenResponse))
                Dim memory As MemoryStream = New MemoryStream(Encoding.UTF8.GetBytes(resultado))
                Dim objResultado As SesionTokenResponse = CType(service.ReadObject(memory), SesionTokenResponse)
                Dim oOrder As New Order
                oOrder.purchaseNumber = SessionManager.NumOrden
                oOrder.amount = Double.Parse(SessionManager.MountOrder)
                oOrder.currency = "PEN"

                Dim oVisaNetEntidad As New VisaNetEntidad
                oVisaNetEntidad.CodigoComercio = AppSettings.valueString("merchantId")
                oVisaNetEntidad.SessionToken = objResultado.sessionKey
                oVisaNetEntidad.Monto = SessionManager.MountOrder
                oVisaNetEntidad.NumeroPedido = SessionManager.NumOrden
                oVisaNetEntidad.Channel = "web"
                oVisaNetEntidad.UrlTimeout = AppSettings.valueString("UrlTimeout")
                oVisaNetEntidad.ExpirationMinutes = AppSettings.valueString("ExpirationTime")
                oVisaNetEntidad.SessionPay = respuesta
                oVisaNetEntidad.Orden = JsonHelper.JsonSerializer(Of Order)(oOrder)
                SessionManager.VisaNet = oVisaNetEntidad
                SessionManager.SelectionTransaction = True
                Log.Instance(GetType(confirmarpago)).LogError("Nª Orden Token: " + oVisaNetEntidad.NumeroPedido)

                '-'-'-'-'-'-'-'-'-'-'-'-'-'-'-
                Dim objPDAGlobals As New ClsGobals
                Dim ASIENTO_IDA As String = ""
                Dim ASIENTO_RETORNO As String = ""

                For n As Int16 = 0 To SessionManager.CantidadAsientosIda - 1
                    'If SessionManager.ListaAsientoWebIda.Item(n).Id_WebOrders = SessionManager.Id_WebOrders Then
                    ASIENTO_IDA = ASIENTO_IDA & " " & SessionManager.ListaAsientoWebIda.Item(n).Nume_Asiento
                    objPDAGlobals.Separa_Venta_Web(SessionManager.Cod_ProgramacionIda, SessionManager.NumOrden, SessionManager.ListaAsientoWebIda.Item(n).Nume_Asiento, SessionManager.OrigenId_Ida, SessionManager.DestinoId_Ida, SessionManager.ListaAsientoWebIda.Item(n).ApePaterno & " " & SessionManager.ListaAsientoWebIda.Item(n).Nombre, SessionManager.ListaAsientoWebIda.Item(n).DNI, SessionManager.Nro_ViajeIda, SessionManager.FechaDeViajeIda, SessionManager.ServicioIdIda, SessionManager.HoraViajeIda, SessionManager.ListaAsientoWebIda.Item(n).Costo)
                    'End If
                Next

                For n As Int16 = 0 To SessionManager.CantidadAsientosRetorno - 1
                    'If SessionManager.ListaAsientoWebRetorno.Item(n).Id_WebOrders = SessionManager.Id_WebOrders Then
                    ASIENTO_RETORNO = ASIENTO_RETORNO & " " & SessionManager.ListaAsientoWebRetorno.Item(n).Nume_Asiento
                    objPDAGlobals.Separa_Venta_Web(SessionManager.Cod_ProgramacionRetorno, SessionManager.NumOrden, SessionManager.ListaAsientoWebRetorno.Item(n).Nume_Asiento, SessionManager.OrigenId_Retorno, SessionManager.DestinoId_Retorno, SessionManager.ListaAsientoWebRetorno.Item(n).ApePaterno & " " & SessionManager.ListaAsientoWebRetorno.Item(n).Nombre, SessionManager.ListaAsientoWebRetorno.Item(n).DNI, SessionManager.Nro_ViajeRetorno, SessionManager.FechaDeViajeRetorno, SessionManager.ServicioIdRetorno, SessionManager.HoraViajeRetorno, SessionManager.ListaAsientoWebRetorno.Item(n).Costo)
                    'End If
                Next


                Dim TIDA As String
                Dim TVUELTA As String
                TIDA = "SERIVIO : " & SessionManager.ServicioIda & " - HORA VIAJE : " & SessionManager.HoraViajeIda & " - ORIGEN : " & SessionManager.Origen_Ida & " - DESTINO : " & SessionManager.Destino_Ida & " - FECHA : " & SessionManager.FechaDeViajeIda & " - NRO PROGRAMACION : " & SessionManager.Cod_ProgramacionIda & " - ASIENTOS : " & ASIENTO_IDA  'SessionManager.CantidadAsientosIda
                If Val(SessionManager.Cod_ProgramacionRetorno) > 0 Then
                    TVUELTA = "SERIVIO : " & SessionManager.ServicioRetorno & " - HORA VIAJE : " & SessionManager.HoraViajeRetorno & " - ORIGEN : " & SessionManager.Origen_Retorno & " - DESTINO : " & SessionManager.Destino_Retorno & " - FECHA : " & SessionManager.FechaDeViajeRetorno & " - NRO PROGRAMACION : " & SessionManager.Cod_ProgramacionRetorno & " - ASIENTO : " & ASIENTO_RETORNO 'SessionManager.CantidadAsientosRetorno
                Else
                    TVUELTA = ""
                End If


                objPDAGlobals.Temporal_Venta_Web(SessionManager.Id_WebOrders, "2", SessionManager.PerfilName, SessionManager.MontoIda + SessionManager.MontoRetorno, TIDA, TVUELTA)

                '-'-'-'-'-'-'-'-'-'-'-'-'-'
                ''SALE A VISA 
                objPDAGlobals.ActualizarWebOrdersEstadoId(SessionManager.Id_WebOrders, 2)
            Catch ex As WebException
                reader = New StreamReader(ex.Response.GetResponseStream, True)
                resultado = reader.ReadToEnd()
                reader.Close()
                Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
                Log.Instance(GetType(confirmarpago)).LogInformacion("Response Token Session Error: " + resultado)
                Response.Redirect("destinos.aspx?error=0")
            Catch ex As Exception
                oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
                Functions.EliminarAsientobySession()
                Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
                Response.Redirect("destinos.aspx?error=0")
            End Try

        Catch ex As Exception
            oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
            Functions.EliminarAsientobySession()
            Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
            Response.Redirect("destinos.aspx?error=0")
        End Try
    End Sub





#End Region

    Dim status As Boolean = True

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Me.Title = Functions.NombreTituloPagina(Me.Title)
        Dim formulario As HtmlForm = CType(Me.FindControl("form1"), HtmlForm)
        formulario.Action = AppSettings.valueString("UrlPageRespuesta")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If SessionManager.FechaDeViajeIda Is Nothing Then
                status = False
                Response.Redirect("end.aspx", False)
                Exit Sub
            End If
            Dim p As String = String.Empty
            If Functions.ValidarSeleccionConfirmacion(p) = False Then
                Response.Redirect(p, False)
                Exit Sub
            End If
            If SessionManager.isReservando = False Then
                Functions.ValiDateOrder(Page)
            End If
            PermisosObjetos()
            CargarInformaciondeViajes()


            If Functions.UserLogueo(Me) Then

                WUCViewUser1.UserName = SessionManager.PerfilName
                WUCViewUser1.LogueoEnd = False
                If WUCViewUser1.LogueoStart = False Then
                    'Server.TransferRequest(Request.Url.AbsolutePath, False)
                    Response.Redirect("confirmarpago.aspx", False)
                End If

                WUCViewUser1.LogueoStart = True
            Else
                WUCViewUser1.LogueoEnd = True
                WUCViewUser1.LogueoStart = False
            End If

            If Not IsPostBack Then
                ListarMediosPagos()
                If SessionManager.isReservando = False Then
                    SessionManager.isReservando = True
                    CreaUsuario()
                    OrderGenerate(1)
                End If
                ''CreaUsuario()
                ''OrderGenerate(1)
                GenerarToken()
            End If
            ScriptUser.JsRegister(Me, Me.ToString, "Vali();")

            '' Me.txtCorreo.Focus()

        Catch ex As Exception
            Log.Instance(GetType(confirmarpago)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub btnregresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnregresar.Click

        Try
            Response.Redirect("asignarpasajero.aspx", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
