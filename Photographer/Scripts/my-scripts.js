$('#addCategoryInput').on('click', function (e) {
    e.preventDefault();
    var data = new FormData();
    if (!($('#CategoryName').val() === "")) {
        data.append("categoryName", $('#CategoryName').val());
        $.ajax({
            type: "POST",
            url: '/Manage/AddCategory',
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result === "ok") {
                    location.reload();
                }
                else {
                    alert(result);
                }
            },
            error: function (xhr, status, p3) {
            }
        });
    }
    else {
        alert("Введите имя новой категории!");
    }
});

$("#editCategoryModal").on("show.bs.modal", function (event) {
    var button = $(event.relatedTarget);
    var rec = button.data('whatever');
    var modal = $(this);
    modal.find('.modal-body #categoryId').val(rec);
    $("#partForm1").submit();
});

function deleteCategory(categoryId) {
    if (confirm("Вы уверены, что хотите удалить категорию и все связанные с ней альбомы и фотографии?")) {
        var data = new FormData();
        data.append("categoryId", categoryId);
        $.ajax({
            type: "POST",
            url: '/Manage/DeleteCategory',
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result === "ok") {
                    location.reload();
                }
                else {
                    alert(result);
                }
            },
            error: function (xhr, status, p3) {
            }
        });
    }
}


$('#addAlbumInput').on('click', function (e) {
    e.preventDefault();
    var data = new FormData();
    if (!($('#AlbumName').val() === "")) {
        data.append("albumName", $('#AlbumName').val());
        data.append("albumCategory", $('#AlbumCategory option:selected').text());
        $.ajax({
            type: "POST",
            url: '/Manage/AddAlbum',
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result === "ok") {
                    location.reload();
                }
                else {
                    alert(result);
                }
            },
            error: function (xhr, status, p3) {
            }
        });
    }
    else {
        alert("Введите имя нового альбома!");
    }
});

$("#editAlbumModal").on("show.bs.modal", function (event) {
    var button = $(event.relatedTarget);
    var rec = button.data('whatever');
    var modal = $(this);
    modal.find('.modal-body #albumId').val(rec);
    $("#partForm2").submit();
});

function deleteAlbum(albumId) {
    if (confirm("Вы уверены, что хотите удалить альбом и все связанные с ним фотографии?")) {
        var data = new FormData();
        data.append("albumId", albumId);
        $.ajax({
            type: "POST",
            url: '/Manage/DeleteAlbum',
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result === "ok") {
                    location.reload();
                }
                else {
                    alert(result);
                }
            },
            error: function (xhr, status, p3) {
            }
        });
    }
}