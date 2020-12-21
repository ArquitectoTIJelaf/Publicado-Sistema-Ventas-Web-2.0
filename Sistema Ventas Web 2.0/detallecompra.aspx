<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false"
    CodeFile="detallecompra.aspx.vb" Inherits="detallecompra" Title="Detalle de compras  -  [NombreEmpresa] - Venta de Pasajes Online" %>

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
    <div class="container contDetcompra" id="detalle-compra">
        <h4 class="title-page">
            <span class="imgsprite"></span>
            <label>
                Detalle Compra</label>
        </h4>
        <div class="contenido info">
            <i class="imgsprite icon-preg"></i>
            <p>
                Revise la información detallada de su compra, presione el botón imprimir para visualizar
                su comprobante de compra.<br>
                Para el embarque del bus tener en cuenta hora, punto y Dirección de embarque.<br>
                El costo total no incluye tasa de embarque en terminales terrestres.<br>
                Los pasajeros deben presentarse 30 minutos antes de la hora de embarque.<br>
            </p>
            <p>
                <a href="#" class="terminos">Términos y condiciones de la compra</a></p>
            <!--p>Presionar este icono para modificar datos del pasajero <img src="images/user.png"></p-->
        </div>
        <div class="contenido datos-viaje">
            <div class="col-md-12  top-titulo1">
            </div>
            <div class="col-md-4">
                &nbsp;
            </div>
            <div class="col-md-4 cont-titulo1">
                <div class="img-left-t2">
                </div>
                <div class="contTitulo1">
                    <h4>
                        Resultados de la compra</h4>
                </div>
                <div class="imgsprite img-right-t2">
                </div>
            </div>
            <div class="col-md-4">
            </div>
            <div class="cl">
            </div>
            <div class="col-md-12 centrado titDetalle">
                <h2>
                    ¡Su compra se ha realizado con éxito!</h2>
                <h4>
                    Su confirmación ha sido enviada a su correo.</h4>
                <div class="cl">
                </div>
                <!--img src="images/logo.png"-->
            </div>
            <div class="cl">
            </div>
            <div class="col-md-12">
                <p class="centrado">
                    <asp:Label ID="lbltitlehorarioida" runat="server" Text="Información de Compra"></asp:Label></p>
                <div class="blockcontencenter">
                    <table width="95%" align="center">
                        <tr id="dataitinerarioidatitle" runat="server">
                            <td class="titlecampo">
                                Nº Orden:
                            </td>
                            <td class="titlevalor">
                                <asp:Label ID="lblnumorden" runat="server"></asp:Label>
                            </td>
                            <td class="titlecampo">
                                Usuario Cliente:
                            </td>
                            <td class="titlevalor">
                                <asp:Label ID="lblusercliente" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titlecampo">
                                Fecha Compra:
                            </td>
                            <td class="titlevalor">
                                <asp:Label ID="lblfechacompra" runat="server"></asp:Label>
                            </td>
                            <td class="titlecampo">
                                Nº Asientos:
                            </td>
                            <td class="titlevalor">
                                <asp:Label ID="lblnumasientos" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titlecampo">
                                Numero de Tarjeta:
                            </td>
                            <td class="titlevalor">
                                <asp:Label ID="lbltarjeta" runat="server"></asp:Label>
                            </td>
                            <td class="titlecampo">
                                Costo Neto (soles) :
                            </td>
                            <td class="titlevalor">
                                <asp:Label ID="lblcostoneto" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titlecampo">
                                Nota:
                            </td>
                            <td class="titlevalor" colspan="3">
                                <asp:Label ID="lblnota" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="cl">
                </div>
                <p class="centrado">
                    <!--i class="imgsprite img-busida"></i -->
                    <asp:Label ID="Label1" runat="server" Text="Datos de Viaje de Ida"></asp:Label></p>
                <div id="idheaderviajeida" runat="server" style="margin-bottom: 20px;">
                    <div class="blockcontencenter">
                        <table width="95%" align="center">
                            <tr id="Tr1" runat="server">
                                <td class="titlecampo">
                                    Origen:
                                </td>
                                <td class="titlevalor">
                                    <asp:Label ID="lblorigenida" runat="server"></asp:Label>
                                </td>
                                <td class="titlecampo">
                                    Fecha y Hora de Viaje:
                                </td>
                                <td class="titlevalor">
                                    <asp:Label ID="lblfechaviajeida" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titlecampo">
                                    Destino
                                </td>
                                <td class="titlevalor">
                                    <asp:Label ID="lbldesitnoida" runat="server"></asp:Label>
                                </td>
                                <td class="titlecampo">
                                    Servicio:
                                </td>
                                <td class="titlevalor">
                                    <asp:Label ID="lblservicionameida" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="iddetalleviajeida" runat="server" class="col-md-12 detalleCompra">
                    <table cellspacing="0" class="tblDC" border="1">
                        <tbody>
                            <tr class="title-fileds">
                                <th width="72px">
                                    Asiento
                                </th>
                                <th width="82px">
                                    S/ Costo
                                </th>
                                <th width="170px">
                                    Pasajero
                                </th>
                                <th width="52px">
                                    Edad
                                </th>
                                <th width="130px">
                                    RUC
                                </th>
                                <th width="140px">
                                    Hora Embarque
                                </th>
                                <th width="145px">
                                    Punto Embarque
                                </th>
                                <th>
                                    Dirección Embarque
                                </th>
                            </tr>
                        </tbody>
                    </table>
                    <asp:GridView ID="gvdetalleventaida" runat="server" AutoGenerateColumns="False" DataKeyNames="nume_asiento,Tipo_Doc,DNI,Nit_Cliente,Edad"
                        GridLines="None" ShowHeader="False">
                        <Columns>
                            <asp:BoundField DataField="Nume_asiento">
                                <ItemStyle HorizontalAlign="Center" Width="72px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Prec_venta" DataFormatString="{0:F2}">
                                <ItemStyle HorizontalAlign="Center" Width="82px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombre">
                                <ItemStyle HorizontalAlign="Center" Width="172px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Edad">
                                <ItemStyle HorizontalAlign="Center" Width="52px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Nit_Cliente">
                                <ItemStyle HorizontalAlign="Center" Width="130px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Hora_Embarque">
                                <ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Punto_Embarque">
                                <ItemStyle HorizontalAlign="Center" Width="145px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Direccion_Embarque" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="cl">
                </div>
                <div id="idheaderviajeretorno" runat="server" style="margin-bottom: 20px;">
                    <p class="centrado">
                        <!--i class="imgsprite img-busretorno"></i -->
                        <asp:Label ID="Label2" runat="server" Text="Datos de Viaje de Retorno"></asp:Label>
                    </p>
                    <div class="cl">
                    </div>
                    <table width="95%" align="center">
                        <tr id="Tr2" runat="server">
                            <td class="titlecampo">
                                Origen:
                            </td>
                            <td class="titlevalor">
                                <asp:Label ID="lblorigenretorno" runat="server"></asp:Label>
                            </td>
                            <td class="titlecampo">
                                Fecha y Hora de Viaje:
                            </td>
                            <td class="titlevalor">
                                <asp:Label ID="lblfechaviajeretorno" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="titlecampo">
                                Destino
                            </td>
                            <td class="titlevalor">
                                <asp:Label ID="lbldestinoretorno" runat="server"></asp:Label>
                            </td>
                            <td class="titlecampo">
                                Servicio:
                            </td>
                            <td class="titlevalor">
                                <asp:Label ID="lblservicioretorno" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="cl">
                </div>
                <div id="iddetalleviajeretorno" runat="server" class="col-md-12 detalleCompra">
                    <table cellspacing="0" class="tblDC" border="1">
                        <tbody>
                            <tr class="title-fileds">
                                <th width="72px">
                                    Asiento
                                </th>
                                <th width="82px">
                                    S/ Costo
                                </th>
                                <th width="170px">
                                    Pasajero
                                </th>
                                <th width="52px">
                                    Edad
                                </th>
                                <th width="130px">
                                    RUC
                                </th>
                                <th width="140px">
                                    Hora Embarque
                                </th>
                                <th width="145px">
                                    Punto Embarque
                                </th>
                                <th>
                                    Dirección Embarque
                                </th>
                            </tr>
                        </tbody>
                    </table>
                    <asp:GridView ID="gvdetallventaretorno" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="nume_asiento,Tipo_Doc,DNI,Nit_Cliente,Edad" GridLines="None" ShowHeader="False">
                        <Columns>
                            <asp:BoundField DataField="Nume_asiento">
                                <ItemStyle HorizontalAlign="Center" Width="72px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Prec_venta" DataFormatString="{0:F2}">
                                <ItemStyle HorizontalAlign="Center" Width="82px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombre">
                                <ItemStyle HorizontalAlign="Center" Width="172px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Edad">
                                <ItemStyle HorizontalAlign="Center" Width="52px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Nit_Cliente">
                                <ItemStyle HorizontalAlign="Center" Width="130px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Hora_Embarque">
                                <ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Punto_Embarque">
                                <ItemStyle HorizontalAlign="Center" Width="145px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Direccion_Embarque" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="col-md-12 centrado contBtnR">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/reporte.aspx" Target="_blank"
                    CssClass="boton">
                Imprimir</asp:HyperLink>
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/destinos.aspx" Target="_blank"
                    CssClass="boton">
                Volver a Inicio</asp:HyperLink>
                <br />
                <div class="cl">
                    <br />
                </div>
            </div>
            <br />
            <div class="cl">
            </div>
        </div>
        <div class="contenido datos-viaje-mobile">
            <div class="contTitulo1">
                <h4>
                    RESULTADOS DE LA COMPRA
                </h4>
            </div>
            <div class="titDetalle">
                <h4>
                    Su confirmación ha sido enviada a su correo. Tambien puede imprimir el siguiente
                    comprabante.
                </h4>
                <h3>
                    INFORMACIÓN DE COMPRA
                </h3>
            </div>
            <table class="info-compra">
                <tr>
                    <td class="campo-mobile">
                        Nº Orden:
                    </td>
                    <td class="valor-mobile">
                        <asp:Label ID="lblnumordenmobile" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="campo-mobile">
                        Usuario Cliente:
                    </td>
                    <td class="valor-mobile">
                        <asp:Label ID="lbluserclientemobile" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="campo-mobile">
                        Fecha Compra:
                    </td>
                    <td class="valor-mobile">
                        <asp:Label ID="lblfechacompramobile" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="campo-mobile">
                        Nº Asientos:
                    </td>
                    <td class="valor-mobile">
                        <asp:Label ID="lblnumasientosmobile" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="campo-mobile">
                        Numero de Tarjeta:
                    </td>
                    <td class="valor-mobile">
                        <asp:Label ID="lbltarjetamobile" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="campo-mobile">
                        Costo Neto (soles) :
                    </td>
                    <td class="valor-mobile">
                        <asp:Label ID="lblcostonetomobile" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="campo-mobile">
                        Nota:
                    </td>
                    <td class="valor-mobile" colspan="3">
                        <asp:Label ID="lblnotamobile" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div id="contIda" runat="server">
                <div class="titDetalle">
                    <h3>
                        DATOS DEL VIAJE DE IDA
                    </h3>
                </div>
                <table class="info-compra">
                    <tr>
                        <td class="campo-mobile">
                            ORIGEN:
                        </td>
                        <td class="valor-mobile">
                            <asp:Label ID="lblorigenidamobile" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="campo-mobile">
                            DESTINO:
                        </td>
                        <td class="valor-mobile">
                            <asp:Label ID="lbldesitnoidamobile" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="campo-mobile">
                            SERVICIO:
                        </td>
                        <td class="valor-mobile">
                            <asp:Label ID="lblservicionameidamobile" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="campo-mobile">
                            FECHA Y HORA VIAJE:
                        </td>
                        <td class="valor-mobile">
                            <asp:Label ID="lblfechaviajeidamobile" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:DataList ID="dlpasajerosida" runat="server" CssClass="conten-detallecompra">
                    <ItemTemplate>
                        <table class="info-compra">
                            <tr>
                                <td class="titulo-mobile">
                                    ASIENTO
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="asiento" runat="server" Text='<%# Eval("Nume_asiento") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    COSTO
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="costo" runat="server" Text='<%# Eval("Prec_venta") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    PASAJERO
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    EDAD
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="edad" runat="server" Text='<%# Eval("Edad") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    RUC
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="ruc" runat="server" Text='<%# Eval("Nit_Cliente") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    HORA EMBARQUE
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="horaembarque" runat="server" Text='<%# Eval("Hora_Embarque") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    PUNTO EMBARQUE
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="puntoembarque" runat="server" Text='<%# Eval("Punto_Embarque") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    DIRC. EMBARQUE
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="dirembarque" runat="server" Text='<%# Eval("Direccion_Embarque") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div id="contRetorno" runat="server">
                <div class="titDetalle">
                    <h3>
                        DATOS DEL VIAJE DE RETORNO
                    </h3>
                </div>
                <table class="info-compra">
                    <tr>
                        <td class="campo-mobile">
                            ORIGEN:
                        </td>
                        <td class="valor-mobile">
                            <asp:Label ID="lblorigenretornomobile" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="campo-mobile">
                            DESTINO:
                        </td>
                        <td class="valor-mobile">
                            <asp:Label ID="lbldestinoretornomobile" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="campo-mobile">
                            SERVICIO:
                        </td>
                        <td class="valor-mobile">
                            <asp:Label ID="lblservicioretornomobile" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="campo-mobile">
                            FECHA Y HORA VIAJE:
                        </td>
                        <td class="valor-mobile">
                            <asp:Label ID="lblfechaviajeretornomobile" runat="server"></asp:Label>
                        </td>
                    </tr>

                </table>
                <asp:DataList ID="dlpasajerosretorno" runat="server" CssClass="conten-detallecompra">
                    <ItemTemplate>
                        <table class="info-compra">
                            <tr>
                                <td class="titulo-mobile">
                                    ASIENTO
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="asiento" runat="server" Text='<%# Eval("Nume_asiento") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    COSTO
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="costo" runat="server" Text='<%# Eval("Prec_venta") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    PASAJERO
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    EDAD
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="edad" runat="server" Text='<%# Eval("Edad") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    RUC
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="ruc" runat="server" Text='<%# Eval("Nit_Cliente") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    HORA EMBARQUE
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="horaembarque" runat="server" Text='<%# Eval("Hora_Embarque") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    PUNTO EMBARQUE
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="puntoembarque" runat="server" Text='<%# Eval("Punto_Embarque") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="titulo-mobile">
                                    DIRC. EMBARQUE
                                </td>
                                <td class="valor-mobile">
                                    <asp:Label ID="dirembarque" runat="server" Text='<%# Eval("Direccion_Embarque") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
</asp:Content>
