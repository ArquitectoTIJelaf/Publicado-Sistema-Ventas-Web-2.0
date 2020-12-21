<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="pagar.aspx.vb" Inherits="pagar" title="Resultados de Transaccion  -  [NombreEmpresa] - Venta de Pasajes Online" %>
<%@ Register src="WUCViewUser.ascx" tagname="WUCViewUser" tagprefix="uc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/styles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContPWeb" Runat="Server">
    <div class="loginuser right" id="viewuser" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc1:WUCViewUser ID="WUCViewUser1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="section-banner">
        <p class="container centrado tituloBanner"></p>
    </div>     
    <div class="indicador">
        <div class="container col-md12 "id="contIndicador">
	        <ul id="menuIndicador">
             <li><a>Rutas</a></li>
             <li><a>Asientos</a></li>
             <li><a>Pasajeros</a></li>
             <li><a >Confirmación</a></li>
             <li><a class="active">Resultados</a></li>
            </ul> 
        </div>
    </div> 
    <div class="container"> 
    <h2 class="tituloverde">Resultado de transacción: Problema durante la transacción </h2>
      <div class="cl"> </div>
      <div class="contenido info">
            <i class="imgsprite icon-aviso"></i>
            <p><h3>Descripción del Problema:</h3>
                <div id="DivErrsave" class="errormsg"  runat="server" > </div> 
                <input id="ETICKET" type="hidden" name="ETICKET" value="<%=SessionManager.ETicket %>" />    
            </p> 
        </div> 
      <div class="cl"> </div>    
        <div class="contenido info"> 
            <p>Sobre cualquier problema, envienos un mensaje a 
            <a href="mailto:informes@carhuamayocargo.com.pe">informes@carhuamayocargo.com.pe</a> o tambien puede consultar a su banco.
            <%--<a href="javascript:void(0);" class="enlace">
            aqui</a>--%><br />
            Puede reiniciar su compra
            
            </p> 
        </div>   
        <div class="cl"> </div>   
             
         <div class="col-md-12 centrado contBtnR">
            <asp:Button ID="cmdAnular" runat="server" Text="Anular Orden" CssClass="boton" />
            <asp:Button ID="cmdSalirsis" runat="server" Text="Salir"  CssClass="boton" />  
            <asp:Button ID="btnreiniciar" runat="server" Text="Reintentar"  CssClass="boton" />  
            <div class="cl"> <br />  </div>                               
        </div>
          <br />  
          <div class="cl"> </div> 
       
      
       <div id="barnav" runat="server">                     
        </div>      
        <div class="block">       
        
                <div style="width:100%;padding:50px 0px;">
                    <div id="idwprocess" runat="server" style="width:700px;text-align:center;font-family:Arial;background:#fff;line-height:20px;padding:20px 10px 20px 10px;margin:0px auto">
                        <div style="padding:5px;width:100%;background:#b09508">
                            <img src="images/logo-min.png" width="237" height="83" alt="" />
                        </div>                
                        <div style="font-size:14px;color:#f8621c;padding:20px 10px 20px 10px;font-weight:bold;">
                            Por favor no cerrar la pantalla, ni realizar ninguna acción hasta que se le 
                            brinde una respuesta<br />
                            Esto puede ocasionar que transacción se realize de forma incompleta.                         </div>
                       
                    </div>    
                    <div class="contenresultado" id="bannerrespuesta" runat="server" >
                                 
                    </div>                                 
                </div>
 
        
        </div>
               
    </div>
    


    <script type="text/javascript">

var theForm = document.forms['aspnetForm'];
if (!theForm) {
    theForm = document.aspnetForm;
}

function __doPostBackPag(vars) {
    if (vars==""){
        MessageModalInformativo("No esta configurado la pasarela de pago",1);
        return false;
    }
    
    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
        top.cerrarok=false;
        act=theForm.action;
        onsubm=theForm.onsubmit;
        theForm.action= vars;
        theForm.onsubmit="";
        theForm.submit();
        theForm.action=act;
        theForm.onsubmit=onsubm;
    }
}

<%= StrActtion%>

  </script>

</asp:Content>

