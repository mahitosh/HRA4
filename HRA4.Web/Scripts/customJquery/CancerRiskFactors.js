$(document).ready(function () {

});

function LoadBreastCancerRiskFactors(url)
{
    //$("#hidSelectedMrn").val(mrn);
    //$("#hidSelectedAppId").val(apptid);

    alert($("#hidMrn").val());

    $.ajax({
        url: url,
        data: { mrn: mrn , apptId:apptId},
        dataType: 'html',
        success: function (data) {
            $('#BreastCancerRisk').html('');
            $('#BreastCancerRisk').html(data);             
        }
    });

}