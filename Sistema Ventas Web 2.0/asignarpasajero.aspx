<%@ Page Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false"
    CodeFile="asignarpasajero.aspx.vb" Inherits="asignarpasajero" Title="Registro de Pasajeros  -  [NombreEmpresa] - Venta de Pasajes Online" %>

<%@ Import Namespace="PUtilitario" %>

<%@ Register Src="WUCViewUser.ascx" TagName="WUCViewUser" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function ValiTab() {
            $(document).ready(function () {
                var index = $("#<%=FlagTabX.ClientID %>").val();
                if (index == 1) {
                    $(".tab-desktop-mobile li").eq(index).find("a").trigger("click");
                    var idElemento = $(".tab-desktop-mobile li").eq(index).find("a").attr("href");
                    if (idElemento == "#home-pasajero") {
                        idElemento = "#home";
                        if ($(idElemento).is(':visible')) {
                            $("#profile").hide();
                        } else {
                            $("#home").show();
                            $("#profile").hide();
                        }
                    } else if (idElemento == "#profile-pasajero") {
                        idElemento = "#profile";
                        if ($(idElemento).is(':visible')) {
                            $("#home").hide();
                        } else {
                            $("#profile").show();
                            $("#home").hide();
                        }
                    }
                }
            });

            $(".tab-mobile").on("click", function (e) {
                var idElemento = $(this).attr("href");
                if (idElemento == "#home-pasajero") {
                    idElemento = "#home";
                    if ($(idElemento).is(':visible')) {
                        $("#profile").hide();
                    } else {
                        $("#home").show();
                        $("#profile").hide();
                    }
                } else if (idElemento == "#profile-pasajero") {
                    idElemento = "#profile";
                    if ($(idElemento).is(':visible')) {
                        $("#home").hide();
                    } else {
                        $("#profile").show();
                        $("#home").hide();
                    }
                }

            });

            $('#<%= btnrefreshuser.ClientID%>').hide();

            $(".searchruc").hide();
            function MostrarError(objeto, texto) {
                var padre = objeto.parent();
                padre.children('.ErrorRequiere').remove();
                padre.children('br').remove();
                padre.append('<br/><span class="ErrorRequiere">' + texto + '</span>');
            }

            $(".savepasajero").click(function (e) {

                cont = 0;
                var TipoDocumento = $(this).parents(".contenido-pasajero").find(".tipodoc option:selected").attr('value');
                var NumeroDocumento = $(this).parents(".contenido-pasajero").find(".doc").val();


                //Validar Tipo Documento

                ValidarLista("#" + $(this).parents(".contenido-pasajero").find(".tipodoc").attr("id"), "Seleccionar un tipo de documento");


                //Validar Numero de Documento
                if (TipoDocumento == "1") {
                    console.log(1);
                    if (NumeroDocumento.length != 8 || NumeroDocumento.match('^[0-9]+$') == null) {
                        MostrarError($(this).parents(".contenido-pasajero").find(".doc"), "Se requiere un número de documento valido")
                        cont = +1;
                    }
                } else if (TipoDocumento == "4" || TipoDocumento == "7") {
                    console.log(3);
                    if ((NumeroDocumento.length < 8 || NumeroDocumento.length > 16) || NumeroDocumento.match('^[ a-zA-ZñÑ0-9áéíóúÁÉÍÓÚ]+$') == null) {
                        console.log(4);
                        MostrarError($(this).parents(".contenido-pasajero").find(".doc"), "Se requiere un número de documento valido")
                        cont = +1;
                    }
                }

                //Validar Nombres
                ValidarCaja("#" + $(this).parents(".contenido-pasajero").find(".nombre").attr("id"), exprregnombres, messagenombres, true, true, 100)

                //Validar Apellido Paterno
                ValidarCaja("#" + $(this).parents(".contenido-pasajero").find(".apepaterno").attr("id"), exprregnombres, messagenombres, true, true, 100)

                //Validar Apellido Materno
                ValidarCaja("#" + $(this).parents(".contenido-pasajero").find(".apematerno").attr("id"), exprregnombres, messagenombres, false, true, 100)

                //Validar Sexo
                ValidarLista("#" + $(this).parents(".contenido-pasajero").find(".sexo").attr("id"), "Seleccionar sexo");

                //Validar Edad
                ValidarCaja("#" + $(this).parents(".contenido-pasajero").find(".edad").attr("id"), exprregnumeros, messagenumeros, true, true, 3)

                //Validar Telefono
                ValidarCaja("#" + $(this).parents(".contenido-pasajero").find(".telefono").attr("id"), exprregnumeros, messagenumeros, false, true, 15)

                //Validar Embarque
                ValidarLista("#" + $(this).parents(".contenido-pasajero").find(".ddlpuntoembarque").attr("id"), "Seleccionar un embarque");

                //Validar Desembarque
                ValidarLista("#" + $(this).parents(".contenido-pasajero").find(".ddlpuntodesembarque").attr("id"), "Seleccionar un desembarque");




                var RucEmpresa = $(this).parents(".contenido-pasajero").find(".ruc");

                if (RucEmpresa.val() != null && RucEmpresa.val() != "") {
                    //Validar R.U.C.
                    ValidarCaja("#" + $(this).parents(".contenido-pasajero").find(".ruc").attr("id"), exprregruc, messageruc, true, true, 11)

                    //Validar Razon Social
                    ValidarCaja("#" + $(this).parents(".contenido-pasajero").find(".razonsocial").attr("id"), exprregrz, messagerz, true, true, 100)

                    //Validar Direccion
                    ValidarCaja("#" + $(this).parents(".contenido-pasajero").find(".direccion").attr("id"), exprredireccion, messagedireccion, true, true, 150)
                } else {
                    $(this).parents(".contenido-pasajero").find(".razonsocial").val("");
                    $(this).parents(".contenido-pasajero").find(".direccion").val("");
                }
                if (cont > 0) { return false; }
            });

            $(".searchpasajero").click(function (e) {

                cont = 0;
                var TipoDocumento = $(this).parents(".contenido-pasajero").find(".tipodoc option:selected").attr('value');
                var NumeroDocumento = $(this).parents(".contenido-pasajero").find(".doc").val();


                //Validar Tipo Documento

                ValidarLista("#" + $(this).parents(".contenido-pasajero").find(".tipodoc").attr("id"), "Seleccionar un tipo de documento");


                //Validar Numero de Documento
                if (TipoDocumento == "1") {
                    console.log(1);
                    if (NumeroDocumento.length != 8 || NumeroDocumento.match('^[0-9]+$') == null) {
                        MostrarError($(this).parents(".contenido-pasajero").find(".doc"), "Se requiere un número de documento valido")
                        cont = +1;
                    }
                } else if (TipoDocumento == "4" || TipoDocumento == "7") {
                    console.log(3);
                    if ((NumeroDocumento.length < 8 || NumeroDocumento.length > 16) || NumeroDocumento.match('^[ a-zA-ZñÑ0-9áéíóúÁÉÍÓÚ]+$') == null) {
                        console.log(4);
                        MostrarError($(this).parents(".contenido-pasajero").find(".doc"), "Se requiere un número de documento valido")
                        cont = +1;
                    }
                }

                if (cont > 0) { return false; }
            });

            //Buscar Pasajero
            $(".doc").on("keyup", function () {
                var numeroDocumento = $(this).val();
                if (numeroDocumento.length >= 8) {
                    var tipoDocumento = $(this).parents(".contenido-pasajero").find(".tipodoc").val();
                    if (tipoDocumento == 4 || tipoDocumento == 7) {
                        if (numeroDocumento.length >= 8 && numeroDocumento.match('^[A-Za-zñÑ0-9]+$') != null) {
                            $(this).parents(".contenido-pasajero").find(".searchpasajero").trigger("click");
                            $(this).focus();
                        }
                    } else if (tipoDocumento == 1) {
                        if (numeroDocumento.length == 8 && numeroDocumento.match('^[0-9]+$') != null) {
                            $(this).parents(".contenido-pasajero").find(".searchpasajero").trigger("click");
                            $(this).focus();
                        }
                    }
                }

            });

            //Buscar Empresa
            $(".ruc").on("keyup", function () {
                if ($(this).val().length == 11) {
                    $(this).parents(".contenido-pasajero").find(".searchruc").trigger("click");
                }
            });

            $('.tabsindex').bind('keypress', function (eInner) {

                if (eInner.keyCode == 13) {
                    var tabindex = $(this).attr('tabindex');
                    tabindex++;
                    if (tabindex == 3) {
                        var obj = $('[tabindex=' + tabindex + ']');

                        obj.click();
                        return false;
                    }
                    else if (tabindex == 13) {
                        var obj = $('[tabindex=' + tabindex + ']');
                        obj.click();
                        return false;
                    }
                    else {

                        var id = $('[tabindex=' + tabindex + ']').attr('id')
                        $('#' + id).focus();
                        return false;
                    }
                }

            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContPWeb" runat="Server">

    <script src="js/jquery.js" type="text/javascript"></script>

    <script src="js/Utiles.js" type="text/javascript"></script>

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
                <li><a href="#" class="active">Pasajeros</a></li>
                <li><a href="#">Confirmación</a></li>
                <li><a href="#">Resultados</a></li>
            </ul>
        </div>
    </div>
    <div class="container">
        <h4 class="title-page">
            <span class="imgsprite"></span>
            <label>
                Registro de pasajeros
            </label>
        </h4>
        <asp:UpdatePanel ID="UpdatePanelWebPasajeros" runat="server">
            <ContentTemplate>
                <div class="contenido resultados" name="registro" id="registro">
                    <div class="col-md-12  top-titulo1">
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4 cont-titulo1">
                        <div class="img-left-t2">
                        </div>
                        <div class="contTitulo1">
                            <h4>Detalle de Itinerario</h4>
                        </div>
                        <div class="imgsprite img-right-t2">
                        </div>
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="cl">
                    </div>
                    <div id="contIda">
                        <div class="centrado" style="width: 500px; margin: auto;" id="lbltitlehorarioida"
                            runat="server">
                            <div class="form-group">
                                <div class="imgsprite img-busida" style="">
                                </div>
                                Itinerario de Ida: <span class="tituloverde">
                                    <asp:Label ID="lblTitIda" runat="server" Text=""></asp:Label></span>
                            </div>
                        </div>
                        <div class="col-md-12" id="lbldatosida" runat="server">
                            <table cellspacing="0" class="tblVerde" border="1">
                                <tbody>
                                    <tr class="title-fileds">
                                        <th>Ruta
                                        </th>
                                        <th>Servicio
                                        </th>
                                        <th>Fecha Viaje
                                        </th>
                                        <th>Hora Salida
                                        </th>
                                        <th>Hora Llegada
                                        </th>
                                        <th scope="col">Monto (S/)
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
                        <div id="dataitinerarioretornotitle" runat="server" class="centrado" style="width: 500px; margin: auto;">
                            <div class="form-group">
                                <div class="imgsprite img-busretorno">
                                </div>
                                Itinerario de Retorno: <span class="tituloverde">
                                    <asp:Label ID="lblTitRetorno" runat="server" Text=""></asp:Label></span>
                            </div>
                        </div>
                        <div class="col-md-12" id="lbldatosretorno" runat="server">
                            <table id="dataitinerarioretornohead" runat="server" cellspacing="0" class="tblVerde"
                                border="1">
                                <tbody>
                                    <tr class="title-fileds">
                                        <th>Ruta
                                        </th>
                                        <th>Servicio
                                        </th>
                                        <th>Fecha Viaje
                                        </th>
                                        <th>Hora Salida
                                        </th>
                                        <th>Hora Llegada
                                        </th>
                                        <th scope="col">Monto (S/)
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
                        <h4>Detalle Itinerario</h4>
                        <ul class="nav nav-tabs  tab-desktop-mobile" role="tablist">
                            <li role="presentation" class="active"><a href="#home-pasajero" aria-controls="home" role="tab"
                                data-toggle="tab" class="tab-mobile">
                                <asp:Label ID="lblrutaidamobile" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:Label ID="lblfechaviajeidamobile" runat="server" Text=""></asp:Label>
                            </a></li>
                            <li id="lbldatosretornomobile" runat="server" role="presentation"><a href="#profile-pasajero"
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
                                            <th>Servicio
                                            </th>
                                            <th>Hora Salida
                                            </th>
                                            <th class="item-tablet">Hora Llegada
                                            </th>
                                            <th scope="col">Monto (S/)
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
                                            <th>Servicio
                                            </th>
                                            <th>Hora Salida
                                            </th>
                                            <th class="item-tablet">Hora Llegada
                                            </th>
                                            <th scope="col">Monto (S/)
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
                    <i class="imgsprite icon-aviso"></i>
                    <p>
                        <strong>Registro de pasajero(s)</strong> Ingrese los datos del pasajero, luego presionar
                        el botón Guardar Datos Luego de registrar a todos los pasajeros, presione el boton
                        continuar
                        <br />
                        <strong>Esta tienda está autorizada por
                            <img src="images/logo_visa_multitarjeta.png" style="width: 60px; margin: 0 3px 0 3px;">
                            para realizar
                            transacciones electrónicas.</strong>
                    </p>
                </div>

                <% Else%>

                <div class="contenido info">
                    <i class="imgsprite icon-aviso"></i>
                    <p>
                        <strong>Registro de pasajero(s)</strong> Ingrese los datos del pasajero, luego presionar
                        el botón Guardar Datos Luego de registrar a todos los pasajeros, presione el boton
                        continuar
                        <br />
                        <strong>Esta tienda está autorizada
                            <img src="images/logo_visa.png" style="width: 60px; margin: 0 3px 0 3px;">para realizar
                            transacciones electrónicas.</strong>
                    </p>
                </div>

                <%End If%>

                <asp:HiddenField ID="FlagTabX" runat="server" Value="0" />
                <div id="registro-pasajero">
                    <ul class="nav nav-tabs  tab-desktop" role="tablist">
                        <li role="presentation" class="active"><a href="#home-pasajero" aria-controls="home-pasajero"
                            role="tab" data-toggle="tab">
                            <div class="circulo">
                                1
                            </div>
                            &nbsp;&nbsp;Registro de pasajeros de ida</a> </li>
                        <li id="travelretorno" runat="server" role="presentation"><a href="#profile-pasajero" aria-controls="profile-pasajero" aria-expanded="false" role="tab" data-toggle="tab">
                            <div class="circulo">
                                2
                            </div>
                            &nbsp;&nbsp;Registro de pasajeros de retorno</a> </li>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content seleccion1">
                        <div role="tabpanel" class="tab-pane active" id="home-pasajero">
                            <div class="centrado" style="width: 500px; margin: auto;">
                                <div class="form-group">
                                    <div class="imgsprite img-busida" style="">
                                    </div>
                                    Registro de Ida
                                </div>
                            </div>
                            <%--INICIO DE DATALIST DE PASAJERO DE IDA--%>
                            <asp:DataList ID="dlpasajerosida" runat="server" Width="100%">
                                <ItemTemplate>
                                    <div class="contenido-pasajero">
                                        <div class="cont-datos-pasajeros">
                                            <div class="cont-asiento">
                                                <div class="asiento">
                                                    <asp:Label ID="lblnumeasiento" runat="server" Text='<%# Eval("Nume_Asiento") %>'></asp:Label>
                                                    <asp:Label ID="lblIDS" runat="server" Text='<%# Eval("IDS") %>' Visible="False"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="cont-registro">
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Documento</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:DropDownList ID="ddltipodocumento" runat="server" TabIndex="1" CssClass="tabsindex tipodoc"
                                                            AppendDataBoundItems="True" DataTextField="NOM_TIP" DataValueField="COD_TIP">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="tipodoc" runat="server" Text='<%# Eval("Tipo_Doc") %>' Visible="False"
                                                            Enabled="False"></asp:Label>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>N° Documento</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtnumdocumento" runat="server" EnableTheming="True" CssClass="tabsindex doc"
                                                            TabIndex="2" Text='<%# Eval("DNI") %>' MaxLength="15"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Nombres</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtnombres" runat="server" Text='<%# Eval("Nombre") %>' TabIndex="3"
                                                            CssClass="tabsindex nombre"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Apellido Paterno</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtapaterno" runat="server" Text='<%# Eval("ApePaterno") %>' TabIndex="4"
                                                            CssClass="tabsindex apepaterno"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Apellido Materno</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtamaterno" runat="server" Text='<%# Eval("ApeMaterno") %>' TabIndex="5"
                                                            CssClass="tabsindex apematerno"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Sexo</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:DropDownList ID="ddlsexo" runat="server" TabIndex="6" CssClass="tabsindex sexo" SelectedValue='<%# Eval("Sexo") %>'>
                                                            <asp:ListItem Selected="True" Value="X">Seleccione Sexo</asp:ListItem>
                                                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                            <asp:ListItem Value="F">Femenino</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Edad</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtedad" runat="server" Text='<%# Eval("Edad") %>' TabIndex="7"
                                                            MaxLength="2" CssClass="tabsindex edad"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Teléfono</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txttelefono" runat="server" Text='<%# Eval("Telefono") %>' TabIndex="8"
                                                            MaxLength="13" CssClass="tabsindex telefono"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Lugar Embarque</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:DropDownList ID="ddlpuntoembarque" runat="server" DataValueField="Codi_PuntoVenta"
                                                            DataTextField="PuntoVenta" TabIndex="9" CssClass="tabsindex ddlpuntoembarque"
                                                            OnSelectedIndexChanged="ddmanu_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblpuntoembarque" runat="server" Text='<%# Eval("Punto_Embarque") %>'
                                                            Visible="False" Enabled="False"></asp:Label>
                                                        <asp:DropDownList ID="ddlhoraembarque" runat="server" DataValueField="Codi_PuntoVenta"
                                                            DataTextField="HoraPaso" Enabled="true" Visible="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Lugar Desembarque</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:DropDownList ID="ddlpuntodesembarque" runat="server" DataValueField="Codi_PuntoVenta"
                                                            DataTextField="PuntoVenta" TabIndex="10" CssClass="tabsindex ddlpuntodesembarque">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblpuntodesembarque" runat="server" Text='<%# Eval("Punto_Desembarque") %>'
                                                            Visible="False"></asp:Label>
                                                    </div>
                                                </article>
                                            </div>
                                        </div>
                                        <div class="cont-datos-empresa">
                                            <div class="message-empresa">
                                                Si desea emitir factura para sustentar costos y gastos, ingresar No de Ruc
                                                y Razón Social
                                            </div>
                                            <div class="cont-empresa">
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>RUC</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtruc" runat="server" TabIndex="11" Text='<%# Eval("NIT_Cliente") %>'
                                                            MaxLength="11" CssClass="tabsindex ruc"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Razon Social</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtrz" runat="server" TabIndex="12" Text='<%# Eval("Razon_Social") %>'
                                                            MaxLength="70" CssClass="tabsindex razonsocial"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Dirección</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtrzdireccion" runat="server" TabIndex="13" Text='<%# Eval("Rz_Direccion")%>'
                                                            MaxLength="100" CssClass="tabsindex direccion"></asp:TextBox>
                                                    </div>
                                                </article>
                                            </div>
                                        </div>
                                        <div class="lista-botones">
                                            <asp:Button ID="btnguardar" runat="server" Text="Guardar Datos" CssClass="boton-pasajero savepasajero"
                                                TabIndex="14" CommandName="Accion" />
                                            <asp:Button ID="btnbuscar" runat="server" Text="Buscar Pasajero" CssClass="boton-pasajero searchpasajero"
                                                TabIndex="15" CommandName="Search" />
                                            <asp:Button ID="btneliminar" runat="server" Text="Eliminar Pasajero" CssClass="boton-pasajero"
                                                CommandName="Delete" />
                                            <asp:Button ID="btnbuscaruc" runat="server" CommandName="Empresa" CssClass="boton-pasajero searchruc"
                                                Text="Buscar Empresa" />
                                        </div>
                                        <div class="cl">
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                            <%--FINAL DE DATALIST DE PASAJERO DE IDA--%>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="profile-pasajero">
                            <div class="centrado" style="width: 500px; margin: auto;">
                                <div class="form-group">
                                    <div class="imgsprite img-busretorno" style="float: left;">
                                    </div>
                                    Registro de retorno
                                </div>
                            </div>
                            <%--INICIO DE DATALIST DE PASAJERO DE RETORNO--%>
                            <asp:DataList ID="dlpasajerosretorno" runat="server" Width="100%">
                                <ItemTemplate>
                                    <div class="contenido-pasajero">
                                        <div class="cont-datos-pasajeros">
                                            <div class="cont-asiento">
                                                <div class="asiento">
                                                    <asp:Label ID="lblnumeasiento" runat="server" Text='<%# Eval("Nume_Asiento") %>'></asp:Label>
                                                    <asp:Label ID="lblIDS" runat="server" Text='<%# Eval("IDS") %>' Visible="False"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="cont-registro">
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Documento</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:DropDownList ID="ddltipodocumento" runat="server" TabIndex="1" CssClass="tabsindex tipodoc"
                                                            AppendDataBoundItems="True" DataTextField="NOM_TIP" DataValueField="COD_TIP">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="tipodoc" runat="server" Text='<%# Eval("Tipo_Doc") %>' Visible="False"
                                                            Enabled="False"></asp:Label>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>N° Documento</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtnumdocumento" runat="server" EnableTheming="True" CssClass="tabsindex doc"
                                                            TabIndex="2" Text='<%# Eval("DNI") %>' MaxLength="15"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Nombres</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtnombres" runat="server" Text='<%# Eval("Nombre") %>' TabIndex="3"
                                                            CssClass="tabsindex nombre"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Apellido Paterno</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtapaterno" runat="server" Text='<%# Eval("ApePaterno") %>' TabIndex="4"
                                                            CssClass="tabsindex apepaterno"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Apellido Materno</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtamaterno" runat="server" Text='<%# Eval("ApeMaterno") %>' TabIndex="5"
                                                            CssClass="tabsindex apematerno"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Sexo</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:DropDownList ID="ddlsexo" runat="server" TabIndex="6" CssClass="tabsindex sexo" SelectedValue='<%# Eval("Sexo") %>'>
                                                            <asp:ListItem Selected="True" Value="X">Seleccione Sexo</asp:ListItem>
                                                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                            <asp:ListItem Value="F">Femenino</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Edad</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtedad" runat="server" Text='<%# Eval("Edad") %>' TabIndex="7"
                                                            MaxLength="2" CssClass="tabsindex edad"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Teléfono</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txttelefono" runat="server" Text='<%# Eval("Telefono") %>' TabIndex="8"
                                                            MaxLength="13" CssClass="tabsindex telefono"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Lugar Embarque</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:DropDownList ID="ddlpuntoembarque" runat="server" DataValueField="Codi_PuntoVenta"
                                                            DataTextField="PuntoVenta" TabIndex="9" CssClass="tabsindex ddlpuntoembarque"
                                                            OnSelectedIndexChanged="ddmanu_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblpuntoembarque" runat="server" Text='<%# Eval("Punto_Embarque") %>'
                                                            Visible="False" Enabled="False"></asp:Label>
                                                        <asp:DropDownList ID="ddlhoraembarque" runat="server" DataValueField="Codi_PuntoVenta"
                                                            DataTextField="HoraPaso" Enabled="true" Visible="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Lugar Desembarque</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:DropDownList ID="ddlpuntodesembarque" runat="server" DataValueField="Codi_PuntoVenta"
                                                            DataTextField="PuntoVenta" TabIndex="10" CssClass="tabsindex ddlpuntodesembarque">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblpuntodesembarque" runat="server" Text='<%# Eval("Punto_Desembarque") %>'
                                                            Visible="False"></asp:Label>
                                                    </div>
                                                </article>
                                            </div>
                                        </div>
                                        <div class="cont-datos-empresa">
                                            <div class="message-empresa">
                                                Si desea emitir factura para sustentar costos y gastos, ingresar No de Ruc
                                                y Razón Social
                                            </div>
                                            <div class="cont-empresa">
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>RUC</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtruc" runat="server" TabIndex="11" Text='<%# Eval("NIT_Cliente") %>'
                                                            MaxLength="11" CssClass="tabsindex ruc"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Razon Social</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtrz" runat="server" TabIndex="12" Text='<%# Eval("Razon_Social") %>'
                                                            MaxLength="70" CssClass="tabsindex razonsocial"></asp:TextBox>
                                                    </div>
                                                </article>
                                                <article class="item-dato">
                                                    <div class="title">
                                                        <label>Dirección</label>
                                                    </div>
                                                    <div class="dato">
                                                        <asp:TextBox ID="txtrzdireccion" runat="server" TabIndex="13" Text='<%# Eval("Rz_Direccion") %>'
                                                            MaxLength="100" CssClass="tabsindex direccion"></asp:TextBox>
                                                    </div>
                                                </article>
                                            </div>
                                        </div>
                                        <div class="lista-botones">
                                            <asp:Button ID="btnguardar" runat="server" Text="Guardar Datos" CssClass="boton-pasajero savepasajero"
                                                TabIndex="14" CommandName="Accion" />
                                            <asp:Button ID="btnbuscar" runat="server" Text="Buscar Pasajero" CssClass="boton-pasajero searchpasajero"
                                                TabIndex="15" CommandName="Search" />
                                            <asp:Button ID="btneliminar" runat="server" Text="Eliminar Pasajero" CssClass="boton-pasajero"
                                                CommandName="Delete" />
                                            <asp:Button ID="btnbuscaruc" runat="server" CommandName="Empresa" CssClass="boton-pasajero searchruc"
                                                Text="Buscar Empresa" />
                                        </div>
                                        <div class="cl">
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                            <%--FINAL DE DATALIST DE PASAJERO DE RETORNO--%>
                        </div>
                        <div class="correo-cliente">
                            Ingrese el Email donde desea que le enviemos la confirmación de compra.
                            <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rvemail_comprador" runat="server" ControlToValidate="txtCorreo"
                                ErrorMessage="Se requiere el correo de confirmación de compra" ValidationGroup="emailcliente"
                                Font-Bold="True"></asp:RequiredFieldValidator><br />
                            <%--                            <asp:RegularExpressionValidator ID="regemail_comprador" runat="server" ControlToValidate="txtCorreo"
                                ErrorMessage="Formato de correo invalido" ValidationGroup="emailcliente" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                Font-Bold="True"></asp:RegularExpressionValidator>--%>
                        </div>
                    </div>
                    <div class="cl">
                    </div>
                    <div class="contBoton">
                        <asp:Button ID="btnregresar" runat="server" Text="Regresar" CssClass="btnRegresar" />
                        <asp:Button ID="btncontinuar" runat="server" Text="Continuar" CssClass="btnSiguiente"
                            ValidationGroup="emailcliente" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:Button ID="btnrefreshuser" runat="server" Width="1px" />
    <input id="emisionallida" type="hidden" runat="server" />
    <input id="emisionallretorno" type="hidden" runat="server" />
    <input id="embarqueallida" type="hidden" runat="server" />
    <input id="embarqueallretorno" type="hidden" runat="server" />
    <input id="emisionidaselect" type="hidden" runat="server" />
    <input id="emisionretornoselect" type="hidden" runat="server" />
    <input id="embarqueidaselect" type="hidden" runat="server" />
    <input id="embarqueretornoselect" type="hidden" runat="server" />
</asp:Content>
