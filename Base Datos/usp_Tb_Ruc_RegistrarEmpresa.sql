GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Tb_Ruc_RegistrarEmpresa')
	BEGIN
		DROP PROCEDURE usp_Tb_Ruc_RegistrarEmpresa
	End
GO
GO
Create Procedure usp_Tb_Ruc_RegistrarEmpresa
@NIT_Cliente	Varchar(11),
@Razon_Social	Varchar(250),
@Rz_Direccion	Varchar(250),
@Rz_Telefono	Varchar(15)
as		
Set NoCount On
  Begin Transaction    			
		INSERT INTO Tb_Ruc(Ruc_Cliente,Razon_Social,Direccion,Telefono)
		VALUES(@NIT_Cliente,Upper(@Razon_Social),@Rz_Direccion,@Rz_Telefono)
	If @@Error<>0
        RollBack Transaction                
	Else
		Commit Transaction