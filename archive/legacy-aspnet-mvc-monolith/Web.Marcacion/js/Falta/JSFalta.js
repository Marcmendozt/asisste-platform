var Servidor = "https://localhost:44375/";


window.onload = function () {
    ListarFalta();
    Inicio();
}



function Inicio() {
    var Perfil = sessionStorage.DTPerfil;
    if (Perfil == "Administrador") {

    } else {
        var DocumentoIdentidad = sessionStorage.DTDocumentoIdentidad;
        var Nombres = sessionStorage.DTNombres;
        var Apellidos = sessionStorage.DTApellidos;

        var btnEmpleado = document.getElementById("btnEmpleado");
        var txtDNIEmpleado = document.getElementById("txtDNIEmpleado");
        var txtNombreDNI = document.getElementById("txtNombreDNI");
        var txtApellidoDNI = document.getElementById("txtApellidoDNI");
        txtDNIEmpleado.value = DocumentoIdentidad;
        txtDNIEmpleado.disabled = true;
        txtNombreDNI.value = Nombres;
        txtApellidoDNI.value = Apellidos;
        btnEmpleado.style.visibility = "hidden";
    }
}

function ListarFalta() {
    var url = Servidor + "Maestros/JListadoFalta";
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
                // var JSONDataFalta = JSON.stringify(Respuesta.Data);

                if (Respuesta.MensajeError == "SE ACABO LA SESIÓN") {
                    alert(Respuesta.MensajeError);
                    window.location = Servidor;
                } else if (Respuesta.Data.length == 0) {
                    RQST.TerminarCargando();
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.warning('NO HAY DATA');
                } else if (Respuesta.Data.length != 0) {
                    
                    ListadoDeFaltas(Respuesta.Data);
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


function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}





function ListadoDeFaltas(DataFalta) {

    var divTBFalta = document.getElementById("divTBFalta");
    var Contador = 1;
    var Contenido = "";
    var Perfil = sessionStorage.DTPerfil;
    Contenido += "<div class='col-lg-12'>";
    Contenido += "<table id='IDTBFalta' class='table table-bordered'>";
    Contenido += "<thead>";
    Contenido += "<tr>";
    Contenido += "<th>#</th>";
    if (Perfil == "Administrador") {
        Contenido += "<th>NOMBRES & APELLIDOS </th>";
        Contenido += "<th>USUARIO</th>";
    }

    Contenido += "<th>FECHA</th>";
    Contenido += "<th>DOCUMENTO</th>";
    Contenido += "<th>ESTADO</th>";
    Contenido += "<th>ACCIONES</th>";
    Contenido += "</tr>";
    Contenido += "</thead>";
    Contenido += "<tbody>";

    for (var i = 0; i < DataFalta.length; i++) {
        var ConvertirFecha = DataFalta[i].Fecha.substr(6);
        var FechaActual = new Date(parseInt(ConvertirFecha));
        var MES = FechaActual.getMonth() + 1;
        var DIA = FechaActual.getDate();
        var ANIO = FechaActual.getFullYear();
        var Fecha = DIA + '/' + MES + '/' + ANIO;
        Contenido += "<tr>";
        Contenido += "<td>" + Contador++ + "</td>";
        if (Perfil == "Administrador") {
            Contenido += "<td>" + DataFalta[i].NombreCompleto + "</td>";
            Contenido += "<td>" + DataFalta[i].NombreUsuario + "</td>";
        }

        Contenido += "<td>" + Fecha + "</td>";
        Contenido += "<td>" + DataFalta[i].Documento + "</td>";
        if (DataFalta[i].ID_Estado == 3) {
            Contenido += "<td>";
            Contenido += "<a class='material-icons';>query_builder</a>";
            Contenido += "</td>";
        } else {
            Contenido += "<td>";
            Contenido += "<a class='material-icons';>beenhere</a>";
            Contenido += "</td>";
        }

        Contenido += "<td>";
        Contenido += "<a class='material-icons'; onclick='AbrirEditar(" + DataFalta[i].ID_Usuario + ',' + DataFalta[i].ID_Estado + " )'> edit </a>";
        Contenido += "<a class='material-icons'; id='idDescargar' href=" + Servidor + 'Maestros/DescargarArchivo?ID_Usuario=' + DataFalta[i].ID_Usuario + ">get_app</a>";
        if (Perfil == "Administrador") {
           // Contenido += "<a id='#Verificar' class='material-icons' onclick='FActivarDocumentoFalta(" + DataFalta[i].ID_Usuario + " )'>check_box</a>";
        }


        Contenido += "</td>";
    }
    Contenido += "</tr>";
    Contenido += "</tbody>";
    Contenido += "</table>";
    Contenido += "</div>";
    divTBFalta.innerHTML = Contenido;


}



var idPieDePagina = document.getElementById("idPieDePagina");
function FActivarDocumentoFalta(ID) {
    $("#idModalActivarFalta").modal('show');
    var Contenido = "";
    Contenido += "";
    Contenido += "<button type='button' class='btn btn-secondary' data-dismiss='modal'>Cerrar</button>";
    Contenido += "<button type='button' onclick='AceptarDocumentoFalta(" + ID + ")' class='btn btn-secondary' data-dismiss='modal'>Confirmar</button>";
    idPieDePagina.innerHTML = Contenido;
}

function Mostrar() {
    document.getElementById("ListadoFalta").style.display = "none";
    document.getElementById("idRegistrarFalta").style.display = "";
}


document.getElementById('idFecha').valueAsDate = new Date();



function AbrirEditar(IDUsuario,Estado) {
    if (Estado != 3) {
        alertify.set('notifier', 'position', 'top-center');
        alertify.warning('YA NO PUEDE EDITAR ESTE DOCUMENTO');;
    } else {
        
    }
}




var idUsuarioGlobal;
var NombreUsuarioGlobal;
function BuscarEmpleado() {
    var txtNombreDNI = document.getElementById("txtNombreDNI");
    var txtApellidoDNI = document.getElementById("txtApellidoDNI");
    var txtDNIEmpleado = document.getElementById("txtDNIEmpleado");
    if (txtDNIEmpleado.value != "") {
            var url = Servidor + "Maestros/BuscarUsuario";
            var Form = new FormData();
            Form.append("DocumentoIdentidad", txtDNIEmpleado.value);
            var xhr = new XMLHttpRequest();
            xhr.open("post", url, true);
            xhr.onloadstart = function () {
            }
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {

                    let Respuesta = xhr.responseText.split("_");
                    if (Respuesta[0] == "CORRECTO") {
                        txtNombreDNI.value = Respuesta[1];
                        txtApellidoDNI.value = Respuesta[2];
                        idUsuarioGlobal = Respuesta[3];
                        NombreUsuarioGlobal = Respuesta[4];
                    } else {
                        alertify.set('notifier', 'position', 'top-center');
                        alertify.error(Respuesta[1]);
                    }
                }
            }
            xhr.send(Form);
       

    } else {

        alert("INGRESE EL NÚMERO DE DNI DEL USUARIO");
    }

}

function fileValidation() {
    var fileInput = document.getElementById('formFile');
    var filePath = fileInput.value;
    var allowedExtensions = /(.jpg|.jpeg|.png|.pdf)$/i;
    var extensionpdf = /(.pdf)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alertify.set('notifier', 'position', 'top-center');
        alertify.warning('Ingrese documentos en formato .jpeg/.jpg/.png/pdf');
        fileInput.value = '';
        return false;
    } else {
        if (extensionpdf.exec(filePath)) {
            var imagePreview = document.getElementById("imagePreview");
            var ContenidoImagen = "";
            ContenidoImagen += "";
            imagePreview.innerHTML = ContenidoImagen;
        } else {
            //Image preview
            if (fileInput.files && fileInput.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var imagePreview = document.getElementById("imagePreview");
                    var ContenidoImagen = "";
                    ContenidoImagen += "</br>";
                    ContenidoImagen += '<img id="idResultadoImagen" src="' + e.target.result + '" />';
                    ContenidoImagen += '<style>img#idResultadoImagen { width: 75%; height: 75%; } </style>';
                    imagePreview.innerHTML = ContenidoImagen;
                };
                reader.readAsDataURL(fileInput.files[0]);
            }
        }
    }
}


function SalirRegistrar() {
    document.getElementById("ListadoFalta").style.display = "";
    document.getElementById("idRegistrarFalta").style.display = "none";
    document.getElementById("formFile").value = "";
    var imagePreview = document.getElementById("imagePreview");
    var ContenidoImagen = "";
    ContenidoImagen += "";
    imagePreview.innerHTML = ContenidoImagen;
    document.getElementById('idFecha').valueAsDate = new Date();
}

function EnviarDocumento() {
    var Perfil = sessionStorage.DTPerfil;
    var EncytypeForm = document.getElementById("IDFormularioEnviarArchivo");
    EncytypeForm.enctype = "application/x-www-form-urlencoded,multipart/form-data,text/plain";
    var txtNombreDNI = document.getElementById("txtNombreDNI");
    var txtApellidoDNI = document.getElementById("txtApellidoDNI");
    var txtDNIEmpleado = document.getElementById("txtDNIEmpleado");
    var idFecha = document.getElementById("idFecha");
    var formFile = document.getElementById("formFile");
    if (txtNombreDNI.value != "" && txtApellidoDNI.value != "" && txtDNIEmpleado.value != "") {
        if (formFile.files.length != 0) {
            if (formFile.files[0].size < 2999999) {
                var URL = Servidor + "Maestros/GuardarArchivo";
                var XMLHR = new XMLHttpRequest();
                var Formulario = new FormData();
                Formulario.append("Archivo", formFile.files[0]);
                Formulario.append("Fecha", idFecha.value);
                if (Perfil != "Administrador") {
                    var ID_Usuario = sessionStorage.DTIDUsuario;
                    var Usuario = sessionStorage.DTNombreUsuario;
                    Formulario.append("ID_Usuario", ID_Usuario);
                    Formulario.append("NombreDeUsuario", Usuario);
                } else {
                    Formulario.append("ID_Usuario", idUsuarioGlobal);
                    Formulario.append("NombreDeUsuario", NombreUsuarioGlobal);
                }
                XMLHR.open("post", URL, true);
                XMLHR.onloadstart = function () {

                }
                XMLHR.onreadystatechange = function () {
                    if (XMLHR.readyState == 4 && XMLHR.status == 200) {
                        var Respuesta = XMLHR.responseText.split("_");
                        if (Respuesta[0] == "CORRECTO") {
                            alert("SE SUBIÓ EL ARCHIVO CORRECTAMENTE");
                            SalirRegistrar();
                            ListarFalta();
                        } else if (Respuesta[0] == "ERROR") {
                            alert(Respuesta[1]);
                        }
                    }
                }
                XMLHR.send(Formulario);
            } else {
                alert("EL ARCHIVO NO DEBE SOBREPASAR LOS 3MB");
            }
        } else {
            alert("NO HAY NINGÚN ARCHIVO SELECCIONADO");
        }

    } else {
        alert("FALTA RELLENAR ALGUNOS CAMPOS");
    }
}


