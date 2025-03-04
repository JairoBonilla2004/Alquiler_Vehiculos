
export function mostrarAlertaAjax(tipo, titulo, texto, confirmText = "Aceptar", cancelText = "Cancelar", ajaxCallback = null) {
    Swal.fire({
        title: titulo,
        text: texto,
        icon: tipo, // "success", "error", "warning", "info", etc.
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: confirmText,
        cancelButtonText: cancelText
    }).then((result) => {
        if (result.isConfirmed) {

            Swal.fire({
                title: "¡Éxito!",
                text: "La acción ha sido completada con éxito.",
                icon: "success",
                confirmButtonText: "OK"
            }).then((result) => {
                if (result.isConfirmed) {
                    if (ajaxCallback && typeof ajaxCallback === "function") {
                        ajaxCallback();
                    }
                }
            });


        }
    });
}
