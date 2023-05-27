
$(document).on("click", "#btn-login", function () {

    $(".invalid-feedback").hide();
    $(".invalid-feedback").html("");
    var $btn = $(this);
    $btn.prop('disabled', true);

    var dataPost = new Object();
    dataPost.email = $("#yourEmail").val();
    dataPost.password = $("#yourPassword").val();

    $.ajax({
        type: "POST",
        url: "",
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.status == false) {
                // login gagal
                generateError(".email .invalid-feedback", response?.result?.errors?.email);
                generateError(".password .invalid-feedback", response?.result?.errors?.password);
                alert(response.result.error);
                $(".invalid-feedback").show();
            } else {
                window.location.href = "../../Home/Index";
            }

            $btn.prop('disabled', false);
        },
        failure: function (response) {
            console.log(response);
            alert('terjadi kesalahan, hubungi tim developer');
            $btn.prop('disabled', false);
        }
    });
});

$(document).on("click", "#btn-register", function () {
    $(".invalid-feedback").hide();
    $(".invalid-feedback").html("");

    var $btn = $(this);
    $btn.prop('disabled', true);

    var dataPost = new Object();
    dataPost.firstName = $("#firstName").val();
    dataPost.lastName = $("#lastName").val();
    dataPost.email = $("#yourEmail").val();
    dataPost.password = $("#yourPassword").val();
    dataPost.repeatPassword = $("#yourRepeatPassword").val();

    if (dataPost.password != dataPost.repeatPassword) {
        alert('password and repeat Password not match');
        return null;
    }

    $.ajax({
        type: "POST",
        url: "",
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            var email = response.result.email;
            if (email != null) {
                alert('create sukses')
                window.location.href = "../../auth/login";
            } else {
                generateError(".firstName .invalid-feedback", response.result.errors.firstName);
                generateError(".lastName .invalid-feedback", response.result.errors.lastName);
                generateError(".yourEmail .invalid-feedback", response.result.errors.email);
                generateError(".yourPassword .invalid-feedback", response.result.errors.password);
                $(".invalid-feedback").show();
            }
            $btn.prop('disabled', false);
        },
        failure: function (response) {
            console.log(response);
            alert('terjadi kesalahan, hubungi tim developer');
            $btn.prop('disabled', false);
        }
    });
});

