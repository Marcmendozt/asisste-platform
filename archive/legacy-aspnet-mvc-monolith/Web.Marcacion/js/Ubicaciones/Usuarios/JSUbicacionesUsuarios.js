var Servidor = "https://localhost:44375/";

window.onload = function () {
    ListarUbicacion();

}

var DataUbicacion;




function ListarUbicacion() {
    var url = Servidor + "Maestros/JListadoUbicacion";
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
        RQST.CargandoVistas();
    }
    xhr.onreadystatechange = function () {

        if (xhr.readyState == 4 && xhr.status == 200) {
            Respuesta = JSON.parse(xhr.response);
            if (Respuesta != null) {
                JSON.stringify(Respuesta.Data);
                if (Respuesta.MensajeError == 'SE ACABO LA SESIÓN') {
                    alert(Respuesta.MensajeError);
                    window.location = Servidor;

                } else if (Respuesta.Data.length != 0) {
                    DataUbicacion = Respuesta.Data;
                    ListaUbicaciones(Respuesta.Data);
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



var Ubicacion = ""
function ListaUbicaciones(DataUbicacion) {

    var divTBUbicacion = document.getElementById("divTBUbicacion");
    var Contador = 1;
    var Contenido = "";
    var Perfil = sessionStorage.DTPerfil;
    Contenido += "<div class='col-lg-12'>";
    Contenido += "<table id='IDTBUbicacion' class='table table-bordered'>";
    Contenido += "<thead>";
    Contenido += "<tr>";
    Contenido += "<th>#</th>";
    Contenido += "<th>USUARIO</th>";
    Contenido += "<th>UBICACIÓN</th>";
    if (Perfil == "Administrador") {
        Contenido += "<th>ACCIONES</th>";
    }
    Contenido += "</tr>";
    Contenido += "</thead>";
    Contenido += "<tbody>";
    for (var i = 0; i < DataUbicacion.length; i++) {
        Contenido += "<tr>";
        Contenido += "<td>" + Contador++ + "</td>";
        Contenido += "<td>" + DataUbicacion[i].NombreUsuario + "</td>";
        Contenido += "<td><a class='material-icons'; onclick='AbrirUbicacion(" + DataUbicacion[i].ID_Ubicacion + " )'>location_on</a></td>";

        if (Perfil == "Administrador") {
            Contenido += "<td>";
            Contenido += "<a class='material-icons'; onclick='AbrirEditarUbicacion(" + DataUbicacion[i].ID_Ubicacion + " )'> edit </a>";
            Contenido += "<a id='#Eliminar' class='material-icons' onclick='ModalEliminar(" + DataUbicacion[i].ID_Ubicacion + " )'>delete</a>";
            Contenido += "</td>";
        }

    }
    Contenido += "</tr>";
    Contenido += "</tbody>";
    Contenido += "</table>";
    Contenido += "</div>";
    divTBUbicacion.innerHTML = Contenido;

    $(document).ready(function () {
        var IDTBUbicacion = $('#IDTBUbicacion').DataTable({

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


var idModalBody = document.getElementById("idModalBody");
var exampleModalLongTitle = document.getElementById("exampleModalLongTitle");

function AbrirUbicacion(IDUbicacion) {
    $("#idModalMostrarUbicacion").modal('show');
    var Contenido = "";
    for (var i = 0; i < DataUbicacion.length; i++) {
        if (DataUbicacion[i].ID_Ubicacion == IDUbicacion) {
            var LatLong = DataUbicacion[i].Ubicacion;
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



var idModalPieDePaginaEditar = document.getElementById("idModalPieDePaginaEditar");
var idModalEditarCuerpo = document.getElementById("idModalEditarCuerpo");
var idModalEditarTitulo = document.getElementById("idModalEditarTitulo");
var makerEditarUbicacion;
function AbrirEditarUbicacion(IDUbicacion) {
    if (!!navigator.geolocation) {
        if (navigator.geolocation) navigator.geolocation.getCurrentPosition(function (pos) {

            $("#idModalEditarUbicacion").modal('show');
            var Contenido = "";
            for (var i = 0; i < DataUbicacion.length; i++) {
                if (DataUbicacion[i].ID_Ubicacion == IDUbicacion) {
                    var LatLong = DataUbicacion[i].Ubicacion;
                    var ID_Usuario = DataUbicacion[i].ID_Usuario;
                }
            }

            var NuevaUbicacion = LatLong.split(",");
            var Latitud = NuevaUbicacion[0];
            var Longitud = NuevaUbicacion[1];
            Contenido += "<div class='form-group row'>";
            Contenido += "<div class='col-sm-6'>";
            Contenido += "<label class='control-label'>Latitud.:</label>";
            Contenido += "<input type='text' class='form-control' id='txtEditarLatitud' value=" + Latitud + " />";
            Contenido += "</div>";
            Contenido += "<div class='col-sm-6'>";
            Contenido += "<label>Longitud:</label>";
            Contenido += "<input type='text' class='form-control' id='txtEditarLongitud'value=" + Longitud + " />";
            Contenido += "</div>";
            Contenido += "</div>";
            Contenido += "<div id='Editarmap_canvas' style='height: 354px; width:470px; border: 1px solid black;'></div>";
            ContenidoPiePagina = "";
            ContenidoPiePagina += "<button type='button' onclick='Editar(" + ID_Usuario + ")' class='btn btn-secondary' data-dismiss='modal'>Guardar cambios</button>";
            idModalEditarTitulo.innerHTML = "EDITAR UBICACIÓN";
            idModalEditarCuerpo.innerHTML = Contenido;
            idModalPieDePaginaEditar.innerHTML = ContenidoPiePagina;
            document.getElementById("txtEditarLatitud").disabled = true;
            document.getElementById("txtEditarLongitud").disabled = true;
            var directionsDisplay, map;
            var geocoder = new google.maps.Geocoder;
            directionsDisplay = new google.maps.DirectionsRenderer();
            var chicago = new google.maps.LatLng(parseFloat(Latitud), parseFloat(Longitud));
            var mapOptions = { zoom: 20, mapTypeId: google.maps.MapTypeId.ROADMAP, center: chicago }
            map = new google.maps.Map(document.getElementById("Editarmap_canvas"), mapOptions);
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
                        makerEditarUbicacion = new google.maps.Marker({
                            position: latlng,
                            map: map,
                            draggable: true,

                        });
                        makerEditarUbicacion.addListener('click', toggleBounce)
                        makerEditarUbicacion.addListener('dragend', function (event) {
                            document.getElementById("txtEditarLatitud").value = this.getPosition().lat();
                            document.getElementById("txtEditarLongitud").value = this.getPosition().lng();
                        });


                    } else {
                        window.alert('No hay resultados');
                    }
                } else {
                    window.alert('Geocoder failed due to: ' + status);
                }
            });

        }, function (error) {
            alert('Ubicación no activada');
        });
    } else {
        alert("NO SE SOPORTA GEOLOCALIZACIÓN");
    }



}



var idModalEliminarPieDePagina = document.getElementById("idModalEliminarPieDePagina");
function ModalEliminar(ID) {
    $("#idModalEliminar").modal('show');
    var Contenido = "";
    Contenido += "";
    Contenido += "<button type='button' class='btn btn-secondary' data-dismiss='modal'>Cerrar</button>";
    Contenido += "<button type='button' onclick='Eliminar(" + ID + ")' class='btn btn-secondary' data-dismiss='modal'>Eliminar</button>";
    idModalEliminarPieDePagina.innerHTML = Contenido;
}

function Eliminar(ID) {


    var url = Servidor + "Maestros/EliminarUbicacion";
    var Form = new FormData();
    Form.append("ID_Ubicacion", ID);
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            let Respuesta = xhr.responseText.split("_");
            if (Respuesta[0] == "Correcto") {
                alertify.set('notifier', 'position', 'top-center');
                alertify.success('LA UBICACIÓN SE ELIMINO CORRECTAMENTE.');
                ListarUbicacion();


            } else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.error(Respuesta[1]);
            }
        }
    }
    xhr.send(Form);

}


function Editar(ID) {
    var txtEditarLatitud = document.getElementById("txtEditarLatitud");
    var txtEditarLongitud = document.getElementById("txtEditarLongitud");

    var url = Servidor + "Maestros/EditarUbicacion";
    var Form = new FormData();
    Form.append("ID_Usuario", ID);
    Form.append("Ubicacion", txtEditarLatitud.value + "," + txtEditarLongitud.value);
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            let Respuesta = xhr.responseText.split("_");
            if (Respuesta[0] == "Correcto") {
                alertify.set('notifier', 'position', 'top-center');
                alertify.success('LA UBICACIÓN SE ACTUALIZO CORRECTAMENTE.');
                ListarUbicacion();


            } else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.error(Respuesta[1]);
            }
        }
    }
    xhr.send(Form);

}

