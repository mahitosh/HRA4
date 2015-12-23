// JavaScript Document
$(function() {
//show datepicker
$('.datepicker').datepicker();

//hide datepicker calendar
$('#dob-date').datepicker()//  id with "dob-date" will pop up a datepicker
	.on('changeDate', function(){ // when the datechanges
		$('#dob-date').datepicker('hide');      // hide the datepicker
});
$('#appt-date').datepicker()//  id with "dob-date" will pop up a datepicker
	.on('changeDate', function(){ // when the datechanges
		$('#appt-date').datepicker('hide');      // hide the datepicker
});

//show tooltip
$('[data-toggle="tooltip"]').tooltip();			
}); //function end here

 
		
		
//hide add appointment link
function hideAppt(){
	$(".add-appointment").hide();
}

//show add appointment link
function showAppt(){
	$(".add-appointment").show();
}
			