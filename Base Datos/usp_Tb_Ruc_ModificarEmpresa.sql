GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Tb_Ruc_ModificarEmpresa')
	BEGIN
		DROP PROCEDURE usp_Tb_Ruc_ModificarEmpresa
	End
GO
GO
Create Procedure usp_Tb_Ruc_ModificarEmpresa
@NIT_Cliente	Varchar(11),
@Razon_Social	Varchar(250),
@Rz_Direccion	Varchar(250),
@Rz_Telefono	Varchar(15)
as		
Set NoCount On
  Begin Transaction    			
		Update Tb_Ruc Set Razon_Social=Upper(@Razon_Social),
		Direccion=@Rz_Direccion,Telefono=@Rz_Telefono
		Where Ruc_Cliente=@NIT_Cliente
	If @@Error<>0
        RollBack Transaction                
	Else
		Commit Transaction