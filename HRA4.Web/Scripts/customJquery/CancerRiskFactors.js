$(document).ready(function () {

});

function LoadBreastCancerRiskFactors(url)
{
    //$("#hidSelectedMrn").val(mrn);
    //$("#hidSelectedAppId").val(apptid);
    var mrn = $("#hidMrn").val();
    var apptId = $("#hidApptId").val();
    alert(url);

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