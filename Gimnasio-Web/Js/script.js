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