Create Procedure usp_Venta_DetalleEmail
@Id_WebOrders			int
as
Set NoCount On
Begin

	Select 
	vw.OrderVenta,
	ori.Descripcion Origen,
	des.Descripcion Destino,
	ser.descripcion Servicio,
	vw.FechaViaje,
	vd.Hora_Embarque_Web HoraViaje,
	orib.Descripcion OrigenBus,
	tb.SAB_CON as Tipo_Doc,
	v.DNI,
	v.Nombre,
	Cast(v.SERIE_BOLETO as SmallInt)SERIE_BOLETO,
	v.NUME_BOLETO,
	v.tipo,
	v.Nume_asiento,
	v.Prec_venta,
	ofi.Descripcion Embarque,
	ofi.Direccion  Direccion_Embarque
	from VentaWeb vw
	Inner Join Venta v on vw.Id_ventaWeb=v.Id_VentaWeb and v.Indi_Anulado='F'
	Inner Join Venta_Derivada vd on v.Id_venta=vd.Id_Venta
	Inner Join Tb_Programacion pro on v.CODI_PROGRAMACION=pro.Codi_Programacion
	Inner Join Tb_Oficinas ori on vw.OrigenId=ori.Codi_Sucursal
	Inner Join Tb_Oficinas des on vw.DestinoId=des.Codi_Sucursal
	Inner Join Tb_Oficinas orib on pro.Codi_Sucursal=orib.Codi_Sucursal
	Inner Join Tb_oficinas ofi on vd.Sube_En=ofi.Codi_Sucursal
	Inner Join Tb_Servicio ser on vd.Servicio=ser.Codi_Servicio
	Inner Join TABLAS tb on v.Tipo_Doc=tb.COD_TIP and '56'=tb.Cod_tab and 8>Sab_Con
	and vw.Id_WebOrders=@Id_WebOrders
End