GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Agencia_Insert_One')
	BEGIN
		DROP PROCEDURE usp_Agencia_Insert_One
	End
GO
GO
Create Procedure [dbo].[usp_Agencia_Insert_One]
@RUC				Varchar(11),
@Rz					Varchar(80),
@Representante		Varchar(80),
@Telefono			Varchar(20),
@Direccion			Varchar(80),
@FActivacion		smalldatetime,
@FVencimiento		smalldatetime,
@ComisionE			real,
@ComisionT			real,
@IdCorrelativo		int,
@Type				int,
@Num_Solicitud		int,
@Contacto			Varchar(80),
@Mail_Contacto		Varchar(80),
@ComixIGV			int,
@FormaPago			int,
@TimeSales			int
as
Set Nocount Off
Begin
  Begin Transaction    
 
		Insert Into Agencia(Agencia_RUC,Agencia_RS,Agencia_Representante,Agencia_Telefono,Agencia_Direccion,
		Agencia_FRegistro,Agencia_FActivacion,Agencia_FVencimiento,Agencia_Comision,Agencia_Comision_Tarjeta,Agencia_lastUpdate,
		Agencia_Status,Agencia_SerieCorrelativo,Agencia_Type,Agencia_Num_Solicitud,Agencia_Externa,Contacto,Mail_Contacto,Agencia_ComiIGV,Agencia_FormaPago,TimeSales)
		Values(@RUC,@Rz,@Representante,@Telefono,@Direccion,getdate(),@FActivacion,@FVencimiento,@ComisionE,@ComisionT,
		getdate(),1,@IdCorrelativo,@Type,@Num_Solicitud,1,@Contacto,@Mail_Contacto,@ComixIGV,@FormaPago,0)
		Update Tb_Correlativo_Auxiliares set Correlativo=cast(@IdCorrelativo as int) Where tabla='Agencia_Correlativo' 
		and oficina='999'
	If @@Error<>0
        RollBack Transaction                
	Else
		Commit Transaction
End

