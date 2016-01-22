
$(document).ready(function () {
    
    var txt = $('#ddlsurveys option:selected').text();
    $("#ddlsurveystext").val(txt);

    $("#ddlsurveys").change(function () {
        var txt = $('#ddlsurveys option:selected').text();
        $("#ddlsurveystext").val(txt);
    });

   
    $('#menu ul').hide();
    $("ul#menu").off("click");
    //alert('hi');
    //$('#loading').data('spinner').stop();
    //$('#loading').hide();
    
    $("#btnAdd").click(function () {

        var url = '@Url.Action("Create")';
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
        var spinner = new Spinner(opts).spin(target);
        $.post(url,
        {
            instName: $('#txtInstitution').val()
        },
        function (data, status) {
            location.reload(true);
        });

    });




});

function ShowNotification(msg)
{
    var sudoNotify = $('.notification-container').sudoNotify({

        // auto hide after x seconds set in duration opiton
        autoHide: true,

        // displat a close button
        showCloseButton: true,

        // for autoHide option
        duration: 2, //seconds

        // top or bottom
        position: 'bottom',

        // log all messages to console with timestamp
        log: false,

        // notification bar opacity levels
        opacity: 0.95,

        // custom styles for default notification bar
        defaultStyle: {
            maxWidth: '1000px',
            fontSize: '16px'
        },

        // custom styles for error notification bar
        errorStyle: {
            color: '#000000',
            backgroundColor: '#FF9494'
        },

        // custom styles for warning notification bar
        warningStyle: {
            color: '#000000',
            backgroundColor: '#FFFF96'
        },

        // custom styles for succsee notification bar
        successStyle: {
            color: '#000000',
            backgroundColor: '#B8FF6D'
        },

        // fade, scroll-left, scroll-left-fade, scroll-right, 
        // scroll-right-fade, slide, slide-fade or none
        animation: {
            type: 'slide-fade',
            showSpeed: 400,
            hideSpeed: 250
        },

        // fire a function when a notificatio bar is closed
        onClose: function (notificationType) {
           // alert(notificationType + ' notification closed');
        },

        // fire a function when a notification bar is trigged.
        onShow: function (notificationType) {
          //  alert(notificationType + ' notification showing');
        }
    });

    sudoNotify.success(msg);
}

