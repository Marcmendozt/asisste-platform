







var Servidor = "https://localhost:44375/";

window.onload = function () {
    TraerListado();

}

function TraerListado() {
    var url = Servidor + "Maestros/JListadoAsistenciaPerfil";
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
        RQST.CargandoVistas();
    }
    xhr.onreadystatechange = function () {

        if (xhr.readyState == 4 && xhr.status == 200) {
            //recibir la información del servidor y operar segun el caso
            Respuesta = JSON.parse(xhr.response);
            if (Respuesta != null) {
                JSON.stringify(Respuesta.Data);
                if (Respuesta.MensajeError == 'SE ACABO LA SESIÓN') {
                    alert(Respuesta.MensajeError);
                    window.location = Servidor;

                } else if (Respuesta.Data.length != 0) {
                    ListarAsistencia(Respuesta.Data);
                    RQST.TerminarCargando();
                } else if (Respuesta.ExisteError == true) {
                    alert(Respuesta.MensajeError);
                    RQST.TerminarCargando();
                } else if (Respuesta.Data.length == 0) {
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.error("NO HAY REGISTROS");
                    RQST.TerminarCargando();
                }

            }
            else {
                WebNotifyAsBlock("error", "NO SE PUDO TRAER LA INFORMACIÓN", "ERROR!!");
            }
        }
    }
    xhr.send();
}


function Filtrar() {
    var dtInicio = document.getElementById("dtInicio");
    var dtFin = document.getElementById("dtFin");
    if (dtInicio.value != "" && dtFin.value != "") {
        var url = Servidor + "Maestros/JListadoAsistenciaFiltradoPerfil";
        var Formulario = new FormData();

        Formulario.append("FechaInicio", dtInicio.value);
        Formulario.append("FechaFin", dtFin.value);
        var xhr = new XMLHttpRequest();
        xhr.open("post", url, true);

        xhr.onloadstart = function () {
            RQST.CargandoVistas();
        }
        xhr.onreadystatechange = function () {

            if (xhr.readyState == 4 && xhr.status == 200) {
                //recibir la información del servidor y operar segun el caso
                Respuesta = JSON.parse(xhr.response);
                if (Respuesta != null) {
                    JSON.stringify(Respuesta.Data);
                    if (Respuesta.MensajeError == 'SE ACABO LA SESIÓN') {
                        alert(Respuesta.MensajeError);
                        window.location = Servidor;

                    } else if (Respuesta.Data.length != 0) {
                        ListarAsistencia(Respuesta.Data);
                        RQST.TerminarCargando();
                    } else if (Respuesta.ExisteError == true) {
                        alert(Respuesta.MensajeError);
                        RQST.TerminarCargando();
                    } else if (Respuesta.Data.length == 0) {
                        alertify.set('notifier', 'position', 'top-center');
                        alertify.error("NO HAY REGISTROS");
                        RQST.TerminarCargando();
                    }

                }
                else {
                    WebNotifyAsBlock("error", "NO SE PUDO TRAER LA INFORMACIÓN", "ERROR!!");
                }
            }
        }
        xhr.send(Formulario);
    } else {
        alertify.set('notifier', 'position', 'top-center');
        alertify.warning("SELECCIONE UNA FECHA ANTES DE FILTRAR");

    }
    
}








function ListarAsistencia(DataAsistencias) {
    var idTBAsistencia = document.getElementById("idTBAsistencia");
    var Contador = 1;
    var Contenido = "";
    var Perfil = sessionStorage.DTPerfil;
    Contenido += "<div class='col-lg-12'>";
    Contenido += "<table id='TBAsistencias' class='table'>";
    Contenido += "<thead>";
    Contenido += "<tr>";
    Contenido += "<th>#</th>";
    if (Perfil == "Administrador") {
        Contenido += "<th>APELLIDOS</th>";
        Contenido += "<th>NOMBRES</th>";
        Contenido += "<th>NRO DOCUMENTO</th>";
    }

    Contenido += "<th>FECHA</th>";
    Contenido += "<th>ENTRADA</th>";
    Contenido += "<th>SALIDA</th> ";
    Contenido += "<th>TARDANZA</th> ";
    Contenido += "<th>LUGAR DE TRABAJO</th> ";
    Contenido += "<th>ASISTENCIA</th>";
    Contenido += "<th>RANGO</th>";
    Contenido += "</tr>";
    Contenido += "</thead>";
    Contenido += "<tbody>";

    for (var i = 0; i < DataAsistencias.length; i++) {
        Contenido += "<tr>";
        //Fecha
        var ConvertirFecha = DataAsistencias[i].Fecha.substr(6);
        var FechaActual = new Date(parseInt(ConvertirFecha));
        var MES = FechaActual.getMonth() + 1;
        var DIA = FechaActual.getDate();
        var ANIO = FechaActual.getFullYear();
        var Fecha = DIA + '/' + MES + '/' + ANIO;
        //Hora Ingreso
        var HORA = DataAsistencias[i].HoraIngresoReal.Hours;
        var MINUTOS = DataAsistencias[i].HoraIngresoReal.Minutes;
        var SEGUNDOS = DataAsistencias[i].HoraIngresoReal.Seconds;
        HORA = checkTime(HORA);
        MINUTOS = checkTime(MINUTOS);
        SEGUNDOS = checkTime(SEGUNDOS);
        var HI = HORA + ':' + MINUTOS + ':' + SEGUNDOS;

        //Hora Salida
        var HORA = DataAsistencias[i].HoraSalidaReal.Hours;
        var MINUTOS = DataAsistencias[i].HoraSalidaReal.Minutes;
        var SEGUNDOS = DataAsistencias[i].HoraSalidaReal.Seconds;
        HORA = checkTime(HORA);
        MINUTOS = checkTime(MINUTOS);
        SEGUNDOS = checkTime(SEGUNDOS);
        var HS = HORA + ':' + MINUTOS + ':' + SEGUNDOS;
        //Horas de tardanza
        var HORATARDANZA = DataAsistencias[i].RetrasoEntrada.Hours;
        var MINUTOSTARDANZA = DataAsistencias[i].RetrasoEntrada.Minutes;


        var Falta = DataAsistencias[i].Falta;
        var ValorTardanza = "";
        var ValorFalta = "";
        Contenido += "<td> " + Contador++ + " </td>";
        if (Perfil == "Administrador") {
            Contenido += "<td> " + DataAsistencias[i].Apellidos + " </td>";
            Contenido += "<td> " + DataAsistencias[i].Nombres + " </td>";
            Contenido += "<td> " + DataAsistencias[i].NumeroDocumento + " </td>";
        }
        Contenido += "<td> " + Fecha + " </td>";
        Contenido += "<td> " + HI + " </td>";
        Contenido += "<td> " + HS + " </td>";
        if (HORATARDANZA > 0 || MINUTOSTARDANZA > 0) {
            ValorTardanza = "Tarde";
        } else {
            ValorTardanza = "Temprano";
        }
        Contenido += "<td> " + ValorTardanza + " </td>";
        if (Falta > 0) {
            ValorFalta = "NO ASISTIO";
        } else {
            ValorFalta = "ASISTIO";
        }
        Contenido += "<td> " + DataAsistencias[i].LugarTrabajo + " </td>";
        Contenido += "<td> " + ValorFalta + " </td>";
        Contenido += "<td> " + DataAsistencias[i].Geovalla + " </td>";
        Contenido += "</tr>";
    }
    Contenido += "</tr>";
    Contenido += "</tbody>";
    Contenido += "</table>";
    Contenido += "</div>";
    idTBAsistencia.innerHTML = Contenido;

    $(document).ready(function () {
        var TBAsistencia = $('#TBAsistencias').DataTable({
            
            lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, "Todos"]],
            language: {
                lengthMenu: "Mostrar _MENU_ registros por página",
                info: "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                search: "Buscar:",
                paginate: {
                    "first": "Primero",
                    "last": "Último",
                    "next": "Siguiente",
                    "previous": "Anterior"
                },
                
            },
            dom: 'Blfrtip',
            buttons: [
                'excel', 'pdf',
            ],


        });
    });
   

}



function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}



