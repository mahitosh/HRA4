// JavaScript Document
$(function() {

$(document).ready(function() {
    $(".add-MRI").hide();
	$("ul#menu").off("click");
});

    //hide datepicker calendar
$('#dob-date, #edit-app-date').datepicker()//  id with "dob-date" will pop up a datepicker
	.on('changeDate', function(){ // when the datechanges
            $('#dob-date').datepicker('hide');      // hide the datepicker
        });
     $('#todays-date').datepicker()//  id with "dob-date" will pop up a datepicker
        .on('changeDate', function () { // when the datechanges
            $('#dob-date').datepicker('hide');      // hide the datepicker
        });

    if ($("#chkbox").is(":checked")) {
        $('#appt-date').datepicker()//  id with "dob-date" will pop up a datepicker
.on('changeDate', function() { // when the datechanges
    $('#appt-date').datepicker('hide');      // hide the datepicker
});
    }






    //show tooltip
    $('[data-toggle="tooltip"]').tooltip();
}); //function end here



//dashboard popup
 $(window).load(function(){
    $('#dashboardmodal').modal('show');
});






//for quick edit		
$(document).ready(function () {
    $(".editmenu").hide();
});

$("#btnSaveQuick").click(function () {




    $(".editmenu").slideUp(100);
});





//MRN edit content
	$("#quick_add_MRNdiv").hide();
	$("#edit-MRN").click( function() {
		$("#quick_add_MRNdiv").toggle(300);	
	});
	$(".close_toggleDiv").click( function(){
		$("#quick_add_MRNdiv").hide(300);
	});
	
//timepicker
	$('#timepicker1').timepicker();
	
	//multiselect dropdown
	$('#race-multiselect').multiselect();
	$("#colo-race-multiselect").multiselect();
    
	$('.modal').modal({backdrop: 'static', keyboard: false})
   



//timepicker
 $(function() {
   $('#timepicker1').timepicker();
 });
 


//for manage user add clinic		
$(document).ready (function (){
	$("#expand-div-content").hide();
	
	$("#btnSaveSlide").click(function(){
			$("#expand-div-content, #change-password-content").slideUp(200);
			$("#change-password-content").hide();
	});
	
	$("#btnCancelSlide").click(function(){
			$("#expand-div-content, #change-password-content").slideUp(200);
			$("#change-password-content").hide();
	});
	
	$(".expand-div").click(function(){
			$("#expand-div-content, #change-password-content").slideDown(200);
			$("#change-password-content").hide();
	});
});

//for manage user change password		
$(document).ready (function (){
	$("#change-password-content").hide();
	
	$("#btnSavePassword").click(function(){
			$("#change-password-content").slideUp(200);
	});
	
	$("#btnCancelPassword").click(function(){
			$("#change-password-content").slideUp(200);
	});
	
	$(".change-password").click(function(){
			$("#change-password-content").slideDown(200);
	});
});

//Collapsible panel
$('.collapse').on('shown.bs.collapse', function(){
$(this).parent().find(".glyphicon-plus-sign").removeClass("glyphicon-plus-sign").addClass("glyphicon-minus-sign");
}).on('hidden.bs.collapse', function(){
$(this).parent().find(".glyphicon-minus-sign").removeClass("glyphicon-minus-sign").addClass("glyphicon-plus-sign");
});

//collapsible panel active
$('.BreastCancerAccordion .panel-heading').on('click', function () {   
    $('.BreastCancerAccordion .panel-heading').removeClass('activeAccordion');
    $(this).addClass('activeAccordion');
 });


	
//add tooltip on ellipse
$('td, th').bind('mouseenter', function () {
		  var $this = $(this);
		  
		  if (this.offsetWidth < this.scrollWidth && !$this.attr('title')) {
			  $this.attr('title', $this.text());
			
		  }
	  });
	