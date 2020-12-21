Public Class Order

    Private _tokenId As String
    Public Property tokenId() As String
        Get
            Return _tokenId
        End Get
        Set(ByVal value As String)
            _tokenId = value
        End Set
    End Property


    Private _purchaseNumber As String
    Public Property purchaseNumber() As String
        Get
            Return _purchaseNumber
        End Get
        Set(ByVal value As String)
            _purchaseNumber = value
        End Set
    End Property

    Private _amount As Double
    Public Property amount() As Double
        Get
            Return _amount
        End Get
        Set(ByVal value As Double)
            _amount = value
        End Set
    End Property

    Private _currency As String
    Public Property currency() As String
        Get
            Return _currency
        End Get
        Set(ByVal value As String)
            _currency = value
        End Set
    End Property

    Private _transactionId As String
    Public Property transactionId() As String
        Get
            Return _transactionId
        End Get
        Set(ByVal value As String)
            _transactionId = value
        End Set
    End Property

    Private _transactionDate As String
    Public Property transactionDate() As String
        Get
            Return _transactionDate
        End Get
        Set(ByVal value As String)
            _transactionDate = value
        End Set
    End Property

End Class
