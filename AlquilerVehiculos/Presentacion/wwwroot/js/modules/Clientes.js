import { actualizarFetch, cargarTabla, eliminarFetch, insertarDatosFetch } from "../App.js";
let d = document;

const $hidden = d.querySelector(".btn-hidden");
const $formulario_cliente = d.getElementById("formulario-cliente");

d.addEventListener("DOMContentLoaded", e => {
    cargarTabla({
        _endpoint: "/Cliente/ObtenerDatos",
        classNameTable: "tablaCliente",
        headersTable: ["Id", "Nombre", "Apellido", "Teléfono", "Correo Electrónico", "Acción"],
        keys: ["id", "nombre", "apellido", "telefono", "email"],
        habilitar_acciondes: true,
        selectorById: "contenedor-tabla-cliente"
    });
});

d.addEventListener("submit", e => {
    e.preventDefault();
    if (e.target.matches("#formulario-cliente")) {
        let id = e.target.id.value;
        let nombre = e.target.nombre.value;
        let apellido = e.target.apellido.value;
        let telefono = e.target.telefono.value;
        let email = e.target.email.value;

        let data = {
            id,
            nombre,
            apellido,
            telefono,
            email
        };

        if (!$hidden.id) {
            data.id = 0; // Indicar que es una inserción
            insertarDatosFetch({
                _endpoint: "/Cliente/InsertarDatos",
                method: "POST",
                obj_data: data
            });
        } else {
            actualizarFetch({
                _endpoint: "/Cliente/ActualizarDatos",
                obj_data: data
            });
        }
    }
});

d.addEventListener("click", e => {
    const $btn_editar = e.target.closest(".editar");
    const $btn_eliminar = e.target.closest(".eliminar");
    const $input_id = d.getElementById("id");
    const $label_id = $input_id.previousElementSibling;

    if ($btn_editar) {
        $formulario_cliente.id.value = $btn_editar.dataset.id;
        $formulario_cliente.nombre.value = $btn_editar.dataset.nombre;
        $formulario_cliente.apellido.value = $btn_editar.dataset.apellido;
        $formulario_cliente.telefono.value = $btn_editar.dataset.telefono;
        $formulario_cliente.email.value = $btn_editar.dataset.email;

        $hidden.id = $btn_editar.dataset.id;
        $input_id.style.display = "block";
        $label_id.style.display = "block";
    }

    if ($btn_eliminar) {
        eliminarFetch(`/Cliente/EliminarDatos/${$btn_eliminar.dataset.id}`, $btn_eliminar.dataset.nombre);
    }

    if (e.target.matches("#crear_cliente_nuevo")) {
        const $input_id = d.getElementById("id");
        const $label_id = $input_id.previousElementSibling;
        $hidden.id = "";
        $formulario_cliente.id.value = "";
        $formulario_cliente.nombre.value = "";
        $formulario_cliente.apellido.value = "";
        $formulario_cliente.telefono.value = "";
        $formulario_cliente.email.value = "";

        $input_id.style.display = "none";
        $label_id.style.display = "none";
        console.log("ddcdcdc",$hidden);
        $hidden.id = "";
    }
});