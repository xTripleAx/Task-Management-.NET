function createList() {
    // Fetch form values
    var listName = $('#listName').val();
    var columnLimit = $('#columnLimit').val();
    var isListForFinish = $('#isListForFinish').prop('checked');
    var boardId = $('#boardId').val();
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: '/List/Create',
        data: {
            Name: listName,
            ColumnLimit: columnLimit,
            isListForFinish: isListForFinish,
            BoardId: boardId,
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


//Editing the List
function openEditListModal(listName, listId, columnLimit, isListForFinish) {
    // Set the modal fields with the selected list data
    $('#editListName').val(listName);
    $('#editColumnLimit').val(columnLimit);
    $('#editIsListForFinish').prop('checked', isListForFinish);
    $('#editListId').val(listId);
    // Show the edit list modal
    $('#editListModal').modal('show');
}


function editList() {
    // Fetch form values
    var listId = $('#editListId').val();
    var name = $('#editListName').val();
    var columnLimit = $('#editColumnLimit').val();
    var isListForFinish = $('#editIsListForFinish').prop('checked');
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: 'POST',
        url: '/List/Edit',
        data: {
            listId: listId,
            Name: name,
            ColumnLimit: columnLimit,
            isListForFinish: isListForFinish,
            __RequestVerificationToken: token,
            projectid: projectid
        },
        success: function (result) {
            if (result.success) {
                // Handle success (e.g., close the modal)
                $('#editListModal').modal('hide');
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
                // Handle failure
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: result.message,
                });
            }
        },
        error: function (error) {
            // Handle errors
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!',
            });
            console.error(error);
        }
    });
}


function confirmDelete(listName, listId) {
    Swal.fire({
        title: 'Are you sure?',
        text: 'You will not be able to recover this list!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, keep it'
    }).then((result) => {
        if (result.isConfirmed) {
            // User clicked 'Yes,' proceed with delete
            deleteList(listId);
        } else {
            // User clicked 'No,' do nothing
        }
    });
}

function deleteList(listId) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    // Use AJAX to send data to your controller
    $.ajax({
        type: 'POST',
        url: '/List/Delete', // Replace with your actual action method URL
        data: {
            listId: listId,
            projectid: projectid,
            __RequestVerificationToken: token
        },
        success: function (result) {
            // Handle success (e.g., refresh the page or update the lists dynamically)
            Swal.fire({
                icon: 'success',
                title: 'List Deleted Successfully',
                showConfirmButton: false,
                timer: 1500
            });
            // Use setTimeout to delay the redirection
            setTimeout(function () {
                if (result.redirectUrl) {
                    window.location.href = result.redirectUrl;
                }
            }, 1500); // Adjust the timeout value as needed
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


//Issue Details Model
function openIssueDetailsModal(IssueId, IssueName, IssueDesc, IssueAssignee, IssueList, IssueDate) {

    // Set the modal fields with the selected list data
    $('#DetailsIssueName').text(IssueName);
    $('#DetailsIssueDescription').text(IssueDesc);
    $('#DetailsIssueAssignee').text(IssueAssignee);
    $('#DetailsIssueList').text(IssueList);

    var dateObject = new Date(IssueDate);
    var formattedDate = dateObject.toISOString().split('T')[0];
    $('#DetailsIssueDate').text(formattedDate);

    // Show the Issue Details modal
    $('#displayIssueDetailsModal').modal('show');
}