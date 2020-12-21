Public Class AuthorizationRequest
    Private _channel As String
    Public Property channel() As String
        Get
            Return _channel
        End Get
        Set(ByVal value As String)
            _channel = value
        End Set
    End Property

    Private _captureType As String
    Public Property captureType() As String
        Get
            Return _captureType
        End Get
        Set(ByVal value As String)
            _captureType = value
        End Set
    End Property

    Private _countable As Boolean
    Public Property countable() As Boolean
        Get
            Return _countable
        End Get
        Set(ByVal value As Boolean)
            _countable = value
        End Set
    End Property

    Private _order As Order
    Public Property order() As Order
        Get
            Return _order
        End Get
        Set(ByVal value As Order)
            _order = value
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

    Private _sponsored As Sponsored
    Public Property sponsored() As Sponsored
        Get
            Return _sponsored
        End Get
        Set(ByVal value As Sponsored)
            _sponsored = value
        End Set
    End Property




End Class
