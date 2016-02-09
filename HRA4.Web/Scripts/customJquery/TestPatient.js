//$("#dob-date").datepicker({ minDate: 0 });




$('#menu ul').hide();
$("ul#menu").off("click");

var txt = $('#ddlsurveys option:selected').text();
$("#ddlsurveystext").val(txt);

$("#ddlsurveys").change(function () {
    var txt = $('#ddlsurveys option:selected').text();
    $("#ddlsurveystext").val(txt);
});

function CheckAllBoxes() {
    $(".sid").each(function () {
        $(this).prop("checked", true);
    });
};


function getSelectedIds(){
    var apptids = '';
    $('.sid').each(function () {
        if ($(this).is(":checked")) {
            if (apptids == '')
                apptids = $(this).attr('id');
            else
                apptids = apptids + ',' + $(this).attr('id');
        }
    });
    return apptids;
}

function Applytablesorter() {
    $("#testpatientdiv").tablesorter(
         {
             headers: {
                 // assign the secound column (we start counting zero)
                 0: {
                     // disable it by setting the property sorter to false
                     sorter: false
                 }

             }
         }
    );

}
function DeleteTestPatients(url) {

    var apptids = getSelectedIds();
   
        $.ajax({
            url: url,
            data: { ids: apptids },
            dataType: 'html',
            success: function (data) {
                $("#confirm-xml").modal('hide');
                ShowNotification('Test Patients Deleted Successfully');
                $('#testpatientsgrid').html('');
                $('#testpatientsgrid').html(data);
                Applytablesorter();
            }
        });
 }

function MarkAsNotTestPatients(url) {

    var apptids = getSelectedIds();
  
    $.ajax({
        url: url,
        data: { ids: apptids },
        dataType: 'html',
        success: function (data) {
            $("#confirm-xml2").modal('hide');
            ShowNotification('Test Patients Maked As Not Test Patient');
            $('#testpatientsgrid').html('');
            $('#testpatientsgrid').html(data);
            Applytablesorter();

        }
    });
}

function RefreshTestPatients(url) {
    $.ajax({
        url: url,
        dataType: 'html',
        success: function (data) {
            $('#testpatientsgrid').html('');
            $('#testpatientsgrid').html(data);
            Applytablesorter();
        }
    });

}
function showNotification() {
    var date = $("#dob-date").val();
    var result = validateDate(date);
    if (result) {
        ShowNotification('Test Patients Created Successfully');
    }else
    {
        ShowErrorNotification('Invalid Date!');
        return false;
    }
}
function validateDate(testdate) {
    var date_regex = /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/;
    return date_regex.test(testdate);
}
function Appendtextonpopup(msg){
    var apptids = getSelectedIds();
    console.log(apptids);
    if(!apptids)
    {
        if (msg == "Delete") {
            $("#divnotblank").hide();
            $("#divblank").show();
            $("#btnyes").hide();
        } else {
            $("#divnotblankMark").hide();
            $("#divblankMark").show();
            $("#btnyesMark").hide();
        }

        
    }
    else
    {
        if (msg == "Delete") {
            $("#divblank").hide();
            $("#divnotblank").show();
            $("#btnyes").show();
        } else {
            $("#divblankMark").hide();
            $("#divnotblankMark").show();
            $("#btnyesMark").show();
        }
        
    }
    
}