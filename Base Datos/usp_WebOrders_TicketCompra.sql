GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_WebOrders_TicketCompra')
	BEGIN
		DROP PROCEDURE usp_WebOrders_TicketCompra
	End
GO
GO
Create Procedure usp_WebOrders_TicketCompra
@Id_WebOrders   int
as
Select  wo.NumOrder_WebOrders,u.Apellidos_Users,u.Nombres_Users, vw.Fecha_VentaWeb as Fecha_Venta,vw.OrderVenta ,
ori.DEscripcion as Origen,des.Descripcion as Destino,ser.descripcion as Servicio, vd.Fecha_Viaje,    
vd.Hora_Viaje,v.Nume_Asiento,v.DNI,v.Nombre,v.Edad,v.Prec_venta,u.Usuario_Users,v.CODI_EMPRESA,e.Ruc,e.Razon_Social,
right( ('00' + cast(v.SERIE_BOLETO as varchar)),3) SERIE_BOLETO ,right( ('000000' + cast(v.NUME_BOLETO as varchar)),7) NUME_BOLETO , v.TIPO_DOC  from WebOrders wo      
Inner Join Users u on wo.Id_Users=u.Id_Users   
Inner Join VentaWeb vw on wo.Id_WebOrders=vw.Id_WebOrders   
Inner Join Venta v on vw.Id_VentaWeb=v.Id_VentaWeb    
Inner Join Venta_Derivada vd on v.Id_Venta=vd.Id_Venta  
Inner Join Tb_Oficinas ori on v.Cod_Origen=ori.Codi_Sucursal  
Inner Join Tb_Oficinas des on v.Codi_SubRuta=des.Codi_Sucursal  
Inner Join Tb_Servicio ser on vd.Servicio=ser.Codi_Servicio  
Inner Join Tb_Empresa e on v.CODI_EMPRESA=e.Codi_Empresa
Where (v.Flag_venta='T' or v.FLAG_VENTA='R1') and v.Indi_Anulado='F'  
and wo.Id_WebOrders=@Id_WebOrders