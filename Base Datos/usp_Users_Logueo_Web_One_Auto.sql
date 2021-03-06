GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Users_Logueo_Web_One_Auto')
	BEGIN
		DROP PROCEDURE usp_Users_Logueo_Web_One_Auto
	End
GO
GO
Create procedure [dbo].[usp_Users_Logueo_Web_One_Auto] 
@UserName as varchar(100)
as
SET NOCOUNT ON
	select top 1 Id_Users,Apellidos_Users,Nombres_Users,Nivel_Users,Password_Users,
	Usuario_Users,Correo_Users,FechaReg_Users from users with (nolock)
	where  Correo_Users=@UserName  and Status_Users=1 
	and (Nivel_Users=1 or Nivel_Users>=7)