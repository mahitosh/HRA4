// Create sidebar

$("#menu-toggle").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });
     $("#menu-toggle-2").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled-2");
        $('#menu ul').hide();
		
			if($("#wrapper").hasClass("toggled-2"))
			{
				
				$("#menu-toggle-2").css("left","32px");
			}
			else
			{
				
				$("#menu-toggle-2").css("left","261px");
			}
		
		
    });
 
 
 $("ul#menu").on("click", "li", function(e){
    e.stopPropagation();	
    $(".sidebar-nav-child:visible", $(this).siblings()).slideUp("fast");
    $(".sidebar-nav-child", this).slideDown();
});

$(document).on("click", function(){
    $(".sidebar-nav-child:visible").slideUp();	
});


    // function initMenu() {
//      $('#menu ul').hide();
//      $('#menu ul').children('.current').parent().show();
//      //$('#menu ul:first').show();
//      $('#menu li a').click(
//        function() {
//          var checkElement = $(this).next();
//          if((checkElement.is('ul')) && (checkElement.is(':visible'))) {
//            return false;
//            }
//          if((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
//            $('#menu ul:visible').slideUp('normal');
//            checkElement.slideDown('normal');
//            return false;
//            }
//          }
//        );
//      }
//    $(document).ready(function() {initMenu();});