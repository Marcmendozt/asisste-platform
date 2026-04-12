

class Request {


    CargandoVistas() {
        var idLoading = document.getElementById("idLoading");
        var hdInicioLoad = document.getElementById("hdInicioLoad");
        var mainLoad = document.getElementById("mainLoad");
        var user = document.getElementById("user");
        var bodydLoad = document.getElementById("bodydLoad");
        idLoading = document.getElementById("idLoading");
        idLoading.classList.add("loading-clock");
        hdInicioLoad.style.display = "none";
        mainLoad.style.display = "none";
        user.style.display = "none";
        bodydLoad.style.backgroundColor = "White";
    }

    TerminarCargando() {
        var idLoading = document.getElementById("idLoading");
        var hdInicioLoad = document.getElementById("hdInicioLoad");
        var mainLoad = document.getElementById("mainLoad");
        var user = document.getElementById("user");
        var bodydLoad = document.getElementById("bodydLoad");
        idLoading = document.getElementById("idLoading");
        idLoading.classList.remove("loading-clock");
        hdInicioLoad.style.display = "";
        mainLoad.style.display = "";
        user.style.display = "";
        bodydLoad.style.backgroundColor = "";
    }



}


var RQST = new Request();




var Servidor = "https://localhost:44375/";




$("user").ready(function () {
    var NombreCompleto = sessionStorage.DTNombreCompleto;
    var Perfil = sessionStorage.DTPerfil;
    if (NombreCompleto != undefined) {
        var Sexo = sessionStorage.DTGenero;
        var Contenido = "";
        Contenido += " <section>";
        if (Sexo == "Hombre") {
            Contenido += " <img src='../imagenes/man.png'/>";
        } else if (Sexo == "Mujer") {
            Contenido += " <img src='../imagenes/woman.png'/>";
        }

        Contenido += " <section>";
        Contenido += " <name>" + NombreCompleto + "</name>";
        Contenido += " <actions><a onclick='CerrarSesion()'>Cerrar sesión</a></actions>";
        Contenido += " </section>";
        Contenido += " </section>";
        document.getElementById("user").innerHTML = Contenido;
        if (Perfil == "Administrador") {

        } else {
            
            $('#idUbicacionesPredeterminadas').remove();
            $('#idMenuAdministracion').remove();
            $('#idMenuJornadas').remove();
        }

    } else {
        window.location = Servidor;

    }



});



function CerrarSesion() {
    sessionStorage.clear();
    var url = Servidor + "Home/CerrarSesion";
    var xhr = new XMLHttpRequest();
    xhr.open("get", url, true);
    xhr.onloadstart = function () {
        RQST.CargandoVistas();
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var respuesta = xhr.responseText.split("_");
            if (respuesta[0] == "Correcto") {
                window.location = Servidor;
            } else if (respuesta[0] == "ERROR") {
                alert("Ocurrio un problema al cerrar la sesión");
            }
        }
    }
    xhr.send();
}

