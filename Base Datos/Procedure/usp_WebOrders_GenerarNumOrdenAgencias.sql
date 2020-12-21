CREATE procedure usp_WebOrders_GenerarNumOrdenAgencias
@NumOrden		Varchar(15),
@IdCID			varchar(50),
@IdUser			numeric(18),
@UserIP			varchar(20),
@UserHost		varchar(50),
@HttpHead		varchar(50),
@Type			int,
@RucCliente		varchar(11)
as
  Begin Transaction      		
		declare @Id as numeric(18)
		declare @Agencia_Id as int
		Select @Agencia_Id=Agencia_id From Agencia Where Agencia_RUC=@RucCliente
		set @Id=0
			insert into WebOrders(Id_Users,IdCID_WebOrders,Fecha_WebOrders,NumOrder_WebOrders,
			HostIP_VentaWeb,HostName_WebOrders,HttpHead_VentaWeb,WOrderType_Id,Estado_WebOrders,Agencia_Cliente_id) 
			values (@IdUser,@IdCID,getdate(),@NumOrden,@UserIP,@UserHost,@HttpHead,@Type,1,@Agencia_Id)
			set @Id=scope_identity()
			update ASIENTO set Id_WebOrders=@Id,CODI_TERMINAL='998' where CODI_TERMINAL='999' and  (IdCID='1'+@IdCID or IdCID='2'+ @IdCID) and Id_Users=0

			select 'OK'+NumOrder_WebOrders+'|'+ cast(@Id as Varchar(10))as Dato  from WebOrders where Id_WebOrders=@Id	
			Update Tb_Correlativo_Auxiliares set Correlativo=cast(@NumOrden as int) Where  tabla='WebOrdersAgencias' and oficina='999'

 If @@Error<>0
       	RollBack Transaction                
    Else
       	Commit Transaction
