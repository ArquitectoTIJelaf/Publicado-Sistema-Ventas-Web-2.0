Imports System.Data
Imports PLogic
Imports PEntity
Imports System.Collections.Generic
Imports System.IO
Imports PUtilitario



Partial Class detallecompra
    Inherits System.Web.UI.Page

    Dim oVentaDetalle As PLGlobals
    Dim oListaVenta As ListaVenta
    Dim status As Boolean = True

#Region "Metodos Privados"



    Private Sub InfoCompra()

        Try
            Dim oPLGlobals As New PLGlobals
            Dim oListaWebOrders As ListaWebOrders = oPLGlobals.InfoVentaWebByIdOrders( SessionManager.Id_WebOrders, 1)
            If oListaWebOrders.Count > 0 Then
                lblnumorden.Text = oListaWebOrders.Item(0).NumOrder_WebOrders.ToString
                lblusercliente.Text = oListaWebOrders.Item(0).Usuario_Users.ToString
                ''lblusercliente.Text = SessionManager.CorreoCliente.ToString
                lblfechacompra.Text = oListaWebOrders.Item(0).Fecha_WebOrders.ToString
                lblnumasientos.Text = oListaWebOrders.Item(0).NAsiento.ToString
                lblnota.Text = "Esta orden fue procesada por el formulario de pagos." 'oListaWebOrders.Item(0).Nota.ToString
                lblcostoneto.Text = oListaWebOrders.Item(0).Costo.ToString(".00")
                lbltarjeta.Text = SessionManager.TarjetaTransaccion
                ''lbltarjeta.Text = SessionManager.NomTarjeta

                ''Datos Mobile
                lblnumordenmobile.Text = oListaWebOrders.Item(0).NumOrder_WebOrders.ToString
                lbluserclientemobile.Text = oListaWebOrders.Item(0).Usuario_Users.ToString
                lblfechacompramobile.Text = oListaWebOrders.Item(0).Fecha_WebOrders.ToString
                lblnumasientosmobile.Text = oListaWebOrders.Item(0).NAsiento.ToString
                lblnotamobile.Text = "Esta orden fue procesado por el formulario de pagos." 'oListaWebOrders.Item(0).Nota.ToString
                lblcostonetomobile.Text = oListaWebOrders.Item(0).Costo.ToString(".00")
                lbltarjetamobile.Text = SessionManager.TarjetaTransaccion

                If oListaWebOrders.Count = 1 Then
                    SessionManager.Id_VentaWebIda = oListaWebOrders.Item(0).Id_VentaWeb
                    SessionManager.RutaConcatenadaIda = oListaWebOrders(0).RutaDoble
                    SessionManager.ViajeRetorno = False
                ElseIf oListaWebOrders.Count = 2 Then
                    SessionManager.Id_VentaWebIda = oListaWebOrders.Item(0).Id_VentaWeb
                    SessionManager.Id_VentaWebRetorno = oListaWebOrders.Item(1).Id_VentaWeb
                    SessionManager.RutaConcatenadaIda = oListaWebOrders(0).RutaDoble
                    SessionManager.RutaConcatenadaRetorno = oListaWebOrders(1).RutaDoble
                    SessionManager.ViajeRetorno = True
                End If
                HeaderCompra()
            Else
                ScriptUser.JQueryMensaje(Me, "No se encontro informacion de la venta")
            End If
        Catch ex As Exception
            Log.Instance(GetType(detallecompra)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Sub HeaderCompra()

        Try
            If SessionManager.OrdenVenta = 0 Then
                If SessionManager.ViajeRetorno = True Then
                    HeaderCompraIda()
                    HeaderCompraRetorno()
                Else
                    HeaderCompraIda()
                End If
            Else
                If SessionManager.OrdenVenta = 1 Then
                    HeaderCompraIda()
                ElseIf SessionManager.OrdenVenta = 2 Then
                    HeaderCompraRetorno()
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(detallecompra)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub HeaderCompraIda(Optional ByVal listar As Boolean = True)

        Try
            If SessionManager.Id_VentaWebIda > 0 Then
                Dim oPLGlobals As New PLGlobals
                Dim oListaVenta As ListaVenta = oPLGlobals.VentaWeb_InfoHeader( SessionManager.Id_VentaWebIda)
                If oListaVenta.Count > 0 Then
                    lblorigenida.Text = oListaVenta.Item(0).Origen
                    lblfechaviajeida.Text = oListaVenta.Item(0).FechaViaje
                    lbldesitnoida.Text = oListaVenta.Item(0).Destino
                    lblservicionameida.Text = oListaVenta.Item(0).Servicio

                    ''Datos Mobile

                    lblorigenidamobile.Text = oListaVenta.Item(0).Origen
                    lblfechaviajeidamobile.Text = oListaVenta.Item(0).FechaViaje
                    lbldesitnoidamobile.Text = oListaVenta.Item(0).Destino
                    lblservicionameidamobile.Text = oListaVenta.Item(0).Servicio


                    If SessionManager.ListaDetalleVentawebIda Is Nothing OrElse listar Then
                        SessionManager.ListaDetalleVentawebIda = oPLGlobals.VentaWeb_InfoDetalle( SessionManager.Id_VentaWebIda, SessionManager.RutaConcatenadaIda)
                    End If
                    If SessionManager.ListaDetalleVentawebIda.Count > 0 Then
                        iddetalleviajeida.Visible = True
                        gvdetalleventaida.DataSource = SessionManager.ListaDetalleVentawebIda
                        gvdetalleventaida.DataBind()
                        dlpasajerosida.DataSource = SessionManager.ListaDetalleVentawebIda
                        dlpasajerosida.DataBind()
                    Else
                        iddetalleviajeida.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            Log.Instance(GetType(detallecompra)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Sub HeaderCompraRetorno(Optional ByVal listar As Boolean = True)

        Try
            If SessionManager.Id_VentaWebRetorno > 0 Then
                Dim oPLGlobals As New PLGlobals
                Dim oListaVenta As ListaVenta = oPLGlobals.VentaWeb_InfoHeader( SessionManager.Id_VentaWebRetorno)
                If oListaVenta.Count > 0 Then
                    lblorigenretorno.Text = oListaVenta.Item(0).Origen
                    lblfechaviajeretorno.Text = oListaVenta.Item(0).FechaViaje
                    lbldestinoretorno.Text = oListaVenta.Item(0).Destino
                    lblservicioretorno.Text = oListaVenta.Item(0).Servicio


                    ''Datos Mobile

                    lblorigenretornomobile.Text = oListaVenta.Item(0).Origen
                    lblfechaviajeretornomobile.Text = oListaVenta.Item(0).FechaViaje
                    lbldestinoretornomobile.Text = oListaVenta.Item(0).Destino
                    lblservicioretornomobile.Text = oListaVenta.Item(0).Servicio


                    If SessionManager.ListaDetalleVentawebRetorno Is Nothing OrElse listar Then
                        SessionManager.ListaDetalleVentawebRetorno = oPLGlobals.VentaWeb_InfoDetalle( SessionManager.Id_VentaWebRetorno, SessionManager.RutaConcatenadaRetorno)
                    End If
                    If SessionManager.ListaDetalleVentawebRetorno.Count > 0 Then
                        iddetalleviajeretorno.Visible = True
                        gvdetallventaretorno.DataSource = SessionManager.ListaDetalleVentawebRetorno
                        gvdetallventaretorno.DataBind()
                        dlpasajerosretorno.DataSource = SessionManager.ListaDetalleVentawebRetorno
                        dlpasajerosretorno.DataBind()
                    Else
                        iddetalleviajeretorno.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
  Log.Instance(GetType(detallecompra)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

#End Region

#Region "Metodos Privados con Eventos"

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Me.Title = Functions.NombreTituloPagina(Me.Title)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''Functions.PageOnlyUsers(Me)
        If SessionManager.Id_WebOrders < 1 Then
            status = False
            Response.Redirect("end.aspx", False)
            Exit Sub
        End If
        Dim p As String = String.Empty
        If SessionManager.SelectionOKTransaction = False Then
            Response.Redirect("destinos.aspx", False)
            Exit Sub
        End If
        iddetalleviajeida.Visible = False
        iddetalleviajeretorno.Visible = False


        InfoCompra()

        If SessionManager.OrdenVenta = 0 Then

            If SessionManager.ViajeRetorno = True Then
                idheaderviajeida.Visible = True
                idheaderviajeretorno.Visible = True
                contIda.Visible = True
                contRetorno.Visible = True
            Else
                idheaderviajeida.Visible = True
                idheaderviajeretorno.Visible = False
                contIda.Visible = True
                contRetorno.Visible = False
            End If
        Else
            If SessionManager.OrdenVenta = 1 Then
                idheaderviajeida.Visible = True
                idheaderviajeretorno.Visible = False
            ElseIf SessionManager.OrdenVenta = 2 Then
                idheaderviajeida.Visible = False
                idheaderviajeretorno.Visible = True
            End If
        End If
        If Functions.UserLogueo(Me) Then
            WUCViewUser1.UserName = SessionManager.PerfilName
            WUCViewUser1.LogueoEnd = False
            WUCViewUser1.LogueoStart = True
        Else
            WUCViewUser1.LogueoEnd = True
            WUCViewUser1.LogueoStart = False
        End If
        ScriptUser.JsRegister(Me, Me.ToString, "Validation();")

    End Sub

    Protected Sub gvdetalleventaida_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvdetalleventaida.SelectedIndexChanged

        Try
            If status Then
                If SessionManager.RutaConcatenadaIda = False Then
                    SessionManager.RutaConcatenadaPasajero = False
                Else
                    SessionManager.RutaConcatenadaPasajero = True
                End If
                SessionManager.ListaDetallePasajero = SessionManager.ListaDetalleVentawebIda
                Dim oEVenta As New EVenta
                oEVenta.Id_Venta = CInt(gvdetalleventaida.SelectedDataKey(0))
                oEVenta.NUME_ASIENTO = gvdetalleventaida.SelectedDataKey(1).ToString
                oEVenta.TIPO_DOC = gvdetalleventaida.SelectedDataKey(2).ToString
                oEVenta.DNI = gvdetalleventaida.SelectedDataKey(3).ToString
                oEVenta.NIT_CLIENTE = gvdetalleventaida.SelectedDataKey(4).ToString
                oEVenta.EDAD = gvdetalleventaida.SelectedDataKey(5).ToString
                oEVenta.PasajeroIda = True
                oEVenta.PasajeroRetorno = False

                SessionManager.Pasajero = oEVenta
                If String.IsNullOrEmpty(SessionManager.FechaDeViajeIda) = False Then
                    SessionManager.Fviaje = SessionManager.FechaDeViajeIda
                End If
                Response.Redirect("asientopasajero.aspx", False)
            End If
        Catch ex As Exception
            Log.Instance(GetType(detallecompra)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub gvdetallventaretorno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvdetallventaretorno.SelectedIndexChanged

        Try
            If status Then
                If SessionManager.RutaConcatenadaRetorno = False Then
                    SessionManager.RutaConcatenadaPasajero = False
                Else
                    SessionManager.RutaConcatenadaPasajero = True
                End If
                SessionManager.ListaDetallePasajero = SessionManager.ListaDetalleVentawebRetorno
                Dim oEVenta As New EVenta
                oEVenta.Id_Venta = CInt(gvdetallventaretorno.SelectedDataKey(0))
                oEVenta.NUME_ASIENTO = gvdetallventaretorno.SelectedDataKey(1).ToString
                oEVenta.TIPO_DOC = gvdetallventaretorno.SelectedDataKey(2).ToString
                oEVenta.DNI = gvdetallventaretorno.SelectedDataKey(3).ToString
                oEVenta.NIT_CLIENTE = gvdetallventaretorno.SelectedDataKey(4).ToString
                oEVenta.EDAD = gvdetallventaretorno.SelectedDataKey(5).ToString
                oEVenta.PasajeroIda = False
                oEVenta.PasajeroRetorno = True

                SessionManager.Pasajero = oEVenta
                If String.IsNullOrEmpty(SessionManager.FechaDeViajeRetorno) = False Then
                    SessionManager.Fviaje = SessionManager.FechaDeViajeRetorno
                End If
                Response.Redirect("asientopasajero.aspx", False)
            End If

        Catch ex As Exception
            Log.Instance(GetType(detallecompra)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

#End Region



End Class

