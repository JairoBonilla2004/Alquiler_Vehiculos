import { actualizarFetch, cargarTabla, eliminarFetch, insertarDatosFetch } from "../App.js";

let d = document;

const $hidden = d.querySelector(".btn-hidden");
const $formulario_vehiculo = d.getElementById("formulario-vehiculo");

d.addEventListener("DOMContentLoaded", () => {
    cargarTabla({
        _endpoint: "/Vehiculo/ObtenerVehiculos",
        classNameTable: "tablaVehiculo",
        headersTable: ["Id", "Marca", "Modelo", "Anio", "Precio", "Estado", "Accion"],
        keys: ["id", "marca", "modelo", "anio", "precio", "estado"],
        habilitar_acciondes: true,
        selectorById: "contenedor-tabla-vehiculo"
    });
});

d.addEventListener("submit", e => {
    e.preventDefault();
    if (e.target.matches("#formulario-vehiculo")) {
        let data = {
            marca: e.target.marca.value,
            modelo: e.target.modelo.value,
            precio: e.target.precio.value,
            anio: e.target.anio.value,
            id: e.target.id.value || 0
        };

        if (!$hidden.id) {
            insertarDatosFetch({
                _endpoint: "/Vehiculo/InsertarDatos",
                method: "POST",
                obj_data: data
            });
        } else {
            actualizarFetch({
                _endpoint: "/Vehiculo/ActualizarDatos",
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
        $formulario_vehiculo.id.value = $btn_editar.dataset.id;
        $formulario_vehiculo.marca.value = $btn_editar.dataset.marca;
        $formulario_vehiculo.modelo.value = $btn_editar.dataset.modelo;
        $formulario_vehiculo.precio.value = $btn_editar.dataset.precio;
        $formulario_vehiculo.anio.value = $btn_editar.dataset.anio;
        $formulario_vehiculo.estado.value = $btn_editar.dataset.estado;

        $hidden.id = $btn_editar.dataset.id;
        $input_id.style.display = "block";
        $label_id.style.display = "block";
    }

    if ($btn_eliminar) {
        eliminarFetch(`/Vehiculo/EliminarDatos/${$btn_eliminar.dataset.id}`, $btn_eliminar.dataset.modelo);
    }

    if (e.target.matches("#crear_vehiculo_nuevo")) {
        $hidden.id = "";
        $formulario_vehiculo.reset();

        $input_id.style.display = "none";
        $label_id.style.display = "none";
    }
});
