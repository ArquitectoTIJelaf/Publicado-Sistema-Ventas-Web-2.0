<%@ Page Language="VB" MasterPageFile="~/Principal.master" Title="Pagina Error -  [NombreEmpresa] - Pasajes en linea" %>

<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Functions.NombreTituloPagina(Me.Title)
    End Sub
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContPWeb" Runat="Server">
    <div class="content">
    <div class="block">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
        </table>
        <table width="386" height="267" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td height="51">&nbsp;</td>
          </tr>
          <tr>
            <td height="130"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td align="center"><table  width="317" height="193" border="0" cellpadding="0" cellspacing="0">
                  <tr>
                    <td background="images/paginas/winerr.png"><table width="236" height="57" border="0" align="center" cellpadding="0" cellspacing="12">
                      <tr>
                        <td align="center"><div id="DivErrsave"  class="errorRpts" runat="server"><strong>Se ha producido un error en la página</strong>...</div></td>
                      </tr>
                    </table>
                      <table width="75%" height="20" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td><em>Intente de nuevo desde la última página...</em></td>
                        </tr>
                      </table></td>
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
          <tr>
            <td>&nbsp;</td>
          </tr>
        </table>    
    </div>
</div>

</asp:Content>

