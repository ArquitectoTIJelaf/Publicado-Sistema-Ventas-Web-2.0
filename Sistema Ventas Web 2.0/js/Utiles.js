
/***********Validar Fecha Anterior Fecha Actual**********/
function ValidateDateBefore(sender, args) {
    var fecha = new Date()
    if (sender._selectedDate < fecha) {
        MessageModalInformativo("Seleccionar una fecha posterior a la actual", 1)
        fecha.setDate(fecha.getDate() + 1)
        sender._selectedDate = fecha
        sender._textbox.set_Value(sender._selectedDate.format(sender._format))

    }
}
//function ValidateDateBefore(obj, type, message) {
//    var father = $(obj).parent();
//    father.children('.ErrorRequiere').remove();
//    father.children('br').remove();

//    var f = new Date()
//    var dia = '0' + f.getDate();
//    var mes = '0' + (1 + f.getMonth());
//    var ano = f.getFullYear();
//    var day = dia.substring(dia.length - 2);
//    var month = mes.substring(mes.length - 2);
//    var fch = day + '/' + month + '/' + ano

//    var fechausr = $(obj).val();

//    if (CompararFechas(fch, fechausr, type) == false) {
//        father.append('<br/><span class="ErrorRequiere">' + message + '</span>');
//        cont += 1
//    }
//}


function DisplayDateToday(sender, args) {
    if (sender._selectedDate == null) {
        sender._selectedDate = new Date();
    }
}


function CompararDate(fecha1, fecha2, obj, type, message) {
    var father = $(obj).parent();
    father.children('.ErrorRequiere').remove();
    father.children('br').remove();

    if (CompararFechas(fecha1, fecha2, type) == false) {
        father.append('<br/><span class="ErrorRequiere">' + message + '</span>');
        cont += 1
    }
}


function DateDifActual(fecha1, fecha2, obj, type, message) {
    var father = $(obj).parent();
    father.children('.ErrorRequiere').remove();
    father.children('br').remove();

    if (CompararFechas(fecha1, fecha2, type) == false) {
        father.append('<br/><span class="ErrorRequiere">' + message + '</span>');
        cont += 1
    }
}


function CompararFechas(Obj1, Obj2, type) {
    String1 = Obj1;
    String2 = Obj2;
    // Si los dias y los meses llegan con un valor menor que 10 
    // Se concatena un 0 a cada valor dentro del string 
    if (String1.substring(1, 2) == "/") {
        String1 = "0" + String1
    }
    if (String1.substring(4, 5) == "/") {
        String1 = String1.substring(0, 3) + "0" + String1.substring(3, 9)
    }

    if (String2.substring(1, 2) == "/") {
        String2 = "0" + String2
    }
    if (String2.substring(4, 5) == "/") {
        String2 = String2.substring(0, 3) + "0" + String2.substring(3, 9)
    }

    dia1 = String1.substring(0, 2);
    mes1 = String1.substring(3, 5);
    anyo1 = String1.substring(6, 10);
    dia2 = String2.substring(0, 2);
    mes2 = String2.substring(3, 5);
    anyo2 = String2.substring(6, 10);


    if (dia1 == "08") // parseInt("08") == 10 base octogonal
        dia1 = "8";
    if (dia1 == '09') // parseInt("09") == 11 base octogonal
        dia1 = "9";
    if (mes1 == "08") // parseInt("08") == 10 base octogonal
        mes1 = "8";
    if (mes1 == "09") // parseInt("09") == 11 base octogonal
        mes1 = "9";
    if (dia2 == "08") // parseInt("08") == 10 base octogonal
        dia2 = "8";
    if (dia2 == '09') // parseInt("09") == 11 base octogonal
        dia2 = "9";
    if (mes2 == "08") // parseInt("08") == 10 base octogonal
        mes2 = "8";
    if (mes2 == "09") // parseInt("09") == 11 base octogonal
        mes2 = "9";

    dia1 = parseInt(dia1);
    dia2 = parseInt(dia2);
    mes1 = parseInt(mes1);
    mes2 = parseInt(mes2);
    anyo1 = parseInt(anyo1);
    anyo2 = parseInt(anyo2);

    //    if (anyo1 > anyo2) {
    //        return false;
    //    }

    //    if ((anyo1 == anyo2) && (mes1 > mes2)) {
    //        return false;
    //    }
    if (type == '1') {
        if ((anyo1 == anyo2) && (mes1 == mes2) && (dia1 >= dia2)) {
            return false;
        }
    } else if (type == '2') {
        if ((anyo1 == anyo2) && (mes1 == mes2) && (dia1 > dia2)) {
            return false;
        }
    } else if (type == '3') {
        if ((anyo1 == anyo2) && (mes1 == mes2) && (dia1 == dia2)) {
            return false;
        }
    } else if (type == '4') {
        if ((anyo1 == anyo2) && (mes1 == mes2) && (dia1 < dia2)) {
            return false;
        }
    }


    return true;
}

function MessageModalInformativo(message, type) {
    var width = 300;
    var height = 150;
    var heighttext = 30;
    var length = message.length
    var width = width + ((parseInt(length / 40)) * 12);
    var height = height + ((parseInt(length / 40)) * 12);
    var heighttext = heighttext + ((parseInt(length / 40)) * 12);


    var top = parseInt((height + 10) / 2);
    var left = parseInt((width + 10) / 2);
    var background = $(document.createElement('div')).addClass('Message-Background');
    var box = $(document.createElement('div')).css({ 'margin-top': '-' + top + 'px', 'margin-left': '-' + left + 'px' }).addClass('Message-Box');
    var cont_box = $(document.createElement('div')).css({ 'width': width + 'px', 'height': height + 'px' }).addClass('Message-Content');
    var title_box = $(document.createElement('div')).addClass('Message-Title');
    var iconx_close = $(document.createElement('div')).addClass('Title-Close').html('<a href="#" style="cursor:pointer;" class="icon">X</a>').click(function () {
        $('.Message-Background').remove();
        $(this).parents('.Message-Box').remove();
        $('.editname').focus();

    });
    var buttom_close = $(document.createElement('div')).addClass('Barra-Close').html('<button type="button" class="boton">Aceptar</button>').click(function () {
        $('.Message-Background').remove();
        $(this).parents('.Message-Box').remove();
        $('.editname').focus();

    });
    var cont_msg = $(document.createElement('div')).addClass('Cont-Message');
    if (type == 0) {
        var icon_msg = $(document.createElement('div')).css({ 'float': 'left' }).addClass('Icon-Error');
    } else if (type == 1) {
        var icon_msg = $(document.createElement('div')).css({ 'float': 'left' }).addClass('Icon-Warning');
    } else if (type = 2) {
        var icon_msg = $(document.createElement('div')).css({ 'float': 'left' }).addClass('Icon-Info');
    }

    var msg = $(document.createElement('div')).css({ 'float': 'right', 'width': (width - 50) + 'px', 'height': heighttext + 'px' }).addClass('Msg');
    msg.html(message)
    var line = $(document.createElement('div')).addClass('Line');

    title_box.append('<p class="left">Aviso del sistema</p>')
    title_box.append(iconx_close)
    cont_msg.append(icon_msg)
    cont_msg.append(msg)
    cont_box.append(title_box)
    cont_box.append(cont_msg)
    cont_box.append(line)


    cont_box.append(buttom_close)

    box.append(cont_box)
    $('body').append(background);
    $('body').append(box);
    $('.boton').focus();
}

function Trim(cadena) {
    return $.trim(cadena);
}

function ValidarCaja(obj, expreg, message, empty, length, lengthmax) {
    var texto = Trim($(obj).val());
    if (empty == false) {
        var father = $(obj).parent();
        father.children('.ErrorRequiere').remove();
        father.children('br').remove();

        if (texto != '') {
            if (texto.match(expreg) == null) {
                father.append('<br/><span class="ErrorRequiere">' + message + '</span>');
                cont += 1
            }
            else if (length == true) {
                if (texto.length > lengthmax) {
                    father.append('<br/><span class="ErrorRequiere">Se requiere un máximo de ' + lengthmax + ' caracteres</span>');
                    cont += 1
                }
            }
        }
    }
    else if (empty == true) {
        var father = $(obj).parent();
        father.children('.ErrorRequiere').remove();
        father.children('br').remove();
        if (texto == '') {
            father.append('<br/><span class="ErrorRequiere"> Se requiere este valor</span>');
            cont += 1
        }
        else if (texto.match(expreg) == null) {
            father.append('<br/><span class="ErrorRequiere">' + message + '</span>');
            cont += 1
        }
        else if (length == true) {
            if (texto.length > lengthmax) {
                father.append('<br/><span class="ErrorRequiere">Se requiere un máximo de ' + lengthmax + ' caracteres</span>');
                cont += 1
            }
        }
    }
}

function ValidarLista(obj, message) {
    var father = $(obj).parent();
    father.children('.ErrorRequiere').remove();
    father.children('br').remove();
    //alert($(obj + ' option:selected').attr('value'))
    if ($(obj + ' option:selected').attr('value') == "" || $(obj + ' option:selected').attr('value') == "0" || $(obj + ' option:selected').attr('value') == "X") {
        father.append('<br><span class="ErrorRequiere"> ' + message + ' </span>');
        cont += 1;
    } else if ($(obj).attr('value') == "" || $(obj).attr('value') == "0" || $(obj).attr('value') == "X") {
        father.append('<br><span class="ErrorRequiere"> ' + message + ' </span>');
        cont += 1;
    }
}
function CompareLista(obj1, obj2, message, type) {
    if ($(obj1 + ' option:selected').attr('value') != '' && $(obj2 + ' option:selected').attr('value') != '') {
        var father = $(obj2).parent();
        father.children('.ErrorRequiere').remove();
        father.children('br').remove();
        if (type == 'different') {
            if ($(obj1 + ' option:selected').attr('value') == $(obj2 + ' option:selected').attr('value')) {
                father.append('<br><span class="ErrorRequiere"> ' + message + ' </span>');
                cont += 1;
            }
        }
    }

}
function ValidarListText(obj, message) {
    var father = $(obj).parent();
    father.children('.ErrorRequiere').remove();
    father.children('br').remove();
    if ($(obj + ' option').length < 1) {
        father.append('<br><span class="ErrorRequiere">' + message + '</span>');
        cont += 1;
    }
}
function ClearMessage(obj) {
    var father = $(obj).parent();
    father.children('.ErrorRequiere').remove();
    father.children('br').remove();
}
function ValorError(obj, valor) {
    var texto = Trim($(obj).val());
    var father = $(obj).parent();
    father.children('.ErrorRequiere').remove();
    father.children('br').remove();
    var condicion = true;
    if (texto == valor)
    { condicion = false; cont += 1; father.append('<br><span class="ErrorRequiere"> El valor ingresado <br/>no esta permitido</span>'); } else { condicion = true; }

    return condicion;
}

var cont = 0;
var exprregletras = "^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$";
var exprregnumeros = "^[0-9]+$";
var exprregbrevete = "^[A-ZÑ]{1,10}\[0-9]{1,10}$";
var exprregprecios2 = "^[0-9]+[.]{1,1}\[0-9]{2,2}$";
var exprregalfanumericos = "^[ a-zA-ZñÑ0-9áéíóúÁÉÍÓÚ]+$";
var exprregnombres = "^[A-ZÑa-zñ' ]+$";
var exprregrz = "^[0-9A-ZÑa-zñ&'.- ]+$";
var exprregruc = "^[1-2]{1,1}\[0]{1,1}\[0-9]{9,9}$";
var exprregmail = "^[0-9A-Za-zñÑ-_.]{1,50}\[@]{1,1}\[0-9A-Za-zñÑ-_.]{1,100}\[.]{1,1}\[a-z]{1,6}";
var exprretelefononac = "^[0-9]{1,2}\[-]{1,1}\[0-9]+$";
var exprretelefonoext = "^[0-9]{1,2}\[-]{1,1}\[0-9]{1,2}\[-]{1,1}\[0-9]+$";
var exprredireccion = "^[ 0-9a-zA-ZñÑáéíóúÁÉÍÓÚ.,/°-]+$";
var exprrevariado = "^[ 0-9a-zA-ZñÑáéíóúÁÉÍÓÚ)($:_,./°-]+$";
var exprrehorarisinmer = "^[0-9]{2,2}\[:]{1,1}\[0-9]{2,2}$";
var exprrefecha = "^[0-9]{2,2}\[/]{1,1}\[0-9]{2,2}\[/]{1,1}\[0-9]{4,4}$";
var exprrehora = "^[0-9]{2,2}\[:]{1,1}\[0-9]{2,2}\[A,P,M]{2,2}$";
var exprreplaca = "^[0-9A-ZÑ]{2,4}\[-]{1,1}\[0-9A-ZÑ]{2,6}$";

var messageletras = "Solo se permite caracteres alfabeticos";
var messagenumeros = "Solo se permite caracteres numericos";
var messagebrevete = "El formato de brevete debe ser alfanumerico";
var messageprecios2 = "Formato de precio no valido (2.00)";
var messagealfanumericos = "Solo se permite caracteres alfanumericos";
var messagenombres = "Solo se permite caracteres alfabeticos y apostrofes";
var messagerz = "Solo se permite caracteres alfanumericos y apostrofes";
var messageruc = "El R.U.C. es invalido";
var messagemail = "El formato de email ingresado no es correcto";
var messagetelefononac = "El formato de telefono debe ser:(01-2870973)";
var messagetelefonoext = "El formato de telefono debe ser:(51-01-2870973)";
var messagedireccion = "Solo se permite caracteres alfanumericos <br>y algunos caracteres especiales";
var messagevariado = "Solo se permite caracteres alfanumericos <br>y algunos caracteres especiales";
var messagehorarisinmer = "El formato de hora debe ser:(00:00)";
var messagefecha = "El formato de fecha debe ser:(01/01/2000)";
var messagehora = "El formato de hora debe ser:(12:00PM ó 12:00AM)";
var messageplaca = "El formato correcto es XXX-XXXX";