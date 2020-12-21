Imports System.Web
Imports System.Web.SessionState
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Public Class ScriptUser
    Public Shared Sub JsRegister(ByVal WUIPage As Object, ByVal IdJs As String, ByVal txtJs As String)
        ScriptManager.RegisterStartupScript(WUIPage, WUIPage.GetType(), IdJs, txtJs, True)
    End Sub
    Public Shared Sub JsAjaxRegister(ByVal WUIPage As Object)
        Dim strMessageErr As String
        Dim JsText As String = String.Empty
        ''Dim vls As Boolean = Functions.UserValid(WUIPage)
        strMessageErr = Variables.MsgError
        strMessageErr = Replace(strMessageErr, Chr(10), "\n")
        strMessageErr = Replace(strMessageErr, Chr(13), "\n")
        JsText = vbCrLf & Util.JsGetVar() & _
        " shoping=1;" & vbCrLf
        ''" shoping=" & SessionManager.IsShoViable & ";" & vbCrLf

        JsRegister(WUIPage, "mainJs", JsText)
        Variables.MsgError = ""
        Util.JsClearVar()
    End Sub
    Public Shared Sub JQueryMensaje(ByVal WUIPage As Object, ByVal mensaje As String, Optional ByVal Type As Integer = 2)
        ScriptManager.RegisterStartupScript(WUIPage, WUIPage.GetType, Guid.NewGuid().ToString(), "MessageModalInformativo('" & mensaje & "'," & Type & ");", True)

    End Sub
 
    Public Shared Sub JQueryEjecutarFuncion(ByVal WUIPage As Object, ByVal nomFuncion As String)
        ScriptManager.RegisterStartupScript(WUIPage, WUIPage.GetType, Guid.NewGuid().ToString(), nomFuncion & "();", True)
    End Sub

End Class
