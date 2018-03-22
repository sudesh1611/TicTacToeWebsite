var globalTurn = false;
var temp = "Sudesh";
var LostCount = 0;
var WonCount = 0;
var TieCount = 0;
$(document).ready(function () {
    globalTurn = false;
    $('#myModal').modal('hide');
    $('#HideRow').hide();
    $('#ShowRow').hide();
    var myID = $('#MyID').text();
    if ($('#Turn').text() == "Mine") {
        globalTurn = true;
        setTimeout(function () {
            $.ajax({
                url: '/HomeM/checkIfOpponentAlive',
                type: 'POST',
                data: { 'ID': myID },
                success: function (response) {
                    if (response == "Tru") {
                        $('#HideUltimate').hide();
                        $('#HideRow').show();
                        $('#turnButton').css('background-color', 'cornflowerblue');
                        $('#turnButton').css('color', '#FFFFFF');
                        $('#turnButton').text('Your Turn');
                        checkStatus(myID);
                    }
                    else if (response == "Fals") {
                        OpponentNotExist();
                    }
                    else {
                        ConnectionChanged();
                    }
                },
                error: function (errorThrown) {
                    console.log(errorThrown);
                    ConnectionChanged();
                }
            });

        }, 5000);
    }
    else {
        globalTurn = false;
        setTimeout(function () {
            $.ajax({
                url: '/HomeM/checkIfOpponentAlive',
                type: 'POST',
                data: { 'ID': myID },
                success: function (response) {
                    if (response == "Tru") {
                        $('#HideUltimate').hide();
                        $('#HideRow').show();
                        $('#turnButton').css('background-color', '#FFFFFF');
                        $('#turnButton').css('color', 'cornflowerblue');
                        $('#turnButton').text("Opponent's Turn");
                        checkStatus(myID);
                    }
                    else if (response == "Fals") {
                        OpponentNotExist();
                    }
                    else {
                        ConnectionChanged();
                    }
                },
                error: function (errorThrown) {
                    console.log(errorThrown);
                    ConnectionChanged();
                }
            });

        }, 5000);
    }
});

function checkStatus(myID) {
    $.ajax(
        {
            url: '/HomeM/checkStatus',
            type: 'POST',
            data: { 'ID': myID },
            dataType: 'json',
            success: function (response) {
                var parsedJson = response;
                temp = parsedJson;
                if (parsedJson.ifQuit == "True") {
                    OpponentQuit(parsedJson.myID);
                }
                else {
                    if (parsedJson.turn == "Mine") {
                        globalTurn = true;
                        YouCanPlayTheGame(parsedJson);
                    }
                    else if (parsedJson.status == "Won") {
                        YouCanPlayTheGame(parsedJson);
                    }
                    else if (parsedJson.status == "Lost") {
                        YouCanPlayTheGame(parsedJson);
                    }
                    else if (parsedJson.status == "Tie") {
                        YouCanPlayTheGame(parsedJson);
                    }
                    else {
                        globalTurn = false;
                        setTimeout(function () {
                            checkStatus(myID);
                        }, 3000);
                    }
                }
            },
            error: function (errorThrown) {
                console.log(errorThrown);
                ConnectionChanged();
            }
        });
}

function YouCanPlayTheGame(parsedJson) {
    $('#turnButton').css('background-color', 'cornflowerblue');
    $('#turnButton').css('color', '#FFFFFF');
    $('#turnButton').text('Your Turn');
    for (i = 0; i <= 8; i++) {
        switch (i) {
            case 0:
                if (parsedJson.symbolPresent[i] == "b") {
                    $('#zeroth').attr('src', '../images/Blank.jpg');
                }
                else if (parsedJson.symbolPresent[i] == "O") {
                    $('#zeroth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#zeroth').attr('src', '../images/Cross.jpg');
                }
                break;
            case 1:
                if (parsedJson.symbolPresent[i] == "b") {
                    $('#first').attr('src', '../images/Blank.jpg');
                }
                else if (parsedJson.symbolPresent[i] == "O") {
                    $('#first').attr('src', '../images/Circle.png');
                }
                else {
                    $('#first').attr('src', '../images/Cross.jpg');
                }
                break;
            case 2:
                if (parsedJson.symbolPresent[i] == "b") {
                    $('#second').attr('src', '../images/Blank.jpg');
                }
                else if (parsedJson.symbolPresent[i] == "O") {
                    $('#second').attr('src', '../images/Circle.png');
                }
                else {
                    $('#second').attr('src', '../images/Cross.jpg');
                }
                break;
            case 3:
                if (parsedJson.symbolPresent[i] == "b") {
                    $('#third').attr('src', '../images/Blank.jpg');
                }
                else if (parsedJson.symbolPresent[i] == "O") {
                    $('#third').attr('src', '../images/Circle.png');
                }
                else {
                    $('#third').attr('src', '../images/Cross.jpg');
                }
                break;
            case 4:
                if (parsedJson.symbolPresent[i] == "b") {
                    $('#fourth').attr('src', '../images/Blank.jpg');
                }
                else if (parsedJson.symbolPresent[i] == "O") {
                    $('#fourth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#fourth').attr('src', '../images/Cross.jpg');
                }
                break;
            case 5:
                if (parsedJson.symbolPresent[i] == "b") {
                    $('#fifth').attr('src', '../images/Blank.jpg');
                }
                else if (parsedJson.symbolPresent[i] == "O") {
                    $('#fifth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#fifth').attr('src', '../images/Cross.jpg');
                }
                break;
            case 6:
                if (parsedJson.symbolPresent[i] == "b") {
                    $('#sixth').attr('src', '../images/Blank.jpg');
                }
                else if (parsedJson.symbolPresent[i] == "O") {
                    $('#sixth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#sixth').attr('src', '../images/Cross.jpg');
                }
                break;
            case 7:
                if (parsedJson.symbolPresent[i] == "b") {
                    $('#seventh').attr('src', '../images/Blank.jpg');
                }
                else if (parsedJson.symbolPresent[i] == "O") {
                    $('#seventh').attr('src', '../images/Circle.png');
                }
                else {
                    $('#seventh').attr('src', '../images/Cross.jpg');
                }
                break;
            case 8:
                if (parsedJson.symbolPresent[i] == "b") {
                    $('#eighth').attr('src', '../images/Blank.jpg');
                }
                else if (parsedJson.symbolPresent[i] == "O") {
                    $('#eighth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#eighth').attr('src', '../images/Cross.jpg');
                }
                break;
        }
    }
    if (parsedJson.status == "Won") {
        YouWon(parsedJson.myID);
    }
    else if (parsedJson.status == "Lost") {
        YouLost(parsedJson.myID);
    }
    else if (parsedJson.status == "Tie") {
        GameTied(parsedJson.myID);
    }
}


function clicked(id) {
    if (globalTurn == true && temp.ifClicked[id] == 0) {
        globalTurn = false;
        var symbol = $('#MySymbol').text();
        if (symbol == "X") {
            temp.ifClicked[id] = 1;
            temp.symbolPresent[id] = "X";
        }
        else if (symbol == "O") {
            temp.ifClicked[id] = 1;
            temp.symbolPresent[id] = "O";
        }
        switch (id) {
            case 0:
                if (symbol == "O") {
                    $('#zeroth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#zeroth').attr('src', '../images/Cross.jpg');
                }
                break;
            case 1:
                if (symbol == "O") {
                    $('#first').attr('src', '../images/Circle.png');
                }
                else {
                    $('#first').attr('src', '../images/Cross.jpg');
                }
                break;
            case 2:
                if (symbol == "O") {
                    $('#second').attr('src', '../images/Circle.png');
                }
                else {
                    $('#second').attr('src', '../images/Cross.jpg');
                }
                break;
            case 3:
                if (symbol == "O") {
                    $('#third').attr('src', '../images/Circle.png');
                }
                else {
                    $('#third').attr('src', '../images/Cross.jpg');
                }
                break;
            case 4:
                if (symbol == "O") {
                    $('#fourth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#fourth').attr('src', '../images/Cross.jpg');
                }
                break;
            case 5:
                if (symbol == "O") {
                    $('#fifth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#fifth').attr('src', '../images/Cross.jpg');
                }
                break;
            case 6:
                if (symbol == "O") {
                    $('#sixth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#sixth').attr('src', '../images/Cross.jpg');
                }
                break;
            case 7:
                if (symbol == "O") {
                    $('#seventh').attr('src', '../images/Circle.png');
                }
                else {
                    $('#seventh').attr('src', '../images/Cross.jpg');
                }
                break;
            case 8:
                if (symbol == "O") {
                    $('#eighth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#eighth').attr('src', '../images/Cross.jpg');
                }
                break;
        }
        var myJson = JSON.stringify(temp);
        $.ajax(
            {
                url: '/HomeM/updateStatus',
                type: 'POST',
                data: { 'updatedJson': myJson },
                success: function (response) {
                    if (response == 'Okay') {
                        globalTurn = false;
                        $('#turnButton').css('background-color', '#FFFFFF');
                        $('#turnButton').css('color', 'cornflowerblue');
                        $('#turnButton').text('Opponent\'s Turn');
                        checkStatus(temp.myID);
                    }
                    else {
                        ConnectionChanged();
                    }
                },
                error: function (errorThrown) {
                    console.log(errorThrown);
                    ConnectionChanged();
                }
            });
    }
}

function YouWon(myID) {
    WonCount++;
    $.ajax({
        url: '/Accounts/UpdateStatsWeb',
        type: 'POST',
        data: { 'Counts': WonCount, 'Type': "DoubleWin" },
        success: function (response) {
            if (response == "True") {
                WonCount = 0;
            }
            else if (response == "False") {
                WonCount++;
            }
            else {
                ConnectionChanged();
            }
        },
        error: function (errorThrown) {
            console.log(errorThrown);
            ConnectionChanged();
        }
    });

    $.ajax(
        {
            url: '/HomeM/Delete',
            type: 'POST',
            data: { 'id': myID },
            success: function (response) {
                if (response == 'Deleted') {
                    $('#myModalLabel').text("Yay! We Won!");
                    $('#myModalbodyText').text("You played like a champ.");
                    $('#myModal').modal('show');
                    $('#HideRow').hide();
                    $('#ShowRow').show();
                    $('#ShowButton').show();
                }
                else {
                    YouWon(myID);
                }
            },
            error: function (errorThrown) {
                console.log(errorThrown);
                ConnectionChanged();
            }
        });

}

function YouLost(myID) {

    LostCount++;
    $.ajax({
        url: '/Accounts/UpdateStatsWeb',
        type: 'POST',
        data: { 'Counts': LostCount, 'Type': "DoubleLoss" },
        success: function (response) {
            if (response == "True") {
                LostCount = 0;
            }
            else if (response == "False") {
                LostCount++;
            }
            else {
                ConnectionChanged();
            }
        },
        error: function (errorThrown) {
            console.log(errorThrown);
            ConnectionChanged();
        }
    });

    $.ajax(
        {
            url: '/HomeM/Delete',
            type: 'POST',
            data: { 'id': myID },
            success: function (response) {
                if (response == 'Deleted') {
                    $('#myModalLabel').text("Oh! We Lost!");
                    $('#myModalbodyText').text("It is okay. We will win next time for sure.");
                    $('#myModal').modal('show');
                    $('#HideRow').hide();
                    $('#ShowRow').show();
                    $('#ShowButton').show();
                }
                else {
                    YouLost(myID);
                }
            },
            error: function (errorThrown) {
                console.log(errorThrown);
                ConnectionChanged();
            }
        });

}

function GameTied(myID) {

    TieCount++;
    $.ajax({
        url: '/Accounts/UpdateStatsWeb',
        type: 'POST',
        data: { 'Counts': TieCount, 'Type': "DoubleTie" },
        success: function (response) {
            if (response == "True") {
                TieCount = 0;
            }
            else if (response == "False") {
                TieCount++;
            }
            else {
                ConnectionChanged();
            }
        },
        error: function (errorThrown) {
            console.log(errorThrown);
            ConnectionChanged();
        }
    });

    $.ajax(
        {
            url: '/HomeM/Delete',
            type: 'POST',
            data: { 'id': myID },
            success: function (response) {
                if (response == 'Deleted') {
                    $('#myModalLabel').text("It's a Tie!!");
                    $('#myModalbodyText').text("Tie calls for a tie breaker round.");
                    $('#myModal').modal('show');
                    $('#HideRow').hide();
                    $('#ShowRow').show();
                    $('#ShowButton').show();
                }
                else {
                    GameTied(myID);
                }
            },
            error: function (errorThrown) {
                console.log(errorThrown);
                ConnectionChanged();
            }
        });

}

function OpponentNotExist() {
    $('#myModalLabel').text("Connection Lost");
    $('#myModalbodyText').text("Connection with server is lost.");
    $('#myModal').modal('show');
    $('#HideRow').hide();
    $('#ShowRow').show();
}


function OpponentQuit(myID) {
    $.ajax(
        {
            url: '/HomeM/DeleteQuit',
            type: 'POST',
            data: { 'id': myID },
            success: function (response) {
                if (response == 'Deleted') {
                    $('#myModalLabel').text("You Won!!!");
                    $('#myModalbodyText').text("Your Opponent Quit The Game.");
                    $('#myModal').modal('show');
                    $('#HideRow').hide();
                    $('#ShowRow').show();
                }
                else {
                    quitButtonClicked();
                }
            },
            error: function (errorThrown) {
                console.log(errorThrown);
                ConnectionChanged();
            }
        });
}

function ConnectionChanged() {
    $('#myModalLabel').text("Connection Lost");
    $('#myModalbodyText').text("Connection with server is lost.");
    $('#myModal').modal('show');
    $('#HideRow').hide();
    $('#ShowRow').show();
    $('#ShowButton').show();
    $('#HideUltimate').hide();
}