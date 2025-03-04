import { TableHeader } from "../components/TableHeader.js"
import {TableBody } from "../components/TableBody.js"
export function Table(className, headers, response, keys, habilitar_acciondes) {
    return `
        <table class="${className} table display" id="example" style="width:100%">
            ${TableHeader(headers)}
            ${TableBody(response, keys, habilitar_acciondes)}
        </table>
    `;
}

export function IncialiazarDataTable() {
    setTimeout(() => {
        new DataTable("#example", {
            language: {
                lengthMenu: "Mostrar _MENU_ registros por página",
                zeroRecords: "No se encontraron resultados",
                info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
                infoEmpty: "No hay registros disponibles",
                infoFiltered: "(filtrado de _MAX_ registros en total)",
                search: "Buscar:",
                paginate: {
                    first: "Primero",
                    last: "Último",
                    next: "Siguiente",
                    previous: "Anterior"
                }
            },
            paging: true,      // Activar paginación
            searching: true,   // Activar la barra de búsqueda
            ordering: true,    // Permitir ordenar columnas
            responsive: true,  // Hacer que la tabla sea responsiva
            pageLength: 10,     // Número de registros por página
            lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]] // Opciones de registros por página
        });
    }, 100);
}