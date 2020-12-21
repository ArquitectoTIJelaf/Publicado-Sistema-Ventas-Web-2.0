Imports PLogic
Imports PEntity
Imports PUtilitario

Public Class Functions

    Public Shared Function NombreTituloPagina(Titulo As String) As String
        Return Titulo.Replace("[NombreEmpresa]", AppSettings.valueString("NombreEmpresa"))
    End Function

    Public Shared Function NombreAsuntoMensajeEmail(Asunto As String) As String
        Return Asunto.Replace("[NombreEmpresa]", AppSettings.valueString("NombreEmpresa"))
    End Function

    Public Shared Sub NotificarErrorConfiguracionSistema(ByVal mensaje As String)
        Try
            NotificacionErrorSystemAdmin(mensaje)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Shared Sub EliminarAsientosCaducados()

        Try
            Dim oGblsr As PLGlobals = New PLGlobals()
            If String.IsNullOrEmpty(SessionManager.IDCID) = False AndAlso SessionManager.TimeOut > 0 Then
                Dim nMinutos As Integer = SessionManager.TimeOut
                oGblsr.LiberarAsientosVencidos(-nMinutos)
                oGblsr.Dispose()
                oGblsr = Nothing
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Shared Sub EliminarAsientobySession()

        Try
            If String.IsNullOrEmpty(SessionManager.IDCID) = False AndAlso SessionManager.TimeOut > 0 Then
                Dim oPLGlobals As New PLGlobals()

                oPLGlobals.LiberarAsientosBySesion(SessionManager.IDCID)
                SessionUser.ResetCantAsientos()

            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Shared Sub LiberarAsientobySession()

        Try
            If String.IsNullOrEmpty(SessionManager.IDCID) = False AndAlso SessionManager.TimeOut > 0 Then
                Dim oPLGlobals As New PLGlobals()

                oPLGlobals.LiberarAsientosBySesion(SessionManager.IDCID)
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Shared Sub EliminarReservasBloqueadas(ByVal s As HttpSessionState)

        Try
            If String.IsNullOrEmpty(s("IDCID").ToString()) = False AndAlso s.Timeout > 0 Then
                Dim oPLGlobals As New PLGlobals()
                oPLGlobals.LiberarAsientosBySesion(s("IDCID").ToString())
                oPLGlobals.LiberarAsientosVencidos(-s.Timeout)
                oPLGlobals.EliminaReservas(CInt(s("Id_WebOrders")))
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub
    Public Shared Function RutaSelection() As Boolean

        Try
            If SessionManager.ViajeRetorno = False Then
                If SessionManager.SelectDestinosIda AndAlso SessionManager.SelectDestinosRetorno = False Then
                    Return True
                Else
                    Return False
                End If
            ElseIf SessionManager.ViajeRetorno = True Then
                If SessionManager.SelectDestinosIda AndAlso SessionManager.SelectDestinosRetorno Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function ValidarSeleccionAsientos(ByRef page As String) As Boolean

        Try
            Dim estado As Boolean = True
            If SessionManager.SelectionRuta = False Then
                page = "destinos.aspx"
                estado = False
            End If
            Return estado
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function ValidarSeleccionPasajeros(ByRef page As String) As Boolean

        Try
            Dim estado As Boolean = True
            If SessionManager.SelectionRuta = False AndAlso SessionManager.SelectionAsientos = False Then
                page = "destinos.aspx"
                estado = False
            ElseIf SessionManager.SelectionRuta = True AndAlso SessionManager.SelectionAsientos = False Then
                page = "seleccionarasientos.aspx#bus"
                estado = False
            End If
            Return estado
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function ValidarSeleccionConfirmacion(ByRef page As String) As Boolean

        Try
            Dim estado As Boolean = True
            If SessionManager.SelectionRuta = False AndAlso SessionManager.SelectionAsientos = False AndAlso SessionManager.SelectionPasajeros = False Then
                page = "destinos.aspx"
                estado = False
            ElseIf SessionManager.SelectionRuta = True AndAlso SessionManager.SelectionAsientos = False AndAlso SessionManager.SelectionPasajeros = False Then
                page = "seleccionarasientos.aspx#bus"
                estado = False
            ElseIf SessionManager.SelectionRuta = True AndAlso SessionManager.SelectionAsientos = True AndAlso SessionManager.SelectionPasajeros = False Then
                page = "asignarpasajero.aspx#registro"
                estado = False
            End If
            Return estado
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function ValidarSeleccionPago(ByRef page As String) As Boolean

        Try
            Dim estado As Boolean = True
            If ValidarSeleccionConfirmacion(page) Then
                If SessionManager.SelectionPago = False Then
                    page = "confirmarpago.aspx"
                    estado = False
                End If
            Else
                estado = False
            End If
            Return estado
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function ValidarPageTransaction(ByRef page As String) As Boolean

        Try
            Dim estado As Boolean = True
            If ValidarSeleccionPago(page) Then
                If SessionManager.SelectionTransaction = False Then
                    page = "pagar.aspx"
                    estado = False
                End If
            Else
                estado = False
            End If
            Return estado
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function ValidarPageOKTransaction(ByRef page As String) As Boolean

        Try
            Dim estado As Boolean = True
            If ValidarPageTransaction(page) Then
                If SessionManager.SelectionTransaction = False Then
                    page = "respuesta.aspx"
                    estado = False
                End If
            Else
                estado = False
            End If
            Return estado
        Catch ex As Exception
            Throw
        End Try
    End Function





    Public Shared Sub ActualizarProgramacion()

        Try
            Dim oPLGlobals As New PLGlobals
            SessionManager.Cod_ProgramacionIda = oPLGlobals.ObtenerCodiProgramacion(SessionManager.Nro_ViajeIda, SessionManager.Fecha_Programacion_Ida)
            Dim oETb_BusIda As ETb_Bus = oPLGlobals.ObtenerBus(SessionManager.Cod_ProgramacionIda, SessionManager.IdEmpresaIda, SessionManager.Codi_Sucursal_ida, SessionManager.ServicioIdIda, SessionManager.Hora_Programacion_Ida, SessionManager.Codi_Destino_Ida)
            SessionManager.IdBusIda = oETb_BusIda.Codi_Bus
            SessionManager.PlanoIda = oETb_BusIda.Plan_Bus

            If SessionManager.Cod_ProgramacionIda > 0 Then
                oPLGlobals.Asiento_ActualizarProgramacion(SessionManager.Nro_ViajeIda, "1" & SessionManager.IDCID, SessionManager.Cod_ProgramacionIda, SessionManager.Fecha_Programacion_Ida)

            End If
            If SessionManager.ViajeRetorno = True Then
                SessionManager.Cod_ProgramacionRetorno = oPLGlobals.ObtenerCodiProgramacion(SessionManager.Nro_ViajeRetorno, SessionManager.Fecha_Programacion_Retorno)
                Dim oETb_BusRetorno As ETb_Bus = oPLGlobals.ObtenerBus(SessionManager.Cod_ProgramacionRetorno, SessionManager.IdEmpresaRetorno, SessionManager.Codi_Sucursal_Retorno, SessionManager.ServicioIdRetorno, SessionManager.Hora_Programacion_Retorno, SessionManager.Codi_Destino_Retorno)
                SessionManager.IdBusRetorno = oETb_BusRetorno.Codi_Bus
                SessionManager.PlanoRetorno = oETb_BusRetorno.Plan_Bus
                If SessionManager.Cod_ProgramacionRetorno > 0 Then
                    oPLGlobals.Asiento_ActualizarProgramacion(SessionManager.Nro_ViajeRetorno, "2" & SessionManager.IDCID, SessionManager.Cod_ProgramacionRetorno, SessionManager.Fecha_Programacion_Retorno)
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Shared Sub ClearPageRutas()
        SessionManager.DeleteListaHorarioProgramacionIda()
        SessionManager.DeleteListaHorarioProgramacionRetorno()
    End Sub

    Public Shared Function ValidoMyOrders() As Boolean

        Try
            If SessionManager.ViajeRetorno Then
                Dim oListaAsientosIda As ListaAsientos
                Dim oListaAsientosRetorno As ListaAsientos

                Dim oG As New PLGlobals()
                oListaAsientosIda = oG.CantidadAsientoReservadosbSession("1" & SessionManager.IDCID)
                oListaAsientosRetorno = oG.CantidadAsientoReservadosbSession("2" & SessionManager.IDCID)

                If oListaAsientosIda.Item(0).Cantidad = SessionManager.CantidadAsientosIda AndAlso oListaAsientosRetorno.Item(0).Cantidad = SessionManager.CantidadAsientosRetorno Then
                    Return True
                Else
                    Return False
                End If
            Else
                Dim oListaAsientosIda As ListaAsientos

                Dim oG As New PLGlobals()
                oListaAsientosIda = oG.CantidadAsientoReservadosbSession("1" & SessionManager.IDCID)
                If oListaAsientosIda.Item(0).Cantidad = SessionManager.CantidadAsientosIda Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw
        End Try

    End Function

    Public Shared Sub ValiDateOrder(ByVal p As Page)

        Try
            Dim oGs As New PLGlobals()
            If ValidoMyOrders() = False Then
                ScriptUser.JQueryMensaje(p, Message.WAsientoEliminados, 1)
                LiberarAsientobySession()
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Shared Function UserLogueo(ByVal p As Page) As Boolean

        Try
            Dim valids = False
            If SessionManager.NivelUser > 0 AndAlso SessionManager.IdUser > 0 AndAlso p.User.Identity.IsAuthenticated = True Then
                valids = True
            Else
                valids = False
            End If
            Return valids
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function UserValid(ByVal p As Page) As Boolean

        Try
            Dim valids = False
            If UserLogueo(p) Then
                SessionManager.IsShoViable = 1
                valids = True
            Else
                SessionManager.IsShoViable = 0
                valids = False
            End If
            Return valids
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function EliminarOrden() As Boolean

        Try
            Dim oOrders As New PLGlobals()
            Dim estado As Boolean = False
            If oOrders.EliminarOrdenbyId(SessionManager.Id_WebOrders) Then
                estado = True
            End If
            Return estado
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Sub ClearPreviewTransaccion()
        Try
            SessionManager.Respuesta = String.Empty
            SessionManager.Cod_Tienda = String.Empty
            SessionManager.NroOrdenTienda = String.Empty
            SessionManager.Cod_Accion = String.Empty
            SessionManager.isPagando = False
            SessionManager.IsProcesable = False
            SessionManager.ListaDetalleVentawebIda = Nothing
            SessionManager.ListaDetalleVentawebRetorno = Nothing
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Shared Sub ClearPostTransaccion()

        Try
            SessionManager.Respuesta = String.Empty
            SessionManager.Cod_Tienda = String.Empty
            SessionManager.NroOrdenTienda = String.Empty
            SessionManager.Cod_Accion = String.Empty
            SessionManager.isPagando = False
            SessionManager.isReservando = False
            SessionManager.IsProcesable = False
            SessionManager.MontoIda = 0
            SessionManager.MontoRetorno = 0
            SessionManager.ListaAsientosbyOrderIda = Nothing
            SessionManager.ListaAsientosbyOrderRetorno = Nothing
            SessionManager.ListaProgramacionIda = Nothing
            SessionManager.ListaProgramacionRetorno = Nothing
            SessionManager.SelectionRuta = False
            SessionManager.SelectionAsientos = False
            SessionManager.SelectionPasajeros = False
            SessionManager.SelectionPago = False
            SessionManager.SelectionTransaction = False
            SessionManager.Cliente = Nothing
            ''carlos
            SessionManager.IDCID = ""
            SessionManager.TimeOut = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Shared Sub PageOnlyAdmin(ByVal Meo As Object)

        Try
            Dim Pag As System.Web.UI.Page = Meo.page
            If Not (SessionUser.IsAdmin() = True AndAlso UserValid(Pag) = True) Then
                Pag.Response.Redirect("end.aspx", False)
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Shared Sub PageOnlyAdminAndCounter(ByVal Meo As Object)

        Try
            Dim Pag As System.Web.UI.Page = Meo.page
            If Not ((SessionUser.IsAdmin() = True OrElse SessionUser.IsCounter = True) AndAlso UserValid(Pag) = True) Then
                Pag.Response.Redirect("end.aspx", False)
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub



    Public Shared Sub PageOnlyUsers(ByVal Meo As Object)
        Try
            Dim Pag As System.Web.UI.Page = Meo.Page
            If Not (SessionManager.NivelUser > 0 AndAlso UserValid(Pag) = True) Then
                Pag.Response.Redirect("destinos.aspx", False)
                Exit Sub

            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub




End Class
