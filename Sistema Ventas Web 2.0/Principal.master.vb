
Partial Class Principal
    Inherits System.Web.UI.MasterPage


    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        ScriptUser.JQueryMensaje(Me, "Error en la página :\n" + e.ToString)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SessionManager.TimeOut = Session.Timeout
        ''ScriptUser.JsRegister(Me, Me.ToString, "MainValidation();")
        If SessionUser.IsAdmin Then

        End If
    End Sub
End Class

