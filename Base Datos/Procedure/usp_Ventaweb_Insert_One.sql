alter Procedure [dbo].[usp_Ventaweb_Insert_One]
@IdWebOrder varchar(18), 
@Nota varchar(8000),
@Costo		Numeric(18,2),
@AsientosD	tinyint,
@AsientosG	tinyint,
@Orderventa	tinyint,
@CostoG		real,
@FechaViaje	smalldatetime,
@HoraViaje	Varchar(10),
@OrigenId	smallint,
@DestinoId	smallint,
@RutaDoble	bit,
@Flag_Venta	Varchar(2),
@NumTarjetaCredito	varchar(25),
@Ruc_Cliente	varchar(11)
as
Begin Transaction 
Declare @Id_VentaWeb	Numeric(18)
	declare @agencia_id int

	set @agencia_id=0

	select @agencia_id=agencia_id from agencia where agencia_ruc=@Ruc_Cliente

	insert VentaWeb (Fecha_VentaWeb,Id_WebOrders,Nota_VentaWeb,Costo,AsientoD,AsientoG,Orderventa,CostoG,FechaViaje,HoraViaje,OrigenId,DestinoId,RutaDoble,Flag_Venta,NumTarjetaPago_ventaWeb,idtVirtual)
	values  (Convert(varchar(10),GETDATE(),103),@IdWebOrder,@Nota,@Costo,@AsientosD,@AsientosG,@Orderventa,@CostoG,@FechaViaje,@HoraViaje,@OrigenId,@DestinoId,@RutaDoble,@Flag_Venta,@NumTarjetaCredito,@agencia_id)

	Set @Id_VentaWeb=scope_identity()

	Select @Id_VentaWeb as Id_VentaWeb

 If @@Error<>0
	      Begin
	        	RollBack Transaction                
	      End 
    Else
	     Begin
	       	Commit Transaction  
	     End