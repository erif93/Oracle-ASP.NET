$(document).ready(function () {
    $('#datatables').DataTable({
        "ajax": LoadIndexSupplier()
    });
    LoadIndexSupplier();
});

function LoadIndexSupplier() {
    debugger;
    $.ajax({
        url: 'http://localhost:1349/api/items',
        dataType: 'json',
        async: false, // penting untuk sort data 
        type: 'GET',
        success: function (data) {
            var html = '';
            for (var i = 0; i < data.length; i++) {
                html = html + '<tr id="">';

                html = html + '<td>' + (i + 1) + '</td>'

                html = html + '<td>' + data[i].Name + '</td>';

                html = html + '<td>' + data[i].Price + '</td>';

                html = html + '<td>' + data[i].Stock + '</td>';

                html = html + '<td>' + data[i].Supplier + '</td>';

                html = html + '<td><input id="qty'+i+'" type="number" onkeyup="this.value = minmax(this.value, 1, '+data[i].Stock+')"></input></td>';

                html = html + '<td><button onclick="AddCart(' + data[i].Id + ',' + i + ')">Add</button></td>';

                html = html + '</tr>';
            }
            $('#tBody').html(html);
        }

    });
}

function Search() {
    debugger;
    var item = new Object($('#cari').val());
    var searchby = new Object($('#searchby').val());
    console.log(item);
    $.ajax({
        url: 'http://localhost:1349/api/items/Search/?name=' + item + '&&search=' + searchby,
        dataType: 'json',
        type: 'GET',
        success: function (data) {
            var html = '';
            for (var i = 0; i < data.length; i++) {
                html = html + '<tr>';
                html = html + '<tr>';

                html = html + '<td>' + (i + 1) + '</td>';

                html = html + '<td>' + data[i].Name + '</td>';

                html = html + '<td>' + data[i].Price + '</td>';

                html = html + '<td>' + data[i].Stock + '</td>';

                html = html + '<td>' + data[i].Supplier + '</td>';

                html = html + '<td><input type="number" id="qty"></input></td>';

                html = html + '<td><button onclick="return GetById(' + data[i].Id + ')">Add</button></td>';

                html = html + '</tr>';
            }
            $('#tBody').html(html);
        }

    });
}
function GetById(Id) {
    $.ajax({
        url: 'http://localhost:1349/api/Items/' + Id,
        type: 'GET',
        dataType: 'json',
        success: function (result) {
            $('#Id').val(result.Id);
            $('#Name').val(result.Name);
            $('#NameOld').val(result.Name);
            $('#Price').val(result.Price);
            $('#PriceOld').val(result.Price);
            $('#Stock').val(result.Stock);
            $('#StockOld').val(result.Stock);
            $('#Supplier').val(result.Supplier);
            $('#SupplierOld').val(result.Supplier);
            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    });
}

function viewCart() {
    $.ajax({
        url: 'http://localhost:1349/api/cart',
        dataType: 'json',
        type: 'GET',
        success: function (data) {
            var html = '';
            for (var i = 0; i < data.length; i++) {
                html = html + '<tr>';
                html = html + '<tr>';

                html = html + '<td>' + (i + 1) + '</td>';

                html = html + '<td>' + data[i].Name + '</td>';

                html = html + '<td>' + data[i].Price + '</td>';

                html = html + '<td>' + data[i].Stock + '</td>';

                html = html + '<td>' + data[i].Supplier + '</td>';

                html = html + '<td>' + data[i].Quantity + '</td>';

                html = html + '<td><button onclick="return GetById(' + data[i].Id + ')">Delete</button></td>';

                html = html + '</tr>';
            }
            $('#tBody').html(html);
        }

    });
}

//function check() {
//    debugger;
//    if (document.getElementById("#qyt").validity.rangeOverflow) {
//        txt = "Value too large";
//    } 
//}

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
        url: 'http://localhost:1349/api/Items/' + Id,
        dataType: 'json',
        async: false, // penting untuk sort data 
        type: 'GET',
        success: function (data) {
            var cart = new Object();
            cart.Quantity = $(qtyy).val();
            var html = '';
                html = html + '<tr>';

                html = html + '<td>' + data.Name + '</td>';

                html = html + '<td>' + data.Price + '</td>';

                html = html + '<td>' + data.Stock + '</td>';

                html = html + '<td>' + data.Supplier+ '</td>';

                html = html + '<td>' + cart.Quantity + '</td>';

                html = html + '<td>' + (data.Price * cart.Quantity) + '</td>';

                html = html + '</tr>';
                
            $('#cartlist').append(html);
            $('#btncart').attr('src', '/Content/icon/shopping-cart-loaded.png');
            //$('#btncart').text('~/Content/icon/shopping-cart.png')
        }
    });
}