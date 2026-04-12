
var Servidor = "https://localhost:44375/";

window.onload = function () {
    ListarPersona();
    ListarTipoDocumento();
    ListarPerfil();
    ListarCargo();
    ListarTipoJornada();
    ListarLugarTrabajo();
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

ListadoComboLugarTrabajo



var slTipoJornada;
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

function ListarLugarTrabajo() {
    var url = Servidor + "Maestros/JListadoLugarTrabajo";
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
                ListadoComboLugarTrabajo(Respuesta.Data);
            }
            else {
                WebNotifyAsBlock("error", "NO SE PUDO TRAER LA INFORMACIÓN", "ERROR!!");
            }
        }
    }
    xhr.send();
}

var slLugarTrabajo;
function ListadoComboLugarTrabajo(DLugarTrabajo) {
    slLugarTrabajo = document.getElementById("slLugarTrabajo");
    var Contenido = "";
    Contenido += "<option value=0 selected>Seleccione</option>";
    for (var i = 0; i < DLugarTrabajo.length; i++) {
        Contenido += "<option value=" + DLugarTrabajo[i].ID_LugarTrabajo + ">" + DLugarTrabajo[i].Descripcion + "</option>";
    }
    Contenido += "</select>";
    slLugarTrabajo.innerHTML = Contenido;

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

var DataEmpleado;
var DTipoDocumento;
function ListarPersona() {
    var url = Servidor + "Maestros/JListadoPersona";
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {

    }
    xhr.onreadystatechange = function () {

        if (xhr.readyState == 4 && xhr.status == 200) {

            Respuesta = JSON.parse(xhr.response);
            if (Respuesta != null) {
                JSON.stringify(Respuesta.Data);
                DataEmpleado = Respuesta.Data;

                PintarTabla(Respuesta.Data);

            }
            else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.error('NO SE PUDO TRAER LA INFORMACIÓN"');


            }
        }
    }
    xhr.send();
}


function ListarTipoDocumento() {
    var url = Servidor + "Maestros/JListadoTipoDocumento";
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
                DTipoDocumento = Respuesta.Data;
                ListadoComboTipoDocumento(Respuesta.Data);

            }
            else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.error('NO SE PUDO TRAER LA INFORMACIÓN"');


            }
        }
    }
    xhr.send();
}

//Listar Combo Tipo Documento
function ListadoComboTipoDocumento(DTipoDocumento) {
    slTipoDocumento = document.getElementById("slTipoDocumento");
    var Contenido = "";
    Contenido += "<option value=0 selected>Seleccione</option>";
    for (var i = 0; i < DTipoDocumento.length; i++) {
        Contenido += "<option value=" + DTipoDocumento[i].ID_TipoDocumento + ">" + DTipoDocumento[i].Descripcion + "</option>";
    }
    slTipoDocumento.innerHTML = Contenido;

}

var slTipoJornada = document.getElementById('slTipoJornada');


//ID Datos Generales

var txtNumeroDocumento = document.getElementById("txtNumeroDocumento");
var slGenero = document.getElementById("slGenero");
var txtAgregarCelular = document.getElementById("txtAgregarCelular");
var txtAgregarDia = document.getElementById("txtAgregarDia");
var slMES = document.getElementById("slMES");
var txtAgregarAño = document.getElementById("txtAgregarAño");
var slTipoDocumento;

function SiguienteP3() {
    if (txtNumeroDocumento.value != "" && txtAgregarCelular.value != 0 &&
        txtAgregarDia.value != "" && slMES.value != 0 && txtAgregarAño.value != "" && slTipoDocumento.selectedIndex != 0) {
        if (txtAgregarCelular.value.length == 9) {
            if (txtAgregarDia.value.length == 2 && txtAgregarDia.value < 32) {
                if (slMES.value > 0) {
                    if (txtAgregarAño.value.length == 4 && txtAgregarAño.value <= 2002) {
                        if (txtNumeroDocumento.value.length == VLongitud) {

                            if (slGenero.value != 0) {
                                document.getElementById("stCrearEmpleado2").style.display = "none";
                                document.getElementById("stCrearEmpleado3").style.display = "";
                            } else {
                                alertify.set('notifier', 'position', 'top-center');
                                alertify.warning('SELECCIONE UN GÉNERO!!');
                            }
                        } else {
                            alertify.set('notifier', 'position', 'top-center');
                            alertify.warning('EL NÚMERO DE DOCUMENTO ELEGIDO DEBE TENER ' + VLongitud + " DÍGITOS");
                        }
                    } else {

                        alertify.set('notifier', 'position', 'top-center');
                        alertify.warning('AGREGUE UN AÑO VALIDO');
                    }
                } else {
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.warning('SELECCIONE UN MES');
                }
            } else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.warning('INGRESE UN DÍA DE MES CORRECTO');
            }
        } else {
            alertify.set('notifier', 'position', 'top-center');
            alertify.warning('EL NÚMERO DEBE TENER 9 DÍGITOS');

        }

    } else {
        alertify.set('notifier', 'position', 'top-center');
        alertify.warning('FALTA RELLENAR ALGUNOS CAMPOS');
    }
}


var VLongitud;

function Longitud() {
    for (var i = 0; i < DTipoDocumento.length; i++) {
        if (DTipoDocumento[i].ID_TipoDocumento == slTipoDocumento.value) {
            VLongitud = DTipoDocumento[i].Longitud;
            txtNumeroDocumento.maxLength = DTipoDocumento[i].Longitud;
            txtNumeroDocumento.value = "";
        }
    }
}

//Crear Usuario

function CrearUsuario() {
    if (slTipoJornada.value != 0 && slCargo.value != 0 && slPerfil.value != 0) {
        var url = Servidor + "Maestros/AgregarPersona";
        var Form = new FormData();
        Form.append("Nombres", txtAgregarNombre.value);
        Form.append("Apellidos", txtAgregarApellidos.value);
        Form.append("NumeroDocumento", txtNumeroDocumento.value);
        Form.append("ID_Genero", slGenero.value);
        Form.append("Correo", txtAgregarCorreo.value);
        Form.append("Movil", txtAgregarCelular.value);
        var FechaNacimiento = txtAgregarDia.value + '/' + slMES.value + '/' + txtAgregarAño.value;

        Form.append("FechaNacimiento", FechaNacimiento);
        Form.append("ID_TipoDocumento", slTipoDocumento.selectedIndex);
        Form.append("ID_Cargo", slCargo.selectedIndex);

        var xhr = new XMLHttpRequest();
        xhr.open("post", url, true);
        xhr.onloadstart = function () {
        }
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {

                let Respuesta = xhr.responseText.split("_");
                if (Respuesta[0] == "Correcto") {
                    if (Respuesta[1] != null) {
                        CrearCredencialesUsuario(Respuesta[1]);
                    } else {
                        alertify.set('notifier', 'position', 'top-center');
                        alertify.warning('ERROR AL CREAR AL USUARIO');
                    }
                } else {
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.warning(Respuesta[1]);
                }
            }
        }
        xhr.send(Form);
    } else {
        alertify.set('notifier', 'position', 'top-center');
        alertify.warning('SELECCIONE LAS LISTAS PENDIENTES');
    }
}





function CrearCredencialesUsuario(ID_Persona) {

    var url = Servidor + "Maestros/AgregarUsuario";
    var Form = new FormData();
    Form.append("Usuario", txtAgregarCorreo.value);
    Form.append("Clave", txtAgregarContraseña.value);
    Form.append("ID_Persona", ID_Persona);
    Form.append("ID_Perfil", slPerfil.value);
    Form.append("ID_LugarTrabajo", slLugarTrabajo.value);
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            let Respuesta = xhr.responseText.split("_");
            if (Respuesta[0] == "Correcto") {
                CrearEmpleadoJornada(Respuesta[1]);
            } else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.error(Respuesta[1]);
            }
        }
    }
    xhr.send(Form);
}



function CrearEmpleadoJornada(ID_Usuario) {

    var url = Servidor + "Maestros/AgregarEmpleadoJornada";
    var Form = new FormData();
    Form.append("ID_Usuario", ID_Usuario);
    Form.append("ID_Horario", slHorarios.value);
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            let Respuesta = xhr.responseText.split("_");
            if (Respuesta[0] == "Correcto") {
                document.getElementById("stListadoPersona").style.display = "";
                document.getElementById("stCrearEmpleado3").style.display = "none";
                alertify.set('notifier', 'position', 'top-center');
                alertify.success('EL USUARIO SE CREO CORRECTAMENTE.');
                LimpiarCajasCrearUsuario();
                LimpiarCajasSelects();
                ListarPersona();
            } else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.error(Respuesta[1]);
            }
        }
    }
    xhr.send(Form);
}


function LimpiarCajasSelects() {
    slCargo.selectedIndex = 0;
    slTipoJornada.selectedIndex = 0;
    slPerfil.selectedIndex = 0;
}



var hdnIDUsuario = document.getElementById("hdnIDUsuario");
var hdnIDEmpleado = document.getElementById("hdnIDEmpleado");

function VerificarUsuarioEditar(stringCorreo) {
    var url = Servidor + "Maestros/VerificarExistencia";
    var Form = new FormData();
    Form.append("Correo", stringCorreo);
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            let Respuesta = xhr.responseText.split("_");
            if (Respuesta[0] == "CORRECTO") {
                if (Respuesta[1] == 0) {
                    UsuarioExiste = "NO EXISTE";
                    EditarEmpleado();

                } else if (Respuesta[1] == 1) {
                    UsuarioExiste = "EL USUARIO EXISTE";
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.warning(UsuarioExiste);
                }

            } else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.error(Respuesta[1]);
            }
        }
    }
    xhr.send(Form);

}


function DescripValor() {
    var url = Servidor + "Home/Desencripta";
    var Form = new FormData();
    Form.append("valor", txtEditarContraseña.value);
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            let Respuesta = xhr.responseText;
            let PassCor = Respuesta.split("_");
            Encri1 = PassCor[0];
            Encri2 = PassCor[1];
            txtEditarContraseña.value = Encri1;
            txtEditarConfirmacion.value = Encri1;
            Click = 1;
        }
    }
    xhr.send(Form);

}

var Encri1;
var Encri2;
function EncripValor() {
    var url = Servidor + "Home/Encripta";
    var Form = new FormData();
    Form.append("valor", Encri1 + '_' + Encri2);
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            let Respuesta = xhr.responseText;
            txtEditarContraseña.value = Respuesta;
            txtEditarConfirmacion.value = Respuesta;
            Click = undefined;
        }
    }
    xhr.send(Form);

}


var Click;
function MostrarPass() {
    if (Click == undefined) {
        DescripValor();
    } else if (Click == 1) {
        EncripValor();
    }
}

function VUEditar() {
    if (CorreoEditar == txtEditarCorreo.value) {
        EditarEmpleado();
    } else {
        VerificarUsuarioEditar(txtEditarCorreo.value);
    }

}

function EditarEmpleado() {

    if (txtEditarNombres.value != "" && txtEditarApellidos.value != "" &&
        txtEditarCorreo.value != "" && txtEditarDia.value != "" && slEditarMES.value != 0 && txtEditarAño.value != "" &&
        slCargoEditar.value != 0 && slEditarTipoDocumento.value != 0 && txtEditarNumeroDocumento.value != "" &&
        txtEditarContraseña.value != "" && slEditarPerfil.value != 0) {
        if (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(txtEditarCorreo.value)) {
            if (txtEditarContraseña.value !== "" && txtEditarConfirmacion.value !== "") {
                if (txtEditarContraseña.value == txtEditarConfirmacion.value) {
                    if (/(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,10}$/.test(txtEditarContraseña.value)) {
                        if (txtEditarCelular.value.length == 9) {
                            if (txtEditarDia.value.length <= 2 && txtEditarDia.value < 32 && txtEditarDia.value.length > 0) {
                                if (slEditarMES.value > 0) {
                                    if (txtEditarAño.value.length == 4 && txtEditarAño.value <= 2002) {
                                        if (txtEditarNumeroDocumento.value.length == VLongitudEditar) {
                                            if (slEditarPerfil.value > 0 && slCargoEditar.value > 0) {
                                                if (slEditarGenero.value != 0) {
                                                    var url = Servidor + "Maestros/EditarPersona";
                                                    var Form = new FormData();
                                                    Form.append("ID_Persona", hdnIDEmpleado.value);
                                                    Form.append("ID_Usuario", hdnIDUsuario.value);
                                                    Form.append("Nombres", txtEditarNombres.value);
                                                    Form.append("Apellidos", txtEditarApellidos.value);
                                                    Form.append("ID_Genero", slEditarGenero.value);
                                                    Form.append("Correo", txtEditarCorreo.value);
                                                    Form.append("Movil", txtEditarCelular.value);
                                                    var FechaNacimiento = txtEditarDia.value + '/' + slEditarMES.value + '/' + txtEditarAño.value;
                                                    Form.append("FechaNacimiento", FechaNacimiento);
                                                    Form.append("ID_Cargo", slCargoEditar.value);
                                                    Form.append("ID_TipoDocumento", slEditarTipoDocumento.value);
                                                    Form.append("NumeroDocumento", txtEditarNumeroDocumento.value);
                                                    Form.append("NombreUsuario", txtEditarCorreo.value);
                                                    Form.append("Clave", txtEditarContraseña.value);
                                                    Form.append("ID_Perfil", slEditarPerfil.value);
                                                    var xhr = new XMLHttpRequest();
                                                    xhr.open("post", url, true);
                                                    xhr.onloadstart = function () {
                                                    }
                                                    xhr.onreadystatechange = function () {
                                                        if (xhr.readyState == 4 && xhr.status == 200) {

                                                            let Respuesta = xhr.responseText.split("_");
                                                            if (Respuesta[0] == "Correcto") {
                                                                document.getElementById("stListadoPersona").style.display = "";
                                                                document.getElementById("stEditarDatosEmpleado").style.display = "none";
                                                                LimpiarCajasEditar();
                                                                alertify.set('notifier', 'position', 'top-center');
                                                                alertify.success('SE ACTUALIZO CORRECTAMENTE EL USUARIO');
                                                                ListarPersona();


                                                            } else {
                                                                alertify.set('notifier', 'position', 'top-center');
                                                                alertify.error(Respuesta[1]);

                                                            }
                                                        }
                                                    }
                                                    xhr.send(Form);
                                                } else {
                                                    alertify.set('notifier', 'position', 'top-center');
                                                    alertify.warning('SELECCIONE UN GÉNERO');
                                                }
                                            } else {
                                                alertify.set('notifier', 'position', 'top-center');
                                                alertify.warning('SELECCIONE LAS LISTAS FALTANTES');
                                            }

                                        } else {
                                            alertify.set('notifier', 'position', 'top-center');
                                            alertify.warning('EL NÚMERO DE DOCUMENTO ELEGIDO DEBE TENER ' + VLongitudEditar + " DÍGITOS");
                                        }
                                    } else {

                                        alertify.set('notifier', 'position', 'top-center');
                                        alertify.warning('AGREGUE UN AÑO VALIDO');
                                    }
                                } else {
                                    alertify.set('notifier', 'position', 'top-center');
                                    alertify.warning('SELECCIONE UN MES');
                                }
                            } else {
                                alertify.set('notifier', 'position', 'top-center');
                                alertify.warning('INGRESE UN DÍA DE MES CORRECTO');
                            }
                        } else {
                            alertify.set('notifier', 'position', 'top-center');
                            alertify.warning('EL NÚMERO DE CELULAR DEBE TENER 9 DÍGITOS');

                        }
                    } else {
                        alertify.set('notifier', 'position', 'top-center');
                        alertify.warning('LA CONTRASEÑA DEBE CONTENER ENTRE 8 Y 10 DÍGITOS CON MINÚSCULAS Y MAYÚSCULAS');

                    }

                } else {

                    alertify.set('notifier', 'position', 'top-center');
                    alertify.warning('VALIDE QUE LAS CONTRASEÑAS SEAN IGUALES');
                }

            } else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.warning('LOS CAMPOS DE CONTRASEÑA NO DEBEN ESTAR VACIO');

            }
        } else {
            alertify.set('notifier', 'position', 'top-center');
            alertify.warning('AGREGA UNA DIRECCIÓN DE CORREO VALIDA');

        }

    } else {
        alertify.set('notifier', 'position', 'top-center');
        alertify.warning('FALTAN RELLENAR CAMPOS');
    }

}

var CorreoEditar;
function AbrirEditar(ID) {
    document.getElementById("stListadoPersona").style.display = "none";
    document.getElementById("stEditarDatosEmpleado").style.display = "";
    for (var i = 0; i < DataEmpleado.length; i++) {
        if (DataEmpleado[i].ID_Persona == ID) {

            txtEditarNombres.value = DataEmpleado[i].Nombres;
            txtEditarApellidos.value = DataEmpleado[i].Apellidos;
            CorreoEditar = DataEmpleado[i].Correo;
            txtEditarCorreo.value = DataEmpleado[i].Correo;
            txtEditarContraseña.value = DataEmpleado[i].Clave;
            txtEditarConfirmacion.value = DataEmpleado[i].Clave;
            txtEditarCelular.value = DataEmpleado[i].Movil;
            var ConvertirFecha = DataEmpleado[i].FechaNacimiento.substr(6);
            var FechaActual = new Date(parseInt(ConvertirFecha));
            var MES = FechaActual.getMonth() + 1;
            var DIA = FechaActual.getDate();
            var ANIO = FechaActual.getFullYear();
            DIA = checkTime(DIA);
            txtEditarDia.value = DIA;
            slEditarMES.value = MES;
            txtEditarAño.value = ANIO;
            slEditarGenero.value = DataEmpleado[i].ID_Genero;
            slEditarTipoDocumento.value = DataEmpleado[i].ID_TipoDocumento;
            txtEditarNumeroDocumento.value = DataEmpleado[i].NumeroDocumento;
            slEditarPerfil.value = DataEmpleado[i].ID_Perfil;
            slCargoEditar.value = DataEmpleado[i].ID_Cargo;
            hdnIDEmpleado.value = ID;
            hdnIDUsuario.value = DataEmpleado[i].ID_Usuario;
        }
    }
    LongitudEditarCargar();
}


function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

//Cerrar Vista Editar Jornada
function CancelarEditar() {
    document.getElementById("stListadoPersona").style.display = "";
    document.getElementById("stEditarDatosEmpleado").style.display = "none";
    LimpiarCajasEditar();
    ListarPersona();
}





//Listar Combo Tipo Documento Editar
var slEditarTipoDocumento;
function ListadoComboTipoDocumentoEditar(DTipoDocumento) {
    var cbxEditarTipoDocumento = document.getElementById("cbxEditarTipoDocumento");
    var Contenido = "";
    Contenido += "<select id='slEditarTipoDocumento' onchange='LongitudEditar()' class='browser-default custom-select'>";
    Contenido += "<option value=0 selected>Seleccione</option>";
    for (var i = 0; i < DTipoDocumento.length; i++) {
        Contenido += "<option value=" + DTipoDocumento[i].ID_TipoDocumento + ">" + DTipoDocumento[i].Descripcion + "</option>";
    }
    Contenido += "</select>";
    cbxEditarTipoDocumento.innerHTML = Contenido;
    slEditarTipoDocumento = document.getElementById("slEditarTipoDocumento");
}

var divTBPersona = document.getElementById("divTBPersona");



function PintarTabla(DEmpleado) {
    var Contador = 1;
    if (DEmpleado !== null) {
        var Contenido = "";
        Contenido += "<div class='row'>";
        Contenido += "<div class='col-lg-12'>";
        Contenido += "<table id='TBPersona' class='table table-bordered'>";
        Contenido += "<thead>";
        Contenido += "<tr>";
        Contenido += "<th>#</th>";
        Contenido += "<th>NOMBRES & APELLIDOS</th>";
        Contenido += "<th>N° DE DOCUMENTO</th>";
        Contenido += "<th>CORREO</th>";
        Contenido += "<th>CARGO</th>";
        Contenido += "<th>ESTADO</th>";
        Contenido += "<th>ACCIONES</th>";
        Contenido += "</tr>";
        Contenido += "</thead>";
        Contenido += "<tbody>";
        for (var i = 0; i < DEmpleado.length; i++) {
            Contenido += "<tr>";

            Contenido += "<td>" + Contador++ + "</td>";
            Contenido += "<td> " + DEmpleado[i].Nombres + ' ' + DEmpleado[i].Apellidos + " </td>";
            Contenido += "<td> " + DEmpleado[i].NumeroDocumento + " </td>";
            Contenido += "<td> " + DEmpleado[i].Correo + " </td>";
            Contenido += "<td> " + DEmpleado[i].Cargo + " </td>";
            if (DEmpleado[i].ID_Estado == 1) {
                Contenido += "<td>";
                Contenido += "<a class='material-icons';>check_box</a>";
                Contenido += "</td>";
            } else if (DEmpleado[i].ID_Estado == 3) {
                Contenido += "<td>";
                Contenido += "<a class='material-icons';>lock</a>";
                Contenido += "</td>";
            }

            Contenido += "<td>";

            //Contenido += "<a class='material-icons'; onclick='AbrirEditar(" + DEmpleado[i].ID_Persona + " )'> edit </a>";
            Contenido += "<a id='#Eliminar' class='material-icons' onclick='ModalEliminar(" + DEmpleado[i].ID_Usuario + " )'>delete</a>";
          
            Contenido += "</td>";
            Contenido += "</tr>";

        }
        Contenido += "</tbody>";
        Contenido += "</table>";
        Contenido += "</div>";
        Contenido += "</div>";


        divTBPersona.innerHTML = Contenido;
    } else {
        alertify.set('notifier', 'position', 'top-center');
        alertify.warning('NO HAY DATA');
    }


}

function ModalActivar(ID, AU) {
    if (AU == 3) {
        alert("¿ESTA SEGURO DE ACTIVAR ESTE USUARIO?");
    } else {
        alert("EL USUARIO YA HA SIDO ACTIVADO");

    }
}

function ListarMovil(ID, SM) {
    if (SM == 0) {
        alertify.set('notifier', 'position', 'top-center');
        alertify.error("EL USUARIO AÚN NO INICIA SESIÓN EN EL DISPOSITIVO");
    } else {
        var url = Servidor + "Maestros/JListadoMovilUsuario";
        var Form = new FormData();
        Form.append("ID_Usuario", ID);
        var xhr = new XMLHttpRequest();
        xhr.open("post", url, true);
        xhr.onloadstart = function () {
        }
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {

                Respuesta = JSON.parse(xhr.response);
                if (Respuesta.ExisteError == false) {
                    JSON.stringify(Respuesta.Data);
                    if (Respuesta.Data.length != 0) {
                        ModalMovil(Respuesta.Data);
                    } else {
                        alertify.set('notifier', 'position', 'top-center');
                        alertify.error("NO HAY DATA");
                    }
                   
                }
                else {
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.error(Respuesta.MensajeError);


                }
            }
        }
        xhr.send(Form);

    }
}

var idModalBody = document.getElementById("idModalBody");
var exampleModalLongTitle = document.getElementById("exampleModalLongTitle");

function ModalMovil(DataMovil) {
    $("#myModal").modal('show');
    var Contenido = "";

    Contenido += "";
    Contenido += "<div class='form-row'>";
    Contenido += "<div class='col-md-6 mb-2'>";
    Contenido += "<label>Model :</label>";
    Contenido += "<input type='text' class='form-control' id='txtModel' value='" + DataMovil[0].Model +"' required>";
    Contenido += "</div>";
    Contenido += "<div class='col-md-6 mb-3'>";
    Contenido += "<label>Manufacturer :</label>";
    Contenido += "<input type='text' id='txtManufacturer' value='" + DataMovil[0].Manufacturer +"' required class='form-control'/>";
    Contenido += "</div>";
    Contenido += " </div>";
    Contenido += "<button type='button' class='btn btn-secondary' data-dismiss='modal'>Cerrar</button>";
    Contenido += "<button type='button' onclick='Editar(" + DataMovil.ID_Usuario + ")' class='btn btn-secondary' data-dismiss='modal'>Guardar cambios</button>";
    idModalBody.style.display = "";
    idmf.style.display = "none";
    exampleModalLongTitle.innerHTML = "INFORMACIÓN DE DISPOSITIVO";
    idModalBody.innerHTML = Contenido;
}



var idmf = document.getElementById("idmf");
function ModalEliminar(ID) {
    $("#myModal").modal('show');
    var Contenido = "";
    Contenido += "";
    Contenido += "<button type='button' class='btn btn-secondary' data-dismiss='modal'>Cerrar</button>";
    Contenido += "<button type='button' onclick='Eliminar(" + ID + ")' class='btn btn-secondary' data-dismiss='modal'>Eliminar</button>";
    exampleModalLongTitle.innerHTML = "¿Esta seguro de eliminar el usuario ?";
    idModalBody.style.display = "none";
    idmf.style.display = "";
    idmf.innerHTML = Contenido;
}


function Eliminar(ID) {


    var url = Servidor + "Maestros/EliminarUsuario";
    var Form = new FormData();
    Form.append("ID_Usuario", ID);
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            let Respuesta = xhr.responseText.split("_");
            if (Respuesta[0] == "Correcto") {
                ListarPersona();
            } else {
                alert(Respuesta[1]);
            }
        }
    }
    xhr.send(Form);

}


//Mostrar Vista Crear Jornada
function Mostrar() {
    document.getElementById("stListadoPersona").style.display = "none";
    document.getElementById("stCrearEmpleado1").style.display = "";

}

//ID INPUTS P1

var txtAgregarNombre = document.getElementById("txtAgregarNombre");
var txtAgregarApellidos = document.getElementById("txtAgregarApellidos");
var txtAgregarCorreo = document.getElementById("txtAgregarCorreo");
var txtAgregarContraseña = document.getElementById("txtAgregarContraseña");
var txtAgregarConfirmacion = document.getElementById("txtAgregarConfirmacion");

function LimpiarCajasCrearUsuario() {
    txtAgregarNombre.value = "";
    txtAgregarApellidos.value = "";
    txtAgregarCorreo.value = "";
    txtAgregarContraseña.value = "";
    txtAgregarConfirmacion.value = "";
}

function LimpiarClaves() {
    txtAgregarContraseña.value = "";
    txtAgregarConfirmacion.value = "";
    $('#idVerContraseña input').attr('type', 'password');
}

$(document).ready(function () {
    $("#idVerContraseña a").on('click', function (event) {
        event.preventDefault();
        if ($('#idVerContraseña input').attr("type") == "text") {
            $('#idVerContraseña input').attr('type', 'password');
            $('#ideyeshow').text("visibility");

        } else if ($('#idVerContraseña input').attr("type") == "password") {
            $('#idVerContraseña input').attr('type', 'text');
            $('#ideyeshow').text("visibility_off");
        }
    });
});


//Cerrar Sección Crear Usuario
function CancelarSeccionCrearUsuario() {
    document.getElementById("stListadoPersona").style.display = "";
    document.getElementById("stCrearEmpleado1").style.display = "none";
    LimpiarCajasCrearUsuario();
}

//Cerrar Datos Generales
function CancelarP2() {
    document.getElementById("stCrearEmpleado1").style.display = "";
    document.getElementById("stCrearEmpleado2").style.display = "none";
    LimpiarClaves();
}

//Cerrar Datos Asignaciones
function CancelarP3() {
    document.getElementById("stCrearEmpleado2").style.display = "";
    document.getElementById("stCrearEmpleado3").style.display = "none";

}

function LimpiarCajasGenerales() {
    slTipoDocumento.selectedIndex = 0;
    txtNumeroDocumento.value = "";
    slMES.value = 0;
    slGenero.selectedIndex = 0;


    txtAgregarCelular.value = "";
    txtAgregarDia.value = "";

    txtAgregarAño.value = "";
    TipoDocumento = 0;
}

var UsuarioExiste;
function VerificarUsuario(stringCorreo) {


    var url = Servidor + "Maestros/VerificarExistencia";
    var Form = new FormData();
    Form.append("Correo", stringCorreo);
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
    }
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            let Respuesta = xhr.responseText.split("_");
            if (Respuesta[0] == "CORRECTO") {
                if (Respuesta[1] == 0) {
                    UsuarioExiste = "NO EXISTE";
                    SiguienteP21();

                } else if (Respuesta[1] == 1) {
                    UsuarioExiste = "EL USUARIO EXISTE";
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.warning(UsuarioExiste);
                }

            } else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.error(Respuesta[1]);
            }
        }
    }
    xhr.send(Form);

}





function SiguienteP21() {
    if (txtAgregarNombre.value != "" && txtAgregarApellidos.value != "" &&
        txtAgregarCorreo.value != "") {
        if (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(txtAgregarCorreo.value)) {
            if (txtAgregarContraseña.value !== "" && txtAgregarConfirmacion.value !== "") {
                if (/^[A-Za-z]\w{7,14}$/.test(txtAgregarContraseña.value)) {

                    if (txtAgregarContraseña.value == txtAgregarConfirmacion.value) {

                        if (UsuarioExiste == "NO EXISTE") {
                            document.getElementById("stCrearEmpleado1").style.display = "none";
                            document.getElementById("stCrearEmpleado2").style.display = "";
                            LimpiarCajasGenerales();
                        } else {
                            alertify.set('notifier', 'position', 'top-center');
                            alertify.warning('EL USUARIO YA EXISTE');
                        }

                    } else {

                        alertify.set('notifier', 'position', 'top-center');
                        alertify.warning('VALIDE QUE LA CONTRASEÑA SEA IGUAL');
                        //  alert("VALIDE QUE LA CONTRASEÑA SEA IGUAL");
                    }
                } else {
                    alertify.set('notifier', 'position', 'top-center');
                    alertify.warning('LA CONTRASEÑA DEBE CONTENER ENTRE 8 Y 10 DÍGITOS CON MINÚSCULAS Y MAYÚSCULAS');
                }



            } else {
                alertify.set('notifier', 'position', 'top-center');
                alertify.warning('LOS CAMPOS DE CONTRASEÑA NO DEBEN ESTAR VACIO');

            }

        } else {
            alertify.set('notifier', 'position', 'top-center');
            alertify.warning('AGREGA UNA DIRECCIÓN DE CORREO VALIDA');

        }

    } else {

        alertify.set('notifier', 'position', 'top-center');
        alertify.warning('RELLENE LOS CAMPOS');
    }
}

function SiguienteP2() {
    VerificarUsuario(txtAgregarCorreo.value);
}




//Lista Perfil
function ListarPerfil() {
    var url = Servidor + "Maestros/JListadoPerfiles";
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
                PintarComboPerfil(Respuesta.Data);

            }
            else {
                WebNotifyAsBlock("error", "NO SE PUDO TRAER LA INFORMACIÓN", "ERROR!!");
            }
        }
    }
    xhr.send();
}


var slPerfil;

//Listar Combo Tipo Perfil
function PintarComboPerfil(DPerfil) {
    slPerfil = document.getElementById("slPerfil");
    var Contenido = "";
    Contenido += "<option value=0 selected>Seleccione</option>";
    for (var i = 0; i < DPerfil.length; i++) {
        Contenido += "<option value=" + DPerfil[i].ID_Perfil + ">" + DPerfil[i].Descripcion + "</option>";
    }
    Contenido += "</select>";
    slPerfil.innerHTML = Contenido;

}


//Listar Combo Tipo Perfil Editar

var slEditarPerfil;
function PintarComboPerfilEditar(DPerfil) {
    var cbxEditarPerfil = document.getElementById("cbxEditarPerfil");
    var Contenido = "";
    Contenido += "<select id='slEditarPerfil' class='browser-default custom-select'>";
    Contenido += "<option value=0 selected>Seleccione</option>";
    for (var i = 0; i < DPerfil.length; i++) {
        Contenido += "<option value=" + DPerfil[i].ID_Perfil + ">" + DPerfil[i].Descripcion + "</option>";
    }
    Contenido += "</select>";
    cbxEditarPerfil.innerHTML = Contenido;
    slEditarPerfil = document.getElementById("slEditarPerfil");
}


//Listar Cargo
function ListarCargo() {
    var url = Servidor + "Maestros/JListadoCargo";
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
                PintarComboCargo(Respuesta.Data);
            }
            else {
                WebNotifyAsBlock("error", "NO SE PUDO TRAER LA INFORMACIÓN", "ERROR!!");
            }
        }
    }
    xhr.send();
}

var slCargo;
//Listar Combo Cargo
function PintarComboCargo(DataCargo) {
    slCargo = document.getElementById("slCargo");
    var Contenido = "";
    Contenido += "<option value=0 selected>Seleccione</option>";
    for (var i = 0; i < DataCargo.length; i++) {
        Contenido += "<option value=" + DataCargo[i].ID_Cargo + ">" + DataCargo[i].Descripcion + "</option>";
    }
    Contenido += "</select>";
    slCargo.innerHTML = Contenido;
}

var slCargoEditar;
//Listar Combo Cargo
function PintarComboCargoEditar(DCargo) {
    var cbxEditarCargo = document.getElementById("cbxEditarCargo");
    var Contenido = "";
    Contenido += "<select id='slCargoEditar' class='browser-default custom-select'>";
    Contenido += "<option value=0 selected>Seleccione</option>";
    for (var i = 0; i < DCargo.length; i++) {
        Contenido += "<option value=" + DCargo[i].ID_Cargo + ">" + DCargo[i].Descripcion + "</option>";
    }
    Contenido += "</select>";
    cbxEditarCargo.innerHTML = Contenido;
    slCargoEditar = document.getElementById("slCargoEditar");

}

//Listar Tipo Jornada

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
                PintarComboTipoJornada(Respuesta.Data);

            }
            else {
                WebNotifyAsBlock("error", "NO SE PUDO TRAER LA INFORMACIÓN", "ERROR!!");
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



