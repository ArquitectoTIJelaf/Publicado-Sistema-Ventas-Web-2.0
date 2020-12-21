Public Class Antifraud
    Private _clientIp As String
    Public Property clientIp() As String
        Get
            Return _clientIp
        End Get
        Set(ByVal value As String)
            _clientIp = value
        End Set
    End Property

    Private _merchantDefineData As MerchantDefineData
    Public Property merchantDefineData() As MerchantDefineData
        Get
            Return _merchantDefineData
        End Get
        Set(ByVal value As MerchantDefineData)
            _merchantDefineData = value
        End Set
    End Property
End Class
