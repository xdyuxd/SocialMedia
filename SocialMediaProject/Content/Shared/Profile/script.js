$(document).ready(function (e) {

    url_api = "https://localhost:44331/api/";

    $(".friendship-notification button").click(function () {
        var id = $(this).val();
        if (id) {
            $.ajax({
                type: "POST",
                url: url_api + `friendship/accept/${id}`,
            });
        }
    });

});