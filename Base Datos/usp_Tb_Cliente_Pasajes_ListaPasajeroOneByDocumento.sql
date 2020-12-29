GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Tb_Cliente_Pasajes_ListaPasajeroOneByDocumento')
	BEGIN
		DROP PROCEDURE usp_Tb_Cliente_Pasajes_ListaPasajeroOneByDocumento
	End
GO
GO
Create Procedure [dbo].[usp_Tb_Cliente_Pasajes_ListaPasajeroOneByDocumento]
@TypeDoc			VarChar(2),
@NumDoc				VarChar(30)
as
select tb_cliente_pasajes.Tipo_Doc_Id,tb_cliente_pasajes.Numero_Doc,tb_cliente_pasajes.Nombre_Clientes,
tb_cliente_pasajes.Apellido_P,tb_cliente_pasajes.Apellido_M,tb_cliente_pasajes.Telefono,tb_cliente_pasajes.Direccion,
tb_cliente_pasajes.Email,isnull(tb_cliente_pasajes.Edad,0) as Edad,
Case isnull(tb_cliente_pasajes.Sexo,'') When '' Then 'X' Else tb_cliente_pasajes.Sexo End Sexo ,tb_cliente_pasajes.ruc_contacto,
tb_ruc.Razon_Social,tb_ruc.Direccion as RzDireccion,tb_ruc.Telefono as RzTelefono ,tb_ruc.ema_cliente as RzEmail,
tb_cliente_pasajes.id_oficinaemision as OfiEmision,tb_cliente_pasajes.id_puntoembarque as PtoEmbarque
from tb_cliente_pasajes
Left Join tb_ruc on tb_cliente_pasajes.ruc_contacto=tb_ruc.Ruc_Cliente
Where tb_cliente_pasajes.Tipo_Doc_Id=@TypeDoc and tb_cliente_pasajes.Numero_Doc=@NumDoc