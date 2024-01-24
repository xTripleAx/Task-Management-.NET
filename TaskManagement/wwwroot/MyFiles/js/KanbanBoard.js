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


function confirmListDelete(listId) {
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
                Swal.fire({
                    icon: 'error',
                    title: 'Oops',
                    text: result.message
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



//Issue Details Model
function openIssueDetailsModal(IssueName, IssueDesc, IssueAssignee, IssueList, IssueDate) {

    // Set the modal fields with the selected Issue data
    $('#DetailsIssueName').text(IssueName);
    $('#DetailsIssueDescription').text(IssueDesc);
    $('#DetailsIssueAssignee').text(IssueAssignee);
    $('#DetailsIssueList').text(IssueList);

    console.log(IssueDate);
    var reformattedDate = IssueDate.replace(/(\d{1,2})\/(\d{1,2})\/(\d{4})/, '$3-$2-$1');
    console.log(reformattedDate)
    var dateObject = new Date(Date.parse(reformattedDate));
    console.log(dateObject);
    var formattedDate = dateObject.toISOString().split('T')[0];
    console.log(formattedDate);
    $('#DetailsIssueDate').text(formattedDate);

    // Show the Issue Details modal
    $('#displayIssueDetailsModal').modal('show');
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