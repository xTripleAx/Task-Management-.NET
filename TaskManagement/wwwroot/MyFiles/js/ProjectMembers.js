function confirmAddMember(memberUsername) {
    Swal.fire({
        title: 'Add Member?',
        text: `Add ${memberUsername} to this project ?`,
        icon: 'info',
        showCancelButton: true,
        confirmButtonText: 'Yes, Add!',
        cancelButtonText: 'No, Don\'t Add'
    }).then((result) => {
        if (result.isConfirmed) {
            AddMember(memberUsername);
        } else {
        }
    });
}

function handleAddMember() {
    var memberUsername = document.getElementById('NewMemberUsername').value;

    if (memberUsername.trim() === "") {
        Swal.fire({
            icon: 'warning',
            title: 'Oops...',
            text: 'Please enter a member username before adding!',
        });
        return;
    }

    confirmAddMember(memberUsername);
}

function AddMember(memberUsername) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    // Use AJAX to send data to your controller
    $.ajax({
        type: 'POST',
        url: '/Project/AddMember',
        data: {
            Username: memberUsername,
            projectid: projectid,
            __RequestVerificationToken: token
        },
        success: function (result) {
            if (result.success) {
                // Handle success (e.g., refresh the page or update the lists dynamically)
                Swal.fire({
                    icon: 'success',
                    title: `${memberUsername} Added Successfully!`,
                    showConfirmButton: false,
                    timer: 1500
                });
                // Use setTimeout to delay the redirection
                setTimeout(function () {
                    if (result.redirectUrl) {
                        window.location.href = result.redirectUrl;
                    }
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
        }
    });
}






function handleRemoveMember(){
    var memberid = document.getElementById('memberId').value;

    if (memberid == null) {
        Swal.fire({
            icon: 'warning',
            title: 'Oops...',
            text: 'No Valid User To Remove!',
        });
        return;
    }

    confirmRemoveMember(memberid);
}


function confirmRemoveMember(memberid){
    Swal.fire({
        title: 'Remove Member?',
        text: `Remove User from this project ?`,
        icon: 'info',
        showCancelButton: true,
        confirmButtonText: 'Yes, Remove!',
        cancelButtonText: 'No, Don\'t Remove'
    }).then((result) => {
        if (result.isConfirmed) {
            RemoveMember(memberid);
        } else {
        }
    });
}

function RemoveMember(memberid) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    // Use AJAX to send data to your controller
    $.ajax({
        type: 'POST',
        url: '/Project/RemoveMember',
        data: {
            memberid: memberid,
            projectid: projectid,
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
                    if (result.redirectUrl) {
                        window.location.href = result.redirectUrl;
                    }
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
        }
    });
}