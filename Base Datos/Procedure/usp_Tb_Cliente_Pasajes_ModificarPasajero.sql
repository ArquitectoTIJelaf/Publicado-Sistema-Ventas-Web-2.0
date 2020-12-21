Alter Procedure usp_Tb_Cliente_Pasajes_ModificarPasajero    
@Tipo_Doc  Varchar(2),    
@DNI   Varchar(15),    
@Nombre   Varchar(100),    
@ApePaterno  Varchar(100),    
@ApeMaterno  Varchar(100),    
@Telefono  Varchar(15),    
@Dire   Varchar(120),    
@Edad   Varchar(2),    
@NIT_Cliente Varchar(11),    
@Sexo   Char(1),    
@id_oficinaemision Int,    
@id_puntoembarque Int    
as     
Set NoCount On    
 Begin Transaction       
   Update Tb_Cliente_Pasajes Set Tipo_Doc_id=@Tipo_Doc,Numero_Doc=@DNI,Nombre_CLientes=Upper(@Nombre),Apellido_P=Upper(@ApePaterno),Apellido_M=Upper(@ApeMaterno),    
   Telefono=@Telefono,Direccion=Upper(@Dire),Edad=@Edad,Base=0,Activo=1,Zona='',Ruc_Contacto=@NIT_Cliente,Sexo=@Sexo,id_oficinaemision=@id_oficinaemision,    
   id_puntoembarque=@id_puntoembarque Where  Tipo_Doc_Id=@Tipo_Doc and Numero_Doc=@DNI    
 If @@Error<>0    
        RollBack Transaction                    
 Else    
  Commit Transaction  