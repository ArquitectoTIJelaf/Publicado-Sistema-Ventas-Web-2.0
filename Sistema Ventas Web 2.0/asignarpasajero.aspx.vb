Imports PLogic
Imports PEntity
Imports System.Collections.Generic
Imports PEncry
Imports PUtilitario

Partial Class asignarpasajero
    Inherits System.Web.UI.Page
    Dim oListaAsientoWebIda As ListaAsientos
    Dim oListaAsientoWebRetorno As New ListaAsientos
    Dim status As Boolean = True

#Region "Metodos Privados"

    Private Sub CreaUsuario()

        Try

            Dim oPLGlobals As New PLGlobals
            Dim oCrypto As New Crypto
            Dim oListaUsers As ListaUsers = oPLGlobals.Logueo_Usuario_Web_Auto( Me.txtCorreo.Text.Trim)
            If oListaUsers.Count > 0 Then
                SessionUser.UserLoaData(oListaUsers)
            Else
                If oPLGlobals.RegistroUsuario_One( txtCorreo.Text.Trim, oCrypto.Encrypt("123456789"), txtCorreo.Text.Trim) Then
                    SessionManager.Name = txtCorreo.Text.Trim
                    Dim Rpta As String = String.Empty
                    Rpta = smNewUser(txtCorreo.Text.Trim, "123456789", txtCorreo.Text.Trim)
                    oListaUsers = oPLGlobals.Logueo_Usuario_Web_Auto( Me.txtCorreo.Text.Trim)
                    SessionUser.UserLoaData(oListaUsers)
                    'Me.User.Identity. = txtCorreo.Text
                    'Exit Sub
                Else
                    'flerror.Text = "Ocurrio un error, por favor comunicarse con el Administrador"
                End If
            End If

            FormsAuthentication.SetAuthCookie(SessionManager.Name, False)
            WUCViewUser1.UserName = SessionManager.PerfilName
            WUCViewUser1.LogueoEnd = False
            WUCViewUser1.LogueoStart = True

        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try


    End Sub

    Private Sub PermisosObjetos()

        Try
            If SessionManager.ViajeRetorno Then
                dataitinerarioretornotitle.Visible = True
                dataitinerarioretornohead.Visible = True
                travelretorno.Visible = True
            Else
                dataitinerarioretornotitle.Visible = False
                dataitinerarioretornohead.Visible = False
                travelretorno.Visible = False
            End If
        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
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
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try


    End Sub

    Private Sub DetalleViajeIda()

        Try
            lblTitIda.Text = SessionManager.Origen_Ida & " / " & SessionManager.Destino_Ida
            lblrutaida.Text = SessionManager.Origen_Ida & " - " & SessionManager.Destino_Ida
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
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub DetalleViajeRetorno()

        Try
            lblTitRetorno.Text = SessionManager.Origen_Retorno & " / " & SessionManager.Destino_Retorno
            lblrutaretorno.Text = SessionManager.Origen_Retorno & " - " & SessionManager.Destino_Retorno
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
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub



    Private Sub ListarPasajeros(Optional ByVal index As Integer = -1)

        Try
            If SessionManager.ViajeRetorno Then
                ListarPasajeroIda()
                ListarPasajeroRetorno()
            ElseIf SessionManager.ViajeRetorno = False Then
                ListarPasajeroIda()
            End If
        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub ListarPasajeroIda(Optional ByVal index As Integer = -1)

        Try
            Using oPLGlobals As New PLGlobals

                If SessionManager.ListaAsientoWebIda Is Nothing OrElse SessionManager.ListaAsientoWebIda.Count = 0 Then
                    SessionManager.ListaAsientoWebIda = oPLGlobals.ListaPasajerosbyAsiento( "1" & SessionManager.IDCID, SessionManager.IdBusIda)
                ElseIf SessionManager.ListaAsientoWebIda.Count < SessionManager.CantidadAsientosIda Then
                    Dim oListaAsientosPasajeros As ListaAsientos = oPLGlobals.ListaPasajerosbyAsiento( "1" & SessionManager.IDCID, SessionManager.IdBusIda)
                    For j As Int16 = 0 To oListaAsientosPasajeros.Count - 1
                        Dim valor As Boolean = False
                        For n As Int16 = 0 To SessionManager.ListaAsientoWebIda.Count - 1
                            If SessionManager.ListaAsientoWebIda.Item(n).IDS = oListaAsientosPasajeros.Item(j).IDS Then
                                valor = True
                                Exit For
                            End If
                        Next
                        If valor = False Then
                            SessionManager.ListaAsientoWebIda.Add(oListaAsientosPasajeros.Item(j))
                        End If
                    Next
                End If

                With SessionManager.ListaAsientoWebIda
                    If .Count < 1 Then
                        ScriptUser.JQueryMensaje(Me, Message.EAsientoEliminadosViajeIda, 0)
                        Response.Redirect("seleccionarasientos.aspx#bus", False)
                        Exit Sub
                    End If
                End With


                'Dim indexida As Integer = 0
                'Dim indexretorno As Integer = 0
                'Dim viajeida As Boolean = PasajerosRegistradosIdaAll(indexida)
                'Dim viajeretorno As Boolean = PasajerosRegistradosRetornoAll(indexretorno)
                'If viajeida = False Then
                '    ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajeroAllIda)
                '    ListarPasajeroIda(indexida)
                '    Exit Sub
                'ElseIf viajeretorno = False Then
                '    ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajeroAllRetorno)
                '    ListarPasajeroRetorno(indexretorno)
                '    Exit Sub
                'Else
                '    FormatearPasajeros(SessionManager.ListaAsientoWebIda, 1)
                '    FormatearPasajeros(SessionManager.ListaAsientoWebRetorno, 2)
                '    SessionManager.SelectionPasajeros = True
                '    Response.Redirect("confirmarcompra.aspx", True)
                '    Exit Sub
                'End If



                Dim oListaAsientos As ListaAsientos = oPLGlobals.CantidadAsientoReservadosbSession( "1" & SessionManager.IDCID)
                If oListaAsientos.Count > 0 Then
                    SessionManager.CantidadAsientosIda = oListaAsientos.Item(0).Cantidad
                    SessionManager.MontoIda = oListaAsientos.Item(0).Total
                Else
                    SessionManager.CantidadAsientosIda = 0
                    SessionManager.MontoIda = 0
                End If
                SessionManager.CantidadAsientos = SessionManager.CantidadAsientosIda + SessionManager.CantidadAsientosRetorno
                lblmontosolesida.Text = SessionManager.MontoIda.ToString("0.00")
                If SessionManager.CantidadAsientosIda = 0 Then
                    Response.Redirect("seleccionarasientos.aspx#bus", False)
                End If
                Dim oEVentaWeb As New EVentaWeb
                'INICIO DE CAMBIO DE WILLIAMS
                For n As Integer = 0 To SessionManager.ListaAsientoWebIda.Count - 1
                    With SessionManager.ListaAsientoWebIda.Item(n)
                        .Nume_Asiento = CInt(.Nume_Asiento).ToString("0#")
                        .Servicio = SessionManager.ServicioIda
                        If .Sexo = "X" Then
                            .Sexo = "X"
                        End If
                    End With
                Next
                'FIN DE CAMBIO DE WILLIAMS
                dlpasajerosida.DataSource = SessionManager.ListaAsientoWebIda
                dlpasajerosida.DataBind()

            End Using
        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Private Sub ListarPasajeroRetorno(Optional ByVal index As Integer = -1, Optional ByVal Listar As Boolean = False)

        Try
            Using oPLGlobals As New PLGlobals
                If SessionManager.ListaAsientoWebRetorno Is Nothing Then
                    SessionManager.ListaAsientoWebRetorno = oPLGlobals.ListaPasajerosbyAsiento( "2" & SessionManager.IDCID, SessionManager.IdBusIda)
                ElseIf SessionManager.ListaAsientoWebRetorno.Count < SessionManager.CantidadAsientosRetorno Then
                    Dim oListaAsientoPasajeros As ListaAsientos = oPLGlobals.ListaPasajerosbyAsiento( "2" & SessionManager.IDCID, SessionManager.IdBusIda)
                    For j As Int16 = 0 To oListaAsientoPasajeros.Count - 1
                        Dim valor As Boolean = False
                        For n As Int16 = 0 To SessionManager.ListaAsientoWebRetorno.Count - 1
                            If SessionManager.ListaAsientoWebRetorno.Item(n).IDS = oListaAsientoPasajeros.Item(j).IDS Then
                                valor = True
                                Exit For
                            End If
                        Next
                        If valor = False Then
                            SessionManager.ListaAsientoWebRetorno.Add(oListaAsientoPasajeros.Item(j))
                        End If
                    Next
                End If

                With SessionManager.ListaAsientoWebRetorno
                    If .Count < 1 Then
                        ScriptUser.JQueryMensaje(Me, Message.EAsientoEliminadosViajeRetorno, 0)
                        Response.Redirect("seleccionarasientos.aspx#bus", False)
                        Exit Sub
                    End If
                End With

                Dim oListaAsientos As ListaAsientos = oPLGlobals.CantidadAsientoReservadosbSession( "2" & SessionManager.IDCID)
                If oListaAsientos.Count > 0 Then
                    SessionManager.CantidadAsientosRetorno = oListaAsientos.Item(0).Cantidad
                    SessionManager.MontoRetorno = oListaAsientos.Item(0).Total
                Else
                    SessionManager.CantidadAsientosRetorno = 0
                    SessionManager.MontoRetorno = 0
                End If
                SessionManager.CantidadAsientos = SessionManager.CantidadAsientosIda + SessionManager.CantidadAsientosRetorno
                If SessionManager.CantidadAsientosRetorno = 0 Then
                    Response.Redirect("seleccionarasientos.aspx#bus", False)
                End If
                lblmontosolesretorno.Text = SessionManager.MontoRetorno.ToString("0.00")
                'INICIO DE CAMBIO DE WILLIAMS
                Dim oEVentaWeb As New EVentaWeb
                For n As Integer = 0 To SessionManager.ListaAsientoWebRetorno.Count - 1
                    With SessionManager.ListaAsientoWebRetorno.Item(n)
                        .Nume_Asiento = CInt(.Nume_Asiento).ToString("0#")
                        .Servicio = SessionManager.ServicioRetorno
                        If .Sexo = "X" Then
                            .Sexo = "X"
                        End If
                    End With
                Next
                'FINAL DE CAMBIO DE WILLIAMS
                dlpasajerosretorno.DataSource = SessionManager.ListaAsientoWebRetorno
                dlpasajerosretorno.DataBind()

            End Using
        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Sub SearchPasajero(ByVal typodoc As String, ByVal numdoc As String, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs, ByVal typeviaje As Integer, ByVal index As Integer)

        Try

            Dim oPLGlobals As New PLGlobals
            Dim oListaClientePasajes As ListaClientePasajes = oPLGlobals.ListaClientePasajesOne( typodoc, numdoc)

            If oListaClientePasajes.Count > 0 Then

                With oListaClientePasajes.Item(0)
                    If typeviaje = 1 Then
                        FlagTabX.Value = 0
                        SessionManager.ListaAsientoWebIda.Item(index).Tipo_Doc = .Tipo_Doc_Id
                        SessionManager.ListaAsientoWebIda.Item(index).DNI = .Numero_Doc
                        SessionManager.ListaAsientoWebIda.Item(index).Nombre = .Nombre_Clientes
                        SessionManager.ListaAsientoWebIda.Item(index).ApePaterno = .Apellido_P
                        SessionManager.ListaAsientoWebIda.Item(index).ApeMaterno = .Apellido_M
                        SessionManager.ListaAsientoWebIda.Item(index).Telefono = .Telefono
                        SessionManager.ListaAsientoWebIda.Item(index).Sexo = IIf(String.IsNullOrWhiteSpace(.Sexo), "X", .Sexo)
                        SessionManager.ListaAsientoWebIda.Item(index).Edad = .Edad
                        SessionManager.ListaAsientoWebIda.Item(index).NIT_Cliente = .Ruc_Contacto
                        SessionManager.ListaAsientoWebIda.Item(index).Razon_Social = .Razon_Social
                        SessionManager.ListaAsientoWebIda.Item(index).Rz_Direccion = .Rz_Direccion



                    ElseIf typeviaje = 2 Then
                        FlagTabX.Value = 1

                        SessionManager.ListaAsientoWebRetorno.Item(index).Tipo_Doc = .Tipo_Doc_Id
                        SessionManager.ListaAsientoWebRetorno.Item(index).DNI = .Numero_Doc
                        SessionManager.ListaAsientoWebRetorno.Item(index).Nombre = .Nombre_Clientes
                        SessionManager.ListaAsientoWebRetorno.Item(index).ApePaterno = .Apellido_P
                        SessionManager.ListaAsientoWebRetorno.Item(index).ApeMaterno = .Apellido_M
                        SessionManager.ListaAsientoWebRetorno.Item(index).Telefono = .Telefono
                        SessionManager.ListaAsientoWebRetorno.Item(index).Sexo = IIf(String.IsNullOrWhiteSpace(.Sexo), "X", .Sexo)
                        SessionManager.ListaAsientoWebRetorno.Item(index).Edad = .Edad
                        SessionManager.ListaAsientoWebRetorno.Item(index).NIT_Cliente = .Ruc_Contacto
                        SessionManager.ListaAsientoWebRetorno.Item(index).Razon_Social = .Razon_Social
                        SessionManager.ListaAsientoWebRetorno.Item(index).Rz_Direccion = .Rz_Direccion
                    End If

                End With

            Else
                If typeviaje = 1 Then
                    FlagTabX.Value = 0
                    SessionManager.ListaAsientoWebIda.Item(index).Tipo_Doc = typodoc
                    SessionManager.ListaAsientoWebIda.Item(index).DNI = numdoc
                    SessionManager.ListaAsientoWebIda.Item(index).Nombre = String.Empty
                    SessionManager.ListaAsientoWebIda.Item(index).ApePaterno = String.Empty
                    SessionManager.ListaAsientoWebIda.Item(index).ApeMaterno = String.Empty
                    SessionManager.ListaAsientoWebIda.Item(index).Telefono = String.Empty
                    'SessionManager.ListaAsientoWebIda.Item(index).Email = String.Empty
                    SessionManager.ListaAsientoWebIda.Item(index).Sexo = "F"
                    SessionManager.ListaAsientoWebIda.Item(index).Edad = 0
                    SessionManager.ListaAsientoWebIda.Item(index).NIT_Cliente = String.Empty
                    SessionManager.ListaAsientoWebIda.Item(index).Razon_Social = String.Empty
                    SessionManager.ListaAsientoWebIda.Item(index).Rz_Direccion = String.Empty
                ElseIf typeviaje = 2 Then
                    FlagTabX.Value = 1
                    SessionManager.ListaAsientoWebRetorno.Item(index).Tipo_Doc = typodoc
                    SessionManager.ListaAsientoWebRetorno.Item(index).DNI = numdoc
                    SessionManager.ListaAsientoWebRetorno.Item(index).Nombre = String.Empty
                    SessionManager.ListaAsientoWebRetorno.Item(index).ApePaterno = String.Empty
                    SessionManager.ListaAsientoWebRetorno.Item(index).ApeMaterno = String.Empty
                    SessionManager.ListaAsientoWebRetorno.Item(index).Telefono = String.Empty
                    'SessionManager.ListaAsientoWebRetorno.Item(index).Email = String.Empty
                    SessionManager.ListaAsientoWebRetorno.Item(index).Sexo = "F"
                    SessionManager.ListaAsientoWebRetorno.Item(index).Edad = 0
                    SessionManager.ListaAsientoWebRetorno.Item(index).NIT_Cliente = String.Empty
                    SessionManager.ListaAsientoWebRetorno.Item(index).Razon_Social = String.Empty
                    SessionManager.ListaAsientoWebRetorno.Item(index).Rz_Direccion = String.Empty
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Private Function PasajerosRegistradosIdaAll(ByRef index As Integer) As Boolean

        Try
            Dim oPLGlobals As New PLGlobals
            ''oListaAsientoWebIda = oPLGlobals.ListaPasajerosbyAsiento( "1" & SessionManager.IDCID, SessionManager.IdBusIda)
            Dim viajeida As Boolean = True

            For i As Integer = 0 To SessionManager.ListaAsientoWebIda.Count - 1
                If String.IsNullOrEmpty(SessionManager.ListaAsientoWebIda.Item(i).DNI) OrElse SessionManager.ListaAsientoWebIda.Item(i).DNI = "[Ingrese el Doc. Identidad]" OrElse SessionManager.ListaAsientoWebIda.Item(i).Guardado = False Then
                    index = i
                    viajeida = False
                    Exit For
                End If
            Next
            Return viajeida
        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function

    Private Function PasajerosRegistradosRetornoAll(ByRef index As Integer) As Boolean

        Try
            Dim oPLGlobals As New PLGlobals
            ''oListaAsientoWebRetorno = oPLGlobals.ListaPasajerosbyAsiento( "2" & SessionManager.IDCID, SessionManager.IdBusRetorno)
            Dim viajeretorno As Boolean = True

            For i As Integer = 0 To SessionManager.ListaAsientoWebRetorno.Count - 1
                If String.IsNullOrEmpty(SessionManager.ListaAsientoWebRetorno.Item(i).DNI) OrElse SessionManager.ListaAsientoWebRetorno.Item(i).DNI = "[Ingrese el Doc. Identidad]" OrElse SessionManager.ListaAsientoWebRetorno.Item(i).Guardado = False Then
                    index = i
                    viajeretorno = False
                    Exit For
                End If
            Next
            Return viajeretorno
        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function

    Private Function ValidarPasajerosAsientobyDocumento(ByVal oListaAsientos As ListaAsientos, ByVal documento As String, ByVal tipo_documento As String, ByVal IDS As Integer) As Boolean
        Try
            Dim valor As Boolean = False
            With oListaAsientos
                For n As Integer = 0 To .Count - 1
                    If .Item(n).DNI = documento AndAlso .Item(n).Tipo_Doc = tipo_documento And .Item(n).IDS <> IDS Then
                        valor = True
                        Exit For
                    End If
                Next
            End With

            Return valor
        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Function

    Private Sub FormatearPasajeros(ByRef oListaAsientos As ListaAsientos, ByVal type As Integer)
        Try
            With oListaAsientos
                For m As Int16 = 0 To oListaAsientos.Count - 1

                    If String.IsNullOrEmpty(.Item(m).Edad) OrElse .Item(m).Edad.Equals("[Ingrese la Edad]") Then
                        .Item(m).Edad = "0"
                    End If
                    If String.IsNullOrEmpty(.Item(m).Telefono) OrElse .Item(m).Telefono.Equals("[Ingrese el Telefono]") Then
                        .Item(m).Telefono = String.Empty
                    End If
                    If String.IsNullOrEmpty(.Item(m).Pais) OrElse .Item(m).Pais.Equals("[Ingrese el Direccion]") Then
                        .Item(m).Pais = String.Empty
                    End If
                    If String.IsNullOrEmpty(.Item(m).Email) OrElse .Item(m).Email.Equals("[Ingrese el Email]") Then
                        .Item(m).Email = String.Empty
                    End If
                    If String.IsNullOrEmpty(.Item(m).NIT_Cliente) OrElse .Item(m).NIT_Cliente.Equals("[Ingrese el RUC Empresa]") Then
                        .Item(m).NIT_Cliente = String.Empty
                    End If
                    If String.IsNullOrEmpty(.Item(m).Razon_Social) OrElse .Item(m).Razon_Social.Equals("[Ingrese la Razon Social]") Then
                        .Item(m).Razon_Social = String.Empty
                    End If
                    If String.IsNullOrEmpty(.Item(m).Rz_Direccion) OrElse .Item(m).Rz_Direccion.Equals("[Ingrese la Direccion Emp.]") Then
                        .Item(m).Rz_Direccion = String.Empty
                    End If
                    If String.IsNullOrEmpty(.Item(m).Rz_Telefono) OrElse .Item(m).Rz_Telefono.Equals("[Ingrese el Telefono Emp.]") Then
                        .Item(m).Rz_Telefono = String.Empty
                    End If
                    If type = 1 Then
                        .Item(m).Codi_Empresa = SessionManager.IdEmpresaIda
                    ElseIf type = 2 Then
                        .Item(m).Codi_Empresa = SessionManager.IdEmpresaRetorno
                    End If
                    ''.Item(m).Codi_Empresa = SessionManager.IdEmpresa
                Next
            End With

        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

#End Region

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
            If Functions.ValidarSeleccionPasajeros(p) = False Then
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
            PermisosObjetos()
            If Not IsPostBack Then

                CargarInformaciondeViajes()
                ListarPasajeros()

            End If
            If SessionManager.ViajeRetorno Then
                lbldatosretornomobile.Visible = True

            Else
                lbldatosretornomobile.Visible = False
            End If
            ScriptUser.JsRegister(Me, Me.ToString, "ValiTab();")
        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub


    Protected Sub dlpasajerosida_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlpasajerosida.ItemDataBound
        Try
            If status Then
                Dim oETablas1 As List(Of ETablas)
                Dim oPLGlobals As New PLGlobals

                oETablas1 = oPLGlobals.ListarTipoDocumentos()
                Dim oListaPuntoembarque As ListaPuntoVenta = oPLGlobals.ListarPuntosdeEmbarque(SessionManager.Codi_Sucursal_ida, SessionManager.Codi_Destino_Ida, SessionManager.ServicioIdIda, SessionManager.IdEmpresaIda, SessionManager.Codi_puntoVenta, SessionManager.Hora_Programacion_Ida)
                Dim oListaPuntoDesembarque As ListaPuntoVenta = oPLGlobals.ListarPuntosdeArribo(SessionManager.Codi_Sucursal_ida, SessionManager.Codi_Destino_Ida, SessionManager.ServicioIdIda, SessionManager.IdEmpresaIda, SessionManager.Codi_puntoVenta, SessionManager.Hora_Programacion_Ida)

                If oListaPuntoembarque.Count < 1 Then
                    Dim oETb_PuntoVenta As New ETb_PuntoVenta
                    oETb_PuntoVenta.Codi_puntoVenta = SessionManager.OrigenId_Ida
                    oETb_PuntoVenta.PuntoVenta = SessionManager.Origen_Ida & " HORA EMB. " & SessionManager.HoraViajeIda
                    oETb_PuntoVenta.HoraPaso = SessionManager.HoraViajeIda
                    oListaPuntoembarque.Add(oETb_PuntoVenta)
                End If

                If oListaPuntoDesembarque.Count < 1 Then
                    Dim oETb_PuntoVenta As New ETb_PuntoVenta
                    oETb_PuntoVenta.Codi_puntoVenta = SessionManager.DestinoId_Ida
                    oETb_PuntoVenta.PuntoVenta = SessionManager.Destino_Ida & " HORA DESEMB. " & SessionManager.HoraLlegadaViajeIda
                    oListaPuntoDesembarque.Add(oETb_PuntoVenta)
                End If

                Dim ddltipodocumento As DropDownList = CType(e.Item.FindControl("ddltipodocumento"), DropDownList)
                Dim ddlpuntoembarque As DropDownList = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList)
                Dim ddlpuntodesembarque As DropDownList = CType(e.Item.FindControl("ddlpuntodesembarque"), DropDownList)
                Dim tipodoc As String = CType(e.Item.FindControl("tipodoc"), Label).Text
                Dim puntodesembarque As String = CType(e.Item.FindControl("lblpuntodesembarque"), Label).Text
                Dim puntoembarque As String = CType(e.Item.FindControl("lblpuntoembarque"), Label).Text
                Dim ddlhoraembarque As DropDownList = CType(e.Item.FindControl("ddlhoraembarque"), DropDownList)

                If Not ddltipodocumento Is Nothing Then
                    ddltipodocumento.DataSource = oETablas1
                    ddltipodocumento.DataBind()
                    ddltipodocumento.SelectedValue = tipodoc
                End If


                If Not ddlpuntoembarque Is Nothing Then
                    ddlpuntoembarque.DataSource = oListaPuntoembarque
                    ddlpuntoembarque.DataBind()
                    Dim item As New ListItem("Seleccionar punto de embarque", "0")
                    ddlpuntoembarque.Items.Insert(0, item)
                    ddlpuntoembarque.SelectedValue = puntoembarque
                End If

                If Not ddlhoraembarque Is Nothing Then
                    ddlhoraembarque.DataSource = oListaPuntoembarque
                    ddlhoraembarque.DataBind()
                    Dim item As New ListItem("Seleccionar hora de embarque", "0")
                    ddlhoraembarque.Items.Insert(0, item)
                End If

                If Not ddlpuntodesembarque Is Nothing Then
                    ddlpuntodesembarque.DataSource = oListaPuntoDesembarque
                    ddlpuntodesembarque.DataBind()
                    Dim item As New ListItem("Seleccionar punto de desembarque", "0")
                    ddlpuntodesembarque.Items.Insert(0, item)
                    ddlpuntodesembarque.SelectedValue = puntodesembarque
                End If
            End If
        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Protected Sub dlpasajerosida_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlpasajerosida.CancelCommand

        Try
            FlagTabX.Value = 0
            If status Then
                dlpasajerosida.EditItemIndex = -1
                ListarPasajeroIda()

            End If

        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub dlpasajerosida_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlpasajerosida.DeleteCommand

        Try
            FlagTabX.Value = 0
            If status Then
                Dim Ids As Integer = CInt(CType(e.Item.FindControl("lblIDS"), Label).Text)

                If Ids > 0 Then
                    Dim oPlgAdelet As PLGlobals = New PLGlobals()
                    oPlgAdelet.EliminarAsientoSeleccionadoWeb(Ids)
                    SessionManager.ListaAsientoWebIda.RemoveAt(e.Item.ItemIndex)
                    oPlgAdelet = Nothing
                    dlpasajerosida.EditItemIndex = -1
                    ListarPasajeroIda()
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub dlpasajerosida_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlpasajerosida.EditCommand

        Try
            FlagTabX.Value = 0
            If status Then
                Dim indice As Int32 = e.Item.ItemIndex

                If indice = 0 Then
                    dlpasajerosida.EditItemIndex = e.Item.ItemIndex
                Else
                    Dim oPLGlobals As New PLGlobals
                    ''oListaAsientoWebIda = oPLGlobals.ListaPasajerosbyAsiento( "1" & SessionManager.IDCID, SessionManager.IdBusIda)
                    If String.IsNullOrEmpty(SessionManager.ListaAsientoWebIda.Item(indice - 1).DNI) OrElse SessionManager.ListaAsientoWebIda.Item(indice - 1).DNI.Equals("[Ingrese el Doc. Identidad]") Then
                        ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajeroBefor, 1)
                        Exit Sub
                    End If
                End If
                dlpasajerosida.EditItemIndex = indice
                ListarPasajeroIda(indice)
            End If

        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Protected Sub dlpasajerosida_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlpasajerosida.ItemCommand
        Try
            If status Then

                If e.CommandName = "Accion" Then
                    Dim oClieEmp As New PLGlobals()
                    Dim oEAsiento As New EAsiento

                    oEAsiento.Tipo_Doc = CType(e.Item.FindControl("ddltipodocumento"), DropDownList).SelectedValue.ToString
                    oEAsiento.IDS = CType(e.Item.FindControl("lblIDS"), Label).Text
                    oEAsiento.DNI = CType(e.Item.FindControl("txtnumdocumento"), TextBox).Text
                    oEAsiento.Nombre = CType(e.Item.FindControl("txtnombres"), TextBox).Text.ToUpper
                    oEAsiento.ApePaterno = CType(e.Item.FindControl("txtapaterno"), TextBox).Text.ToUpper
                    oEAsiento.ApeMaterno = CType(e.Item.FindControl("txtamaterno"), TextBox).Text.ToUpper
                    If String.IsNullOrEmpty(CType(e.Item.FindControl("ddlsexo"), DropDownList).SelectedValue.ToString) Then
                        oEAsiento.Sexo = "M"
                    Else
                        oEAsiento.Sexo = CType(e.Item.FindControl("ddlsexo"), DropDownList).SelectedValue.ToString
                    End If

                    If String.IsNullOrEmpty(CType(e.Item.FindControl("txtedad"), TextBox).Text) Then
                        oEAsiento.Edad = 0
                    Else
                        oEAsiento.Edad = CType(e.Item.FindControl("txtedad"), TextBox).Text
                    End If
                    oEAsiento.Telefono = CType(e.Item.FindControl("txttelefono"), TextBox).Text
                    oEAsiento.NIT_Cliente = CType(e.Item.FindControl("txtruc"), TextBox).Text
                    oEAsiento.Razon_Social = CType(e.Item.FindControl("txtrz"), TextBox).Text.ToUpper
                    oEAsiento.Rz_Direccion = CType(e.Item.FindControl("txtrzdireccion"), TextBox).Text.ToUpper
                    oEAsiento.Punto_Embarque = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList).SelectedValue.ToString
                    oEAsiento.Punto_Desembarque = CType(e.Item.FindControl("ddlpuntodesembarque"), DropDownList).SelectedValue.ToString
                    CType(e.Item.FindControl("ddlhoraembarque"), DropDownList).SelectedIndex = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList).SelectedIndex
                    oEAsiento.Dir_Embarque = oClieEmp.ObtenerDireccionEmbarque(SessionManager.IdEmpresaIda, oEAsiento.Punto_Embarque)
                    oEAsiento.Dir_Desembarque = oClieEmp.ObtenerDireccionEmbarque(SessionManager.IdEmpresaIda, oEAsiento.Punto_Desembarque)
                    oEAsiento.Oficina_Emision = SessionManager.OrigenId_Ida
                    oEAsiento.Hora_Embarque = CType(e.Item.FindControl("ddlhoraembarque"), DropDownList).SelectedItem.Text

                    If oEAsiento.Punto_Desembarque <= 0 OrElse oEAsiento.Punto_Embarque <= 0 Then
                        ScriptUser.JQueryMensaje(Me, Message.WSelectEmbarqueAndDesembarque)
                        Exit Sub
                    End If

                    If ValidarPasajerosAsientobyDocumento(SessionManager.ListaAsientoWebIda, oEAsiento.DNI, oEAsiento.Tipo_Doc, oEAsiento.IDS) = True Then
                        ScriptUser.JQueryMensaje(Me, Message.WNoPasajeroRegister)
                        CType(e.Item.FindControl("ddltipodocumento"), DropDownList).Focus()
                    Else
                        If String.IsNullOrWhiteSpace(oEAsiento.NIT_Cliente) = False Then
                            Dim oETb_Ruc As ETb_Ruc = oClieEmp.ValidarEmpresa(oEAsiento.NIT_Cliente)
                            If String.IsNullOrWhiteSpace(oETb_Ruc.Ruc_Cliente) = False AndAlso String.IsNullOrWhiteSpace(oETb_Ruc.Razon_Social) = False Then
                                oEAsiento.NIT_Cliente = oETb_Ruc.Ruc_Cliente
                                oEAsiento.Razon_Social = oETb_Ruc.Razon_Social
                                oEAsiento.Rz_Direccion = oETb_Ruc.Direccion
                            Else
                                ScriptUser.JQueryMensaje(Me, "El R.U.C. de la empresa ingresada no existe")
                                Exit Sub
                            End If
                        End If
                        If oClieEmp.RegistrarPasajeroAsiento(oEAsiento) Then
                            For i As Integer = 0 To SessionManager.ListaAsientoWebIda.Count - 1
                                If SessionManager.ListaAsientoWebIda.Item(i).IDS = oEAsiento.IDS Then
                                    SessionManager.ListaAsientoWebIda.Item(i).Tipo_Doc = oEAsiento.Tipo_Doc
                                    SessionManager.ListaAsientoWebIda.Item(i).DNI = oEAsiento.DNI
                                    SessionManager.ListaAsientoWebIda.Item(i).Nombre = oEAsiento.Nombre
                                    SessionManager.ListaAsientoWebIda.Item(i).ApePaterno = oEAsiento.ApePaterno
                                    SessionManager.ListaAsientoWebIda.Item(i).ApeMaterno = oEAsiento.ApeMaterno
                                    SessionManager.ListaAsientoWebIda.Item(i).Edad = oEAsiento.Edad
                                    SessionManager.ListaAsientoWebIda.Item(i).Telefono = oEAsiento.Telefono
                                    SessionManager.ListaAsientoWebIda.Item(i).Direccion = oEAsiento.Direccion
                                    SessionManager.ListaAsientoWebIda.Item(i).Edad = oEAsiento.Edad
                                    SessionManager.ListaAsientoWebIda.Item(i).Sexo = oEAsiento.Sexo
                                    SessionManager.ListaAsientoWebIda.Item(i).NIT_Cliente = oEAsiento.NIT_Cliente
                                    SessionManager.ListaAsientoWebIda.Item(i).Razon_Social = oEAsiento.Razon_Social
                                    SessionManager.ListaAsientoWebIda.Item(i).Rz_Direccion = oEAsiento.Rz_Direccion
                                    SessionManager.ListaAsientoWebIda.Item(i).Oficina_Emision = oEAsiento.Oficina_Emision
                                    SessionManager.ListaAsientoWebIda.Item(i).Punto_Embarque = oEAsiento.Punto_Embarque
                                    SessionManager.ListaAsientoWebIda.Item(i).Punto_Desembarque = oEAsiento.Punto_Desembarque
                                    SessionManager.ListaAsientoWebIda.Item(i).Hora_Embarque = oEAsiento.Hora_Embarque
                                    SessionManager.ListaAsientoWebIda.Item(i).NroPolizaBus = SessionManager.NroPolizaBusIda
                                    SessionManager.ListaAsientoWebIda.Item(i).Guardado = True


                                    If Not SessionManager.ListaAsientoWebRetorno Is Nothing AndAlso i <= SessionManager.ListaAsientoWebRetorno.Count - 1 Then
                                        SessionManager.ListaAsientoWebRetorno.Item(i).Tipo_Doc = oEAsiento.Tipo_Doc
                                        SessionManager.ListaAsientoWebRetorno.Item(i).DNI = oEAsiento.DNI
                                        SessionManager.ListaAsientoWebRetorno.Item(i).Nombre = oEAsiento.Nombre.ToUpper
                                        SessionManager.ListaAsientoWebRetorno.Item(i).ApePaterno = oEAsiento.ApePaterno.ToUpper
                                        SessionManager.ListaAsientoWebRetorno.Item(i).ApeMaterno = oEAsiento.ApeMaterno.ToUpper
                                        SessionManager.ListaAsientoWebRetorno.Item(i).Edad = oEAsiento.Edad
                                        SessionManager.ListaAsientoWebRetorno.Item(i).Telefono = oEAsiento.Telefono
                                        SessionManager.ListaAsientoWebRetorno.Item(i).Edad = oEAsiento.Edad
                                        SessionManager.ListaAsientoWebRetorno.Item(i).Sexo = oEAsiento.Sexo
                                        SessionManager.ListaAsientoWebRetorno.Item(i).NIT_Cliente = oEAsiento.NIT_Cliente
                                        SessionManager.ListaAsientoWebRetorno.Item(i).Razon_Social = oEAsiento.Razon_Social.ToUpper
                                        SessionManager.ListaAsientoWebRetorno.Item(i).Rz_Direccion = oEAsiento.Rz_Direccion.ToUpper
                                        SessionManager.ListaAsientoWebRetorno.Item(i).NroPolizaBus = SessionManager.NroPolizaBusRetorno
                                    End If
                                End If
                            Next
                            ScriptUser.JQueryMensaje(Me, Message.IRegisterPasajero)
                            ListarPasajeroIda()
                        Else
                            ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajero)
                        End If
                    End If

                End If
                If e.CommandName = "Search" Then
                    Dim tipo_doc As String = CType(e.Item.FindControl("ddltipodocumento"), DropDownList).SelectedValue.ToString
                    Dim numero_doc As String = CType(e.Item.FindControl("txtnumdocumento"), TextBox).Text
                    SearchPasajero(tipo_doc, numero_doc, e, 1, e.Item.ItemIndex)
                End If

                If e.CommandName = "Empresa" Then
                    Dim oPLGlobals As New PLGlobals
                    Dim oEAsiento As New EAsiento

                    oEAsiento.Tipo_Doc = CType(e.Item.FindControl("ddltipodocumento"), DropDownList).SelectedValue.ToString
                    oEAsiento.IDS = CType(e.Item.FindControl("lblIDS"), Label).Text
                    oEAsiento.DNI = CType(e.Item.FindControl("txtnumdocumento"), TextBox).Text
                    oEAsiento.Nombre = CType(e.Item.FindControl("txtnombres"), TextBox).Text
                    oEAsiento.ApePaterno = CType(e.Item.FindControl("txtapaterno"), TextBox).Text
                    oEAsiento.ApeMaterno = CType(e.Item.FindControl("txtamaterno"), TextBox).Text
                    If String.IsNullOrEmpty(CType(e.Item.FindControl("ddlsexo"), DropDownList).SelectedValue.ToString) Then
                        oEAsiento.Sexo = "M"
                    Else
                        oEAsiento.Sexo = CType(e.Item.FindControl("ddlsexo"), DropDownList).SelectedValue.ToString
                    End If

                    If String.IsNullOrEmpty(CType(e.Item.FindControl("txtedad"), TextBox).Text) Then
                        oEAsiento.Edad = 0
                    Else
                        oEAsiento.Edad = CType(e.Item.FindControl("txtedad"), TextBox).Text
                    End If
                    oEAsiento.Telefono = CType(e.Item.FindControl("txttelefono"), TextBox).Text
                    oEAsiento.NIT_Cliente = CType(e.Item.FindControl("txtruc"), TextBox).Text
                    oEAsiento.Razon_Social = CType(e.Item.FindControl("txtrz"), TextBox).Text.ToUpper
                    oEAsiento.Rz_Direccion = CType(e.Item.FindControl("txtrzdireccion"), TextBox).Text.ToUpper
                    oEAsiento.Punto_Embarque = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList).SelectedValue.ToString
                    oEAsiento.Punto_Desembarque = CType(e.Item.FindControl("ddlpuntodesembarque"), DropDownList).SelectedValue.ToString
                    CType(e.Item.FindControl("ddlhoraembarque"), DropDownList).SelectedIndex = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList).SelectedIndex
                    oEAsiento.Oficina_Emision = SessionManager.OrigenId_Ida
                    oEAsiento.Hora_Embarque = CType(e.Item.FindControl("ddlhoraembarque"), DropDownList).SelectedItem.Text



                    Dim oETb_Ruc As ETb_Ruc = oPLGlobals.BuscarEmpresaSunat(oEAsiento.NIT_Cliente)

                    For i As Integer = 0 To SessionManager.ListaAsientoWebIda.Count - 1
                        If SessionManager.ListaAsientoWebIda.Item(i).IDS = oEAsiento.IDS Then
                            SessionManager.ListaAsientoWebIda.Item(i).Tipo_Doc = oEAsiento.Tipo_Doc
                            SessionManager.ListaAsientoWebIda.Item(i).DNI = oEAsiento.DNI
                            SessionManager.ListaAsientoWebIda.Item(i).Nombre = oEAsiento.Nombre
                            SessionManager.ListaAsientoWebIda.Item(i).ApePaterno = oEAsiento.ApePaterno
                            SessionManager.ListaAsientoWebIda.Item(i).ApeMaterno = oEAsiento.ApeMaterno
                            SessionManager.ListaAsientoWebIda.Item(i).Edad = oEAsiento.Edad
                            SessionManager.ListaAsientoWebIda.Item(i).Telefono = oEAsiento.Telefono
                            SessionManager.ListaAsientoWebIda.Item(i).Direccion = oEAsiento.Direccion
                            SessionManager.ListaAsientoWebIda.Item(i).Edad = oEAsiento.Edad
                            SessionManager.ListaAsientoWebIda.Item(i).Sexo = oEAsiento.Sexo
                            SessionManager.ListaAsientoWebIda.Item(i).NIT_Cliente = oETb_Ruc.Ruc_Cliente
                            SessionManager.ListaAsientoWebIda.Item(i).Razon_Social = oETb_Ruc.Razon_Social
                            SessionManager.ListaAsientoWebIda.Item(i).Rz_Direccion = oETb_Ruc.Direccion
                            SessionManager.ListaAsientoWebIda.Item(i).Oficina_Emision = oEAsiento.Oficina_Emision
                            SessionManager.ListaAsientoWebIda.Item(i).Punto_Embarque = oEAsiento.Punto_Embarque
                            SessionManager.ListaAsientoWebIda.Item(i).Punto_Desembarque = oEAsiento.Punto_Desembarque
                            SessionManager.ListaAsientoWebIda.Item(i).Hora_Embarque = oEAsiento.Hora_Embarque
                        End If
                    Next
                End If

                If SessionManager.ViajeRetorno Then
                    dlpasajerosida.DataSource = SessionManager.ListaAsientoWebIda
                    dlpasajerosida.DataBind()
                    dlpasajerosretorno.DataSource = SessionManager.ListaAsientoWebRetorno
                    dlpasajerosretorno.DataBind()

                Else
                    dlpasajerosida.DataSource = SessionManager.ListaAsientoWebIda
                    dlpasajerosida.DataBind()
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub dlpasajerosida_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlpasajerosida.UpdateCommand

        Try
            FlagTabX.Value = 0
            If status Then
                Dim oClieEmp As New PLGlobals()
                Dim oEAsiento As New EAsiento

                oEAsiento.Tipo_Doc = CType(e.Item.FindControl("ddltipodocumento"), DropDownList).SelectedValue.ToString
                oEAsiento.DNI = CType(e.Item.FindControl("txtnumdocumento"), TextBox).Text
                oEAsiento.Nombre = CType(e.Item.FindControl("txtnombres"), TextBox).Text
                oEAsiento.ApePaterno = CType(e.Item.FindControl("txtapaterno"), TextBox).Text
                oEAsiento.ApeMaterno = CType(e.Item.FindControl("txtamaterno"), TextBox).Text
                If String.IsNullOrEmpty(CType(e.Item.FindControl("txtedad"), TextBox).Text) Then
                    oEAsiento.Edad = 0
                Else
                    oEAsiento.Edad = CType(e.Item.FindControl("txtedad"), TextBox).Text
                End If

                oEAsiento.Telefono = CType(e.Item.FindControl("txttelefono"), TextBox).Text
                oEAsiento.Direccion = String.Empty
                oEAsiento.Email = String.Empty
                oEAsiento.Sexo = CType(e.Item.FindControl("ddlsexo"), DropDownList).SelectedValue.ToString
                oEAsiento.NIT_Cliente = CType(e.Item.FindControl("txtruc"), TextBox).Text
                oEAsiento.Razon_Social = CType(e.Item.FindControl("txtrz"), TextBox).Text
                oEAsiento.Rz_Direccion = CType(e.Item.FindControl("txtrzdireccion"), TextBox).Text
                oEAsiento.Rz_Telefono = String.Empty
                oEAsiento.IDS = CInt(CType(e.Item.FindControl("lblIDS"), Label).Text)
                'oEAsiento.Oficina_Emision = SessionManager.OrigenId_Ida
                'oEAsiento.Punto_Embarque = SessionManager.OrigenId_Ida
                oEAsiento.Oficina_Emision = CType(e.Item.FindControl("ddloficinaemision"), DropDownList).SelectedValue
                oEAsiento.Punto_Embarque = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList).SelectedValue
                'Dim indicepuntoembarque = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList).SelectedIndex
                CType(e.Item.FindControl("ddlhoraembarque"), DropDownList).SelectedIndex = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList).SelectedIndex
                oEAsiento.Hora_Embarque = CType(e.Item.FindControl("ddlhoraembarque"), DropDownList).SelectedItem.Text


                If ValidarPasajerosAsientobyDocumento(SessionManager.ListaAsientoWebIda, oEAsiento.DNI, oEAsiento.Tipo_Doc, oEAsiento.IDS) = True Then
                    ScriptUser.JQueryMensaje(Me, Message.WNoPasajeroRegister, 1)
                    CType(e.Item.FindControl("txtnumdocumento"), TextBox).Focus()
                Else
                    If oClieEmp.RegistrarPasajeroAsiento(oEAsiento) Then
                        For i As Int16 = 0 To SessionManager.ListaAsientoWebIda.Count - 1
                            If SessionManager.ListaAsientoWebIda.Item(i).IDS = oEAsiento.IDS Then
                                SessionManager.ListaAsientoWebIda.Item(i).Tipo_Doc = oEAsiento.Tipo_Doc
                                SessionManager.ListaAsientoWebIda.Item(i).DNI = oEAsiento.DNI
                                SessionManager.ListaAsientoWebIda.Item(i).Nombre = oEAsiento.Nombre
                                SessionManager.ListaAsientoWebIda.Item(i).ApePaterno = oEAsiento.ApePaterno
                                SessionManager.ListaAsientoWebIda.Item(i).ApeMaterno = oEAsiento.ApeMaterno
                                SessionManager.ListaAsientoWebIda.Item(i).Edad = oEAsiento.Edad
                                SessionManager.ListaAsientoWebIda.Item(i).Telefono = oEAsiento.Telefono
                                SessionManager.ListaAsientoWebIda.Item(i).Direccion = oEAsiento.Direccion
                                SessionManager.ListaAsientoWebIda.Item(i).Email = oEAsiento.Email
                                SessionManager.ListaAsientoWebIda.Item(i).Sexo = oEAsiento.Sexo
                                SessionManager.ListaAsientoWebIda.Item(i).NIT_Cliente = oEAsiento.NIT_Cliente
                                SessionManager.ListaAsientoWebIda.Item(i).Razon_Social = oEAsiento.Razon_Social
                                SessionManager.ListaAsientoWebIda.Item(i).Rz_Direccion = oEAsiento.Rz_Direccion
                                SessionManager.ListaAsientoWebIda.Item(i).Rz_Telefono = oEAsiento.Rz_Telefono
                                SessionManager.ListaAsientoWebIda.Item(i).Oficina_Emision = oEAsiento.Oficina_Emision
                                SessionManager.ListaAsientoWebIda.Item(i).Punto_Embarque = oEAsiento.Punto_Embarque
                                SessionManager.ListaAsientoWebIda.Item(i).Hora_Embarque = oEAsiento.Hora_Embarque
                                ''If String.IsNullOrEmpty(emisionallida.Value) Then
                                ''    emisionallida.Value = 0
                                ''End If
                                ''If String.IsNullOrEmpty(embarqueallida.Value) Then
                                ''    embarqueallida.Value = 0
                                ''End If

                                ''If emisionallida.Value = 0 Then
                                ''    SessionManager.ListaAsientoWebIda.Item(i).Oficina_EmisionAll = False
                                ''ElseIf emisionallida.Value = 1 Then
                                ''    SessionManager.ListaAsientoWebIda.Item(i).Oficina_EmisionAll = True
                                ''End If
                                ''If embarqueallida.Value = 0 Then
                                ''    SessionManager.ListaAsientoWebIda.Item(i).Punto_EmbarqueAll = False
                                ''ElseIf embarqueallida.Value = 1 Then
                                ''    SessionManager.ListaAsientoWebIda.Item(i).Punto_EmbarqueAll = True
                                ''End If

                            End If
                        Next
                        dlpasajerosida.EditItemIndex = -1
                        ScriptUser.JQueryMensaje(Me, Message.IRegisterPasajero)
                        ListarPasajeroIda()
                        Dim indexida As Integer = 0
                        Dim indexretorno As Integer = 0
                        Dim viajeida As Boolean = PasajerosRegistradosIdaAll(indexida)
                        If viajeida AndAlso SessionManager.ViajeRetorno Then
                            Dim viajeretorno As Boolean = PasajerosRegistradosRetornoAll(indexretorno)
                            If viajeretorno = False Then
                                ScriptUser.JQueryMensaje(Me, Message.IAvisoRegisterPasajeroRetorno)
                                ''apasajeros.SelectedIndex = 1
                            End If
                        End If
                    Else
                        ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajero, 1)
                    End If
                End If
            End If
        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub dlpasajerosretorno_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlpasajerosretorno.ItemDataBound
        Try


            If status Then
                Dim oETablas1 As List(Of ETablas)
                Dim oPLGlobals As New PLGlobals

                oETablas1 = oPLGlobals.ListarTipoDocumentos()
                Dim oListaPuntoembarque As ListaPuntoVenta = oPLGlobals.ListarPuntosdeEmbarque(SessionManager.Codi_Sucursal_Retorno, SessionManager.Codi_Destino_Retorno, SessionManager.ServicioIdRetorno, SessionManager.IdEmpresaRetorno, SessionManager.Codi_PuntoVentaRetorno, SessionManager.Hora_Programacion_Retorno)
                Dim oListaPuntoDesembarque As ListaPuntoVenta = oPLGlobals.ListarPuntosdeArribo(SessionManager.Codi_Sucursal_Retorno, SessionManager.Codi_Destino_Retorno, SessionManager.ServicioIdRetorno, SessionManager.IdEmpresaRetorno, SessionManager.Codi_PuntoVentaRetorno, SessionManager.Hora_Programacion_Retorno)

                If oListaPuntoembarque.Count < 1 Then
                    Dim oETb_PuntoVenta As New ETb_PuntoVenta
                    oETb_PuntoVenta.Codi_puntoVenta = SessionManager.OrigenId_Retorno
                    oETb_PuntoVenta.PuntoVenta = SessionManager.Origen_Retorno & " HORA EMB. " & SessionManager.HoraViajeRetorno
                    oETb_PuntoVenta.HoraPaso = SessionManager.HoraViajeRetorno
                    oListaPuntoembarque.Add(oETb_PuntoVenta)
                End If

                If oListaPuntoDesembarque.Count < 1 Then
                    Dim oETb_PuntoVenta As New ETb_PuntoVenta
                    oETb_PuntoVenta.Codi_puntoVenta = SessionManager.DestinoId_Retorno
                    oETb_PuntoVenta.PuntoVenta = SessionManager.Destino_Retorno & " HORA DESEMB. " & SessionManager.HoraLlegadaViajeRetorno
                    oListaPuntoDesembarque.Add(oETb_PuntoVenta)
                End If

                Dim ddltipodocumento As DropDownList = CType(e.Item.FindControl("ddltipodocumento"), DropDownList)
                Dim ddlpuntoembarque As DropDownList = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList)
                Dim ddlpuntodesembarque As DropDownList = CType(e.Item.FindControl("ddlpuntodesembarque"), DropDownList)
                Dim tipodoc As String = CType(e.Item.FindControl("tipodoc"), Label).Text
                Dim puntodesembarque As String = CType(e.Item.FindControl("lblpuntodesembarque"), Label).Text
                Dim puntoembarque As String = CType(e.Item.FindControl("lblpuntoembarque"), Label).Text
                Dim ddlhoraembarque As DropDownList = CType(e.Item.FindControl("ddlhoraembarque"), DropDownList)

                If Not ddltipodocumento Is Nothing Then
                    ddltipodocumento.DataSource = oETablas1
                    ddltipodocumento.DataBind()
                    ddltipodocumento.SelectedValue = tipodoc
                End If


                If Not ddlpuntoembarque Is Nothing Then
                    ddlpuntoembarque.DataSource = oListaPuntoembarque
                    ddlpuntoembarque.DataBind()
                    Dim item As New ListItem("Seleccionar punto de embarque", "0")
                    ddlpuntoembarque.Items.Insert(0, item)
                    ddlpuntoembarque.SelectedValue = puntoembarque
                End If

                If Not ddlhoraembarque Is Nothing Then
                    ddlhoraembarque.DataSource = oListaPuntoembarque
                    ddlhoraembarque.DataBind()
                    Dim item As New ListItem("Seleccionar hora de embarque", "0")
                    ddlhoraembarque.Items.Insert(0, item)
                End If

                If Not ddlpuntodesembarque Is Nothing Then
                    ddlpuntodesembarque.DataSource = oListaPuntoDesembarque
                    ddlpuntodesembarque.DataBind()
                    Dim item As New ListItem("Seleccionar punto de desembarque", "0")
                    ddlpuntodesembarque.Items.Insert(0, item)
                    ddlpuntodesembarque.SelectedValue = puntodesembarque
                End If
            End If
        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub dlpasajerosretorno_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlpasajerosretorno.EditCommand

        Try
            FlagTabX.Value = 1
            If status Then
                Dim indice As Int32 = e.Item.ItemIndex

                If indice = 0 Then
                    dlpasajerosretorno.EditItemIndex = e.Item.ItemIndex
                Else
                    Dim oPLGlobals As New PLGlobals
                    ''oListaAsientoWebRetorno = oPLGlobals.ListaPasajerosbyAsiento( "2" & SessionManager.IDCID, SessionManager.IdBusRetorno)
                    If String.IsNullOrEmpty(SessionManager.ListaAsientoWebRetorno.Item(indice - 1).DNI) Then
                        ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajeroBefor, 1)
                        Exit Sub
                    End If
                End If
                dlpasajerosretorno.EditItemIndex = indice
                ListarPasajeroRetorno(indice)
            End If
        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub

    Protected Sub dlpasajerosretorno_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlpasajerosretorno.CancelCommand
        Try
            FlagTabX.Value = 1
            If status Then
                dlpasajerosretorno.EditItemIndex = -1
                ListarPasajeroRetorno()

            End If
        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub dlpasajerosretorno_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlpasajerosretorno.DeleteCommand


        Try
            FlagTabX.Value = 1
            If status Then
                Dim Ids As Integer = CInt(CType(e.Item.FindControl("lblIDS"), Label).Text)

                If Ids > 0 Then
                    Dim oPlgAdelet As PLGlobals = New PLGlobals()
                    oPlgAdelet.EliminarAsientoSeleccionadoWeb(Ids)
                    SessionManager.ListaAsientoWebRetorno.RemoveAt(e.Item.ItemIndex)
                    oPlgAdelet = Nothing
                    dlpasajerosida.EditItemIndex = -1
                    ListarPasajeroRetorno()
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub dlpasajerosretorno_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlpasajerosretorno.ItemCommand
        Try
            FlagTabX.Value = 1
            If status Then
                If e.CommandName = "Accion" Then
                    Dim oClieEmp As New PLGlobals()

                    Dim oEAsiento As New EAsiento

                    oEAsiento.Tipo_Doc = CType(e.Item.FindControl("ddltipodocumento"), DropDownList).SelectedValue.ToString
                    oEAsiento.IDS = CType(e.Item.FindControl("lblIDS"), Label).Text
                    oEAsiento.DNI = CType(e.Item.FindControl("txtnumdocumento"), TextBox).Text
                    oEAsiento.Nombre = CType(e.Item.FindControl("txtnombres"), TextBox).Text.ToUpper
                    oEAsiento.ApePaterno = CType(e.Item.FindControl("txtapaterno"), TextBox).Text.ToUpper
                    oEAsiento.ApeMaterno = CType(e.Item.FindControl("txtamaterno"), TextBox).Text.ToUpper
                    If String.IsNullOrEmpty(CType(e.Item.FindControl("ddlsexo"), DropDownList).SelectedValue.ToString) Then
                        oEAsiento.Sexo = "M"
                    Else
                        oEAsiento.Sexo = CType(e.Item.FindControl("ddlsexo"), DropDownList).SelectedValue.ToString
                    End If

                    If String.IsNullOrEmpty(CType(e.Item.FindControl("txtedad"), TextBox).Text) Then
                        oEAsiento.Edad = 0
                    Else
                        oEAsiento.Edad = CType(e.Item.FindControl("txtedad"), TextBox).Text
                    End If
                    oEAsiento.Telefono = CType(e.Item.FindControl("txttelefono"), TextBox).Text
                    oEAsiento.NIT_Cliente = CType(e.Item.FindControl("txtruc"), TextBox).Text
                    oEAsiento.Razon_Social = CType(e.Item.FindControl("txtrz"), TextBox).Text.ToUpper
                    oEAsiento.Rz_Direccion = CType(e.Item.FindControl("txtrzdireccion"), TextBox).Text.ToUpper
                    oEAsiento.Punto_Embarque = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList).SelectedValue.ToString
                    oEAsiento.Punto_Desembarque = CType(e.Item.FindControl("ddlpuntodesembarque"), DropDownList).SelectedValue.ToString
                    CType(e.Item.FindControl("ddlhoraembarque"), DropDownList).SelectedIndex = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList).SelectedIndex
                    oEAsiento.Dir_Embarque = oClieEmp.ObtenerDireccionEmbarque(SessionManager.IdEmpresaRetorno, oEAsiento.Punto_Embarque)
                    oEAsiento.Dir_Desembarque = oClieEmp.ObtenerDireccionEmbarque(SessionManager.IdEmpresaRetorno, oEAsiento.Punto_Desembarque)
                    oEAsiento.Oficina_Emision = SessionManager.OrigenId_Retorno
                    oEAsiento.Hora_Embarque = CType(e.Item.FindControl("ddlhoraembarque"), DropDownList).SelectedItem.Text

                    If oEAsiento.Punto_Desembarque <= 0 OrElse oEAsiento.Punto_Embarque <= 0 Then
                        ScriptUser.JQueryMensaje(Me, Message.WSelectEmbarqueAndDesembarque)
                        Exit Sub
                    End If

                    If ValidarPasajerosAsientobyDocumento(SessionManager.ListaAsientoWebRetorno, oEAsiento.DNI, oEAsiento.Tipo_Doc, oEAsiento.IDS) = True Then
                        ScriptUser.JQueryMensaje(Me, Message.WNoPasajeroRegister)
                        CType(e.Item.FindControl("ddltipodocumento"), DropDownList).Focus()
                    Else
                        If String.IsNullOrWhiteSpace(oEAsiento.NIT_Cliente) = False Then
                            Dim oETb_Ruc As ETb_Ruc = oClieEmp.ValidarEmpresa(oEAsiento.NIT_Cliente)
                            If String.IsNullOrWhiteSpace(oETb_Ruc.Ruc_Cliente) = False AndAlso String.IsNullOrWhiteSpace(oETb_Ruc.Razon_Social) = False Then
                                oEAsiento.NIT_Cliente = oETb_Ruc.Ruc_Cliente
                                oEAsiento.Razon_Social = oETb_Ruc.Razon_Social
                                oEAsiento.Rz_Direccion = oETb_Ruc.Direccion
                            Else
                                ScriptUser.JQueryMensaje(Me, "El R.U.C. de la empresa ingresada no existe")
                                Exit Sub
                            End If
                        End If
                        If oClieEmp.RegistrarPasajeroAsiento(oEAsiento) Then
                            For i As Integer = 0 To SessionManager.ListaAsientoWebRetorno.Count - 1
                                If SessionManager.ListaAsientoWebRetorno.Item(i).IDS = oEAsiento.IDS Then
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Tipo_Doc = oEAsiento.Tipo_Doc
                                    SessionManager.ListaAsientoWebRetorno.Item(i).DNI = oEAsiento.DNI
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Nombre = oEAsiento.Nombre
                                    SessionManager.ListaAsientoWebRetorno.Item(i).ApePaterno = oEAsiento.ApePaterno
                                    SessionManager.ListaAsientoWebRetorno.Item(i).ApeMaterno = oEAsiento.ApeMaterno
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Edad = oEAsiento.Edad
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Telefono = oEAsiento.Telefono
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Direccion = oEAsiento.Direccion
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Email = oEAsiento.Email
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Sexo = oEAsiento.Sexo
                                    SessionManager.ListaAsientoWebRetorno.Item(i).NIT_Cliente = oEAsiento.NIT_Cliente
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Razon_Social = oEAsiento.Razon_Social
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Rz_Direccion = oEAsiento.Rz_Direccion.ToUpper
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Oficina_Emision = oEAsiento.Oficina_Emision
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Punto_Embarque = oEAsiento.Punto_Embarque
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Punto_Desembarque = oEAsiento.Punto_Desembarque
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Hora_Embarque = oEAsiento.Hora_Embarque
                                    SessionManager.ListaAsientoWebRetorno.Item(i).NroPolizaBus = SessionManager.NroPolizaBusRetorno
                                    SessionManager.ListaAsientoWebRetorno.Item(i).Guardado = True
                                End If
                            Next
                            ScriptUser.JQueryMensaje(Me, Message.IRegisterPasajero)
                            ListarPasajeroIda()
                        Else
                            ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajero)
                        End If
                    End If

                End If

                If e.CommandName = "Search" Then
                    Dim tipo_doc As String = CType(e.Item.FindControl("ddltipodocumento"), DropDownList).SelectedValue.ToString
                    Dim numero_doc As String = CType(e.Item.FindControl("txtnumdocumento"), TextBox).Text
                    SearchPasajero(tipo_doc, numero_doc, e, 2, e.Item.ItemIndex)
                End If

                If SessionManager.ViajeRetorno Then
                    dlpasajerosida.DataSource = SessionManager.ListaAsientoWebIda
                    dlpasajerosida.DataBind()
                    dlpasajerosretorno.DataSource = SessionManager.ListaAsientoWebRetorno
                    dlpasajerosretorno.DataBind()

                Else
                    dlpasajerosida.DataSource = SessionManager.ListaAsientoWebIda
                    dlpasajerosida.DataBind()
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub dlpasajerosretorno_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlpasajerosretorno.UpdateCommand

        Try
            If status Then
                Dim oClieEmp As New PLGlobals()
                Dim oEAsiento As New EAsiento
                FlagTabX.Value = 1
                oEAsiento.Tipo_Doc = CType(e.Item.FindControl("ddltipodocumento"), DropDownList).SelectedValue.ToString
                oEAsiento.DNI = CType(e.Item.FindControl("txtnumdocumento"), TextBox).Text
                oEAsiento.Nombre = CType(e.Item.FindControl("txtnombres"), TextBox).Text
                oEAsiento.ApePaterno = CType(e.Item.FindControl("txtapaterno"), TextBox).Text
                oEAsiento.ApeMaterno = CType(e.Item.FindControl("txtamaterno"), TextBox).Text
                If String.IsNullOrEmpty(CType(e.Item.FindControl("txtedad"), TextBox).Text) Then
                    oEAsiento.Edad = 0
                Else
                    oEAsiento.Edad = CType(e.Item.FindControl("txtedad"), TextBox).Text
                End If

                oEAsiento.Telefono = CType(e.Item.FindControl("txttelefono"), TextBox).Text
                oEAsiento.Direccion = String.Empty
                oEAsiento.Email = String.Empty
                'oEAsiento.Sexo = CType(e.Item.FindControl("ddlsexo"), DropDownList).SelectedValue.ToString

                If String.IsNullOrEmpty(CType(e.Item.FindControl("ddlsexo"), DropDownList).SelectedValue.ToString) Then
                    oEAsiento.Sexo = "M"
                Else
                    oEAsiento.Sexo = CType(e.Item.FindControl("ddlsexo"), DropDownList).SelectedValue.ToString
                End If

                oEAsiento.NIT_Cliente = CType(e.Item.FindControl("txtruc"), TextBox).Text
                oEAsiento.Razon_Social = CType(e.Item.FindControl("txtrz"), TextBox).Text
                oEAsiento.Rz_Direccion = CType(e.Item.FindControl("txtrzdireccion"), TextBox).Text
                oEAsiento.Rz_Telefono = String.Empty
                oEAsiento.IDS = CInt(CType(e.Item.FindControl("lblIDS"), Label).Text)
                'oEAsiento.Oficina_Emision = SessionManager.OrigenId_Ida
                'oEAsiento.Punto_Embarque = SessionManager.OrigenId_Retorno
                oEAsiento.Oficina_Emision = CType(e.Item.FindControl("ddloficinaemision"), DropDownList).SelectedValue
                oEAsiento.Punto_Embarque = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList).SelectedValue
                CType(e.Item.FindControl("ddlhoraembarque"), DropDownList).SelectedIndex = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList).SelectedIndex
                oEAsiento.Hora_Embarque = CType(e.Item.FindControl("ddlhoraembarque"), DropDownList).SelectedItem.Text
                CType(e.Item.FindControl("lbldirembarque"), Label).Text = CType(e.Item.FindControl("ddlpuntoembarque"), DropDownList).SelectedIndex

                If ValidarPasajerosAsientobyDocumento(SessionManager.ListaAsientoWebRetorno, oEAsiento.DNI, oEAsiento.Tipo_Doc, oEAsiento.IDS) = True Then
                    ScriptUser.JQueryMensaje(Me, Message.WNoPasajeroRegister, 1)
                    CType(e.Item.FindControl("txtnumdocumento"), TextBox).Focus()
                Else
                    If oClieEmp.RegistrarPasajeroAsiento(oEAsiento) Then
                        With SessionManager.ListaAsientoWebRetorno
                            For i As Int16 = 0 To .Count - 1
                                If .Item(i).IDS = oEAsiento.IDS Then
                                    .Item(i).Tipo_Doc = oEAsiento.Tipo_Doc
                                    .Item(i).DNI = oEAsiento.DNI
                                    .Item(i).Nombre = oEAsiento.Nombre
                                    .Item(i).ApePaterno = oEAsiento.ApePaterno
                                    .Item(i).ApeMaterno = oEAsiento.ApeMaterno
                                    .Item(i).Edad = oEAsiento.Edad
                                    .Item(i).Telefono = oEAsiento.Telefono
                                    .Item(i).Direccion = oEAsiento.Direccion
                                    .Item(i).Email = oEAsiento.Email
                                    .Item(i).Sexo = oEAsiento.Sexo
                                    .Item(i).NIT_Cliente = oEAsiento.NIT_Cliente
                                    .Item(i).Razon_Social = oEAsiento.Razon_Social
                                    .Item(i).Rz_Direccion = oEAsiento.Rz_Direccion
                                    .Item(i).Rz_Telefono = oEAsiento.Rz_Telefono
                                    .Item(i).Oficina_Emision = oEAsiento.Oficina_Emision
                                    .Item(i).Punto_Embarque = oEAsiento.Punto_Embarque
                                    .Item(i).Hora_Embarque = oEAsiento.Hora_Embarque

                                    ''If String.IsNullOrEmpty(emisionallretorno.Value) Then
                                    ''    emisionallretorno.Value = 0
                                    ''End If
                                    ''If String.IsNullOrEmpty(embarqueallretorno.Value) Then
                                    ''    embarqueallretorno.Value = 0
                                    ''End If

                                    ''If emisionallretorno.Value = 0 Then
                                    ''    .Item(i).Oficina_EmisionAll = False
                                    ''ElseIf emisionallretorno.Value = 1 Then
                                    ''    .Item(i).Oficina_EmisionAll = True
                                    ''End If
                                    ''If embarqueallretorno.Value = 0 Then
                                    ''    .Item(i).Punto_EmbarqueAll = False
                                    ''ElseIf embarqueallretorno.Value = 1 Then
                                    ''    .Item(i).Punto_EmbarqueAll = True
                                    ''End If
                                End If
                            Next
                        End With

                        dlpasajerosretorno.EditItemIndex = -1
                        ScriptUser.JQueryMensaje(Me, Message.IRegisterPasajero)
                        ListarPasajeroRetorno()
                        Dim indexida As Integer = 0
                        Dim viajeretorno As Boolean = PasajerosRegistradosRetornoAll(indexida)
                        If viajeretorno Then
                            Dim viajeida As Boolean = PasajerosRegistradosIdaAll(indexida)
                            If viajeida = False Then
                                ScriptUser.JQueryMensaje(Me, Message.IAvisoRegisterPasajeroIda)
                                ''apasajeros.SelectedIndex = 0
                            End If
                        End If
                    Else
                        ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajero, 1)
                    End If
                End If
            End If

        Catch ex As Exception
            Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try
    End Sub

    Protected Sub btncontinuar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncontinuar.Click
        Dim x As Integer = 0
        Try
            If status Then
                If SessionManager.ViajeRetorno Then
                    For n As Integer = 0 To SessionManager.ListaAsientoWebIda.Count - 1
                        With SessionManager.ListaAsientoWebIda.Item(n)
                            If ValidarPasajerosAsientobyDocumento(SessionManager.ListaAsientoWebIda, .DNI, .Tipo_Doc, .IDS) Then
                                ScriptUser.JQueryMensaje(Me, "Los pasajeros del viaje de ida tiene que tener datos diferentes. Por favor corregirlos")
                                Exit Sub
                            End If
                        End With
                    Next
                    For n As Integer = 0 To SessionManager.ListaAsientoWebRetorno.Count - 1
                        With SessionManager.ListaAsientoWebRetorno.Item(n)
                            If ValidarPasajerosAsientobyDocumento(SessionManager.ListaAsientoWebRetorno, .DNI, .Tipo_Doc, .IDS) Then
                                ScriptUser.JQueryMensaje(Me, "Los pasajeros del viaje de retorno tiene que tener datos diferentes. Por favor corregirlos")
                                Exit Sub
                            End If
                        End With
                    Next
                    Dim indexida As Integer = 0
                    Dim indexretorno As Integer = 0
                    Dim viajeida As Boolean = PasajerosRegistradosIdaAll(indexida)
                    Dim viajeretorno As Boolean = PasajerosRegistradosRetornoAll(indexretorno)
                    If viajeida = False Then
                        ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajeroAllIda)
                        ListarPasajeroIda(indexida)
                        Exit Sub
                    ElseIf viajeretorno = False Then
                        ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajeroAllRetorno)
                        ListarPasajeroRetorno(indexretorno)
                        Exit Sub
                    Else
                        FormatearPasajeros(SessionManager.ListaAsientoWebIda, 1)
                        FormatearPasajeros(SessionManager.ListaAsientoWebRetorno, 2)
                        SessionManager.SelectionPasajeros = True
                        x = 1
                        ''CreaUsuario()
                        SessionManager.CorreoCliente = txtCorreo.Text.Trim
                        Response.Redirect("confirmarpago.aspx", False)
                        ''Response.Redirect("confirmarcompra.aspx", False)
                        Exit Sub
                    End If

                ElseIf SessionManager.ViajeRetorno = False Then
                    For n As Integer = 0 To SessionManager.ListaAsientoWebIda.Count - 1
                        With SessionManager.ListaAsientoWebIda.Item(n)
                            If ValidarPasajerosAsientobyDocumento(SessionManager.ListaAsientoWebIda, .DNI, .Tipo_Doc, .IDS) Then
                                ScriptUser.JQueryMensaje(Me, "Los pasajeros del viaje de ida tiene que tener datos diferentes. Por favor corregirlos")
                                Exit Sub
                            End If
                        End With
                    Next
                    Dim indexida As Integer = 0
                    Dim viajeida As Boolean = PasajerosRegistradosIdaAll(indexida)

                    If viajeida = False Then
                        ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajeroAll)
                        ListarPasajeroIda(indexida)
                    Else
                        FormatearPasajeros(SessionManager.ListaAsientoWebIda, 1)
                        SessionManager.SelectionPasajeros = True
                        x = 1
                        ''CreaUsuario()
                        SessionManager.CorreoCliente = txtCorreo.Text.Trim
                        Response.Redirect("confirmarpago.aspx", True)
                        ''Response.Redirect("confirmarcompra.aspx", True)
                        Exit Sub
                    End If

                End If
            End If

        Catch ex As System.Exception
            If x = 0 Then
                Log.Instance(GetType(asignarpasajero)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
            End If
        End Try


        'Try
        '    If status Then
        '        If SessionManager.ViajeRetorno Then
        '            Dim indexida As Integer = 0
        '            Dim indexretorno As Integer = 0

        '            Dim viajeida As Boolean = PasajerosRegistradosIdaAll(indexida)
        '            Dim viajeretorno As Boolean = PasajerosRegistradosRetornoAll(indexretorno)
        '            If viajeida = False Then
        '                ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajeroAllIda, 1)
        '                ''apasajeros.SelectedIndex = 0
        '                dlpasajerosida.EditItemIndex = indexida
        '                ListarPasajeroIda(indexida)
        '                Exit Sub
        '            ElseIf viajeretorno = False Then
        '                ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajeroAllRetorno, 1)
        '                ''apasajeros.SelectedIndex = 1
        '                dlpasajerosretorno.EditItemIndex = indexretorno
        '                ListarPasajeroRetorno(indexretorno)
        '                Exit Sub
        '            Else
        '                FormatearPasajeros(SessionManager.ListaAsientoWebIda, 1)
        '                FormatearPasajeros(SessionManager.ListaAsientoWebRetorno, 2)
        '                SessionManager.SelectionPasajeros = True
        '                Response.Redirect("confirmarcompra.aspx", False)
        '                Exit Sub
        '            End If
        '        ElseIf SessionManager.ViajeRetorno = False Then
        '            Dim indexida As Integer = 0
        '            Dim viajeida As Boolean = PasajerosRegistradosIdaAll(indexida)
        '            If viajeida = False Then
        '                ScriptUser.JQueryMensaje(Me, Message.WRegisterPasajeroAll, 1)
        '                ''apasajeros.SelectedIndex = 0
        '                dlpasajerosida.EditItemIndex = indexida
        '                ListarPasajeroIda(indexida)
        '            Else
        '                FormatearPasajeros(SessionManager.ListaAsientoWebIda, 1)
        '                SessionManager.SelectionPasajeros = True
        '                Response.Redirect("confirmarcompra.aspx", False)
        '                Exit Sub
        '            End If
        '        End If
        '    End If

        'Catch ex As Exception
        '    If String.IsNullOrEmpty(Variables.ErrorMetodo) Then
        '        Variables.ErrorMetodo = "btncontinuar_Click"
        '    End If
        '    Functions.ControlarException(ex)
        'End Try

    End Sub

    Protected Sub btnregresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnregresar.Click
        Response.Redirect("seleccionarasientos.aspx#bus")
        'CAMBIOS(WILLIAMS)
        '================
        'ListarPasajeroIda()
        'ListarPasajeroRetorno()
        'SearchPasajero()
        'dlpasajerosida_ItemDataBound()
        'dlpasajerosida_ItemCommand()
        'dlpasajerosretorno_ItemDataBound()
        'dlpasajerosretorno_ItemCommand()
    End Sub

    Protected Sub ddmanu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim var1 As Integer = 2
        Dim var2 As Integer = 3

        Dim sum As Integer = var1 + var2


    End Sub



#End Region


End Class
