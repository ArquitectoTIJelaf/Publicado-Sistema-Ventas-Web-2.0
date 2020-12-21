Alter Procedure [dbo].[usp_Agencia_Update_One]
@Id					int,
@RUC				Varchar(11),
@Rz					Varchar(80),
@Representante		Varchar(80),
@Telefono			Varchar(20),
@Direccion			Varchar(80),
@FActivacion		smalldatetime,
@FVencimiento		smalldatetime,
@ComisionE			real,
@ComisionT			real,
@Type				int,
@Num_Solicitud		int,
@Contacto			Varchar(80),
@Mail_Contacto		Varchar(80),
@ComixIGV			int,
@FormaPago			int,
@Estado				bit
as
Set Nocount Off
Begin
  Begin Transaction    
		Update Agencia Set  Agencia_RUC=@RUC,Agencia_RS=@Rz,Agencia_Representante=@Representante,Agencia_Telefono=@Telefono,
		Agencia_Direccion=@Direccion,Agencia_FActivacion=@FActivacion,Agencia_FVencimiento=@FVencimiento,Agencia_Comision=@ComisionE,
		Agencia_Comision_Tarjeta=@ComisionT,Agencia_lastUpdate=getdate(),Agencia_Status=@Estado,Agencia_Type=@Type,
		Agencia_Num_Solicitud=@Num_Solicitud,Contacto=@Contacto,Mail_Contacto=@Mail_Contacto,Agencia_ComiIGV=@ComixIGV,
		Agencia_FormaPago=@FormaPago
		Where Agencia_id=@Id

	If @@Error<>0
        RollBack Transaction                
	Else
		Commit Transaction
End
