ALTER Procedure usp_Tb_Detalle_Encomienda_ListDetalleEncomiendabyNBoleto 
@Num_Boleta		VarChar(13),    
@Tipo_Doc		VarChar(2),    
@Empresa		int  ,
@Clave			Varchar(20)  
as    
SELECT  COUNT(de.Cnt_Encomienda)AS Cnt_Encomienda,de.Descripcion_Paquete,ec.CODI_BUS as Bus,    
ec.INDICADOR,(((isnull(de.Precio_Paquete,0.00))/Cnt_Encomienda)* COUNT(de.Cnt_Encomienda))as Precio_Paquete,ec.ID_DETALLE_ENC    
,ec.Fecha_Encomienda,ec.Hora_Encomienda, e.ID_ENCOMIENDA FROM   Tb_Encomienda e    
INNER JOIN Tb_Detalle_Encomienda de ON  de.Nume_Boleto =e.Nume_Boleto and de.Codi_Documento=e.Codi_Documento and de.Codi_Empresa=e.Codi_Empresa    
Inner JOIN TB_ENCOMIENDA_CODIGO ec ON de.Id_Detalle = ec.ID_DETALLE_ENC  
Inner JOIN tb_Encomienda_Claves ce ON e.ID_ENCOMIENDA = ce.id_encomienda 
where e.Nume_Boleto =@Num_Boleta and e.Codi_Documento=@Tipo_Doc and (e.Codi_Empresa=@Empresa or @Empresa=0)    
and ce.clave=@Clave and e.Indi_Anulado<>'A' and e.Indi_Anulado<>'AN'     
Group By de.Descripcion_Paquete,de.Cnt_Encomienda,ec.CODI_BUS,de.Precio_Paquete,    
ec.INDICADOR,ec.ID_DETALLE_ENC,ec.Fecha_Encomienda,ec.Hora_Encomienda , e.ID_ENCOMIENDA