import { actualizarFetch, cargarTabla, eliminarFetch, insertarDatosFetch } from "../App.js";

let d = document;
const $hidden = d.querySelector(".btn-hidden");
const $formulario_seguro = d.getElementById("formulario-seguro");
const $reservaSelect = d.getElementById("reservaId");


d.addEventListener("DOMContentLoaded", async () => {
    await cargarReservas(); 

    cargarTabla({
        _endpoint: "/Seguro/ObtenerSeguros",
        classNameTable: "tablaSeguro",
        headersTable: ["Id", "Reserva", "Cliente", "Tipo de Seguro", "Costo"],
        keys: ["id", "reservaId", "clienteNombre", "tipoSeguro", "costo"],
        habilitar_acciondes: false,
        selectorById: "contenedor-tabla-seguro"
    });
});


async function cargarReservas(reservaIdSeleccionado = null) {
    try {
        let response = await fetch("/Seguro/ObtenerReservasSinSeguro"); 
        let reservas = await response.json();

        $reservaSelect.innerHTML = '<option value="">Seleccione una Reserva</option>';

        if (reservaIdSeleccionado) {
            let responseReservaActual = await fetch(`/Reserva/ObtenerReserva/${reservaIdSeleccionado}`);
            let reservaActual = await responseReservaActual.json();
            console.log("ddcd",reservaActual);
            if (reservaActual) {
                let optionActual = document.createElement("option");
                optionActual.value = reservaActual.id;
                optionActual.textContent = `Reserva #${reservaActual.id} - ${reservaActual.clienteNombre}`;
                optionActual.selected = true;
                $reservaSelect.appendChild(optionActual);
            }
        }

  
        reservas.forEach(reserva => {
            let option = document.createElement("option");
            option.value = reserva.reservaId;
            option.textContent = `Reserva #${reserva.reservaId} - ${reserva.clienteNombre}`;

            $reservaSelect.appendChild(option);
        });

    } catch (error) {
        console.error(" Error al cargar reservas:", error);
    }
}




d.addEventListener("submit", e => {
    e.preventDefault();
    if (e.target.matches("#formulario-seguro")) {
        let reservaId = e.target.reservaId.value;
        let tipoSeguro = e.target.tipoSeguro.value;
        let costo = e.target.costo.value;
        let id = e.target.id.value;

        let data = { reservaId, tipoSeguro, costo, id };
        console.log(data);
        if (!$hidden.id) {
            data.id = 0;
            insertarDatosFetch({
                _endpoint: "/Seguro/InsertarDatos",
                method: "POST",
                obj_data: data
            });
        } else {
            actualizarFetch({
                _endpoint: "/Seguro/ActualizarDatos",
                obj_data: data
            });
        }
    }
});


d.addEventListener("click", async e => {
    const $btn_editar = e.target.closest(".editar");

    if ($btn_editar) {
        console.log("Editando seguro:", $hidden);
        $formulario_seguro.id.value = $btn_editar.dataset.id;
        $formulario_seguro.tipoSeguro.value = $btn_editar.dataset.tiposeguro;
        $formulario_seguro.costo.value = $btn_editar.dataset.costo;


        await cargarReservas($btn_editar.dataset.reservaid);

        $hidden.id = $btn_editar.dataset.id;
    }
});


