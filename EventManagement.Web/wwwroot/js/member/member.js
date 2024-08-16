$(document).ready(function () {
    loadMembers();

    $('#saveMember').on("click", function () {
        var formData = {
            Name: $('#Name').val()
        };

        if (!formData.Name) {
            showToast('Error', 'Name is required', 'error');
            return;
        }

        $.ajax({
            url: '/Member/Create',
            method: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $('#Name').val('');
                    $('#exampleModal').modal('hide');
                    showSwal('success', response.message);
                    loadMembers();
                    removeModalBackdrop();
                } else {
                    showSwal('error', response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error('AJAX request failed:', status, error);
                showSwal('error', xhr.responseText || "An error occurred");
            }
        });
    });

    $('#memberList').on('click', '.edit-btn', function () {
        var $row = $(this).closest('tr');
        var $memberNameSpan = $row.find('.member-name');
        var $memberNameInput = $row.find('.member-name-input');

        if ($(this).text() === 'Edit') {
            $(this).text('Save');
            toggleEditMode($memberNameSpan, $memberNameInput);
            toggleButtonStates($row, true);
        } else {
            var memberId = $(this).data('id');
            var newName = $memberNameInput.val();

            if (newName === '') {
                showToast('Error', 'Name is required', 'error');
                return;
            }

            $.ajax({
                url: '/Member/Edit',
                method: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ id: memberId, name: newName }),
                success: function (response) {
                    handleEditResponse(response, $row, $memberNameSpan, $memberNameInput, newName);
                },
                error: function (xhr, status, error) {
                    console.error('Error updating member name:', status, error);
                    showSwal('error', xhr.responseText || "An error occurred");
                },
                complete: function () {
                    $(this).text('Edit');
                    loadMembers();
                    toggleButtonStates($row, false);
                }.bind(this) 
            });
        }
    });
    $('#memberList').on('click', '.delete-btn', function () {
        var $row = $(this).closest('tr');
        var memberId = $(this).data('id');

        Swal.fire({
            title: 'Are you sure?',
            text: 'You will not be able to recover this member!',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Member/Delete',
                    method: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify({ id: memberId }),
                    success: function (response) {
                        if (response.success) {
                            showSwal('success', response.message);
                            $row.remove();
                        } else {
                            showSwal('error', response.message);
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error('Error deleting member:', status, error);
                        showSwal('error', xhr.responseText || "An error occurred");
                    }
                });
            }
        });
    });
});


function toggleButtonStates($row, isSaveMode) {
    if (isSaveMode) {
        $('#memberList .edit-btn, #memberList .delete-btn').prop('disabled', true);
        $row.find('.edit-btn, .delete-btn').prop('disabled', false);
    } else {
        $('#memberList .edit-btn, #memberList .delete-btn').prop('disabled', false);
    }
}
function loadMembers() {
    $.ajax({
        url: '/Member/GetAllMembers',
        method: 'GET',
        dataType: 'json',
        success: function (response) {
            populateMemberTable(response);
            initializeDataTable();
        },
        error: function (xhr, status, error) {
            console.error('Error fetching members:', status, error);
            alert('An error occurred while fetching members.');
        }
    });
}

function populateMemberTable(members) {
    var tableBody = $('#memberList tbody');
    tableBody.empty();
    members.forEach(function (member, index) {
        var row = '<tr>' +
            '<td>' + (index + 1) + '</td>' +
            '<td>' +
            '<span class="member-name">' + member.name + '</span>' +
            '<input type="text" class="form-control member-name-input d-none" value="' + member.name + '">' +
            '</td>' +
            '<td>' +
            '<button class="btn btn-primary edit-btn mr-2" data-id="' + member.id + '">Edit</button>' +
            '<button class="btn btn-danger delete-btn" data-id="' + member.id + '">Delete</button>' +
            '</td>' +
            '</tr>';
        tableBody.append(row);
    });
}

function initializeDataTable() {
    if ($.fn.DataTable.isDataTable('#memberList')) {
        $('#memberList').DataTable().destroy();
    }
    $('#memberList').DataTable({
        paging: true,
        searching: false,
        info: true,
    });
}

function showSwal(icon, message) {
    Swal.fire({
        position: "center",
        icon: icon,
        title: icon === 'success' ? message : "Oops...",
        text: icon === 'success' ? '' : message,
        showConfirmButton: false,
        timer: 2000
    });
}

function showToast(heading, text, icon) {
    $.toast({
        heading: heading,
        text: text,
        showHideTransition: 'slide',
        icon: icon,
        position: 'bottom-right'
    });
}

function toggleEditMode($memberNameSpan, $memberNameInput) {
    $memberNameSpan.toggleClass('d-none');
    $memberNameInput.toggleClass('d-none').focus();
}

function removeModalBackdrop() {
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}

function handleEditResponse(response, $row, $memberNameSpan, $memberNameInput, newName) {
    if (response.success) {
        if (response.changesMade) {
            showSwal('success', response.message);
        } else {
            showSwal('info', "No changes were made.");
        }
    } else {
        showSwal('error', response.message);
    }
    $memberNameInput.addClass('d-none');
    $memberNameSpan.removeClass('d-none').text(newName);
}

