Imports PEntity
Imports PUtilitario
Imports PDataAccess

Public Class SessionUser


    Public Shared Sub InitialTransaction()

        Try
            SessionManager.Reservacion = False
            SessionManager.SelectDestinos = False
            SessionManager.ViajeRetorno = False
            SessionManager.SelectDestinosIda = False
            SessionManager.SelectDestinosRetorno = False
            SessionManager.SelectionRuta = False
            SessionManager.SelectionAsientos = False
            SessionManager.SelectionPasajeros = False
            SessionManager.SelectionPago = False
            SessionManager.SelectionTransaction = False
            SessionManager.SelectionOKTransaction = False
            SessionManager.ListaAsientoWebIda = Nothing
            SessionManager.ListaAsientoWebRetorno = Nothing
            SessionManager.RutaConcatenadaIda = False
            SessionManager.RutaConcatenadaRetorno = False
            SessionManager.IdVenta = -1
            SessionManager.IsShoViable = 1
            ResetCantAsientos()
            SessionManager.IdEmpresaIda = AppSettings.valueString("Empresa")
            SessionManager.IdEmpresaRetorno = AppSettings.valueString("Empresa")
            SessionManager.Promociones = CBool(AppSettings.valueString("Promo"))
            SessionManager.TransactionPromociones = False
            ''SessionManager.IDCID = ""
            ''SessionManager.TimeOut = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Shared Sub EndAll(ByVal s As HttpSessionState)
        Try
            Dim oClsGobals As New ClsGobals
            Functions.EliminarAsientobySession()
            Functions.EliminarAsientosCaducados()
            oClsGobals.EliminaReservas(SessionManager.Id_WebOrders)
            s.Abandon()
            System.Web.Security.FormsAuthentication.SignOut()
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Public Shared Sub FinalizeSession(s As HttpSessionState)
        Try
            Functions.EliminarReservasBloqueadas(s)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Shared Sub GlobalInitSession(ByVal p As HttpSessionState)
        Try
            SessionManager.IDCID = p.SessionID
            SessionManager.TimeOut = p.Timeout
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Shared Sub ResetCantAsientos()

        Try
            SessionManager.CantidadAsientos = 0
            SessionManager.CantidadAsientosIda = 0
            SessionManager.CantidadAsientosRetorno = 0
            SessionManager.MountOrder = 0.0
            SessionManager.MountVisa = 0.0
            SessionManager.MontoIda = 0.0
            SessionManager.MontoRetorno = 0.0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Shared Function IsAdmin() As Boolean
        If SessionManager.NivelUser >= 8 Then Return True
    End Function

    Shared Function IsCounter() As Boolean
        If SessionManager.NivelUser = 7 Then Return True
    End Function


    Public Shared Sub UserLoaData(ByVal olistausers As ListaUsers)

        Try
            If olistausers.Count = 1 Then

                With olistausers.Item(0)

                    SessionManager.IdUser = CInt(.Id_Users)
                    SessionManager.IdPerfil = SessionManager.IdUser
                    SessionManager.Name = .Usuario_Users
                    SessionManager.NivelUser = CInt(.Nivel_Users)
                    If String.IsNullOrEmpty(.Apellidos_Users) OrElse String.IsNullOrEmpty(.Nombres_Users) Then
                        SessionManager.PerfilName = .Usuario_Users
                    Else
                        SessionManager.PerfilName = .Apellidos_Users & " " & .Nombres_Users
                    End If
                    SessionManager.Pws = .Password_Users
                    SessionManager.Correo_Users = .Correo_Users
                    SessionManager.FechaReg_Users = .FechaReg_Users
                End With
            Else
                olistausers = Nothing
            End If
            olistausers = Nothing
        Catch ex As Exception
            Throw
        End Try
    End Sub


End Class
