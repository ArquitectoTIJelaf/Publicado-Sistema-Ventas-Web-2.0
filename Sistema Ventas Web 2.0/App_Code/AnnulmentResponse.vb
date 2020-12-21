Imports Microsoft.VisualBasic

Public Class AnnulmentResponse
    Private _dataMap As DataTransaccionVisaNet
    Public Property dataMap() As DataTransaccionVisaNet
        Get
            Return _dataMap
        End Get
        Set(ByVal value As DataTransaccionVisaNet)
            _dataMap = value
        End Set
    End Property
End Class
