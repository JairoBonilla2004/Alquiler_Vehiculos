import { ajax } from "../js/helpers/ajax.js"
import { showSuccessMessage } from "../js/components/Alerts.js"
const d = document;


const MESSAGES = {
    EMAIL_REGISTERED: "El correo ya se encuentra registrado"
};



export function iniciarSesion(event) {//evento submit iniciar sesión
    let email = event.target.email.value;
    let clave = event.target.loginPassword.value;
    let $content_validation = d.getElementById("content_iniciar_sesion");
    const $remember_password = d.getElementById("rememberMe");

    ajax({
        endpoint: "/Acceso/LogInPeticion",
        _method: "POST",
        content_type: "application/json",
        data: {
            email,
            clave
        },
        cbSuccess: (response) => {
            console.log(response);
            if (!response.success) {
                setTimeout(() => {
                    $content_validation.classList.add("Active");
                }, 10);
                $content_validation.textContent = response.message;
                event.target.email.value = "";
                event.target.loginPassword.value = "";
                $remember_password.checked = false;
                setTimeout(() => {
                    $content_validation.classList.remove("Active");
                }, 15000);
            } else {
                window.location.href = response.redirectUrl;

            }
        },
        cbError: (response) => {
            console.log(response);
        }
    });

}

export function registrarse(event) {
    let nombre = event.target.firstName.value;
    let apellido = event.target.lastName.value;
    let email = event.target.registerEmail.value;
    let telefono = event.target.phone.value;
    let cargo = event.target.cargo.value;
    let clave = event.target.registerPassword.value;
    const $loader = d.getElementById("spinner_loading");
    $loader.classList.add("spinner-border");
    $loader.textContent = "";

    ajax({
        endpoint: "/Acceso/Registrar",
        _method: "POST",
        content_type: "application/json",
        data: {
            nombre,
            apellido,
            email,
            telefono,
            cargo,
            clave
        },
        cbSuccess: (response) => {
            console.log(response);
            $loader.classList.remove("spinner-border");
            if (!response.success) {
                $loader.style.color = "red";
                $loader.innerHTML = response.message;//ir añadiendo clases de boobstrap

                if (response.message === MESSAGES.EMAIL_REGISTERED) {
                    event.target.registerEmail.value = "";
                }
            } else {
                showSuccessMessage(response);
            }
            
            console.log(response);
        },
        cbError: (response) => {
            console.log("error en la petición "+response);
        }
    });

}

export function limpiarCamposFormulario($formulario_inicio, $formulario_resgistrarse) {
    $formulario_resgistrarse.firstName.value = "";
    $formulario_resgistrarse.lastName.value = "";
    $formulario_resgistrarse.registerEmail.value = "";
    $formulario_resgistrarse.phone.value = "";
    $formulario_resgistrarse.cargo.value = "";
    $formulario_resgistrarse.registerPassword.value = "";
    $formulario_inicio.email.value = "";
    $formulario_inicio.loginPassword.value = "";
}