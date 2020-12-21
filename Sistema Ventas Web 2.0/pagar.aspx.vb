Imports System.Data
Imports PLogic
Imports PEntity
Imports System.Collections.Generic
Imports System.IO
Imports System.Xml
Imports PDataAccess
Imports PUtilitario

Partial Class pagar
    Inherits System.Web.UI.Page

    Private oG As PLGlobals
    Dim objPDAGlobals As New ClsGobals
    Public StrMOUNT As String = String.Empty
    Public StrNumOrder As String = String.Empty
    Public merchantIds As String = AppSettings.valueString("merchantId").ToString.Trim
    Public StrActtion As String = String.Empty

#Region "Metodos Privados con Eventos"

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Me.Title = Functions.NombreTituloPagina(Me.Title)
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim p As String = String.Empty
            If Functions.ValidarSeleccionPago(p) = False Then
                Response.Redirect(p, False)
                Exit Sub
            End If
            barnav.Visible = False
            bannerrespuesta.Visible = False
            'WUCBanner1.Visible = False
            idwprocess.Visible = True
            If Functions.UserLogueo(Me) Then
                WUCViewUser1.UserName = SessionManager.PerfilName
                WUCViewUser1.LogueoEnd = False
                WUCViewUser1.LogueoStart = True
            Else
                WUCViewUser1.LogueoEnd = True
                WUCViewUser1.LogueoStart = False
            End If

        Catch ex As Exception
            Dim oPLGlobals As New PLGlobals
            barnav.Visible = True
            bannerrespuesta.Visible = True
            'WUCBanner1.Visible = True
            idwprocess.Visible = False
            DivErrsave.InnerHtml = Message.EPagoTransaccionSystem
            oPLGlobals.RegistraErroresTransaccionSales(Message.EPagoTransaccionSystem, Now, SessionManager.IdUser, SessionManager.NumOrden, 1)
            Log.Instance(GetType(pagar)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)

        Catch ex As Exception
            Log.Instance(GetType(pagar)).LogError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex)
        End Try

    End Sub



    Protected Sub cmdSalirsis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSalirsis.Click
        Response.Redirect("destinos.aspx", False)
    End Sub


#End Region


End Class
