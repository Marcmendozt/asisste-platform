



var Servidor = "https://localhost:44375/";

function EnviarCorreo(){

    var txtUsuario = document.getElementById("txtUsuario");

    if (txtUsuario.value != "") {

        var URL = Servidor + "Maestros/RecuperarCorreo";
        var Formulario = new FormData();
        Formulario.append("Usuario", txtUsuario.value);
        var xhr = new XMLHttpRequest();
        xhr.open("post", URL, true);
        xhr.onloadstart = function () {

        }
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                let Respuesta = xhr.responseText.split("_");
                if (Respuesta[0] == "Correcto") {
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.success('CORREO ENVIADO CORRECTAMENTE VERIFIQUE SU BUZÓN');
                    setTimeout(window.location = Servidor, 3000);
            
               
                } else if (Respuesta[0] == "Error") {
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.error(Respuesta[1]);
                  
                }
            }
        }
        xhr.send(Formulario);
    } else {
        alertify.set('notifier', 'position', 'top-center');
        alertify.warning("ESCRIBA EL CORREO");
    }
}

function Volver() {
    window.location = Servidor;
}










