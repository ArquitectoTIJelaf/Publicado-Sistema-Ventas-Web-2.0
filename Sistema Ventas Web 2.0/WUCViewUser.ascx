<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WUCViewUser.ascx.vb"
    Inherits="WUCViewUser" %>
<%@ Import Namespace="PUtilitario" %>

<script type="text/javascript">
    $(document).ready(function() {
        $('.login').click(function() {
            popUp('Login.aspx', 350, 360, 'iframe')
            return false;
        })
        $('.register').click(function() {
            popUp('registrousuario.aspx', 350, 360, 'iframe')
            return false;
        })
        /*  $("ul.subnav").parent().append("<span></span>"); //Only shows drop down trigger when js is enabled (Adds empty span tag after ul.subnav*)
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
        */

    });
</script>
 <header>
        <div class="topnavbar">
            <div class="container">
                <div class="col-md-3"></div>
                <div class="col-md-12"></div>
            </div>
        </div>
        <div class="navbar">
            <div class="container">
                <div class="col-md-5 col-sm-5 col-xs-12">
                    <a href="#" class="imgsprite icono-menu-mobile"></a>               
                    <div class="cont-logo">
                        <img src="<%= AppSettings.valueString("RutaLocalLogo")%>">
                    </div>
                </div>
                
                <div id="loguot" runat="server" class="col-md-4 col-sm-4">
                    <ul class="menu-sesion">
                        <li><div class="img-user"></div></li>
                        <li><a href="#" class="register">Registrarse</a></li>
                        <li><a href="#"class="login">Iniciar Sesión</a></li>
                    </ul>
                </div> 
                
                <div id="viewlogueo" runat="server" class="col-md-4  col-sm-4">
                    <ul class="menu-sesion">
                        <li>
                            <div class="img-user"></div>
                            <asp:Label ID="lbusers" runat="server"></asp:Label>
                        </li>
                        <li class="dropdown"> 
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" 
                            data-hover="dropdown" data-delay="0" data-close-others="false">
                                Opciones <b class=" icon-angle-down"></b>
                           </a>
                            <ul class="dropdown-menu">
                                <li><a href="PerfilUser.aspx">Perfil Usuario</a> </li>
                                <!--- li><a href="ventas.aspx">Consulta de Compras</a> </li -->
                                <li id="reservas" runat="server"><a href="reservas.aspx">Consulta de Reservas</a>
                                </li>
                                <li id="mediopago" runat="server"><a href="mediospago.aspx">Medios de Pagos</a>
                                </li>
                                <li id="timereservas" runat="server"><a href="configtimereservas.aspx">Mecanismos de
                                    Reservas</a> </li>
                                <li><a href="changepassword.aspx">Cambio de Contraseña</a> </li>
                                <li><a href="destinos.aspx">Comprar Pasajes</a> </li>
                            </ul>
                        </li>
                        <li><a href="end.aspx">Cerrar sesión</a></li>
                    </ul>
                </div>
                
                <div id="networking" class="col-md-3  col-sm-3">
                    <ul id="redes">
                        <li><span>Síguenos:</span></li>
                        <li><a href="#"><div class="imgsprite img-fc"></div></a></li>
                        <li><a href="#"><div class="imgsprite img-tw"></div></a></li>
                        <li><a href="#"><div class="imgsprite img-yo"></div></a></li>
                    </ul>
                </div>
            </div>
        </div>
    </header>
    
 <%--     Validar que muestre el menu cuando inicie sesion
<div id="viewlogueo" runat="server" class="user">
    <img src="images/icon-user.png" alt="" />
    <asp:Label ID="lbusers" runat="server"></asp:Label>
    &nbsp;
    <div class="menu">
        <ul class="right topnav">
            <li><a href="#">Opciones</a>
                <ul class="subnav">
                    <li><a href="PerfilUser.aspx">Perfil Usuario</a> </li>
                    <li><a href="ventas.aspx">Consulta de Compras</a> </li>
                    <li id="reservas" runat="server"><a href="reservas.aspx">Consulta de Reservas</a>
                    </li>
                    <li id="mediopago" runat="server"><a href="mediospago.aspx">Medios de Pagos</a>
                    </li>
                    <li id="timereservas" runat="server"><a href="configtimereservas.aspx">Mecanismos de
                        Reservas</a> </li>
                    <li><a href="changepassword.aspx">Cambio de Contraseña</a> </li>
                    <li><a href="destinos.aspx">Comprar Pasajes</a> </li>
                </ul>
            </li>
            <li>|</li>
            <li><a href="end.aspx">Cerrar sesión</a></li>
        </ul>
    </div>
</div>

<div id="loguot" runat="server" class="menu inisesion">
    <ul class="right topnav">
        <li><a href="javascript:void(0);" class="register">Registrarse</a> </li>
        <li>|</li>
        <li><a href="javascript:void(0);" class="login">Iniciar sesión</a></li>
    </ul>
</div>--%>
