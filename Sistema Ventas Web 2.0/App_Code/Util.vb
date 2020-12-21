Imports System.IO
Imports PUtilitario

Public Class Util

    Public Shared JsVar As String = String.Empty

    Shared UNIDADES As String() = {"", "un ", "dos ", "tres ", "cuatro ", _
"cinco ", "seis ", "siete ", "ocho ", "nueve "}
    Shared DECENAS As String() = {"diez ", "once ", "doce ", "trece ", "catorce ", _
    "quince ", "dieciseis ", "diecisiete ", "dieciocho ", "diecinueve", "veinte ", _
    "treinta ", "cuarenta ", "cincuenta ", "sesenta ", "setenta ", "ochenta ", "noventa "}
    Shared CENTENAS As String() = {"", "ciento ", "doscientos ", "trecientos ", _
    "cuatrocientos ", "quinientos ", "seiscientos ", "setecientos ", "ochocientos ", "novecientos "}

    Shared r As Regex


    Sub NumLetra()
    End Sub

    Dim DevIdDoc As String

    Public Shared Function Convertir(ByVal numero As String, ByVal mayusculas As Boolean) As String
        Dim literal As String = ""
        Dim parte_decimal As String = ""
        'si el numero utiliza (.) en lugar de (,) -> se reemplaza
        numero = Replace(numero, ".", ",")
        'si el numero no tiene parte decimal, se le agrega ,00        
        If numero.IndexOf(",") = -1 Then
            numero = numero & ",00"
        End If
        'se valida formato de entrada -> 0,00 y 999 999 999,00
        'if (Pattern.matches("\\d{1,9},\\d{1,2}", numero)) {

        r = New Regex("\d{1,9},\d{1,2}")
        Dim mc As MatchCollection = r.Matches(numero)
        If mc.Count > 0 Then
            'se divide el numero 0000000,00 -> entero y decimal
            Dim Num As String() = numero.Split(",")
            'de da formato al numero decimal
            parte_decimal = Num(1) & "/100 Soles."
            'se convierte el numero a literal            
            If Num(0) = 0 Then
                literal = "cero "
            ElseIf Num(0) > 999999 Then
                literal = getMillones(Num(0))
            ElseIf Num(0) > 999 Then
                literal = getMiles(Num(0))
            ElseIf Num(0) > 99 Then
                literal = getCentenas(Num(0))
            ElseIf Num(0) > 9 Then
                literal = getDecenas(Num(0))
            Else
                literal = getUnidades(Num(0))
            End If
            'devuelve el resultado en mayusculas o minusculas
            If mayusculas Then
                Return (literal & parte_decimal).ToUpper
            Else
                Return literal & parte_decimal
            End If
        Else
            Return ""
        End If

    End Function

    ' funciones para convertir los numeros a literales

    Private Shared Function getUnidades(ByVal numero As String) As String '1 - 9
        'si tuviera algun 0 antes se lo quita -> 09 = 9 o 009=9
        Dim num As String = numero.Substring(numero.Length - 1)
        Return UNIDADES(num)
    End Function

    Private Shared Function getDecenas(ByVal numero As String) As String '99
        If numero < 10 Then 'para casos como -> 01 - 09
            Return getUnidades(numero)
        ElseIf numero > 19 Then 'para 20...99
            Dim u As String = getUnidades(numero)
            If u.Equals("") Then 'para 20,30,40,50,60,70,80,90
                Return DECENAS(numero.Substring(0, 1) + 8)
            Else
                Return DECENAS(numero.Substring(0, 1) + 8) & "y " & u
            End If
        Else
            Return DECENAS(numero - 10)
        End If
    End Function

    Private Shared Function getCentenas(ByVal numero As String) As String
        If numero > 99 Then 'es centena
            If numero = 100 Then 'caso especial
                Return "cien "
            Else
                Return CENTENAS(numero.Substring(0, 1)) & getDecenas(numero.Substring(1))
            End If
        Else 'se quita el 0 antes de convertir a decenas
            Return getDecenas(numero)
        End If
    End Function


    Private Shared Function getMiles(ByVal numero As String) As String
        'obtiene las centenas'
        Dim c As String = numero.Substring(numero.Length - 3)
        'obtiene los miles
        Dim m As String = numero.Substring(0, numero.Length - 3)
        Dim n As String = ""
        'se comprueba que miles tenga valor entero
        If m > 0 Then
            n = getCentenas(m)
            Return n & " mil " & getCentenas(c)
        Else
            Return "" & getCentenas(c)
        End If
    End Function

    Private Shared Function getMillones(ByVal numero As String) As String
        'se obtiene los miles
        Dim miles As String = numero.Substring(numero.Length - 6)
        'millones
        Dim millon As String = numero.Substring(0, numero.Length - 6)
        Dim n As String = ""
        If millon > 9 Then
            n = getCentenas(millon) & " millones "
        Else
            n = getUnidades(millon) & " millon "
        End If
        Return n & getMiles(miles)
    End Function



    Public Shared Function DescripcionDoc(ByVal tipo_doc As String) As String
        Dim documento As String = String.Empty
        If tipo_doc = "1" OrElse tipo_doc = "01" Then
            documento = "D.N.I"
        ElseIf tipo_doc = "4" OrElse tipo_doc = "04" Then
            documento = "Pasaporte"
        ElseIf tipo_doc = "7" OrElse tipo_doc = "07" Then
            documento = "Carnet de Extranjeria"

        End If
        Return documento
    End Function

    Public Shared Function FormatTimeLong(ByVal año As Integer, ByVal mes As Integer, ByVal dia As Integer, ByVal hora As Integer, ByVal minuto As Integer, ByVal segundo As Integer) As DateTime
        ''Dim datetimelong As New DateTime(año, mes, dia, hora, minuto, segundo)
        Return New DateTime(año, mes, dia, hora, minuto, segundo)
    End Function


    Public Shared Function CharRandom(Optional ByVal cant As Integer = 0) As String
        Dim str As String = ""
        Dim y, x, i As Integer
        If cant = 0 Then cant = 1

        For x = 1 To cant
            Do
                Randomize()
                i = Int(64 * Rnd())
                y = i + 65
            Loop While (y > 90 And y < 97) Or (y > 122)
            str = Chr(y) + str
        Next
        Return str
    End Function

    Public Shared Function OpenFiles(ByVal strName As String) As String
        Dim FileItem As New FileInfo(Variables.sPathDir & strName)
        Dim strRet As String = String.Empty
        If FileItem.Exists Then
            Dim r As StreamReader = FileItem.OpenText()
            strRet = r.ReadToEnd
            r.Close()
        End If
        Return strRet
    End Function

    Public Shared Function HeadVars(ByVal p As Page, ByVal var As String) As String
        HeadVars = ""
        Return p.Request.ServerVariables(var)
    End Function

    Public Shared Function IpUser(ByVal p As Page) As String
        IpUser = ""
        Return p.Request.UserHostAddress
    End Function

    Public Shared Function HostUser(ByVal p As Page) As String
        HostUser = ""
        Return p.Request.UserHostName
    End Function

    Public Shared Function PaisUser(ByVal p As Page) As String
        PaisUser = ""
        'Return p.Request.UserAgent
        Dim StrVars As String
        Dim Pais As String
        StrVars = p.Request.ServerVariables("HTTP_ACCEPT_LANGUAGE")
        StrVars = Left(StrVars, 5)

        Select Case StrVars
            Case "es-ar"
                Pais = "Argentina"
            Case "es-bo"
                Pais = "Bolivia"
            Case "es-cl"
                Pais = "Chile"
            Case "es-co"
                Pais = "Colombia"
            Case "es-mx"
                Pais = "Mexico"
            Case "es-py"
                Pais = "Paraguay"
            Case "es-es"
                Pais = "España"
            Case "es-uy"
                Pais = "Uruguay"
            Case "es-ve"
                Pais = "Venezuela"
            Case "es-pe"
                Pais = "Perú"
            Case Else
                Pais = "Otro"
        End Select

        Return Pais
    End Function

    Public Shared Function GetToStr(ByVal p As Page, ByVal strVar As String) As String
        Dim rVal As String = String.Empty
        If strVar.Trim.Length > 0 Then
            rVal = CType(p.Request.QueryString(strVar), String)
        End If
        Return rVal
    End Function

    'Public Shared Sub UrlNavigateEmpty()
    '    SessionManager.UrlNavigate = String.Empty
    'End Sub

    Public Shared Sub JsLineAdd(ByVal Line As String)
        JsVar = JsVar & Line & vbCrLf
    End Sub

    Public Shared Sub JsClearVar()
        JsVar = ""
    End Sub

    Public Shared Function JsGetVar() As String
        Return JsVar
    End Function

End Class
