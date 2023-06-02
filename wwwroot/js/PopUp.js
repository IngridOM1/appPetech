$(document).ready(function(){
    const listOfBuyButtons = document.querySelectorAll('.productComponentContainer_btn');
    $("#sidebar").hide();

    $("#sideBar-open").click(()=>{
        $("#sidebar").show();
    })

    $(".productComponentContainer_btn").click(function() {
        $("#popup-container").show();
      });
      
    $("#popup-close").click(function(){
        $("#popup-container").hide();
    });

    $("#sidebar-close").click(function() {
        $("#sidebar").hide();
      });
})



