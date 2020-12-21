<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false"
    CodeFile="respuesta.aspx.vb" Inherits="respuesta" Title="Resultado de Transaccion   -  [NombreEmpresa] - Venta de Pasajes Online" %>

<%@ Register Src="WUCViewUser.ascx" TagName="WUCViewUser" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContPWeb" runat="Server">
    <div class="loginuser right" id="viewuser" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc1:WUCViewUser ID="WUCViewUser1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="section-banner">
        <p class="container centrado tituloBanner">
            </p>
    </div>
    <div class="indicador">
        <div class="container col-md12 " id="contIndicador">
            <ul id="menuIndicador">
                <li><a>Rutas</a></li>
                <li><a>Asientos</a></li>
                <li><a>Pasajeros</a></li>
                <li><a>Confirmación</a></li>
                <li><a class="active">Resultados</a></li>
            </ul>
        </div>
    </div>
    <div class="container" id="ResultadoTransaccion">
        <h4 class="title-page">
            <span class="imgsprite"></span>
            <label>
                Resultado Transacción</label>
        </h4>
        <h2 class="tituloverde">
            Resultado de transacción: Problema durante la transacción
        </h2>
        <h3 class="tituloverde">
            Problema durante la transacción
        </h3>
        
        <div class="cl">
        </div>
        <div class="contenido info">
            <i class="imgsprite icon-aviso"></i>
            <p>
                <h3>
                    Descripción del Problema:</h3>
                <div id="DivErrsave" class="errormsg" runat="server">
                </div>
            </p>
        </div>
        <div class="cl">
        </div>
        <div class="contenido info info-adicional">
            <p>
                Sobre cualquier problema, envienos un mensaje o tambien puede consultar a su banco.
                <br />
                Puede reiniciar su compra
            </p>
        </div>
        <div class="cl">
        </div>
        <div class="col-md-12 centrado contBtnR">
            <asp:Button ID="cmdSalirsis" runat="server" Text="Salir" CssClass="boton" />
            <div class="cl">
                <br />
            </div>
        </div>
        <br />
        <div class="cl">
        </div>
    </div>
</asp:Content>
