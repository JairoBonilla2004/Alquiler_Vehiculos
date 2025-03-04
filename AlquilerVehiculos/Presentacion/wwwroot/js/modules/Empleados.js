import { confirmarRestablecimientoClave, iniciarSesion, limpiarCamposFormulario, recuperacionClave, registrarse, requerimientosFormulario } from "../App.js"
const d = document;


let $formulario_resgistrarse = d.getElementById("registerForm");
let $formulario_inicio = d.getElementById("loginForm");
let $formulario_restablecer = d.getElementById("recoveryForm");


d.addEventListener("DOMContentLoaded", e => {
    if (document.body.id === "formulario_restablecer_cuenta") {
        requerimientosFormulario();
    }

});

d.addEventListener("submit", e => {

    if (e.target.matches("#loginForm")) {
        e.preventDefault();
        iniciarSesion(e);
    }

    if (e.target.matches("#registerForm")) {
        e.preventDefault();
        registrarse(e);
    }

    if (e.target.matches("#recoveryForm")) {
        e.preventDefault();
        recuperacionClave(e, $formulario_restablecer);
    }

    if (e.target.matches("#resetForm")) {
        e.preventDefault();
        confirmarRestablecimientoClave(e);
    }
});


d.addEventListener("click", e => {
    if (e.target.matches("#register-tab") || e.target.matches("#login-tab")) {

        limpiarCamposFormulario($formulario_inicio, $formulario_resgistrarse);
    }
});