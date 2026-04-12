



var Servidor = "https://localhost:44375/";

function IniciarSesion() {

    var email = document.getElementById("email");
    var password = document.getElementById("password");
    if (email.value != "" && password.value != "") {

        var URL = Servidor + "Home/Ingresar";
        var Formulario = new FormData();
        Formulario.append("us", email.value);
        Formulario.append("pw", password.value);

        var xhr = new XMLHttpRequest();
        xhr.open("post", URL, true);
        xhr.onloadstart = function () {
            Cargando();
        }
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {

                let Respuesta = xhr.responseText.split("_");
                if (Respuesta[0] == "Correcto") {

                    sessionStorage.setItem('DTID_Perfil', Respuesta[1]);
                    sessionStorage.setItem('DTID_Persona', Respuesta[2]);
                    sessionStorage.setItem('DTNombreCompleto', Respuesta[3]);
                    sessionStorage.setItem('DTGenero', Respuesta[4]);
                    sessionStorage.setItem('DTPerfil', Respuesta[5]);
                    sessionStorage.setItem('DTIDUsuario', Respuesta[6]);
                    sessionStorage.setItem('DTDocumentoIdentidad', Respuesta[7]);
                    sessionStorage.setItem('DTNombres', Respuesta[8]);
                    sessionStorage.setItem('DTApellidos', Respuesta[9]);
                    sessionStorage.setItem('DTNombreUsuario', Respuesta[10]);
                    window.location = Servidor + "Maestros/Inicio";
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.success('DATOS CORRECTOS - BIENVENIDO.');
                } else if (Respuesta[0] == "Error") {

                    alertify.set('notifier', 'position', 'top-center');
                    alertify.error(Respuesta[1] + " INGRESE LOS DATOS NUEVAMENTE");
                    email.value = "";
                    password.value = "";
                    idLoading.classList.remove("loading-clock");
                    idContainer.style.display = "";
                }
            }
        }
        xhr.send(Formulario);

    } else {
        alertify.set('notifier', 'position', 'top-center');
        alertify.warning("RELLENE DE LOS CAMPOS");

    }
}

var idContainer = document.getElementById("idContainer");
var idLoading;
function Cargando() {
    idLoading = document.getElementById("idLoading");
    idLoading.classList.add("loading-clock");
    idContainer.style.display = "none";
}
