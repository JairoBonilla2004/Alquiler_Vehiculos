export function TableHeader(elements) {
    console.log("elements", elements);
    let html = "";
    elements.forEach(th => {
        console.log(th);
        html += `<th scope = "col" >${th}</th>`
    });
    return `
    <thead>
        <tr>
            ${html}        
        </tr>
     </thead>`;
}