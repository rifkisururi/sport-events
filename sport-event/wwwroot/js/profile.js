
$(document).on("click", "#updateUser", function () {
    var $btn = $(this);
    $btn.prop('disabled', true);

    var data = new Object();
    data.firstName = $("#firstName").val();
    data.lastName = $("#lastName").val();
    data.email = $("#email").val();

    $.ajax({
        type: "POST",
        url: "updateData",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.status == false) {
                // login gagal
                alert(response.result.message);
            } else {
                alert('udpate data success');
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

$(document).on("click", "#changePassword", function () {
    var $btn = $(this);
    $btn.prop('disabled', true);

    var data = new Object();
    data.currentPassword = $("#currentPassword").val();
    data.newPassword = $("#newPassword").val();
    data.renewPassword = $("#renewPassword").val();
    if (data.newPassword != data.renewPassword) {
        alert('New Password and Re-enter New Password mush same !');
        $btn.prop('disabled', false);
        return null;
    }

    $.ajax({
        type: "POST",
        url: "updatePassword",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.status == false) {
                // login gagal
                alert('udpate password gagal');
            } else {
                alert('udpate password success');
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

$(document).on("click", "#deteleUser", function () {
    var $btn = $(this);
    $btn.prop('disabled', true);

    $.ajax({
        type: "GET",
        url: "delete",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.status == false) {
                alert(response.result.message);
            } else {
                alert('delete success');
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

