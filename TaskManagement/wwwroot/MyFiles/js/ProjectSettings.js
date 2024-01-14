function handleDelete() {
    var projectid = document.getElementById('IdDelete').value;

    Swal.fire({
        icon: 'warning',
        title: 'Delete Project!',
        text: 'Do you really want to delete the Project? All Data Will Be Lost!',
        showCancelButton: true,
        confirmButtonText: 'Yes, Delete!',
        cancelButtonText: 'No, Don\'t Delete'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteProject(projectid);
        } else {
        }
    });
}

function DeleteProject(projectid) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    // Use AJAX to send data to your controller
    $.ajax({
        type: 'DELETE',
        url: '/Project/DeleteProjectById/' + projectid,
        data: {
            __RequestVerificationToken: token
        },
        success: function (result) {
            if (result.success) {
                // Handle success (e.g., refresh the page or update the lists dynamically)
                Swal.fire({
                    icon: 'success',
                    title: result.message,
                    showConfirmButton: false,
                    timer: 1500
                });
                // Use setTimeout to delay the redirection
                setTimeout(function () {
                    window.location.href = '/Project/All';
                }, 1500);
            }
            else {
                // Handle failure
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: result.message,
                });
            }
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!',
            });
            console.log(error.message);
        }
    });
}