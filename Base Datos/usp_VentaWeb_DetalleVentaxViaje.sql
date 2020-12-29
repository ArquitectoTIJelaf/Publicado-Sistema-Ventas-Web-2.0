GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_VentaWeb_DetalleVentaxViaje')
	BEGIN
		DROP PROCEDURE usp_VentaWeb_DetalleVentaxViaje
	End
GO
GO
Create Procedure usp_VentaWeb_DetalleVentaxViaje

@Id_VentaWeb		int

as

Select o.Descripcion as Origen,d.Descripcion as Destino,s.Descripcion as Servicio,

vd.Fecha_Viaje,vd.Hora_Viaje,v.DNI,v.Tipo_Doc,v.Nombre,v.Nume_Asiento,v.Prec_Venta from VentaWeb vw 

Inner Join Venta v on vw.Id_VentaWeb=v.Id_ventaWeb and v.Indi_Anulado='F'

Inner Join Venta_Derivada vd on v.Id_Venta=vd.Id_venta

Inner Join Tb_oficinas o on vw.OrigenId=o.Codi_Sucursal

Inner Join Tb_oficinas d on vw.DestinoId=d.Codi_Sucursal

Inner Join Tb_Servicio s on vd.Servicio=s.Codi_Servicio

Where vw.Id_VentaWeb=@Id_VentaWeb




