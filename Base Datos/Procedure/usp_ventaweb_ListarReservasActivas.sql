Create Procedure usp_ventaweb_ListarReservasActivas
@FechaDesde			Varchar(10),
@FechaHasta			Varchar(10)
as
Set NoCount On
	Begin
		Select rh.Id_venta,rh.Fecha_C,rh.Hora_C,vw.Id_VentaWeb from ventaweb vw 
		Inner Join venta v on vw.Id_VentaWeb=v.Id_VentaWeb
		Inner Join tb_reservacion_hora_fecha rh on v.Id_Venta=rh.Id_Venta
		where vw.Fecha_VentaWeb>=@FechaDesde and vw.Fecha_VentaWeb<=@FechaHasta and vw.Flag_Venta='R1' and IdTVirtual=0
	End

