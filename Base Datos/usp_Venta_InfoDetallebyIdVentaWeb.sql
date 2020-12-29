GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Venta_InfoDetallebyIdVentaWeb')
	BEGIN
		DROP PROCEDURE usp_Venta_InfoDetallebyIdVentaWeb
	End
GO
GO
Create Procedure usp_Venta_InfoDetallebyIdVentaWeb 
@Id_VentaWeb			int
as
Set NoCount On
Begin
	Select 
	v.Id_venta,
	v.Nume_asiento,
	v.Prec_venta,
	v.Nombre,
	v.Edad,
	v.Nit_Cliente,
	v.DNI,tb.SAB_CON as Tipo_Doc,
	Hora_Embarque_Web,
	ofi.Descripcion,ofi.Direccion,
	v.SERIE_BOLETO,
	v.NUME_BOLETO,
	v.tipo
	from VentaWeb vw
	Inner Join Venta v on vw.Id_ventaWeb=v.Id_VentaWeb and v.Indi_Anulado='F'
	Inner Join Venta_Derivada vd on v.Id_venta=vd.Id_Venta
	Inner Join Tb_oficinas ofi on vd.Sube_En=ofi.Codi_Sucursal
	Inner Join TABLAS tb on v.Tipo_Doc=tb.COD_TIP and '56'=tb.Cod_tab and 8>Sab_Con
	and vw.Id_ventaweb=@Id_VentaWeb
End