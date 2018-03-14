$(document).ready(function () {

    if ($('#IfConnected').text() == "False") {
        ConnectServer();
    }
    else {
        PlayGame();
    }

});
function ConnectServer() {
    $.ajax(
        {
            url: '/FirstPage/WaitingForPlayer',
            type: 'POST',
            data: { 'id': $('#Playeridd').val() },
            success: function (response) {
                if (response == 'Tru') {
                    PlayGame();
                }
                else {
                    setTimeout(function () {
                        ConnectServer();
                    }, 3000);

                }
            },
            error: function (errorThrown) {
                console.log(errorThrown);
            }
        });
}




function PlayGame() {
    $('#Hide1').prop("hidden", true);
    $('#Hide2').prop("hidden", true);
    $('#Hide3').hide();
    $('#quitButton').hide();
    $('#Show4').prop("hidden", false);
    $('#Show1').prop("hidden", false);
    $('#Show3').prop("hidden", false);
}