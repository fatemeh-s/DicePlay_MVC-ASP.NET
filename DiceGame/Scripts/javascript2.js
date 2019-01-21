turn = 0 ;
current0 = 0;
current1 = 0;
total0 = 0;
total1 = 0;
finalScore = 100;

function finalS() {
  finalScore = document.getElementById("score").value;
}
//choose two random number
function roll_dice() {
  var roll_num = Math.floor(Math.random() * 6) + 1;
  var roll_num2 = Math.floor(Math.random() * 6) + 1;
  switch (roll_num){
    case 1 :
      document.getElementById("imgId").src = "dice-1.png";
      break;
    case 2 :
      document.getElementById("imgId").src = "dice-2.png";
      break;
    case 3 :
      document.getElementById("imgId").src = "dice-3.png";
      break;
    case 4 :
      document.getElementById("imgId").src = "dice-4.png";
      break;
    case 5 :
      document.getElementById("imgId").src = "dice-5.png";
      break;
    case 6 :
      document.getElementById("imgId").src = "dice-6.png";
      break;
    default:
      window.alert("default");
  }
  switch (roll_num2){
    case 1 :
      document.getElementById("imgId2").src = "dice-1.png";
      break;
    case 2 :
      document.getElementById("imgId2").src = "dice-2.png";
      break;
    case 3 :
      document.getElementById("imgId2").src = "dice-3.png";
      break;
    case 4 :
      document.getElementById("imgId2").src = "dice-4.png";
      break;
    case 5 :
      document.getElementById("imgId2").src = "dice-5.png";
      break;
    case 6 :
      document.getElementById("imgId2").src = "dice-6.png";
      break;
    default:
      window.alert("default");
  }
  if (roll_num == 1 || roll_num2==1){
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
      current0 += roll_num2;
      document.getElementById("current0").innerHTML = current0;
    }else if (turn == 1){
      current1 += roll_num;
      current1 += roll_num2;
      document.getElementById("current1").innerHTML = current1;
    }
  }
}

function hold() {
  finalScore = document.getElementById("score").value;
  if (turn == 0){
    total0 += current0;
    document.getElementById("tp0").innerHTML = total0;
    current0 = 0;
    document.getElementById("current0").innerHTML = 0;
    turn = 1;
    if(total0 >= finalScore){
      document.getElementById("p0").innerHTML = "WINNER"
    }
    document.getElementById("right_box").style.backgroundColor = "rgb(244, 248, 247)";
    document.getElementById("left_box").style.backgroundColor = "white";
    document.getElementById("turn0").style.display="none";
    document.getElementById("turn1").style.display="inline";
  } else if(turn == 1){
    total1 += current1;
    document.getElementById("tp1").innerHTML = total1;
    current1 = 0 ;
    document.getElementById("current1").innerHTML = 0;
    turn = 0;
    if(total1 >= finalScore){
      document.getElementById("p1").innerHTML = "WINNER"
    }
    document.getElementById("right_box").style.backgroundColor = "white";
    document.getElementById("left_box").style.backgroundColor = "rgb(244, 248, 247)";
    document.getElementById("turn0").style.display="inline";
    document.getElementById("turn1").style.display="none";
  }
}
