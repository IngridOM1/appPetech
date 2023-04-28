$(document).ready(function(){
    const listOfBuyButtons = document.querySelectorAll('.productComponentContainer_btn');
    //alert('hole')
    $(".productComponentContainer_btn").click(function() {
        $("#popup-container").show();
      });
    $("#popup-close").click(function(){
        $("#popup-container").hide();
    });
})
