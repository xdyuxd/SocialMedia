$(document).ready(function (e) {
    $("#counter-button").on("click", function () {
        $(".counter").addClass('counter--show');
        $('.counter--show').removeClass("counter");
    })
});

