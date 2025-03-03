export function showSuccessMessage(response) {
    Swal.fire(response.message).then((result) => {
        if (result.isConfirmed) {
            location.reload(); 
        }
    });
}