$(document).ready(function () {
    $('#datatable').DataTable({
        "ajax": loadData()
    });
    loadData();
});

//Load Data function  
function loadData() {
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
    var qtyy = '#qty' + qty;
    $.ajax({
        url: '/Items/GetbyID/' + Id,
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

            html = html + '<td>' + cart.Quantity + '</td>';

            html = html + '<td>' + (data.PRICE * cart.Quantity) + '</td>';

            html = html + '<td><button type="button" class="removebutton" id="delete' + (data.PRICE * cart.Quantity) + '" onclick="Delete(' + (data.PRICE * cart.Quantity) + ')" style="cursor: pointer;font-weight: bold;">Delete</button></td>'

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
                var countTotal = $('#total').val().toString();
                var sumTotal = parseInt(countTotal);
                sumTotal = sumTotal + (data.PRICE * cart.Quantity);
                $('#total').val(sumTotal);
            }
        }
    });
}

function Delete(Id) {
    $('#delete'+Id).closest('tr').remove();
    var rowCount = document.getElementById('tablecart').rows.length;
    if (rowCount == 1) {
        $('#btncart').attr('src', '/Content/icon/shopping-cart.png');
        $('#total').val('');
    } else {
        var total = $('#total').val();
        var totalmin = total - Id;
        $('#total').val(totalmin);
    }
}