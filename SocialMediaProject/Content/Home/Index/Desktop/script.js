$(document).ready(function (e) {
    function runEffect() {
        $("#box_registration").fadeOut(1);
        $("#box_login").fadeIn(450);
    }

    $("#login_button").on("click", function () {
            runEffect();
            return false;
    });
    $("#button_return").on("click", function () {
        $("#box_login").fadeOut(1);
        $("#box_registration").fadeIn(450);
        return false;
    });

    function Login() {
        $.ajax
            ({
                type: "POST",
                url: "https://localhost:44331/api/client/create",
                crossDomain: true,
                dataType: 'json',
                async: true,
                headers: {
                    'nickname': $("input#nickname").val(),
                    'name': $("input#first_name").val(),
                    'surname': $("input#last_name").val(),
                    'email': $("input#email").val(),
                    'birthdate': new Date($("input#birthdate").val()).getTime() / 1000,
                    'password': $("input#password").val(),

                }
            });
    }


    $("#register_button").on("click", function () {
        Login();
        return false;
    });
});


