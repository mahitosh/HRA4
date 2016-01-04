// JavaScript Document
$(function () {


    //hide datepicker calendar
    $('#dob-date').datepicker()//  id with "dob-date" will pop up a datepicker
        .on('changeDate', function () { // when the datechanges
            $('#dob-date').datepicker('hide');      // hide the datepicker
        });
    if ($("#chkbox").is(":checked")) {
        $('#appt-date').datepicker()//  id with "dob-date" will pop up a datepicker
.on('changeDate', function () { // when the datechanges
    $('#appt-date').datepicker('hide');      // hide the datepicker
});
    }


    //show tooltip
    $('[data-toggle="tooltip"]').tooltip();
}); //function end here


//dashboard popup
$(window).load(function () {
    $('#dashboardmodal').modal('show');
});


//hide add appointment link
function hideAppt() {
    $(".add-appointment").hide();
}

//show add appointment link
function showAppt() {
    $(".add-appointment").show();
}

//for quick edit		
$(document).ready(function () {
    $(".editmenu").hide();
});
$("#btnSaveQuick").click(function () {
    $(".editmenu").slideUp(100);
});

$(".btnClear").click(function () {
    $(".editmenu").slideUp(100);
});


