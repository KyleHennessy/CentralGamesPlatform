function checkNumber() {

    var attempt = 0;

    function generateRandomAnswer() {

        var num = (Math.floor(Math.random() * 8900) + 1000).toString();

        return num;
    }


    function validateInput(str) {
        if (str.length == 4) {
            return true;
        }
        else {
            return false;
        }


    }


    var userInput = document.getElementById("user-input").value;

    var answer = document.getElementById("answer").value;
    var attempt = document.getElementById("attempts").value

    var msg = document.getElementById("msg");
    var results = document.getElementById("results");

    console.log(userInput);




    if (!attempt) {
        attempt = 0;
    }

    if (!validateInput(userInput)) {

        msg.innerHTML = "<p class = 'msg-danger'> The Number is Invalid !  It must be 4-digit </p>";

        return
    }
    else {

        msg.innerHTML = "";
        attempt++;
        document.getElementById("attempts").value = attempt;
    }



    if (!answer) {
        var answer = generateRandomAnswer();
        document.getElementById("answer").value = answer;
    }



    var correctDigit = 0;

    var html = '<tr><td>' + userInput + '</td><td>'


    for (let i = 0; i < userInput.length; i++) {
        if (userInput[i] == answer[i]) {
            html = html + '<i class="fa fa-check text-success" style="padding:3px;"> </i>'
            correctDigit++;

        }
        else if (answer.indexOf(userInput[i]) > -1) {
            html = html + '<i class="fa fa-exchange text-warning" style="padding:3px;" ></i>'

        }
        else {
            html = html + '<i class="fa fa-times text-danger" style="padding:3px;"></i>'
        }


    }

    html = html + '</td></tr>';


    results.innerHTML += html;

    if (correctDigit == userInput.length) {
        msg.innerHTML = "<p class = 'msg-success'>  Hurray! You Made It! </p>";
        document.getElementById("btn-guess").style = "display:none;";
        document.getElementById("btn-replay").style = "display:block;";
    }

    else if (attempt >= 10) {

        msg.innerHTML = "<p class = 'msg-danger'> You Lost, Please try Again!</p>";
        document.getElementById('lost-msg').innerHTML = "<p>Correct Answer Was: </p>" + answer;
        document.getElementById("btn-guess").style = "display:none;";
        document.getElementById("btn-replay").style = "display:block;";


    }
    else {
        msg.innerHTML = "<p class = 'msg-retry'> Incorrect Guess ,Keep Guessing ! </p>";


    }






}