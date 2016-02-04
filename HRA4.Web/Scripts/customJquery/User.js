$(document).ready(function () {
    ApplyUserSorter();
});
$(document).ready(function () {
    $(".trEditProfile").slideUp(0);
    $("#expand-div-content").hide();

    $("#btnSaveSlide").click(function () {
        $("#expand-div-content, #change-password-content").slideUp(200);
        $("#change-password-content").hide();
    });

    $("#btnCancelSlide").click(function () {
        $("#expand-div-content, #change-password-content").slideUp(200);
        $("#change-password-content").hide();
    });

    $(".expand-div").click(function () {

        $("#expand-div-content, #change-password-content").slideDown(200);
        $("#change-password-content").hide();
    });
});

//for manage user change password		
$(document).ready(function () {
    $(".trEditPassword").slideUp(0);
    $("#change-password-content").hide();

    $("#btnSavePassword").click(function () {
        $("#change-password-content").slideUp(200);
    });

    $("#btnCancelPassword").click(function () {
        $("#change-password-content").slideUp(200);
    });

    $(".change-password").click(function () {
        $("#change-password-content").slideDown(200);
    });
});
function ApplyUserSorter()
{
    $("#tblUsers").tablesorter(
         {
             headers: {
                 // assign the secound column (we start counting zero)
                 3: {
                     // disable it by setting the property sorter to false
                     sorter: false
                 }

             }
         }
);
}

function ValidateNewUser()
{
    var uname = $("#txtUserName").val();
    var fname = $("#txtFirstName").val();
    var lname = $("#txtLastname").val();

    if(uname.length==0 || fname.length==0 || lname.length==0)
    {
        ShowErrorNotification('Please fill mandatory fields.');
        return false;
    }
    return true;
}

function Validate(userid)
{
    var nPassword = $("#" + userid + "-nPassword").val();
    var cPassword = $("#" + userid + "-cPassword").val();
    var patt = new RegExp("^(?=.*[A-Za-z])(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{6,}$");
    
    if (nPassword.length < 6)
    {
        ShowErrorNotification('Password should be atleast 6 characters long.');
        return false;
    }
    else if(nPassword!=cPassword)
    {
        ShowErrorNotification('Password and Confirm password should match');
        return false;
    }
    else if (!patt.test(nPassword))
    {
       // alert('hi');
        ShowErrorNotification('Password must have atleast one special character.');
        return false;
    }
    //alert(nPassword);
    return true;
}
function CancelProfile(userid)
{
    $('#' + userid + '-divProfile').html('');
    $('#' + userid + '-divChangePassword').html('');
    $(".trEditProfile").slideUp(0);
    $(".trEditPassword").slideUp(0);
    return false;
}

function AddNewProfile(url,userid)
{
    //alert(userid);
    //al(url);
    $.ajax({
        url: url,
        data:{userid:userid},
        dataType: 'html',
        success: function (data) {
            $("#tblProfile").append(data);
        }
    });
}
function SetProfile(url, userid) {
     // alert(url);
     //alert(userid);
    $(".trEditProfile").slideUp(0);
    $(".trEditPassword").slideUp(0);
   

    $.ajax({
        url: url,
        data: { userid: userid },
        dataType: 'html',
        success: function (data) {
            $('#' + userid + '-divProfile').html('');
            $('#' + userid + '-divProfile').html(data);
            ApplyUserSorter();
            $('#' + userid + '-expand-div-content').slideDown(100);
        }
    });

}

function ChangePassword(url, userid)
{
  //  alert(url);
    // alert(userid);
    $(".trEditProfile").slideUp(0);
    $(".trEditPassword").slideUp(0);
   
    $.ajax({
        url: url,
        data: { userid: userid },
        dataType: 'html',
        success: function (data) {            
            $('#' + userid + '-divChangePassword').html('');
            $('#' + userid + '-divChangePassword').html(data);
            $('#' + userid + '-change-password-content').slideDown(100);
            ApplyUserSorter();
        }
    });

}