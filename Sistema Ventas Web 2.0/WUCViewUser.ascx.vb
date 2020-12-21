
Partial Class WUCViewUser
    Inherits System.Web.UI.UserControl



    Public Property UserName() As String
        Get
            Return lbusers.Text
        End Get
        Set(ByVal value As String)
            lbusers.Text = value
        End Set
    End Property
    Public Property LogueoStart() As Boolean
        Get
            Return viewlogueo.Visible
        End Get
        Set(ByVal value As Boolean)
            viewlogueo.Visible = value
        End Set
    End Property
    Public Property LogueoEnd() As Boolean
        Get
            Return loguot.Visible
        End Get
        Set(ByVal value As Boolean)
            loguot.Visible = value
        End Set
    End Property



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''ScriptUser.JsRegister(Me, Me.ToString, "ValidaMain()")
        If SessionUser.IsCounter OrElse SessionUser.IsAdmin Then
            reservas.Visible = True
        Else
            reservas.Visible = False
        End If
        If SessionUser.IsAdmin Then
            mediopago.Visible = True
            timereservas.Visible = True
        Else
            mediopago.Visible = False
            timereservas.Visible = False
        End If
    End Sub
End Class
