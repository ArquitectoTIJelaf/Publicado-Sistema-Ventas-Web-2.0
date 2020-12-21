Public Class Message

#Region "Mensajes Informativos"
    Public Const IRegisterPasajero As String = "Se registro correctamente el pasajero...Continue"
    Public Const IAvisoRegisterPasajeroIda As String = "Por favor registre ahora los pasajeros del viaje de ida"
    Public Const IAvisoRegisterPasajeroRetorno As String = "Por favor registre ahora los pasajeros del viaje de retorno"
    Public Const ISelectFechaViaje As String = "Acaba de selecionar su fecha de viaje. Por favor  ahora seleccione en la parte inferior su hora de viaje"
    Public Const ISelectFechaViajeIda As String = "Acaba de selecionar su fecha de viaje de ida. Por favor  ahora seleccione en la parte inferior su hora de viaje de ida"
    Public Const ISelectFechaViajeRetorno As String = "Acaba de selecionar su fecha de viaje de retorno. Por favor  ahora seleccione en la parte inferior su hora de viaje de retorno"
    Public Const ILoginSales As String = "Por favor inicio sesion antes de confirmar la compra"
    Public Const IPasajeroNoHallado As String = "Este pasajero no esta registrado. Por favor registrelo o trato con otro pasajero"
    Public Const IHorarioTurnoNoDisponible As String = "No se encontraron programaciones disponibles en el servicio y fecha seleccionada"
    Public Const IChangePassword As String = "Se cambio la contraseña"
    Public Const INoChangePassword As String = "No se cambio la contraseña"

#End Region

#Region "Mensajes Advertencia"
    Public Const WSelectHorarioViajeIda As String = "Seleccione el horario de su viaje de ida"
    Public Const WSelectHorarioViajeRetorno As String = "Seleccione el horario de su viaje de retorno"
    Public Const WSelectHorarioViaje As String = "Seleccione el horario de su viaje"
    Public Const WSelectHorarioViajeIda_Retorno As String = "Seleccione el horario de su viaje de ida y retorno"
    Public Const WHorarioViajeError As String = "Este horario no se encuentra configurado, seleccione otro por favor"
    Public Const WHorarioViajeAsientoOcupada As String = "Este horario ya no tiene asientos disponibles"
    Public Const WRegisterPasajero As String = "No se registro el pasajero. Intentelo nuevamente."
    Public Const WRegisterPasajeroBefor As String = "Por favor primero ingrese el pasajero anterior"
    Public Const WRegisterPasajeroAllIda As String = "Por favor registrar todos los pasajeros de su viaje de ida"
    Public Const WRegisterPasajeroAllRetorno As String = "Por favor registrar todos los pasajeros de su viaje de retorno"
    Public Const WRegisterPasajeroAll As String = "Por favor registrar todos los pasajeros"
    Public Const WFechaViajeIdaInvalida As String = "El intervalo de fechas de viaje de ida y retorno, no se permite por tiempo de viaje de la ruta de viaje de ida. Por favor seleccionar otro rango de fechas"
    Public Const WFechaHoraViajeIdaInvalida As String = "El intervalo de fechas y horas de viaje de ida y retorno, no se permite por tiempo de viaje de la ruta de viaje de ida. Por favor seleccionar otro rango de fechas y horas"
    Public Const WSelectTipoViaje As String = "Por favor seleccione un tipo de viaje"
    Public Const WAsientoEliminados As String = "Su reservacion fue eliminada por exceso de tiempo, o por un administrador"
    Public Const WErrorTransaccion As String = "Lo sentimos ocurrio un problema con el sistema, durante su compra. Por favor intente otra vez."
    Public Const WSelectFormaPago As String = "Por favor selecione una forma de pago"
    Public Const WPrecioNoConfiguradoAsientos As String = "El Precio de este nivel no esta configurado. Disculpe las molestias"
    Public Const WNoVender As String = "Ocurrio un error en sistema. Intente mas tarde. Disculpe las molestias"
    Public Const WFechaTurnoViajeInvalida As String = "No hay turnos de retorno dispoibles, para fecha seleccionada. Por favor seleccione otra fecha de retorno o ida"
    Public Const WFechaHoraTurnoViajeInvalida As String = "No hay turnos de retorno dispoibles, para fecha seleccionada. Por favor seleccione otra fecha y/o hora de retorno o ida"
    Public Const WNoPasajeroRegister As String = "Este pasajero ya fue registrado. Por favor ingrese otro"
    Public Const WSelectAsientos As String = "Debe de terminar de seleccionar los asientos"
    Public Const WHoraEmbarqueNoConfigurada As String = "El punto de embarque seleccionado no fue correctamente configurado. Seleccione otro por favor"
    Public Const WSelectEmbarqueAndDesembarque As String = "Por favor seleccione el Punto de Embarque y/o Punto de Desembarque"


#End Region

#Region "Mensajes Error"
    Public Const EConexionServer As String = "Error al conectarse al SERVIDOR DB"
    Public Const EPage As String = "Error en la página"
    Public Const EPagoTransaccionPasarela As String = "Hubo un problema de comunicacion con la pasarela de pago,por favor reintente. De persistir el problema por favor comunique con nosotros"
    Public Const EPagoTransaccionSystem As String = "Ocurrio un error en el sistema, por favor intente mas tarde. Disculpe las molestias"
    Public Const ERequesETicketNuloFormulario As String = "VisaNet no devolvio ningun ETicket de su pasarela de pago"
    Public Const EXMLRequestQueryWSVisaNetNulo As String = "No se puedo generar el XML con que se iba consultar el web Services de Visa.Net"
    Public Const EXMLRespondQueryWSVisaNetNulo As String = "VisaNet no devolvio ningun XML como resultdado desde su Web Services de resultados"
    Public Const EXMLRespondQueryWSOperacionesNulo As String = "VisaNet no devolvio ningun operacion en su XML que retorno"
    Public Const EEtcketDiferecs As String = "El ETicket devuelto por VisaNet desde su pasarela no es el mismo que genero antes de ir a la pasarela"
    Public Const ETramaJsonAutorizacion As String = "La trama json de VisaNet no creo el objeto de datos"
    Public Const ENuloTramaJsonAutorizacion As String = "La trama json de VisaNet es nula."
    Public Const EVisaNetCodTiendaNumOrdenRetornoNulo As String = "El numero de orden o codigo de comercio devuelpor VisaNet desde su Web services es nulo"
    Public Const ECodTiendaNumOrdenDiferecs As String = "El numero de orden y/o codigo de comercio devuelto por VisaNet desde su Web services no es el mismo que genero antes de ir a la pasarela"
    Public Const ENunOrdeNoGenerado As String = "El numero de orden no fue generado"
    Public Const ENumOrdeNulo As String = "El numero de orden no fue encontrado"
    Public Const EVisaNetMontoNumOrdenDiferecs As String = "El monto develto por Visante VisaNet desde su Web services no coincide con el que se le envio inicialmente"
    Public Const EVisaNetMontoNumOrdenDiferecsCliente As String = "El monto procesado en Visa no corresponde al monto real del pasaje.Por favor seleccione sus asientos nuevamente.De persistir el problema comuniquese con sistemas. El monto procesado se reembolsara en los proximos dias"
    Public Const ERegisterVentaIda As String = "Se registro su viaje de ida. Pero no se pudo registrar su viaje de retorno"
    Public Const ERegisterVentaRetorno As String = "Se registro su viaje de retorno. Pero no se pudo registrar su viaje de ida"

    Public Const ENoRegisterVenta As String = "Sucedio un error y la venta no pudo concreatrse. Comuniquese con la empresa para solucionar el problema de su compra"
    Public Const ENoRegisterReserva As String = "Sucedio un error y la reserva no pudo concreatrse. Comuniquese con la empresa para solucionar el problema de su compra"

    Public Const ERegisterVentaNulo As String = "Error intente de nuevo"
    Public Const EAsientoEliminadosViaje As String = "Los asientos de su viaje fueron eliminados por el administrador o por tiempo de sesion de su compra. Por favor reinicie su compra. De persistir el problema comunique con nosotros"
    Public Const EAsientoEliminadosViajeIda As String = "Los asientos de su viaje de ida fueron eliminados por el administrador o por tiempo de sesion de su compra. Por favor reinicie su compra. De persistir el problema comunique con nosotros"
    Public Const EAsientoEliminadosViajeRetorno As String = "Los asientos de su viaje de retorno fueron eliminados por el administrador o por tiempo de sesion de su compra. Por favor reinicie su compra. De persistir el problema comunique con nosotros"

#End Region

#Region "Etiquetas"
    Public Const TitleHdSelectViaje As String = "Seleccione su Itinerario de Viaje"
    Public Const TitleHdSelectViajeIda As String = "Itinerario de Viaje de Ida"
    Public Const TitleHdSelectViajeRetorno As String = "Itinerario de Viaje de Retorno"
    Public Const ErrorHorarioViajePlanoBus As String = "El Plano de Bus o Codigo Bus no fue configurado para el horario de esta ruta"
    Public Const PrecioNoConfigurado As String = "No Disponible"
    Public Const TitleRutaIda As String = "Itinerario de Ida"
    Public Const TitleRutaRetorno As String = "Itinerario de Retorno"
    Public Const TitleRuta As String = "Ruta de Viaje"

#End Region

End Class
