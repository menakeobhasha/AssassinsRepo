function InformationMessage(message) {
    $("#PopMessage").dialog(
        {
            title: "Information",
            modal: true,
            buttons: {
                Close: function () {
                    $(this).dialog('close');
                }
            }
        });
    document.getElementById("MessageText").innerHTML = message;
}

function ErrorMessage(message, controllerId) {
    $("#PopMessage").dialog(
        {
            title: "Error",
            modal: true,
            buttons: {
                Close: function () {
                    $(this).dialog('close');
                    $('[id*=' + controllerId + ']').select();
                    $('[id*=' + controllerId + ']').focus();
                }
            }
        });
    document.getElementById("MessageText").innerHTML = message;
}

function SearchPopFunction(divName, titleNmae) {

    $("#" + divName).dialog(
        {
            minHeight: 200,
            minWidth: 600,
            draggable: true,
            title: titleNmae,
            modal: true,
            buttons: {
                OK: function () {
                    $(this).dialog('close');
                },
                Close: function () {
                    $(this).dialog('close');
                }
            }, open: function (type, data) {
                $(this).parent().appendTo("form");
            }
        });
}

