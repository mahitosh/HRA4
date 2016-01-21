

//show add appointment link
function hidedelete() {
    $.ajax({
        url: '/TestPatient/RefreshTestPatients',
        dataType: 'html',
        success: function (data) {
            $('#testpatientsgrid').html('');
            $('#testpatientsgrid').html(data);
        }
    });
}

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


function DeleteTestPatients() {

    var apptids = getSelectedIds();
  
    $.ajax({
        url: '/TestPatient/DeleteTestPatientsByapptids',
        data: { ids: apptids },
        dataType: 'html',
        success: function (data) {
            $("#confirm-xml").modal('hide');
            $('#testpatientsgrid').html('');
            $('#testpatientsgrid').html(data);
        }
    });
}

function MarkAsNotTestPatients() {

    var apptids = getSelectedIds();
  
    $.ajax({
        url: '/TestPatient/ExcludeTestPatientsByapptids',
        data: { ids: apptids },
        dataType: 'html',
        success: function (data) {
            $('#testpatientsgrid').html('');
            $('#testpatientsgrid').html(data);
        }
    });
}

function RefreshTestPatients() {
    $.ajax({
        url: '/TestPatient/RefreshTestPatients',
        dataType: 'html',
        success: function (data) {
            $('#testpatientsgrid').html('');
            $('#testpatientsgrid').html(data);
        }
    });

}