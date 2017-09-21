

function ConfirmReactive() {
    return confirm("What you sure you want to re-active this user?");
}

function ConfirmDelete(id, page) {
    var r = confirm("What you sure you want to delete this user?");

    if (r) {
        window.onDeactive(id, page);
    }
}

