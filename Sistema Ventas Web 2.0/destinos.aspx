<%@ Page Language="VB" MasterPageFile="~/Principal.master" EnableEventValidation="false"
    AutoEventWireup="false" CodeFile="destinos.aspx.vb" Inherits="destinos" Title="Seleccionar  ruta(s)  -  [NombreEmpresa] - Venta de Pasajes Online" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="WUCViewUser.ascx" TagName="WUCViewUser" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
            
        function ComparaFecha(fechainicio, fechafin, flag) {
            comp1 = fechainicio.substr(6, 4) + '' + fechainicio.substr(3, 2) + '' + fechainicio.substr(0, 2);
            comp2 = fechafin.substr(6, 4) + '' + fechafin.substr(3, 2) + '' + fechafin.substr(0, 2);
            if (flag == '0') {
                if ((comp1) > (comp2)) {
                    return false;
                }
            }
            if (flag == '1') {
                if ((comp1) >= (comp2)) {
                    return false;
                }
            }
            return true;
        }

    
        function Validation() {
            var __dcid = __dcid || []; __dcid.push(["DigiCertClickID_OmsQu21P", "13", "m", "black", "OmsQu21P"]); (function() { var cid = document.createElement("script"); cid.type = "text/javascript"; cid.async = true; cid.src = ("https:" === document.location.protocol ? "https://" : "http://") + "seal.digicert.com/seals/cascade/seal.min.js"; var s = document.getElementsByTagName("script"); var ls = s[(s.length - 1)]; ls.parentNode.insertBefore(cid, ls.nextSibling); } ());
            $(document).ready(function() {
                $("ul.topnav li").children("span").remove();
                $("ul.subnav").parent().append("<span></span>"); //Only shows drop down trigger when js is enabled (Adds empty span tag after ul.subnav*)
                $("ul.topnav li span").click(function() { //When trigger is clicked...
                    //Following events are applied to the subnav itself (moving subnav up and down)
                    $(this).parent().find("ul.subnav").slideDown('fast').show(); //Drop down the subnav on click
                    $(this).parent().hover(function() {
                    }, function() {
                        $(this).parent().find("ul.subnav").slideUp('slow'); //When the mouse hovers out of the subnav, move it back up
                    });
                    //Following events are applied to the trigger (Hover events for the trigger)
                }).hover(function() {
                    $(this).addClass("subhover"); //On hover over, add class "subhover"
                }, function() {	//On Hover Out
                    $(this).removeClass("subhover"); //On hover out, remove class "subhover"
                });
            });

            $('#<%=btnrefreshuser.ClientId %>').hide();
            $("#<%=txtfecactual.ClientID %>").hide();
            if ($('#<%=btnida.ClientId %>').attr('checked') == true) {
                $('#<%=txtfecharetorno.ClientId %>').hide();
                $('#<%=ImageButton2.ClientID %>').hide();
                $('#<%=lblfecharetorno.ClientID %>').hide();
            }

            if ($('#<%=btnidaandretorno.ClientId %>').attr('checked') == true) {
                $('.contRetorno').show();
            }

            $('#<%=btnida.ClientID %>').change(function() {
                $('.contRetorno').hide();
            })


            $('#<%=btnidaandretorno.ClientID %>').change(function() {
                $('.contRetorno').show();
            })

            $('#<%=btnserachprog.ClientID %>').click(function() {

                ValidarLista('#<%= DDListOrigen.ClientID%>', "Seleccionar origen")
                ValidarLista('#<%= DDListDestino.ClientID%>', "Seleccionar destino")


                if (cont == 0) {
                    CompareLista('#<%= DDListOrigen.ClientID%>', '#<%= DDListDestino.ClientID%>', 'El origen y destino <br>deben ser diferentes', 'different')
                }
                ClearMessage('#<%= txtfechaida.ClientID%>')
                ClearMessage('#<%= txtfecharetorno.ClientID%>')

                ValorError('#<%= txtfechaida.ClientID%>', 'Fecha Ida')

                if ($('#<%=btnidaandretorno.ClientId %>').attr('checked') == true) {
                    ValorError('#<%= txtfecharetorno.ClientID%>', 'Fecha Retorno')
                    if (ComparaFecha($('#<%= txtfechaida.ClientID%>').attr('value'), $('#<%= txtfecharetorno.ClientID%>').attr('value'), '1') == false) {
                        MessageModalInformativo('La Fecha Ida no debe ser mayor a la fecha de retorno', 1)
                        return false;
                    }
                    DateDifActual($('#<%=txtfechaida.ClientID%>').val(), $('#<%=txtfecactual.ClientID%>').val(), $('#<%= txtfechaida.ClientID%>'), '4', 'La fecha de viaje no puede ser <br> igual a la fecha de hoy');
                }
                else {
                    DateDifActual($('#<%=txtfechaida.ClientID%>').val(), $('#<%=txtfecactual.ClientID%>').val(), $('#<%= txtfechaida.ClientID%>'), '4', 'La fecha de viaje no puede ser <br> igual a la fecha de hoy');
                }

                if (cont > 0) { cont = 0; return false; }
            })

            $('.recomendacion').click(function() {
                popUp('html/recomendaciones.htm', 730, 330, 'iframe')
                return false;
            });
            /* Datapicker con inicio y fecha final*/
            from = $("#<%=txtfechaida.ClientID%>")
            .datepicker({
                defaultDate: new Date(),
                minDate: new Date(),
                changeMonth: true,
                buttonImage: "images/calendario.jpg",
                buttonImageOnly: true,
                showOn: "button",
                dateFormat: "dd/mm/yy",
                changeYear: true,
                yearRange: "1900:2023",
                numberOfMonths: 1
            })
            .on("change", function () {
                to.datepicker("option", "minDate", getDate(this));
            });

            to = $("#<%=txtfecharetorno.ClientID%>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                buttonImage: "images/calendario.jpg",
                buttonImageOnly: true,
                showOn: "button",
                dateFormat: "dd/mm/yy",
                minDate: new Date(),
                numberOfMonths: 1
            })
            .on("change", function () {
                from.datepicker("option", "maxDate", getDate(this));
            });

            var dateFormat = "dd/mm/yy";
            function getDate(element) {
                var date;
                try {
                    date = $.datepicker.parseDate(dateFormat, element.value);
                } catch (error) {
                    date = null;
                }

                return date;
            }
        }   
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContPWeb" runat="Server">
    <div class="loginuser right" id="viewuser" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc1:WUCViewUser ID="WUCViewUser1" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnrefreshuser" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="section-banner">
        <%--<span class="texto-banner">Escoge tu destino</span>
        <span class="container centrado tituloBanner">Escoge tu destino</span>--%>
    </div>
    <div class="indicador">
        <div class="container col-md12 " id="contIndicador">
            <ul id="menuIndicador">
                <li><a href="#" class="active">Ruta</a></li>
                <li><a href="#">Asientos</a></li>
                <li><a href="#">Pasajeros</a></li>
                <li><a href="#">Confirmación</a></li>
                <li><a href="#">Resultados</a></li>
            </ul>
        </div>
    </div>
    <div class="container">
        <!-- div class="contenido info">
        <i class="icon-preg"></i>
        <p>Los turnos mostrados en el sistema de ventas web no son todos con los que cuenta la empresa y que a futuro se
        implementaran en dicho sistema la totalidad de rutas y turnos de viaje.</p>
    </div -->
        <div class="contenido datos-viaje">
            <div class="col-md-12 col-sm-12  top-titulo1">
            </div>
            <div class="col-md-4 col-sm-1">
            </div>
            <div class="col-md-4 col-sm-10 cont-titulo1">
                <div class="img-left-t2">
                </div>
                <div class="contTitulo1">
                    <h4>
                        INGRESE DATOS DE VIAJE</h4>
                </div>
                <div class="imgsprite img-right-t2">
                </div>
            </div>
            <div class="col-md-4 col-sm-1">
            </div>
            <div class="cl">
            </div>
            <div style="margin-bottom: 6px;">
                <div class="col-md-6 col-sm-6 col-xs-6 text-right">
                    <asp:RadioButton ID="btnidaandretorno" runat="server" Text="Ida y Retorno" Font-Size="14px"
                        Checked="True" GroupName="typeviaje" Style="margin-right: 10px; color: #124aa1" />
                </div>
                <div class="col-md-6 col-sm-6 col-xs-6 text-left">
                    <asp:RadioButton ID="btnida" runat="server" Text="Solo Ida" Font-Size="14px" GroupName="typeviaje"
                        Style="margin-left: 10px; color: #124aa1" />
                </div>
                <div class="cl">
                </div>
            </div>
            <div class="col-md-12 contDatos">
                <div class="col-md-3 col-sm-6">
                    <div class="form-group">
                        <div class="imgsprite img-origen">
                        </div>
                        Origen</div>
                    <asp:DropDownList ID="DDListOrigen" runat="server" ValidationGroup="ConsValidates"
                        CssClass="tabsindex" AccessKey="O" TabIndex="1" DataTextField="Descripcion" DataValueField="Codi_Sucursal">
                    </asp:DropDownList>
                </div>
                <div class="col-md-3 col-sm-6">
                    <div class="form-group">
                        <div class="imgsprite img-destino">
                        </div>
                        Destino<br>
                    </div>
                    <asp:DropDownList ID="DDListDestino" runat="server" AccessKey="D" CssClass="tabsindex"
                        TabIndex="2" ValidationGroup="ConsValidates" DataTextField="Descripcion" DataValueField="Codi_Sucursal">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-sm-6">
                    <div class="form-group">
                        <div class="imgsprite img-busida">
                        </div>
                        <asp:Label ID="lblfechaida" runat="server" Text="Fecha Ida"></asp:Label><br>
                    </div>
                    <asp:TextBox ID="txtfechaida" runat="server" Enabled="False" EnableViewState="False"
                        BackColor="White" CssClass="datepicker disable_future_dates input-fecha"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton1" runat="server" TabIndex="3" Visible="false" CssClass="img-calendar" />
                </div>
                <div class="col-md-2 col-sm-6 contRetorno" id="contRetorno" runat="server">
                    <div class="form-group">
                        <div class="imgsprite img-busretorno">
                        </div>
                        <asp:Label ID="lblfecharetorno" runat="server" Text="Fecha Retorno"></asp:Label><br>
                    </div>
                    <asp:TextBox ID="txtfecharetorno" runat="server" BackColor="White" Enabled="False"
                        EnableViewState="False" CssClass="datepicker disable_future_dates input-fecha"
                        OnClientDateSelectionChanged="ValidateDateBefore"> </asp:TextBox>
                    <asp:ImageButton ID="ImageButton2" runat="server" TabIndex="4" Visible="false" CssClass="img-calendar" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnserachprog" runat="server" Text="Buscar" CssClass="btnBuscar" />
                </div>
            </div>
            <div class="cl">
            </div>
        </div>
        <div class="cl">
        </div>
        <asp:UpdatePanel ID="upresultados" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="contResultados" runat="server" class="contenido resultados">
                    <div class="col-md-12  top-titulo2">
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4 cont-titulo2">
                        <div class="img-left-t2">
                        </div>
                        <div class="contTitulo2">
                            <h4>
                                RESULTADOS DE BÚSQUEDA</h4>
                        </div>
                        <div class="imgsprite img-right-t2">
                        </div>
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="cl">
                    </div>
                       <div class="contTitulo3">
                            <h4>
                                RESULTADOS DE BÚSQUEDA</h4>
                        </div>
                    <!-- modulo rutas ida -->
                    <div id="iditinerarioida" runat="server" class="itinerario">
                        <div id="contIda">
                            <div class="centrado contenedor-resultados head-resultados">
                                <div class="form-group">
                                    <div class="imgsprite img-busida">
                                    </div>
                                    Itinerario de Ida: <span class="tituloverde">
                                        <asp:Label ID="lblrutaida" runat="server" CssClass="text"></asp:Label></span>
                                </div>
                            </div>
                            <div class="cl">
                            </div>
                            <div class="centrado contenedor-resultados">
                                <asp:GridView ID="gvrutasida" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="True">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/opt-destino.png" ShowSelectButton="True"
                                            ControlStyle-CssClass="select-img" />
                                        <asp:BoundField HeaderText="Empresa" DataField="Razon_Social">
                                            <ItemStyle CssClass="value-1" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Fecha Viaje" DataField="Hora_Partida_Large">
                                            <ItemStyle CssClass="value-1" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Servicio" DataField="Servicio">
                                            <ItemStyle CssClass="value-1" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="1º Piso" DataField="Piso_1">
                                            <ItemStyle CssClass="value-2" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="2º Piso" DataField="Piso_2">
                                            <ItemStyle CssClass="value-2" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Asientos Disponibles" DataField="AsientosLibres">
                                            <HeaderStyle CssClass="value-tablet" />
                                            <ItemStyle CssClass="value-1 value-tablet" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Embarques" DataField="ListaEmbarques">
                                            <HeaderStyle CssClass="value-tablet" />
                                            <ItemStyle CssClass="value-2 value-tablet"  />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="title-fileds" />
                                    <RowStyle CssClass="list-itinerario" />
                                    <SelectedRowStyle CssClass="list-itinerario-select" />
                                    <EmptyDataTemplate>
                                        <div class="advertencia">
                                            No se encontraron turnos disponibles</div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="cl">
                    </div>
                    <!-- modulo rutas ida -->
                    <div id="iditinerarioretorno" runat="server" class="itinerario">
                        <div id="contRetorno">
                            <div class="centrado contenedor-resultados head-resultados">
                                <div class="form-group">
                                    <div class="imgsprite img-busretorno">
                                    </div>
                                    Itinerario de Retorno: <span class="tituloverde">
                                        <asp:Label ID="lblrutaretorno" runat="server" CssClass="text"></asp:Label></span>
                                </div>
                            </div>
                            <div class="cl">
                            </div>
                            <div class="centrado contenedor-resultados">
                                <asp:GridView ID="gvrutasretorno" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="True">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/opt-destino.png" ShowSelectButton="True" />
                                        <asp:BoundField HeaderText="Empresa" DataField="Razon_Social">
                                            <ItemStyle CssClass="value-1" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField HeaderText="Fecha Viaje" DataField="Hora_Partida_Large">
                                            <ItemStyle CssClass="value-1" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Servicio" DataField="Servicio">
                                            <ItemStyle CssClass="value-1" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="1º Piso" DataField="Piso_1">
                                            <ItemStyle CssClass="value-2" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="2º Piso" DataField="Piso_2">
                                            <ItemStyle CssClass="value-2" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Asientos Disponibles" DataField="AsientosLibres">
                                            <HeaderStyle CssClass="value-tablet" />
                                            <ItemStyle CssClass="value-1 value-tablet" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Embarques" DataField="ListaEmbarques">
                                            <HeaderStyle CssClass="value-tablet" />
                                            <ItemStyle CssClass="value-2 value-tablet"  />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="title-fileds" />
                                    <RowStyle CssClass="list-itinerario" />
                                    <SelectedRowStyle CssClass="list-itinerario-select"/>
                                    <EmptyDataTemplate>
                                        <div class="advertencia">
                                            No se encontraron turnos disponibles</div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="cl">
                    </div>
                </div>
                <%--Resultados--%>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnserachprog" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="contBoton">
            <asp:UpdatePanel ID="upcontinuar" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnretorini" runat="server" Text="Regresar" CssClass="btnRegresar" />
                    <asp:Button ID="btncontinuar" runat="server" Text="Continuar" CssClass="btnSiguiente" />
                    <asp:Button ID="btnsoloidaoreto" runat="server" Text="Solo Ida o Retorno" CssClass="btnSiguiente" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnserachprog" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="gvrutasida" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="gvrutasretorno" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="cl">
        </div>
        <%--Para que es esto??--%>
        <asp:Button ID="btnrefreshuser" runat="server" Width="1px" />
        <div class="clear" style="visibility: hidden;">
            <asp:TextBox ID="txtfecactual" runat="server" Height="16px" Width="16px"></asp:TextBox>
        </div>
        <%--------%>
    </div>
    <%--contendor--%>
</asp:Content>
