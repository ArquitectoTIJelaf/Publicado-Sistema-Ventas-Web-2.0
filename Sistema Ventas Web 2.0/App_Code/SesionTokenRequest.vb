Public Class SesionTokenRequest
    Private _channel As String
    Public Property channel() As String
        Get
            Return _channel
        End Get
        Set(ByVal value As String)
            _channel = value
        End Set
    End Property

    Private _amount As Double
    Public Property amount() As String
        Get
            Return _amount
        End Get
        Set(ByVal value As String)
            _amount = value
        End Set
    End Property

    Private _recurrenceMaxAmount As Double
    Public Property recurrenceMaxAmount() As Double
        Get
            Return _recurrenceMaxAmount
        End Get
        Set(ByVal value As Double)
            _recurrenceMaxAmount = value
        End Set
    End Property

    Private _antifraud As Antifraud
    Public Property antifraud() As Antifraud
        Get
            Return _antifraud
        End Get
        Set(ByVal value As Antifraud)
            _antifraud = value
        End Set
    End Property
End Class
