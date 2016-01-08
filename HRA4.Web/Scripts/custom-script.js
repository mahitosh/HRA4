// JavaScript Document
$(function() {

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
		
		
//hide add appointment link
function hideAppt(){
	$(".add-appointment").hide();
}

//show add appointment link
function showAppt(){
	$(".add-appointment").show();
}
		
//for quick edit		
$(document).ready(function () {
    $(".editmenu").hide();
});
    $("#btnSaveQuick").click(function () {
        $(".editmenu").slideUp(100);
    });

   


//timepicker
 $(function() {
   $('#timepicker1').timepicker();
 });
 
 //schedule grid more deatils content
$(document).ready(function(){
$(".schedule-more-detail-content").hide();
    $(".schedule-more-detail").click(function(){
        $(".schedule-more-detail-content").toggle(200);
		$("i",this).toggleClass("fa-plus-circle fa-minus-circle");
    });
});
 
//for quick edit		
/*$(document).ready (function (){
	$("#quick_edit_div").hide();
	
	$("#btnSaveQuick").click(function(){
			$("#quick_edit_div").slideUp(100);
	});
	
	$("#btnClearQuick").click(function(){
			$("#quick_edit_div").slideUp(100);
	});
	
	$("#quick_edit").click(function(){
			$("#quick_edit_div").slideDown(100);
	});
});*/