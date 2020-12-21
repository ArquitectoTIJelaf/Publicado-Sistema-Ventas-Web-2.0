<%@ Page Language="VB" AutoEventWireup="false" CodeFile="confirmarpago.aspx.vb" Inherits="confirmarpago" %>

<%@ Import Namespace="PUtilitario" %>

<%@ Register Src="WUCPrgss.ascx" TagName="WUCPrgss" TagPrefix="uc2" %>
<%@ Register Src="WUCViewUser.ascx" TagName="WUCViewUser" TagPrefix="uc1" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <title>[NombreEmpresa]</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <meta name="author" content="Jelaf integradores SRL" />
    <meta name="description" content="<%= AppSettings.valueString("RazonSocial")%>" />
    <meta name="keywords" content="Pasajes a diversas ciudades del peru" />
    <meta name="DC.title" content="<%= AppSettings.valueString("RazonSocial")%>" />
    <meta name="DC.description" lang="es" content="<%= AppSettings.valueString("RazonSocial")%>" />
    <meta name="geo.region" content="PE-LIM" />
    <meta property="og:title" content="<%= AppSettings.valueString("RazonSocial")%>" />
    <meta property="og:type" content="company" />
    <meta property="og:description" content="<%= AppSettings.valueString("RazonSocial")%>" />
    <link rel="shortcut icon" href="images/favicon.ico" type="image/x-icon" />
    <!--[if lt IE 9]>
    <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
    <style type="text/css">
        .gradient {
            filter: none;
        }
    </style>
    <![endif]-->
    <link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="stylesheet" href="css/jquery-ui-1.9.2.custom.min.css">
    <link rel="stylesheet" href="css/style.css">

    <script type="text/javascript" src="js/Extension.min.js"></script>

    <script type="text/javascript" src="js/jquery-1-11-1.js"></script>

    <script type="text/javascript" src="js/jquery-ui-1.9.2.custom.min.js"></script>

    <script type="text/javascript" src="js/bootstrap.min.js"></script>

    <script type="text/javascript" src="js/Utiles.js"></script>

    <script type="text/javascript" src="js/functions.js"></script>

    <script type="text/javascript" src="js/main.js"></script>

    <style type="text/css">
        .start-js-btn
        {
            margin-left: -530px;
            position: relative;
            left: 50%;
            margin-top: 10px;
        }
    </style>

    <script type="text/javascript">
    function Vali()
    {
        $(".start-js-btn").click(function(e){
          if ($('#ckbterminos').is(':checked') == false) {
                MessageModalInformativo('Por favor acepte los términos y condiciones')
                e.pre
                return false;
            }
        })          

        $('.contensection input:radio').attr('name','fp')
        $('.omp input:radio').click(function() {
            if ($(this).is(':checked')) {
                var idradio = $(this).attr('id')
                var father = $('#' + idradio).parent();
                var flagventa = $(father).next()
                var comision = $(father).next().next()
                var proveedor = $(father).next().next().next()
                var formapago = $(father).next().next().next().next();

                $('#<%= hivflagventa.ClientID%>').attr('value', $(flagventa).get(0).value)
                $('#<%= hivcomision.ClientID%>').attr('value', $(comision).get(0).value)
                $('#<%= hivproveedor.ClientID%>').attr('value', $(proveedor).get(0).value)
                $('#<%= hivformapago.ClientID%>').attr('value', $(formapago).get(0).value)


            }
        })
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
        //setTimeout(function () { window.location.href = 'end.aspx'; }, 60000);
        setTimeout(function () { window.location.href = 'end.aspx'; }, 900000);

    }

    $(document).ready(function() {
        $('#<%=btnrefreshuser.ClientId %>').hide()

        $('#<%= btncontinuar.ClientID%>').click(function() {

        if ($('#ckbterminos').is(':checked') == false) {
                MessageModalInformativo('Por favor acepte los términos y condiciones')
                return false;
            }
            
            if ($('.omp input:radio').is(':checked') == false) {
                MessageModalInformativo('Por favor seleccionar un medio de pago')
                return false;
            }
        })
 
    })
    </script>

</head>
<body>
    <form id="form1" runat="server" action="http://localhost:8542/respuesta.aspx"
    method="post">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnablePageMethods="True" ScriptMode="Release">
    </asp:ScriptManager>
    <uc2:WUCPrgss ID="WUCPrgss1" runat="server" />

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
        <p class="container centrado tituloBanner">
         </p>
    </div>
    <div class="indicador">
        <div class="container col-md12 " id="contIndicador">
            <ul id="menuIndicador">
                <li><a href="#">Rutas</a></li>
                <li><a href="#">Asientos</a></li>
                <li><a href="#">Pasajeros</a></li>
                <li><a href="#" class="active">Confirmación</a></li>
                <li><a href="#">Resultados</a></li>
            </ul>
        </div>
    </div>
    <div class="container">
        <h4 class="title-page">
            <span class="imgsprite"></span>
            <label>
                Confirmación
            </label>
        </h4>
        <div class="contenido resultados" id="confirmacion">
            <div class="col-md-12  top-titulo1">
            </div>
            <div class="col-md-4">
            </div>
            <div class="col-md-4 cont-titulo1">
                <div class="img-left-t2">
                </div>
                <div class="contTitulo1">
                    <h4>
                        Detalle de Itinerario</h4>
                </div>
                <div class="imgsprite img-right-t2">
                </div>
            </div>
            <div class="col-md-4">
            </div>
            <div class="cl">
            </div>
            <div id="contIda">
                <div id="dataitinerarioidahead" runat="server" class="centrado" style="width: 500px;
                    margin: auto;">
                    <div class="form-group">
                        <div class="imgsprite img-busretorno">
                        </div>
                        Itinerario de Ida:
                    </div>
                </div>
                <div class="col-md-12">
                    <table cellspacing="0" class="tblVerde" border="1" id="dataitinerarioida" runat="server">
                        <tbody>
                            <tr class="title-fileds">
                                <th>
                                    Ruta
                                </th>
                                <th>
                                    Servicio
                                </th>
                                <th>
                                    Fecha Viaje
                                </th>
                                <th>
                                    Hora Salida
                                </th>
                                <th>
                                    Hora Llegada
                                </th>
                                <th scope="col">
                                    Monto (S/)
                                </th>
                            </tr>
                            <tr class="list-itinerario">
                                <td>
                                    <asp:Label ID="lblrutaida" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblservicioida" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblfechaviajeida" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblhorasalidaida" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblhorallegadaida" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblmontosolesida" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="cl">
            </div>
            <div id="contRetorno">
                <div id="dataitinerarioretornotitle" runat="server" class="centrado" style="width: 500px;
                    margin: auto;">
                    <div class="form-group">
                        <div class="imgsprite img-busretorno">
                        </div>
                        <asp:Label ID="lbldatosretorno" runat="server" Text=""></asp:Label>:
                    </div>
                </div>
                <div class="col-md-12">
                    <table id="dataitinerarioretornohead" runat="server" cellspacing="0" class="tblVerde"
                        border="1">
                        <tbody>
                            <tr class="title-fileds">
                                <th>
                                    Ruta
                                </th>
                                <th>
                                    Servicio
                                </th>
                                <th>
                                    Fecha Viaje
                                </th>
                                <th>
                                    Hora Salida
                                </th>
                                <th>
                                    Hora Llegada
                                </th>
                                <th scope="col">
                                    Monto (S/)
                                </th>
                            </tr>
                            <tr class="list-itinerario">
                                <td>
                                    <asp:Label ID="lblrutaretorno" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblservicioretorno" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblfechaviajeretorno" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblhorasalidaretorno" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblhorallegadaretorno" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblmontosolesretorno" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="cl">
            </div>
            <div class="detalle-compra-mobile">
                <h4>
                    Detalle Itinerario</h4>
                <ul class="nav nav-tabs" role="tablist">
                    <li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab"
                        data-toggle="tab" class="tab-mobile">
                        <asp:Label ID="lblrutaidamobile" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Label ID="lblfechaviajeidamobile" runat="server" Text=""></asp:Label>
                    </a></li>
                    <li id="lbldatosretornomobile" runat="server" role="presentation"><a href="#profile"
                        aria-controls="profile" role="tab" data-toggle="tab" aria-expanded="false" class="tab-mobile">
                        <asp:Label ID="lblrutaretornomobile" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Label ID="lblfechaviajeretornomobile" runat="server" Text=""></asp:Label>
                    </a></li>
                </ul>
                <div class="tab-content seleccion1">
                    <div role="tabpanel" class="tab-pane active" id="home">
                        <table cellspacing="0" class="tblVerde" border="1">
                            <tbody>
                                <tr class="title-fileds">
                                    <th>
                                        Servicio
                                    </th>
                                    <th>
                                        Hora Salida
                                    </th>
                                    <th class="item-tablet">
                                        Hora Llegada
                                    </th>
                                    <th scope="col">
                                        Monto (S/)
                                    </th>
                                </tr>
                                <tr class="list-itinerario">
                                    <td>
                                        <asp:Label ID="lblservicioidamobile" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblhorasalidaidamobile" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="item-tablet">
                                        <asp:Label ID="lblhorallegadaidamobile" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblmontosolesidamobile" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="profile">
                        <table cellspacing="0" class="tblVerde" border="1">
                            <tbody>
                                <tr class="title-fileds">
                                    <th>
                                        Servicio
                                    </th>
                                    <th>
                                        Hora Salida
                                    </th>
                                    <th class="item-tablet">
                                        Hora Llegada
                                    </th>
                                    <th scope="col">
                                        Monto (S/)
                                    </th>
                                </tr>
                                <tr class="list-itinerario">
                                    <td>
                                        <asp:Label ID="lblservicioretornomobile" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblhorasalidaretornomobile" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="item-tablet">
                                        <asp:Label ID="lblhorallegadaretornomobile" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblmontosolesretornomobile" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>


    <%If (AppSettings.valueString("PasarelaMultiTarjeta") = "1") Then%>
            <div class="contenido info">
                <i class="imgsprite icon-preg"></i>
                <p>
                    Si vas a pagar con tu tarjeta de crédito, recuerda:
                <br>
                    - Pedir autorización en tu banco para realizar compras por internet con este medio
                de pago.<br>
                    - Tu banco puede cobrarte comisiones y/o importes adicionales por el uso de la tarjeta.<br>
                    - Si quieres pagar con Visa, la tarjeta debe ser emitida por una entidad bancaria
                y estar registrada en el servicio Verified by Visa.<br>
                    - Según la tarifa y la anticipación con la que compres tu pasaje, algunos medios
                de pago podrían no estar disponibles.
                </p>
                <p class="centrado" style="font-size: 1.2em">
                    <strong>Esta tienda está autorizada por
                    <img src="images/logo_visa_multitarjeta.png" style="width: 60px; margin: 0 3px 0 3px;">
                        <%--                    <img src="images/logo_pagoefectivo_112x52.png" style="width: 110px; margin: 0 3px 0 3px;">--%>
                    para realizar
                    transacciones electrónicas.</strong>
                </p>

            </div>	
	<% Else %>
        <div class="contenido info">
            <i class="imgsprite icon-preg"></i>
            <p>
                Si vas a pagar con tu tarjeta de crédito, recuerda:
                <br>
                - Pedir autorización en tu banco para realizar compras por internet con este medio
                de pago.<br>
                - Tu banco puede cobrarte comisiones y/o importes adicionales por el uso de la tarjeta.<br>
                - Si quieres pagar con Visa, la tarjeta debe ser emitida por una entidad bancaria
                y estar registrada en el servicio Verified by Visa.<br>
                - Según la tarifa y la anticipación con la que compres tu pasaje, algunos medios
                de pago podrían no estar disponibles.</p>
            <p class="centrado" style="font-size: 1.2em">
                <strong>Esta tienda está autorizada 
                    <img src="images/logo_visa.png" style="width: 60px; margin: 0 3px 0 3px;">para realizar
                    transacciones electrónicas.</strong></p>
        </div>	 
	<%End If %>
        <div class="cl">
        </div>
        <div class="contenido fPagos">
            <div class="col-md-12  top-titulo2">
            </div>
            <div class="col-md-4">
            </div>
            <div class="col-md-4 cont-titulo2">
                <div class="img-left-t2">
                </div>
                <div class="contTitulo2">
                    <h4>
                        FORMA DE PAGO</h4>
                </div>
                <div class="imgsprite img-right-t2">
                </div>
            </div>
            <div class="col-md-4">
            </div>
            <div class="cl">
            </div>
            <div class="col-md-12">
                <input id="hivflagventa" type="hidden" runat="server" />
                <input id="hivcomision" type="hidden" runat="server" />
                <input id="hivproveedor" type="hidden" runat="server" />
                <input id="hivformapago" type="hidden" runat="server" />
                Seleccione su tarjeta:
            </div>
            <div class="col-md-12">
            </div>
        </div>
    </div>
    <div class="fPagosMobile">
        Forma de pago
    </div>

    <script id="javascriptBoton" src='<%= AppSettings.valueString("UrlLibreria")%>' data-sessiontoken='<%= SessionManager.VisaNet.SessionToken%>'
        data-merchantname='<%= AppSettings.valueString("NombreComercio")%>' data-merchantid='<%= SessionManager.VisaNet.CodigoComercio%>' data-channel='<%= SessionManager.VisaNet.Channel%>'
        data-merchantlogo='<%= AppSettings.valueString("UrlHttpLogoEmpresa")%>' data-formbuttoncolor="#124aa1" data-purchasenumber='<%= SessionManager.VisaNet.NumeroPedido%>'  
        data-expirationminutes='<%= SessionManager.VisaNet.ExpirationMinutes%>' data-timeouturl='<%= SessionManager.VisaNet.UrlTimeout%>'
        data-amount='<%= SessionManager.VisaNet.Monto%>'>
    </script>

    <div class="container">
        <div class="contenidoTerminos">
            <input id="ckbterminos" type="checkbox" checked="checked" />
            <label for="ckbterminos">
                <a href="javascript:void(0);" class="enlace terminos">Leer y Aceptar Terminos y Condiciones</a></label><br />
        </div>
        <div class="col-md-10 centrado">
        </div>
        <div style="clear: both;">
        </div>
        <div class="contBoton">
            <asp:Button ID="btnregresar" runat="server" Text="Regresar" CssClass="btnRegresar"
                Visible="false" />
            <a class="btnRegresar" href="asignarpasajero.aspx">Regresar</a>
            <div style="float: right;">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btncontinuar" runat="server" Text="Pagar" CssClass="btnPagar" Visible="false"
                            ValidationGroup="emailcliente" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="cl">
        </div>
    </div>
    <asp:Button ID="btnrefreshuser" runat="server" Width="1px" />
    </form>
    
    <footer>    
        <div id="contFoter">
            <div class="container">
                <div class="col-md-6">
                    <p class="f-dev">Desarrollado por: <a href="http://www.jelaf.pe/" target="_blank">Jelaf integradores SRL</a></p><br/>
                    <ul id="menu-foter">
                        <li><a href="#" class="terminos">Términos y condiciones</a></li>
                        <li><a href="#">Libro de reclamaciones</a></li>
                        <li><a href="#">Preguntas frecuentes</a></li>
                    </ul>
                </div>
                <div class="col-md-1">

                </div>
                <div class="col-md-5">
                    Formas de Pago: &nbsp; &nbsp; Certificado:  <br/>
                   
                    <div class="col-md-3">
                        <img src="images/visa_pos_fc.png" alt="Pague con VISA" width="90px" />
                    </div>
                    <div class="col-md-4">
                    
                        <!-- Begin DigiCert site seal HTML --> 
                       
                    
                    </div> 
                    
                     <!--div class="col-md-1 imgsprite img-mastercard"></div>
                    <div class="col-md-1 imgsprite img-americanexpress"></div>
                    <div class="col-md-1 imgsprite img-diners"></div-->
                    
                    <!-- Begin DigiCert site seal HTML and JavaScript -->
                   
                    <!-- End DigiCert site seal HTML and JavaScript -->
                    
                    
                </div>       
            </div>
        </div>

        <div id="copyright">
            <p class="container"> Transportes Carhuamayo S.R.L. Copyright © Todos los derechos reservados.</p>
        </div>
    </footer>
</body>
</html>
