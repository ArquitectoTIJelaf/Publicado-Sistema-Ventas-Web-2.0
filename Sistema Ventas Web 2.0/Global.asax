<%@ Application Language="VB" %>
<%@ Import Namespace="PUtilitario" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta al iniciarse la aplicación

    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta durante el cierre de aplicaciones
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta al producirse un error no controlado
        'Session("CurrentError") = "Global: " & Server.GetLastError.Message
        'Server.Transfer("AppErrors.aspx")
    End Sub
    
    Public Sub Application_OnStart()
        Application("Err") = 0
    End Sub
    

    Public Sub Session_OnStart()
        
        Try
            Application("Err") = 0
            Application.Lock()
            
            
            Functions.EliminarAsientosCaducados()
            Application.UnLock()
            Variables.sPathDir = Server.MapPath("~/")
            SessionUser.GlobalInitSession(Session)
        Catch ex As Exception
            Application("Err") = 1
        End Try
    End Sub

    Public Sub Session_OnEnd()
        
        Try
            SessionUser.FinalizeSession(Session)
            Application.Lock()
            Application.UnLock()
        Catch ex As Exception
            Throw
        End Try

    End Sub

       
</script>