GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Users_ListarFilter')
	BEGIN
		DROP PROCEDURE usp_Users_ListarFilter
	End
GO
GO
Create Procedure usp_Users_ListarFilter
@User			varchar(80),
@Nombres		varchar(50),
@Apellidos		varchar(50)
as
Set NoCount On
Begin
	Select Id_Users,Apellidos_Users,Nombres_Users,DOC_Users,Telefono_Users,Pais_Users,Correo_Users,Password_Users,
	Direccion_Users,FechaNacimiento_Users,Usuario_Users,TipoDoc_Users,Sexo_Users,Status_Users,Nivel_Users,
	Nombres_Users+ ', '+Apellidos_Users as Nombres_Apellidos,Codi_Sucursal,d.NameTypeDoc From Users u 
	Inner Join vw_TypeDoc d on u.TipoDoc_Users=d.IdTypeDoc
	Where Nivel_Users>=6 and Usuario_Users like '%'+@User+'%' 
	and Nombres_Users like '%'+@Nombres+'%'	and Apellidos_Users like '%'+@Apellidos+'%'
End


