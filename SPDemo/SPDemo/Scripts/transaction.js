//Load Data in Table when documents is ready  
var Items = []
//fetch categories from database
function LoadCategory(element) {
    if (Items.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: '/Transactions/GetItem',
            success: function (data) {
                Items = data;
                //render catagory
                renderCategory(element);
                
            }
        })
    }
    else {
        //render catagory to the element
        renderCategory(element);
    }
}

function renderCategory(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(Items, function (i, val) {
        $ele.append($('<option/>').val(val.ID).text(val.NAME));
    })
}

$(document).ready(function () {
    //Add button click event
    $('#add').click(function () {
        //validation and add order items
        var isAllValid = true;
        if ($('#Item').val() == "0") {
            isAllValid = false;
            $('#Item').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Item').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#Qty').val().trim() != '' && (parseInt($('#Qty').val()) || 0))) {
            isAllValid = false;
            $('#Qty').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Qty').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#Price').val().trim() != '' && (parseInt($('#Price').val()) || 0))) {
            isAllValid = false;
            $('#Price').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#Price').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAttr('id');
            $('.pc', $newRow).val($('#Item').val());
            
            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#Item,#Qty,#Price,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#orderdetailsItems').append($newRow);

            //clear select data
            $('#Item').val('0');
            $('#Qty,#Price').val('');
            $('#orderItemError').empty();
        }

    })

    //remove button click event
    $('#orderdetailsItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    $('#submit').click(function () {
        var isAllValid = true;

        //validate order items
        $('#orderItemError').text('');
        var list = [];
        var transac;
        var errorItemCount = 0;
        $('#orderdetailsItems tbody tr').each(function (index, ele) {
            if (
                $('select.pc', this).val() == "0" ||
                (parseInt($('.quantity', this).val()) || 0) == 0 ||
                $('.rate', this).val() == "" ||
                isNaN($('.rate', this).val())
                ) {
                errorItemCount++;
                $(this).addClass('error');
            } else {
                transac = {
                    ID_ITEM: parseInt($('select.pc', this).val()),
                    QTY: parseInt($('.quantity', this).val()),
                    PRICE: parseInt($('.rate', this).val()),
                }
                console.log(transac);
                list.push(transac);
            }
        })

        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + " invalid entry in order item list.");
            isAllValid = false;
        }

        //if (list.length == 0) {
        //    $('#orderItemError').text('At least 1 order item required.');
        //    isAllValid = false;
        //}

        if (isAllValid) {
            //var dataku = {
            //    transac: list
            //}

            $(this).val('Please wait...');

            $.ajax({
                type: 'POST',
                url: '/Transactions/Add',
                data: JSON.stringify(list),
                contentType: 'application/json;charset=utf-8',
                dataType: "json",
                success: function (data) {
                    if (data.status) {
                        alert('Successfully saved');
                        //here we will clear the form
                        list = [];
                        $('#orderdetailsItems').empty();
                    }
                    else {
                        alert('Error');
                    }
                    $('#submit').val('Save');
                },
                error: function (error) {
                    console.log(error);
                    $('#submit').val('Save');
                }
            });
        }

    });

    
});
LoadCategory($('#Item'));
////Load Data function  
//function loadData() {
//    $.ajax({
//        url: "/Items/List",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            var html = '';
//            $.each(result, function (key, item) {
//                html += '<tr>';
//                html += '<td>' + item.ID + '</td>';
//                html += '<td>' + item.NAME + '</td>';
//                html += '<td>' + item.PRICE + '</td>';
//                html += '<td>' + item.STOCK + '</td>';
//                html += '<td><a href="#" onclick="return getbyID(' + item.ID + ')">Edit</a> | <a href="#" onclick="Delete(' + item.ID + ')">Delete</a></td>';
//                html += '</tr>';
//            });
//            $('.tbody').html(html);
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}

//function getItem() {
//    $.ajax({
//        url: '/Transactions/getItem/',
//        dataType: 'json',
//        type: 'GET',
//        success: function (result) {
//            var Item = $("#Item");
//            $.each(result, function (i, item) {
//                $("<option></option>").val(item.ID).text(item.NAME).appendTo(Item);
//            });
//        }

//    });
//}

//function Add() {
//    var e = document.getElementById("Item");
//    var dspText = e.options[e.selectedIndex].text;
//    var res = validate();
//    if (res == false) {
//        return false;
//    }
//    var empObj = {
//        ID_ITEM: $('#Item').val(),
//        TOTAL: $('#Total').val(),
//        NAME: dspText,
//        PRICE: $('#Price').val(),
//        QTY: $('#Qty').val()
//    };
//    $.ajax({
//        url: "/Transactions/Add",
//        data: JSON.stringify(empObj),
//        type: "POST",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            $('#myModal').modal('hide');
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}

//function for updating record  
//function Update() {
//    var res = validate();
//    if (res == false) {
//        return false;
//    }
//    var empObj = {
//        ID: $('#ID').val(),
//        Name: $('#Name').val(),
//        Price: $('#Price').val(),
//        Stock: $('#Stock').val(),
//    };
//    $.ajax({
//        url: "/Items/Update",
//        data: JSON.stringify(empObj),
//        type: "POST",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            loadData();
//            $('#myModal').modal('hide');
//            $('#ID').val("");
//            $('#Name').val("");
//            $('#Price').val("");
//            $('#Stock').val("");
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}

////function for deleting record  
//function Delete(ID) {
//    var ans = confirm("Are you sure you want to delete this Record?");
//    if (ans) {
//        $.ajax({
//            url: "/Items/Delete/" + ID,
//            type: "POST",
//            contentType: "application/json;charset=UTF-8",
//            dataType: "json",
//            success: function (result) {
//                loadData();
//            },
//            error: function (errormessage) {
//                alert(errormessage.responseText);
//            }
//        });
//    }
//}

////Function for getting the Data Based upon ID  
//function getbyID(itemID) {
//    $('#Name').css('border-color', 'lightgrey');
//    $('#Price').css('border-color', 'lightgrey');
//    $('#Stock').css('border-color', 'lightgrey');
//    $.ajax({
//        url: "/Items/getbyID/" + itemID,
//        typr: "GET",
//        contentType: "application/json;charset=UTF-8",
//        dataType: "json",
//        success: function (result) {
//            $('#ID').val(result.ID);
//            $('#Name').val(result.NAME);
//            $('#Price').val(result.PRICE);
//            $('#Stock').val(result.STOCK);

//            $('#myModal').modal('show');
//            $('#btnUpdate').show();
//            $('#btnAdd').hide();
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//    return false;
//}

//Function for clearing the textboxes  
//function clearTextBox() {
//    $('#Qty').val("");
//    $('#Price').val("");
//    $('#Total').val("");
//    $('#btnUpdate').hide();
//    $('#btnAdd').show();
//    $('#Qty').css('border-color', 'lightgrey');
//    $('#Price').css('border-color', 'lightgrey');
//    $('#Total').css('border-color', 'lightgrey');
//}
//Valdidation using jquery  
//function validate() {
//    var isValid = true;
//    if ($('#Qty').val().trim() == "") {
//        $('#Qty').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Qty').css('border-color', 'lightgrey');
//    }
//    if ($('#Price').val().trim() == "") {
//        $('#Price').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Price').css('border-color', 'lightgrey');
//    }
//    if ($('#Total').val().trim() == "") {
//        $('#Total').css('border-color', 'Red');
//        isValid = false;
//    }
//    else {
//        $('#Total').css('border-color', 'lightgrey');
//    }

//    return isValid;
//}