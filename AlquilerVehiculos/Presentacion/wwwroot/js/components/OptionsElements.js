export function OptionsElements(response, keyNombre) {
    let optionHTML = "";
    response.forEach(item => {
        optionHTML += `<option value="${item[keyNombre]}">${item[keyNombre]}</option>`;
    });
    return optionHTML;
}
