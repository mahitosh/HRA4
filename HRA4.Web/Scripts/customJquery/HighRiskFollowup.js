var chkExcludeCancerGeneticsPatients = false;
var chkExcludeDoNotContactPatients = false;
var chkExcludepatientswithgenetictesting = false;
var txtSearch = "";

function SetValuesAndSearch(url) {
    //debugger;
    chkExcludeCancerGeneticsPatients = $('#chkExcludeCancerGeneticsPatients').is(':checked');
    chkExcludeDoNotContactPatients = $('#chkExcludeDoNotContactPatients').is(':checked');
    chkExcludepatientswithgenetictesting = $('#chkExcludepatientswithgenetictesting').is(':checked');
    txtSearch = $('#txtSearch').val();
    Search(txtSearch, chkExcludeCancerGeneticsPatients, chkExcludeDoNotContactPatients, chkExcludepatientswithgenetictesting, url);
}


function Clear(url)
{

    $('#txtSearch').val("");
    if ($('#chkExcludeDoNotContactPatients').is(':checked'))
    {
        $('#chkExcludeDoNotContactPatients').attr('checked', false);
    }
    if ($('#chkExcludeCancerGeneticsPatients').is(':checked')) {
        $('#chkExcludeCancerGeneticsPatients').attr('checked', false);
    }
    if ($('#chkExcludepatientswithgenetictesting').is(':checked')) {
        $('#chkExcludepatientswithgenetictesting').attr('checked', false);
    }

   // Search(txtSearch, chkExcludeCancerGeneticsPatients, chkExcludeDoNotContactPatients, chkExcludepatientswithgenetictesting, url);
}

function Applytablesorter(tableid,columnid) {
    $(tableid).tablesorter(
         {
             headers: {
                 // assign the secound column (we start counting zero)
                 columnid: {
                     // disable it by setting the property sorter to false
                     sorter: false
                 }

             }
         }
);

}



function Search(txtSearch, chkExcludeCancerGeneticsPatients, chkExcludeDoNotContactPatients, chkExcludepatientswithgenetictesting, url) {
    

    

    $.ajax({
        url: url,
        data: {
            txtSearch: txtSearch,
            chkExcludeCancerGeneticsPatients: chkExcludeCancerGeneticsPatients,
            chkExcludeDoNotContactPatients: chkExcludeDoNotContactPatients,
            chkExcludepatientswithgenetictesting: chkExcludepatientswithgenetictesting
        },
        dataType: 'html',
        success: function (data) {
            debugger;
            $('#divTable').html('');
            $('#divRecordStatus').html('');
            $('#divTable').html(data);
            
            var rowCount = $('#tblHighRisk tr').length - 1;
            $('#spanRecordCount').html(rowCount)
            if (rowCount == 0)
            {
                $('#divRecordStatus').html('No records found.');
            }

        }
    });
}


function GetPatientDetails(unitnum, globalGetJSONPath) {



    $.ajax({
        type: "POST",
        url: globalGetJSONPath,
        data: { unitnum: unitnum },
        dataType: "json",
        async: true,
        success: function (Data) {


            $("#spanPatientName").html(Data.obj.name);
            $("#spanMRN").html(Data.obj.unitnum);
            $("#spanAge").html(Data.obj.age);
            $("#spanH").html(Data.obj.homephone);
            $("#spanC").html(Data.obj.cellphone);
            $("#spanW").html(Data.obj.workphone);
            $("#spanDOB").html(Data.obj.dob);
            $("#spanPCP").html('');
            $("#imagediv").empty();
            var img = $('<img id="imgPedigree">');
            img.attr('src', Data.ImageUrl);
            img.appendTo('#imagediv');
            $('#LifetimeCancer-pedigree-diagram-modal').modal('show');
        }

    })

}
