function Login() {
    var empObj = {
        Username: $('#username').val(),
        Password: $('#password').val()
    };
    $.ajax({
        url: "/Login/Login",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.ID > 0) {
                var a = window.location.host + "/transactions";
                alert('Berhasil = '+a);
                location.href = "/transactions";
            }
            else {
                alert('gagal');
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText+" Error");
        }
    });
}