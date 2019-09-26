$(document).ready(function (e) {

    var client = localStorage.getItem('client');
    var name = localStorage.getItem('name');
    var profile_pic = localStorage.getItem('profile_pic');

    var url_api = "https://localhost:44331/api/"; //"https://socialmedia-api.azurewebsites.net/api" //"https://localhost:44331/api/";
    var path_client_update = "client/update/";
    var extensions = new RegExp("\.jpg|\.jpeg|\.png");
    var gallery_pagination = 1;
    var gallery_end = false;


    $("#dropdown-button-logout").on("click", function () {
        $.ajax({
            type: "post",
            url: "https://localhost:44347/logout"
        }).done(function (data) {
            window.location.replace(data.url);
        })
    });


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
        $("body").removeClass("locked");

        var url_update = url_api + "client/update/" + nickname;
        var type = "PUT";
        UpdateBio(type, url_update, formData, name, surname, bio);
    });

    /* POP UP END */

    /* REQUEST TO BE FRIEND BUTTON START */

    function SendRequestFriend(session, nickname) {
        var response = $.ajax({
            type: "POST",
            url: url_api + "friendship/create/" + session + "/" + nickname,
            headers: { Timestamp: String(new Date().getTime()) },
        });
    }

    function UndoRequestFriend(session, nickname) {
        var response = $.ajax({
            type: "DELETE",
            url: url_api + "friendship/delete/" + session + "/" + nickname,
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

    /* UPLOUD IMAGE START */

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
                alert("Uploud error");
            }
        });
    });

    /* UPLOUD IMAGE END */


    /* RENDER FEED START */
    var paused = false;

    function RenderPost(response) {
        $(".gallery-waypoint").remove();
        $(".gallery-loader").remove();
        if (response.length == 0) {
            $(".gallery-body").remove();
            $(".gallery").append('<h4>No photos yet :c</h4>')
        }
        else {
            response.forEach(function (photo) {
                $(".gallery-body").append(`<div class="post"><img src="data:image/png;base64,${photo.Img}"/></div>`)
            })
        }

        if (response.length == 9) {
            $(".gallery-body").append('<div class="gallery-waypoint"></div>');
            gallery_pagination++;
            console.log(gallery_pagination);
            paused = false;
        }
        else {
            gallery_end = true;
            paused = true;
        }
    }

    $(window).scroll(function () {
        if (client != window.location.pathname.substr(1)) {
            client = window.location.pathname.substr(1)
        }

        if (gallery_end == false && paused == false) {
            if (Math.trunc($(window).scrollTop()) > ($(document).height() - $(window).height() - 200)) {
                paused = true;
                $(".gallery-waypoint").append('<div class="loader gallery-loader"></div>')
                $.ajax({
                    type: "GET",
                    url: url_api + "client/" + client + "/gallery/" + gallery_pagination,
                    crossDomain: true,
                    headers: { "Access-Control-Allow-Origin": "*" },
                    success: function (response) {
                        RenderPost(response);
                    },
                    error: function (e) {
                        console.log(e);
                    }
                });
            }
        }
    });

    /* RENDER FEED END */

    $(".form-commentary div").on("click", function () {
        var comment = $('.form-commentary textarea').val();
        if (comment) {
            var recepient = window.location.pathname.substr(1)
            var comment_html = `<div class="comment"><figure><img src="data:image/png;base64,${profile_pic}"/><figcaption>${name}</figcaption></figure><h5>${comment}</h5><div class="fas fa-heart-square" id="counter-button" type="button">❤</div><h6 class="counter">0</h6></div>`;

            $('.commentary').prepend(comment_html);
            $(".form-commentary textarea").val("");

            var comment_counter = $('.comment').length;
            if (comment_counter >= 5) {
                $(".comment:last-child").remove();
            }

            $.ajax({
                type: "POST",
                url: url_api + `comment/create/${client}/${recepient}`,
                headers: { text: comment },
            });
        }
        else {
            if (!$(".section-commentary span")[0]) {
                $('.form-commentary').prepend('<span style="color: red; display: block; text-align: center">Please write some text before send the comment</span>');
            }
        }
    });


    $("#counter-button").on("click", function () {
        $(".counter").show();
    });


    $("#counter-button").on("click", function () {
        var likes = parseInt($('.counter').text());
        likes++;
        $(".counter").text(likes);
    });



    $(".form-commentary textarea").change(function (e) {
        if (e.target.value != "")
            $(".section-commentary span").remove();
    });


});

