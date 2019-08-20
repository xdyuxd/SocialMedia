$(document).ready(function (e) {

    //Date
    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    $("#birthdate").val(today);


    function runEffect() {
        $("#box-registration").fadeOut(1)
        $("#box_login").fadeIn(450)
    }

    function Register() {
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


<<<<<<< HEAD
    $(".register-form-login").on("click", function () {
            runEffect();
    });

    $("#button_return").on("click", function () {
        $("#box_login").fadeOut(1);
        $("#box-registration").fadeIn(450);
    });

    $(".register-form-sign-up").on("click", function () {
        Register();
    });



    //var first_color = new Array(199, 179, 255, 100);
    //var second_color = new Array(212, 208, 162, 83); //https://www.gradient-animator.com/ 
    //setInterval(function () {
    //    $(".gradient").css({ "background-image": `linear-gradient(${deg}deg, rgba(${first_color.toString()}), rgba(${second_color.toString()})` })
    //    console.log(first_color.toString())

    //    //first_color[0] += 15;
    //    //second_color[0] -= 15;
    //    deg += 15;
=======
        first_color[0] += 15;
        second_color[0] -= 15;
        deg += 15;
>>>>>>> 879a93c323404c6d10eda472b6fbed4e33e9a579

    //}, 1500);



});


