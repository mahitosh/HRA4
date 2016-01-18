/*======Start Append Pedigree image url dynamically ============ */

function ShowPedigreeImage(unitnum, apptid, PatientName, globalGetJSONPath) {
   
   // var globalGetJSONPath = '@Url.Action("ShowPedigreeImage", "Institution")';

    $.ajax({
        type: "POST",
        url: globalGetJSONPath,
        data: { unitnum: unitnum, apptid: apptid },
        dataType: "json",
        async: true,
        success: function (Data) {


            //var ImageUrl = Data.ImageUrl;

            $("#spanPatientName").html(PatientName);
            $("#spanMRN").html(unitnum);
            $("#imagediv").empty();
            var img = $('<img id="imgPedigree">'); //Equivalent: $(document.createElement('img'))
            img.attr('src', Data.ImageUrl);
            img.appendTo('#imagediv');

            // $('img[id="imgPedigree"]').attr({ src: Data.ImageUrl});
            $('#pedigree-diagram-modal').modal('show');


        }

    })






}

/*======End Append Pedigree image url dynamically ============ */


/*======Start Search for Appointment logic ============ */
function SearchAppointment(globalGetJSONPath) {
    
    
    var name = $('#name').val();
    var appdt = $('#chkbox').is(":checked") ? $('#appt-date').val() : "";
    var clinicId = $('#ddClinic').val().length > 0 ? $('#ddClinic').val() : "-1";

    //var globalGetJSONPath = '@Url.Action("FilteredInstitution", "Institution")';
    //Loading popup - Mahitosh
    $("#loading").fadeIn();
    var opts = {
        lines: 12, // The number of lines to draw
        length: 7, // The length of each line
        width: 4, // The line thickness
        radius: 10, // The radius of the inner circle
        color: '#000', // #rgb or #rrggbb
        speed: 1, // Rounds per second
        trail: 60, // Afterglow percentage
        shadow: false, // Whether to render a shadow
        hwaccel: false // Whether to use hardware acceleration
    };
    var target = document.getElementById('loading');
    // alert(target);
    var spinner = new Spinner(opts).spin(target);
    $(target).data('spinner', spinner);
    // Loading popup - Mahitosh
    $.ajax({
        type: "POST",
        url: globalGetJSONPath,
        data: { name: name, appdt: appdt, clinicId: clinicId },
        dataType: "json",
        async: true,
        success: function (Data) {
            $('#divRecordStatus').html('');
            $('#ScheduleCount').html(Data.apps_count);
            $('#divInstitutionGrid').html(Data.view);
            Applytablesorter();
            $('#loading').data('spinner').stop();
            $('#loading').hide();
            if (parseInt(Data.apps_count, 0) == 0) {
                $('#divRecordStatus').html("No records found.");
            }


        }

    })

}
/*======End Search for Appointment logic ============ */

/*======Start DNC logic ============ */

function AddRemoveTask(isDNC, unitnum, apptid, globalGetJSONPath) {

    var name = $('#name').val();
    var appdt = $('#appt-date').val();
    var clinicId = $('#ddClinic').val().length > 0 ? $('#ddClinic').val() : "-1";

   // var globalGetJSONPath = '@Url.Action("AddRemoveTask", "Institution")';

    $.ajax({
        type: "POST",
        url: globalGetJSONPath,
        data: { name: name, appdt: appdt, isDNC: isDNC, unitnum: unitnum, apptid: apptid, clinicId: clinicId },
        dataType: "json",
        async: true,
        success: function (Data) {
            $('#divRecordStatus').html('');
            $('#ScheduleCount').html(Data.apps_count);
            $('#divInstitutionGrid').html(Data.view);
            Applytablesorter();
            if (parseInt(Data.apps_count, 0) == 0) {
                $('#divRecordStatus').html("No records found.");
            }
        }
    })
    return false;
};
/*======End DNC logic ============ */

/*======Start Delete confirmation logic ============ */

function confirmation() {
    var c = confirm("Are you sure you want to delete this Appointment?");
    return c;
};
/*======End Delete confirmation logic ============ */

/* ============Upload===================== */
function SetValues(MRN, apptid, xmlType,url) {
    $("#txtFileUpload").val('');
    $("#hidMrn").val(MRN);
    $("#hidAppid").val(apptid);
    $("#hidType").val(xmlType);
    $("#hidUploadUrl").val(url);
}

function UploadXml() {

    var strMrn = $("#hidMrn").val();
    var apptId = $("#hidAppid").val();
    var xmlType = $("#hidType").val();  
    var globalGetJSONPath = $("#hidUploadUrl").val();
    globalGetJSONPath = globalGetJSONPath + '?mrn=' + strMrn + '&apptId=' + apptId;
    alert(globalGetJSONPath);
    var formData = new FormData();
    var file = $("#txtFileUpload")[0].files[0];    
    formData.append("file", file);
    
    $.ajax({
        type: "POST",
        url: globalGetJSONPath,
        data: formData,
        dataType: 'json',
        async: true,
        contentType: false,
        processData: false,
        success: function (data) {
            //alert('hi');
            $("#upload-xml").modal('hide');

        }
    }).always(function (Data) {

    });
}
/* ===================Xml Upload================== */


/*======Start RiskCalculation logic ============ */
function RiskCalculation(globalGetJSONPath,MRN,apptid) {

    $.ajax({
        type: "POST",
        url: globalGetJSONPath,
        data: {MRN: MRN, apptid: apptid},
        dataType: "json",
        async: true,
        success: function (Data) {
            $('#divriskcalculate').html(Data.view);
        }

    })

}
/*======End RiskCalculation logic ============ */

function NewDocument(globalGetJSONPath, MRN, apptid)
{
    $.ajax({
        type: "POST",
        url: globalGetJSONPath,
        data: { MRN: MRN, apptid: apptid },
        dataType: "json",
        async: true,
        success: function (Data) {
            $('#divriskcalculate').html(Data.view);
        }
    })
}