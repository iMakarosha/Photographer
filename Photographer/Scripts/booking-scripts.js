/*booking*/
$('#addBookingInput').on('click', function (e) {
    e.preventDefault();
    var data = new FormData();
    if (!($('#UserName').val() === "" | $('#UserEmail').val() === "" | $('#UserPhone').val() === "")) {
        data.append("name", $('#UserName').val());
        data.append("email", $('#UserEmail').val());
        data.append("phone", $('#UserPhone').val());
        data.append("comment", $('#UserComment').val());
        data.append("service", $('#ServiceSelect').val());

        $.ajax({
            type: "POST",
            url: '/Home/AddBooking',
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result === "ok") {
                    location.reload();
                }
                else {
                    alert(result);
                }
            },
            error: function (xhr, status, p3) {
            }
        });
    }
    else {
        alert("Введите имя, телефон и емейл!");
    }
});