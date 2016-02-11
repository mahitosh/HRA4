$(document).ready(function () {

});

function LoadBreastCancerRiskFactors(url)
{
    //$("#hidSelectedMrn").val(mrn);
    //$("#hidSelectedAppId").val(apptid);
    var mrn = $("#hidMrn").val();
    var apptId = $("#hidApptId").val();


    $.ajax({
        url: url,
        data: { mrn: mrn , apptId: apptId },
        dataType: 'html',
        success: function (data) {
            $('#BreastCancerRisk').html('');
            $('#BreastCancerRisk').html(data);             
        }
    });

}

function LoadColorectalCancerRiskFactors(url) {
 
    var mrn = $("#hidMrn").val();
    var apptId = $("#hidApptId").val();
    

    $.ajax({
        url: url,
        data: { mrn: mrn, apptId: apptId },
        dataType: 'html',
        success: function (data) {
            $('#ColorectalCancerRisk').html('');
            $('#ColorectalCancerRisk').html(data);
        }
    });

}
