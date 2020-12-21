Imports System.Data
Imports PLogic
Imports PEntity
Imports System.Collections.Generic
Imports System.IO
Imports PUtilitario

Partial Class seleccionarasientos
    Inherits System.Web.UI.Page

    Public nADisponibleJsIda As String = String.Empty
    Public nADisponibleJsRetorno As String = String.Empty

    Public nASelecionadoJsIda As String = String.Empty
    Public nASelecionadoJsRetorno As String = String.Empty
    Dim status As Boolean = True

#Region "Metodos Privados con Eventos"

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Me.Title = Functions.NombreTituloPagina(Me.Title)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If SessionManager.FechaDeViajeIda Is Nothing Then
                status = False
                Response.Redirect("end.aspx", False)
                Exit Sub
            End If
            Dim p As String = String.Empty
            If Functions.ValidarSeleccionAsientos(p) = False Then
                Response.Redirect(p, False)
                Exit Sub
            End If
            PermisosObjetos()
            Functions.ActualizarProgramacion()
            If Not IsPostBack Then
                If SessionManager.isReservando Then
                    SessionManager.isReservando = False
                    Dim oPLGlobals As New PLGlobals
                    oPLGlobals.EliminaReservas(SessionManager.Id_WebOrders)
                    SessionManager.Id_WebOrders = 0
                    SessionManager.ListaAsientoWebIda = Nothing
                    SessionManager.ListaAsientoWebRetorno = Nothing
                    Functions.LiberarAsientobySession()
                End If
                PageLoader()
            End If
            If Functions.UserLogueo(Me) Then
                WUCViewUser1.UserName = SessionManager.PerfilName
                WUCViewUser1.LogueoEnd = False
                WUCViewUser1.LogueoStart = True
            Else
                WUCViewUser1.LogueoEnd = True
                WUCViewUser1.LogueoStart = False
            End If
            ScriptUser.JsRegister(Me, Me.ToString, "HiddenElementos()")

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub ddlmonedasida_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlmonedasida.SelectedIndexChanged
        Try
            If status Then
                ''ConvertidoMontosIda()
                CargarAsientos()
            End If

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub ddlmonedasretorno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlmonedasretorno.SelectedIndexChanged
        Try
            If status Then
                ConvertidoMontosRetorno()
                CargarAsientos()
            End If

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub btncostoida1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncostoida1.Click

        Try
            If status Then
                If IsPostBack Then
                    FlagTab.Value = 0
                    If String.IsNullOrEmpty(lblprecionivel1ida.Text) = False Then
                        If IsNumeric(lblprecionivel1ida.Text) Then
                            If lblprecionivel1ida.Text > 0 Then
                                Dim precio As Decimal = RecalcularPrecioAsiento(SessionManager.IdEmpresaIda, SessionManager.OrigenId_Ida, SessionManager.DestinoId_Ida, SessionManager.ServicioIdIda, SessionManager.HoraViajeIda, SessionManager.FechaDeViajeIda, nasientoida.Value, CDbl(lblprecionivel1ida.Text))
                                BloquearAsiento(SessionManager.Nro_ViajeIda, nasientoida.Value, SessionManager.Fecha_Programacion_Ida, SessionManager.FechaDeViajeIda, precio, "1" & SessionManager.IDCID, 1, SessionManager.Cod_ProgramacionIda)
                                Exit Sub
                            End If
                        End If
                    End If
                    CargarAsientos()
                    CargarInformaciondeViajes()
                    ScriptUser.JQueryMensaje(Me, Message.WPrecioNoConfiguradoAsientos, 1)
                End If
            End If


        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub btncostoida2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncostoida2.Click


        Try
            If status Then
                If IsPostBack Then
                    FlagTab.Value = 0
                    If String.IsNullOrEmpty(lblprecionivel2ida.Text) = False Then
                        If IsNumeric(lblprecionivel2ida.Text) Then
                            If lblprecionivel2ida.Text > 0 Then
                                Dim precio As Decimal = RecalcularPrecioAsiento(SessionManager.IdEmpresaIda, SessionManager.OrigenId_Ida, SessionManager.DestinoId_Ida, SessionManager.ServicioIdIda, SessionManager.HoraViajeIda, SessionManager.FechaDeViajeIda, nasientoida.Value, CDbl(lblprecionivel2ida.Text))
                                BloquearAsiento(SessionManager.Nro_ViajeIda, nasientoida.Value, SessionManager.Fecha_Programacion_Ida, SessionManager.FechaDeViajeIda, precio, "1" & SessionManager.IDCID, 1, SessionManager.Cod_ProgramacionIda)
                                Exit Sub
                            End If
                        End If
                    End If
                    CargarAsientos()
                    CargarInformaciondeViajes()
                    ScriptUser.JQueryMensaje(Me, Message.WPrecioNoConfiguradoAsientos, 1)
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub btncostoretorno1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncostoretorno1.Click
        Try
            If status Then
                If IsPostBack Then
                    FlagTab.Value = 1
                    If String.IsNullOrEmpty(lblprecionivel1retorno.Text) = False Then
                        If IsNumeric(lblprecionivel1retorno.Text) Then
                            If lblprecionivel1retorno.Text > 0 Then
                                Dim precio As Decimal = RecalcularPrecioAsiento(SessionManager.IdEmpresaRetorno, SessionManager.OrigenId_Retorno, SessionManager.DestinoId_Retorno, SessionManager.ServicioIdRetorno, SessionManager.HoraViajeRetorno, SessionManager.FechaDeViajeRetorno, nasientoretorno.Value, CDbl(lblprecionivel1retorno.Text))
                                BloquearAsiento(SessionManager.Nro_ViajeRetorno, nasientoretorno.Value, SessionManager.Fecha_Programacion_Retorno, SessionManager.FechaDeViajeRetorno, precio, "2" & SessionManager.IDCID, 2, SessionManager.Cod_ProgramacionRetorno)
                                Exit Sub
                            End If
                        End If
                    End If
                    CargarAsientos()
                    CargarInformaciondeViajes()
                    ScriptUser.JQueryMensaje(Me, Message.WPrecioNoConfiguradoAsientos, 1)
                End If
            End If
            ScriptUser.JQueryEjecutarFuncion(Me, "tabRetorno")
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub btncostoretorno2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncostoretorno2.Click

        Try
            If status Then
                If IsPostBack Then
                    FlagTab.Value = 1
                    If String.IsNullOrEmpty(lblprecionivel2retorno.Text) = False Then
                        If IsNumeric(lblprecionivel2retorno.Text) Then
                            If lblprecionivel2retorno.Text > 0 Then
                                Dim precio As Decimal = RecalcularPrecioAsiento(SessionManager.IdEmpresaRetorno, SessionManager.OrigenId_Retorno, SessionManager.DestinoId_Retorno, SessionManager.ServicioIdRetorno, SessionManager.HoraViajeRetorno, SessionManager.FechaDeViajeRetorno, nasientoretorno.Value, CDbl(lblprecionivel2retorno.Text))
                                BloquearAsiento(SessionManager.Nro_ViajeRetorno, nasientoretorno.Value, SessionManager.Fecha_Programacion_Retorno, SessionManager.FechaDeViajeRetorno, precio, "2" & SessionManager.IDCID, 2, SessionManager.Cod_ProgramacionRetorno)
                                Exit Sub
                            End If
                        End If
                    End If
                    CargarAsientos()
                    CargarInformaciondeViajes()
                    ScriptUser.JQueryMensaje(Me, Message.WPrecioNoConfiguradoAsientos, 1)
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        ScriptUser.JQueryMensaje(Me, ErrorPage)
    End Sub

    Protected Sub Page_PreRenderComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRenderComplete
        ScriptUser.JsAjaxRegister(Me)
    End Sub

    Protected Sub gvasientoseleccionadosida_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvasientoseleccionadosida.PageIndexChanging

        'Try
        '    If status Then
        '        If Page.IsPostBack Then
        '            gvasientoseleccionadosida.PageIndex = e.NewPageIndex()
        '            CargarAsientos()
        '        End If
        '    End If

        'Catch ex As Exception
        '    If String.IsNullOrEmpty(Variables.ErrorMetodo) Then
        '        Variables.ErrorMetodo = "gvasientoseleccionadosida_PageIndexChanging()"
        '    End If
        '    Functions.ControlarException(ex)
        'End Try
    End Sub

    Protected Sub gvasientoseleccionadosida_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvasientoseleccionadosida.RowDeleting

        Try
            If status Then
                If gvasientoseleccionadosida.Rows.Count < 1 Then
                    e.Cancel = True
                    Exit Sub
                End If
                If IsPostBack Then
                    e.Cancel = True
                    DeleteAsiento(gvasientoseleccionadosida.DataKeys.Item(e.RowIndex).Value)
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub gvasientoseleccionadosretorno_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvasientoseleccionadosretorno.PageIndexChanging

        'Try
        '    If status Then
        '        If Page.IsPostBack Then
        '            gvasientoseleccionadosretorno.PageIndex = e.NewPageIndex()
        '            CargarAsientos()
        '        End If
        '    End If

        'Catch ex As Exception
        '    If String.IsNullOrEmpty(Variables.ErrorMetodo) Then
        '        Variables.ErrorMetodo = "gvasientoseleccionadosretorno_PageIndexChanging()"
        '    End If
        '    Functions.ControlarException(ex)
        'End Try
    End Sub

    Protected Sub gvasientoseleccionadosretorno_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvasientoseleccionadosretorno.RowDeleting

        Try
            If status Then
                If gvasientoseleccionadosretorno.Rows.Count < 1 Then
                    e.Cancel = True
                    Exit Sub
                End If
                If IsPostBack Then
                    e.Cancel = True
                    DeleteAsiento(gvasientoseleccionadosretorno.DataKeys.Item(e.RowIndex).Value)
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub



    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefreshuser.Click

        Try
            If status Then
                If Functions.UserLogueo(Me) Then
                    WUCViewUser1.UserName = SessionManager.PerfilName
                    WUCViewUser1.LogueoEnd = False
                    WUCViewUser1.LogueoStart = True
                    CargarAsientos()
                Else
                    WUCViewUser1.LogueoEnd = True
                    WUCViewUser1.LogueoStart = False
                End If
            End If
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub btncontinuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncontinuar.Click
        Try
            If status Then
                If SessionManager.ViajeRetorno Then
                    If SessionManager.CantidadAsientosIda > 0 AndAlso SessionManager.CantidadAsientosRetorno > 0 Then
                        SessionManager.SelectionAsientos = True
                        Response.Redirect("asignarpasajero.aspx#registro", False)
                    Else
                        ScriptUser.JQueryMensaje(Me, Message.WSelectAsientos, 1)
                    End If
                ElseIf SessionManager.ViajeRetorno = False Then
                    If SessionManager.CantidadAsientosIda > 0 Then
                        SessionManager.SelectionAsientos = True
                        Response.Redirect("asignarpasajero.aspx#registro", False)
                    Else
                        ScriptUser.JQueryMensaje(Me, Message.WSelectAsientos, 1)
                    End If
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub btnregresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnregresar.Click
        Response.Redirect("destinos.aspx")
    End Sub

    Protected Sub btndeleteida_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndeleteida.Click
        Dim oPLGlobals As New PLGlobals
        Dim oListaAsiento As ListaAsientos = oPLGlobals.ListarAsientosSeleccionados("1" & SessionManager.IDCID, SessionManager.Cod_ProgramacionIda, SessionManager.IdBusIda)

        If oListaAsiento.Count > 0 Then
            For x As Integer = 0 To oListaAsiento.Count - 1
                With oListaAsiento.Item(x)

                    If CInt(.Nume_Asiento) = CInt(nasientoida.Value) Then
                        DeleteAsiento(.IDS)
                        Exit For
                    End If
                End With
            Next
        End If
    End Sub

    Protected Sub btndeleteretorno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndeleteretorno.Click
        Dim oPLGlobals As New PLGlobals
        Dim oListaAsiento As ListaAsientos = oPLGlobals.ListarAsientosSeleccionados("2" & SessionManager.IDCID, SessionManager.Cod_ProgramacionIda, SessionManager.IdBusIda)
        If oListaAsiento.Count > 0 Then
            For x As Integer = 0 To oListaAsiento.Count - 1
                With oListaAsiento.Item(x)
                    If CInt(.Nume_Asiento) = CInt(nasientoretorno.Value) Then
                        DeleteAsiento(.IDS)
                        Exit For
                    End If
                End With
            Next
        End If
    End Sub

    Protected Sub gvasientoseleccionadosretornomobile_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvasientoseleccionadosretornomobile.RowDeleting
        Try
            If status Then
                If gvasientoseleccionadosretornomobile.Rows.Count < 1 Then
                    e.Cancel = True
                    Exit Sub
                End If
                If IsPostBack Then
                    e.Cancel = True
                    DeleteAsiento(gvasientoseleccionadosretornomobile.DataKeys.Item(e.RowIndex).Value)
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub gvasientoseleccionadosidamobile_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvasientoseleccionadosidamobile.RowDeleting
        Try
            If status Then
                If gvasientoseleccionadosidamobile.Rows.Count < 1 Then
                    e.Cancel = True
                    Exit Sub
                End If
                If IsPostBack Then
                    e.Cancel = True
                    DeleteAsiento(gvasientoseleccionadosidamobile.DataKeys.Item(e.RowIndex).Value)
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

#End Region


#Region "Metodos Privados"

    Private Sub PermisosObjetos()

        Try
            Dim oPLGlobals As New PLGlobals
            If SessionManager.ListaMonedas Is Nothing Then
                SessionManager.ListaMonedas = oPLGlobals.ListarMonedas()
            ElseIf SessionManager.ListaMonedas.Count = 0 Then
                SessionManager.ListaMonedas = oPLGlobals.ListarMonedas()
            End If
            If Not IsPostBack Then
                ddlmonedasida.DataSource = SessionManager.ListaMonedas
                ddlmonedasida.DataBind()
            End If
            vista_detalle_mobile_ida.Visible = False
            vista_detalle_mobile_retorno.Visible = False

            If SessionManager.ViajeRetorno Then
                'dataitinerarioretornotitle.Visible = True
                'dataitinerarioretornohead.Visible = True
                'dataitinerarioretorno.Visible = True
                'dataitinerarioidatitle.Visible = True
                'dataitinerarioidahead.Visible = True
                'dataitinerarioida.Visible = True
                'lbldatosida.Text = Message.TitleRutaIda

                croquisida.Visible = True
                croquisretorno.Visible = True
                ''vista_detalle_mobile_retorno.Visible = True
                lbldatosretorno.Visible = True
                If Not IsPostBack Then
                    ddlmonedasretorno.DataSource = SessionManager.ListaMonedas
                    ddlmonedasretorno.DataBind()
                End If
            Else
                'dataitinerarioretornotitle.Visible = False
                'dataitinerarioretornohead.Visible = False
                'dataitinerarioretorno.Visible = False
                'dataitinerarioidatitle.Visible = True
                'dataitinerarioidahead.Visible = True
                'dataitinerarioida.Visible = True
                'lbldatosida.Text = Message.TitleRuta
                croquisida.Visible = True
                croquisretorno.Visible = False
                lbldatosretorno.Visible = False
            End If
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Sub PageLoader()

        Try
            CargarAsientos()
            If Not IsPostBack Then
                CargarInformaciondeViajes()
                CargarPlano()
                ViajeDetalleCosto()
            End If
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Sub CargarAsientos()

        Try
            FillGirdVAsientosSelecionados()
            If SessionManager.ViajeRetorno Then
                CargarAsientosIda()
                CargarAsientosRetorno()
            ElseIf SessionManager.ViajeRetorno = False Then
                CargarAsientosIda()
            End If
            CargarBotones()
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Public Sub FillGirdVAsientosSelecionados()

        Try
            FillGirdVAsientosSelecionadosIda("1" & SessionManager.IDCID)
            If SessionManager.ViajeRetorno Then
                FillGirdVAsientosSelecionadosRetorno("2" & SessionManager.IDCID)
                If SessionManager.CantidadAsientosIda > 0 AndAlso SessionManager.CantidadAsientosRetorno > 0 Then
                    btncontinuar.Visible = True
                Else
                    btncontinuar.Visible = False
                End If
            Else
                If SessionManager.CantidadAsientosIda > 0 Then
                    btncontinuar.Visible = True
                Else
                    btncontinuar.Visible = False
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Public Sub FillGirdVAsientosSelecionadosIda(ByVal IDCID As String)

        Try
            nASelecionadoJsIda = String.Empty
            Dim oPLGlobals As New PLGlobals()
            Dim oListaAsiento As ListaAsientos = oPLGlobals.ListarAsientosSeleccionados( IDCID, SessionManager.Cod_ProgramacionIda, SessionManager.IdBusIda)
            With oListaAsiento
                For x As Integer = 1 To .Count
                    nASelecionadoJsIda = nASelecionadoJsIda & "'1" & .Item(x - 1).Nivel & Format(Val(.Item(x - 1).Nume_Asiento), "0#") & "',"
                Next
            End With
            If Not SessionManager.ListaAsientoWebIda Is Nothing Then
                If SessionManager.ListaAsientoWebIda.Count > 0 Then
                    Dim estado As Boolean = True
                    For n As Int16 = 0 To SessionManager.ListaAsientoWebIda.Count - 1
                        If n < SessionManager.ListaAsientoWebIda.Count Then
                            If oListaAsiento.Count > 0 Then
                                For j As Int16 = 0 To oListaAsiento.Count - 1
                                    If SessionManager.ListaAsientoWebIda.Item(n).IDS = oListaAsiento.Item(j).IDS Then
                                        estado = False
                                        Exit For
                                    End If
                                Next
                                If estado Then
                                    SessionManager.ListaAsientoWebIda.RemoveAt(n)
                                    n -= 1
                                Else
                                    estado = True
                                End If
                            Else
                                SessionManager.ListaAsientoWebIda.RemoveAt(n)
                                n -= 1
                            End If
                        Else
                            Exit For
                        End If
                    Next
                End If
                If oListaAsiento.Count > 0 Then
                    For j As Int16 = 0 To oListaAsiento.Count - 1
                        Dim estado As Boolean = True
                        For m As Int16 = 0 To SessionManager.ListaAsientoWebIda.Count - 1
                            If SessionManager.ListaAsientoWebIda.Item(m).IDS = oListaAsiento.Item(j).IDS Then
                                estado = False
                            End If
                        Next
                        If estado = True Then
                            Dim oEAsiento As New EAsiento
                            oEAsiento.IDS = oListaAsiento.Item(j).IDS
                            oEAsiento.Nume_Asiento = oListaAsiento.Item(j).Nume_Asiento
                            oEAsiento.Costo = oListaAsiento.Item(j).Costo
                            oEAsiento.Nivel = oListaAsiento.Item(j).Nivel
                            SessionManager.ListaAsientoWebIda.Add(oEAsiento)
                        Else
                            estado = True
                        End If
                    Next
                End If
                If SessionManager.ListaAsientoWebIda.Count > 0 Then
                    For k As Int16 = 0 To SessionManager.ListaAsientoWebIda.Count - 1
                        SessionManager.ListaAsientoWebIda.Item(k).Item = k + 1
                    Next
                End If
            End If

            gvasientoseleccionadosida.DataSource = oListaAsiento
            gvasientoseleccionadosida.DataBind()

            ''Datos Mobiles

            gvasientoseleccionadosidamobile.DataSource = oListaAsiento
            gvasientoseleccionadosidamobile.DataBind()

            If oListaAsiento.Count > 0 Then
                vista_detalle_mobile_ida.Visible = True
            Else
                vista_detalle_mobile_ida.Visible = False
            End If

            Dim oListaAsientos As ListaAsientos = oPLGlobals.CantidadAsientoReservadosbSession( IDCID)
            If oListaAsientos.Count > 0 Then
                SessionManager.CantidadAsientosIda = oListaAsientos.Item(0).Cantidad
                SessionManager.MontoIda = oListaAsientos.Item(0).Total
            Else
                SessionManager.CantidadAsientosIda = 0
                SessionManager.MontoIda = 0
            End If
            SessionManager.CantidadAsientos = SessionManager.CantidadAsientosIda + SessionManager.CantidadAsientosRetorno
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try


    End Sub

    Public Sub FillGirdVAsientosSelecionadosRetorno(ByVal IDCID As String)
        Try
            nASelecionadoJsRetorno = ""
            Dim oPLGlobals As New PLGlobals()
            Dim oListaAsiento As ListaAsientos = oPLGlobals.ListarAsientosSeleccionados( IDCID, SessionManager.Cod_ProgramacionRetorno, SessionManager.IdBusRetorno)
            With oListaAsiento
                For x As Integer = 1 To .Count
                    nASelecionadoJsRetorno = nASelecionadoJsRetorno & "'2" & .Item(x - 1).Nivel & Format(Val(.Item(x - 1).Nume_Asiento), "0#") & "',"
                Next
            End With
            If Not SessionManager.ListaAsientoWebRetorno Is Nothing Then
                If SessionManager.ListaAsientoWebRetorno.Count > 0 Then
                    Dim estado As Boolean = True
                    For n As Int16 = 0 To SessionManager.ListaAsientoWebRetorno.Count - 1
                        If n < SessionManager.ListaAsientoWebRetorno.Count Then
                            If oListaAsiento.Count > 0 Then
                                For j As Int16 = 0 To oListaAsiento.Count - 1
                                    If SessionManager.ListaAsientoWebRetorno.Item(n).IDS = oListaAsiento.Item(j).IDS Then
                                        estado = False
                                        Exit For
                                    End If
                                Next
                                If estado Then
                                    SessionManager.ListaAsientoWebRetorno.RemoveAt(n)
                                    n -= 1
                                Else
                                    estado = True
                                End If
                            Else
                                SessionManager.ListaAsientoWebRetorno.RemoveAt(n)
                                n -= 1
                            End If
                        Else
                            Exit For
                        End If
                    Next
                End If
                If oListaAsiento.Count > 0 Then
                    For j As Int16 = 0 To oListaAsiento.Count - 1
                        Dim estado As Boolean = True
                        For m As Int16 = 0 To SessionManager.ListaAsientoWebRetorno.Count - 1
                            If SessionManager.ListaAsientoWebRetorno.Item(m).IDS = oListaAsiento.Item(j).IDS Then
                                estado = False
                            End If
                        Next
                        If estado = True Then
                            Dim oEAsiento As New EAsiento
                            oEAsiento.IDS = oListaAsiento.Item(j).IDS
                            oEAsiento.Nume_Asiento = oListaAsiento.Item(j).Nume_Asiento
                            oEAsiento.Costo = oListaAsiento.Item(j).Costo
                            oEAsiento.Nivel = oListaAsiento.Item(j).Nivel
                            SessionManager.ListaAsientoWebRetorno.Add(oEAsiento)
                        Else
                            estado = True
                        End If
                    Next
                End If
                If SessionManager.ListaAsientoWebRetorno.Count > 0 Then
                    For k As Int16 = 0 To SessionManager.ListaAsientoWebRetorno.Count - 1
                        SessionManager.ListaAsientoWebRetorno.Item(k).Item = k + 1
                    Next
                End If
            End If
            gvasientoseleccionadosretorno.DataSource = oListaAsiento
            gvasientoseleccionadosretorno.DataBind()

            ''Datos Mobile
            gvasientoseleccionadosretornomobile.DataSource = oListaAsiento
            gvasientoseleccionadosretornomobile.DataBind()

            If oListaAsiento.Count > 0 Then
                vista_detalle_mobile_retorno.Visible = True
            Else
                vista_detalle_mobile_retorno.Visible = False
            End If

            Dim oListaAsientos As ListaAsientos = oPLGlobals.CantidadAsientoReservadosbSession( IDCID)
            If oListaAsientos.Count > 0 Then
                SessionManager.CantidadAsientosRetorno = oListaAsientos.Item(0).Cantidad
                SessionManager.MontoRetorno = oListaAsientos.Item(0).Total
            Else
                SessionManager.CantidadAsientosRetorno = 0
                SessionManager.MontoRetorno = 0
            End If
            SessionManager.CantidadAsientos = SessionManager.CantidadAsientosIda + SessionManager.CantidadAsientosRetorno
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Sub CargarAsientosIda()

        Try
            Dim oPLGlobls As New PLGlobals()
            Dim oListaAsientoNivel As ListaAsientoNivel = oPLGlobls.ListarAsientoDisponibles( SessionManager.Cod_ProgramacionIda, SessionManager.PlanoIda, SessionManager.IdBusIda, SessionManager.Fecha_Programacion_Ida, SessionManager.Nro_ViajeIda)
            nADisponibleJsIda = String.Empty
            Dim tRegs As Integer = oListaAsientoNivel.Count
            lblprecionivel1ida.Visible = False
            lblprecionivel2ida.Visible = False

            If tRegs > 0 Then
                With oListaAsientoNivel
                    Dim nAsiento As Integer = 0
                    For x As Integer = 0 To tRegs - 1
                        If .Item(x).Nivel = 1 Then
                            nAsiento = CInt(.Item(x).Nume_Asiento)
                            nADisponibleJsIda = nADisponibleJsIda & "'11" & Format(nAsiento, "0#") & "'" & ","
                            lblprecionivel1ida.Visible = True
                        ElseIf .Item(x).Nivel = 2 Then
                            nAsiento = CInt(.Item(x).Nume_Asiento)
                            nADisponibleJsIda = nADisponibleJsIda & "'12" & Format(nAsiento, "0#") & "'" & ","
                            lblprecionivel2ida.Visible = True
                        End If
                    Next
                End With
            Else

                If SessionManager.ViajeRetorno Then
                    ScriptUser.JQueryMensaje(Me, "No hay asiento disponibles para el viaje de ida")
                Else
                    ScriptUser.JQueryMensaje(Me, "No hay asiento disponibles para el viaje")
                End If
            End If
            JsAsientosBackIda("Cargando Asiento")
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub CargarAsientosRetorno()

        Try
            Dim oPLGlobls As New PLGlobals()
            Dim oListaAsientoNivel As ListaAsientoNivel = oPLGlobls.ListarAsientoDisponibles( SessionManager.Cod_ProgramacionRetorno, SessionManager.PlanoRetorno, SessionManager.IdBusRetorno, SessionManager.Fecha_Programacion_Retorno, SessionManager.Nro_ViajeRetorno)
            nADisponibleJsRetorno = String.Empty
            lblprecionivel1retorno.Visible = False
            lblprecionivel2retorno.Visible = False
            Dim tRegs As Integer = oListaAsientoNivel.Count
            If tRegs > 0 Then
                With oListaAsientoNivel
                    Dim nAsiento As Integer = 0
                    For x As Integer = 0 To tRegs - 1
                        If .Item(x).Nivel = 1 Then
                            nAsiento = CInt(.Item(x).Nume_Asiento)
                            nADisponibleJsRetorno = nADisponibleJsRetorno & "'21" & Format(nAsiento, "0#") & "'" & ","
                            lblprecionivel1retorno.Visible = True
                        ElseIf .Item(x).Nivel = 2 Then
                            nAsiento = CInt(.Item(x).Nume_Asiento)
                            nADisponibleJsRetorno = nADisponibleJsRetorno & "'22" & Format(nAsiento, "0#") & "'" & ","
                            lblprecionivel2retorno.Visible = True
                        End If
                    Next
                End With
            Else
                ScriptUser.JQueryMensaje(Me, "No hay asiento disponibles en el viaje de retorno")
            End If
            JsAsientosBackRetorno("Cargando Asiento") 
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Public Sub JsAsientosBackIda(Optional ByVal s As String = "")

        Try
            If Len(nADisponibleJsIda) > 2 Then
                nADisponibleJsIda = Mid(nADisponibleJsIda, 1, Len(nADisponibleJsIda) - 1)
            End If
            If Len(nASelecionadoJsIda) > 2 Then
                nASelecionadoJsIda = Mid(nASelecionadoJsIda, 1, Len(nASelecionadoJsIda) - 1)
            End If
            Dim JsA As String
            JsA = vbCrLf
            JsA = JsA & " var AryIdaD = new Array(" & nADisponibleJsIda & "); " & vbCrLf
            JsA = JsA & " var AryIdaS = new Array(" & nASelecionadoJsIda & ");" & vbCrLf
            JsA = JsA & "PorcesOptionsIda(AryIdaD,0);" & vbCrLf
            JsA = JsA & "PorcesOptionsIda(AryIdaS,1);" & vbCrLf
            If SessionManager.ViajeRetorno = True Then
                JsA = JsA & "CambiarTabPlano();" & vbCrLf
            End If
            ScriptUser.JsRegister(UpdatePanelWeb, "JsAsientoSelectIda", JsA)
            nADisponibleJsIda = ""
            nASelecionadoJsIda = ""
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Public Sub JsAsientosBackRetorno(Optional ByVal s As String = "")

        Try
            If Len(nADisponibleJsRetorno) > 2 Then
                nADisponibleJsRetorno = Mid(nADisponibleJsRetorno, 1, Len(nADisponibleJsRetorno) - 1)
            End If
            If Len(nASelecionadoJsRetorno) > 2 Then
                nASelecionadoJsRetorno = Mid(nASelecionadoJsRetorno, 1, Len(nASelecionadoJsRetorno) - 1)
            End If
            Dim JsA As String
            JsA = vbCrLf
            JsA = JsA & " var AryRetornoD = new Array(" & nADisponibleJsRetorno & "); " & vbCrLf
            JsA = JsA & " var AryRetornoS = new Array(" & nASelecionadoJsRetorno & ");" & vbCrLf
            JsA = JsA & "PorcesOptionsRetorno(AryRetornoD,0);" & vbCrLf
            JsA = JsA & "PorcesOptionsRetorno(AryRetornoS,1);" & vbCrLf
            If SessionManager.ViajeRetorno = True Then
                JsA = JsA & "CambiarTabPlano();" & vbCrLf
            End If
            ScriptUser.JsRegister(UpdatePanelWeb, "JsAsientoSelectRetorno", JsA)
            nADisponibleJsRetorno = ""
            nASelecionadoJsRetorno = ""
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub CargarBotones()

        Try
            If SessionManager.ViajeRetorno Then
                CargarBotonesIda()
                CargarBotonesRetorno()
            ElseIf SessionManager.ViajeRetorno = False Then
                CargarBotonesIda()
            End If
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub CargarBotonesIda()

        Try
            If IsNumeric(AppSettings.valueString("MaxPorVenta")) Then
                If SessionManager.CantidadAsientosIda >= Val(AppSettings.valueString("MaxPorVenta")) Then
                    btncostoida1.Enabled = False
                    btncostoida2.Enabled = False
                    DivMessageIda.Visible = True
                Else
                    btncostoida1.Enabled = True
                    btncostoida2.Enabled = True
                    DivMessageIda.Visible = False
                End If
            End If
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub CargarBotonesRetorno()

        Try
            If IsNumeric(AppSettings.valueString("MaxPorVenta")) Then
                If SessionManager.CantidadAsientosRetorno >= Val(AppSettings.valueString("MaxPorVenta")) Then
                    btncostoretorno1.Enabled = False
                    btncostoretorno2.Enabled = False
                    DivMessageRetorno.Visible = True
                Else
                    btncostoretorno1.Enabled = True
                    btncostoretorno2.Enabled = True
                    DivMessageRetorno.Visible = False
                End If
            End If
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Public Sub CargarInformaciondeViajes()

        Try
            If SessionManager.ViajeRetorno Then
                DetalleViajeIda()
                DetalleViajeRetorno()
                ''ConvertidoMontosRetorno()
            ElseIf SessionManager.ViajeRetorno = False Then
                DetalleViajeIda()
                ''ConvertidoMontosIda()
            End If
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub DetalleViajeIda()

        Try
            'lblrutaidaEti.Text = SessionManager.Origen_Ida & "-" & SessionManager.Destino_Ida
            lblrutaida.Text = SessionManager.Origen_Ida & "-" & SessionManager.Destino_Ida
            lblfechaviajeida.Text = SessionManager.FechaDeViajeIda
            lblhorasalidaida.Text = SessionManager.HoraViajeIda
            lblhorallegadaida.Text = SessionManager.HoraLlegadaViajeIda
            lblservicioida.Text = SessionManager.ServicioIda
            lblmontosolesida.Text = Format(SessionManager.MontoIda, "0.00")

            ''Datos Mobile

            ''IDA
            lblrutaidamobile.Text = "IDA: " & SessionManager.Origen_Ida & "-" & SessionManager.Destino_Ida
            lblfechaviajeidamobile.Text = DateTime.Parse(SessionManager.FechaDeViajeIda).ToString("dddd") & " " & SessionManager.FechaDeViajeIda




        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub DetalleViajeRetorno()

        Try
            'lblrutaretornoEti.Text = SessionManager.Origen_Retorno & "-" & SessionManager.Destino_Retorno
            lblrutaretorno.Text = SessionManager.Origen_Retorno & "-" & SessionManager.Destino_Retorno
            lblfechaviajeretorno.Text = SessionManager.FechaDeViajeRetorno
            lblhorasalidaretorno.Text = SessionManager.HoraViajeRetorno
            lblhorallegadaretorno.Text = SessionManager.HoraLlegadaViajeRetorno
            lblservicioretorno.Text = SessionManager.ServicioRetorno
            lblmontosolesretorno.Text = Format(SessionManager.MontoRetorno, "0.00")

            ''Datos Mobile

            ''RETORNO
            lblrutaretornomobile.Text = "RET: " & SessionManager.Origen_Retorno & "-" & SessionManager.Destino_Retorno
            lblfechaviajeretornomobile.Text = DateTime.Parse(SessionManager.FechaDeViajeRetorno).ToString("dddd") & " " & SessionManager.FechaDeViajeRetorno

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub ConvertidoMontosRetorno()

        Try
            lblmontoextida.Text = "Monto " & ddlmonedasida.SelectedItem.ToString.Substring(ddlmonedasida.SelectedItem.Text.Length - 5, 5)
            lblmontoextrida.Text = Format((SessionManager.MontoIda / ddlmonedasida.SelectedValue), "0.00")
            lblmontoextretorno.Text = "Monto " & ddlmonedasretorno.SelectedItem.ToString.Substring(ddlmonedasretorno.SelectedItem.Text.Length - 5, 5)
            lblmontoextrretorno.Text = Format((SessionManager.MontoRetorno / ddlmonedasretorno.SelectedValue), "0.00")
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try


    End Sub

    Private Sub ConvertidoMontosIda()

        Try
            lblmontoextida.Text = "Monto " & ddlmonedasida.SelectedItem.ToString.Substring(ddlmonedasida.SelectedItem.Text.Length - 5, 5)
            lblmontoextrida.Text = Format((SessionManager.MontoIda / ddlmonedasida.SelectedValue), "0.00")
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try



    End Sub

    Private Sub CargarPlano()


        Try
            If SessionManager.ViajeRetorno Then
                CargarPlanoIda()
                CargarPlanoRetorno()
            ElseIf SessionManager.ViajeRetorno = False Then
                CargarPlanoIda()
            End If
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub CargarPlanoIda()

        Try
            If Not IsPostBack Then
                GenerarPlanoIda(SessionManager.PlanoIda)
            End If

            If Not SessionManager.PlanoIda Is Nothing Then
                DivPlanoBusIda.InnerHtml = Util.OpenFiles("/planos/" & SessionManager.PlanoIda & "_ida" & ".html")
            End If
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub CargarPlanoRetorno()

        Try
            If Not IsPostBack Then
                GenerarPlanoRetorno(SessionManager.PlanoRetorno)
            End If
            If Not SessionManager.PlanoRetorno Is Nothing Then
                DivPlanoBusRetorno.InnerHtml = Util.OpenFiles("/planos/" & SessionManager.PlanoRetorno & "_retorno" & ".html")
            End If
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Function GenerarPlanoIda(ByVal PlanoId As String) As Boolean


        Try
            Dim oPLGlobls As New PLGlobals()
            Dim oListaPlanoBus As ListaPlanoBus = oPLGlobls.ListarPlanoBus( PlanoId)
            Dim listaida As ListaAsientoNivel = oPLGlobls.ListarAsientoNivelAll( SessionManager.IdBusIda)


            If oListaPlanoBus.Count > 0 Then

                Dim sbhtml As New StringBuilder
                Dim sbfront As New StringBuilder
                Dim sbback As New StringBuilder
                Dim sbfirstlevel As New StringBuilder
                Dim sbsecondlevel As New StringBuilder
                Dim sblinelevel As New StringBuilder
                ''HTML DE DELANTE
                sbfront.AppendLine("<link rel=""stylesheet"" type=""text/css"" href=""../css/style.css"" /> ")
                sbfront.AppendLine("<div class=""plano-ida"">")
                sbfront.AppendLine("<div class=""frontis-a""><img src=""images/frontis-mobile.png""  width=""100%""></div>")
                sbfront.AppendLine("<div class=""contenedor-niveles"">")

                ''HTML DE ATRAS
                sbback.AppendLine("</div>")
                sbback.AppendLine("	<div class=""frontis-b""></div>")
                sbback.AppendLine("</div>")

                Dim maxvalor As Int16 = CInt(oListaPlanoBus.Item(oListaPlanoBus.Count - 1).Indice)
                Dim residuo As Int16 = maxvalor Mod 5
                If residuo > 0 Then
                    maxvalor += (5 - residuo)
                End If
                Dim odnivel1 As New Dictionary(Of Integer, String)
                Dim odnivel2 As New Dictionary(Of Integer, String)
                Dim odbase As New Dictionary(Of Integer, String)
                Dim m As Int16 = maxvalor
                Dim contador As Int16 = 1
                Dim contemp As Int16 = 0


                Dim fielcontsecond As Int16 = 1
                Dim second As Boolean = False
                Dim first As Boolean = False
                Dim max1 As Integer = 0
                Dim max2 As Integer = 0
                Dim min1 As Integer = 0
                Dim min2 As Integer = 0
                Dim n1 As String = "1"
                Dim n2 As String = "2"
                Do Until contador > maxvalor
                    Dim noexists As Boolean = True
                    Dim newvalor As Int16 = m - contemp
                    For j As Int16 = 0 To oListaPlanoBus.Count - 1
                        If CInt(oListaPlanoBus.Item(j).Indice.ToString) = newvalor Then
                            If IsNumeric(oListaPlanoBus.Item(j).Tipo) Then
                                For i As Int16 = 0 To listaida.Count - 1
                                    If CInt(oListaPlanoBus.Item(j).Tipo) = CInt(listaida.Item(i).Nume_Asiento) Then
                                        If listaida.Item(i).Nivel = n1 Then
                                            odbase.Add(newvalor, n1 & CInt(oListaPlanoBus.Item(j).Tipo).ToString("0#"))
                                            noexists = False
                                            If first = False Then
                                                first = True
                                                fielcontsecond = (contador - (contador Mod 5))
                                            End If
                                            Exit For
                                        ElseIf listaida.Item(i).Nivel = n2 Then

                                            second = True
                                            odbase.Add(newvalor, n2 & CInt(oListaPlanoBus.Item(j).Tipo).ToString("0#"))
                                            noexists = False
                                            Exit For
                                        End If
                                    End If
                                Next
                            Else
                                odbase.Add(newvalor, oListaPlanoBus.Item(j).Tipo.ToString)
                                noexists = False
                            End If
                            Exit For
                        ElseIf CInt(oListaPlanoBus.Item(j).Indice.ToString) > newvalor Then
                            odbase.Add(newvalor, "VA")
                            noexists = False
                            Exit For
                        End If

                    Next
                    If noexists Then
                        odbase.Add(newvalor, "VA")
                    End If
                    If contemp = 4 Then
                        contemp = 0
                        m -= 5
                    Else
                        contemp += 1
                    End If
                    contador += 1
                Loop

                Dim sbitemfirstlevel As New StringBuilder
                Dim sbitemsecondlevel As New StringBuilder
                Dim cont1 As Int16 = 1
                Dim cont2 As Int16 = 1

                Dim sbitem As String = String.Empty
                If second = False Then
                    secondlevelida.Visible = False
                End If
                If second = True Then
                    Dim cont As Int16 = 0
                    For Each kv As KeyValuePair(Of Integer, String) In odbase
                        If cont >= fielcontsecond Then
                            odnivel1.Add(kv.Key, kv.Value)
                        Else
                            odnivel2.Add(kv.Key, kv.Value)
                        End If
                        cont += 1
                    Next
                    Dim pila1 As New Stack(Of String)
                    Dim pila2 As New Stack(Of String)

                    For Each kvp As KeyValuePair(Of Integer, String) In odnivel1
                        Dim res As Int16 = 1000
                        If cont1 > 5 Then
                            res = cont1 Mod 5
                        End If

                        If cont1 = 1 OrElse res = 1 Then
                            sbitem += "<ul>"
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn1" & kvp.Value & """  name=""btn1" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                        ElseIf cont1 = 5 OrElse res = 0 Then
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn1" & kvp.Value & """  name=""btn1" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                            sbitem += "</ul>"
                            pila1.Push(sbitem)
                            ''sbitemfirstlevel.AppendLine(sbitem.ToString)
                            sbitem = String.Empty
                        Else
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn1" & kvp.Value & """  name=""btn1" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                        End If
                        cont1 += 1
                    Next
                    For i As Integer = 0 To pila1.Count - 1
                        sbitemfirstlevel.AppendLine(pila1.Pop)
                    Next

                    sbitem = String.Empty
                    For Each kvp As KeyValuePair(Of Integer, String) In odnivel2
                        Dim res As Int16 = 1000
                        If cont2 > 5 Then
                            res = cont2 Mod 5
                        End If
                        If cont2 = 1 OrElse res = 1 Then
                            sbitem += "<ul>"
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn1" & kvp.Value & """  name=""btn1" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                        ElseIf cont2 = 5 OrElse res = 0 Then
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn1" & kvp.Value & """  name=""btn1" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                            sbitem += "</ul>"
                            pila2.Push(sbitem)
                            ''sbitemsecondlevel.AppendLine(sbitem.ToString)
                            sbitem = String.Empty
                        Else
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn1" & kvp.Value & """  name=""btn1" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                        End If
                        cont2 += 1
                    Next

                    For i As Integer = 0 To pila2.Count - 1
                        sbitemsecondlevel.AppendLine(pila2.Pop)
                    Next
                    sbfirstlevel.AppendLine("<div class=""primer-nivel"">")
                    sbfirstlevel.AppendLine(sbitemfirstlevel.ToString)
                    sbfirstlevel.AppendLine("<div class=""letrero-primer-nivel""><img src=""images/icon-1erpiso.png"" width=""100%"" /></div>")
                    sbfirstlevel.AppendLine("</div>")


                    sbsecondlevel.AppendLine("<div class=""segundo-nivel"">")
                    sbsecondlevel.AppendLine(sbitemsecondlevel.ToString)
                    sbsecondlevel.AppendLine("<div class=""letrero-segundo-nivel""><img src=""images/icon-2dopiso.png"" width=""100%"" /></div>")
                    sbsecondlevel.AppendLine("</div>")


                    sbhtml.AppendLine(sbfront.ToString)
                    sbhtml.AppendLine(sbfirstlevel.ToString)
                    sbhtml.AppendLine("<div class=""conten-separador""><ul class=""line-separador""><li></li><li></li><li></li><li></li><li></li></ul></div>")
                    sbhtml.AppendLine(sbsecondlevel.ToString)
                    sbhtml.AppendLine(sbback.ToString)
                Else
                    odnivel1 = odbase
                    Dim pila1 As New Stack(Of String)
                    For Each kvp As KeyValuePair(Of Integer, String) In odnivel1
                        Dim res As Int16 = 1000
                        If cont1 > 5 Then
                            res = cont1 Mod 5
                        End If

                        If cont1 = 1 OrElse res = 1 Then
                            sbitem += "<ul>"
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn1" & kvp.Value & """  name=""btn1" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                        ElseIf cont1 = 5 OrElse res = 0 Then
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn1" & kvp.Value & """  name=""btn1" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                            sbitem += "</ul>"
                            pila1.Push(sbitem)
                            ''sbitemfirstlevel.AppendLine(sbitem.ToString)
                            sbitem = String.Empty
                        Else
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn1" & kvp.Value & """  name=""btn1" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                        End If
                        cont1 += 1
                    Next
                    For i As Integer = 0 To pila1.Count - 1
                        sbitemfirstlevel.AppendLine(pila1.Pop)
                    Next
                    sbfirstlevel.AppendLine("<div class=""primer-nivel"">")
                    sbfirstlevel.AppendLine(sbitemfirstlevel.ToString)
                    sbfirstlevel.AppendLine("<div class=""letrero-primer-nivel""><img src=""images/icon-1erpiso.png"" width=""100%"" /></div>")
                    sbfirstlevel.AppendLine("</div>")
                    sbhtml.AppendLine(sbfront.ToString)
                    sbhtml.AppendLine(sbfirstlevel.ToString)
                    sbhtml.AppendLine(sbback.ToString)
                End If



                Dim Path As String = Variables.sPathDir & "planos\" & PlanoId & "_ida.html"
                If File.Exists(Path) Then
                    File.Delete(Path)
                    File.WriteAllText(Path, sbhtml.ToString)
                Else
                    File.WriteAllText(Path, sbhtml.ToString)
                End If

            Else
                Return False
            End If

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function

    Private Function GenerarPlanoRetorno(ByVal PlanoId As String) As Boolean


        Try
            Dim oPLGlobls As New PLGlobals()
            Dim oListaPlanoBus As ListaPlanoBus = oPLGlobls.ListarPlanoBus( PlanoId)
            Dim listaretorno As ListaAsientoNivel = oPLGlobls.ListarAsientoNivelAll( SessionManager.IdBusRetorno)
            If oListaPlanoBus.Count > 0 Then

                Dim sbhtml As New StringBuilder
                Dim sbfront As New StringBuilder
                Dim sbback As New StringBuilder
                Dim sbfirstlevel As New StringBuilder
                Dim sbsecondlevel As New StringBuilder
                Dim sblinelevel As New StringBuilder
                ''HTML DE DELANTE
                sbfront.AppendLine("<link rel=""stylesheet"" type=""text/css"" href=""../css/style.css"" /> ")
                sbfront.AppendLine("<div class=""plano-retorno"">")
                sbfront.AppendLine("<div class=""frontis-a""><img src=""images/frontis-mobile.png""  width=""100%""></div>")
                sbfront.AppendLine("<div class=""contenedor-niveles"">")

                ''HTML DE ATRAS
                sbback.AppendLine("</div>")
                sbback.AppendLine("	<div class=""frontis-b""></div>")
                sbback.AppendLine("</div>")

                Dim odnivel1 As New Dictionary(Of Integer, String)
                Dim odnivel2 As New Dictionary(Of Integer, String)
                Dim odbase As New Dictionary(Of Integer, String)
                Dim maxvalor As Int16 = CInt(oListaPlanoBus.Item(oListaPlanoBus.Count - 1).Indice)
                Dim residuo As Int16 = maxvalor Mod 5
                If residuo > 0 Then
                    maxvalor += (5 - residuo)
                End If
                Dim m As Int16 = 1
                Dim contador As Int16 = 1
                Dim contemp As Int16 = 0
                Dim fielcontsecond As Int16 = 1
                Dim second As Boolean = False
                Dim first As Boolean = False

                Do Until contador > maxvalor
                    Dim newvalor As Int16 = m + contemp
                    Dim noexists As Boolean = True
                    For j As Int16 = 0 To oListaPlanoBus.Count - 1
                        If CInt(oListaPlanoBus.Item(j).Indice.ToString) = newvalor Then
                            If IsNumeric(oListaPlanoBus.Item(j).Tipo) Then
                                For i As Int16 = 0 To listaretorno.Count - 1
                                    If CInt(oListaPlanoBus.Item(j).Tipo) = CInt(listaretorno.Item(i).Nume_Asiento) Then
                                        If listaretorno.Item(i).Nivel = "1" Then
                                            odbase.Add(newvalor, "1" & CInt(oListaPlanoBus.Item(j).Tipo).ToString("0#"))
                                            noexists = False
                                        End If
                                        If listaretorno.Item(i).Nivel = "2" Then
                                            If first = False Then
                                                first = True
                                                fielcontsecond = (contador - (contador Mod 5))
                                            End If
                                            second = True
                                            odbase.Add(newvalor, "2" & CInt(oListaPlanoBus.Item(j).Tipo).ToString("0#"))
                                            noexists = False
                                        End If
                                        Exit For
                                    End If
                                Next
                            Else
                                odbase.Add(newvalor, oListaPlanoBus.Item(j).Tipo.ToString)
                                noexists = False
                                Exit For
                            End If
                            Exit For

                        ElseIf CInt(oListaPlanoBus.Item(j).Indice.ToString) > newvalor Then
                            odbase.Add(newvalor, "VA")
                            noexists = False
                            Exit For
                        End If
                    Next
                    If noexists Then
                        odbase.Add(newvalor, "VA")
                    End If
                    If contemp = 4 Then
                        contemp = 0
                        m += 5
                    Else
                        contemp += 1
                    End If
                    contador += 1
                Loop


                Dim sbitemfirstlevel As New StringBuilder
                Dim sbitemsecondlevel As New StringBuilder
                Dim cont1 As Int16 = 1
                Dim cont2 As Int16 = 1

                Dim sbitem As String = String.Empty
                If second = False Then
                    secondlevelretorno.Visible = False
                End If
                If second = True Then
                    Dim cont As Int16 = 0
                    For Each kv As KeyValuePair(Of Integer, String) In odbase
                        If cont >= fielcontsecond Then
                            odnivel2.Add(kv.Key, kv.Value)

                        Else
                            odnivel1.Add(kv.Key, kv.Value)
                        End If
                        cont += 1
                    Next
                    For Each kvp As KeyValuePair(Of Integer, String) In odnivel1
                        Dim res As Int16 = 1000
                        If cont1 > 5 Then
                            res = cont1 Mod 5
                        End If
                        If cont1 = 1 OrElse res = 1 Then
                            sbitem += "<ul>"
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn2" & kvp.Value & """  name=""btn2" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                        ElseIf cont1 = 5 OrElse res = 0 Then
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn2" & kvp.Value & """  name=""btn2" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                            sbitem += "</ul>"
                            sbitemfirstlevel.AppendLine(sbitem.ToString)
                            sbitem = String.Empty
                        Else
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn2" & kvp.Value & """  name=""btn2" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                        End If
                        cont1 += 1
                    Next
                    sbitem = String.Empty
                    For Each kvp As KeyValuePair(Of Integer, String) In odnivel2
                        Dim res As Int16 = 1000
                        If cont2 > 5 Then
                            res = cont2 Mod 5
                        End If
                        If cont2 = 1 OrElse res = 1 Then
                            sbitem += "<ul>"
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn2" & kvp.Value & """  name=""btn2" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                        ElseIf cont2 = 5 OrElse res = 0 Then
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn2" & kvp.Value & """  name=""btn2" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                            sbitem += "</ul>"
                            sbitemsecondlevel.AppendLine(sbitem.ToString)
                            sbitem = String.Empty
                        Else
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn2" & kvp.Value & """  name=""btn2" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                        End If
                        cont2 += 1
                    Next
                    sbfirstlevel.AppendLine("<div class=""primer-nivel"">")
                    sbfirstlevel.AppendLine(sbitemfirstlevel.ToString)
                    sbfirstlevel.AppendLine("<div class=""letrero-primer-nivel""><img src=""images/icon-1erpiso.png"" width=""100%"" /></div>")
                    sbfirstlevel.AppendLine("</div>")


                    sbsecondlevel.AppendLine("<div class=""segundo-nivel"">")
                    sbsecondlevel.AppendLine(sbitemsecondlevel.ToString)
                    sbsecondlevel.AppendLine("<div class=""letrero-segundo-nivel""><img src=""images/icon-2dopiso.png"" width=""100%"" /></div>")
                    sbsecondlevel.AppendLine("</div>")


                    sbhtml.AppendLine(sbfront.ToString)
                    sbhtml.AppendLine(sbfirstlevel.ToString)
                    sbhtml.AppendLine("<div class=""conten-separador""><ul class=""line-separador""><li></li><li></li><li></li><li></li><li></li></ul></div>")
                    sbhtml.AppendLine(sbsecondlevel.ToString)
                    sbhtml.AppendLine(sbback.ToString)
                Else
                    odnivel1 = odbase

                    For Each kvp As KeyValuePair(Of Integer, String) In odnivel1
                        Dim res As Int16 = 1000
                        If cont1 > 5 Then
                            res = cont1 Mod 5
                        End If
                        If cont1 = 1 OrElse res = 1 Then
                            sbitem += "<ul>"
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn2" & kvp.Value & """  name=""btn2" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                        ElseIf cont1 = 5 OrElse res = 0 Then
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"

                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn2" & kvp.Value & """  name=""btn2" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                            sbitem += "</ul>"
                            sbitemfirstlevel.AppendLine(sbitem.ToString)
                            sbitem = String.Empty
                        Else
                            If kvp.Value = "VA" OrElse kvp.Value = "PP" OrElse kvp.Value = "CU" OrElse kvp.Value = "CERO" Then
                                sbitem += "<li><div class=""blockfree""></div></li>"
                            ElseIf kvp.Value = "LI" Then
                            ElseIf kvp.Value = "TV" Then
                                sbitem += "<li><div class=""icon-tv""></div></li>"
                            ElseIf kvp.Value = "PU" Then
                                sbitem += "<li><div class=""icon-puerta""></div></li>"
                            ElseIf kvp.Value = "EE" Then
                                sbitem += "<li><div class=""icon-escalera""></div></li>"
                            ElseIf kvp.Value = "BA" Then
                                sbitem += "<li><div class=""icon-sshh""></div></li>"
                            Else
                                If IsNumeric(kvp.Value) Then
                                    sbitem += "<li><button type=""button"" id=""btn2" & kvp.Value & """  name=""btn2" & kvp.Value & """      class=""asiento-ocupado"">" & kvp.Value.Substring(1, 2).ToString & "</button></li>"
                                Else
                                    sbitem += "<li><div class=""blockfree""></div></li>"
                                End If
                            End If
                        End If
                        cont1 += 1
                    Next
                    sbfirstlevel.AppendLine("<div class=""primer-nivel"">")
                    sbfirstlevel.AppendLine(sbitemfirstlevel.ToString)
                    sbfirstlevel.AppendLine("<div class=""letrero-primer-nivel""><img src=""images/icon-1erpiso.png"" width=""100%"" /></div>")
                    sbfirstlevel.AppendLine("</div>")
                    sbhtml.AppendLine(sbfront.ToString)
                    sbhtml.AppendLine(sbfirstlevel.ToString)
                    sbhtml.AppendLine(sbback.ToString)
                End If



                Dim Path As String = Variables.sPathDir & "planos\" & PlanoId & "_retorno.html"
                If File.Exists(Path) Then
                    File.Delete(Path)
                    File.WriteAllText(Path, sbhtml.ToString)
                Else
                    File.WriteAllText(Path, sbhtml.ToString)
                End If

            Else
                Return False
            End If

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function

    Private Sub ViajeDetalleCosto()

        Try
            If SessionManager.ViajeRetorno Then
                DetalleCostoViajeRetorno()
                DetalleCostoViajeIda()
            ElseIf SessionManager.ViajeRetorno = False Then
                DetalleCostoViajeIda()
            End If
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub DetalleCostoViajeIda()

        Try
            Dim oPLGlobls As New PLGlobals()
            Dim oETb_Precio_Fecha As ETb_Precio_Fecha = oPLGlobls.ListarPreciosNivelesPiso( SessionManager.OrigenId_Ida, SessionManager.DestinoId_Ida, SessionManager.HoraViajeIda, SessionManager.FechaDeViajeIda, SessionManager.ServicioIdIda, SessionManager.IdEmpresaIda, SessionManager.IdPrecioNacionalidadIda, SessionManager.TransactionPromociones)
            lblprecionivel1ida.Text = oETb_Precio_Fecha.Precio_Nivel1
            lblprecionivel2ida.Text = oETb_Precio_Fecha.Precio_Nivel2

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Sub DetalleCostoViajeRetorno()

        Try
            Dim oPLGlobls As New PLGlobals()
            Dim oETb_Precio_Fecha As ETb_Precio_Fecha = oPLGlobls.ListarPreciosNivelesPiso( SessionManager.OrigenId_Retorno, SessionManager.DestinoId_Retorno, SessionManager.HoraViajeRetorno, SessionManager.FechaDeViajeRetorno, SessionManager.ServicioIdRetorno, SessionManager.IdEmpresaRetorno, SessionManager.IdPrecioNacionalidadRetorno, SessionManager.TransactionPromociones)
            lblprecionivel1retorno.Text = oETb_Precio_Fecha.Precio_Nivel1
            lblprecionivel2retorno.Text = oETb_Precio_Fecha.Precio_Nivel2

        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Public Function RecalcularPrecioAsiento(ByVal Codi_Empresa As Byte, ByVal Codi_Origen As String, ByVal Codi_Destino As String, ByVal Codi_Servicio As String, ByVal Hora As String, ByVal FechaViaje As String, ByVal Nume_Asiento As Integer, Precio As Decimal) As Decimal
        Try
            Dim Monto As Decimal = 0
            Dim oPLGlobals As New PLGlobals
            Dim oETb_BF_Det_Promo As ETb_BF_Det_Promo = oPLGlobals.ListarPreciobyAsiento(Codi_Empresa, Codi_Origen, Codi_Destino, Codi_Servicio, Hora, FechaViaje, Nume_Asiento)
            If Not oETb_BF_Det_Promo Is Nothing Then
                If oETb_BF_Det_Promo.Tipo = "01" Then
                    Monto = oETb_BF_Det_Promo.Monto
                ElseIf oETb_BF_Det_Promo.Tipo = "02" Then
                    Monto = Monto - oETb_BF_Det_Promo.Monto
                ElseIf oETb_BF_Det_Promo.Tipo = "03" Then
                    Monto = Monto - (Monto * (oETb_BF_Det_Promo.Monto / 100))
                Else
                    Monto = Precio
                End If
            Else
                Monto = Precio
            End If
            Return Monto
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub BloquearAsiento(ByVal Nro_Viaje As Integer, ByVal IdAsiento As Integer, ByVal FechaProgramacion As String, ByVal Fecha_Viaje As DateTime, ByVal Costo As Double, ByVal IDCID As String, ByVal typeruta As Int16, ByVal Codi_Programacion As Int32)

        Try
            If IsPostBack Then
                Dim oPLGlobals As New PLGlobals()
                Functions.ActualizarProgramacion()
                Dim indicador As Integer = oPLGlobals.SeleccionarAsientobyNroViaje( Nro_Viaje, Format(IdAsiento, "00"), IDCID, FechaProgramacion, Costo, Codi_Programacion)
                CargarAsientos()
                CargarInformaciondeViajes()
            End If
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Sub DeleteAsiento(ByVal Ids As Integer)

        Try
            If Ids > 0 Then
                Dim oPlgAdelet As PLGlobals = New PLGlobals()
                oPlgAdelet.EliminarAsientoSeleccionadoWeb( Ids)
                oPlgAdelet = Nothing
                CargarAsientos()
                CargarInformaciondeViajes()
            End If
        Catch ex As Exception
            Log.Instance(GetType(seleccionarasientos)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub


#End Region


End Class
