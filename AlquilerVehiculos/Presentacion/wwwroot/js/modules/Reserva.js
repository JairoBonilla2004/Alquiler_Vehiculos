import { actualizarFetch, cargarSelect, cargarTabla, eliminarFetch, insertarDatosFetch } from "../App.js";

let d = document;

const $hidden = d.querySelector(".btn-hidden");
const $formulario_reserva = d.getElementById("formulario-reserva");

d.addEventListener("DOMContentLoaded", () => {
    cargarTabla({
        _endpoint: "/Reserva/ObtenerReservas",
        classNameTable: "tablaReserva",
        headersTable: ["Id", "Cliente", "Vehículo", "Fecha Inicio", "Fecha Fin"],
        keys: ["id", "clienteNombre", "vehiculoNombre", "fechaInicio", "fechaFin"],
        habilitar_acciondes: false,
        selectorById: "contenedor-tabla-reserva"
    });
});

d.addEventListener("submit", e => {
    e.preventDefault();
    if (e.target.matches("#formulario-reserva")) {
        let id = e.target.id.value;
        let clienteId = e.target.clienteId.value;
        let vehiculoId = e.target.vehiculoId.value;
        let fechaInicio = e.target.fechaInicio.value;
        let fechaFin = e.target.fechaFin.value;
        let estado = e.target.estado.value;

        let data = {
            id,
            clienteId,
            vehiculoId,
            fechaInicio,
            fechaFin,
            estado
        };

        if (!$hidden.id) {
            data.id = 0;
            insertarDatosFetch({
                _endpoint: "/Reserva/InsertarDatos",
                method: "POST",
                obj_data: data
            });
        } else {
            console.log()
            actualizarFetch({
                _endpoint: "/Reserva/ActualizarDatos",
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
        // Asignar valores al formulario
        $formulario_reserva.id.value = $btn_editar.dataset.id;
        $formulario_reserva.fechaInicio.value = $btn_editar.dataset.fechainicio.split("T")[0];
        $formulario_reserva.fechaFin.value = $btn_editar.dataset.fechafin.split("T")[0];
        $formulario_reserva.estado.value = $btn_editar.dataset.estado;

        // Asignar Cliente y Vehículo a los input de solo lectura
        $formulario_reserva.clienteId.value = $btn_editar.dataset.clientenombre;
        $formulario_reserva.vehiculoId.value = $btn_editar.dataset.vehiculonombre;

        // Mostrar el campo ID
        $hidden.id = $btn_editar.dataset.id;
        $input_id.style.display = "block";
        $label_id.style.display = "block";
    }

    if ($btn_eliminar) {
        eliminarFetch(`/Reserva/EliminarDatos/${$btn_eliminar.dataset.id}`, "Reserva");
    }

    if (e.target.matches("#crear_reserva_nueva")) {
        $hidden.id = "";
        $formulario_reserva.reset();
        $input_id.style.display = "none";
        $label_id.style.display = "none";
    }
});
