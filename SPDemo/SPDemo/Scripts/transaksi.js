$(document).ready(function () {
    $('#datatable').DataTable({
        "ajax": loadData()
    });
    loadData();
});

//Load Data function  
function loadData() {
    debugger;
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
    var qtyy = '#qty' + qty;
    debugger;
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

            html = html + '<td>' + data.NAME + '</td>';

            html = html + '<td>' + data.PRICE + '</td>';

            html = html + '<td>' + data.STOCK + '</td>';

            html = html + '<td>' + cart.Quantity + '</td>';

            html = html + '<td>' + (data.PRICE * cart.Quantity) + '</td>';

            html = html + '</tr>';

            $('#cartlist').append(html);
            $('#btncart').attr('src', '/Content/icon/shopping-cart-loaded.png');
            //$('#btncart').text('~/Content/icon/shopping-cart.png')
        }
    });
}