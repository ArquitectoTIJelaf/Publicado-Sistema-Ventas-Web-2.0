Imports System.Data
Imports PDataAccess
Imports PLogic
Imports Microsoft.VisualBasic
Imports System.Web
Imports PEntity
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Windows.Forms

Imports vb = Microsoft.VisualBasic
Imports PUtilitario

Partial Class destinos
    Inherits System.Web.UI.Page

    Dim objPLGlobls As New PLGlobals
    Dim selected As Boolean

#Region "Metodos Privados"

    Private Sub Errors()
        Try
            SessionManager.Destino_Ida = String.Empty
            SessionManager.Origen_Ida = String.Empty
            SessionManager.Destino_Retorno = String.Empty
            SessionManager.Origen_Retorno = String.Empty
            SessionManager.DestinoId_Ida = 0
            SessionManager.OrigenId_Ida = 0
            SessionManager.DestinoId_Retorno = 0
            SessionManager.OrigenId_Retorno = 0
            SessionManager.ServicioIda = String.Empty
            SessionManager.ServicioRetorno = String.Empty
            SessionManager.FechaDeViajeIda = String.Empty
            SessionManager.FechaDeViajeRetorno = String.Empty
            SessionManager.HoraViajeRetorno = String.Empty
            SessionManager.HoraViajeIda = String.Empty
            SessionManager.HoraLlegadaViajeIda = String.Empty
            SessionManager.HoraLlegadaViajeRetorno = String.Empty

            SessionManager.ViajeRetorno = True
            SessionManager.SelectDestinosIda = False
            SessionManager.SelectDestinosRetorno = False

            SessionManager.OrdenVenta = 0
        Catch ex As Exception
            Log.Instance(GetType(destinos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub LoadDataProg()

        Try
            objPLGlobls = New PLGlobals()

            If SessionManager.ViajeRetorno = True AndAlso SessionManager.Promociones = True Then
                SessionManager.TransactionPromociones = True
            Else
                SessionManager.TransactionPromociones = False
            End If

            If Not IsDate(txtfechaida.Text) Then
                txtfechaida.Text = "01/01/1900"
            End If

            If DateDiff(DateInterval.Day, Now, CDate(txtfechaida.Text)) < 0 Then
                ScriptUser.JQueryMensaje(Me, "La fecha de viaje no pueder ser menor a hoy")
                Exit Sub
            End If


            SessionManager.FechaDeViajeIda = txtfechaida.Text
            lblrutaida.Text = SessionManager.Origen_Ida & " - " & SessionManager.Destino_Ida

            SessionManager.ListaItinerariosIda = objPLGlobls.ListarItinerario(SessionManager.OrigenId_Ida, SessionManager.DestinoId_Ida, 0, 1, SessionManager.FechaDeViajeIda, SessionManager.Promociones)
            iditinerarioida.Visible = True

            If SessionManager.ListaItinerariosIda.Count > 0 Then
                gvrutasida.DataSource = SessionManager.ListaItinerariosIda
                gvrutasida.DataBind()
            End If

            If SessionManager.ViajeRetorno Then
                iditinerarioretorno.Visible = True

                If Not IsDate(txtfecharetorno.Text) Then
                    txtfecharetorno.Text = "01/01/1900"
                End If

                If DateDiff(DateInterval.Day, Now, CDate(txtfechaida.Text)) < 0 Then
                    ScriptUser.JQueryMensaje(Me, "La fecha de ida no pueder ser menor a hoy")
                    Exit Sub
                End If
                If DateDiff(DateInterval.Day, Now, CDate(txtfecharetorno.Text)) < 0 Then
                    ScriptUser.JQueryMensaje(Me, "La fecha de retorno no pueder ser menor a hoy")
                    Exit Sub
                End If
                If DateDiff(DateInterval.Day, CDate(txtfecharetorno.Text), CDate(txtfechaida.Text)) > 0 Then
                    ScriptUser.JQueryMensaje(Me, "La fecha de ida no pueder ser menor a la fecha de retorno")
                    Exit Sub
                End If

                SessionManager.FechaDeViajeRetorno = txtfecharetorno.Text
                lblrutaretorno.Text = SessionManager.Origen_Retorno & " - " & SessionManager.Destino_Retorno

                SessionManager.ListaItinerariosRetorno = objPLGlobls.ListarItinerario( SessionManager.OrigenId_Retorno, SessionManager.DestinoId_Retorno, 0, 1, SessionManager.FechaDeViajeRetorno, SessionManager.Promociones)
                If SessionManager.ListaItinerariosRetorno.Count > 0 Then
                    gvrutasretorno.DataSource = SessionManager.ListaItinerariosRetorno
                    gvrutasretorno.DataBind()
                End If

            End If


        Catch ex As Exception
            Log.Instance(GetType(destinos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Function ListadoFechas(ByVal FechaViaje As String) As String()
        Try
            Dim arrayfechas(6) As String
            Dim fechaini As String = String.Empty
            If DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, 1, CDate(Now.ToShortDateString)), CDate(FechaViaje)) >= 3 Then
                fechaini = DateAdd(DateInterval.Day, -3, CDate(FechaViaje))
            ElseIf DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, 1, CDate(Now.ToShortDateString)), CDate(FechaViaje)) = 2 Then
                fechaini = DateAdd(DateInterval.Day, -2, CDate(FechaViaje))
            ElseIf DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, 1, CDate(Now.ToShortDateString)), CDate(FechaViaje)) = 1 Then
                fechaini = DateAdd(DateInterval.Day, -1, CDate(FechaViaje))
            ElseIf DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, 1, CDate(Now.ToShortDateString)), CDate(FechaViaje)) = 0 Then
                fechaini = FechaViaje

            End If

            For i As Int16 = 0 To 6
                arrayfechas(i) = DateAdd("d", i, CDate(fechaini)).ToShortDateString
            Next
            Return arrayfechas
        Catch ex As Exception
            Log.Instance(GetType(destinos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function

    Private Function ValidarTiempoRutasbyFecha(ByRef msg As String, ByVal FechaIda As String, ByVal FechaRetorno As String, ByRef oListaMaestroProgramacion As ListaMaestroProgramacion, Optional ByVal index As Integer = -1) As Boolean
        Dim oPLGlobals As New PLGlobals

        Try
            Dim respuesta As Boolean = True
            Dim tiempo As String = oPLGlobals.ObtenerTiempoOficinas( SessionManager.OrigenId_Ida, SessionManager.DestinoId_Ida, SessionManager.ServicioIdIda)
            If String.IsNullOrEmpty(tiempo) = False Then
                Dim time() As String = tiempo.Split(":")
                If SessionManager.ListaHorarioProgramacionIda Is Nothing Then
                    respuesta = False
                    msg = Message.WSelectHorarioViajeIda
                    Exit Function
                End If
                If SessionManager.ListaHorarioProgramacionRetorno Is Nothing Then
                    respuesta = False
                    msg = Message.WSelectHorarioViajeRetorno
                    Exit Function
                End If
                Dim timeofi As Integer = (CInt(time(0)) * 60) + time(1)
                Dim conhorarioretorno As Boolean = True
                If index >= 0 Then
                    Dim horaida As DateTime = SessionManager.ListaHorarioProgramacionIda.Item(index).Hora_Partida
                    Dim horaidaformat As String = horaida.ToString("HH:mm")
                    Dim fechahoraida As DateTime = Util.FormatTimeLong(CInt(FechaIda.Substring(6, 4)), CInt(FechaIda.Substring(3, 2)), CInt(FechaIda.Substring(0, 2)), CInt(horaidaformat.Substring(0, 2)), CInt(horaidaformat.Substring(3, 2)), 0)
                    Dim fechahorallegadaprox As DateTime = DateAdd(DateInterval.Minute, timeofi, fechahoraida)
                    For Each entidad_retorno As ETb_Maestro_Programacion In SessionManager.ListaHorarioProgramacionRetorno
                        Dim horaretorno As DateTime = entidad_retorno.Hora_Partida
                        Dim horaretornoformat As String = horaretorno.ToString("HH:mm")
                        Dim fechahoraretorno As DateTime = Util.FormatTimeLong(CInt(FechaRetorno.Substring(6, 4)), CInt(FechaRetorno.Substring(3, 2)), CInt(FechaRetorno.Substring(0, 2)), CInt(horaretornoformat.Substring(0, 2)), CInt(horaretornoformat.Substring(3, 2)), 0)
                        If DateDiff(DateInterval.Hour, fechahorallegadaprox, fechahoraretorno) > 0 Then
                            oListaMaestroProgramacion.Add(entidad_retorno)
                        End If
                    Next
                    If oListaMaestroProgramacion.Count > 0 Then
                        respuesta = True
                    Else
                        respuesta = False
                        msg = Message.WFechaHoraViajeIdaInvalida
                    End If
                Else
                    For Each entidad_ida As ETb_Maestro_Programacion In SessionManager.ListaHorarioProgramacionIda
                        Dim horaida As DateTime = entidad_ida.Hora_Partida
                        Dim horaidaformat As String = horaida.ToString("HH:mm")
                        Dim fechahoraida As DateTime = Util.FormatTimeLong(CInt(FechaIda.Substring(6, 4)), CInt(FechaIda.Substring(3, 2)), CInt(FechaIda.Substring(0, 2)), CInt(horaidaformat.Substring(0, 2)), CInt(horaidaformat.Substring(3, 2)), 0)
                        Dim fechahorallegadaprox As DateTime = DateAdd(DateInterval.Minute, timeofi, fechahoraida)
                        For Each entidad_retorno As ETb_Maestro_Programacion In SessionManager.ListaHorarioProgramacionRetorno
                            Dim horaretorno As DateTime = entidad_retorno.Hora_Partida
                            Dim horaretornoformat As String = horaretorno.ToString("HH:mm")
                            Dim fechahoraretorno As DateTime = Util.FormatTimeLong(CInt(FechaRetorno.Substring(6, 4)), CInt(FechaRetorno.Substring(3, 2)), CInt(FechaRetorno.Substring(0, 2)), CInt(horaretornoformat.Substring(0, 2)), CInt(horaretornoformat.Substring(3, 2)), 0)
                            If DateDiff(DateInterval.Hour, fechahorallegadaprox, fechahoraretorno) > 0 Then
                                oListaMaestroProgramacion.Add(entidad_retorno)
                            Else
                                conhorarioretorno = False
                            End If
                        Next
                        If oListaMaestroProgramacion.Count = SessionManager.ListaHorarioProgramacionRetorno.Count Then
                            Exit For
                        End If
                    Next
                    If oListaMaestroProgramacion.Count > 0 Then
                        respuesta = True
                    Else
                        respuesta = False
                        msg = Message.WFechaViajeIdaInvalida
                    End If
                End If
            Else
                respuesta = False
                msg = Message.WFechaViajeIdaInvalida
                Functions.NotificarErrorConfiguracionSistema("No se ha configurado el tiempo de viaje entre la oficinas: " & SessionManager.Origen_Ida & " - " & SessionManager.Destino_Ida & " , y servicio " & SessionManager.ServicioIda)
            End If
            Return respuesta

        Catch ex As Exception
            Log.Instance(GetType(destinos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function

#End Region

#Region "Metodos Privados con Eventos"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Me.Title = Functions.NombreTituloPagina(Me.Title)
        Try
            Dim oPLGlobals As New PLogic.PLGlobals
            If Not IsPostBack Then
                Dim errorreserv As String
                Dim errorrtrans As String
                If String.IsNullOrEmpty(Request.QueryString("reserva")) Then
                    errorreserv = String.Empty
                Else
                    errorreserv = Request.QueryString("reserva").ToString
                End If
                If String.IsNullOrEmpty(Request.QueryString("error")) Then
                    errorrtrans = String.Empty
                Else
                    errorrtrans = Request.QueryString("error").ToString
                End If

                If errorreserv = "0" Then
                    ScriptUser.JQueryMensaje(Me, Message.WAsientoEliminados, 1)
                End If
                If errorrtrans = "0" Then
                    ScriptUser.JQueryMensaje(Me, Message.WErrorTransaccion, 1)
                End If
                oPLGlobals.EliminarReservasVencidas()
                SessionUser.GlobalInitSession(Session)
                SessionUser.InitialTransaction()
                Functions.EliminarAsientobySession()
                oPLGlobals.EliminaReservasCaducadas()
            End If

            If CInt(Application("Err")) = 0 Then

                If SessionManager.ListarOficinasWebActivas Is Nothing Then
                    SessionManager.ListarOficinasWebActivas = oPLGlobals.ListarOficinasWebActivas()
                End If
                ''Dim itemorigen As New ListItem("Origen", "0")
                ''Dim itemdestino As New ListItem("Destino", "0")
                DDListDestino.DataSource = SessionManager.ListarOficinasWebActivas
                DDListDestino.DataBind()
                DDListOrigen.DataSource = SessionManager.ListarOficinasWebActivas
                DDListOrigen.DataBind()
                ''DDListOrigen.Items.Insert(0, itemorigen)
                ''DDListDestino.Items.Insert(0, itemdestino)
            Else
                ScriptUser.JQueryMensaje(Me, Message.EConexionServer, 0)
                Functions.NotificarErrorConfiguracionSistema(Message.EConexionServer)
            End If
            ''WUCViewUser1.Visible = False
        Catch ex As Exception
            Log.Instance(GetType(destinos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim valor As String = "1"
            iditinerarioida.Visible = False
            iditinerarioretorno.Visible = False
            contResultados.Visible = False
            btncontinuar.Visible = False
            btnretorini.Visible = False
            btnsoloidaoreto.Visible = False
            If Not IsPostBack Then
                If btnida.Checked AndAlso btnidaandretorno.Checked = False Then
                    txtfecharetorno.Visible = False
                    contRetorno.Visible = False
                    'ImageButton2.Visible = False
                ElseIf btnida.Checked = False AndAlso btnidaandretorno.Checked Then
                    txtfecharetorno.Visible = True
                    contRetorno.Visible = True
                    'ImageButton2.Visible = True
                Else
                    txtfecharetorno.Visible = True
                    contRetorno.Visible = True
                    'ImageButton2.Visible = True
                    ScriptUser.JQueryMensaje(Me, Message.WSelectTipoViaje, 1)
                End If

                ''inicio
                Dim OrigenId As String = String.Empty
                Dim DestinoId As String = String.Empty
                Dim TypeViaje As String = String.Empty
                Dim FechaIda As String = String.Empty
                Dim FechaRetorno As String = String.Empty
                Dim FechaActual As String = String.Empty

                If String.IsNullOrEmpty(Request.QueryString("idorigen")) Then
                    OrigenId = String.Empty
                Else
                    OrigenId = Request.QueryString("idorigen").ToString
                End If
                If String.IsNullOrEmpty(Request.QueryString("iddestino")) Then
                    DestinoId = String.Empty
                Else
                    DestinoId = Request.QueryString("iddestino").ToString
                End If
                If String.IsNullOrEmpty(Request.QueryString("viaje")) Then
                    TypeViaje = String.Empty
                Else
                    TypeViaje = Request.QueryString("viaje").ToString
                End If
                If String.IsNullOrEmpty(Request.QueryString("fechaida")) Then
                    FechaIda = String.Empty
                Else
                    FechaIda = Request.QueryString("fechaida").ToString.Replace("-", "/")
                End If
                If String.IsNullOrEmpty(Request.QueryString("fecharetorno")) Then
                    FechaRetorno = String.Empty
                Else
                    FechaRetorno = Request.QueryString("fecharetorno").ToString.Replace("-", "/")
                End If
                If String.IsNullOrEmpty(Request.QueryString("fechaactual")) Then
                    FechaActual = String.Empty
                Else
                    FechaActual = Request.QueryString("fechaactual").ToString.Replace("-", "/")
                End If

                If TypeViaje = "1" Then
                    If String.IsNullOrEmpty(OrigenId) = False AndAlso String.IsNullOrEmpty(DestinoId) = False AndAlso String.IsNullOrEmpty(FechaIda) = False Then
                        btnida.Checked = True
                        btnidaandretorno.Checked = False
                        DDListOrigen.SelectedValue = OrigenId
                        DDListDestino.SelectedValue = DestinoId
                        txtfechaida.Text = FechaIda
                        txtfecactual.Text = FechaActual
                        btnserachprog_Click(sender, New System.Web.UI.ImageClickEventArgs(0, 0))
                        valor = "2"
                    End If
                ElseIf TypeViaje = "2" Then
                    If String.IsNullOrEmpty(OrigenId) = False AndAlso String.IsNullOrEmpty(DestinoId) = False AndAlso String.IsNullOrEmpty(FechaIda) = False AndAlso String.IsNullOrEmpty(FechaRetorno) = False Then
                        btnida.Checked = False
                        btnidaandretorno.Checked = True
                        DDListOrigen.SelectedValue = OrigenId
                        DDListDestino.SelectedValue = DestinoId
                        txtfechaida.Text = FechaIda
                        txtfecharetorno.Text = FechaRetorno
                        txtfecactual.Text = FechaActual
                        btnserachprog_Click(sender, New System.Web.UI.ImageClickEventArgs(0, 0))
                        valor = "2"
                    End If
                Else
                    txtfechaida.Text = Now.ToShortDateString()
                    txtfecharetorno.Text = Now.ToShortDateString()
                    txtfecactual.Text = Now.ToShortDateString()
                End If
            End If
            btncontinuar.Visible = False
            btnretorini.Visible = False
            btnsoloidaoreto.Visible = False
            If Functions.UserLogueo(Me) Then
                WUCViewUser1.UserName = SessionManager.PerfilName
                WUCViewUser1.LogueoEnd = False
                WUCViewUser1.LogueoStart = True
            Else
                WUCViewUser1.LogueoEnd = True
                WUCViewUser1.LogueoStart = False
            End If
            'ScriptUser.JsRegister(Me, Me.ToString, "var type=" & valor & ";")
            ScriptUser.JsRegister(Me, Me.ToString, "Validation();")
        Catch ex As Exception
            Log.Instance(GetType(destinos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub btnserachprog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnserachprog.Click
        Try

            Errors()
            contResultados.Visible = True
            If btnida.Checked Then
                SessionManager.ViajeRetorno = False
            ElseIf btnidaandretorno.Checked Then
                SessionManager.ViajeRetorno = True
            End If

            SessionManager.OrigenId_Ida = Val(DDListOrigen.SelectedValue.ToString)
            SessionManager.DestinoId_Ida = Val(DDListDestino.SelectedValue.ToString)
            SessionManager.Origen_Ida = DDListOrigen.SelectedItem.ToString
            SessionManager.Destino_Ida = DDListDestino.SelectedItem.ToString

            SessionManager.DestinoId_Retorno = Val(DDListOrigen.SelectedValue.ToString)
            SessionManager.OrigenId_Retorno = Val(DDListDestino.SelectedValue.ToString)
            SessionManager.Destino_Retorno = DDListOrigen.SelectedItem.ToString
            SessionManager.Origen_Retorno = DDListDestino.SelectedItem.ToString

            gvrutasida.DataSource = Nothing
            gvrutasida.DataBind()
            gvrutasretorno.DataSource = Nothing
            gvrutasretorno.DataBind()
            LoadDataProg()

        Catch ex As Exception
            Log.Instance(GetType(destinos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Protected Sub gvrutasida_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvrutasida.SelectedIndexChanged
        contResultados.Visible = True
        If SessionManager.ViajeRetorno Then
            iditinerarioida.Visible = True
            iditinerarioretorno.Visible = True
        Else
            iditinerarioida.Visible = True
        End If
        Dim indice As Integer = 0
        If gvrutasida.PageIndex > 0 Then
            indice = gvrutasida.SelectedIndex + (gvrutasida.PageIndex * gvrutasida.PageSize)
        Else
            indice = gvrutasida.SelectedIndex
        End If

        Dim oETb_Maestro_Programacion As ETb_Maestro_Programacion = SessionManager.ListaItinerariosIda.Item(indice)
        Dim DiasViajeIda As Integer = 0
        SessionManager.ServicioIdIda = oETb_Maestro_Programacion.Codi_Servicio
        SessionManager.HoraViajeIda = oETb_Maestro_Programacion.Hora_Partida
        SessionManager.HoraLlegadaViajeIda = oETb_Maestro_Programacion.Hora_Llegada
        SessionManager.ServicioIda = oETb_Maestro_Programacion.Servicio
        SessionManager.IdPrecioNacionalidadIda = oETb_Maestro_Programacion.IdNacional
        SessionManager.Nro_ViajeIda = oETb_Maestro_Programacion.Nro_Viaje
        DiasViajeIda = oETb_Maestro_Programacion.Dias
        SessionManager.Codi_Sucursal_ida = oETb_Maestro_Programacion.Codi_Sucursal
        SessionManager.Codi_Destino_Ida = oETb_Maestro_Programacion.Codi_Destino
        SessionManager.Hora_Programacion_Ida = oETb_Maestro_Programacion.Hora
        SessionManager.IdEmpresaIda = oETb_Maestro_Programacion.Codi_Empresa
        SessionManager.Codi_puntoVenta = oETb_Maestro_Programacion.Codi_PuntoVenta
        SessionManager.Codi_PuntoVentaIda = oETb_Maestro_Programacion.Codi_PuntoVenta
        SessionManager.Cod_ProgramacionIda = oETb_Maestro_Programacion.Codi_Programacion
        SessionManager.IdBusIda = oETb_Maestro_Programacion.Codi_Bus
        SessionManager.PlanoIda = oETb_Maestro_Programacion.Codi_Plano
        SessionManager.NroPolizaBusIda = oETb_Maestro_Programacion.NroPolizaBus
        If DiasViajeIda > 0 Then
            SessionManager.Fecha_Programacion_Ida = DateAdd(DateInterval.Day, -DiasViajeIda, CDate(SessionManager.FechaDeViajeIda))
        Else
            SessionManager.Fecha_Programacion_Ida = SessionManager.FechaDeViajeIda
        End If
        Dim oListaAsientoNivel As ListaAsientoNivel = oETb_Maestro_Programacion.ListaAsientosDisponibles
        If oListaAsientoNivel.Count > 0 Then
            Dim oListaPlanoBus As ListaPlanoBus = objPLGlobls.ListarPlanoBus( SessionManager.PlanoIda)
            If oListaPlanoBus.Count > 0 Then
                Dim listaasientoida As ListaAsientoNivel = objPLGlobls.ListarAsientoNivelAll( SessionManager.IdBusIda)
                Dim listaida As ListaAsientoNivel = objPLGlobls.ListarAsientoNivelWeb( SessionManager.IdBusIda)
                If listaida.Count > 0 Then
                    Dim first As Boolean = False
                    For n As Integer = 0 To listaasientoida.Count - 1
                        If listaasientoida.Item(n).Nivel = "1" Then
                            first = True
                            Exit For
                        End If
                    Next
                    ''For n As Integer = 0 To listaida.Count - 1
                    ''    If listaida.Item(n).Nivel = "1" Then
                    ''        first = True
                    ''        Exit For
                    ''    End If
                    ''Next

                    If first = True Then
                        Dim listaidaall As ListaAsientoNivel = objPLGlobls.ListarAsientoNivelAll( SessionManager.IdBusIda)
                        Dim asiento As Boolean = False
                        Dim nasiento As String = String.Empty
                        ''For m As Integer = 0 To oListaPlanoBus.Count - 1
                        ''    If IsNumeric(oListaPlanoBus.Item(m).Tipo) Then
                        ''        asiento = False
                        ''        For j As Integer = 0 To listaidaall.Count - 1
                        ''            If CInt(oListaPlanoBus.Item(m).Tipo) = CInt(listaidaall.Item(j).Nume_Asiento) Then
                        ''                asiento = True
                        ''                Exit For
                        ''            End If
                        ''        Next
                        ''        If asiento = False Then
                        ''            nasiento = oListaPlanoBus.Item(m).Tipo
                        ''            Exit For
                        ''        End If
                        ''    End If
                        ''Next
                        asiento = True
                        If asiento Then
                            SessionManager.SelectDestinosIda = True
                            If Functions.RutaSelection Then
                                btncontinuar.Visible = True
                                'btnsoloidaoreto.Visible = True
                            End If
                        Else
                            SessionManager.SelectDestinosIda = False
                            Functions.NotificarErrorConfiguracionSistema("El asiento Nº :  " & nasiento & " del plano del bus " & SessionManager.IdBusIda & " no se encuentra configurado en la opcion de niveles de asientos del bus")
                            ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeError)
                            Exit Sub
                        End If
                    Else

                        SessionManager.SelectDestinosIda = False
                        Functions.NotificarErrorConfiguracionSistema("El busº " & SessionManager.IdBusIda & " no tiene ningun asiento asignado en el primer nivel, en la opcion de niveles de asientos del bus. No en el plano del bus en cuestion")
                        ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeError)
                        Exit Sub
                    End If
                Else

                    SessionManager.SelectDestinosIda = False
                    Functions.NotificarErrorConfiguracionSistema("No se ha configurado asientos para web en el busº " & SessionManager.IdBusIda)
                    ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeError)
                    Exit Sub
                End If
            Else

                SessionManager.SelectDestinosIda = False
                Functions.NotificarErrorConfiguracionSistema("No se encontrado el diseño del plano del bus " & SessionManager.IdBusIda & " , cuyo codigo de plano es: " & SessionManager.PlanoIda)
                ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeError)
                Exit Sub
            End If
        Else

            SessionManager.SelectDestinosIda = False
            ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeAsientoOcupada)
            Functions.NotificarErrorConfiguracionSistema("No se ha configurado asientos visibles para la web en el bus : " & SessionManager.IdBusIda)
            Exit Sub
        End If
        gvrutasida.Visible = True
        If btnidaandretorno.Checked = True Then
            If SessionManager.ListaItinerariosIda.Count > 0 AndAlso SessionManager.ListaItinerariosRetorno.Count = 0 AndAlso gvrutasretorno.DataSource Is Nothing Then
                btnsoloidaoreto.Visible = True
            Else
                btnsoloidaoreto.Visible = False
            End If
        End If

    End Sub

    Protected Sub gvrutasretorno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvrutasretorno.SelectedIndexChanged
        contResultados.Visible = True
        If SessionManager.ListaItinerariosIda.Count > 0 AndAlso SessionManager.ListaItinerariosRetorno.Count > 0 Then
            If SessionManager.ViajeRetorno Then
                iditinerarioida.Visible = True
                iditinerarioretorno.Visible = True
            Else
                iditinerarioida.Visible = True
            End If
            Dim indice As Integer = 0
            If gvrutasretorno.PageIndex > 0 Then
                indice = gvrutasretorno.SelectedIndex + (gvrutasretorno.PageIndex * gvrutasretorno.PageSize)
            Else
                indice = gvrutasretorno.SelectedIndex
            End If

            Dim oETb_Maestro_Programacion As ETb_Maestro_Programacion = SessionManager.ListaItinerariosRetorno.Item(indice)
            Dim DiasViajeRetorno As Integer = 0
            SessionManager.ServicioIdRetorno = oETb_Maestro_Programacion.Codi_Servicio
            SessionManager.HoraViajeRetorno = oETb_Maestro_Programacion.Hora_Partida
            SessionManager.HoraLlegadaViajeRetorno = oETb_Maestro_Programacion.Hora_Llegada
            SessionManager.ServicioRetorno = oETb_Maestro_Programacion.Servicio
            SessionManager.IdPrecioNacionalidadRetorno = oETb_Maestro_Programacion.IdNacional
            SessionManager.Nro_ViajeRetorno = oETb_Maestro_Programacion.Nro_Viaje
            DiasViajeRetorno = oETb_Maestro_Programacion.Dias
            SessionManager.Codi_Sucursal_Retorno = oETb_Maestro_Programacion.Codi_Sucursal
            SessionManager.Codi_Destino_Retorno = oETb_Maestro_Programacion.Codi_Destino
            SessionManager.Hora_Programacion_Retorno = oETb_Maestro_Programacion.Hora
            SessionManager.IdEmpresaRetorno = oETb_Maestro_Programacion.Codi_Empresa

            If DiasViajeRetorno > 0 Then
                SessionManager.Fecha_Programacion_Retorno = DateAdd(DateInterval.Day, -DiasViajeRetorno, CDate(SessionManager.FechaDeViajeRetorno))
            Else
                SessionManager.Fecha_Programacion_Retorno = SessionManager.FechaDeViajeRetorno
            End If
            SessionManager.Codi_PuntoVentaRetorno = oETb_Maestro_Programacion.Codi_PuntoVenta
            SessionManager.Cod_ProgramacionRetorno = oETb_Maestro_Programacion.Codi_Programacion
            SessionManager.IdBusRetorno = oETb_Maestro_Programacion.Codi_Bus
            SessionManager.PlanoRetorno = oETb_Maestro_Programacion.Codi_Plano
            SessionManager.NroPolizaBusRetorno = oETb_Maestro_Programacion.NroPolizaBus
            Dim oListaAsientoNivel As ListaAsientoNivel = oETb_Maestro_Programacion.ListaAsientosDisponibles
            If oListaAsientoNivel.Count > 0 Then
                Dim oListaPlanoBus As ListaPlanoBus = objPLGlobls.ListarPlanoBus( SessionManager.PlanoRetorno)
                If oListaPlanoBus.Count > 0 Then
                    Dim listaasientoretorno As ListaAsientoNivel = objPLGlobls.ListarAsientoNivelAll( SessionManager.IdBusRetorno)
                    Dim listaida As ListaAsientoNivel = objPLGlobls.ListarAsientoNivelWeb( SessionManager.IdBusRetorno)
                    If listaida.Count > 0 Then
                        Dim first As Boolean = False
                        For n As Integer = 0 To listaasientoretorno.Count - 1
                            If listaasientoretorno.Item(n).Nivel = "1" Then
                                first = True
                                Exit For
                            End If
                        Next
                        ''For n As Integer = 0 To listaida.Count - 1
                        ''    If listaida.Item(n).Nivel = "1" Then
                        ''        first = True
                        ''        Exit For
                        ''    End If
                        ''Next

                        If first = True Then
                            Dim listaidaall As ListaAsientoNivel = objPLGlobls.ListarAsientoNivelAll( SessionManager.IdBusRetorno)
                            Dim asiento As Boolean = False
                            Dim nasiento As String = String.Empty
                            ''For m As Integer = 0 To oListaPlanoBus.Count - 1
                            ''    If IsNumeric(oListaPlanoBus.Item(m).Tipo) Then
                            ''        asiento = False
                            ''        For j As Integer = 0 To listaidaall.Count - 1
                            ''            If CInt(oListaPlanoBus.Item(m).Tipo) = CInt(listaidaall.Item(j).Nume_Asiento) Then
                            ''                asiento = True
                            ''                Exit For
                            ''            End If
                            ''        Next
                            ''        If asiento = False Then
                            ''            nasiento = oListaPlanoBus.Item(m).Tipo
                            ''            Exit For
                            ''        End If
                            ''    End If
                            ''Next
                            asiento = True
                            If asiento Then

                                SessionManager.SelectDestinosRetorno = True
                                If Functions.RutaSelection Then
                                    btncontinuar.Visible = True
                                    btnretorini.Visible = True
                                    'btnsoloidaoreto.Visible = True
                                End If
                            Else

                                SessionManager.SelectDestinosRetorno = False
                                Functions.NotificarErrorConfiguracionSistema("El asiento Nº :  " & nasiento & " del plano del bus " & SessionManager.IdBusRetorno & " no se encuentra configurado en la opcion de niveles de asientos del bus")
                                ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeError)
                                Exit Sub
                            End If
                        Else

                            SessionManager.SelectDestinosRetorno = False
                            Functions.NotificarErrorConfiguracionSistema("El busº " & SessionManager.IdBusRetorno & " no tiene ningun asiento asignado en el primer nivel, en la opcion de niveles de asientos del bus. No en el plano del bus en cuestion")
                            ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeError)
                            Exit Sub
                        End If
                    Else

                        SessionManager.SelectDestinosRetorno = False
                        Functions.NotificarErrorConfiguracionSistema("No se ha configurado asientos para web en el busº " & SessionManager.IdBusRetorno)
                        ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeError)
                        Exit Sub
                    End If
                Else

                    SessionManager.SelectDestinosRetorno = False
                    Functions.NotificarErrorConfiguracionSistema("No se encontrado el diseño del plano del bus " & SessionManager.IdBusRetorno & " , cuyo codigo de plano es: " & SessionManager.PlanoIda)
                    ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeError)
                    Exit Sub
                End If
            Else

                SessionManager.SelectDestinosRetorno = False
                ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeAsientoOcupada)
                Functions.NotificarErrorConfiguracionSistema("No se ha configurado asientos visibles para la web en el bus : " & SessionManager.IdBusRetorno)
                Exit Sub
            End If
            btncontinuar.Visible = True
            btnretorini.Visible = True
            ''SE ADICIONO EL CODIGO POR WEMO_15092015
        ElseIf SessionManager.ListaItinerariosIda.Count = 0 AndAlso SessionManager.ListaItinerariosRetorno.Count > 0 Then


            If SessionManager.ViajeRetorno Then
                iditinerarioida.Visible = True
                iditinerarioretorno.Visible = True
            Else
                iditinerarioida.Visible = True
            End If
            Dim indice As Integer = 0
            If gvrutasretorno.PageIndex > 0 Then
                indice = gvrutasretorno.SelectedIndex + (gvrutasretorno.PageIndex * gvrutasretorno.PageSize)
            Else
                indice = gvrutasretorno.SelectedIndex
            End If

            Dim oETb_Maestro_Programacion As ETb_Maestro_Programacion = SessionManager.ListaItinerariosRetorno.Item(indice)
            Dim DiasViajeIda As Integer = 0
            SessionManager.ServicioIdIda = oETb_Maestro_Programacion.Codi_Servicio
            SessionManager.HoraViajeIda = oETb_Maestro_Programacion.Hora_Partida
            SessionManager.HoraLlegadaViajeIda = oETb_Maestro_Programacion.Hora_Llegada
            SessionManager.ServicioIda = oETb_Maestro_Programacion.Servicio
            SessionManager.IdPrecioNacionalidadIda = oETb_Maestro_Programacion.IdNacional
            SessionManager.Nro_ViajeIda = oETb_Maestro_Programacion.Nro_Viaje
            DiasViajeIda = oETb_Maestro_Programacion.Dias
            SessionManager.Codi_Sucursal_ida = oETb_Maestro_Programacion.Codi_Sucursal
            SessionManager.Codi_Destino_Ida = oETb_Maestro_Programacion.Codi_Destino
            SessionManager.Hora_Programacion_Ida = oETb_Maestro_Programacion.Hora
            SessionManager.IdEmpresaIda = oETb_Maestro_Programacion.Codi_Empresa
            SessionManager.Codi_puntoVenta = oETb_Maestro_Programacion.Codi_PuntoVenta
            SessionManager.Cod_ProgramacionIda = oETb_Maestro_Programacion.Codi_Programacion
            SessionManager.IdBusIda = oETb_Maestro_Programacion.Codi_Bus
            SessionManager.PlanoIda = oETb_Maestro_Programacion.Codi_Plano


            If DiasViajeIda > 0 Then
                SessionManager.Fecha_Programacion_Ida = DateAdd(DateInterval.Day, -DiasViajeIda, CDate(SessionManager.FechaDeViajeIda))
            Else
                SessionManager.Fecha_Programacion_Ida = SessionManager.FechaDeViajeIda
            End If
            Dim oListaAsientoNivel As ListaAsientoNivel = oETb_Maestro_Programacion.ListaAsientosDisponibles
            If oListaAsientoNivel.Count > 0 Then
                Dim oListaPlanoBus As ListaPlanoBus = objPLGlobls.ListarPlanoBus( SessionManager.PlanoIda)
                If oListaPlanoBus.Count > 0 Then
                    Dim listaasientoida As ListaAsientoNivel = objPLGlobls.ListarAsientoNivelAll( SessionManager.IdBusIda)
                    Dim listaida As ListaAsientoNivel = objPLGlobls.ListarAsientoNivelWeb( SessionManager.IdBusIda)
                    If listaida.Count > 0 Then
                        Dim first As Boolean = False
                        For n As Integer = 0 To listaasientoida.Count - 1
                            If listaasientoida.Item(n).Nivel = "1" Then
                                first = True
                                Exit For
                            End If
                        Next
                        ''For n As Integer = 0 To listaida.Count - 1
                        ''    If listaida.Item(n).Nivel = "1" Then
                        ''        first = True
                        ''        Exit For
                        ''    End If
                        ''Next

                        If first = True Then
                            Dim listaidaall As ListaAsientoNivel = objPLGlobls.ListarAsientoNivelAll( SessionManager.IdBusIda)
                            Dim asiento As Boolean = False
                            Dim nasiento As String = String.Empty
                            For m As Integer = 0 To oListaPlanoBus.Count - 1
                                If IsNumeric(oListaPlanoBus.Item(m).Tipo) Then
                                    asiento = False
                                    For j As Integer = 0 To listaidaall.Count - 1
                                        If CInt(oListaPlanoBus.Item(m).Tipo) = CInt(listaidaall.Item(j).Nume_Asiento) Then
                                            asiento = True
                                            Exit For
                                        End If
                                    Next
                                    If asiento = False Then
                                        nasiento = oListaPlanoBus.Item(m).Tipo
                                        Exit For
                                    End If
                                End If
                            Next
                            If asiento Then
                                SessionManager.SelectDestinosIda = True
                                If Functions.RutaSelection Then
                                    btncontinuar.Visible = True
                                    btnretorini.Visible = True
                                    'btnsoloidaoreto.Visible = True
                                End If
                            Else
                                SessionManager.SelectDestinosIda = False
                                Functions.NotificarErrorConfiguracionSistema("El asiento Nº :  " & nasiento & " del plano del bus " & SessionManager.IdBusIda & " no se encuentra configurado en la opcion de niveles de asientos del bus")
                                ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeError)
                                Exit Sub
                            End If
                        Else

                            SessionManager.SelectDestinosIda = False
                            Functions.NotificarErrorConfiguracionSistema("El busº " & SessionManager.IdBusIda & " no tiene ningun asiento asignado en el primer nivel, en la opcion de niveles de asientos del bus. No en el plano del bus en cuestion")
                            ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeError)
                            Exit Sub
                        End If
                    Else

                        SessionManager.SelectDestinosIda = False
                        Functions.NotificarErrorConfiguracionSistema("No se ha configurado asientos para web en el busº " & SessionManager.IdBusIda)
                        ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeError)
                        Exit Sub
                    End If
                Else

                    SessionManager.SelectDestinosIda = False
                    Functions.NotificarErrorConfiguracionSistema("No se encontrado el diseño del plano del bus " & SessionManager.IdBusIda & " , cuyo codigo de plano es: " & SessionManager.PlanoIda)
                    ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeError)
                    Exit Sub
                End If
            Else

                SessionManager.SelectDestinosIda = False
                ScriptUser.JQueryMensaje(Me, Message.WHorarioViajeAsientoOcupada)
                Functions.NotificarErrorConfiguracionSistema("No se ha configurado asientos visibles para la web en el bus : " & SessionManager.IdBusIda)
                Exit Sub
            End If
            gvrutasida.Visible = True
            'btncontinuar.Visible = True
            btnsoloidaoreto.Visible = True
            btnretorini.Visible = True
        End If
    End Sub

    Protected Sub btncontinuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncontinuar.Click
        Try
            If SessionManager.ViajeRetorno = False AndAlso SessionManager.SelectDestinosIda = True Then
                SessionManager.SelectionRuta = True
                Response.Redirect("seleccionarasientos.aspx#bus", False)
                Exit Sub
            Else
                If SessionManager.ViajeRetorno = True AndAlso SessionManager.SelectDestinosIda = True AndAlso SessionManager.SelectDestinosRetorno = True Then
                    SessionManager.SelectionRuta = True
                    Response.Redirect("seleccionarasientos.aspx#bus", False)
                    Exit Sub
                Else
                    If SessionManager.ViajeRetorno = True AndAlso SessionManager.SelectDestinosIda = False AndAlso SessionManager.SelectDestinosRetorno Then
                        ScriptUser.JQueryMensaje(Me, Message.WSelectHorarioViajeIda, 1)
                        Exit Sub
                    ElseIf SessionManager.ViajeRetorno = True AndAlso SessionManager.SelectDestinosIda AndAlso SessionManager.SelectDestinosRetorno = False Then
                        ScriptUser.JQueryMensaje(Me, Message.WSelectHorarioViajeRetorno, 1)
                        Exit Sub
                    ElseIf SessionManager.ViajeRetorno = True AndAlso SessionManager.SelectDestinosIda = False AndAlso SessionManager.SelectDestinosRetorno = False Then
                        ScriptUser.JQueryMensaje(Me, Message.WSelectHorarioViajeIda_Retorno, 1)
                        Exit Sub
                    End If
                End If
                ScriptUser.JQueryMensaje(Me, Message.WSelectHorarioViaje, 1)
                Exit Sub
            End If

        Catch ex As Exception
            Log.Instance(GetType(destinos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub gvrutasida_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvrutasida.PageIndexChanging
        contResultados.Visible = True
        Try
            gvrutasida.EditIndex = -1
            gvrutasida.PageIndex = e.NewPageIndex()
            LoadDataProg()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub gvrutasretorno_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvrutasretorno.PageIndexChanging
        contResultados.Visible = True
        Try
            gvrutasretorno.EditIndex = -1
            gvrutasretorno.PageIndex = e.NewPageIndex()
            gvrutasretorno.DataSource = SessionManager.ListaItinerariosRetorno
            LoadDataProg()
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Protected Sub btnretorini_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnretorini.Click
        PortalPrincipal()
    End Sub

    Private Sub PortalPrincipal()
        Dim strportalini As String = String.Empty
        strportalini = AppSettings.valueString("PortalPrincipal").ToString
        Response.Redirect(strportalini, False)
    End Sub

    Protected Sub btnsoloidaoreto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsoloidaoreto.Click
        Try
            If btnidaandretorno.Checked = True Then
                SessionManager.ViajeRetorno = False
            End If

            If SessionManager.ListaItinerariosIda.Count > 0 AndAlso SessionManager.ListaItinerariosRetorno.Count = 0 Then
                SessionManager.OrigenId_Ida = DDListOrigen.SelectedValue.ToString
                SessionManager.DestinoId_Ida = DDListDestino.SelectedValue.ToString
                SessionManager.Origen_Ida = DDListOrigen.SelectedItem.ToString
                SessionManager.Destino_Ida = DDListDestino.SelectedItem.ToString
                SessionManager.FechaDeViajeIda = txtfechaida.Text
            ElseIf SessionManager.ListaItinerariosRetorno.Count > 0 AndAlso SessionManager.ListaItinerariosIda.Count = 0 Then
                SessionManager.OrigenId_Ida = DDListDestino.SelectedValue.ToString
                SessionManager.DestinoId_Ida = DDListOrigen.SelectedValue.ToString
                SessionManager.Origen_Ida = DDListDestino.SelectedItem.ToString
                SessionManager.Destino_Ida = DDListOrigen.SelectedItem.ToString
                SessionManager.FechaDeViajeIda = txtfecharetorno.Text
            End If
            objPLGlobls = New PLGlobals()
            If SessionManager.ViajeRetorno = True AndAlso SessionManager.Promociones = True Then
                SessionManager.TransactionPromociones = True
            Else
                SessionManager.TransactionPromociones = False
            End If

            'lblrutaida.Text = SessionManager.Origen_Ida & " - " & SessionManager.Destino_Ida
            SessionManager.ListaItinerariosIda = objPLGlobls.ListarItinerario( SessionManager.OrigenId_Ida, SessionManager.DestinoId_Ida, 0, 1, SessionManager.FechaDeViajeIda, SessionManager.Promociones)

            If SessionManager.ViajeRetorno = False AndAlso SessionManager.SelectDestinosIda = True Then
                SessionManager.SelectionRuta = True
                Response.Redirect("seleccionarasientos.aspx#bus", False)
                Exit Sub
            ElseIf SessionManager.ViajeRetorno = False AndAlso SessionManager.SelectDestinosRetorno = True Then
                SessionManager.SelectionRuta = True
                Response.Redirect("seleccionarasientos.aspx#bus", False)
                Exit Sub
            End If
        Catch ex As Exception
            Log.Instance(GetType(destinos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        ScriptUser.JQueryMensaje(Me, Message.EPage, 0)
    End Sub



#End Region



End Class
