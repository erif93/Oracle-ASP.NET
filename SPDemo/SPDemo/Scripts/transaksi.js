﻿$(document).ready(function () {
    $('#datatable').DataTable({
        "ajax": loadData()
    });
    loadData();
    var errorItemCount = 0;
    $('#Save').click(function () {
        debugger;
        var isAllValid = true;
        var list = [];
        var transac;
        var tbody = $("#tablecart tbody");
        if (tbody.children().length == 0) {
            errorItemCount++;
            console.log(errorItemCount);
            $(this).addClass('error');
        } else {
            $('#tablecart tr').each(function (row, tr) {
                transac = {
                    ID_ITEM: parseInt($(tr).find('td:eq(0)').text()),
                    QTY: parseInt($(tr).find('td:eq(4)').text()),
                    PRICE: parseInt($(tr).find('td:eq(2)').text()),
                }
                list.push(transac);
            })
            list.shift();
        }

        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + " invalid entry in order item list.");
            isAllValid = false;
        }

        if (isAllValid) {

            $(this).val('Please wait...');
            console.log(list);
            //$.ajax({
            //    type: 'POST',
            //    url: '/Transactions/Add',
            //    data: JSON.stringify(list),
            //    contentType: 'application/json;charset=utf-8',
            //    dataType: "json",
            //    success: function (data) {
            //        if (data.status) {
            //            alert('Successfully saved');
            //            //here we will clear the form
            //            list = [];
            //            $('#tablecart').empty();
            //        }
            //        else {
            //            alert('Error');
            //        }
            //        $('#Save').val('Save');
            //    },
            //    error: function (error) {
            //        console.log(error);
            //        $('#Save').val('Save');
            //    }
            //});
        }

    });
    $('#Close').click(function () {
        debugger;
        errorItemCount = 0;
        $('#orderItemError').text('');
    
    })
    
});

//Load Data function  
function loadData() {
    console.log("tes");
    $.ajax({
        url: "/Items/List",
        type: "GET",
        async: false,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + (key+1) + '</td>';
                html += '<td>' + item.NAME + '</td>';
                html += '<td>' + item.PRICE + '</td>';
                html += '<td>' + item.STOCK + '</td>';
                html += '<td><input id="qty' + key + '" type="number" onkeyup="this.value = minmax(this.value, 1, ' + item.STOCK + ')"></input></td>';
                html += '<td><button onclick="AddCart(' + item.ID + ',' + key + ')">Add</button></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function minmax(value, min, max) {
    if (parseInt(value) < min || isNaN(parseInt(value)))
        return min;
    else if (parseInt(value) > max)
        return max;
    else return value;
}

function AddCart(Id, qty) {
    debugger;
    var qtyy = '#qty' + qty;
    $.ajax({
        url: '/Items/GetbyID/'+Id,
        dataType: 'json',
        async: false, // penting untuk sort data 
        type: 'GET',
        success: function (data) {
            var cart = new Object();
            cart.Quantity = $(qtyy).val();
            var html = '';
            html = html + '<tr>';

            html = html + '<td>' + data.ID + '</td>';

            html = html + '<td>' + data.NAME + '</td>';

            html = html + '<td>' + data.PRICE + '</td>';

            html = html + '<td>' + data.STOCK + '</td>';

            html = html + '<td>' + cart.Quantity + '</td>';

            html = html + '<td>' + (data.PRICE * cart.Quantity) + '</td>';

            html = html + '<td><button type="button" class="removebutton" id="delete" onclick="Delete()" style="cursor: pointer;font-weight: bold;">Delete</button></td>'

            html = html + '</tr>';

            $('#cartlist').append(html);
            $('#btncart').attr('src', '/Content/icon/shopping-cart-loaded.png');
            swal("Item Berhasil Masuk Keranjang");
            $('#qty' + qty).val('');

            var rowCount = document.getElementById('tablecart').rows.length;
            if (rowCount == 2) {
                var countTotal = 0;
                countTotal = countTotal + (data.PRICE * cart.Quantity);
                $('#total').val(countTotal);
            } else {
                var countTotal = $('#total');
                countTotal = countTotal + (data.PRICE * cart.Quantity);
                $('#total').val(countTotal);
            }
        }
    });
}

function Delete() {
    debugger;
    $('#delete').closest('tr').remove();
    var rowCount = document.getElementById('tablecart').rows.length;
    if (rowCount == 1) {
        $('#btncart').attr('src', '/Content/icon/shopping-cart.png');
    }
}
function Total() {
    debugger;
    var rowCount = document.getElementById('tablecart').rows.length;
    var total = 0;
    for (var i = 0 ; i <= rowCount; i++) {
        console.log('Row ' + parseFloat(i + 1) + ' : ' + document.getElementById('tablecart').rows[i].cells.length + ' column');
    }
}