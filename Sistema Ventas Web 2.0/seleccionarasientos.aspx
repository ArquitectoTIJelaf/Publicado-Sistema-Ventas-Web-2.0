<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false"
    CodeFile="seleccionarasientos.aspx.vb" Inherits="seleccionarasientos" Title="Seleccionar mis asientos  -  [NombreEmpresa] - Venta de Pasajes Online" %>

<%@ Register Src="WUCViewUser.ascx" TagName="WUCViewUser" TagPrefix="uc1" %>
<%@ Register Src="WUCBanner.ascx" TagName="WUCBanner" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" src="js/asientos.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/functions.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContPWeb" runat="Server">

    <script language="javascript" type="text/javascript">


        AryData = new Array({}, {}, {});
        AryData[0][0] = 'Asiento disponible para comprar';
        AryData[0][1] = 'asiento-disponible';
        AryData[1][0] = 'Asiento seleccionado para comprar';
        AryData[1][1] = 'asiento-seleccionado';

        AryDataRetorno = new Array({}, {}, {});
        AryDataRetorno[0][0] = 'Asiento disponible para comprar';
        AryDataRetorno[0][1] = 'asiento-disponible';
        AryDataRetorno[1][0] = 'Asiento seleccionado para comprar';
        AryDataRetorno[1][1] = 'asiento-seleccionado';


        function pbtnOnClickIda(e, obj, opc) {
            if (opc == 2) {

                DeleteSubmitIda(obj);
            }
            else { SelectAndSubmitIda(obj); }
        }

        function pbtnOnClickRetorno(e, obj, opc) {

            if (opc == 2)

                DeleteSubmitRetorno(obj);

            else { SelectAndSubmitRetorno(obj); }
        }

        function SelectAndSubmitIda(obj) {
            try {
                //var selName="ctl00_ContPWeb_Dlstn";
                var btnName = "ctl00_ContPWeb_btncostoida"
                //selName=selName+obj.id.substr(3,1);
                btnName = btnName + obj.id.substr(4, 1);
                //objSel=EventUtil.getObj(selName);
                objBtn = EventUtil.getObj(btnName)
                var Btnvalue = obj.id.substr(4, 1);

                if (objBtn.disabled == false) {
                    if (Btnvalue.substr(0, 1) == "0") { Btnvalue = Btnvalue.substr(1, 1); }
                    //objSel.value=Btnvalue
                    //if (objSel.value!=""){

                    objBtn = EventUtil.getObj(btnName)
                    objhiddenida = EventUtil.getObj("ctl00_ContPWeb_nasientoida")
                    objhiddenida.value = obj.id.substr(5, 2);
                    objBtn.click();
                    //}else{objSel.selectedIndex=0;}
                } else { /*alert("Nivel no habilitado")*/; }
            } catch (err) { MessageModalInformativo(err, 0); }
        }
        function SelectAndSubmitRetorno(obj) {
            try {
                //var selName="ctl00_ContPWeb_Dlstn";
                var btnName = "ctl00_ContPWeb_btncostoretorno"
                //selName=selName+obj.id.substr(3,1);
                btnName = btnName + obj.id.substr(4, 1);
                objBtn = EventUtil.getObj(btnName)
                //objSel=EventUtil.getObj(selName);
                var Btnvalue = obj.id.substr(4, 1);
                if (objBtn.disabled == false) {
                    if (Btnvalue.substr(0, 1) == "0") { Btnvalue = Btnvalue.substr(1, 1); }
                    //objSel.value=Btnvalue
                    //if (objSel.value!=""){
                    objBtn = EventUtil.getObj(btnName)
                    objhiddenretorno = EventUtil.getObj("ctl00_ContPWeb_nasientoretorno")
                    objhiddenretorno.value = obj.id.substr(5, 2);
                    objBtn.click();
                    //}else{objSel.selectedIndex=0;}
                } else { /*alert("Nivel no habilitado")*/; }
            } catch (err) { MessageModalInformativo(err, 0); }

        }

        function DeleteSubmitIda(obj) {

            try {
                var btnName = "ctl00_ContPWeb_btndeleteida"
                objBtn = EventUtil.getObj(btnName)
                if (objBtn.disabled == false) {
                    objBtn = EventUtil.getObj(btnName)
                    objhiddenida = EventUtil.getObj("ctl00_ContPWeb_nasientoida")
                    objhiddenida.value = obj.id.substr(5, 2);
                    objBtn.click();
                }
            } catch (err) { MessageModalInformativo(err, 0); }
        }

        function DeleteSubmitRetorno(obj) {

            try {
                var btnName = "ctl00_ContPWeb_btndeleteretorno"
                objBtn = EventUtil.getObj(btnName)
                if (objBtn.disabled == false) {
                    objBtn = EventUtil.getObj(btnName)
                    objhiddenida = EventUtil.getObj("ctl00_ContPWeb_nasientoretorno")
                    objhiddenida.value = obj.id.substr(5, 2);
                    objBtn.click();
                }
            } catch (err) { MessageModalInformativo(err, 0); }
        }


        function PorcesOptionsIda(Ary, opc) {
            for (i = 0; i < Ary.length; i++) {

                var strNameBtn = "btn" + Ary[i];
                //alert(strNameBtn)
                var oBtns = EventUtil.getObj(strNameBtn);
                if (oBtns != null && oBtns != "undefine" && oBtns != "undefined" && oBtns != "null") {
                    //alert(AryData[opc][1])
                    oBtns.disabled = false;
                    oBtns.className = AryData[opc][1]
                    EventUtil.addEventHandler(oBtns, "click", EventUtil.setfunctionArgs(pbtnOnClickIda, oBtns, [oBtns, opc + 1]));
                }
            }
        }
        function PorcesOptionsRetorno(Ary, opc) {
            for (i = 0; i < Ary.length; i++) {
                var strNameBtn = "btn" + Ary[i];
                //alert(strNameBtn)
                var oBtns = EventUtil.getObj(strNameBtn);
                if (oBtns != null && oBtns != "undefine" && oBtns != "undefined" && oBtns != "null") {
                    oBtns.title = AryDataRetorno[opc][0]
                    oBtns.disabled = false;
                    oBtns.className = AryDataRetorno[opc][1]
                    EventUtil.addEventHandler(oBtns, "click", EventUtil.setfunctionArgs(pbtnOnClickRetorno, oBtns, [oBtns, opc + 1]));
                }
            }
        }

        function CambiarTabPlano() {
            var index = $("#<%= FlagTab.ClientID%>").val();
            $(".nav-tabs li").eq(index).find("a").trigger("click");
        }


        function HiddenElementos() {

            $("#<%=btncostoida1.ClientID %>").hide()
            $("#<%=btncostoida2.ClientID %>").hide()
            $("#<%=btncostoretorno1.ClientID %>").hide()
            $("#<%=btncostoretorno2.ClientID %>").hide()
            $("#<%=btndeleteida.ClientID %>").hide();
            $("#<%=btndeleteretorno.ClientID %>").hide();

            $('#<%=btnrefreshuser.ClientId %>').hide()
        }
    </script>

    <div class="loginuser right" id="viewuser" runat="server">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc1:WUCViewUser ID="WUCViewUser1" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnrefreshuser" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="section-banner">

    </div>
    <div class="indicador">
        <div class="container col-md12 " id="contIndicador">
            <ul id="menuIndicador">
                <li><a href="#">Rutas</a></li>
                <li><a href="#" class="active">Asientos</a></li>
                <li><a href="#">Pasajeros</a></li>
                <li><a href="#">Confirmación</a></li>
                <li><a href="#">Resultados</a></li>
            </ul>
        </div>
    </div>
    <div class="container" id="bus" name="bus">
        <h4 class="title-page">
            <span class="imgsprite"></span>
            <label>
                Seleccione los asientos</label>
        </h4>
        <div class="contenido info">
            <i class="imgsprite icon-preg"></i>
            <p>
                Seleccionar asiento(s) y Continuar Los asientos de la primera fila solo serán ocupados
                por pasajeros adultos. Los niños no podrán viajar en estos asientos por seguridad.
                Por favor seleccionar otro.</p>
        </div>
        <asp:UpdatePanel ID="UpdatePanelWeb" runat="server">
            <ContentTemplate>
                <!-- td visible="false">
                <span class="valor" >Valor referencial</span>
                <div class="head-datos-asientos">
                    <asp:Label ID="lblmontoextida" runat="server" Text=""></asp:Label></div>
            </td>
            <%--<td>
                <div class="head-datos-asientos-large">Convertidor Moneda</div>
            </td>                                           --%>
             <td visible="false">
                <div class="item-detalles-right">                 
                    <asp:Label ID="lblmontoextrida" runat="server" Text=""></asp:Label>                                                          
                </div>                                         
            </td>
            <td visible="false">
                <div class="item-detalles-complete">

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                                                                         
                                  <asp:DropDownList ID="ddlmonedasida" runat="server" DataTextField="Descripcion" 
                                    DataValueField="Cambio" Width="100px" AutoPostBack="True"> </asp:DropDownList>                                                             
                            </ContentTemplate>
                        </asp:UpdatePanel>                                                                     
                </div>                                            
            </td> -->
                <!-- td visible="false">
                <span class="valor" >Valor referencial</span>
                <div class="head-datos-asientos">
                    <asp:Label ID="lblmontoextretorno" runat="server" Text=""></asp:Label></div>
            </td>
            <%--<td>
                <div class="head-datos-asientos-large">Convertidor Moneda</div>
            </td>  --%>                                         
         
       
            
            <td visible="false">
                <div class="item-detalles-right">           
                    <asp:Label ID="lblmontoextrretorno" runat="server" Text=""></asp:Label>          
                </div>                                         
            </td>
            <td visible="false">
                <div class="item-detalles-complete"> 
                     <asp:DropDownList ID="ddlmonedasretorno" runat="server" Width="100px" 
                            DataTextField="Descripcion" DataValueField="Cambio" AutoPostBack="True">
                        </asp:DropDownList>                   
                </div>                                            
            </td>
            -->
                <asp:HiddenField ID="FlagTab" runat="server" Value="0" />
                <div class="cont-planos">
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab"
                            data-toggle="tab" class="tab-desktop">
                            <div class="circulo">
                                1</div>
                            &nbsp;&nbsp;Seleccione asientos de ida </a><a href="#home" aria-controls="home" role="tab"
                                data-toggle="tab" class="tab-mobile">
                                <asp:Label ID="lblrutaidamobile" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:Label ID="lblfechaviajeidamobile" runat="server" Text=""></asp:Label>
                            </a></li>
                        <li id="lbldatosretorno" runat="server" role="presentation"><a href="#profile" aria-controls="profile"
                            role="tab" data-toggle="tab" aria-expanded="false" class="tab-desktop">
                            <div class="circulo">
                                2</div>
                            &nbsp;&nbsp;Seleccione asientos de retorno </a><a href="#profile" aria-controls="profile"
                                role="tab" data-toggle="tab" aria-expanded="false" class="tab-mobile">
                                <asp:Label ID="lblrutaretornomobile" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:Label ID="lblfechaviajeretornomobile" runat="server" Text=""></asp:Label>
                            </a></li>
                    </ul>
                    <div class="tab-content seleccion1">
                        <div role="tabpanel" class="tab-pane active" id="home">
                            <!--div class="centrado" style="width: 500px; margin: auto;">
                            <div class="form-group">
                                <div class="imgsprite img-busida">
                                </div>
                                Itinerario de Ida: <span class="tituloverde">
                                    <asp:Label ID="lblrutaidaEti" runat="server" Text=""></asp:Label></span>
                            </div>
                        </div-->
                            <table cellspacing="0" class="tblVerde" border="1">
                                <tbody>
                                    <tr class="title-fileds">
                                        <th class="item-desktop">
                                            Ruta
                                        </th>
                                        <th>
                                            Servicio
                                        </th>
                                        <th class="item-desktop">
                                            Fecha Viaje
                                        </th>
                                        <th>
                                            Hora Salida
                                        </th>
                                        <th class="item-tablet">
                                            Hora Llegada
                                        </th>
                                        <th scope="col">
                                            Monto (S/.)
                                        </th>
                                    </tr>
                                    <tr class="list-itinerario">
                                        <td class="item-desktop">
                                            <asp:Label ID="lblrutaida" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblservicioida" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="item-desktop">
                                            <asp:Label ID="lblfechaviajeida" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblhorasalidaida" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="item-tablet">
                                            <asp:Label ID="lblhorallegadaida" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblmontosolesida" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="col-md-4 col-sm-4 col-xs-4 form-group centrado leyenda-desktop">
                                <ul class="listIcon">
                                    <li>Asiento
                                        <br>
                                        Disponible</li>
                                    <li>
                                        <div class="imgsbus img-asientod">
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-4 form-group centrado leyenda-desktop">
                                <ul class="listIcon">
                                    <li>Asiento
                                        <br>
                                        Seleccionado</li>
                                    <li>
                                        <div class="imgsbus img-asientos">
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-4 form-group centrado leyenda-desktop">
                                <ul class="listIcon">
                                    <li>Asiento
                                        <br>
                                        No Disponible</li>
                                    <li>
                                        <div class="imgsbus img-asienton">
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="cl">
                            </div>
                            <div class="col-md-12 centrado plano-mobile">
                                <asp:Label ID="lbltitlecroquisida" runat="server" Text=""></asp:Label>
                                <div id="croquisida" class="croquis" runat="server">
                                    <div id="DivPlanoBusIda" runat="server" class="contenido-plano">
                                    </div>
                                    <div class="leyenda-mobile">
                                        <div class="form-group centrado item-leyenda">
                                            <ul class="listIcon">
                                                <li>Asiento
                                                    <br>
                                                    Disponible</li>
                                                <li>
                                                    <div class="imgsbus img-asientod">
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="form-group centrado item-leyenda">
                                            <ul class="listIcon">
                                                <li>Asiento
                                                    <br>
                                                    Seleccionado</li>
                                                <li>
                                                    <div class="imgsbus img-asientos">
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="form-group centrado item-leyenda">
                                            <ul class="listIcon">
                                                <li>Asiento
                                                    <br>
                                                    No Disponible</li>
                                                <li>
                                                    <div class="imgsbus img-asienton">
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="cl">
                                    </div>
                                    <div id="DivMessageIda" runat="server" class="contenido aviso">
                                        <i class="imgsprite icon-aviso"></i>Por política de la empresa, solo se pueden comprar
                                        como máximo 4 asientos a la vez por transacción de un viaje. Si desea más de esa
                                        cantidad permitida, inicie otra transaccion adicional o las que requiera, según
                                        la cantidad de asientos que desee comprar.
                                    </div>
                                    <div class="leyenda-tablet">
                                        <div class="col-md-4 col-sm-4 col-xs-4 form-group centrado">
                                            <ul class="listIcon">
                                                <li>Asiento
                                                    <br>
                                                    Disponible</li>
                                                <li>
                                                    <div class="imgsbus img-asientod">
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-md-4 col-sm-4 col-xs-4 form-group centrado">
                                            <ul class="listIcon">
                                                <li>Asiento
                                                    <br>
                                                    Seleccionado</li>
                                                <li>
                                                    <div class="imgsbus img-asientos">
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-md-4 col-sm-4 col-xs-4 form-group centrado">
                                            <ul class="listIcon">
                                                <li>Asiento
                                                    <br>
                                                    No Disponible</li>
                                                <li>
                                                    <div class="imgsbus img-asienton">
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="cl">
                                    </div>
                                    <div class="col-md-12 detalle-compra-desktop">
                                        <div class="col-md-6 centrado">
                                            <table class="tblVerde" style="width: 65%">
                                                <tr class="title-fileds" style="background: #e1e1e1;">
                                                    <th colspan="2">
                                                        Nivel del Bus
                                                    </th>
                                                    <th colspan="2">
                                                        Precio por Nivel
                                                    </th>
                                                </tr>
                                                <tr id="firstlevelida" runat="server">
                                                    <td class="literal">
                                                        1º Piso
                                                    </td>
                                                    <td>
                                                        <div class="imgsbus img-piso1">
                                                        </div>
                                                    </td>
                                                    <td class="literal">
                                                        <asp:Label ID="lblpreciosimbolonivel1" runat="server" Text="S/."></asp:Label>
                                                    </td>
                                                    <td class="literal" align="right">
                                                        <asp:Label ID="lblprecionivel1ida" runat="server" Text="0.00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="secondlevelida" runat="server">
                                                    <td class="literal">
                                                        2º Piso
                                                    </td>
                                                    <td>
                                                        <div class="imgsbus img-piso2">
                                                        </div>
                                                    </td>
                                                    <td class="literal">
                                                        <asp:Label ID="lblpreciosimbolonivel2" runat="server" Text="S/."></asp:Label>
                                                    </td>
                                                    <td class="literal" align="right">
                                                        <asp:Label ID="lblprecionivel2ida" runat="server" Text="0.00"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-md-6 centrado">
                                            <table class="tblVerde" style="width: 65%">
                                                <tr class="title-fileds" style="background: #e1e1e1;">
                                                    <th>
                                                        Acción
                                                    </th>
                                                    <th>
                                                        Asiento
                                                    </th>
                                                    <th>
                                                        Nivel
                                                    </th>
                                                    <th>
                                                        Precio
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="gvasientoseleccionadosida" runat="server" AutoGenerateColumns="False"
                                                                    ShowHeader="False" AllowPaging="True" BorderStyle="None" DataKeyNames="IDS" GridLines="None"
                                                                    PageSize="4">
                                                                    <Columns>
                                                                        <asp:TemplateField ShowHeader="False">
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="Button1" runat="server" CssClass="item-asiento-seleccion btnQuitar"
                                                                                    CausesValidation="False" CommandName="Delete" Text="" Width="75px" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="Nume_Asiento">
                                                                            <ItemStyle CssClass="item-asiento-seleccion" Width="84px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Nivel">
                                                                            <ItemStyle CssClass="item-asiento-seleccion" Width="64px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Costo" DataFormatString="{0:F2}">
                                                                            <ItemStyle CssClass="item-asiento-seleccion" Width="72px" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" CssClass="gridfooter" />
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btncostoida1" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="btncostoida2" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="cl">
                                </div>
                            </div>
                            <div class="cl">
                            </div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="profile">
                            <!-- div class="centrado" style="width: 500px; margin: auto;">
                            <div class="form-group">
                                <div class="imgsprite img-busretorno" style="">
                                </div>
                                Itinerario de Retorno: <span class="tituloverde">
                                    <asp:Label ID="lblrutaretornoEti" runat="server" Text=""></asp:Label>
                                </span>
                            </div>
                        </div -->
                            <table cellspacing="0" class="tblVerde" border="1">
                                <tbody>
                                    <tr class="title-fileds">
                                        <th class="item-desktop">
                                            Ruta
                                        </th>
                                        <th>
                                            Servicio
                                        </th>
                                        <th class="item-desktop">
                                            Fecha Viaje
                                        </th>
                                        <th>
                                            Hora Salida
                                        </th>
                                        <th class="item-tablet">
                                            Hora Llegada
                                        </th>
                                        <th scope="col">
                                            Monto (S/.)
                                        </th>
                                    </tr>
                                    <tr class="list-itinerario">
                                        <td class="item-desktop">
                                            <asp:Label ID="lblrutaretorno" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblservicioretorno" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="item-desktop">
                                            <asp:Label ID="lblfechaviajeretorno" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblhorasalidaretorno" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="item-tablet">
                                            <asp:Label ID="lblhorallegadaretorno" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblmontosolesretorno" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="col-md-4 col-sm-4 form-group centrado leyenda-desktop">
                                <ul class="listIcon">
                                    <li>Asiento
                                        <br>
                                        Disponible</li>
                                    <li>
                                        <div class="imgsbus img-asientod">
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-md-4 col-sm-4 form-group centrado leyenda-desktop">
                                <ul class="listIcon">
                                    <li>Asiento
                                        <br>
                                        Seleccionado</li>
                                    <li>
                                        <div class="imgsbus img-asientos">
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-md-4 col-sm-4 form-group centrado leyenda-desktop">
                                <ul class="listIcon">
                                    <li>Asiento
                                        <br>
                                        No Disponible</li>
                                    <li>
                                        <div class="imgsbus img-asienton">
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="cl">
                            </div>
                            <div class="col-md-12 centrado plano-mobile">
                                <asp:Label ID="lbltitlecroquisretorno" runat="server" Text=""></asp:Label>
                                <div id="croquisretorno" class="croquis" runat="server">
                                    <div id="DivPlanoBusRetorno" runat="server" class="contenido-plano">
                                    </div>
                                    <div class="leyenda-mobile">
                                        <div class="form-group centrado item-leyenda">
                                            <ul class="listIcon">
                                                <li>Asiento
                                                    <br>
                                                    Disponible</li>
                                                <li>
                                                    <div class="imgsbus img-asientod">
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="form-group centrado item-leyenda">
                                            <ul class="listIcon">
                                                <li>Asiento
                                                    <br>
                                                    Seleccionado</li>
                                                <li>
                                                    <div class="imgsbus img-asientos">
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="form-group centrado item-leyenda">
                                            <ul class="listIcon">
                                                <li>Asiento
                                                    <br>
                                                    No Disponible</li>
                                                <li>
                                                    <div class="imgsbus img-asienton">
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="cl">
                                    </div>
                                    <div id="DivMessageRetorno" runat="server" class="contenido aviso">
                                        <i class="imgsprite icon-aviso"></i>Por política de la empresa, solo se pueden comprar
                                        como máximo 4 asientos a la vez por transacción de un viaje. Si desea más de esa
                                        cantidad permitida, inicie otra transaccion adicional o las que requiera, según
                                        la cantidad de asientos que desee comprar.
                                    </div>
                                    <div class="leyenda-tablet">
                                        <div class="col-md-4 col-sm-4 col-xs-4 form-group centrado">
                                            <ul class="listIcon">
                                                <li>Asiento
                                                    <br>
                                                    Disponible</li>
                                                <li>
                                                    <div class="imgsbus img-asientod">
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-md-4 col-sm-4 col-xs-4 form-group centrado">
                                            <ul class="listIcon">
                                                <li>Asiento
                                                    <br>
                                                    Seleccionado</li>
                                                <li>
                                                    <div class="imgsbus img-asientos">
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-md-4 col-sm-4 col-xs-4 form-group centrado">
                                            <ul class="listIcon">
                                                <li>Asiento
                                                    <br>
                                                    No Disponible</li>
                                                <li>
                                                    <div class="imgsbus img-asienton">
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="cl">
                                    </div>
                                    <div class="col-md-12 detalle-compra-desktop">
                                        <div class="col-md-6 centrado">
                                            <table class="tblVerde" style="width: 65%">
                                                <tr class="title-fileds" style="background:#e1e1e1;">
                                                    <td colspan="2">
                                                        Nivel del Bus
                                                    </td>
                                                    <td colspan="2">
                                                        Precio por Nivel
                                                    </td>
                                                </tr>
                                                <tr id="firstlevelretorno" runat="server">
                                                    <td class="literal">
                                                        1º Piso
                                                    </td>
                                                    <td>
                                                        <div class="imgsbus img-piso1">
                                                        </div>
                                                    </td>
                                                    <td class="literal">
                                                        <asp:Label ID="Label2" runat="server" Text="S/."></asp:Label>
                                                    </td>
                                                    <td class="literal" align="right">
                                                        <asp:Label ID="lblprecionivel1retorno" runat="server" Text="0.00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="secondlevelretorno" runat="server">
                                                    <td class="literal">
                                                        2º Piso
                                                    </td>
                                                    <td>
                                                        <div class="imgsbus img-piso2">
                                                        </div>
                                                    </td>
                                                    <td class="literal">
                                                        <asp:Label ID="Label4" runat="server" Text="S/."></asp:Label>
                                                    </td>
                                                    <td class="literal" align="right">
                                                        <asp:Label ID="lblprecionivel2retorno" runat="server" Text="0.00"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-md-6 centrado">
                                            <table class="tblVerde" style="width: 65%">
                                                <tr class="title-fileds" style="background:#e1e1e1;">
                                                    <th>
                                                        Acción
                                                    </th>
                                                    <th>
                                                        Asiento
                                                    </th>
                                                    <th>
                                                        Nivel
                                                    </th>
                                                    <th>
                                                        Precio
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="gvasientoseleccionadosretorno" runat="server" AutoGenerateColumns="False"
                                                                    ShowHeader="False" AllowPaging="True" BorderStyle="None" DataKeyNames="IDS" GridLines="None"
                                                                    PageSize="4">
                                                                    <Columns>
                                                                        <asp:TemplateField ShowHeader="False">
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="Button1" runat="server" CssClass="item-asiento-seleccion btnQuitar"
                                                                                    CausesValidation="False" CommandName="Delete" Text="" Width="75px" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="Nume_Asiento">
                                                                            <ItemStyle CssClass="item-asiento-seleccion" Width="84px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Nivel">
                                                                            <ItemStyle CssClass="item-asiento-seleccion" Width="64px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Costo" DataFormatString="{0:F2}">
                                                                            <ItemStyle CssClass="item-asiento-seleccion" Width="72px" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" CssClass="gridfooter" />
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btncostoretorno1" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="btncostoretorno2" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="cl">
                            </div>
                        </div>
                        <!-- panel tab!-->
                        <!-- Detalle de Compra!-->
                        <div class="conten-detalle-compra" id="vista_detalle_mobile_ida" runat="server">
                            <div class="titulo-detalle-compra">
                                IDA
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvasientoseleccionadosidamobile" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                                        AllowPaging="True" BorderStyle="None" DataKeyNames="IDS" GridLines="None" PageSize="4" CssClass="detalle-compra-app" >
                                        <Columns>
                                            <asp:TemplateField  ShowHeader="true" HeaderText="Accion" ItemStyle-CssClass="item-accion">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" runat="server" CssClass="btnQuitar"
                                                        CausesValidation="False" CommandName="Delete" Text=""/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Nume_Asiento" ShowHeader="true" HeaderText="Asiento">
                                                <ItemStyle CssClass="item-asiento" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nivel" ShowHeader="true" HeaderText="Piso">
                                                <ItemStyle CssClass="item-piso" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Costo" DataFormatString="{0:F2}" ShowHeader="true" HeaderText="Precio S/.">
                                                <ItemStyle CssClass="item-precio" />
                                            </asp:BoundField>
                                        </Columns>
                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" CssClass="gridfooter" />
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btncostoretorno1" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btncostoretorno2" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="conten-detalle-compra" id="vista_detalle_mobile_retorno" runat="server">
                            <div class="titulo-detalle-compra">
                                RETORNO
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvasientoseleccionadosretornomobile" runat="server" AutoGenerateColumns="False" ShowHeader="True"
                                        AllowPaging="True" BorderStyle="None" DataKeyNames="IDS" GridLines="None" PageSize="4" CssClass="detalle-compra-app" >
                                        <Columns>
                                            <asp:TemplateField  ShowHeader="true" HeaderText="Accion" ItemStyle-CssClass="item-accion">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" runat="server" CssClass="btnQuitar"
                                                        CausesValidation="False" CommandName="Delete" Text=""/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Nume_Asiento" ShowHeader="true" HeaderText="Asiento">
                                                <ItemStyle CssClass="item-asiento" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nivel" ShowHeader="true" HeaderText="Piso">
                                                <ItemStyle CssClass="item-piso" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Costo" DataFormatString="{0:F2}" ShowHeader="true" HeaderText="Precio S/.">
                                                <ItemStyle CssClass="item-precio" />
                                            </asp:BoundField>
                                        </Columns>
                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" CssClass="gridfooter" />
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btncostoretorno1" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btncostoretorno2" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        
                    </div>
                </div>
                <div class="cl">
                </div>
                <div class="contBoton">
                    <asp:Button ID="btnregresar" runat="server" Text="Regresar" CssClass="btnRegresar" />
                    <asp:Button ID="btncontinuar" runat="server" Text="Continuar" CssClass="btnSiguiente" />
                </div>
                <div class="cl">
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btncostoida1" runat="server" Text="Button" Visible="true" />
                        <asp:Button ID="btncostoida2" runat="server" Text="Button" Visible="true" />
                        <asp:Button ID="btncostoretorno1" runat="server" Text="Button" Visible="true" />
                        <asp:Button ID="btncostoretorno2" runat="server" Text="Button" Visible="true" />
                        <asp:Button ID="btndeleteida" runat="server" Text="Button" Visible="true" />
                        <asp:Button ID="btndeleteretorno" runat="server" Text="Button" Visible="true" />
                        <input id="nasientoida" name="nasientoida" type="hidden" runat="server" />
                        <input id="nasientoretorno" name="nasientoretorno" type="hidden" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="btnrefreshuser" runat="server" Width="1px" />
    </div>
</asp:Content>
