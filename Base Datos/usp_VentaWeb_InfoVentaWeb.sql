GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_VentaWeb_InfoVentaWeb')
	BEGIN
		DROP PROCEDURE usp_VentaWeb_InfoVentaWeb
	End
GO
GO
Create Procedure usp_VentaWeb_InfoVentaWeb  
@Id_ventaweb int  
as  
Select top 1 ori.Descripcion as Origen,des.Descripcion as Destino,  
convert(varchar(10),vd.Fecha_Viaje,103)+ ' ' + vd.Hora_Viaje as Fecha_Hora_Viaje,  
ser.Descripcion as Servicio from VentaWeb vw  
Inner Join Tb_oficinas ori on vw.OrigenId=ori.Codi_Sucursal  
Inner Join Tb_oficinas des on vw.DestinoId=des.Codi_Sucursal  
Inner Join Venta v on vw.Id_ventaweb=v.Id_ventaweb  
Inner Join venta_derivada vd on v.Id_venta=vd.Id_venta  
Inner Join Tb_servicio ser on vd.Servicio=ser.Codi_Servicio  
Where vw. Id_VentaWeb=@Id_ventaweb and v.INDI_ANULADO='F'