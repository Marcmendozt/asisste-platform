

var Servidor = "https://localhost:44375/";

window.onload = function () {
    ListarPerfil();

}

var Data;




function ListarPerfil() {
    var url = Servidor + "Maestros/JListadoPerfiles";
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
                Data = Respuesta.Data;
                //TraerDataPerfil(Respuesta.Data);
                changePage(1);
                current_page = 1;
                RQST.TerminarCargando();
            }
            else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.warning('NO SE PUDO TRAER LA INFORMACIÓN');
            }
        }
    }
    xhr.send();
}


var divTBPerfil = document.getElementById("divTBPerfil");

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
    var divTBPerfil = document.getElementById("divTBPerfil");
    var page_span = document.getElementById("page");

    // Validate page
    if (page < 1) page = 1;
    if (page > numPages()) page = numPages();


    var Contador = 1;
    var Contenido = "";
  
    Contenido += "<div class='col-lg-12'>";
    Contenido += "<table id='IDTBPerfil' class='table table-bordered'>";
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
            Contenido += "<a class='material-icons'; onclick='AbrirEditar(" + Data[i].ID_Perfil + " )'> edit </a>";
            Contenido += "<a id='#Eliminar' class='material-icons' onclick='ModalEliminar(" + Data[i].ID_Perfil + " )'>delete</a>";
            Contenido += "</td>";
    }
    Contenido += "</tr>";
    Contenido += "</tbody>";
    Contenido += "</table>";
    Contenido += "</div>";
    divTBPerfil.innerHTML = Contenido;

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
    var CANNP = Math.ceil(Data.length / records_per_page);
    return CANNP;
}




var txtDesc = document.getElementById("txtDesc");
var hdnIDPerfil = document.getElementById("hdnIDPerfil");

function AbrirEditar(ID) {
    document.getElementById("ListadoPerfiles").style.display = "none";
    document.getElementById("EditarPerfil").style.display = "";
    for (var i = 0; i < Data.length; i++) {
        if (Data[i].ID_Perfil == ID) {
            txtDesc.value = Data[i].Descripcion;
            hdnIDPerfil.value = ID;
        }
    }
}

function EditarPerfil() {
    if (txtDesc.value != "") {
        if (txtDesc.value.length >= 4) {
            var url = Servidor + "Maestros/EditarPerfil";
            var Form = new FormData();
            Form.append("ID_Perfil", hdnIDPerfil.value);
            Form.append("Descripcion", txtDesc.value);
            var xhr = new XMLHttpRequest();
            xhr.open("post", url, true);
            xhr.onloadstart = function () {
            }
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {

                    let Respuesta = xhr.responseText.split("_");
                    if (Respuesta[0] == "Correcto") {
                        document.getElementById("ListadoPerfiles").style.display = "";
                        document.getElementById("EditarPerfil").style.display = "none";
                        alertify.set('notifier', 'position', 'top-center');
                        alertify.success('EL PERFIL SE ACTUALIZO CORRECTAMENTE.');
                        ListarPerfil();


                    } else {
                        alertify.set('notifier', 'position', 'top-center');
                        alertify.error(Respuesta[1]);
                    }
                }
            }
            xhr.send(Form);
        } else {
            alertify.set('notifier', 'position', 'top-center');
            alertify.warning('INGRESE UNA DESCRIPCION VALIDA');
        }
    } else {
        alertify.set('notifier', 'position', 'top-center');
        alertify.warning('RELLENE EL CAMPO DESCRIPCION');
    }

}



var idmf = document.getElementById("idmf");
function ModalEliminar(ID) {
    $("#myModal").modal('show');
    var Contenido = "";
    Contenido += "";
    Contenido += "<button type='button' class='btn btn-secondary' data-dismiss='modal'>Cerrar</button>";
    Contenido += "<button type='button' onclick='Eliminar(" + ID + ")' class='btn btn-secondary' data-dismiss='modal'>Eliminar</button>";
    idmf.innerHTML = Contenido;
}

function Eliminar(ID) {


    var url = Servidor + "Maestros/EliminarPerfil";
    var Form = new FormData();
    Form.append("ID_Perfil", ID);
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            let Respuesta = xhr.responseText.split("_");
            if (Respuesta[0] == "Correcto") {
                alertify.set('notifier', 'position', 'top-center');
                alertify.success('EL PERFIL SE ELIMINO CORRECTAMENTE.');
                ListarPerfil();


            } else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.error(Respuesta[1]);
            }
        }
    }
    xhr.send(Form);

}


function Mostrar() {
    document.getElementById("ListadoPerfiles").style.display = "none";
    document.getElementById("CrearPerfil").style.display = "";
}


function GuardarPerfil() {
    var txtPerfil = document.getElementById("txtPerfil");

    if (txtPerfil.value !== "") {
        if (txtPerfil.value.length > 3) {
            var url = Servidor + "Maestros/AgregarPerfil";
            var Form = new FormData();
            Form.append("Descripcion", txtPerfil.value);
            var xhr = new XMLHttpRequest();
            xhr.open("post", url, true);
            xhr.onloadstart = function () {
            }
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {

                    let Respuesta = xhr.responseText.split("_");
                    if (Respuesta[0] == "Correcto") {
                        document.getElementById("CrearPerfil").style.display = "none";
                        document.getElementById("ListadoPerfiles").style.display = "";
                        txtPerfil.value = "";
                        alertify.set('notifier', 'position', 'top-center');
                        alertify.success('EL PERFIL SE CREO CORRECTAMENTE.');
                        ListarPerfil();


                    } else {
                        alertify.set('notifier', 'position', 'top-center');
                        alertify.error(Respuesta[1]);
                    }
                }
            }
            xhr.send(Form);
        } else {
            alertify.set('notifier', 'position', 'top-center');
            alertify.warning('INGRESE UNA DESCRIPCIÓN VALIDA');

        }

    } else {
        alertify.set('notifier', 'position', 'top-center');
        alertify.warning('FALTA RELLENAR ALGUNOS CAMPOS');
    }
}


function CancelarCrear() {
    document.getElementById("ListadoPerfiles").style.display = "";
    document.getElementById("CrearPerfil").style.display = "none";
    txtPerfil.value = "";
}


function CancelarEditar() {
    document.getElementById("ListadoPerfiles").style.display = "";
    document.getElementById("EditarPerfil").style.display = "none";
    txtPerfil.value = "";
}





function Editar(ID_Perfil) {

    RQST.Encrypt(ID_Perfil.toString(), "T_Perfil/Edit");
}


