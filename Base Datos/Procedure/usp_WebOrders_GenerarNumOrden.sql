alter Procedure usp_WebOrders_GenerarNumOrden
@IdCID			varchar(50),
@IdUser			numeric(18),
@UserIP			varchar(20),
@UserHost		varchar(50),
@HttpHead		varchar(50),
@Type			int			,
@Email_Cliente	varchar(200)  
as

  Begin Transaction      		

			declare @Id as numeric(18)

			Declare @NumOrden		Varchar(15)
			Declare @Correlativo	Int
			SELECT @Correlativo=Cast(correlativo as Int)+1 from TB_CORRELATIVO_AUXILIARES 
			where tabla='WebOrdersPasajes' and oficina='999'

			Set @NumOrden=Right(('00000000'+Cast(@Correlativo as Varchar(15))),9)
			set @Id=0

			insert into WebOrders(Id_Users,IdCID_WebOrders,Fecha_WebOrders,NumOrder_WebOrders,

			HostIP_VentaWeb,HostName_WebOrders,HttpHead_VentaWeb,WOrderType_Id,Estado_WebOrders,Email_Cliente) 

			values (@IdUser,@IdCID,getdate(),@NumOrden,@UserIP,@UserHost,@HttpHead,@Type,1,@Email_Cliente)

			set @Id=scope_identity()

			update ASIENTO set Id_WebOrders=@Id,Id_Users=@IdUser where CODI_TERMINAL='999' and  (IdCID='1'+@IdCID or IdCID='2'+ @IdCID) and Id_Users=0

			select 'OK'+NumOrder_WebOrders+'|'+ cast(@Id as Varchar(10))as Dato  from WebOrders where Id_WebOrders=@Id	

			Update Tb_Correlativo_Auxiliares set Correlativo=cast(@Correlativo as Varchar(15)) Where tabla='WebOrdersPasajes' and oficina='999'



 If @@Error<>0

       	RollBack Transaction                

    Else

       	Commit Transaction
