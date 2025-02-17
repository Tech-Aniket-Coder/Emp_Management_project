var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": "/Department/GetAll",
            "data": function (d) {
                d.search = d.search.value;
                d.page = (d.start / d.length) + 1;
                d.pageSize = d.length;
            }
        },
        "columns": [
            { "data": "name", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Department/Upsert/${data}" class="btn btn-outline-warning">
                                <i class="fas fa-edit"></i> Edit
                            </a>
                            <a class="btn btn-outline-danger" onclick=Delete("/Department/Delete/${data}")>
                                <i class="fas fa-trash-alt"></i> Delete
                            </a>
                        </div>
                    `;
                }
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Want To Delete This Data?",
        text: "You will not be able to recover this data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function () {
                    toastr.error("An error occurred while deleting the department.");
                }
            });
        }
    });
}
$('#employeeForm').on('submit', function (e) {
    e.preventDefault();
    var name = $('#Name').val().trim();
    if (name === "") {
        toastr.error("Department name is required! Please Fill The Name Input");
        return;
    }

    var formData = $(this).serialize();

    $.ajax({
        url: '/Department/Upsert',
        type: 'POST',
        data: formData,
        success: function (data) {
            if (data.success) {
                swal({
                    title: "Success!",
                    text: data.message,
                    icon: "success",
                    buttons: false,
                    timer: 1000
                }).then(() => {
                    window.location.href = '/Department/Index';
                });
            } else {
                toastr.error(data.message);
            }
        },
        error: function (xhr, status, error) {
            toastr.error("An error occurred while saving the department.");
        }
    });
});
