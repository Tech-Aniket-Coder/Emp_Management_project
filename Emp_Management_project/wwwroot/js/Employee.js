var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": "/Employee/GetAll",
            "data": function (d) {
                d.search = d.search.value;
                d.page = (d.start / d.length) + 1;
                d.pageSize = d.length;
            }
        },
        "columns": [
            { "data": "firstName"},
            { "data": "address"},
            { "data": "gender" },
            { "data": "email" },
            { "data": "contact" },
            { "data": "departmentName" },
            { "data": "teamName" },
            { "data": "positionName"},
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="row" style="display:flex">
                        <div class="text-center">
                            <a href="/Employee/Upsert/${data}" class="btn btn-warning">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a class="btn btn-danger" onclick=Delete("/Employee/Delete/${data}")>
                                <i class="fas fa-trash-alt"></i>
                            </a>
                          <a href="/Salary/Upsert?employeeId=${data}" class="btn btn-success">
                             <i class="fas fa-plus"></i> Salary
                            </a>
                        </div>
</div>
                    `; 
                }
            }
        ]
    });
}


    // Disable Team and Position dropdowns initially
    $('#TeamId').prop('disabled', true);
    $('#PositionId').prop('disabled', true);

    // Event listener for department change
    $('#DepartmentId').change(function () {
        var departmentId = $(this).val();
        if (departmentId) {
            $.ajax({
                url: '/Employee/GetTeamsByDepartmentId',
                type: 'GET',
                data: { departmentId: departmentId },
                success: function (data) {
                    $('#TeamId').empty();
                    $('#TeamId').append('<option value="">Select Team</option>');
                    $.each(data, function (index, item) {
                        $('#TeamId').append('<option value="' + item.value + '">' + item.text + '</option>');
                    });
                    $('#TeamId').prop('disabled', false); // Enable Team dropdown
                },
                error: function () {
                    toastr.error("An error occurred while loading teams.");
                }
            });
        } else {
            $('#TeamId').empty();
            $('#TeamId').append('<option value="">Select Team</option>');
            $('#TeamId').prop('disabled', true); // Disable Team dropdown
            $('#PositionId').empty();
            $('#PositionId').append('<option value="">Select Position</option>');
            $('#PositionId').prop('disabled', true); // Disable Position dropdown
        }
    });

    // Event listener for team change
    $('#TeamId').change(function () {
        var teamId = $(this).val();
        if (teamId) {
            $.ajax({
                url: '/Employee/GetPositionByTeamId',
                type: 'GET',
                data: { teamId: teamId },
                success: function (data) {
                    $('#PositionId').empty();
                    $('#PositionId').append('<option value="">Select Position</option>');
                    $.each(data, function (index, item) {
                        $('#PositionId').append('<option value="' + item.value + '">' + item.text + '</option>');
                    });
                    $('#PositionId').prop('disabled', false); // Enable Position dropdown
                },
                error: function () {
                    toastr.error("An error occurred while loading positions.");
                }
            });
        } else {
            $('#PositionId').empty();
            $('#PositionId').append('<option value="">Select Position</option>');
            $('#PositionId').prop('disabled', true); // Disable Position dropdown
        }
    });


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
    
    var name = $('#FirstName').val().trim();
    var email = $('#Email').val().trim();
    var contact = $('#Contact').val().trim();
    var address = $('#Address').val().trim();
    var department = $('#DepartmentId').val();
    var team = $('#TeamId').val();
    var position = $('#PositionId').val();

    // Basic field validation
    if (name === "") {
        toastr.error("Employee name is required! Please Fill The Name Input");
        return;
    }
    if (email === "") {
        toastr.error("Email is required! Please provide an email address.");
        return;
    }
    if (!validateEmail(email)) {
        toastr.error("Please provide a valid email address.");
        return;
    }
    if (contact === "") {
        toastr.error("Contact number is required!");
        return;
    }
    if (!/^\d{10}$/.test(contact)) {  // Ensure it's a 10-digit number (modify this to fit your requirements)
        toastr.error("Contact number must be a valid 10-digit number.");
        return;
    }
    if (address === "") {
        toastr.error("Address is required!");
        return;
    }
    if (!department) {
        toastr.error("Please select a department.");
        return;
    }
    if (!team) {
        toastr.error("Please select a team.");
        return;
    }
    if (!position) {
        toastr.error("Please select a position.");
        return;
    }

    var formData = $(this).serialize();

    $.ajax({
        url: '/Employee/Upsert',
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
                    window.location.href = '/Employee/Index';
                });
            } else {
                toastr.error(data.message);
            }
        },
        error: function (xhr, status, error) {
            toastr.error("An error occurred while saving the employee.");
        }
    });
});

function validateEmail(email) {
    var re = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    return re.test(email);
}