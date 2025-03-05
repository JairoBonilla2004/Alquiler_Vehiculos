import { actualizarFetch, cargarTabla, eliminarFetch, insertarDatosFetch } from "../App.js";

let d = document;

const $hidden = d.querySelector(".btn-hidden");
const $formulario_empleado = d.getElementById("formulario-empleado");

d.addEventListener("DOMContentLoaded", () => {
    cargarTabla({
        _endpoint: "/Empleado/ObtenerEmpleados",
        classNameTable: "tablaEmpleado",
        headersTable: ["Nombre", "Apellido", "Cargo", "Teléfono", "Email"],
        keys: ["nombre", "apellido", "cargo", "telefono", "email"],
        habilitar_acciondes: false,
        selectorById: "contenedor-tabla-empleado"
    });
});
