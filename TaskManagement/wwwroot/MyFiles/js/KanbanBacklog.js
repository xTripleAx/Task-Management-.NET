function createIssue() {
    // Fetch form values
    var IssueName = $('#IssueName').val();
    var IssueDescription = $('#IssueDescription').val();
    var IssueAssignee = $('#IssueAssignee').val();
    var IssueList = $('#IssueList').val();
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: '/Issue/Create',
        data: {
            IssueName: IssueName,
            IssueDescription: IssueDescription,
            AssigneeId: IssueAssignee,
            ListId: IssueList,
            __RequestVerificationToken: token,
            projectid: projectid
        },
        success: function (result) {
            // Handle success (e.g., close the modal)
            if (result.success) {
                $('#createListModal').modal('hide');
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
            } else {
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
            console.error(error);
        }
    });
}


function openIssueEditModal(IssueId, IssueName, IssueDesc, IssueAssignee, IssueList) {

    // Set the modal fields with the selected Issue data
    $('#EditIssueName').val(IssueName);
    $('#EditIssueDescription').val(IssueDesc);
    $('#EditIssueAssignee').val(IssueAssignee);
    $('#EditIssueList').val(IssueList);
    $('#editIssueId').val(IssueId);

    // Show the Issue Edit modal
    $('#EditIssueModal').modal('show');
}


function editIssue() {
    var issueId = $('#editIssueId').val();
    var issuename = $('#EditIssueName').val();
    var issueDesc = $('#EditIssueDescription').val();
    var issueAssignee = $('#EditIssueAssignee').val();
    var issueList = $('#EditIssueList').val();
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: '/Issue/Edit',
        data: {
            IssueId: issueId,
            IssueName: issuename,
            IssueDescription: issueDesc,
            AssigneeId: issueAssignee,
            ListId: issueList,
            __RequestVerificationToken: token,
            projectid: projectid
        },
        success: function (result) {
            // Handle success (e.g., close the modal)
            if (result.success) {
                $('#EditIssueModal').modal('hide');
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
            } else {
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
            console.error(error);
        }
    });
}


function confirmIssueDelete(IssueId) {
    Swal.fire({
        title: 'Are you sure?',
        text: 'You will not be able to recover this issue!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, keep it'
    }).then((result) => {
        if (result.isConfirmed) {
            // User clicked 'Yes,' proceed with delete
            deleteIssue(IssueId);
        } else {
            // User clicked 'No,' do nothing
        }
    });
}

function deleteIssue(issueid) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    // Use AJAX to send data to your controller
    $.ajax({
        type: 'POST',
        url: '/Issue/Delete',
        data: {
            IssueId: issueid,
            projectid: projectid,
            __RequestVerificationToken: token
        },

        success: function (result) {
            if (result.success) {
                // Handle success
                Swal.fire({
                    icon: 'success',
                    title: 'Issue Deleted Successfully',
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
            console.error(error);
        }
    });
}