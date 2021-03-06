﻿//Load Data in Table when documents is ready  
$(document).ready(function () {
    $('#datatable').DataTable({
        "ajax": loadData()
    });
    loadData();
});

//Load Data function  
function loadData() {
    debugger;
    $.ajax({
        url: "/Items/List",
        type: "GET",
        async:false,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ID + '</td>';
                html += '<td>' + item.NAME + '</td>';
                html += '<td>' + item.PRICE + '</td>';
                html += '<td>' + item.STOCK + '</td>';
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
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        ID: $('#ID').val(),
        Name: $('#Name').val(),
        Price: $('#Price').val(),
        Stock: $('#Stock').val()
    };
    $.ajax({
        url: "/Items/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
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
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        ID: $('#ID').val(),
        Name: $('#Name').val(),
        Price: $('#Price').val(),
        Stock: $('#Stock').val(),
    };
    $.ajax({
        url: "/Items/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ID').val("");
            $('#Name').val("");
            $('#Price').val("");
            $('#Stock').val("");
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
            url: "/Items/Delete/" + ID,
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
function getbyID(itemID) {
    $('#Name').css('border-color', 'lightgrey');
    $('#Price').css('border-color', 'lightgrey');
    $('#Stock').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Items/getbyID/" + itemID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#ID').val(result.ID);
            $('#Name').val(result.NAME);
            $('#Price').val(result.PRICE);
            $('#Stock').val(result.STOCK);

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
    $('#Name').val("");
    $('#Price').val("");
    $('#Stock').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Price').css('border-color', 'lightgrey');
    $('#Stock').css('border-color', 'lightgrey');
}
//Valdidation using jquery  
function validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#Price').val().trim() == "") {
        $('#Price').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Price').css('border-color', 'lightgrey');
    }
    if ($('#Stock').val().trim() == "") {
        $('#Stock').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Stock').css('border-color', 'lightgrey');
    }
    
    return isValid;
}