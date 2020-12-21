Create Table Tb_Info_TransaccionVisaNet
(
Id_WebOrders		int,
Nro_Orden			Int,
Importe_Autorizado	Real,
NumTarjetaHabiente	Varchar(200),
NomTarjeta			Varchar(100),
Transaccion_Id		Varchar(20),
Fecha_Compra		SmallDateTime,
Estado_Transaccion	Varchar(50),
Cod_Accion			Varchar(5),
Mensaje_Error		Varchar(250),
Trans_Fraude		Bit
)

