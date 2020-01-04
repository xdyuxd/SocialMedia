$(document).ready(function (e) {

    //Date
    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    $('input[name="birthdate"]').val(today);

    $(".register-button-login").on("click", function () {
        $(".box-registration").fadeOut(1)
        $(".box-login").fadeIn(450)
    });

    $(".login-button-back").on("click", function () {
        $(".box-login").fadeOut(1);
        $(".box-registration").fadeIn(450);
    });

    //var first_color = new Array(199, 179, 255, 100);
    //var second_color = new Array(212, 208, 162, 83); //https://www.gradient-animator.com/ 
    //setInterval(function () {
    //    $(".gradient").css({ "background-image": `linear-gradient(${deg}deg, rgba(${first_color.toString()}), rgba(${second_color.toString()})` })
    //    console.log(first_color.toString())

    //    //first_color[0] += 15;
    //    //second_color[0] -= 15;
    //    deg += 15;

    //}, 1500);

});


