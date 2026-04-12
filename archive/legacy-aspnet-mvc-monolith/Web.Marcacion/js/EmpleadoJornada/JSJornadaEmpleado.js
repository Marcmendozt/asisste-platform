var Servidor = "https://localhost:44375/";

window.onload = function () {
    ListarJornadasAsignadas();
    ListarTipoJornada();
}

var Data;

function ListarJornadasAsignadas() {
    var url = Servidor + "Maestros/JListadoJornadaEmpleado";
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
                PintarTabla(Respuesta.Data);
            }
            else {
                WebNotifyAsBlock("error", "NO SE PUDO TRAER LA INFORMACIÓN", "ERROR!!");
            }
        }
    }
    xhr.send();
}

var idTBJornadasAsignadas = document.getElementById("idTBJornadasAsignadas");



function PintarTabla(Data) {
    var Contador = 1;
    if (Data !== null) {
        var Contenido = "";
        Contenido += "<div class='row'>";
        Contenido += "<div class='col-lg-12'>";
        Contenido += "<table id='TBJornadaEmpleado' class='table table-bordered'>";
        Contenido += "<thead>";
        Contenido += "<tr>";
        Contenido += "<th>#</th>";
        Contenido += "<th>JORNADA</th>";
        Contenido += "<th>NOMBRES & APELLIDOS</th>";
        Contenido += "<th>ÁREA DE TRABAJO</th>";
        Contenido += "<th>CARGO</th>";
        Contenido += "<th>ENTRADA</th>";
        Contenido += "<th>SALIDA</th>";
        Contenido += "<th>TOLERANCIA</th>";
        Contenido += "<th>ESTADO</th>";
        Contenido += "<th>ACCIONES</th>";
        Contenido += "</tr>";
        Contenido += "</thead>";
        Contenido += "<tbody>";
        for (var i = 0; i < Data.length; i++) {
            Contenido += "<tr>";
            //Hora Ingreso
            var HORA = Data[i].Entrada.Hours;
            var MINUTOS = Data[i].Entrada.Minutes;
            var SEGUNDOS = Data[i].Entrada.Seconds;
            HORA = checkTime(HORA);
            MINUTOS = checkTime(MINUTOS);
            SEGUNDOS = checkTime(SEGUNDOS);
            var E = HORA + ':' + MINUTOS + ':' + SEGUNDOS;

            //Hora Salida
            var HORA = Data[i].Salida.Hours;
            var MINUTOS = Data[i].Salida.Minutes;
            var SEGUNDOS = Data[i].Salida.Seconds;
            HORA = checkTime(HORA);
            MINUTOS = checkTime(MINUTOS);
            SEGUNDOS = checkTime(SEGUNDOS);
            var S = HORA + ':' + MINUTOS + ':' + SEGUNDOS;

            Contenido += "<td>" + Contador++ + "</td>";
            Contenido += "<td> " + Data[i].TipoJornada + " </td>";
            Contenido += "<td> " + Data[i].NombreCompleto + " </td>";
            Contenido += "<td> " + Data[i].AreaTrabajo + " </td>";
            Contenido += "<td> " + Data[i].Cargo + " </td>";
            Contenido += "<td> " + E + " </td>";
            Contenido += "<td> " + S + " </td>";
            Contenido += "<td> " + Data[i].Tolerancia + " </td>";
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

            Contenido += "<a class='material-icons'; onclick='AbrirEditar(" + Data[i].ID_EmpleadoJornada + " )'> edit </a>";
            Contenido += "</td>";
            Contenido += "</tr>";
        }
        Contenido += "</tbody>";
        Contenido += "</table>";
        Contenido += "</div>";
        Contenido += "</div>";


        idTBJornadasAsignadas.innerHTML = Contenido;
    } else {
        alert("NO REGISTROS PARA MOSTRAR");
    }


}


function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}


document.querySelector("#idFiltrar").onkeyup = function () {
    $TableFilter("#TBJornadaEmpleado", this.value);
}
$TableFilter = function (id, value) {
    var rows = document.querySelectorAll(id + ' tbody tr');
    for (var i = 0; i < rows.length; i++) {
        var showRow = false;
        var row = rows[i];
        row.style.display = 'none';

        for (var x = 0; x < row.childElementCount; x++) {
            if (row.children[x].textContent.toLowerCase().indexOf(value.toLowerCase().trim()) > -1) {
                showRow = true;
                break;
            }
        }
        if (showRow) {
            row.style.display = null;
        }
    }
}

//Cerrar Vista Editar Jornada Empleado
function CancelarEditar() {
    document.getElementById("stListadoJornadaEmpleado").style.display = "";
    document.getElementById("stEditarJornadaEmpleado").style.display = "none";
    
}



//ID Oculto de Jornada Empleado
var hdnIDJornadaEmpleado = document.getElementById("hdnIDJornadaEmpleado");

//Campos Editar
var txtEditarNombreCompleto = document.getElementById("txtEditarNombreCompleto");
var txtEditarCargo = document.getElementById("txtEditarCargo");
var slTipoJornada;
var IDHorario;
function AbrirEditar(ID) {
    document.getElementById("stListadoJornadaEmpleado").style.display = "none";
    document.getElementById("stEditarJornadaEmpleado").style.display = "";
   
    for (var i = 0; i < Data.length; i++) {
        if (Data[i].ID_EmpleadoJornada == ID) {

            txtEditarNombreCompleto.value = Data[i].NombreCompleto;
            txtEditarCargo.value = Data[i].Cargo;
            slTipoJornada.value = Data[i].ID_TipoJornada;
            hdnIDJornadaEmpleado.value = ID;
            IDHorario = Data[i].ID_Horario;
            ListarHorarios(Data[i].ID_TipoJornada);
          
        }
    }
}

function CambiarHorario() {
    var url = Servidor + "Maestros/JListadoHorariosTipoJornada";
    var Form = new FormData();
    Form.append("ID_TipoJornada", slTipoJornada.value);
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
                CambioDeHorario(Respuesta.Data);
            }
            else {
                WebNotifyAsBlock("error", "NO SE PUDO TRAER LA INFORMACIÓN", "ERROR!!");
            }
        }
    }
    xhr.send(Form);
}

function CambioDeHorario(DataHorarios) {
    slHorarios = document.getElementById("slHorarios");
    var Contenido = "";
    Contenido += "<option value=0 selected>Seleccione</option>";
    for (var i = 0; i < DataHorarios.length; i++) {
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
        Contenido += "<option value=" + DataHorarios[i].ID_Horario + ">" + E + ' a ' + S + "</option>";
    }
    Contenido += "</select>";
    slHorarios.innerHTML = Contenido;
  
}

function ListarHorarios(ID_TipoJornada) {
    var url = Servidor + "Maestros/JListadoHorariosTipoJornada";
    var Form = new FormData();
    Form.append("ID_TipoJornada", ID_TipoJornada);
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
                ListadoComboHorarios(Respuesta.Data);
            }
            else {
                WebNotifyAsBlock("error", "NO SE PUDO TRAER LA INFORMACIÓN", "ERROR!!");
            }
        }
    }
    xhr.send(Form);
}
var slHorarios;
//Listar Combo Horarios
function ListadoComboHorarios(DataHorarios) {
    slHorarios = document.getElementById("slHorarios");
    var Contenido = "";
    Contenido += "<option value=0 selected>Seleccione</option>";
    for (var i = 0; i < DataHorarios.length; i++) {
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
        Contenido += "<option value=" + DataHorarios[i].ID_Horario + ">" + E + ' a ' + S + "</option>";
    }
    Contenido += "</select>";
    slHorarios.innerHTML = Contenido;
    slHorarios.value = IDHorario;

}


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
                ListadoComboTipoJornada(Respuesta.Data);
            }
            else {
                WebNotifyAsBlock("error", "NO SE PUDO TRAER LA INFORMACIÓN", "ERROR!!");
            }
        }
    }
    xhr.send();
}



//Listar Combo
function ListadoComboTipoJornada(DTipoJornada) {
    slTipoJornada = document.getElementById("slTipoJornada");
    var Contenido = "";
    Contenido += "<option value=0 selected>Seleccione</option>";
    for (var i = 0; i < DTipoJornada.length; i++) {
        Contenido += "<option value=" + DTipoJornada[i].ID_TipoJornada + ">" + DTipoJornada[i].Descripcion + "</option>";
    }
    Contenido += "</select>";
    slTipoJornada.innerHTML = Contenido;
  
}



//Editar 
function EditarJornadaEmpleado() {

    var url = Servidor + "Maestros/EditarJornadaEmpleado";
    var Form = new FormData();
    if (slTipoJornada.value != 0) {
        Form.append("ID_EmpleadoJornada", hdnIDJornadaEmpleado.value);
        Form.append("ID_Horario", slHorarios.value);
        var xhr = new XMLHttpRequest();
        xhr.open("post", url, true);
        xhr.onloadstart = function () {
        }
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {

                let Respuesta = xhr.responseText.split("_");
                if (Respuesta[0] == "Correcto") {
                    CancelarEditar();
                    ListarJornadasAsignadas();


                } else {
                    alert(Respuesta[1]);
                }
            }
        }
        xhr.send(Form);
    } else {
        alert("SELECCIONE UNA JORNADA");
    }
  
}