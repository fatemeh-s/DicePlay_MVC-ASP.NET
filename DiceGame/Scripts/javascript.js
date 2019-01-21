
turn = 0;//if turn=0 player0 if turn=1 player1
current0 = 0;//current of player0
current1 = 0;//current of player1
total0 = 0;//total of player0
total1 = 0;//total of player1
finalScore = 100;//score entered by user defalt = 100
last_score0 = 0;//last roll_dice of player0
last_score1 = 0;




$("#roll").click(function () {
    //alert(someSessionVariable);
        roll_dice();
    });
//gets final score value for endig game

function finalS() {
  finalScore = document.getElementById("score").value;
}

//choose a random number between 1 to 6
function roll_dice() {
  var roll_num = Math.floor(Math.random() * 6) + 1;
  switch (roll_num){
    case 1 :
      document.getElementById("imgId").src = "../Content/dice-1.png";
      last_score0 = 1;
      break;
    case 2 :
      document.getElementById("imgId").src = "../Content/dice-2.png";
      last_score0 = 2;
      break;
    case 3 :
      document.getElementById("imgId").src = "../Content/dice-3.png";
      last_score0 = 3;
      break;
    case 4 :
          document.getElementById("imgId").src = "../Content/dice-4.png";
      last_score0 = 4;
      break;
    case 5 :
          document.getElementById("imgId").src = "../Content/dice-5.png";
      last_score0 = 5;
      break;
    case 6 :
          document.getElementById("imgId").src = "../Content/dice-6.png";
      last_score0 = 6;
      last_score1 = 6;
      break;
    default:
      window.alert("default");
  }

  // for checking roll_dice=1 for change turn
  if (roll_num == 1){
    if(turn == 0){
      turn = 1;
      current0 = 0;
      document.getElementById("current0").innerHTML = 0;
      document.getElementById("right_box").style.backgroundColor = "rgb(244, 248, 247)";
      document.getElementById("left_box").style.backgroundColor = "white";
      document.getElementById("turn0").style.display="none";
      document.getElementById("turn1").style.display="inline";
    }else if (turn == 1){
      turn = 0;
      current1 = 0;
      document.getElementById("current1").innerHTML = 0;
      document.getElementById("right_box").style.backgroundColor = "white";
      document.getElementById("left_box").style.backgroundColor = "rgb(244, 248, 247)";
      document.getElementById("turn0").style.display="inline";
      document.getElementById("turn1").style.display="none";
    }
  }else{
    if(turn == 0){
      current0 += roll_num;
      document.getElementById("current0").innerHTML = current0;
    }else if (turn == 1){
      current1 += roll_num;
      document.getElementById("current1").innerHTML = current1;
    }
  }

  var roles = { current0: current0, current1: current1, total0: total0, total1: total1, Turn: turn };

  //alert(JSON.stringify(roles));
  jQuery.ajax({
      type: "POST",
      url: "updateGame",
      dataType: "json",
      contentType: "application/json; charset=utf-8",
      data: JSON.stringify(roles)
  });
}

// this function for save current in total score of each player
function hold() {
  finalScore = document.getElementById("score").value;
  if (turn == 0){
    total0 += current0;
    document.getElementById("tp0").innerHTML = total0;
    current0 = 0;
    document.getElementById("current0").innerHTML = 0;
    turn = 1;
    //for change color for showing turn
    document.getElementById("right_box").style.backgroundColor = "rgb(244, 248, 247)";
    document.getElementById("left_box").style.backgroundColor = "white";
    document.getElementById("turn0").style.display="none";
    document.getElementById("turn1").style.display="inline";
    if(total0 >= finalScore){
      document.getElementById("p0").innerHTML = "WINNER"
    }
  } else if(turn == 1){
    total1 += current1;
    document.getElementById("tp1").innerHTML = total1;
    current1 = 0 ;
    document.getElementById("current1").innerHTML = 0;
    turn = 0;
    document.getElementById("right_box").style.backgroundColor = "white";
    document.getElementById("left_box").style.backgroundColor = "rgb(244, 248, 247)";
    document.getElementById("turn0").style.display="inline";
    document.getElementById("turn1").style.display="none";
    if(total1 >= finalScore){
      document.getElementById("p1").innerHTML = "WINNER"
    }
  }

  var roles = { current0: current0, current1: current1, total0: total0, total1: total1, Turn: turn };
  jQuery.ajax({
      type: "POST",
      url: "updateGame",
      dataType: "json",
      contentType: "application/json; charset=utf-8",
      data: JSON.stringify(roles)
  });
}

//$(function () {
//    $(".roll_dice_button").click(function () {
//        var game = roll_dice();

//        // poor man's validation
//        if (game == null) {
//            alert("Specify a name please!");
//            return;
//        }

//        var json = $.toJSON(game);

//        $.ajax({
//            url: '/Game/GameIndex',
//            type: 'POST',
//            dataType: 'json',
//            data: json,
//            contentType: 'application/json; charset=utf-8',
//            //success: function (data) {
//            //    // get the result and do some magic with it
//            //    var message = data.Message;
//            //    $("#resultMessage").html(message);
//            //}
//        });
//    });
//});

//function getPerson() {
//    var name = $("#Name").val();
//    var age = $("#Age").val();

//    // poor man's validation
//    return (name == "") ? null : { Name: name, Age: age };
//}