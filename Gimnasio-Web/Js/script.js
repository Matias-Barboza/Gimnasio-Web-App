function VincularDatosSocioAModal(apellido, nombre, codigoSocio, estado)
{
    let pInfoSocio = document.getElementById("infoSocioACambiarEstado");
    let codigoSocioModalLabel = document.getElementById("codigoSocioModalLabel");
    let codigoSocioHidden = document.getElementById("CodigoSocioHiddenField");
    let estadoActual = (estado.toLowerCase() === "true");
    let nuevoEstado = estadoActual == true ? "inactividad" : "actividad";

    codigoSocioHidden.value = codigoSocio.toString();

    pInfoSocio.innerText = "¿Pasar al socio: '" + apellido + ", " + nombre +
        "' a " + nuevoEstado + "?"; 
    codigoSocioModalLabel.innerText = "Código de socio: '" + codigoSocio + "'.";
}

function VincularDatosTipoCuotaAModal(descripcion, codigoTipoCuota, estado)
{
    let pInfoTipoCuota = document.getElementById("infoTipoCuotaACambiarEstado");
    let codigoTipoCuotaModalLabel = document.getElementById("codigoTipoCuotaModalLabel");
    let codigoTipoCuotaHidden = document.getElementById("CodigoTipoCuotaHiddenField");
    let estadoActual = (estado.toLowerCase() === "true");
    let nuevoEstado = estadoActual == true ? "inactiva" : "activa";

    codigoTipoCuotaHidden.value = codigoTipoCuota.toString();

    pInfoTipoCuota.innerText = "¿Pasar el tipo de cuota: '" + descripcion +
    "' a " + nuevoEstado + "?";
    codigoTipoCuotaModalLabel.innerText = "Código de tipo de cuota: '" + codigoTipoCuota + "'.";
}