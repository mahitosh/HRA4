
$(document).ready(function () {
    $("#divNotification").css("display", "none");
    $('#menu ul').hide();
    $("ul#menu").off("click");
    $("#tblInstitutions").tablesorter();
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

