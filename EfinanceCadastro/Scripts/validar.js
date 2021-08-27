$(document).ready(function(){
	$("#form").validate({
		rules:{
			nome:{
				required: true,
				minlength: 3
			},
			codigouf: {
				required: false,
				maxlength: 2
			},
			uf: {
				required: true,
				maxlength: 2
			}
		},
		messages:{
			nome:{
				required: "Digite um nome!",
				minlength: "No minimo 3 letras"
			},
			uf:{
				required: "Digite a UF!",
				email: "Digite uma UF válida!"
			}
		}
	});
});

























































































