export function FormularioRestablecerEnviar(response) {
    const baseUrl = window.location.origin;
    let html = "";
    let colorTexto = "";

    html = `<div class="text-center py-4">`;

    if (response.success) {
        colorTexto = "--success-color";
        html += `
            <div class="mb-4" style="color: var(${colorTexto}); font-size: 3rem;">
                <i class="fas fa-check-circle"></i>
            </div>
        `;
    } else {
        colorTexto = "--error-color";
        html += `
            <div class="mb-4" style="color: var(${colorTexto}); font-size: 3rem;">
                <i class="fa-solid fa-circle-exclamation"></i>
            </div>
        `;
    }

    html += `
        <h3 class="mb-3" style="color: var(${colorTexto});">${response.title}</h3>
        <p class="mb-4">${response.message}</p>             
        <a class="btn btn-secondary" href="${baseUrl}">
            <i class="fas fa-arrow-left me-2"></i>Volver al login
        </a>
    </div>`;

    return html;
}
