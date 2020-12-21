Partial Class Ends
    Inherits System.Web.UI.Page

    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        ScriptUser.JQueryMensaje(Me, "Error en la página ")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SessionUser.EndAll(Session)
        Response.Redirect("destinos.aspx")

    End Sub
End Class
