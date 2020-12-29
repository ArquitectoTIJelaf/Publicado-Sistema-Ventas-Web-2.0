GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_ValidarPermisoPanel')
	BEGIN
		DROP PROCEDURE usp_ValidarPermisoPanel
	End
GO
GO
Create Procedure usp_ValidarPermisoPanel
@Codi_Panel	Varchar(3)
as
	Select Top 1 1 from tb_panel_control_sistema
	Where codi_panel=@Codi_Panel and valor=1