Public Class DataTransaccionVisaNet


    Private _AUTHORIZATION_CODE As String
    Public Property AUTHORIZATION_CODE() As String
        Get
            Return _AUTHORIZATION_CODE
        End Get
        Set(ByVal value As String)
            _AUTHORIZATION_CODE = value
        End Set
    End Property


    Private _MERCHANT As String
    Public Property MERCHANT() As String
        Get
            Return _MERCHANT
        End Get
        Set(ByVal value As String)
            _MERCHANT = value
        End Set
    End Property


    Private _NUMORDEN As String
    Public Property NUMORDEN() As String
        Get
            Return _NUMORDEN
        End Get
        Set(ByVal value As String)
            _NUMORDEN = value
        End Set
    End Property


    Private _ACTION_CODE As String
    Public Property ACTION_CODE() As String
        Get
            Return _ACTION_CODE
        End Get
        Set(ByVal value As String)
            _ACTION_CODE = value
        End Set
    End Property


    Private _AMOUNT As String
    Public Property AMOUNT() As String
        Get
            Return _AMOUNT
        End Get
        Set(ByVal value As String)
            _AMOUNT = value
        End Set
    End Property

    Private _TRANSACTION_DATE As String
    Public Property TRANSACTION_DATE() As String
        Get
            Return _TRANSACTION_DATE
        End Get
        Set(ByVal value As String)
            _TRANSACTION_DATE = value
        End Set
    End Property


    Private _CARD As String
    Public Property CARD() As String
        Get
            Return _CARD
        End Get
        Set(ByVal value As String)
            _CARD = value
        End Set
    End Property


    Private _ACTION_DESCRIPTION As String
    Public Property ACTION_DESCRIPTION() As String
        Get
            Return _ACTION_DESCRIPTION
        End Get
        Set(ByVal value As String)
            _ACTION_DESCRIPTION = value
        End Set
    End Property

    Private _BRAND As String
    Public Property BRAND() As String
        Get
            Return _BRAND
        End Get
        Set(ByVal value As String)
            _BRAND = value
        End Set
    End Property

    Private _TRANSACTION_ID As String
    Public Property TRANSACTION_ID() As String
        Get
            Return _TRANSACTION_ID
        End Get
        Set(ByVal value As String)
            _TRANSACTION_ID = value
        End Set
    End Property


    Private _STATUS As String
    Public Property STATUS() As String
        Get
            Return _STATUS
        End Get
        Set(ByVal value As String)
            _STATUS = value
        End Set
    End Property

End Class
