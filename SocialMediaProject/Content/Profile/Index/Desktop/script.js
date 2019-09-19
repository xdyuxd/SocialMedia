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

    $(".fa-edit").on("click", function () {
        document.querySelector("body").classList.add("locked");
        document.body.scrollTop = 0;
        document.documentElement.scrollTop = 0;
        $(".profile-settings-block").show();
        $(".profile-settings").show();
    });

    $(".fa-edit").on("click", function () {
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
        }).done(function (data) {
            window.location.replace(data.url);
        })
    });

    $(".comment-button").on("click", function () {
        var comment_input = $('.comment-input').val();
        var comment = 
            '<div class="comments"><div>' +
            '<figure>' +
            ' <img src="" />' +
            '<figcaption></figcaption>' +
            '</figure>' +
            ' <h5> ' +comment_input+ '</h5>' +
            '<div class="answer-comment">' +
            '<form>' +
            '<button class="fas fa-heart-square" id="counter-button" type="button">❤</button>' +
            '<h6 class="counter">0</h6>' +
            '<textarea m axlength="20"></textarea>' +
            '</form>' +
            '<h6></h6>' +
            '</div>' +
            '</div>' +
            '</div>';         

        $('.comments-1').prepend(comment);
        $(".comment-input").val(" ");

        function NoMoreThanFive() {
            var countdiv = $('.comments').length;
            if (countdiv >= 5) {
                $(".comments:last-child").remove();
            }
        }
        NoMoreThanFive();

    });
});

