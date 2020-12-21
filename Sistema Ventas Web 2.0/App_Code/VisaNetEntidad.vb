Public Class VisaNetEntidad



    Private _SessionToken As String
    Public Property SessionToken() As String
        Get
            Return _SessionToken
        End Get
        Set(ByVal value As String)
            _SessionToken = value
        End Set
    End Property


    Private _CodigoComercio As String
    Public Property CodigoComercio() As String
        Get
            Return _CodigoComercio
        End Get
        Set(ByVal value As String)
            _CodigoComercio = value
        End Set
    End Property


    Private _Monto As String
    Public Property Monto() As String
        Get
            Return _Monto
        End Get
        Set(ByVal value As String)
            _Monto = value
        End Set
    End Property


    Private _NumeroPedido As String
    Public Property NumeroPedido() As String
        Get
            Return _NumeroPedido
        End Get
        Set(ByVal value As String)
            _NumeroPedido = value
        End Set
    End Property

    Private _Channel As String
    Public Property Channel() As String
        Get
            Return _Channel
        End Get
        Set(ByVal value As String)
            _Channel = value
        End Set
    End Property

    Private _UrlTimeout As String
    Public Property UrlTimeout() As String
        Get
            Return _UrlTimeout
        End Get
        Set(ByVal value As String)
            _UrlTimeout = value
        End Set
    End Property

    Private _ExpirationMinutes As Integer
    Public Property ExpirationMinutes() As Integer
        Get
            Return _ExpirationMinutes
        End Get
        Set(ByVal value As Integer)
            _ExpirationMinutes = value
        End Set
    End Property

    Private _SessionPay As String
    Public Property SessionPay() As String
        Get
            Return _SessionPay
        End Get
        Set(ByVal value As String)
            _SessionPay = value
        End Set
    End Property

    Private _Orden As String
    Public Property Orden() As String
        Get
            Return _Orden
        End Get
        Set(ByVal value As String)
            _Orden = value
        End Set
    End Property



End Class
