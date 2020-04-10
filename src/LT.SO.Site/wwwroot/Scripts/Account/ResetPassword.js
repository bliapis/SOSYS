/// <reference path="../Shared/Helpers.js" />
var ResetPassword = {

    Init: function () {
        Helpers.Initialize();
        //this.Events.Init();
    },

    Methods: {
        ResetarSenha: function () {

            $body.addClass("loading");

            $("#ResetPasswordForm").submit();
        }
    }
};

$(document).ready(function () {
    ResetPassword.Init();

    $body = $("body");
    $(document).on({
        ajaxStart: function () { $body.addClass("loading"); },
        ajaxStop: function () { $body.removeClass("loading"); }
    });
});