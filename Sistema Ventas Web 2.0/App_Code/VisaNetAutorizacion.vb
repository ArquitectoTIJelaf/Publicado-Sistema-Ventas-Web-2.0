Public Class VisaNetAutorizacion

    Private _errorCode As Integer
    Public Property errorCode() As Integer
        Get
            Return _errorCode
        End Get
        Set(ByVal value As Integer)
            _errorCode = value
        End Set
    End Property


    Private _errorMessage As String
    Public Property errorMessage() As String
        Get
            Return _errorMessage
        End Get
        Set(ByVal value As String)
            _errorMessage = value
        End Set
    End Property

    Private _transactionUUID As String
    Public Property transactionUUID() As String
        Get
            Return _transactionUUID
        End Get
        Set(ByVal value As String)
            _transactionUUID = value
        End Set
    End Property


    Private _externalTransactionId As String
    Public Property externalTransactionId() As String
        Get
            Return _externalTransactionId
        End Get
        Set(ByVal value As String)
            _externalTransactionId = value
        End Set
    End Property


    Private _transactionDateTime As String
    Public Property transactionDateTime() As String
        Get
            Return _transactionDateTime
        End Get
        Set(ByVal value As String)
            _transactionDateTime = value
        End Set
    End Property


    Private _transactionDuration As Integer
    Public Property transactionDuration() As Integer
        Get
            Return _transactionDuration
        End Get
        Set(ByVal value As Integer)
            _transactionDuration = value
        End Set
    End Property


    Private _merchantId As String
    Public Property merchantId() As String
        Get
            Return _merchantId
        End Get
        Set(ByVal value As String)
            _merchantId = value
        End Set
    End Property

    Private _dataMap As DataTransaccionVisaNet
    Public Property dataMap() As DataTransaccionVisaNet
        Get
            Return _dataMap
        End Get
        Set(ByVal value As DataTransaccionVisaNet)
            _dataMap = value
        End Set
    End Property

    Private _data As DataTransaccionVisaNet
    Public Property data() As DataTransaccionVisaNet
        Get
            Return _data
        End Get
        Set(ByVal value As DataTransaccionVisaNet)
            _data = value
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




End Class
