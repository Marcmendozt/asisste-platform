var Servidor = "https://localhost:44375/";

window.onload = function () {
    ListarLugarTrabajo();

}

var DataLugarTrabajo;




function ListarLugarTrabajo() {
    var url = Servidor + "Maestros/JListadoLugarTrabajo";
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
                DataLugarTrabajo = Respuesta.Data;
                if (DataLugarTrabajo.length == 0) {
                    RQST.TerminarCargando();
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.warning('NO HAY DATA');
                } else {
                    changePage(1);
                    current_page = 1;
                    RQST.TerminarCargando();
                }
      
            }
            else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.warning('NO SE PUDO TRAER LA INFORMACIÓN');
            }
        }
    }
    xhr.send();
}


var current_page = 1;
var records_per_page = 10;


function prevPage() {
    if (current_page > 1) {
        current_page--;
        changePage(current_page);
    }
}

function nextPage() {
    if (current_page < numPages()) {
        current_page++;
        changePage(current_page);
    }
}


function changePage(page) {
    var btn_next = document.getElementById("btn_next");
    var btn_prev = document.getElementById("btn_prev");
    var divTBLugarTrabajo = document.getElementById("divTBLugarTrabajo");
    var page_span = document.getElementById("page");

    // Validate page
    if (page < 1) page = 1;
    if (page > numPages()) page = numPages();


    var Contador = 1;
    var Contenido = "";

    Contenido += "<div class='col-lg-12'>";
    Contenido += "<table id='IDTBLugarTrabajo' class='table table-bordered'>";
    Contenido += "<thead>";
    Contenido += "<tr>";
    Contenido += "<th>#</th>";
    Contenido += "<th>DESCRIPCIÓN</th>";
    Contenido += "<th>ESTADO</th>";
    Contenido += "<th>ACCIONES</th>";
    Contenido += "</tr>";
    Contenido += "</thead>";
    Contenido += "<tbody>";
    for (var i = (page - 1) * records_per_page; i < (page * records_per_page) && i < DataLugarTrabajo.length; i++) {
        Contenido += "<tr>";
        Contenido += "<td>" + Contador++ + "</td>";
        Contenido += "<td>" + DataLugarTrabajo[i].Descripcion + "</td>";
        if (DataLugarTrabajo[i].ID_Estado == true) {
            Contenido += "<td>";
            Contenido += "<a class='material-icons';>check_circle</a>";
            Contenido += "</td>";
        } else {
            Contenido += "<td>";
            Contenido += "<a class='material-icons';>check_circle_outline</a>";
            Contenido += "</td>";
        }
        Contenido += "<td>";
        Contenido += "<a class='material-icons'; onclick='AbrirEditar(" + DataLugarTrabajo[i].ID_LugarTrabajo + " )'> edit </a>";
        Contenido += "<a id='#Eliminar' class='material-icons' onclick='ModalEliminar(" + DataLugarTrabajo[i].ID_LugarTrabajo + " )'>delete</a>";
        Contenido += "</td>";
    }
    Contenido += "</tr>";
    Contenido += "</tbody>";
    Contenido += "</table>";
    Contenido += "</div>";
    divTBLugarTrabajo.innerHTML = Contenido;

    page_span.innerHTML = page + "/" + numPages();

    if (page == 1) {
        btn_prev.style.visibility = "hidden";
    } else {
        btn_prev.style.visibility = "visible";
    }

    if (page == numPages()) {
        btn_next.style.visibility = "hidden";
    } else {
        btn_next.style.visibility = "visible";
    }
}

function numPages() {
    var CANNP = Math.ceil(DataLugarTrabajo.length / records_per_page);
    return CANNP;
}

