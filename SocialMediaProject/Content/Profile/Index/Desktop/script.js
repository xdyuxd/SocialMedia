$(document).ready(function (e) {

    /* POP UP START */

    function Reader(e) {
        for (var i = 0; i < e.originalEvent.srcElement.files.length; i++) {

            var file = e.originalEvent.srcElement.files[i];

            var img = document.createElement("img");
            var reader = new FileReader();
            reader.onloadend = function () {
                img.src = reader.result;
            }
            reader.readAsDataURL(file);
            return img
        }
    }
    $(".settings-cover-photo input").change(function (e) {
        let img = Reader(e);
        document.querySelector(".settings-cover-photo").appendChild(img);
        document.querySelector(".settings-cover-photo img").remove();
    });

    $(".settings-profile-photo input").change(function (e) {
        let img = Reader(e);
        document.querySelector(".settings-profile-photo").appendChild(img);
        document.querySelector(".settings-profile-photo img").remove();
    });

    $("#profile-edit").on("click", function () {
        document.querySelector("body").classList.add("locked");
        document.body.scrollTop = 0;
        document.documentElement.scrollTop = 0;
        $(".profile-settings-block").show();
        $(".profile-settings").show();
    });

    $("#profile-edit").on("click", function () {
        document.querySelector("body").classList.add("locked");
        document.body.scrollTop = 0;
        document.documentElement.scrollTop = 0;
        $(".profile-settings-block").show();
        $(".profile-settings").show();
    });

    $(".settings-header i").on("click", function () {
        document.querySelector("body").classList.remove("locked");
        $(".profile-settings-block").hide();
        $(".profile-settings").hide();
    });

    /* POP UP END */

    $("#counter-button").on("click", function () {
        $(".counter").show();
    });


    $("#counter-button").on("click", function () {
        var likes = parseInt($('.counter').text());
        likes++;
        $(".counter").text(likes);
    });

    $("#dropdown-button-logout").on("click", function () {
        $.ajax({
            type: "post",
            url: "https://localhost:44347/logout"
        })
    });
});

