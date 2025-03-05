import { actualizarFetch, cargarTabla, eliminarFetch, insertarDatosFetch } from "../App.js";

let d = document;
const $hidden = d.querySelector(".btn-hidden");
const $formulario_pago = d.getElementById("formulario-pago");
const $reservaSelect = d.getElementById("reservaId");

d.addEventListener("DOMContentLoaded", async () => {
    await cargarReservas(); 

    cargarTabla({
        _endpoint: "/Pago/ObtenerPagos",
        classNameTable: "tablaPago",
        headersTable: ["Id", "Reserva", "Monto", "Método de Pago", "Fecha de Pago", "Acción"],
        keys: ["id", "reservaId", "monto", "metodoPago", "fechaPago"],
        habilitar_acciondes: true,
        selectorById: "contenedor-tabla-pago"
    });
});

async function cargarReservas(reservaIdSeleccionado = null) {
    try {
        let response = await fetch("/Reserva/ObtenerReservas");
        let reservas = await response.json();

        $reservaSelect.innerHTML = '<option value="">Seleccione una Reserva</option>';
        reservas.forEach(reserva => {
            let option = document.createElement("option");
            option.value = reserva.id;
            option.textContent = `Reserva #${reserva.id} - ${reserva.clienteNombre}`;

            if (reservaIdSeleccionado && reserva.id == reservaIdSeleccionado) {
                option.selected = true;
            }

            $reservaSelect.appendChild(option);
        });
    } catch (error) {
        console.error("Error al cargar reservas:", error);
    }
}

d.addEventListener("submit", e => {
    e.preventDefault();
    if (e.target.matches("#formulario-pago")) {
        let reservaId = e.target.reservaId.value;
        let monto = e.target.monto.value;
        let metodoPago = e.target.metodoPago.value;
        let fechaPago = e.target.fechaPago.value;
        let id = e.target.id.value;

        let data = { reservaId, monto, metodoPago, fechaPago, id };

        if (!$hidden.id) {
            data.id = 0;
            insertarDatosFetch({
                _endpoint: "/Pago/InsertarDatos",
                method: "POST",
                obj_data: data
            });
        } else {
            actualizarFetch({
                _endpoint: "/Pago/ActualizarDatos",
                obj_data: data
            });
        }
    }
});

d.addEventListener("click", async e => {
    const $btn_editar = e.target.closest(".editar");
    const $btn_eliminar = e.target.closest(".eliminar");

    if ($btn_editar) {
        console.log("Editando pago:", $hidden);
        $formulario_pago.id.value = $btn_editar.dataset.id;
        $formulario_pago.monto.value = $btn_editar.dataset.monto;
        $formulario_pago.metodoPago.value = $btn_editar.dataset.metodopago;
        $formulario_pago.fechaPago.value = $btn_editar.dataset.fechapago;

        await cargarReservas($btn_editar.dataset.reservaid);
        $hidden.id = $btn_editar.dataset.id;
    }

    if ($btn_eliminar) {
        console.log("Eliminando pago:", $btn_eliminar.id);
        eliminarFetch(`/Pago/EliminarDatos/${$btn_eliminar.dataset.id}`, `Pago ID ${$btn_eliminar.dataset.id}`);
    }

    if (e.target.matches("#crear_pago_nuevo")) {
        $hidden.id = "";
        $formulario_pago.id.value = "";
        $formulario_pago.monto.value = "";
        $formulario_pago.metodoPago.value = "Efectivo";
        $formulario_pago.fechaPago.value = "";

        // Cargar reservas disponibles
        await cargarReservas();
    }
});
