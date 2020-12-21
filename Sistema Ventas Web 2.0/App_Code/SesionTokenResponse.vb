Public Class SesionTokenResponse
    Private _sessionKey As String
    Public Property sessionKey() As String
        Get
            Return _sessionKey
        End Get
        Set(ByVal value As String)
            _sessionKey = value
        End Set
    End Property

End Class
