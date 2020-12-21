Imports System.Web
Imports System.Web.SessionState
Imports PEntity
Imports System.Collections.Generic
Public Class SessionManager

    Public Shared ReadOnly Property Session() As HttpSessionState
        Get
            Return HttpContext.Current.Session
        End Get
    End Property


#Region "Atributos"

    Const _Reservacion As String = "Reservacion"
    Const _ViajeRetorno As String = "ViajeRetorno"
    Const _SelectDestinos As String = "SelectDestinos"
    Const _SelectDestinosIda As String = "SelectDestinosIda"
    Const _SelectDestinosRetorno As String = "SelectDestinosRetorno"
    Const _CantidadAsientos As String = "CantidadAsientos"
    Const _CantidadAsientosIda As String = "CantidadAsientosIda"
    Const _CantidadAsientosRetorno As String = "CantidadAsientosRetorno"
    Const _MountOrder As String = "MountOrder"
    Const _MountVisa As String = "MountVisa"
    Const _MontoIda As String = "MontoIda"
    Const _MontoRetorno As String = "MontoRetorno"
    Const _IsShoViable As String = "IsShoViable"
    Const _TimeOut As String = "TimeOut"
    Const _IDCID As String = "IDCID"
    Const _IdEmpresaIda As String = "IdEmpresaIda"
    Const _IdEmpresaRetorno As String = "IdEmpresaRetorno"
    Const _RUCEmpresaIda As String = "RUCEmpresaIda"
    Const _RUCEmpresaRetorno As String = "RUCEmpresaRetorno"
    Const _PerfilName As String = "PerfilName"
    Const _Destino_Ida As String = "Destino_ida"
    Const _Origen_Ida As String = "Origen_ida"
    Const _DestinoId_Ida As String = "DestinoId_ida"
    Const _OrigenId_Ida As String = "OrigenId_ida"
    Const _Codi_puntoVenta As String = "Codi_puntoVenta"
    Const _PuntoVenta As String = "PuntoVenta"
    Const _Codi_PuntoVentaIda As String = "Codi_PuntoVentaIda"
    Const _Codi_PuntoVentaRetorno As String = "Codi_PuntoVentaRetorno"
    Const _Destino_Retorno As String = "Destino_Retorno"
    Const _Origen_Retorno As String = "Origen_Retorno"
    Const _OrigenId_Retorno As String = "OrigenId_Retorno"
    Const _DestinoId_Retorno As String = "DestinoId_Retorno"
    Const _ServicioIda As String = "ServicioIda"
    Const _ServicioRetorno As String = "ServicioRetorno"
    Const _FechaDeViajeIda As String = "FechaDeViajeIda"
    Const _FechaDeViajeRetorno As String = "FechaDeViajeRetorno"
    Const _HoraViajeIda As String = "HoraViajeIda"
    Const _HoraViajeRetorno As String = "HoraViajeRetorno"
    Const _HoraLlegadaViajeIda As String = "HoraLlegadaViajeIda"
    Const _HoraLlegadaViajeRetorno As String = "HoraLlegadaViajeRetorno"
    Const _ListaHorarioProgramacionIda As String = "ListaHorarioProgramacionIda"
    Const _ListaHorarioProgramacionRetorno As String = "ListaHorarioProgramacionRetorno"
    Const _IdPrecioNacionalidadIda As String = "IdPrecioNacionalidadIda"
    Const _IdPrecioNacionalidadRetorno As String = "IdPrecioNacionalidadRetorno"
    Const _Nro_ViajeIda As String = "Nro_ViajeIda"
    Const _Nro_ViajeRetorno As String = "Nro_ViajeRetorno"
    Const _Codi_Sucursal_ida As String = "Codi_Sucursal_id"
    Const _Codi_Sucursal_Retorno As String = "Codi_Sucursal_Retorno"
    Const _Codi_Destino_Ida As String = "Codi_Destino_Ida"
    Const _Codi_Destino_Retorno As String = "Codi_Destino_Retorno"
    Const _Hora_Programacion_Ida As String = "Hora_Programacion_Ida"
    Const _Hora_Programacion_Retorno As String = "Hora_Programacion_Retorno"
    Const _Fecha_Programacion_Ida As String = "Fecha_Programacion_Ida"
    Const _Fecha_Programacion_Retorno As String = "Fecha_Programacion_Retorno"
    Const _Cod_ProgramacionIda As String = "Cod_ProgramacionIda"
    Const _Cod_ProgramacionRetorno As String = "Cod_ProgramacionRetorno"
    Const _ServicioIdIda As String = "ServicioIdIda"
    Const _ServicioIdRetorno As String = "ServicioIdRetorno"
    Const _PlanoIda As String = "PlanoIda"
    Const _PlanoRetorno As String = "PlanoRetorno"
    Const _IdBusIda As String = "IdBusIda"
    Const _IdBusRetorno As String = "IdBusRetorno"
    Const _NroPolizaBusIda As String = "NroPolizaBusIda"
    Const _NroPolizaBusRetorno As String = "NroPolizaBusRetorno"
    Const _ListaAsientoWebIda As String = "ListaAsientoWebIda"
    Const _ListaAsientoWebRetorno As String = "ListaAsientoWebRetorno"
    Const _ListarOficinasWebActivas As String = "ListarOficinasWebActivas"
    Const _SelectionRuta As String = "SelectionRuta"
    Const _SelectionAsientos As String = "SelectionAsientos"
    Const _SelectionPasajeros As String = "SelectionPasajeros"
    Const _SelectionPago As String = "SelectionPago"
    Const _SelectionTransaction As String = "SelectionTransaction"
    Const _SelectionOKTransaction As String = "SelectionTransaction"
    Const _ETicket As String = "ETicket"
    Const _ProveedorPasarela As String = "ProveedorPasarela"
    Const _FormaPagoWeb As String = "FormaPagoWeb"
    Const _IdVenta As String = "IdVenta"
	Const _OrdenVenta As String = "OrdenVenta"
    Const _AsientoAsignando As String = "AsientoAsignando"
    Const _ListaTipoDocumentos As String = "ListaTipoDocumentos"
    Const _ListaMonedas As String = "ListaMonedas"
    Const _ListaPaises As String = "ListaPaises"
    Const _ListaVentaweb As String = "ListaVentaweb"
    Const _ListaVentawebReservas As String = "ListaVentawebReservas"
    Const _ListaVentawebVentas As String = "ListaVentawebVentas"

    Const _ListaDetalleVentawebIda As String = "ListaDetalleVentawebIda"
    Const _ListaDetalleVentawebRetorno As String = "ListaDetalleVentawebRetorno"
    Const _ListaDetallePasajero As String = "ListaDetallePasajero"
    Const _ListaAsientosbyOrderIda As String = "ListaAsientosbyOrderIda"
    Const _ListaAsientosbyOrderRetorno As String = "ListaAsientosbyOrderRetorno"
    Const _ListaProgramacionIda As String = "ListaProgramacionIda"
    Const _ListaProgramacionRetorno As String = "ListaProgramacionRetorno"
    Const _RutaConcatenadaIda As String = "RutaConcatenadaIda"
    Const _RutaConcatenadaRetorno As String = "RutaConcatenadaRetorno"
    Const _RutaConcatenadaPasajero As String = "RutaConcatenadaPasajero"
    Const _IdUser As String = "IdUser"
    Const _IdPerfil As String = "IdPerfil"
    Const _Name As String = "Name"
    Const _NivelUser As String = "NivelUser"
    Const _Pws As String = "Pws"
    Const _Pasajero As String = "Pasajero"
    Const _Cliente As String = "Cliente"
    Const _Respuesta As String = "Respuesta"
    Const _Cod_Tienda As String = "Cod_Tienda"
    Const _NroOrdenTienda As String = "NroOrdenTienda"
    Const _FechaTransaccionVisa As String = "FechaTransaccionVisa"
    Const _TarjetaTransaccion As String = "TarjetaTransaccion"
    Const _Transaccion_Id As String = "Transaccion_Id"
    Const _NomTarjeta As String = "NomTarjeta"
    Const _Cod_Accion As String = "Cod_Accion"
    Const _Cod_Desc_Accion As String = "Cod_Desc_Accion"
    Const _isReservando As String = "isReservando"
    Const _isPagando As String = "isPagando"
    Const _IsProcesable As String = "IsProcesable"
    Const _Fviaje As String = "Fviaje"
    Const _NumOrder As String = "NumOrder"
    Const _Id_VentaWebIda As String = "Id_VentaWebIda"
    Const _Id_VentaWebRetorno As String = "Id_VentaWebRetorno"
    Const _Id_WebOrders As String = "Id_WebOrders"
    Const _Correo_Users As String = "Correo_Users"
    Const _FechaReg_Users As String = "FechaReg_Users"
    Const _ListaPreciosFechasIda As String = "ListaPreciosFechasIda"
    Const _ListaPreciosFechasRetorno As String = "ListaPreciosFechasRetorno"
    Const _Promociones As String = "Promociones"
    Const _TransactionPromociones As String = "TransactionPromociones"
    Const _ListarMediosPagosTCredito As String = "ListarMediosPagosTCredito"
    Const _ListarMediosPagos As String = "ListarMediosPagos"
    Const _ListaItinerariosIda As String = "ListaItinerariosIda"
    Const _ListaItinerariosRetorno As String = "ListaItinerariosRetorno"
    Const _VisaNet As String = "VisaNet"
    Const _CorreoCliente As String = "CorreoCliente"

#End Region

#Region "Metodos"

    Public Shared Sub DeleteListaHorarioProgramacionIda()
        Session.Remove(_ListaHorarioProgramacionIda)
    End Sub

    Public Shared Sub DeleteListaHorarioProgramacionRetorno()
        Session.Remove(_ListaHorarioProgramacionRetorno)
    End Sub

    Public Shared Sub DeleteListaPreciosFechasIda()
        Session.Remove(_ListaPreciosFechasIda)
    End Sub

    Public Shared Sub DeleteListaPreciosFechasRetorno()
        Session.Remove(_ListaPreciosFechasRetorno)
    End Sub

#End Region

#Region "Propiedades"



    Public Shared Property Reservacion() As Boolean
        Get
            Return CBool(Session(_Reservacion))
        End Get
        Set(ByVal value As Boolean)
            Session(_Reservacion) = value
        End Set
    End Property

    Public Shared Property ViajeRetorno() As Boolean
        Get
            Return CBool(Session(_ViajeRetorno))
        End Get
        Set(ByVal value As Boolean)
            Session(_ViajeRetorno) = value
        End Set
    End Property

    Public Shared Property SelectDestinos() As Boolean
        Get
            Return CBool(Session(_SelectDestinos))
        End Get
        Set(ByVal value As Boolean)
            Session(_SelectDestinos) = value
        End Set
    End Property

    Public Shared Property SelectDestinosIda() As Boolean
        Get
            Return CBool(Session(_SelectDestinosIda))
        End Get
        Set(ByVal value As Boolean)
            Session(_SelectDestinosIda) = value
        End Set
    End Property

    Public Shared Property SelectDestinosRetorno() As Boolean
        Get
            Return CBool(Session(_SelectDestinosRetorno))
        End Get
        Set(ByVal value As Boolean)
            Session(_SelectDestinosRetorno) = value
        End Set
    End Property

    Public Shared Property CantidadAsientos() As Int32
        Get
            Return CInt(Session(_CantidadAsientos))
        End Get
        Set(ByVal value As Int32)
            Session(_CantidadAsientos) = value
        End Set
    End Property

    Public Shared Property CantidadAsientosIda() As Int32
        Get
            Return CInt(Session(_CantidadAsientosIda))
        End Get
        Set(ByVal value As Int32)
            Session(_CantidadAsientosIda) = value
        End Set
    End Property

    Public Shared Property CantidadAsientosRetorno() As Int32
        Get
            Return CInt(Session(_CantidadAsientosRetorno))
        End Get
        Set(ByVal value As Int32)
            Session(_CantidadAsientosRetorno) = value
        End Set
    End Property

    Public Shared Property MountOrder() As String
        Get
            Return CStr(Session(_MountOrder))
        End Get
        Set(ByVal value As String)
            Session(_MountOrder) = value
        End Set
    End Property

    Public Shared Property MountVisa() As String
        Get
            Return CStr(Session(_MountVisa))
        End Get
        Set(ByVal value As String)
            Session(_MountVisa) = value
        End Set
    End Property



    Public Shared Property MontoIda() As Double
        Get
            Return CDbl(Session(_MontoIda))
        End Get
        Set(ByVal value As Double)
            Session(_MontoIda) = value
        End Set
    End Property

    Public Shared Property MontoRetorno() As Double
        Get
            Return CDbl(Session(_MontoRetorno))
        End Get
        Set(ByVal value As Double)
            Session(_MontoRetorno) = value
        End Set
    End Property

    Public Shared Property IsShoViable() As Int32
        Get
            Return CStr(Session(_IsShoViable))
        End Get
        Set(ByVal value As Int32)
            Session(_IsShoViable) = value
        End Set
    End Property

    Public Shared Property TimeOut() As Integer
        Get
            Return CStr(Session(_TimeOut))
        End Get
        Set(ByVal value As Integer)
            Session(_TimeOut) = value
        End Set
    End Property

    Public Shared Property IDCID() As String
        Get
            Return CStr(Session(_IDCID))
        End Get
        Set(ByVal value As String)
            Session(_IDCID) = value
        End Set
    End Property

    Public Shared Property IdEmpresaIda() As Byte
        Get
            Return CByte(Session(_IdEmpresaIda))
        End Get
        Set(ByVal value As Byte)
            Session(_IdEmpresaIda) = value
        End Set
    End Property

    Public Shared Property IdEmpresaRetorno() As Byte
        Get
            Return CByte(Session(_IdEmpresaRetorno))
        End Get
        Set(ByVal value As Byte)
            Session(_IdEmpresaRetorno) = value
        End Set
    End Property

    Public Shared Property RUCEmpresaIda() As String
        Get
            Return CStr(Session(_RUCEmpresaIda))
        End Get
        Set(ByVal value As String)
            Session(_RUCEmpresaIda) = value
        End Set
    End Property

    Public Shared Property RUCEmpresaRetorno() As String
        Get
            Return CStr(Session(_RUCEmpresaRetorno))
        End Get
        Set(ByVal value As String)
            Session(_RUCEmpresaRetorno) = value
        End Set
    End Property

    Public Shared Property PerfilName() As String
        Get
            Return CStr(Session(_PerfilName))
        End Get
        Set(ByVal value As String)
            Session(_PerfilName) = value
        End Set
    End Property

    Public Shared Property Destino_Ida() As String
        Get
            Return CStr(Session(_Destino_Ida))
        End Get
        Set(ByVal value As String)
            Session(_Destino_Ida) = value
        End Set
    End Property

    Public Shared Property Origen_Ida() As String
        Get
            Return CStr(Session(_Origen_Ida))
        End Get
        Set(ByVal value As String)
            Session(_Origen_Ida) = value
        End Set
    End Property

    Public Shared Property DestinoId_Ida() As Int16
        Get
            Return CShort(Session(_DestinoId_Ida))
        End Get
        Set(ByVal value As Int16)
            Session(_DestinoId_Ida) = value
        End Set
    End Property

    Public Shared Property OrigenId_Ida() As Int16
        Get
            Return CShort(Session(_OrigenId_Ida))
        End Get
        Set(ByVal value As Int16)
            Session(_OrigenId_Ida) = value
        End Set
    End Property


    Public Shared Property Codi_puntoVenta() As Int32
        Get
            Return CStr(Session(_Codi_puntoVenta))
        End Get
        Set(ByVal value As Int32)
            Session(_Codi_puntoVenta) = value
        End Set
    End Property
    Public Shared Property PuntoVenta() As Int32
        Get
            Return CStr(Session(_PuntoVenta))
        End Get
        Set(ByVal value As Int32)
            Session(_PuntoVenta) = value
        End Set
    End Property

    Public Shared Property Codi_PuntoVentaIda() As Int32
        Get
            Return CStr(Session(_Codi_PuntoVentaIda))
        End Get
        Set(ByVal value As Int32)
            Session(_Codi_PuntoVentaIda) = value
        End Set
    End Property

    Public Shared Property Codi_PuntoVentaRetorno() As Int32
        Get
            Return CStr(Session(_Codi_PuntoVentaRetorno))
        End Get
        Set(ByVal value As Int32)
            Session(_Codi_PuntoVentaRetorno) = value
        End Set
    End Property

    Public Shared Property Destino_Retorno() As String
        Get
            Return CStr(Session(_Destino_Retorno))
        End Get
        Set(ByVal value As String)
            Session(_Destino_Retorno) = value
        End Set
    End Property

    Public Shared Property Origen_Retorno() As String
        Get
            Return CStr(Session(_Origen_Retorno))
        End Get
        Set(ByVal value As String)
            Session(_Origen_Retorno) = value
        End Set
    End Property

    Public Shared Property OrigenId_Retorno() As Int16
        Get
            Return CShort(Session(_OrigenId_Retorno))
        End Get
        Set(ByVal value As Int16)
            Session(_OrigenId_Retorno) = value
        End Set
    End Property

    Public Shared Property DestinoId_Retorno() As Int16
        Get
            Return CShort(Session(_DestinoId_Retorno))
        End Get
        Set(ByVal value As Int16)
            Session(_DestinoId_Retorno) = value
        End Set
    End Property

    Public Shared Property ServicioIda() As String
        Get
            Return CStr(Session(_ServicioIda))
        End Get
        Set(ByVal value As String)
            Session(_ServicioIda) = value
        End Set
    End Property

    Public Shared Property ServicioRetorno() As String
        Get
            Return CStr(Session(_ServicioRetorno))
        End Get
        Set(ByVal value As String)
            Session(_ServicioRetorno) = value
        End Set
    End Property

    Public Shared Property FechaDeViajeIda() As String
        Get
            Return CStr(Session(_FechaDeViajeIda))
        End Get
        Set(ByVal value As String)
            Session(_FechaDeViajeIda) = value
        End Set
    End Property

    Public Shared Property FechaDeViajeRetorno() As String
        Get
            Return CStr(Session(_FechaDeViajeRetorno))
        End Get
        Set(ByVal value As String)
            Session(_FechaDeViajeRetorno) = value
        End Set
    End Property

    Public Shared Property HoraViajeIda() As String
        Get
            Return CStr(Session(_HoraViajeIda))
        End Get
        Set(ByVal value As String)
            Session(_HoraViajeIda) = value
        End Set
    End Property

    Public Shared Property HoraViajeRetorno() As String
        Get
            Return CStr(Session(_HoraViajeRetorno))
        End Get
        Set(ByVal value As String)
            Session(_HoraViajeRetorno) = value
        End Set
    End Property

    Public Shared Property HoraLlegadaViajeRetorno() As String
        Get
            Return CStr(Session(_HoraLlegadaViajeRetorno))
        End Get
        Set(ByVal value As String)
            Session(_HoraLlegadaViajeRetorno) = value
        End Set
    End Property

    Public Shared Property HoraLlegadaViajeIda() As String
        Get
            Return CStr(Session(_HoraLlegadaViajeIda))
        End Get
        Set(ByVal value As String)
            Session(_HoraLlegadaViajeIda) = value
        End Set
    End Property

    Public Shared Property ListaHorarioProgramacionIda() As ListaMaestroProgramacion
        Get
            Return CType(Session(_ListaHorarioProgramacionIda), ListaMaestroProgramacion)
        End Get
        Set(ByVal value As ListaMaestroProgramacion)
            Session(_ListaHorarioProgramacionIda) = value
        End Set
    End Property

    Public Shared Property ListaHorarioProgramacionRetorno() As ListaMaestroProgramacion
        Get
            Return CType(Session(_ListaHorarioProgramacionRetorno), ListaMaestroProgramacion)
        End Get
        Set(ByVal value As ListaMaestroProgramacion)
            Session(_ListaHorarioProgramacionRetorno) = value
        End Set
    End Property

    Public Shared Property IdPrecioNacionalidadRetorno() As Byte
        Get
            Return CByte(Session(_IdPrecioNacionalidadRetorno))
        End Get
        Set(ByVal value As Byte)
            Session(_IdPrecioNacionalidadRetorno) = value
        End Set
    End Property

    Public Shared Property IdPrecioNacionalidadIda() As Byte
        Get
            Return CByte(Session(_IdPrecioNacionalidadIda))
        End Get
        Set(ByVal value As Byte)
            Session(_IdPrecioNacionalidadIda) = value
        End Set
    End Property

    Public Shared Property Nro_ViajeIda() As String
        Get
            Return CStr(Session(_Nro_ViajeIda))
        End Get
        Set(ByVal value As String)
            Session(_Nro_ViajeIda) = value
        End Set
    End Property

    Public Shared Property Nro_ViajeRetorno() As String
        Get
            Return CStr(Session(_Nro_ViajeRetorno))
        End Get
        Set(ByVal value As String)
            Session(_Nro_ViajeRetorno) = value
        End Set
    End Property

    Public Shared Property Codi_Sucursal_ida() As Int16
        Get
            Return CShort(Session(_Codi_Sucursal_ida))
        End Get
        Set(ByVal value As Int16)
            Session(_Codi_Sucursal_ida) = value
        End Set
    End Property

    Public Shared Property Codi_Sucursal_Retorno() As Int16
        Get
            Return CShort(Session(_Codi_Sucursal_Retorno))
        End Get
        Set(ByVal value As Int16)
            Session(_Codi_Sucursal_Retorno) = value
        End Set
    End Property

    Public Shared Property Codi_Destino_Ida() As Int16
        Get
            Return CShort(Session(_Codi_Destino_Ida))
        End Get
        Set(ByVal value As Int16)
            Session(_Codi_Destino_Ida) = value
        End Set
    End Property

    Public Shared Property Codi_Destino_Retorno() As Int16
        Get
            Return CShort(Session(_Codi_Destino_Retorno))
        End Get
        Set(ByVal value As Int16)
            Session(_Codi_Destino_Retorno) = value
        End Set
    End Property

    Public Shared Property Hora_Programacion_Ida() As String
        Get
            Return CStr(Session(_Hora_Programacion_Ida))
        End Get
        Set(ByVal value As String)
            Session(_Hora_Programacion_Ida) = value
        End Set
    End Property

    Public Shared Property Hora_Programacion_Retorno() As String
        Get
            Return CStr(Session(_Hora_Programacion_Retorno))
        End Get
        Set(ByVal value As String)
            Session(_Hora_Programacion_Retorno) = value
        End Set
    End Property

    Public Shared Property Fecha_Programacion_Ida() As String
        Get
            Return CStr(Session(_Fecha_Programacion_Ida))
        End Get
        Set(ByVal value As String)
            Session(_Fecha_Programacion_Ida) = value
        End Set
    End Property

    Public Shared Property Fecha_Programacion_Retorno() As String
        Get
            Return CStr(Session(_Fecha_Programacion_Retorno))
        End Get
        Set(ByVal value As String)
            Session(_Fecha_Programacion_Retorno) = value
        End Set
    End Property

    Public Shared Property Cod_ProgramacionIda() As Int32
        Get
            Return CInt(Session(_Cod_ProgramacionIda))
        End Get
        Set(ByVal value As Int32)
            Session(_Cod_ProgramacionIda) = value
        End Set
    End Property

    Public Shared Property Cod_ProgramacionRetorno() As Int32
        Get
            Return CInt(Session(_Cod_ProgramacionRetorno))
        End Get
        Set(ByVal value As Int32)
            Session(_Cod_ProgramacionRetorno) = value
        End Set
    End Property

    Public Shared Property ServicioIdIda() As Byte
        Get
            Return CByte(Session(_ServicioIdIda))
        End Get
        Set(ByVal value As Byte)
            Session(_ServicioIdIda) = value
        End Set
    End Property

    Public Shared Property ServicioIdRetorno() As Byte
        Get
            Return CByte(Session(_ServicioIdRetorno))
        End Get
        Set(ByVal value As Byte)
            Session(_ServicioIdRetorno) = value
        End Set
    End Property

    Public Shared Property PlanoIda() As String
        Get
            Return CStr(Session(_PlanoIda))
        End Get
        Set(ByVal value As String)
            Session(_PlanoIda) = value
        End Set
    End Property

    Public Shared Property PlanoRetorno() As String
        Get
            Return CStr(Session(_PlanoRetorno))
        End Get
        Set(ByVal value As String)
            Session(_PlanoRetorno) = value
        End Set
    End Property

    Public Shared Property IdBusIda() As String
        Get
            Return CStr(Session(_IdBusIda))
        End Get
        Set(ByVal value As String)
            Session(_IdBusIda) = value
        End Set
    End Property

    Public Shared Property IdBusRetorno() As String
        Get
            Return CStr(Session(_IdBusRetorno))
        End Get
        Set(ByVal value As String)
            Session(_IdBusRetorno) = value
        End Set
    End Property

    Public Shared Property NroPolizaBusIda() As String
        Get
            Return CStr(Session(_NroPolizaBusIda))
        End Get
        Set(ByVal value As String)
            Session(_NroPolizaBusIda) = value
        End Set
    End Property

    Public Shared Property NroPolizaBusRetorno() As String
        Get
            Return CStr(Session(_NroPolizaBusRetorno))
        End Get
        Set(ByVal value As String)
            Session(_NroPolizaBusRetorno) = value
        End Set
    End Property

    Public Shared Property ListarOficinasWebActivas() As ListarOficinas
        Get
            Return CType(Session(_ListarOficinasWebActivas), ListarOficinas)
        End Get
        Set(ByVal value As ListarOficinas)
            Session(_ListarOficinasWebActivas) = value
        End Set
    End Property

    Public Shared Property ListaMonedas() As ListaMonedas
        Get
            Return CType(Session(_ListaMonedas), ListaMonedas)
        End Get
        Set(ByVal value As ListaMonedas)
            Session(_ListaMonedas) = value
        End Set
    End Property

    Public Shared Property SelectionRuta() As Boolean
        Get
            Return CBool(Session(_SelectionRuta))
        End Get
        Set(ByVal value As Boolean)
            Session(_SelectionRuta) = value
        End Set
    End Property

    Public Shared Property SelectionAsientos() As Boolean
        Get
            Return CBool(Session(_SelectionAsientos))
        End Get
        Set(ByVal value As Boolean)
            Session(_SelectionAsientos) = value
        End Set
    End Property

    Public Shared Property SelectionPasajeros() As Boolean
        Get
            Return CBool(Session(_SelectionPasajeros))
        End Get
        Set(ByVal value As Boolean)
            Session(_SelectionPasajeros) = value
        End Set
    End Property

    Public Shared Property SelectionPago() As Boolean
        Get
            Return CBool(Session(_SelectionPago))
        End Get
        Set(ByVal value As Boolean)
            Session(_SelectionPago) = value
        End Set
    End Property

    Public Shared Property SelectionTransaction() As Boolean
        Get
            Return CBool(Session(_SelectionTransaction))
        End Get
        Set(ByVal value As Boolean)
            Session(_SelectionTransaction) = value
        End Set
    End Property

    Public Shared Property SelectionOKTransaction() As Boolean
        Get
            Return CBool(Session(_SelectionOKTransaction))
        End Get
        Set(ByVal value As Boolean)
            Session(_SelectionOKTransaction) = value
        End Set
    End Property

    Public Shared Property ETicket() As String
        Get
            Return CStr(Session(_ETicket))
        End Get
        Set(ByVal value As String)
            Session(_ETicket) = value
        End Set
    End Property

    Public Shared Property ProveedorPasarela() As Int32
        Get
            Return CInt(Session(_ProveedorPasarela))
        End Get
        Set(ByVal value As Int32)
            Session(_ProveedorPasarela) = value
        End Set
    End Property

    Public Shared Property FormaPagoWeb() As Int32
        Get
            Return CInt(Session(_FormaPagoWeb))
        End Get
        Set(ByVal value As Int32)
            Session(_FormaPagoWeb) = value
        End Set
    End Property

    Public Shared Property IdVenta() As String
        Get
            Return CStr(Session(_IdVenta))
        End Get
        Set(ByVal value As String)
            Session(_IdVenta) = value
        End Set
    End Property
	
    Public Shared Property OrdenVenta() As Integer
        Get
            Return CInt(Session(_OrdenVenta))
        End Get
        Set(ByVal value As Integer)
            Session(_OrdenVenta) = value
        End Set
    End Property
	
    Public Shared Property AsientoAsignando() As String
        Get
            Return CStr(Session(_AsientoAsignando))
        End Get
        Set(ByVal value As String)
            Session(_AsientoAsignando) = value
        End Set
    End Property

    Public Shared Property ListaTipoDocumentos() As List(Of ETablas)
        Get
            Return CType(Session(_ListaTipoDocumentos), List(Of ETablas))
        End Get
        Set(ByVal value As List(Of ETablas))
            Session(_ListaTipoDocumentos) = value
        End Set
    End Property

    Public Shared Property ListaPaises() As ListaPaises
        Get
            Return CType(Session(_ListaPaises), ListaPaises)
        End Get
        Set(ByVal value As ListaPaises)
            Session(_ListaPaises) = value
        End Set
    End Property

    Public Shared Property ListaVentaweb() As ListaVentaWeb
        Get
            Return CType(Session(_ListaVentaweb), ListaVentaWeb)
        End Get
        Set(ByVal value As ListaVentaWeb)
            Session(_ListaVentaweb) = value
        End Set
    End Property

    Public Shared Property ListaVentawebReservas() As ListaVentaWeb
        Get
            Return CType(Session(_ListaVentawebReservas), ListaVentaWeb)
        End Get
        Set(ByVal value As ListaVentaWeb)
            Session(_ListaVentawebReservas) = value
        End Set
    End Property

    Public Shared Property ListaVentawebVentas() As ListaVentaWeb
        Get
            Return CType(Session(_ListaVentawebVentas), ListaVentaWeb)
        End Get
        Set(ByVal value As ListaVentaWeb)
            Session(_ListaVentawebVentas) = value
        End Set
    End Property


    Public Shared Property ListaDetalleVentawebIda() As ListaVenta
        Get
            Return CType(Session(_ListaDetalleVentawebIda), ListaVenta)
        End Get
        Set(ByVal value As ListaVenta)
            Session(_ListaDetalleVentawebIda) = value
        End Set
    End Property

    Public Shared Property ListaDetalleVentawebRetorno() As ListaVenta
        Get
            Return CType(Session(_ListaDetalleVentawebRetorno), ListaVenta)
        End Get
        Set(ByVal value As ListaVenta)
            Session(_ListaDetalleVentawebRetorno) = value
        End Set
    End Property

    Public Shared Property ListaDetallePasajero() As ListaVenta
        Get
            Return CType(Session(_ListaDetallePasajero), ListaVenta)
        End Get
        Set(ByVal value As ListaVenta)
            Session(_ListaDetallePasajero) = value
        End Set
    End Property

    Public Shared Property ListaAsientosbyOrderIda() As ListaAsientos
        Get
            Return CType(Session(_ListaAsientosbyOrderIda), ListaAsientos)
        End Get
        Set(ByVal value As ListaAsientos)
            Session(_ListaAsientosbyOrderIda) = value
        End Set
    End Property

    Public Shared Property ListaAsientosbyOrderRetorno() As ListaAsientos
        Get
            Return CType(Session(_ListaAsientosbyOrderRetorno), ListaAsientos)
        End Get
        Set(ByVal value As ListaAsientos)
            Session(_ListaAsientosbyOrderRetorno) = value
        End Set
    End Property

    Public Shared Property ListaAsientoWebIda() As ListaAsientos
        Get
            Return CType(Session(_ListaAsientoWebIda), ListaAsientos)
        End Get
        Set(ByVal value As ListaAsientos)
            Session(_ListaAsientoWebIda) = value
        End Set
    End Property

    Public Shared Property ListaAsientoWebRetorno() As ListaAsientos
        Get
            Return CType(Session(_ListaAsientoWebRetorno), ListaAsientos)
        End Get
        Set(ByVal value As ListaAsientos)
            Session(_ListaAsientoWebRetorno) = value
        End Set
    End Property

    Public Shared Property ListaProgramacionIda() As ListaProgramacion
        Get
            Return CType(Session(_ListaProgramacionIda), ListaProgramacion)
        End Get
        Set(ByVal value As ListaProgramacion)
            Session(_ListaProgramacionIda) = value
        End Set
    End Property

    Public Shared Property ListaProgramacionRetorno() As ListaProgramacion
        Get
            Return CType(Session(_ListaProgramacionRetorno), ListaProgramacion)
        End Get
        Set(ByVal value As ListaProgramacion)
            Session(_ListaProgramacionRetorno) = value
        End Set
    End Property

    Public Shared Property RutaConcatenadaIda() As Boolean
        Get
            Return CBool(Session(_RutaConcatenadaIda))
        End Get
        Set(ByVal value As Boolean)
            Session(_RutaConcatenadaIda) = value
        End Set
    End Property

    Public Shared Property RutaConcatenadaRetorno() As Boolean
        Get
            Return CBool(Session(_RutaConcatenadaRetorno))
        End Get
        Set(ByVal value As Boolean)
            Session(_RutaConcatenadaRetorno) = value
        End Set
    End Property

    Public Shared Property RutaConcatenadaPasajero() As Boolean
        Get
            Return CBool(Session(_RutaConcatenadaPasajero))
        End Get
        Set(ByVal value As Boolean)
            Session(_RutaConcatenadaPasajero) = value
        End Set
    End Property

    Public Shared Property IdUser() As Integer
        Get
            Return CInt(Session(_IdUser))
        End Get
        Set(ByVal value As Integer)
            Session(_IdUser) = value
        End Set
    End Property

    Public Shared Property Name() As String
        Get
            Return CStr(Session(_Name))
        End Get
        Set(ByVal value As String)
            Session(_Name) = value
        End Set
    End Property

    Public Shared Property NivelUser() As Int32
        Get
            Return CInt(Session(_NivelUser))
        End Get
        Set(ByVal value As Int32)
            Session(_NivelUser) = value
        End Set
    End Property

    Public Shared Property Pws() As String
        Get
            Return CStr(Session(_Pws))
        End Get
        Set(ByVal value As String)
            Session(_Pws) = value
        End Set
    End Property

    Public Shared Property IdPerfil() As String
        Get
            Return CStr(Session(_IdPerfil))
        End Get
        Set(ByVal value As String)
            Session(_IdPerfil) = value
        End Set
    End Property

    Public Shared Property Cliente() As EUsers
        Get
            Return CType(Session(_Cliente), EUsers)
        End Get
        Set(ByVal value As EUsers)
            Session(_Cliente) = value
        End Set
    End Property

    Public Shared Property Pasajero() As EVenta
        Get
            Return CType(Session(_Pasajero), EVenta)
        End Get
        Set(ByVal value As EVenta)
            Session(_Pasajero) = value
        End Set
    End Property

    Public Shared Property Correo_Users() As String
        Get
            Return CStr(Session(_Correo_Users))
        End Get
        Set(ByVal value As String)
            Session(_Correo_Users) = value
        End Set
    End Property

    Public Shared Property FechaReg_Users() As Date
        Get
            Return CDate(Session(_FechaReg_Users))
        End Get
        Set(ByVal value As Date)
            Session(_FechaReg_Users) = value
        End Set
    End Property

    Public Shared Property Respuesta() As String
        Get
            Return CStr(Session(_Respuesta))
        End Get
        Set(ByVal value As String)
            Session(_Respuesta) = value
        End Set
    End Property

    Public Shared Property Cod_Tienda() As String
        Get
            Return CStr(Session(_Cod_Tienda))
        End Get
        Set(ByVal value As String)
            Session(_Cod_Tienda) = value
        End Set
    End Property

    Public Shared Property NroOrdenTienda() As String
        Get
            Return CStr(Session(_NroOrdenTienda))
        End Get
        Set(ByVal value As String)
            Session(_NroOrdenTienda) = value
        End Set
    End Property


    Public Shared Property TarjetaTransaccion() As String
        Get
            Return CStr(Session(_TarjetaTransaccion))
        End Get
        Set(ByVal value As String)
            Session(_TarjetaTransaccion) = value
        End Set
    End Property

    Public Shared Property FechaTransaccionVisa() As String
        Get
            Return CStr(Session(_FechaTransaccionVisa))
        End Get
        Set(ByVal value As String)
            Session(_FechaTransaccionVisa) = value
        End Set
    End Property

    Public Shared Property Transaccion_Id() As String
        Get
            Return CStr(Session(_Transaccion_Id))
        End Get
        Set(ByVal value As String)
            Session(_Transaccion_Id) = value
        End Set
    End Property

    Public Shared Property NomTarjeta() As String
        Get
            Return CStr(Session(_NomTarjeta))
        End Get
        Set(ByVal value As String)
            Session(_NomTarjeta) = value
        End Set
    End Property


    Public Shared Property Cod_Desc_Accion() As String
        Get
            Return CStr(Session(_Cod_Desc_Accion))
        End Get
        Set(ByVal value As String)
            Session(_Cod_Desc_Accion) = value
        End Set
    End Property

    Public Shared Property Cod_Accion() As String
        Get
            Return CStr(Session(_Cod_Accion))
        End Get
        Set(ByVal value As String)
            Session(_Cod_Accion) = value
        End Set
    End Property


    Public Shared Property isPagando() As Boolean
        Get
            Return CBool(Session(_isPagando))
        End Get
        Set(ByVal value As Boolean)
            Session(_isPagando) = value
        End Set
    End Property

    Public Shared Property isReservando() As Boolean
        Get
            Return CBool(Session(_isReservando))
        End Get
        Set(ByVal value As Boolean)
            Session(_isReservando) = value
        End Set
    End Property

    Public Shared Property IsProcesable() As Boolean
        Get
            Return CBool(Session(_IsProcesable))
        End Get
        Set(ByVal value As Boolean)
            Session(_IsProcesable) = value
        End Set
    End Property

    Public Shared Property Fviaje() As String
        Get
            Return CStr(Session(_Fviaje))
        End Get
        Set(ByVal value As String)
            Session(_Fviaje) = value
        End Set
    End Property

    Public Shared Property NumOrden() As String
        Get
            Return CStr(Session(_NumOrder))
        End Get
        Set(ByVal value As String)
            Session(_NumOrder) = value
        End Set
    End Property

    Public Shared Property Id_VentaWebRetorno() As String
        Get
            Return CStr(Session(_Id_VentaWebRetorno))
        End Get
        Set(ByVal value As String)
            Session(_Id_VentaWebRetorno) = value
        End Set
    End Property

    Public Shared Property Id_VentaWebIda() As String
        Get
            Return CStr(Session(_Id_VentaWebIda))
        End Get
        Set(ByVal value As String)
            Session(_Id_VentaWebIda) = value
        End Set
    End Property

    Public Shared Property Id_WebOrders() As Integer
        Get
            Return CInt(Session(_Id_WebOrders))
        End Get
        Set(ByVal value As Integer)
            Session(_Id_WebOrders) = value
        End Set
    End Property

    Public Shared Property ListaPreciosFechasIda() As ListaPreciosFechas
        Get
            Return CType(Session(_ListaPreciosFechasIda), ListaPreciosFechas)
        End Get
        Set(ByVal value As ListaPreciosFechas)
            Session(_ListaPreciosFechasIda) = value
        End Set
    End Property

    Public Shared Property ListaPreciosFechasRetorno() As ListaPreciosFechas
        Get
            Return CType(Session(_ListaPreciosFechasRetorno), ListaPreciosFechas)
        End Get
        Set(ByVal value As ListaPreciosFechas)
            Session(_ListaPreciosFechasRetorno) = value
        End Set
    End Property

    Public Shared Property Promociones() As Boolean
        Get
            Return CBool(Session(_Promociones))
        End Get
        Set(ByVal value As Boolean)
            Session(_Promociones) = value
        End Set
    End Property

    Public Shared Property TransactionPromociones() As Boolean
        Get
            Return CBool(Session(_TransactionPromociones))
        End Get
        Set(ByVal value As Boolean)
            Session(_TransactionPromociones) = value
        End Set
    End Property

    Public Shared Property ListarMediosPagosTCredito() As ListaMediosPagos
        Get
            Return CType(Session(_ListarMediosPagosTCredito), ListaMediosPagos)
        End Get
        Set(ByVal value As ListaMediosPagos)
            Session(_ListarMediosPagosTCredito) = value
        End Set
    End Property

    Public Shared Property ListarMediosPagos() As ListaMediosPagos
        Get
            Return CType(Session(_ListarMediosPagos), ListaMediosPagos)
        End Get
        Set(ByVal value As ListaMediosPagos)
            Session(_ListarMediosPagos) = value
        End Set
    End Property

    Public Shared Property ListaItinerariosIda() As ListaMaestroProgramacion
        Get
            Return CType(Session(_ListaItinerariosIda), ListaMaestroProgramacion)
        End Get
        Set(ByVal value As ListaMaestroProgramacion)
            Session(_ListaItinerariosIda) = value
        End Set
    End Property

    Public Shared Property ListaItinerariosRetorno() As ListaMaestroProgramacion
        Get
            Return CType(Session(_ListaItinerariosRetorno), ListaMaestroProgramacion)
        End Get
        Set(ByVal value As ListaMaestroProgramacion)
            Session(_ListaItinerariosRetorno) = value
        End Set
    End Property

    Public Shared Property VisaNet() As VisaNetEntidad
        Get
            Return CType(Session(_VisaNet), VisaNetEntidad)
        End Get
        Set(ByVal value As VisaNetEntidad)
            Session(_VisaNet) = value
        End Set
    End Property

    Public Shared Property CorreoCliente() As String
        Get
            Return CType(Session(_CorreoCliente), String)
        End Get
        Set(ByVal value As String)
            Session(_CorreoCliente) = value
        End Set
    End Property

#End Region















End Class
