$(document).ready(function () {
    loadMembers();
    $('#saveFestival').on("click", function () {
        var formData = {
            Name: $('#Name').val()
        };

        if (!formData.Name) {
            $.toast({
                heading: 'Error',
                text: 'Name is required',
                showHideTransition: 'slide',
                icon: 'error',
                position: 'bottom-right'
            });
            return;
        }

        $.ajax({
            url: '/Festival/Create',
            method: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $('#Name').val('');
                    $('#exampleModal').modal('hide');
                    Swal.fire({
                        position: "center",
                        icon: "success",
                        title: response.message,
                        showConfirmButton: false,
                        timer: 3000
                    });
                    loadMembers();
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                } else {
                    Swal.fire({
                        position: "center",
                        icon: "error",
                        title: "Oops...",
                        text: response.message,
                        timer: 2000
                    });
                }
            },
            error: function (xhr, status, error) {
                console.error('AJAX request failed:', status, error);
                Swal.fire({
                    position: "center",
                    icon: "error",
                    title: "Oops...",
                    text: xhr.responseText || "An error occurred",
                    timer: 2000
                });
            }
        });
    });

});
function loadMembers() {
    $.ajax({
        url: '/Festival/GetAllFestivals',
        method: 'GET',
        dataType: 'json',
        success: function (response) {
            var tableBody = $('#festivalList tbody');
            tableBody.empty();
            response.forEach(function (festival, index) {
                debugger
                var row = '<tr>' +
                    '<td>' + (index + 1) + '</td>' +
                    '<td>' +
                    '<span class="festival-name">' + festival.name + '</span>' +
                    '<input type="text" class="form-control festival-name-input d-none" value="' + festival.name + '">' +
                    '</td>' +
                    '<td>' +
                    '<button class="btn btn-primary edit-btn mr-2" data-id="' + festival.id + '">Edit</button>' +
                    '<button class="btn btn-danger delete-btn" data-id="' + festival.id + '">Delete</button>' +
                    '</td>' +
                    '</tr>';
                tableBody.append(row);
            });

            if ($.fn.DataTable.isDataTable('#festivalList')) {
                $('#festivalList').DataTable().destroy();
            }

            $('#festivalList').DataTable({
                paging: true,
                searching: false,
                info: true,
            });
        },
        error: function (xhr, status, error) {
            console.error('Error fetching members:', status, error);
            alert('An error occurred while fetching members.');
        }
    });
}


