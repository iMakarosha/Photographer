/*category*/
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


/*album*/
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


/*photo*/
var Store = { files: [] }; 
document.getElementById('fileInput').addEventListener('change', handleChange, false);

function handleChange(evt) {
    Store.files.splice(0, Store.files.length);
    var file = evt.target.files; 
    if (file.length != 0) {
        var f = file[0];
        addFiles(f);
        if (!f.type.match('image.*')) {
            alert("Image only please....");
        }
    }
}
function addFiles(files) {
    Store.files = Store.files.concat(files);
}

$('#addPhotoInput').on('click', function (e) {
    e.preventDefault();
    var files = Store.files;
    if (files.length > 0) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            if (files[0].type === "image/jpeg") {
                data.append("photoAlbum", $('#PhotoAlbum option:selected').text());
                data.append("file", files[0]);
                $.ajax({
                    type: "POST",
                    url: '/Manage/AddPhoto',
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
                        if (xhr.status == "500") {
                            alert("Размер загружаемых изображений превысил установленный лимит в 4МБ")
                        }
                    }
                });
            }
        }
        else {
            alert("Браузер не поддерживает загрузку файлов html5!");
        }
    }
    else {
        alert("Выберите изображение!");
    }        
});

$("#editPhotoModal").on("show.bs.modal", function (event) {
    var button = $(event.relatedTarget);
    var rec = button.data('whatever');
    var modal = $(this);
    modal.find('.modal-body #photoId').val(rec);
    $("#partForm3").submit();
});

function deletePhoto(photoId) {
    if (confirm("Вы уверены, что хотите удалить фотографию?")) {
        var data = new FormData();
        data.append("photoId", photoId);
        $.ajax({
            type: "POST",
            url: '/Manage/DeletePhoto',
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


function deleteBooking(bookingId) {
    if (confirm("Вы уверены, что хотите удалить категорию и все связанные с ней альбомы и фотографии?")) {
        var data = new FormData();
        data.append("bookingId", bookingId);
        $.ajax({
            type: "POST",
            url: '/Manage/DeleteBooking',
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