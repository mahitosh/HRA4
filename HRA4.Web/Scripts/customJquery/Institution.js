

$(".editmenu").slideUp(0);
$(".schedule-more-detail-content").hide();

function HideAddAppt() {
    $("#EnteredMRN").val('');
    $("#add-edit-MRN").modal('hide');
}
function ShowAddAppointment(path) {
    
    var MRN = $("#EnteredMRN").val();
    var clinicId = $("#ddClinic").val();
    if (clinicId == '')
        clinicId = "-1";
    $.ajax({
        type: "POST",
        url: path,
        data: { MRN: MRN, clinicId: clinicId},
        dataType: "json",
        async: true,
        success: function (Data) {
            $("#MRNBody").hide();
            $("#ApptFooter").hide();
            $('#ApptBody').html(Data.view);

        }
    })


}

function validateDate(testdate) {
    var date_regex = /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/;
    return date_regex.test(testdate);
}
function ValidateModel() {
   
    var PatientName = $("#PatientName").val();
    var MRN = $("#MRN").val();
    var dobdate = $("#dob-date").val();
    var ddlGenders = $("#ddlGenders").val();
    var editappdate = $("#edit-app-date").val();
    var ddlappttimes = $("#ddlappttimes").val();
    var Survey = $("#Survey").val();
    var ddlclinics = $("#ddlclinics").val();

    if(PatientName==''||MRN==''||dobdate==''||editappdate==''||Survey=='')
    {
        ShowNotification('Please fill Mandatory fields!');
        return false;
    }
    //var result1=validateDate(dobdate);
    //var result2=validateDate(editappdate);
    //if (result1==false ||result2==false)
    //{
    //    ShowNotification("Invalid Date!");
    //    return false;
    //}
    
    ShowNotification('Appointment Saved Successfully!');
    $("#add-edit-MRN").modal('hide');
    return true;
}


function moredetails() {
    $(".schedule-more-detail-content").toggle(200);
    $("i", this).toggleClass("fa-plus-circle fa-minus-circle");
}
function ShowEdit(obj, mrn, apptid, path) {
    $(".editmenu").slideUp(0);
    var trid = "#" + obj.id + "-b";
    $(trid).slideDown(100);
    $("#hidSelectedMrn").val(mrn);
    $("#hidSelectedAppId").val(apptid);
    ///
    var dt = $("#appt-date").val();
    var nm = $("#name").val();
    var clinicId = $("#ddClinic").val();
    $.ajax({
        type: "POST",
        url: path,
        data: { apptid: apptid, name: nm, appdt: dt, clinicId: clinicId },
        dataType: "json",
        async: true,
        success: function (Data) {
            $('.InstitutionPartialdiv').html('');
            $('.InstitutionPartialdiv').html(Data.view);
            Applytablesorter();
            $(".schedule-more-detail-content").hide();
        }
    })


}

function cancel(obj,status,path,apptid,IsGoldenAppointment) {
    if (status != "Yes")
    {
        if (IsGoldenAppointment == "No") {
            var clinicId = $("#ddClinic").val();
            if (clinicId == '')
                clinicId = "-1";
            $.ajax({
                type: "POST",
                url: path,
                data: { apptid: apptid, name: '', appdt: '', clinicId: clinicId ,flag:'true'},
                dataType: "json",
                async: true,
                success: function (Data) {
                                     

                }
            })
        }
        $("#MRNBody").show();
        $("#ApptFooter").show();
        $("#ApptBody").html('');
        $("#EnteredMRN").val('');
        $("#add-edit-MRN").modal('hide');
    }
    else {
        var chkid = "#" + obj.id + "chk";
        var hfid = "#" + obj.id + "hf";
        var initialvalue = $(hfid).val();
        if (initialvalue == 'True') {
            $(chkid).prop("checked", true);

        } else {
            $(chkid).prop("checked", false);

        }

        $(".editmenu").slideUp(100);
    }
    

}

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
            $(".editmenu").slideUp(0);
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
            $(".editmenu").slideUp(0);
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
function SetValues(MRN, apptid, xmlType, url) {
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
    //alert(globalGetJSONPath);
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
            $("#upload-xml").modal('hide');
            ShowNotification('File uploaded successfully');
            // $("#divNotification").text('File uploaded successfully');
            // $("#divNotification").fadeToggle(2000);
            //$("#divNotification").fadeToggle(2000);
        }
    }).always(function (Data) {

    });
}
/* ===================Xml Upload================== */

/*=======Delete Appointment=======*/
function DeleteAppointment() {

    var apptId = $("#hidAppid").val();
    var globalGetJSONPath = $("#hidUploadUrl").val();
    var deleteUrl = $("#hidType").val();
    
    alert(globalGetJSONPath);

    $.ajax({
        type: "Get",
        url: globalGetJSONPath,
        data: { apptid: apptId, flag: 'false' },
        async: true,
        //contentType: false,
        //processData: false,
        success: function (Data) {
            $("#confirm-xml").modal('hide');
            $('#divRecordStatus').html('');
            $('#ScheduleCount').html(Data.apps_count);
            $('#divInstitutionGrid').html(Data.view);
            Applytablesorter();
            $(".editmenu").slideUp(0);
            if (parseInt(Data.apps_count, 0) == 0) {
                $('#divRecordStatus').html("No records found.");
            }
            ShowNotification('Appointment deleted successfully');
            //SearchAppointment(deleteUrl);
            

        }
    }).always(function (Data) {

    });

}
/*=======Delete Appointment=======*/


/*======Start RiskCalculation logic ============ */
function RiskCalculation(globalGetJSONPath, MRN, apptid, status) {
    $.ajax({
        beforeSend: function () {
            if (status == 'Run') {
                $("#RiskScoreProgressdiv").show();
                $("#divfooter > button").prop("disabled", true);
                $("#divheader > button").prop("disabled", true);

            }

        },
        type: "POST",
        url: globalGetJSONPath,
        data: { MRN: MRN, apptid: apptid, status: status },
        dataType: "json",
        async: true,
        success: function (Data) {

            $('#divriskcalculate').html(Data.view);
            $("#RiskScoreProgressdiv").hide();
            $("#divfooter > button").prop("disabled", false);
            $("#divheader > button").prop("disabled", false);
        }

    })

}
/*======End RiskCalculation logic ============ */

function NewDocument(globalGetJSONPath, MRN, apptid) {

    $.ajax({
        type: "POST",
        url: globalGetJSONPath,
        data: { mrn: MRN, apptid: apptid },
        dataType: "json",
        async: true,
        success: function (Data) {
            $('#divNewDocument').html(Data.view);
        }
    })
}

function ShowDocument(globalGetJSONPath, templateid) {
    var mrn = $("#hidSelectedMrn").val();
    var apptId = $("#hidSelectedAppId").val();

    $("#divShowHtml").fadeIn();
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
    var target = document.getElementById('divShowHtml');
    // alert(target);
    var spinner = new Spinner(opts).spin(target);
    $(target).data('spinner', spinner);

    $("#btnDocDownload").attr('disabled', true);
    $("#btnDocCancel").attr('disabled', true);
    // alert(mrn); btnDocCancel
    //alert(apptId);
    $.ajax({
        type: "POST",
        url: globalGetJSONPath,
        data: { templateId: templateid, mrn: mrn, apptId: apptId },
        dataType: "json",
        async: true,
        success: function (Data) {
            $('#divShowHtml').data('spinner').stop();
            //$('#divShowHtml').hide();
            $("#btnDocDownload").attr('disabled', false);
            $("#btnDocCancel").attr('disabled', false);
            $('#divShowHtml').html(Data.view);

        }
    })
}
function pop_print() {
    w = window.open(null, 'Print_Page', 'scrollbars=yes');
    w.document.write(jQuery('#divPrint').html());
    w.document.close();
    w.print();
}

function printDiv(divID) {


    //Get the HTML of div
    var divElements = document.getElementById('divPrint').innerHTML;
    //Get the HTML of whole page
    var oldPage = document.body.innerHTML;

    //Reset the page's HTML with div's HTML only
    document.body.innerHTML =
      "<html><head> <script src='https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js'></script><title>Test</title></head><body>" +
      divElements + "</body>";

    //Print Page
    window.print();
    // $("#schedule-new-doc-modal").modal("hide");
    //Restore orignal HTML
    document.body.innerHTML = oldPage;

    //location.reload();
    // $("#schedule-new-doc-modal").modal('hide');
    return true;

}

function CloseDocModel() {
    // alert($("#schedule-new-doc-modal"));
    document.getElementById('schedule-new-doc-modal').style.display = "none";
    $("#schedule-new-doc-modal").modal("hide");
}

