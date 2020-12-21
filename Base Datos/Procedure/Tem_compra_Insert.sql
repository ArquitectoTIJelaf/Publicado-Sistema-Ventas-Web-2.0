ALter Procedure Tem_compra_Insert
@NroOrden	varchar(30),
@usuario     varchar(100),
@fecha       varchar(30),
@indicador   varchar(50),
@importe     numeric(15,3),
@turno1      varchar(500),
@turno2      varchar(500)
as
set nocount on
begin
	insert into Tem_compra
	(NroOrden,usuario,fecha,indicador,importe,turno1,turno2)
	values
	(@NroOrden,@usuario,convert(varchar,getdate(),103),@indicador,@importe,@turno1,@turno2)
end
