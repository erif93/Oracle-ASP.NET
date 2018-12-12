//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    $.ajax({
        url: "/Accounts/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.USERNAME + '</td>';
                html += '<td>' + item.PASSWORD + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.ID + ')">Edit</a> | <a href="#" onclick="Delete(' + item.ID + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Add() {
    var empObj = {
        Username: $('#Username').val(),
        Password: $('#Password').val()
    };
    $.ajax({
        url: "/Accounts/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#ID').val("");
            $('#Username').val("");
            $('#Password').val("");
            $('#myModal').modal('hide');
            loadData();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for updating record  
function Update() {
    var empObj = {
        ID: $('#ID').val(),
        Username: $('#Username').val(),
        Password: $('#Password').val()
    };
    $.ajax({
        url: "/Accounts/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ID').val("");
            $('#Username').val("");
            $('#Password').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting record  
function Delete(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Accounts/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for getting the Data Based upon ID  
function getbyID(accountID) {
    $('#Username').css('border-color', 'lightgrey');
    $('#Password').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Accounts/getbyID/" + accountID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#ID').val(result.ID);
            $('#Username').val(result.USERNAME);
            $('#Password').val(result.PASSWORD);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();    
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
//Function for clearing the textboxes  
function clearTextBox() {
    $('#ID').val("");
    $('#Username').val("");
    $('#Password').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Username').css('border-color', 'lightgrey');
    $('#Password').css('border-color', 'lightgrey');
}