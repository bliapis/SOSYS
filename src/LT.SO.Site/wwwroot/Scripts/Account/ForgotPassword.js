/// <reference path="../Shared/Helpers.js" />
var ForgotPassword = {

    Init: function () {
        Helpers.Initialize();
        //this.Events.Init();
    },

    Methods: {
        EnviarEmail: function () {
            
            $body.addClass("loading");

            $("#ForgotPasswordForm").submit();
        }
    }
};

$(document).ready(function () {

    ForgotPassword.Init();

    $body = $("body");

    $(document).on({
        ajaxStart: function () { $body.addClass("loading"); },
        ajaxStop: function () { $body.removeClass("loading"); }
    });

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
});