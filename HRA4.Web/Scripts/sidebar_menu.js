// Create sidebar

$("#menu-toggle").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });
     $("#menu-toggle-2").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled-2");
		$("i",this).toggleClass("fa-chevron-circle-left fa-chevron-circle-right");
        $('#menu ul').hide();
		
			if($("#wrapper").hasClass("toggled-2"))
			{
				
				$("ul#menu").off("click");
				$("#menu-toggle-2").css("left","32px");
			}
			else
			{
				$("ul#menu").on("click", "li", function(e){
					e.stopPropagation();	
					$(".sidebar-nav-child:visible", $(this).siblings()).slideUp("fast");
					$(".sidebar-nav-child", this).slideDown();
				});
								
			
				$("#menu-toggle-2").css("left","261px");
			}
		
		
    });
 
 
 $("ul#menu").on("click", "li", function(e){
    e.stopPropagation();	
    $(".sidebar-nav-child:visible", $(this).siblings()).slideUp("fast");
    $(".sidebar-nav-child", this).slideDown();
});


