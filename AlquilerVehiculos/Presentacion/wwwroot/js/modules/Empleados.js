import { iniciarSesion, limpiarCamposFormulario, registrarse } from "../App.js"
const d = document;


let $formulario_resgistrarse = d.getElementById("registerForm");
let $formulario_inicio = d.getElementById("loginForm");
d.addEventListener("submit", e => {
    if (e.target.matches("#loginForm")) {
        e.preventDefault();
        iniciarSesion(e);
    }

    if (e.target.matches("#registerForm")) {
        e.preventDefault();
        registrarse(e);
    }
});


d.addEventListener("click", e => {
    if (e.target.matches("#register-tab") || e.target.matches("#login-tab")) {

        limpiarCamposFormulario($formulario_inicio, $formulario_resgistrarse);
    }

    
});