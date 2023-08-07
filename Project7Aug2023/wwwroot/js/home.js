
$(document).ready(function () {
    table();
});

function table() {
    $.ajax({
        type: 'GET',
        url: "Employees/Employee",
        success: function (response) {
            var html = '';
            for (var i = 0; i < response.length; i++) {
                var data = response[i];
                html += `<tr>
                            <td>${i + 1}</td>
                            <td>${data.Name}</td>
                            <td>${data.Department}</td>
                            <td><button type="button" onclick="getEmployeeById(${data.Id})"><i class="fa-solid fa-pencil" style="color:blue;"></i> Edit </button>
                                <button type="button" onclick="deleteConfirm(${data.Id})"> <i class="fa-solid fa-trash" style="color:red;"></i> Delete</td></button>
                          </tr>`;
            }
            
            $("#getTable").html(html);

        },
        error: function () {
            alert("Data not found")
        }

    });
}


function save() {
    var data = {
        Id: $("#id").val() ?? 0,
        Name: $("#name").val(),
        Department: $("#department").val()
    };
    var err = "";
    $("#errName").html("");
    $("#errDepartment").html("");

    var val = validation(data);
    if (val == "Name") {
        err = "Name cannot be empty";
        $("#errName").html(err);   
        return;
    }
    if (val == "Department") {
        err = "Department cannot be empty"
        $("#errDepartment").html(err);
        return;
    }
    $.ajax({
        type: 'POST',
        url: "Employees/AddOrUpdateEmployee",
        data: data,
        success: function (respon) {
            alert("Sukses");
            location.reload();
        },
        error: function () {
            alert("Error")
        }

    });
}

function validation(data)
{
    if (data.Name == null || data.Name == "") return "Name";
    if (data.Department == null || data.Department == "") return "Department";
}

function deleteEmployee() {
    var id = $("#idDelete").val();
    $.ajax({
        type: 'POST',
        url: "Employees/DeleteEmployee?id=" + id,
        success: function (response) {
            alert("Delete Success");
            location.reload();

        },
        error: function () {
            alert("Data not found")
        }

    });
}

function getEmployeeById(id)
{
    $.ajax({
        type: 'GET',
        url: "Employees/GetEmployeeById?id=" + id,
        success: function (respon) {
            $("#id").val(respon.Id);
            $("#name").val(respon.Name);
            $("#department").val(respon.Department);
            $("#formEmployee").modal("show");

        },
        error: function () {
            alert("Data not found")
        }

    });
}

function showModal()
{
    $("#name").val("");
    $("#department").val("");
    $("#formEmployee").modal("show");
}

function deleteConfirm(id)
{
    $("#idDelete").val(id);
    $("#formDelete").modal("show");
}