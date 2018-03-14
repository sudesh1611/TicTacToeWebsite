var filled = 0;
var box;
var currentPlayer;
var won = 0;
var lost = 0;
var tied = 0;
var wondiv = 0;
var lostdiv = 0;
var tieddiv = 0;

$(document).ready(function () {

    $('#myModal').modal('hide');
    $('#ShowRow').hide();
    $('#HideRow').show();
    $('#won').html("<strong>Won: </strong>" + won);
    $('#lost').html("<strong>Lost: </strong>" + lost);
    $('#tied').html("<strong>Tied: </strong>" + tied);
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
    $('#won').html("<strong>Won: </strong>" + won);
    $('#lost').html("<strong>Lost: </strong>" + lost);
    $('#tied').html("<strong>Tied: </strong>" + tied);
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
            var a = AlphaBeta(-100, 100, true);
            var index = a[0];
            box[index] = 'X';
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
    $('#won').html("<strong>Won: </strong>" + won);
    $('#lost').html("<strong>Lost: </strong>" + lost);
    $('#tied').html("<strong>Tied: </strong>" + tied);
    $('#myModalLabel').text("Yay! We Won!");
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

    $('#won').html("<strong>Won: </strong>" + won);
    $('#lost').html("<strong>Lost: </strong>" + lost);
    $('#tied').html("<strong>Tied: </strong>" + tied);
    $('#myModalLabel').text("Oh! We Lost!");
    $('#myModalbodyText').text("It is okay. We will win next time for sure.");
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

    $('#won').html("<strong>Won: </strong>" + won);
    $('#lost').html("<strong>Lost: </strong>" + lost);
    $('#tied').html("<strong>Tied: </strong>" + tied);
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


function AlphaBeta(alpha, beta, player) {

    var x = gameOver();
    var a = [0, 1];

    if (x != -1) {
        a[0] = -1;
        a[1] = x;
        return a;
    }

    if (player == true) {
        var mx = -100;
        for (var i = 1; i < 10; i++) {
            if (box[i] == ' ') {
                box[i] = 'X';
                filled++;
                a = AlphaBeta(alpha, beta, !(player));
                if (mx < a[1]) {
                    mx = a[1];
                    var index = i;
                    alpha = Math.max(alpha, mx);

                    if (beta <= alpha) {
                        box[i] = ' ';
                        filled--;
                        break;
                    }
                }
                box[i] = ' ';
                filled--;
            }
        }
        a[0] = index;
        a[1] = mx;
        return a;
    }
    else {
        var mn = 100;
        for (var i = 1; i < 10; i++) {
            if (box[i] == ' ') {
                box[i] = 'O';
                filled++;
                a = AlphaBeta(alpha, beta, !(player));
                if (mn > a[1]) {
                    mn = a[1];
                    var index = i;
                    beta = Math.min(beta, mn);
                    if (beta <= alpha) {
                        box[i] = ' ';
                        filled--;
                        break;
                    }
                }
                box[i] = ' ';
                filled--;
            }
        }
        a[0] = index;
        a[1] = mn;
        return a;
    }
}
