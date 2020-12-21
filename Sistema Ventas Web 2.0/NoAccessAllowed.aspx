<%@ Page Language="VB" MasterPageFile="~/Principal.master" Title="Acceso denegado  -  [NombreEmpresa] - Venta de Pasajes Online" %>

<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Functions.NombreTituloPagina(Me.Title)
    End Sub
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContPWeb" Runat="Server">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="center">&nbsp;</td>
  </tr>
</table>
<table width="386" height="267" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="20"></td>
  </tr>
  <tr>
    <td height="130"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td align="center"><table  width="321" height="193" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td width="321" background="images/paginas/winerr.png"><table width="236" height="57" border="0" align="center" cellpadding="0" cellspacing="12">
              <tr>
                <td align="center"><div id="DivErrsave"  class="errorRpts" runat="server"><strong>
                    Página no autorizada!!</strong></div></td>
              </tr>
              
            </table>
              <table width="74%" height="20" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                  <td><em>Por favor seleccione alguna los enlaces del menu...</em></td>
                </tr>
              </table>
              </td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
</table>
</asp:Content>

