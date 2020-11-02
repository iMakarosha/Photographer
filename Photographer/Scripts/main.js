$(document).ready(function () {

    // опроверка поля номера телефона и создание маски номера телефона
    $(function ($) {
        phone = $(".phone");
        var options = {
            onChange: function () {
                $('.go').attr('disabled', 'disabled');
            },
            onComplete: function () {
                $('.go').removeAttr('disabled');
            }
        };

        phone.mask("+7(999) 999-9999", options);
    });
    // обработка и отправка форм сайта
});