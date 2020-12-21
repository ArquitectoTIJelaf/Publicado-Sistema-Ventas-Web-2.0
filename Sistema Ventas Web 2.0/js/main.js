/// <reference name="MicrosoftAjax.js"/>
$(document).ready(function() {
$('.terminos').click(function(e) {
        e.preventDefault();
        popUp('html/terminos_condiciones.htm', 741, 427, 'iframe')
        return false;
    })
    $('.oficina').click(function() {
        popUp('puntosventas.aspx', 616, 500, 'iframe')
        return false;
    })
});
   
function MainValidation() {

    $(document).ready(function() {

        $("ul.subnav").parent().append("<span></span>"); //Only shows drop down trigger when js is enabled (Adds empty span tag after ul.subnav*)

        $("ul.topnav li span").click(function() { //When trigger is clicked...
            //Following events are applied to the subnav itself (moving subnav up and down)
            $(this).parent().find("ul.subnav").slideDown('fast').show(); //Drop down the subnav on click
            $(this).parent().hover(function() {
            }, function() {
                $(this).parent().find("ul.subnav").slideUp('slow'); //When the mouse hovers out of the subnav, move it back up
            });
            //Following events are applied to the trigger (Hover events for the trigger)
        }).hover(function() {
            $(this).addClass("subhover"); //On hover over, add class "subhover"
        }, function() {	//On Hover Out
            $(this).removeClass("subhover"); //On hover out, remove class "subhover"
        });

    });

}

getScrollPos = function() {
    var docElem = document.documentElement;
    this.scrollX = self.pageXOffset || (docElem && docElem.scrollLeft) || document.body.scrollLeft;
    this.scrollY = self.pageYOffset || (docElem && docElem.scrollTop) || document.body.scrollTop;
}

getPageSize = function() {
    var docElem = document.documentElement
    this.width = self.innerWidth || (docElem && docElem.clientWidth) || document.body.clientWidth;
    this.height = self.innerHeight || (docElem && docElem.clientHeight) || document.body.clientHeight;
}


function MM_preloadImages() { //v3.0
    var d = document; if (d.images) {
        if (!d.MM_p) d.MM_p = new Array();
        var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
            if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; } 
    }
}
function MM_swapImgRestore() { //v3.0
    var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
}
function MM_findObj(n, d) { //v4.01
    var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
        d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
    }
    if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
    for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
    if (!x && d.getElementById) x = d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
    var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
        if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
}




function pageLoad() {
    ShowBox();
}
function ShowLoad() {
    //ShowProgress() 
}
function PosicionarCapas() {
    ShowBox();
}

function ShowBox() {
    var oDivFweb = $get('divFrameWeb');
    if (oDivFweb != null && oDivFweb != 'null' && oDivFweb != 'undefine') {
        var pagesize = new getPageSize();
        var scrollPos = new getScrollPos();
        oDivFweb.style.width = pagesize.width + scrollPos.scrollX + 'px';
        oDivFweb.style.height = pagesize.height + scrollPos.scrollY + 'px';
    }
}
function ShowProgress() {
    ShowBox();
    var oDivProgss = $get('ctl00_WUCPrgss1_upPWeb');
    if (oDivProgss != null && oDivProgss != 'null' && oDivProgss != 'undefine') {
        oDivProgss.style.display = 'block';
        //alert(oDivProgss.style.display);
    } else { return false; }
}

function pageUnload() {


}

function ExitBefore() {
    ShowProgress();
}

//function AlertasAlCargar()
//{
//    alert(shoping);
//}

//document.body.onbeforeunload=ExitBefore;
//window.onbeforeunload = ExitBefore;


//function IsNumeric(passedVal)

//{
//    var ValidChars = "0123456789";
//    var IsNumber=true;
//    var Char;
//    if(passedVal == "")
//	    {return false;}
//	        for (i = 0; i < passedVal.length && IsNumber == true; i++)
//	        {
//		        Char = passedVal.charAt(i);
//		            if (ValidChars.indexOf(Char) == -1)
//		            {
//		            IsNumber = false;
//		            }
//		        }
//	return IsNumber;
//}

//]]>
//function MensajeModal(mensaje)
//{
//    alert(mensaje);
//}   
//function ValidateDateLost(sender,args) 
//{
//    var fecha=new Date()
//    if(sender._selectedDate < fecha)    {
//        MensajeModal("No puede seleccionar una fecha anterior al de mañana");
//        fecha.setDate(fecha.getDate()+1)
//        sender._selectedDate=fecha
//        sender._textbox.set_Value (sender._selectedDate.format (sender._format))
//    }
//    
//}

//function DisplayDateToday (sender, args) {
//    if (sender._selectedDate == null) {
//        sender._selectedDate = new Date();
//    }
//}

$(function() {
    $(".tabs").tabs();
//    $(".datepicker").datepicker({
//        showOn: "button",
//        buttonImage: "images/calendario.jpg",
//        buttonImageOnly: true,
//        dateFormat: "dd/mm/yy",
//        minDate: 0
//        //                beforeShowDay:function(day){
//        //                    var day = day.getDay();
//        //                    if (day == 0 || day == 6) {
//        //                        return [false, "somecssclass"]    
//        //                    }else{
//        //                        return  [true, "someothercssclass"]  
//        //                    }
//        //                }
//    });
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '<Ant',
        nextText: 'Sig>',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);

});