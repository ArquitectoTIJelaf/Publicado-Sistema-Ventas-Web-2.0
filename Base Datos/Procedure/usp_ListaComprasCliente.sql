Create Procedure usp_ListaComprasCliente
@FechaInicio	SmallDateTime,
@FechaFin		SmallDateTime,
@TipoFecha		Char(1),
@EmailCliente	Varchar(200),
@Nro_Orden		Varchar(20)
as
	If @TipoFecha='1'
		Begin
			Select wo.NumOrder_WebOrders,vw.Fecha_VentaWeb FechaVenta,vw.FechaViaje,vw.HoraViaje,ori.Descripcion Origen,
			des.Descripcion Destino,ser.descripcion Servicio,v.NUME_ASIENTO,v.PREC_VENTA,emp.Razon_Social,
			v.NUME_BOLETO,v.SERIE_BOLETO,v.tipo,v.NOMBRE from WebOrders wo
			Inner Join VentaWeb vw on wo.Id_WebOrders=vw.Id_WebOrders
			Inner Join Venta v on vw.Id_VentaWeb=v.Id_VentaWeb
			Inner Join VENTA_DERIVADA vd on v.id_venta=vd.id_venta
			Inner Join Tb_Oficinas ori on ori.Codi_Sucursal=vw.OrigenId
			Inner Join Tb_Oficinas des on des.Codi_Sucursal=vw.DestinoId
			Inner Join Tb_Servicio ser on vd.Servicio=ser.Codi_Servicio
			Inner Join Tb_Empresa emp on v.CODI_EMPRESA=emp.Codi_Empresa
			Where wo.Email_Cliente=@EmailCliente and (wo.NumOrder_WebOrders=@Nro_Orden or @Nro_Orden='')
			and (vw.Fecha_VentaWeb>=@FechaInicio and vw.Fecha_VentaWeb<=@FechaFin) and v.INDI_ANULADO='F' and v.FLAG_VENTA='T' 
			Order by vw.Fecha_VentaWeb desc
		End
	Else If @TipoFecha='2'
		Begin
			Select wo.NumOrder_WebOrders,vw.Fecha_VentaWeb FechaVenta,vw.FechaViaje,vw.HoraViaje,ori.Descripcion Origen,
			des.Descripcion Destino,ser.descripcion Servicio,v.NUME_ASIENTO,v.PREC_VENTA,emp.Razon_Social,
			v.NUME_BOLETO,v.SERIE_BOLETO,v.tipo from WebOrders wo
			Inner Join VentaWeb vw on wo.Id_WebOrders=vw.Id_WebOrders
			Inner Join Venta v on vw.Id_VentaWeb=v.Id_VentaWeb
			Inner Join VENTA_DERIVADA vd on v.id_venta=vd.id_venta
			Inner Join Tb_Oficinas ori on ori.Codi_Sucursal=vw.OrigenId
			Inner Join Tb_Oficinas des on des.Codi_Sucursal=vw.DestinoId
			Inner Join Tb_Servicio ser on vd.Servicio=ser.Codi_Servicio
			Inner Join Tb_Empresa emp on v.CODI_EMPRESA=emp.Codi_Empresa
			Where wo.Email_Cliente=@EmailCliente and (wo.NumOrder_WebOrders=@Nro_Orden or @Nro_Orden='')
			and (vw.FechaViaje>=@FechaInicio and vw.FechaViaje<=@FechaFin) and v.INDI_ANULADO='F' and v.FLAG_VENTA='T'
			Order by vw.FechaViaje desc
		End

