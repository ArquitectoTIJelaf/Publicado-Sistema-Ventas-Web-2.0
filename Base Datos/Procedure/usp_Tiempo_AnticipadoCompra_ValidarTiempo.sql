ALter Procedure usp_Tiempo_AnticipadoCompra_ValidarTiempo
@Cod_Origen		smallint,
@Cod_Destino	smallint,
@Minutos			smallint
as
	Select Top 1 1 From Tb_Tiempo_AnticipadoCompra
	Where 
	Cod_Origen		=	@Cod_Origen		and 
	Cod_Destino		=	@Cod_Destino	and
	Minutos			<	@Minutos
	Union
	Select Top 1 1 From Tb_Tiempo_AnticipadoCompra
	Where 
	Cod_Origen		=	0				and 
	Cod_Destino		=	@Cod_Destino	and
	Minutos			<	@Minutos
	Union
	Select Top 1 1 From Tb_Tiempo_AnticipadoCompra
	Where 
	Cod_Origen		=	@Cod_Origen		and 
	Cod_Destino		=	0				and
	Minutos			<	@Minutos
	Union
	Select Top 1 1 From Tb_Tiempo_AnticipadoCompra
	Where 
	Cod_Origen		=	0		and 
	Cod_Destino		=	0				and
	Minutos			<	@Minutos
