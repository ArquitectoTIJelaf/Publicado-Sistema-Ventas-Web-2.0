Public Class Sponsored
    Private _merchantId As String
    Public Property merchantId() As String
        Get
            Return _merchantId
        End Get
        Set(ByVal value As String)
            _merchantId = value
        End Set
    End Property
    Private _mcci As String
    Public Property mcci() As String
        Get
            Return _mcci
        End Get
        Set(ByVal value As String)
            _mcci = value
        End Set
    End Property

    Private _name As String
    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _address As String
    Public Property address() As String
        Get
            Return _address
        End Get
        Set(ByVal value As String)
            _address = value
        End Set
    End Property

    Private _phoneNumber As String
    Public Property phoneNumber() As String
        Get
            Return _phoneNumber
        End Get
        Set(ByVal value As String)
            _phoneNumber = value
        End Set
    End Property


End Class
