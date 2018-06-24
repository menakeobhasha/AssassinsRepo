function ShowMessageSuccess() {
    $('#msgSuccess').show();
    $("box").scrollTop(0);
    setTimeout(function () {
        $('#msgSuccess').fadeTo(300, 0.5).slideUp(1000, function () {
            $('#msgSuccess').alert('close');
        });
    }, 2000);
}

function ShowMessageError() {
    $('#msgError').show();
    $("#msgError").focus(function () {
        alert("Handler for .focus() called.");
    });
    setTimeout(function () {
        $('#msgError').fadeTo(300, 0.5).slideUp(1000, function () {
            $('#msgError').alert('close');
        });
    }, 2000);
}