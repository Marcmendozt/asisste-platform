
var Servidor = "https://localhost:44375/";

window.onload = function () {
    ListarTipoJornada();
}

var Data;

function ListarTipoJornada() {
    var url = Servidor + "Maestros/JListadoTipoJornada";
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
        //Aqui se bloquea el boton y se bloquea los input mientras se ejecuta la consulta
    }
    xhr.onreadystatechange = function () {

        if (xhr.readyState == 4 && xhr.status == 200) {
            //recibir la información del servidor y operar segun el caso
            Respuesta = JSON.parse(xhr.response);
            if (Respuesta != null) {
                JSON.stringify(Respuesta.Data);
                Data = Respuesta.Data;
                changePage(1);
                current_page = 1;
                RQST.TerminarCargando();
            }
            else {
                WebNotifyAsBlock("error", "NO SE PUDO TRAER LA INFORMACIÓN", "ERROR!!");
            }
        }
    }
    xhr.send();
}

var divTBTipoJornadas = document.getElementById("divTBTipoJornadas");


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
 
    var page_span = document.getElementById("page");

    // Validate page
    if (page < 1) page = 1;
    if (page > numPages()) page = numPages();


    var Contador = 1;
    var Contenido = "";
    if (Data !== null) {
        Contenido += "<div class='row'>";
        Contenido += "<div class='col-lg-12'>";
        Contenido += "<table id='TBTipoJornada' class='table table-bordered'>";
        Contenido += "<thead>";
        Contenido += "<tr>";
        Contenido += "<th>#</th>";
        Contenido += "<th>DESCRIPCIÓN</th>";
        Contenido += "<th>ESTADO</th>";
        Contenido += "<th>ACCIONES</th>";
        Contenido += "</tr>";
        Contenido += "</thead>";
        Contenido += "<tbody>";
        for (var i = (page - 1) * records_per_page; i < (page * records_per_page) && i < Data.length; i++) {
            Contenido += "<tr>";
            Contenido += "<td>" + Contador++ + "</td>";
            Contenido += "<td>" + Data[i].Descripcion + "</td>";
            if (Data[i].Estado == true) {
                Contenido += "<td>";
                Contenido += "<a class='material-icons';>check_circle</a>";
                Contenido += "</td>";
            } else {
                Contenido += "<td>";
                Contenido += "<a class='material-icons';>check_circle_outline</a>";
                Contenido += "</td>";
            }
            Contenido += "<td>";
            Contenido += "<a class='material-icons'; onclick='AbrirEditar(" + Data[i].ID_TipoJornada + " )'> edit </a>";
            Contenido += "<a id='#Detalles' class='material-icons' onclick='ModalDetalles(" + Data[i].ID_TipoJornada + " )'>notes</a>";
            Contenido += "<a id='#Eliminar' class='material-icons' onclick='ModalEliminar(" + Data[i].ID_TipoJornada + " )'>delete</a>";
            Contenido += "</td>";
            Contenido += "</tr>";
        }
        Contenido += "</tr>";
        Contenido += "</tbody>";
        Contenido += "</table>";
        Contenido += "</div>";
        divTBTipoJornadas.innerHTML = Contenido;

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
    } else {
        alert("NO REGISTROS PARA MOSTRAR");
    }
   
}

function numPages() {
    var CANNP = Math.ceil(Data.length / records_per_page);
    return CANNP;
}






//Campos Agregar
var txtAgregarDescripcion = document.getElementById("txtAgregarDescripcion");
var txtAgregarDetalles = document.getElementById("txtAgregarDetalles");

//Limpiar Cajas de texto Agregar
function LimpiarCajasAgregar() {
    txtAgregarDescripcion.value = "";
    txtAgregarDetalles.value = "";

}

//Guardar Jornada
function GuardarTipoJornada() {
    
    if (txtAgregarDescripcion.value !== "" && txtAgregarDescripcion.value.length > 3 && txtAgregarDetalles.value != "") {
        var url = Servidor + "Maestros/AgregarTipoJornada";
        var Form = new FormData();
        Form.append("Descripcion", txtAgregarDescripcion.value);
        Form.append("Detalles", txtAgregarDetalles.value);
        var xhr = new XMLHttpRequest();
        xhr.open("post", url, true);
        xhr.onloadstart = function () {
        }
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {

                let Respuesta = xhr.responseText.split("_");
                if (Respuesta[0] == "Correcto") {
                    document.getElementById("stListadoTipoJornada").style.display = "";
                    document.getElementById("stCrearTipoJornada").style.display = "none";
                    LimpiarCajasAgregar();
                    ListarTipoJornada()


                } else {
                    alert(Respuesta[1]);
                }
            }
        }
        xhr.send(Form);
    } else {
        alert("Falta rellenar algunos datos");
    }
}


//Mostrar Vista Crear Jornada
function Mostrar() {
    document.getElementById("stListadoTipoJornada").style.display = "none";
    document.getElementById("stCrearTipoJornada").style.display = "";
    LimpiarCajasAgregar();
}

//Cerrar Vista Crear Jornada
function CancelarCrear() {
    document.getElementById("stListadoTipoJornada").style.display = "";
    document.getElementById("stCrearTipoJornada").style.display = "none";
    LimpiarCajasAgregar;
}


//Cerrar Vista Editar Jornada
function CancelarEditar() {
    document.getElementById("stListadoTipoJornada").style.display = "";
    document.getElementById("stEditarTipoJornada").style.display = "none";
    LimpiarCajasEditar();
}






//Campos Editar
var txtEditarDescripcion = document.getElementById("txtEditarDescripcion");
var txtEditarDetalles = document.getElementById("txtEditarDetalles");

//Limpiar Cajas de texto Editar
function LimpiarCajasEditar() {
    txtEditarDescripcion.value = "";
    txtEditarDetalles.value = "";

}

//Abrir Vista Editar 
function AbrirEditar(ID) {
    document.getElementById("stListadoTipoJornada").style.display = "none";
    document.getElementById("stEditarTipoJornada").style.display = "";
    for (var i = 0; i < Data.length; i++) {
        if (Data[i].ID_TipoJornada == ID) {
           
            txtEditarDescripcion.value = Data[i].Descripcion;
            txtEditarDetalles.value = Data[i].Detalles;
            hdnIDTipoJornada.value = ID;
        }
    }
}

//ID Oculto de Jornada
var hdnIDTipoJornada = document.getElementById("hdnIDTipoJornada");

function EditarTipoJornada() {
    if (txtEditarDescripcion.value != "") {
        if (txtEditarDetalles.value == "") {
            txtEditarDetalles.value = "No hay detalles";
        }
        var url = Servidor + "Maestros/EditarTipoJornada";
        var Form = new FormData();
        Form.append("ID_TipoJornada", hdnIDTipoJornada.value);
        Form.append("Descripcion", txtEditarDescripcion.value);
        Form.append("Detalles", txtEditarDetalles.value);
        var xhr = new XMLHttpRequest();
        xhr.open("post", url, true);
        xhr.onloadstart = function () {
        }
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {

                let Respuesta = xhr.responseText.split("_");
                if (Respuesta[0] == "Correcto") {
                    CancelarEditar();

                    ListarTipoJornada();


                } else {
                    alert(Respuesta[1]);
                }
            }
        }
        xhr.send(Form);
    } else {

        alert("NO PUEDE DEJAR VACIA LA DESCRIPCIÓN");
    }
    
}


function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

//Modal Eliminar
var idmf = document.getElementById("idmf");
function ModalEliminar(ID) {
    $("#myModal").modal('show');
    var Contenido = "";
    Contenido += "";
    Contenido += "<button type='button' class='btn btn-secondary' data-dismiss='modal'>Cerrar</button>";
    Contenido += "<button type='button' onclick='Eliminar(" + ID + ")' class='btn btn-secondary' data-dismiss='modal'>Eliminar</button>";
    idmf.innerHTML = Contenido;
}

var idModalDetalles = document.getElementById("idModalDetalles");
var txtModalDetalles = document.getElementById("txtModalDetalles");
function ModalDetalles(ID) {
    $("#myModalDetalles").modal('show');
    var Contenido = "";
    var ContenidoDetalle = "";
    Contenido += "";
    ContenidoDetalle = "";
    for (var i = 0; i < Data.length; i++) {
        if (Data[i].ID_TipoJornada == ID) {
            ContenidoDetalle = "<p>" + Data[i].Detalles +"</p>";
     
        }
    }

    Contenido += "<button type='button' class='btn btn-secondary' data-dismiss='modal'>Cerrar</button>";
    idModalDetalles.innerHTML = Contenido;
    txtModalDetalles.innerHTML = ContenidoDetalle;
}


//Eliminar Jornada
function Eliminar(ID) {
    var url = Servidor + "Maestros/EliminarTipoJornada";
    var Form = new FormData();
    Form.append("ID_TipoJornada", ID);
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            let Respuesta = xhr.responseText.split("_");
            if (Respuesta[0] == "Correcto") {
                ListarTipoJornada();


            } else {
                alert(Respuesta[1]);
            }
        }
    }
    xhr.send(Form);

}