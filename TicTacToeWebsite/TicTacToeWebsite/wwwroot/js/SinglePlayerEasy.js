var filled = 0;
var box;
var currentPlayer;
var won = 0;
var lost = 0;
var tied = 0;
var wondiv = 0;
var lostdiv = 0;
var tieddiv = 0;
var LostCount = 0;
var WonCount = 0;
var TieCount = 0;

$(document).ready(function () {

    $('#myModal').modal('hide');
    $('#ShowRow').hide();
    $('#HideRow').show();
    $('#turnButton').css('background-color', 'cornflowerblue');
    $('#turnButton').css('color', '#FFFFFF');
    $('#turnButton').text('Your Turn');
    currentPlayer = 'O';
    box = [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '];
    printArena(box);

});

function resetGame() {
    filled = 0;
    $('#myModal').modal('hide');
    $('#ShowRow').hide();
    $('#HideRow').show();
    $('#won').html( won);
    $('#lost').html( lost);
    $('#tied').html( tied);
    $('#turnButton').css('background-color', 'cornflowerblue');
    $('#turnButton').css('color', '#FFFFFF');
    $('#turnButton').text('Your Turn');
    currentPlayer = 'O';
    box = [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '];
    printArena(box);
}


function printArena(symbolPresent) {
    for (var i = 1; i <= 9; i++) {
        switch (i) {
            case 1:
                if (symbolPresent[i] == ' ') {
                    $('#zeroth').attr('src', '../images/Blank.jpg');
                }
                else if (symbolPresent[i] == 'O') {
                    $('#zeroth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#zeroth').attr('src', '../images/Cross.jpg');
                }
                break;
            case 2:
                if (symbolPresent[i] == ' ') {
                    $('#first').attr('src', '../images/Blank.jpg');
                }
                else if (symbolPresent[i] == 'O') {
                    $('#first').attr('src', '../images/Circle.png');
                }
                else {
                    $('#first').attr('src', '../images/Cross.jpg');
                }
                break;
            case 3:
                if (symbolPresent[i] == ' ') {
                    $('#second').attr('src', '../images/Blank.jpg');
                }
                else if (symbolPresent[i] == 'O') {
                    $('#second').attr('src', '../images/Circle.png');
                }
                else {
                    $('#second').attr('src', '../images/Cross.jpg');
                }
                break;
            case 4:
                if (symbolPresent[i] == ' ') {
                    $('#third').attr('src', '../images/Blank.jpg');
                }
                else if (symbolPresent[i] == 'O') {
                    $('#third').attr('src', '../images/Circle.png');
                }
                else {
                    $('#third').attr('src', '../images/Cross.jpg');
                }
                break;
            case 5:
                if (symbolPresent[i] == ' ') {
                    $('#fourth').attr('src', '../images/Blank.jpg');
                }
                else if (symbolPresent[i] == 'O') {
                    $('#fourth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#fourth').attr('src', '../images/Cross.jpg');
                }
                break;
            case 6:
                if (symbolPresent[i] == ' ') {
                    $('#fifth').attr('src', '../images/Blank.jpg');
                }
                else if (symbolPresent[i] == 'O') {
                    $('#fifth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#fifth').attr('src', '../images/Cross.jpg');
                }
                break;
            case 7:
                if (symbolPresent[i] == ' ') {
                    $('#sixth').attr('src', '../images/Blank.jpg');
                }
                else if (symbolPresent[i] == 'O') {
                    $('#sixth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#sixth').attr('src', '../images/Cross.jpg');
                }
                break;
            case 8:
                if (symbolPresent[i] == ' ') {
                    $('#seventh').attr('src', '../images/Blank.jpg');
                }
                else if (symbolPresent[i] == 'O') {
                    $('#seventh').attr('src', '../images/Circle.png');
                }
                else {
                    $('#seventh').attr('src', '../images/Cross.jpg');
                }
                break;
            case 9:
                if (symbolPresent[i] == ' ') {
                    $('#eighth').attr('src', '../images/Blank.jpg');
                }
                else if (symbolPresent[i] == 'O') {
                    $('#eighth').attr('src', '../images/Circle.png');
                }
                else {
                    $('#eighth').attr('src', '../images/Cross.jpg');
                }
                break;
        }
    }
}


function clicked(index) {


    if (currentPlayer == 'O') {

        currentPlayer = 'X';
        if (box[index] != ' ') {
            currentPlayer = 'O';
            return;
        }
        box[index] = 'O';
        filled++;
        printArena(box);
        $('#turnButton').css('background-color', '#FFFFFF');
        $('#turnButton').css('color', 'cornflowerblue');
        $('#turnButton').text("Opponent's Turn");
        var temp = gameOver();
        if (filled >= 9 || temp != -1) {
            displayResult();
            return;
        }
        computerTurn();
    }
}

function computerTurn() {
    if (currentPlayer == 'X') {
        setTimeout(function () {
            for (var i = 1; i < 10; i++) {
                if (box[i] == ' ') {
                    box[i] = 'X';
                    var x = gameOver();
                    if (x == 10) {
                        box[i] = 'X';
                        printArena(box);
                        displayResult();
                        return;
                    }
                    box[i] = ' ';
                }
            }

            for (var i = 1; i < 10; i++) {
                if (box[i] == ' ') {
                    box[i] = 'O';
                    var x = gameOver();
                    if (x == -10) {
                        box[i] = 'X';
                        filled++;
                        printArena(box);
                        if (filled >= 9) {
                            displayResult();
                            return;
                        }
                        $('#turnButton').css('background-color', 'cornflowerblue');
                        $('#turnButton').css('color', '#FFFFFF');
                        $('#turnButton').text('Your Turn');
                        currentPlayer = 'O';
                        return;
                    }
                    box[i] = ' ';
                }
            }

            {
                if (box[1]=='X' && box[2]==' ' && box[3]==' ') {
                    box[2] = 'X';
                }
                else if (box[1] == 'X' && box[4] == ' ' && box[7] == ' ') {
                    box[4] = 'X';
                }
                else if (box[1] == 'X' && box[5] == ' ' && box[9] == ' ') {
                    box[5] = 'X';
                }
                else if (box[2] == 'X' && box[1] == ' ' && box[3] == ' ') {
                    box[1] = 'X';
                }
                else if (box[2] == 'X' && box[5] == ' ' && box[8] == ' ') {
                    box[5] = 'X';
                }
                else if (box[3] == 'X' && box[1] == ' ' && box[2] == ' ') {
                    box[1] = 'X';
                }
                else if (box[3] == 'X' && box[5] == ' ' && box[7] == ' ') {
                    box[5] = 'X';
                }
                else if (box[3] == 'X' && box[6] == ' ' && box[9] == ' ') {
                    box[6] = 'X';
                }
                else if (box[4] == 'X' && box[1] == ' ' && box[7] == ' ') {
                    box[1] = 'X';
                }
                else if (box[4] == 'X' && box[5] == ' ' && box[6] == ' ') {
                    box[5] = 'X';
                }
                else if (box[5] == 'X' && box[4] == ' ' && box[6] == ' ') {
                    box[4] = 'X';
                }
                else if (box[5] == 'X' && box[2] == ' ' && box[8] == ' ') {
                    box[2] = 'X';
                }
                else if (box[5] == 'X' && box[1] == ' ' && box[9] == ' ') {
                    box[1] = 'X';
                }
                else if (box[5] == 'X' && box[3] == ' ' && box[7] == ' ') {
                    box[3] = 'X';
                }
                else if (box[6] == 'X' && box[4] == ' ' && box[5] == ' ') {
                    box[4] = 'X';
                }
                else if (box[6] == 'X' && box[3] == ' ' && box[9] == ' ') {
                    box[3] = 'X';
                }
                else if (box[7] == 'X' && box[8] == ' ' && box[9] == ' ') {
                    box[8] = 'X';
                }
                else if (box[7] == 'X' && box[1] == ' ' && box[4] == ' ') {
                    box[1] = 'X';
                }
                else if (box[7] == 'X' && box[5] == ' ' && box[3] == ' ') {
                    box[3] = 'X';
                }
                else if (box[8] == 'X' && box[2] == ' ' && box[5] == ' ') {
                    box[2] = 'X';
                }
                else if (box[8] == 'X' && box[7] == ' ' && box[9] == ' ') {
                    box[7] = 'X';
                }
                else if (box[9] == 'X' && box[7] == ' ' && box[8] == ' ') {
                    box[7] = 'X';
                }
                else if (box[9] == 'X' && box[1] == ' ' && box[5] == ' ') {
                    box[1] = 'X';
                }
                else if (box[9] == 'X' && box[3] == ' ' && box[6] == ' ') {
                    box[3] = 'X';
                }
                else {
                    for (var i = 1; i < 10; i++) {
                        if (box[i] == ' ') {
                            box[i] = 'X';
                            break;
                        }
                    }
                }
            }
            filled++;
            printArena(box);
            var temp = gameOver();
            if (filled >= 9 || temp != -1) {
                displayResult();
                return;
            }
            $('#turnButton').css('background-color', 'cornflowerblue');
            $('#turnButton').css('color', '#FFFFFF');
            $('#turnButton').text('Your Turn');
            currentPlayer = 'O';
            return;
        }, 2000);
    }
}


function displayResult() {
    won = $('#won').text();
    lost = $('#lost').text();
    tied = $('#tied').text();
    wondiv = won;
    lostdiv = lost;
    tieddiv = tied;
    var x = gameOver();
    if (x == 10) {
        computerWon();
    } else if (x == -10) {
        playerWon();
    } else {
        draw();
    }
}

function playerWon() {
    won++;
    wondiv++;
    WonCount++;
    $.ajax({
        url: '/Accounts/UpdateStatsWeb',
        type: 'POST',
        data: { 'Counts': WonCount, 'Type': "SingleEasyWin" },
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

    $('#won').html( won);
    $('#lost').html(lost);
    $('#tied').html( tied);
    $('#myModalLabel').text("Yay! We Won.");
    $('#myModalbodyText').text("You played like a champ.");
    $('#myModal').modal('show');
    $('#HideRow').hide();
    $('#ShowRow').show();
    $('#wond').html("<strong>Won: </strong>" + wondiv);
    $('#lostd').html("<strong>Lost: </strong>" + lostdiv);
    $('#tiedd').html("<strong>Tied: </strong>" + tieddiv);
}

function computerWon() {
    lost++;
    lostdiv++;
    LostCount++;
        $.ajax({
            url: '/Accounts/UpdateStatsWeb',
            type: 'POST',
            data: { 'Counts': LostCount, 'Type':"SingleEasyLoss" },
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

    $('#won').html(won);
    $('#lost').html( lost);
    $('#tied').html( tied);
    $('#myModalLabel').text("Oh! We Lost.");
    $('#myModalbodyText').text("It's okay. Give it another go.");
    $('#myModal').modal('show');
    $('#HideRow').hide();
    $('#ShowRow').show();
    $('#wond').html("<strong>Won: </strong>" + wondiv);
    $('#lostd').html("<strong>Lost: </strong>" + lostdiv);
    $('#tiedd').html("<strong>Tied: </strong>" + tieddiv);
}

function draw() {
    tied++;
    tieddiv++;
    TieCount++;
    $.ajax({
        url: '/Accounts/UpdateStatsWeb',
        type: 'POST',
        data: { 'Counts': TieCount, 'Type': "SingleEasyTie" },
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

    $('#won').html( won);
    $('#lost').html( lost);
    $('#tied').html( tied);
    $('#myModalLabel').text("It's a Tie!!");
    $('#myModalbodyText').text("Tie calls for a tie breaker round.");
    $('#myModal').modal('show');
    $('#HideRow').hide();
    $('#ShowRow').show();
    $('#wond').html("<strong>Won: </strong>" + wondiv);
    $('#lostd').html("<strong>Lost: </strong>" + lostdiv);
    $('#tiedd').html("<strong>Tied: </strong>" + tieddiv);
}

function gameOver() {

    if (box[1] == box[2] && box[2] == box[3] && box[2] != ' ')
        return box[2] == 'X' ? 10 : -10;

    if (box[4] == box[5] && box[5] == box[6] && box[4] != ' ')
        return box[4] == 'X' ? 10 : -10;

    if (box[7] == box[8] && box[8] == box[9] && box[8] != ' ')
        return box[8] == 'X' ? 10 : -10;

    if (box[1] == box[4] && box[4] == box[7] && box[1] != ' ')
        return box[1] == 'X' ? 10 : -10;

    if (box[2] == box[5] && box[5] == box[8] && box[2] != ' ')
        return box[2] == 'X' ? 10 : -10;

    if (box[3] == box[6] && box[6] == box[9] && box[3] != ' ')
        return box[3] == 'X' ? 10 : -10;

    if (box[1] == box[5] && box[5] == box[9] && box[1] != ' ')
        return box[1] == 'X' ? 10 : -10;

    if (box[3] == box[5] && box[5] == box[7] && box[3] != ' ')
        return box[3] == 'X' ? 10 : -10;

    if (filled == 9)
        return 0;

    return -1;
}


function ConnectionChanged() {
    $('#myModalLabel').text("Connection Lost");
    $('#myModalbodyText').text("Connection with server is lost.");
    $('#myModal').modal('show');
    $('#HideRow').hide();
    $('#ShowRow').show();
    $('#ShowButton').show();
}