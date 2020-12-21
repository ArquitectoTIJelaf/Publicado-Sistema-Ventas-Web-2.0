//function preCarga(){
//	if (!document.images) return;
//	var ar = new Array();
//	var arguments = preload.arguments;
//	for(var i = 0; i < arguments.length; i++){
//		ar[i] = new Image();
//		ar[i].src = arguments[i];
//	}
//}

var ScriptManya = {
	popUp : function(optionManya){
		var src = optionManya.src;
		
		var alto = (optionManya.height == '') ? 0 : optionManya.height;
		var ancho = (optionManya.width == '') ? 0 : optionManya.width;
		
		if(optionManya.type == 'image'){
			var foto = new Image();
			foto.src = src;
			ancho = foto.width;
			alto = foto.height;
			var num = 6;
		}else{
			var num = 10;
		}
		
		var top = parseInt((alto + num)/2);
		var left = parseInt((ancho + num)/2);
		
		var background = $(document.createElement('div')).css({'position':'fixed',
														'top': '0px',
														'left': '0px',
														'width': '100%',
														'height': '100%',
														'z-index' : '99999'})
													.addClass('popUp-Background');
		
		var box = $(document.createElement('div')).css({'position':'fixed',
														'top': '50%',
														'left': '50%',
														'width': (ancho + num) +'px',
														'height': (alto + num) +'px',
														'margin-top': '-'+ top +'px',
														'margin-left': '-'+ left +'px',
														'z-index' : '100000'}).addClass('popUp-Box');

		if(optionManya.type == 'iframe'){
			var cbox = $(document.createElement('div')).addClass('popUp-Content');
			var cont = $(document.createElement('iframe')).css({'width': ancho +'px',
																'height': alto +'px',
																'background': 'transparent',
																'border': '0'})
															.attr({
																'frameborder':0,
																'src': src
															})
															.addClass('popUp-Body');
		}else{
			//var cbox = $(document.createElement('div')).addClass('popUp-Content none');
			var cont = $(document.createElement('div')).css({'width': ancho +'px','height': alto +'px'}).addClass('popUp-Body').html('<img src="'+ foto.src +'" />');
		}
		
		var close = $(document.createElement('div')).css({'float':'right'})
													.addClass('popUp-Close')
													.html('<a href="#" style="cursor:pointer;"><div class="icon" id="iconclose">X</div><div class="flotante"></div></a>')
													.click(function() {					    
														$('.popUp-Background').remove();
														$(this).parents('.popUp-Box').remove();
														
													});
		
		cbox.append(close);
		cbox.append(cont);
		box.append(cbox);
		$('body').append(background);
		$('body').append(box);
	}
	
};

function popUp(page,w,h,type){
	ScriptManya.popUp({
		src:page,
		type:type,
		width:w,
		height:h
	});
	
	
}



