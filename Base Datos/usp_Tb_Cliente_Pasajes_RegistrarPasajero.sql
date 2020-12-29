GO
IF EXISTS (SELECT Top 1 1 FROM sys.objects WHERE type = 'P' AND name = 'usp_Tb_Cliente_Pasajes_RegistrarPasajero')
	BEGIN
		DROP PROCEDURE usp_Tb_Cliente_Pasajes_RegistrarPasajero
	End
GO
GO
Create Procedure usp_Tb_Cliente_Pasajes_RegistrarPasajero  
@Tipo_Doc  Varchar(2),  
@DNI   Varchar(15),  
@Nombre   Varchar(100),  
@ApePaterno  Varchar(100),  
@ApeMaterno  Varchar(100),  
@Telefono  Varchar(15),  
@Dire   Varchar(50),  
@Edad   Varchar(2),  
@NIT_Cliente Varchar(11),  
@Sexo   Char(1),  
@id_oficinaemision Int,  
@id_puntoembarque Int  
as   
Set NoCount On  
  
 Begin Transaction     
  
   INSERT INTO Tb_Cliente_Pasajes(Tipo_Doc_id,Numero_Doc,Nombre_CLientes,Apellido_P,Apellido_M,  
   Telefono,Direccion,Edad,Base,Activo,Zona,Ruc_Contacto,Sexo,id_oficinaemision,id_puntoembarque,fecha_ing)  
   Values( @Tipo_Doc,@DNI,Upper(@Nombre),Upper(@ApePaterno),Upper(@ApeMaterno),  
   @Telefono,Upper(@Dire),@Edad,0,1,'',@NIT_Cliente,@Sexo,@id_oficinaemision,@id_puntoembarque,getdate())  
 If @@Error<>0  
  
        RollBack Transaction                  
  
 Else  
  
  Commit Transaction