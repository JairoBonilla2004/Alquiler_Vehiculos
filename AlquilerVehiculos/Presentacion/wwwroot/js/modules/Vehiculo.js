import {actualizarFetch, cargarTabla, eliminarFetch, insertarDatosFetch } from "../App.js"
let d = document;

const $hidden = d.querySelector(".btn-hidden");
const $formulario_vehiculo = d.getElementById("formulario-vehiculo");


d.addEventListener("DOMContentLoaded", e => {
    cargarTabla({
        _endpoint:"/Vehiculo/ObtenerVehiculos",
        classNameTable: "tablaVehiculo",
        headersTable: ["Id", "Marca", "Modelo", "Anio", "Precio", "Estado", "Accion"],
        keys: ["id", "marca", "modelo", "anio", "precio", "estado"],
        habilitar_acciondes: true,
        selectorById: "contenedor-tabla-medicamento"
    })
});

d.addEventListener("submit", e => {
    e.preventDefault();
    if (e.target.matches("#formulario-vehiculo")) {
        let marca = e.target.marca.value;
        let modelo = e.target.modelo.value;
        let precio = e.target.precio.value;

        let anio = e.target.anio.value;
        let id = e.target.id.value;
        let data = {
            marca,
            modelo,
            precio,
            anio,
            id
        }
        if (!$hidden.id) {
            data.id = 0;
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
    const $input_estado = d.getElementById("estado");
    const $label_id = $input_id.previousElementSibling;
    const $label_estado = $input_estado.previousElementSibling;
    if ($btn_editar) {
        console.log("tru", $hidden);
        $formulario_vehiculo.id.value = $btn_editar.dataset.id;
        $formulario_vehiculo.marca.value = $btn_editar.dataset.marca;
        $formulario_vehiculo.modelo.value = $btn_editar.dataset.modelo;
        $formulario_vehiculo.precio.value = $btn_editar.dataset.precio;
        $formulario_vehiculo.anio.value = $btn_editar.dataset.anio;
        $formulario_vehiculo.estado.value = $btn_editar.dataset.estado;
        $hidden.id = $btn_editar.dataset.id;
        $input_id.style.display = "block";
        $label_id.style.display = "block";
        $input_estado.style.display = "block"
        $label_estado.style.display = "block";
    }

    if ($btn_eliminar) {
        console.log($btn_eliminar.id);
        eliminarFetch(`/Vehiculo/EliminarDatos/${$btn_eliminar.dataset.id}`, $btn_eliminar.dataset.modelo);
    }

    if (e.target.matches("#crear_vehiculo_nuevo")) {
        const $input_id = d.getElementById("id");
        const $label_id = $input_id.previousElementSibling;
        $hidden.id = "";
        $formulario_vehiculo.id.value = "";
        $formulario_vehiculo.marca.value = "";
        $formulario_vehiculo.modelo.value = "";
        $formulario_vehiculo.precio.value = "";
        $formulario_vehiculo.anio.value = "";
        $input_id.style.display = "none";
        $label_id.style.display = "none";
        $input_estado.style.display = "none";
        $label_estado.style.display = "none";
    }
});
