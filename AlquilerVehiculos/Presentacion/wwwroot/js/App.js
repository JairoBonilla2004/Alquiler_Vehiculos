import { ajax } from "../js/helpers/ajax.js"
import { mostrarAlertaAjax } from "../js/helpers/mostrarAlertaAjax.js"
import { showSuccessMessage } from "../js/components/Alerts.js"
import { FormularioRestablecerEnviar } from "../js/components/FormularioRestablecerEnviado.js"
import { IncialiazarDataTable, Table } from "./components/Table.js";
import { OptionsElements } from "./components/OptionsElements.js"
const d = document;


const MESSAGES = {
    EMAIL_REGISTERED: "El correo ya se encuentra registrado"
};

/*
----  PETICIONES GENÉRICAS ----

*/
export function cargarSelect(props) {
    const { _endpoint, selectorById,key} = props;// la key me va a servir tanto para el value de los option como para el contenido

    ajax({
        endpoint: _endpoint,
        _method: "GET",
        content_type: "application/json",
        data: "",
        cbSuccess: (response) => {
            const $select_vehiculo = d.getElementById(selectorById);
            $select_vehiculo.innerHTML = OptionsElements(response, key);
        },
        cbError: (response) => {
            console.error(`Error en la petición al cargar ${selectorById}: `, response);
        }
    });
}


export function cargarTabla(props) {
    const { _endpoint, classNameTable, headersTable, keys, habilitar_acciondes, selectorById } = props;
    ajax(
        {
            endpoint: _endpoint,
            _method: "GET",
            content_type: "application/json",
            data: "",
            cbSuccess: (response) => {
                console.log("dcd",response);
                const $element = d.getElementById(selectorById);
                $element.innerHTML = Table(classNameTable, headersTable, response, keys, habilitar_acciondes);
   
                IncialiazarDataTable();
            },
            cbError: (response) => {
                console.log("error en la petición " + response);
            }
        }
    );
}

export function insertarDatosFetch(props) {
    const { _endpoint, method, obj_data } = props;
    console.log(obj_data);
    ajax(
        {
            endpoint: _endpoint,
            _method: method,
            content_type: "application/json",
            data: obj_data,
            cbSuccess: (response) => {
                if (response.success) {
                    console.log(response.success);
                    location.reload();
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: response.message || "Ocurrió un error al insertar",  // Si no hay message, usa el texto por defecto
                    });
                }
            },
            cbError: (response) => {
                console.log("error en la petición " + response);
            }
        }
    );
}

export function actualizarFetch(props) {
    const { _endpoint, obj_data } = props;

    mostrarAlertaAjax(
        "warning", 
        "¿Estás seguro?",
        "¿Deseas actualizar este registro?", 
        "Actualizar",
        "Cancelar",
        () => {
            ajax({
                endpoint: _endpoint,
                _method: "POST", 
                content_type: "application/json",
                data: obj_data,
                cbSuccess: (response) => {
                    location.reload(); 
                },
                cbError: (response) => {
                    console.log("Error en la petición de actualización: " + response);
                }
            });
        }
    );
}

export function eliminarFetch(_endpoint, nombre) {
  
    mostrarAlertaAjax(
        "warning",
        `¿Estás seguro de que quieres eliminar el vehículo ${nombre}`,
        "¡No podrás revertir esta acción!",
        "Sí, eliminarlo",
        "Cancelar",
        () => {
            ajax({
                endpoint: _endpoint,
                _method: "DELETE", 
                content_type: "application/json",
                data: "", 
                cbSuccess: (response) => {
                    location.reload(); 
                },
                cbError: (response) => {
                    console.log("Error en la petición de eliminación: " + response);
                }
            });
        }
    );
}



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
            console.log("error en la petición " + response);
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

export function recuperacionClave(event, $form) {
    if ($form.checkValidity()) {
        const submitBtn = $form.querySelector('button[type="submit"]');
        const originalText = submitBtn.innerHTML;

        // Change button to loading state
        submitBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>Enviando...';
        submitBtn.disabled = true;
        const email = event.target.email.value;
        console.log(email);
        //hacer una peticion post al recurso para enviar el formulario
        ajax({
            endpoint: "/Acceso/PeticionRestablecer",
            _method: "POST",
            content_type: "application/json",
            data: {
                email
            },
            cbSuccess: (response) => {
                console.log(response);

                const $formBody = document.querySelector('.form-body');
                $formBody.innerHTML = FormularioRestablecerEnviar(response);
            },
            cbError: (response) => {
                console.log("error en la petición " + response);
            }
        });

    } else {
        $form.classList.add('was-validated');
    }
}

export function confirmarRestablecimientoClave(event) {
    const submitBtn = document.getElementById("submitBtn");
    const token = event.target.token.value;
    const clave = event.target.password.value;
    console.log(event.target.token);
    if (!submitBtn.disabled) {
        submitBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>Procesando...';
        submitBtn.disabled = true;

        ajax({
            endpoint: "/Acceso/ActualizarClave",
            _method: "POST",
            content_type: "application/json",
            data: {
                token,
                clave
            },
            cbSuccess: (response) => {
                console.log(response);
                const $formBody = document.querySelector('.form-body');
                $formBody.innerHTML = FormularioRestablecerEnviar(response);
            },
            cbError: (response) => {
                console.log("error en la petición " + response);
            }
        });

    }
}

export function requerimientosFormulario() {
    const newPassword = document.getElementById("newPassword");
    const confirmPassword = document.getElementById("confirmPassword");
    const strengthMeter = document.getElementById("strengthMeter");
    const strengthText = document.getElementById("strengthText");
    const strengthPercentage = document.getElementById("strengthPercentage");
    const submitBtn = document.getElementById("submitBtn");
    const passwordMatchFeedback = document.getElementById("passwordMatchFeedback");
    const toggleNewPassword = document.getElementById("toggleNewPassword");
    const toggleConfirmPassword = document.getElementById("toggleConfirmPassword");

    const requirements = {
        length: document.getElementById("req-length"),
        uppercase: document.getElementById("req-uppercase"),
        lowercase: document.getElementById("req-lowercase"),
        number: document.getElementById("req-number"),
        special: document.getElementById("req-special")
    };


    toggleNewPassword.addEventListener("click", function () {
        togglePasswordVisibility(newPassword, this);
    });

    toggleConfirmPassword.addEventListener("click", function () {
        togglePasswordVisibility(confirmPassword, this);
    });

    function togglePasswordVisibility(input, button) {

        input.type = input.type === "password" ? "text" : "password";
        const icon = button.querySelector("i");
        if (input.type === "password") {
            icon.classList.remove("fa-eye-slash");
            icon.classList.add("fa-eye");
        } else {
            icon.classList.remove("fa-eye");
            icon.classList.add("fa-eye-slash");
        }
    }

    newPassword.addEventListener("input", function () {
        const password = newPassword.value;
        let score = 0;

        const checks = {
            length: password.length >= 8,
            uppercase: /[A-Z]/.test(password),
            lowercase: /[a-z]/.test(password),
            number: /[0-9]/.test(password),
            special: /[!@#$%^&*(),.?":{}|<>]/.test(password)
        };


        for (const key in checks) {
            const element = requirements[key];
            const icon = element.querySelector("i");

            if (checks[key]) {
                element.classList.add("valid");
                element.classList.remove("invalid");
                icon.classList.remove("fa-circle");
                icon.classList.add("fa-check-circle");
                score++;
            } else {
                element.classList.remove("valid");
                element.classList.add("invalid");
                icon.classList.remove("fa-check-circle");
                icon.classList.add("fa-circle");
            }
        }


        const strengthLevels = ["Débil", "Regular", "Buena", "Fuerte", "Muy Fuerte"];
        const scorePercentage = (score / 5) * 100;

        strengthMeter.style.width = `${scorePercentage}%`;

        if (scorePercentage <= 20) {
            strengthMeter.style.backgroundColor = "#e74c3c"; // Rojo - Débil
        } else if (scorePercentage <= 40) {
            strengthMeter.style.backgroundColor = "#f39c12"; // Naranja - Regular
        } else if (scorePercentage <= 60) {
            strengthMeter.style.backgroundColor = "#f1c40f"; // Amarillo - Buena
        } else if (scorePercentage <= 80) {
            strengthMeter.style.backgroundColor = "#2ecc71"; // Verde claro - Fuerte
        } else {
            strengthMeter.style.backgroundColor = "#27ae60"; // Verde oscuro - Muy Fuerte
        }

        strengthText.textContent = `Fuerza: ${score > 0 ? strengthLevels[Math.min(score - 1, 4)] : "No ingresada"}`;
        strengthPercentage.textContent = `${scorePercentage}%`;

        validatePasswords();
    });

    confirmPassword.addEventListener("input", validatePasswords);

    function validatePasswords() {
        const password = newPassword.value;
        const confirm = confirmPassword.value;

        if (confirm === "") {
            confirmPassword.classList.remove("is-invalid");
            passwordMatchFeedback.style.display = "none";
        } else if (password === confirm) {
            confirmPassword.classList.remove("is-invalid");
            confirmPassword.classList.add("is-valid");
            passwordMatchFeedback.style.display = "none";
        } else {
            confirmPassword.classList.add("is-invalid");
            confirmPassword.classList.remove("is-valid");
            passwordMatchFeedback.style.display = "block";
        }

        const allRequirementsMet = Array.from(
            document.querySelectorAll(".requirement")
        ).every(req => req.classList.contains("valid"));

        submitBtn.disabled = !(allRequirementsMet && password === confirm && password !== "");
    }
}

