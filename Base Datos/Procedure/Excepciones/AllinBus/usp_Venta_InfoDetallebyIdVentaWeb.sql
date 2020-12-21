Alter Procedure usp_Venta_InfoDetallebyIdVentaWeb 
@Id_VentaWeb			int
as
Set NoCount On
Begin
	Select v.Id_venta,v.Nume_asiento,v.Prec_venta,v.Nombre,v.Edad, v.Nit_Cliente,v.DNI,tb.SAB_CON as Tipo_Doc,Hora_Embarque_Web,
	IsNull(emb.Descripcion,'FUNCIDION') + ' - ' + ISNULL(emb.Direccion,'PROL. LEONCIO PRADO MANZANA N. LOTE 185- PUENTE PIEDRA') + ' - ' + vd.Hora_Embarque_Web Embarque,
	IsNull(dese.Descripcion,'FUNCIDION') + ' - ' + ISNULL(dese.Direccion,'PROL. LEONCIO PRADO MANZANA N. LOTE 185- PUENTE PIEDRA') Desembarque
	from VentaWeb vw
	Inner Join Venta v on vw.Id_ventaWeb=v.Id_VentaWeb and v.Indi_Anulado='F'
	Inner Join Venta_Derivada vd on v.Id_venta=vd.Id_Venta
	Left Join Tb_oficinas emb on vd.Sube_En=emb.Codi_Sucursal
	Left Join Tb_oficinas dese on vd.baja_en=dese.Codi_Sucursal
	Inner Join TABLAS tb on v.Tipo_Doc=tb.COD_TIP and '56'=tb.Cod_tab and 8>Sab_Con
	and vw.Id_ventaweb=@Id_VentaWeb
End
 
