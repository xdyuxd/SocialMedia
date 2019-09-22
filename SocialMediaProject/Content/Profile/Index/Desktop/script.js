$(document).ready(function (e) {

    var url_api = "https://localhost:44331/api/";
    var path_client_update = "client/update/";
    var extensions = new RegExp("\.jpg|\.jpeg|\.png");

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

    function CreateImg(photo) {
        var file = photo
        var img = document.createElement("img");
        var reader = new FileReader();
        reader.onloadend = function () {
            img.src = reader.result;
        }
        reader.readAsDataURL(file);
        return img
    }

    $(".settings-cover-photo input").change(function (e) {
        if (extensions.exec(e.target.files[0].name)) {
            let img = Reader(e);
            document.querySelector(".settings-cover-photo").appendChild(img);
            document.querySelector(".settings-cover-photo img").remove();
        }
    });

    $(".settings-profile-photo input").change(function (e) {
        if (extensions.exec(e.target.files[0].name)) {
            let img = Reader(e);
            document.querySelector(".settings-profile-photo").appendChild(img);
            document.querySelector(".settings-profile-photo img").remove();
        }
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

    function UpdateBio(type, url, data, name, surname, bio) {
        $.ajax({
            type: type,
            url: url,
            data: data,
            contentType: false, 
            processData: false,
            headers: {
                Name: name,
                Surname: surname,
                Bio: bio,
            },
        });
    }

    $(".settings-footer").on("click", function () {
        var nickname = window.location.pathname.substr(1);
        var formData = new FormData();
        var profile_file = $('.settings-profile-photo input');
        var cover_file = $('.settings-cover-photo input');

        if (profile_file[0].files.length) {
            $.each(profile_file, function (key, value) {
                var file = $(value)[0].files[0];
                formData.append("profile.png", file);
            });

            var img = CreateImg(profile_file[0].files[0])
            document.querySelector(".profile-photo").appendChild(img);
            document.querySelector(".profile-photo img").remove();
        }
        if (cover_file[0].files.length) {
            $.each(cover_file, function (key, value) {
                var file = $(value)[0].files[0];
                formData.append("cover.png", file);
            });

            var img = CreateImg(cover_file[0].files[0])
            document.querySelector(".cover-photo").appendChild(img);
            document.querySelector(".cover-photo img").remove();
        }

        var name = $(".settings-fullname div:first-child input").val();
        var surname = $(".settings-fullname div:last-child input").val();
        var bio = $(".settings-bio textarea").val();

        $(".profile-name h2").text(name);
        $(".profile-bio h3").text(bio);
        $(".profile-settings-block").hide();
        $(".profile-settings").hide();

        var url_update = "https://localhost:44331/api/client/update/" + nickname;
        var type = "PUT";
        UpdateBio(type, url_update, formData, name, surname, bio);
    });

    /* POP UP END */

    /* REQUEST TO BE FRIEND BUTTON START */

    function SendRequestFriend(session, nickname) {
        var response = $.ajax({
            type: "POST",
            url: "https://localhost:44331/api/friendship/create/" + session + "/" + nickname,
            headers: { Timestamp: String(new Date().getTime()) },
        });
    }

    function UndoRequestFriend(session, nickname) {
        var response = $.ajax({
            type: "DELETE",
            url: "https://localhost:44331/api/friendship/delete/" + session + "/" + nickname,
        });
    }


    $(".profile-request").on("click", function () {
        if ($(".profile-request h4").text() == "Undo request") {
            $(".profile-request i").removeClass("fa-user-times");
            $(".profile-request i").addClass("fa-user-plus");
            $(".profile-request h4").text("Add");
            var response = $.ajax({
                type: "POST",
                url: "https://localhost:44347/session/getcurrentclient",
                statusCode: {
                    200: function (response) {
                        nickname = window.location.pathname.substr(1);
                        UndoRequestFriend(response, nickname);
                    }
                },
            });
        }
        else {
            $(".profile-request i").removeClass("fa-user-plus");
            $(".profile-request i").addClass("fa-user-times");
            $(".profile-request h4").text("Undo request");
            var response = $.ajax({
                type: "POST",
                url: "https://localhost:44347/session/getcurrentclient",
                statusCode: {
                    200: function (response) {
                        nickname = window.location.pathname.substr(1);
                        SendRequestFriend(response, nickname);
                    }
                },
             });

        }
    });

    /* REQUEST TO BE FRIEND BUTTON END */



    $(".uploud-button input").change(function (e) {
        var formData = new FormData();
        var file = e.target.files[0];
        formData.append(file.name, file);

        $.ajax({
            url: url_api + "/client/" + localStorage.getItem('client') + "/post",
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: formData,
            error: function (e) {
                alert("FUDEU BICHO")
            }
        });
    });




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

    $(".form-commentary textarea").change(function (e) {
        if (e.target.value != "")
            $(".section-commentary span").remove();
    });

    $(".form-commentary div").on("click", function () {
        var comment = $('.form-commentary textarea').val();
        if (comment) {
            var name = "Teste";
            var src = "http://www.regrom.com/wp-content/uploads/2017/07/2006BF4261-400x400.jpg";
            var comment = `<div class="comment"><figure><img src="${src}" /><figcaption>${name}</figcaption></figure><h5>${comment}</h5><div class="fas fa-heart-square" id="counter-button" type="button">❤</div><h6 class="counter">0</h6></div>`;

            $('.commentary').prepend(comment);
            $(".form-commentary textarea").val("");

            var comment_counter = $('.comment').length;
            if (comment_counter >= 5) {
                $(".comment:last-child").remove();
            }
        }
        else {
            if (!$(".section-commentary span")[0]){
                $('.form-commentary').prepend('<span style="color: red; display: block; text-align: center">Please write some text before send the comment</span>');
            }
        }
    });
});

