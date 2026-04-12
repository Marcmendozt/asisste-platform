
var Servidor = "https://localhost:44375/";


window.onload = function () {
    ListarDatosInicio();
}


function ListarDatosInicio() {
   
        var url = Servidor + "Maestros/JListadoInicio";
        var xhr1 = new XMLHttpRequest();
        xhr1.open("post", url, true);
        xhr1.onloadstart = function () {
            //Aqui se bloquea el boton y se bloquea los input mientras se ejecuta la consulta
        }
        xhr1.onreadystatechange = function () {

            if (xhr1.readyState == 4 && xhr1.status == 200) {
                //recibir la información del servidor y operar segun el caso
                Respuesta = JSON.parse(xhr1.response);
                if (Respuesta != null) {
                    JSON.stringify(Respuesta);
                    PintarDatosInicio(Respuesta);
                }
                else {
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.warning('NO SE PUDO TRAER LA INFORMACIÓN');
                }
            }
        }
        xhr1.send();
}







function PintarDatosInicio(Data) {
    if (Data != null) {
        var Welcome = document.getElementById("Welcome");
        var Contenido = "";
        Contenido += "<div class='container-fluid'>";
        Contenido += "<h1>Bienvenido al portal de control de asistencia</h1>";
        Contenido += "<h4 class='text-left'>Hola, "+ Data.NombreCompleto + "</h4>";
        var ConvertirFecha = Data.UltimaConexion.substr(6);
        var FechaActual = new Date(parseInt(ConvertirFecha));
        var MES = FechaActual.getMonth() + 1;
        var DIA = FechaActual.getDate();
        var ANIO = FechaActual.getFullYear();
        var Hora = FechaActual.getHours();
        var Minutos = FechaActual.getMinutes();
        Hora = checkTime(Hora);
        var Fecha = DIA + '/' + MES + '/' + ANIO + ' ' + Hora + ':' + Minutos;

        Contenido += "<h4 class='text-left'>Ultimo acceso : " + Fecha + "</h4>";
        Contenido += "<h4 class='text-left'>Perfil : " + Data.Perfil + "</h4>";
        Contenido += "</div>";
        Welcome.innerHTML = Contenido;
    } else {

        alert("NO SE PUEDEN CARGAN LOS DATOS DE INICIO");
    }
}
  

function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}