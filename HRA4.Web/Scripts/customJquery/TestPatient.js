

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
        }
    });

}
function showNotification() {
    ShowNotification('Test Patients Created Successfully');
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