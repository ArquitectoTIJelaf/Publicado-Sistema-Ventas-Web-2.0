Alter Procedure usp_Venta_BuscarReservas
@Id_WebOrders		Int
as
Select 
vw.Id_VentaWeb,
v.Id_venta,
v.CODI_EMPRESA,
Isnull(v.NIT_CLIENTE,'') Nit_Cliente,
Isnull(r.Razon_Social,'')Razon_Social,
Isnull(r.Direccion,'')Direccion,
v.DNI,
v.EDAD,
v.PREC_VENTA,
v.TIPO_DOC,
v.SEXO,
v.NUME_ASIENTO,
v.SUC_VENTA,
v.CODI_PROGRAMACION,
v.Punto_Venta Codi_PuntoVenta,
vd.Fecha_Viaje,
vd.Hora_Viaje,
p.Codi_ruta,
v.cod_origen Codi_Origen,
v.CODI_SUBRUTA Codi_Destino,
vd.Servicio Codi_Servicio,
v.NOMBRE,
vd.Sube_En Codi_Embarque,
vd.baja_en Codi_Arribo,
vd.Hora_Embarque_Web Hora_Embarque,
ori.Descripcion Nom_Origen,
des.Descripcion Nom_Destino,
ser.descripcion Nom_Servicio,
v.TELEFONO,
vd.Recoje_En,
vd.Comision_web,
vd.proveedor_tar,
vw.OrderVenta,
e.Ruc Ruc_Empresa,
v.FECH_VENTA Fecha_Venta
from VentaWeb vw
Inner Join Venta v on vw.Id_VentaWeb=v.Id_VentaWeb
Inner Join VENTA_DERIVADA vd on v.id_venta=vd.id_venta
Inner Join Tb_Programacion p on v.CODI_PROGRAMACION=p.Codi_Programacion
Inner Join Tb_Oficinas ori on v.cod_origen=ori.Codi_Sucursal
Inner Join Tb_Oficinas des on v.CODI_SUBRUTA=des.Codi_Sucursal
Inner Join Tb_Servicio ser on vd.Servicio=ser.Codi_Servicio
Inner Join Tb_Empresa e on v.CODI_EMPRESA=e.Codi_Empresa
Left Join Tb_Ruc r on v.NIT_CLIENTE=r.Ruc_Cliente
Where vw.Id_WebOrders=@Id_WebOrders and v.FLAG_VENTA='R' and v.INDI_ANULADO='F'