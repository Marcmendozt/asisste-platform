var Servidor = "https://localhost:44375/";

window.onload = function () {
    TraerListado();
}

var DataAsistencias;
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
                    DataAsistencias = Respuesta.Data;
                    ListarControlDeAsistencia(Respuesta.Data);
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












function ListarControlDeAsistencia(Data) {
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
    Contenido += "<th>UBICACIÓN DE ENTRADA</th>";
    Contenido += "<th>SALIDA</th> ";
    Contenido += "<th>UBICACIÓN DE SALIDA</th>";
    Contenido += "<th>TARDANZA</th> ";
    Contenido += "<th>LUGAR DE TRABAJO</th> ";
    Contenido += "<th>ASISTENCIA</th>";
    Contenido += "<th>RANGO</th>";
    Contenido += "</tr>";
    Contenido += "</thead>";
    Contenido += "<tbody>";
    for (var i = 0; i < Data.length; i++) {
        Contenido += "<tr>";
        //Fecha
        var ConvertirFecha = Data[i].Fecha.substr(6);
        var FechaActual = new Date(parseInt(ConvertirFecha));
        var MES = FechaActual.getMonth() + 1;
        var DIA = FechaActual.getDate();
        var ANIO = FechaActual.getFullYear();
        var Fecha = DIA + '/' + MES + '/' + ANIO;
        //Hora Ingreso
        var HORA = Data[i].HoraIngresoReal.Hours;
        var MINUTOS = Data[i].HoraIngresoReal.Minutes;
        var SEGUNDOS = Data[i].HoraIngresoReal.Seconds;
        HORA = checkTime(HORA);
        MINUTOS = checkTime(MINUTOS);
        SEGUNDOS = checkTime(SEGUNDOS);
        var HI = HORA + ':' + MINUTOS + ':' + SEGUNDOS;

        //Hora Salida
        var HORA = Data[i].HoraSalidaReal.Hours;
        var MINUTOS = Data[i].HoraSalidaReal.Minutes;
        var SEGUNDOS = Data[i].HoraSalidaReal.Seconds;
        HORA = checkTime(HORA);
        MINUTOS = checkTime(MINUTOS);
        SEGUNDOS = checkTime(SEGUNDOS);
        var HS = HORA + ':' + MINUTOS + ':' + SEGUNDOS;
        //Horas de tardanza
        var HORATARDANZA = Data[i].RetrasoEntrada.Hours;
        var MINUTOSTARDANZA = Data[i].RetrasoEntrada.Minutes;


        var Falta = Data[i].Falta;
        var ValorTardanza = "";
        var ValorFalta = "";

        Contenido += "<td> " + Contador++ + " </td>";
        if (Perfil == "Administrador") {
            Contenido += "<td> " + Data[i].Apellidos + " </td>";
            Contenido += "<td> " + Data[i].Nombres + " </td>";
            Contenido += "<td> " + Data[i].NumeroDocumento + " </td>";
        }

        Contenido += "<td> " + Fecha + " </td>";
        Contenido += "<td> " + HI + " </td>";
        var UbicacionEntrada = 1;
        var UbicacionSalida = 2;
        Contenido += "<td><a class='material-icons'; onclick='AbrirUbicacion(" + Data[i].ID_Asistencia + ',' + UbicacionEntrada +" )'>location_on</a></td>";
        Contenido += "<td> " + HS + " </td>";
        Contenido += "<td><a class='material-icons'; onclick='AbrirUbicacion(" + Data[i].ID_Asistencia + ',' + UbicacionSalida +"  )'>location_on</a></td>";
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
        Contenido += "<td> " + Data[i].LugarTrabajo + " </td>";
        Contenido += "<td> " + ValorFalta + " </td>";
        Contenido += "<td> " + Data[i].Geovalla + " </td>";
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
               
            ],


        });
    });

  
}

function AbrirUbicacion(IDAsistencia,ID) {
    $("#idModalMostrarUbicacion").modal('show');
    var Contenido = "";
    for (var i = 0; i < DataAsistencias.length; i++) {
        if (DataAsistencias[i].ID_Asistencia == IDAsistencia) {
            if (ID == 1) {
                var LatLong = DataAsistencias[i].UbicacionIngreso;
            } else {
                var LatLong = DataAsistencias[i].UbicacionSalida;
            }
          
        }
    }

    var NuevaUbicacion = LatLong.split(",");
    var Latitud = NuevaUbicacion[0];
    var Longitud = NuevaUbicacion[1];


    Contenido += "<div class='form-group row'>";
    Contenido += "<div class='col-sm-6'>";
    Contenido += "<label class='control-label'>Latitud.:</label>";
    Contenido += "<input type='text' class='form-control' id='txtLatitud' value=" + Latitud + " />";
    Contenido += "</div>";
    Contenido += "<div class='col-sm-6'>";
    Contenido += "<label>Longitud:</label>";
    Contenido += "<input type='text' class='form-control' id='txtLongitud'value=" + Longitud + " />";
    Contenido += "</div>";
    Contenido += "</div>";
    Contenido += "<div id='map_canvas' style='height: 354px; width:470px; border: 1px solid black;'></div>";
    idModalBody.style.display = "";
    idmf.style.display = "none";
    exampleModalLongTitle.innerHTML = "UBICACIÓN DE USUARIO";
    idModalBody.innerHTML = Contenido;
    document.getElementById("txtLatitud").disabled = true;
    document.getElementById("txtLongitud").disabled = true;
    var directionsDisplay, map;
    var geocoder = new google.maps.Geocoder;
    var infowindow = new google.maps.InfoWindow;
    directionsDisplay = new google.maps.DirectionsRenderer();
    var chicago = new google.maps.LatLng(parseFloat(Latitud), parseFloat(Longitud));
    var mapOptions = { zoom: 20, mapTypeId: google.maps.MapTypeId.ROADMAP, center: chicago }
    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    directionsDisplay.setMap(map);

    var latlng = {
        lat: parseFloat(Latitud),
        lng: parseFloat(Longitud)
    };
    geocoder.geocode({
        'location': latlng
    }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            if (results[1]) {
                map.setZoom(20);
                markerAbrirUbicacion = new google.maps.Marker({
                    position: latlng,
                    map: map,
                    animation: google.maps.Animation.DROP
                });
                infowindow.setContent(results[1].formatted_address);
                infowindow.open(map, markerAbrirUbicacion);
            } else {
                window.alert('No hay resultados');
            }
        } else {
            window.alert('Geocoder failed due to: ' + status);
        }
    });

}

var markerAbrirUbicacion;
function toggleBounce() {
    if (marker.getAnimation() !== null) {
        marker.setAnimation(null);
    } else {
        var geocoder = new google.maps.Geocoder;
        var infowindow = new google.maps.InfoWindow;
        marker.setAnimation(google.maps.Animation.BOUNCE);

    }
}

function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}





