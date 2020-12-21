Imports System.Data
Imports System.IO
Imports PLogic
Imports PEntity
Imports System.Collections.Generic
Imports Microsoft.Reporting.WebForms
Imports PUtilitario
Partial Class reporte
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Me.Title = Functions.NombreTituloPagina(Me.Title)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            MostrarReporte()
        End If
    End Sub

    Private Sub MostrarReporte()
        If Functions.UserLogueo(Me) = False Then
            Response.Redirect("end.aspx")
            Exit Sub
        End If

        Dim oListaVenta As New ListaVenta
        Dim oPLGlobals As New PLGlobals
        Dim fileReporte As String = "~/Reportes/RpTicket.rdlc"
        oListaVenta = oPLGlobals.TicketVentaWeb( SessionManager.Id_WebOrders)
        If oListaVenta.Count > 0 Then
            If File.Exists(Server.MapPath(fileReporte)) = True Then
                ReportViewer1.ProcessingMode = ProcessingMode.Local
                ReportViewer1.LocalReport.EnableExternalImages = True
                ReportViewer1.LocalReport.ReportPath = Server.MapPath(fileReporte)
                Dim datasource As New ReportDataSource("DataSet1", oListaVenta)
                ReportViewer1.LocalReport.DataSources.Clear()
                ReportViewer1.LocalReport.DataSources.Add(datasource)
            Else
                ScriptUser.JQueryMensaje(Me, "No se encontro el reporte.")
            End If
        Else
            ScriptUser.JQueryMensaje(Me, "No hay datos")
        End If
    End Sub
End Class
