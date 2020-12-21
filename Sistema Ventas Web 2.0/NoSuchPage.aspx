<%@ Page Language="VB" MasterPageFile="~/Principal.master" Title="Página No Encontrada  -  [NombreEmpresa] - Venta de Pasajes Online" %>

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
        <td align="center"><table  width="317" height="193" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td background="images/paginas/winerr.png"><table width="236" height="57" border="0" align="center" cellpadding="0" cellspacing="12">
                <tr>
                  <td align="center"><div id="DivErrsave"  class="errorRpts" runat="server"><strong>No 
                      se ha encontrado la página solicitada...</strong></div></td>
                </tr>
                
            </table>
              <table width="75%" height="20" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                  <td><em>Ingrese a alguno de los enlaces del menu...!</em></td>
                </tr>
              </table></td>
          </tr>
        </table>        </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
</table>

</asp:Content>

