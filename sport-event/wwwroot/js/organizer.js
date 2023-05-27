$(document).ready(function () {
    getData(1,10);
});

function getData(page,perPage) {
    $('.dataorganizer').dataTable().fnClearTable();
    $('.dataorganizer').dataTable().fnDestroy();

    $('.dataorganizer').DataTable({
        processing: true,
        serverSide: true,
        ajax: {
            url: 'GetData?page=' + page + '&perPage=' + perPage,
            type: 'GET',
            dataSrc: 'data'
        },
        columns: [
            { data: 'id' },
            { data: 'organizerName' },
            {
                data: 'imageLocation',
                render: function (data) {
                    return '<img src="' + data + '" alt="Image" width="50">';
                }
            },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (o) {
                    var btnChange = ""
                    btnChange += "<button class='btn btn-primary btnUpdate' id='id_" + o.id + "'> <span class='spinner-border spinner-border-sm' role='status' aria-hidden='true' style='display:none;'></span> Update</button> "
                    btnChange += "<button class='btn btn-danger btnRemove' id='id_" + o.id + "'><span class='spinner-border spinner-border-sm' role='status' aria-hidden='true' style='display:none;'></span> Remove</button>"
                    return `${btnChange}`;
                }
            }
        ],
        paging: true,
        lengthMenu: [10, 25, 50, 75, 100],
        pageLength: 10
    });

    $(".dataTables_filter").remove()
    $(".dataTables_length label select").val(perPage);

}
$(document).on("change", ".dataTables_length label select", function () {
    console.log('jumlah record', $(this).val());
    getData(1, $(this).val());
    
});


$(document).on("click", ".btnRemove", function () {
    let text = "are you sure ?";
    if (confirm(text) == true) {
    } else {
        return null;
    }

    var $btn = $(this);
    $btn.prop('disabled', true);
    $btn.find('span.spinner-border').show();

    id = $(this).attr("id")
    id = id.replace("id_", "")
    console.log(id);

    $.ajax({
        type: "Get",
        url: "removeData?id="+id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.status == true) {
                alert('remove sukses')
            } else {
                alert('terjadi kesalahan')
            }

            $btn.prop('disabled', false);
            $btn.find('span.spinner-border').hide();
        },
        failure: function (response) {
            console.log(response);
            alert('terjadi kesalahan, hubungi tim developer');
            $btn.prop('disabled', false);
        }
    });

});

$(document).on("click", "#btn-save", function () {
    $(".invalid-feedback").hide();
    $(".invalid-feedback").html("");

    var $btn = $(this);
    $btn.prop('disabled', true);
    $btn.find('span.spinner-border').show();

    var dataPost = new Object();
    dataPost.organizerName = $("#name").val();
    dataPost.imageLocation = $("#image").val();

    $.ajax({
        type: "POST",
        url: "addData",
        data: JSON.stringify(dataPost),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.result.id == 0) {
                generateError(".name .invalid-feedback", response.result.errors.organizerName);
                generateError(".image .invalid-feedback", response.result.errors.imageLocation);
                $(".invalid-feedback").show();
            } else {
                alert('sukses')
                $(".closeAdd").click()
            }

            $btn.prop('disabled', false);
            $btn.find('span.spinner-border').hide();
        },
        failure: function (response) {
            console.log(response);
            alert('terjadi kesalahan, hubungi tim developer');
            $btn.prop('disabled', false);
        }
    });
});

$(document).on("click", ".btnUpdate", function () {
    alert('dalam pengembangan')
});
