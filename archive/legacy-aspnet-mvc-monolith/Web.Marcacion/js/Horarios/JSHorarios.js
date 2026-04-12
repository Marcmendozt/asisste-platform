

var Servidor = "https://localhost:44375/";

window.onload = function () {
    TraerDataHorarios();
    ListarComboTipoJornada();
}

var DataHorariosGlobal;

function TraerDataHorarios() {
    var url = Servidor + "Maestros/JListadoHorarios";
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
        RQST.CargandoVistas();
    }
    xhr.onreadystatechange = function () {

        if (xhr.readyState == 4 && xhr.status == 200) {
            //recibir la información del servidor y operar segun el caso
            Respuesta = JSON.parse(xhr.response);
            if (Respuesta.Data.length != 0) {
                DataHorariosGlobal = Respuesta.Data;
                ListarHorarios(Respuesta.Data);
                RQST.TerminarCargando();
            } else {
                alert("NO HAY DATA");
                RQST.TerminarCargando();
            }
        }
    }
    xhr.send();
}



function MostrarFormularioRegistrar() {

    document.getElementById("CrearNuevoHorario").style.display = "";
    document.getElementById("ListadoHorarios").style.display = "none";
}




function ListarHorarios(DataHorarios) {
    var divTBHorarios = document.getElementById("divTBHorarios");
    var Contador = 1;
    var Contenido = "";
    Contenido += "<div class='col-lg-12'>";
    Contenido += "<table id='IDTBHorarios' class='table table-bordered'>";
    Contenido += "<thead>";
    Contenido += "<tr>";
    Contenido += "<th>#</th>";
    Contenido += "<th>JORNADA</th>";
    Contenido += "<th>ENTRADA</th>";
    Contenido += "<th>SALIDA</th>";
    Contenido += "<th>TOLERANCIA</th>";
    Contenido += "<th>ACCIONES</th>";
    Contenido += "</tr>";
    Contenido += "</thead>";
    Contenido += "<tbody>";

    for (var i = 0; i < DataHorarios.length; i++) {
        Contenido += "<tr>";
        Contenido += "<td>" + Contador++ + "</td>";
        Contenido += "<td>" + DataHorarios[i].Jornada + "</td>";
        var HORA = DataHorarios[i].Entrada.Hours;
        var MINUTOS = DataHorarios[i].Entrada.Minutes;
        var SEGUNDOS = DataHorarios[i].Entrada.Seconds;
        HORA = checkTime(HORA);
        MINUTOS = checkTime(MINUTOS);
        SEGUNDOS = checkTime(SEGUNDOS);
        var E = HORA + ':' + MINUTOS + ':' + SEGUNDOS;
        //Hora Salida
        var HORA = DataHorarios[i].Salida.Hours;
        var MINUTOS = DataHorarios[i].Salida.Minutes;
        var SEGUNDOS = DataHorarios[i].Salida.Seconds;
        HORA = checkTime(HORA);
        MINUTOS = checkTime(MINUTOS);
        SEGUNDOS = checkTime(SEGUNDOS);
        var S = HORA + ':' + MINUTOS + ':' + SEGUNDOS;
        Contenido += "<td>" + E + "</td>";
        Contenido += "<td>" + S + "</td>";
        Contenido += "<td>" + DataHorarios[i].Tolerancia + ' minutos' + "</td>";
        Contenido += "<td>";
        Contenido += "<a class='material-icons'; onclick='AbrirEditar(" + DataHorarios[i].ID_Horario + " )'> edit </a>";
        Contenido += "<a id='#Eliminar' class='material-icons' onclick='ModalEliminar(" + DataHorarios[i].ID_Horario + " )'>delete</a>";
        Contenido += "</td>";
    }
    Contenido += "</tr>";
    Contenido += "</tbody>";
    Contenido += "</table>";
    Contenido += "</div>";
    divTBHorarios.innerHTML = Contenido;
}

var idmf = document.getElementById("idmf");
function ModalEliminar(ID) {
    $("#ModalEliminarHorario").modal('show');
    var Contenido = "";
    Contenido += "";
    Contenido += "<button type='button' class='btn btn-secondary' data-dismiss='modal'>Cerrar</button>";
    Contenido += "<button type='button' onclick='Eliminar(" + ID + ")' class='btn btn-secondary' data-dismiss='modal'>Eliminar</button>";
    idmf.innerHTML = Contenido;
}


function Eliminar(ID) {


    var url = Servidor + "Maestros/EliminarHorarios";
    var Form = new FormData();
    Form.append("ID_Horario", ID);
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            let Respuesta = xhr.responseText.split("_");
            if (Respuesta[0] == "Correcto") {
                alertify.set('notifier', 'position', 'top-center');
                alertify.success('EL HORARIO SE ELIMINO CORRECTAMENTE.');
                TraerDataHorarios();


            } else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.error(Respuesta[1]);
            }
        }
    }
    xhr.send(Form);

}

function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

function ListarComboTipoJornada() {
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
                PintarComboTipoJornada(Respuesta.Data);
                PintarComboTipoJornadaEditar(Respuesta.Data);
            }
            else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.warning('NO SE PUDO TRAER LA INFO');
            }
        }
    }
    xhr.send();
}


var slTipoJornada;
//Listar Combo Tipo jornada
function PintarComboTipoJornada(DataTipoJornada) {
    slTipoJornada = document.getElementById("slTipoJornada");
    var Contenido = "";
    Contenido += "<option value=0 selected>Seleccione</option>";
    for (var i = 0; i < DataTipoJornada.length; i++) {
        Contenido += "<option value=" + DataTipoJornada[i].ID_TipoJornada + ">" + DataTipoJornada[i].Descripcion + "</option>";
    }
    Contenido += "</select>";
    slTipoJornada.innerHTML = Contenido;
}


function PintarComboTipoJornadaEditar(DataTipoJornadaEdit) {
    var slEditarTipoJornada = document.getElementById("slEditarTipoJornada");
    var Contenido = "";
    Contenido += "<option value=0 selected>Seleccione</option>";
    for (var i = 0; i < DataTipoJornadaEdit.length; i++) {
        Contenido += "<option value=" + DataTipoJornadaEdit[i].ID_TipoJornada + ">" + DataTipoJornadaEdit[i].Descripcion + "</option>";
    }
    Contenido += "</select>";
    slEditarTipoJornada.innerHTML = Contenido;
}


function CrearNuevoHorario() {
    var txtAgregarHoraEntrada = document.getElementById("txtAgregarHoraEntrada");
    var txtAgregarHoraSalida = document.getElementById("txtAgregarHoraSalida");
    var txtAgregarTolerancia = document.getElementById("txtAgregarTolerancia");
    var slTipoJornada = document.getElementById("slTipoJornada");
    if (txtAgregarHoraEntrada.value != "" && txtAgregarHoraSalida.value != "" && txtAgregarTolerancia.value != "" && slTipoJornada.value != 0) {
        var url = Servidor + "Maestros/AgregarHorarios";
        var Form = new FormData();
        Form.append("ID_TipoJornada", slTipoJornada.value);
        Form.append("Entrada", txtAgregarHoraEntrada.value);
        Form.append("Salida", txtAgregarHoraSalida.value);
        Form.append("Tolerancia", txtAgregarTolerancia.value);
        var xhr = new XMLHttpRequest();
        xhr.open("post", url, true);
        xhr.onloadstart = function () {
        }
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {

                let Respuesta = xhr.responseText.split("_");
                if (Respuesta[0] == "Correcto") {
                    document.getElementById("CrearNuevoHorario").style.display = "none";
                    document.getElementById("ListadoHorarios").style.display = "";
                    slTipoJornada.value = 0;
                    txtAgregarHoraEntrada.value = "";
                    txtAgregarHoraSalida.value = "";
                    txtAgregarTolerancia.value = "";
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.success('EL HORARIO SE CREO CORRECTAMENTE.');
                    TraerDataHorarios();
                    ListarComboTipoJornada();


                } else {
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.error(Respuesta[1]);
                }
            }
        }
        xhr.send(Form);
    } else {
        alertify.set('notifier', 'position', 'top-center');
        alertify.warning('FALTA RELLENAR ALGUNOS CAMPOS');

    }
}


function CancelarNuevoHorario() {
    var txtAgregarHoraEntrada = document.getElementById("txtAgregarHoraEntrada");
    var txtAgregarHoraSalida = document.getElementById("txtAgregarHoraSalida");
    var txtAgregarTolerancia = document.getElementById("txtAgregarTolerancia");
    var slTipoJornada = document.getElementById("slTipoJornada");
    document.getElementById("CrearNuevoHorario").style.display = "none";
    document.getElementById("ListadoHorarios").style.display = "";
    slTipoJornada.value = 0;
    txtAgregarHoraEntrada.value = "";
    txtAgregarHoraSalida.value = "";
    txtAgregarTolerancia.value = "";
}


function AbrirEditar(ID) {
    document.getElementById("ListadoHorarios").style.display = "none";
    document.getElementById("EditarHorario").style.display = "";
    for (var i = 0; i < DataHorariosGlobal.length; i++) {
        if (DataHorariosGlobal[i].ID_Horario == ID) {
            var HORA = DataHorariosGlobal[i].Entrada.Hours;
            var MINUTOS = DataHorariosGlobal[i].Entrada.Minutes;
            HORA = checkTime(HORA);
            MINUTOS = checkTime(MINUTOS);
            var NuevaHoraEntrada = HORA + ':' + MINUTOS;
            txtEditarHoraEntrada.value = NuevaHoraEntrada;
            var HORA = DataHorariosGlobal[i].Salida.Hours;
            var MINUTOS = DataHorariosGlobal[i].Salida.Minutes;
            HORA = checkTime(HORA);
            MINUTOS = checkTime(MINUTOS);
            var NuevaHoraSalida = HORA + ':' + MINUTOS;
            txtEditarHoraSalida.value = NuevaHoraSalida;
            txtEditarTolerancia.value = DataHorariosGlobal[i].Tolerancia;
            slTipoJornadaEditar.value = DataHorariosGlobal[i].ID_TipoJornada;
            IDHorarioHidden.value = ID;
        }
    }
}

function CancelarEditarHorario() {
    document.getElementById("EditarHorario").style.display = "none";
    document.getElementById("ListadoHorarios").style.display = "";
}
var IDHorarioHidden = document.getElementById("IDHorarioHidden");
var txtEditarHoraEntrada = document.getElementById("txtEditarHoraEntrada");
var txtEditarHoraSalida = document.getElementById("txtEditarHoraSalida");
var txtEditarTolerancia = document.getElementById("txtEditarTolerancia");
var slTipoJornadaEditar = document.getElementById("slEditarTipoJornada");
function EditarHorario() {
    if (txtEditarHoraEntrada.value != "" && txtEditarHoraSalida.value != "" && txtEditarTolerancia.value != ""
        && slTipoJornadaEditar.value != 0) {

        var url = Servidor + "Maestros/EditarHorarios";
        var Form = new FormData();
        Form.append("ID_Horario", IDHorarioHidden.value);
        Form.append("Entrada", txtEditarHoraEntrada.value);
        Form.append("Salida", txtEditarHoraSalida.value);
        Form.append("Tolerancia", txtEditarTolerancia.value);
        Form.append("ID_TipoJornada", slTipoJornadaEditar.value);
        var xhr = new XMLHttpRequest();
        xhr.open("post", url, true);
        xhr.onloadstart = function () {
        }
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {

                let Respuesta = xhr.responseText.split("_");
                if (Respuesta[0] == "Correcto") {
                    document.getElementById("EditarHorario").style.display = "none";
                    document.getElementById("ListadoHorarios").style.display = "";
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.success('EL HORARIO SE ACTUALIZO CORRECTAMENTE.');
                    TraerDataHorarios();


                } else {
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.error(Respuesta[1]);
                }
            }
        }
        xhr.send(Form);

    } else {
        alertify.set('notifier', 'position', 'top-center');
        alertify.warning('RELLENE EL CAMPO DESCRIPCION');
    }

}