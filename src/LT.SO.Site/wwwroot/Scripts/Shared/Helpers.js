var Helpers = {

    ToJavaScriptDate: function (value) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var dt = new Date(parseFloat(results[1]));
        return dt.getDate() + "/" + (dt.getMonth() + 1) + "/" + dt.getFullYear();
    },

    Initialize: function () {
        
    },


    SomenteNumero: function (e) {
        var tecla = (window.event) ? event.keyCode : e.which;
        if ((tecla > 47 && tecla < 58)) return true;
        else {
            if (tecla === 8 || tecla === 0) return true;
            else return false;
        }
    },


    DataValida: function (d) {
        var patternData =
            /^(((0[1-9]|[12][0-9]|3[01])([-.\/])(0[13578]|10|12)([-.\/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-.\/])(0[469]|11)([-.\/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-.\/])(02)([-.\/])(\d{4}))|((29)(\.|-|\/)(02)([-.\/])([02468][048]00))|((29)([-.\/])(02)([-.\/])([13579][26]00))|((29)([-.\/])(02)([-.\/])([0-9][0-9][0][48]))|((29)([-.\/])(02)([-.\/])([0-9][0-9][2468][048]))|((29)([-.\/])(02)([-.\/])([0-9][0-9][13579][26])))$/;
        if (!patternData.test(d))
            return false;
        else
            return true;
    },

    ConvertFormToViewModel: function (formElement) {

        var arr = $(formElement).serializeArray();
        var model = {};

        $.each(arr, function (index, obj) {
            model[obj.name] = obj.value;
        });

        return model;
    },

    ShowModelValidation: function (validationResult) {

        var msg = "";

        if (validationResult.ListErrors.length != 0) {

            $.each(validationResult.ListErrors, function (index, element) {

                var $input = $("input[data-mensagem-erro=\"" + element.ErrorField + "\"]");
                var $textarea = $("textarea[data-mensagem-erro=\"" + element.ErrorField + "\"]");
                var $select = $("select[data-mensagem-erro=\"" + element.ErrorField + "\"]");
                var $span = $("span[data-mensagem-erro=\"" + element.ErrorField + "\"]");

                $input.closest("div").addClass("has-error");
                $textarea.closest("div").addClass("has-error");
                $select.closest("div").addClass("has-error");
                $input.closest(".form-group").addClass("has-error");

                $span.html(element.ErrorMessage);

                $input.on("blur", function () {
                    $input.closest("div").removeClass("has-error");
                    $input.closest(".form-group").removeClass("has-error");
                    $span.html(" ");
                });

                $textarea.on("blur", function () {
                    $textarea.closest("div").removeClass("has-error");
                    $span.html(" ");
                });

                $select.on("blur", function () {
                    $select.closest("div").removeClass("has-error");
                    $span.html(" ");
                });

                msg = msg + "<p>" + element.ErrorMessage + "</p>";

            });
        }

        if (msg !== "")
            GS_Alert.Errortoastr(msg);

        if (validationResult.StackTrace != null)
            GS_Alert.Errortoastr(validationResult.Message);
    },

    GetBrowser: function () {


        if (((!!window.opr && !!opr.addons) || !!window.opera || navigator.userAgent.indexOf(" OPR/") >= 0) === true) {

            return "OPERA";
        }

        else if ((!!window.chrome && !!window.chrome.webstore) === true) {

            return "CHROME";
        }

        else if ((/constructor/i.test(window.HTMLElement) || (function (p) { return p.toString() === "[object SafariRemoteNotification]"; })(!window["safari"] || safari.pushNotification)) === true) {

            return "SAFARI";
        }

        else if ((false || !!document.documentMode) == true) {

            return "INTERNET EXPLORER";

        }


        else if (typeof InstallTrigger !== "undefined") {

            return "FIREFOX";
        }

        else {

            return "EDGE";
        }



        // Chrome 1+
        //var isChrome = !!window.chrome && !!window.chrome.webstore;
    },

    MessageValidation: function (msgStatus, message, callBackUrl) {

        if (msgStatus == 1) {

            toastr["success"](typeof message === 'string' ? message : message[0]);

            if (callBackUrl != "" && callBackUrl !== undefined) {
                setTimeout(function () {
                    window.location = callBackUrl;
                }, 3000);
            }
        }
        else if (msgStatus == 2) {
            if (typeof message === 'string')
                toastr["error"](message);
            else {

                for (var i = 0; i < message.length; i++) {
                    toastr["error"](message[i]);
                }
            }
        }
        else {
            toastr["info"](message);
        }
    },

    ShowLoader: function () {
        $('#page-loader').removeClass('hide');
        $('#page-loader').addClass('show');
    },

    HideLoader: function () {
        $('#page-loader').removeClass('show');
        $('#page-loader').addClass('hide');
    }
};

var LT_Alert = {

    Errortoastr: function (msg) {
        toastr.error(msg, "Erro");
    },

    Msgtoastr: function (msg, callback) {
        toastr.info(msg, "Mensagem", { onclick: callback, onHidden: callback, onCloseClick: callback });
    },

    Simples: function (mensagem) {
        bootbox.dialog({
            size: "small",
            title: "Alerta!",
            message: mensagem,
            closeButton: true,
            buttons: {
                ok: {
                    label: "OK",
                    className: "btn-primary",
                    callback: function () {
                        bootbox.hideAll();
                        //location.reload(true);
                    }
                }
            }
        });

    },

    SimplesComTitulo: function (mensagem, titulo, sizePorcentagem) {

        bootbox.hideAll();

        bootbox.dialog({
            size: "larger",
            title: titulo,
            message: mensagem,
            closeButton: true,
            buttons: {
                ok: {
                    label: "OK",
                    className: "btn-primary",
                    callback: function () {
                        bootbox.hideAll();
                        //location.reload(true);
                    }
                }
            }
        });

        if (typeof sizePorcentagem === "undefined" || sizePorcentagem === null) {
            sizePorcentagem = "60%";
        }

        $(".modal-body").css("font-size", "100%");
        $(".modal-dialog").css("width", sizePorcentagem);
        $(".bootbox-body").css("max-height", "400px").css("overflow-y", "auto");

        Helpers.Initialize();
    },

    SimplesComTituloSemBotoes: function (mensagem, titulo) {

        bootbox.hideAll();

        bootbox.dialog({
            size: "larger",
            title: titulo,
            message: mensagem,
            closeButton: true
        });

        $(".modal-body").css("font-size", "14px");
        $(".bootbox-body").css("max-height", "400px").css("overflow-y", "auto");

        Helpers.Initialize();
    },

    SimplesGrande: function (mensagem, titulo) {

        bootbox.dialog({
            size: "large",
            title: titulo,
            message: mensagem,
            closeButton: true,
            buttons: {
                ok: {
                    label: "OK",
                    className: "btn-primary",
                    callback: function () {
                        bootbox.hideAll();
                    }
                }
            }
        });

    },

    SimpleSuccess: function (msg, callback) {


        if (callback != undefined) {

            bootbox.alert({
                title: "Mensagem",
                closeButton: true,
                size: "small",
                message: msg,
                callback: function () {
                    callback();
                }
            });

        }
        else {

            bootbox.alert({
                title: "Mensagem",
                closeButton: true,
                size: "small",
                message: msg
            });

        }

    },

    SimplesSucesso: function (mensagem) {
        bootbox.dialog({
            size: "small",
            title: "Sucesso!",
            message: mensagem,
            closeButton: true,
            buttons: {
                ok: {
                    label: "OK",
                    className: "btn-primary",
                    callback: function () {
                        bootbox.hideAll();
                        location.reload(true);
                    }
                }
            }
        });

    },

    SimplesErro: function (mensagem) {
        bootbox.hideAll();
        bootbox.dialog({
            size: "medium",
            title: "Alerta!",
            message: "Ocorreu um erro em sua solicitação!<br/>Por favor," +
                "entre em contato com a equipe técnica responsável.",
            closeButton: true,
            buttons: {
                ok: {
                    label: "OK",
                    className: "btn-primary",
                    callback: function () {
                        bootbox.hideAll();
                        //location.reload(true);
                    }
                }
            }
        });

        $("div .bootbox-body").append("<br/><br/>" +
            "<div> <a id=\"btnErroDetalhes\" href=\"javascript:Helpers.ExibiDetalhesErro();\">detalhes</a> </div>" +
            "<br/>" +
            "<div id=\"DivErroDetalhes\" style=\"display:none;overflow-y: auto\">" + mensagem + "</div>");
    },

    SimplesNoHideenAll: function (mensagem) {
        bootbox.dialog({
            size: "small",
            title: "Alerta!",
            message: mensagem,
            closeButton: true,
            buttons: {
                ok: {
                    label: "OK",
                    className: "btn-primary"
                }
            }
        });
    },

    SimplesCallBack: function (mensagem, callback) {
        bootbox.dialog({
            size: "small",
            title: "Alerta!",
            message: mensagem,
            closeButton: false,
            buttons: {
                ok: {
                    label: "OK",
                    className: "btn-primary",
                    callback: function () {
                        bootbox.hideAll();
                        callback(true);
                    }
                }
            }
        });
    },

    SimplesCallBackLarger: function (mensagem, callback, closeBtn) {
        //Estava dando erro no chrome
        if (closeBtn == "" || closeBtn == null || closeBtn == undefined) {
            closeBtn = false;
        }

        bootbox.dialog({
            size: "",
            title: "Notas de Atualização!",
            message: mensagem,
            className: "modal60",
            closeButton: closeBtn,
            buttons: {
                ok: {
                    label: "OK",
                    className: "btn-primary",
                    callback: function () {
                        bootbox.hideAll();
                        callback(true);
                    }
                }
            }
        });
        //$(".modal60 .bootbox-body").children().css("font-size", "15px").css("max-width", "1000px");
        $(".modal60 .bootbox-body").children().css("font-size", "15px");
    },

    Dialogo: function (mensagem, callback) {
        bootbox.dialog({
            size: "small",
            message: mensagem,
            title: "Alerta!",
            closeButton: true,
            buttons: {
                sim: {
                    label: "Sim",
                    className: "btn-primary",
                    callback: function () {
                        bootbox.hideAll();
                        callback(true);
                    }
                },
                nao: {
                    label: "Não",
                    className: "btn-default",
                    callback: function () {
                        bootbox.hideAll();
                        callback(false);
                    }
                }
            }
        });
    },

    DialogoNoHideenAll: function (mensagem, callback) {
        bootbox.dialog({
            size: "small",
            message: mensagem,
            title: "Alerta!",
            closeButton: true,
            buttons: {
                nao: {
                    label: "Não",
                    className: "btn-default",
                    callback: function () {
                        callback(false);
                    }
                },
                sim: {
                    label: "Sim",
                    className: "btn-primary",
                    callback: function () {
                        callback(true);
                    }
                }
            }
        });
    }
};


var LT_Path = {
    GetUrl: function (url) {
        var appUrl = baseUrl;
        return (appUrl == "/") ? url : appUrl + url;
    }
};


var LT_Ajax = {

    AjaxPOST: function (url, dataType, data, callback) {
        $.ajax({
            url: url,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: dataType,
            data: JSON.stringify({ 'data': data })
        })
            .done(function (resultado) {
                Helpers.HideLoader();
                callback(resultado);

            }).fail(function (response) {
                Helpers.HideLoader();
                var erro = JSON.parse(response.responseText);
                Helpers.MessageValidation(erro.resultApi.msgType, erro.resultApi.messages, '');
            });
    },

    AjaxGET: function (url, dataType, data, callback) {
        $.ajax({
            url: url,
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: dataType,
            data: { 'data': data }
        })
            .done(function (resultado) {
                if (resultado === "timeout") {
                    GS_Alert.SimplesCallBack("Sua sessão expirou! Você será redirecionado!", function () {
                        window.location.href = LT_Path.GetUrl("/Login/Index");
                    });
                }
                else if (resultado === "logout") {
                    GS_Alert.SimplesCallBack("Seu usuário efetuou login em outro terminal!", function () {
                        window.location.href = LT_Path.GetUrl("/Login/Index");
                    });
                } else {
                    callback(resultado);
                }
            })
            .fail(function () {
                GS_Alert.SimplesErro(response.responseText);
            });
    },

    AjaxNoData: function (url, dataType, callback) {
        $.ajax({
            url: url,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: dataType
        })
            .done(function (resultado) {
                Helpers.HideLoader();
                callback(resultado);

            }).fail(function (response) {

                Helpers.HideLoader();
                var erro = JSON.parse(response.responseText);
                Helpers.MessageValidation(erro.resultApi.msgType, erro.resultApi.messages, '');
            });
    },

    AjaxNoDataType: function (url, data, callback) {
        $.ajax({
            url: url,
            type: "POST",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8"
        }).done(function (resultado) {

            Helpers.HideLoader();
            callback(resultado);

        }).fail(function (response) {

            Helpers.HideLoader();
            //LT_Modal.ShowLayoutModal("Ocorreu um erro durante a requisição", response.message, )
            LT_Alert.Errortoastr(response.message);
        });
    }
};


var LT_Modal = {

    ShowLayoutModal: function (titulo, conteudo, btnActionTxt, btnActionFunc, btnActionClass, conteudoHtml = false) {

        LT_Modal.InitModal();

        if (btnActionClass === null)
            btnActionClass = "btn btn-default";

        $("#layoutModalHeader").text(titulo);

        if (conteudoHtml)
            $("#layoutModalBody").html(conteudo);
        else
            $("#layoutModalBody").text(conteudo);

        $("#layoutModalBtnAction").text(btnActionTxt);
        $("#layoutModalBtnAction").attr('onclick', btnActionFunc);
        $("#layoutModalBtnAction").addClass(btnActionClass);

        $('#layoutModal').modal('toggle');
    },

    ShowModalDelete: function (conteudo, btnActionFunc) {
        LT_Modal.ShowLayoutModal(
            "Tem certeza que deseja deletar o registro?",
            conteudo,
            "Deletar",
            btnActionFunc,
            "btn btn-danger"
        );
    },

    ShowPopDetalhes: function (conteudo) {

        LT_Modal.InitModal();

        LT_Modal.ShowLayoutModal(
            "Detalhes",
            conteudo,
            "",
            "",
            "",
            true
        );

        $("#layoutModalBtnAction").hide();
    },

    InitModal: function () {

        $("#layoutModalHeader").text();
        $("#layoutModalBody").text();
        $("#layoutModalBtnAction").text();
        $("#layoutModalBtnAction").attr('onclick','');
        $("#layoutModalBtnAction").attr('class', '');
        $("#layoutModalBtnAction").show();
    }
};


var LT_Grid = {

    CreateGrid: function(tableId, ajaxUrl, filterObjs, columnDefs, btnsDefault = [['detail', true, 'func'], ['edit', true, 'func'], ['del', true, 'func']]) {

        var btns = "";
        $.each(btnsDefault, function (i) {

            if (btnsDefault[i][0] == 'detail' && btnsDefault[i][1]) {
                btns += "<button id='btnDetails' class='btn btn-info' title='Detalhes' onclick='" + btnsDefault[i][2] +"' data-toggle='tooltip' data-placement='top'><i class='fa fa-search'></i></button>";
            }

            if (btnsDefault[i][0] == 'edit' && btnsDefault[i][1]) {
                btns += "<button id='btnDetails' class='btn btn-lime ml-1' title='Editar' onclick='" + btnsDefault[i][2] +"' data-toggle='tooltip' data-placement='top'><i class='fa fa-edit'></i></button>";
            }

            if (btnsDefault[i][0] == 'del' && btnsDefault[i][1]) {
                btns += "<button id='btnDetails' class='btn btn-danger ml-1' title='Deletar' onclick='" + btnsDefault[i][2] +"' data-toggle='tooltip' data-placement='top'><i class='fa fa-window-close'></i></button>";
            }
        });

        columnDefs.push({ "targets": -1, "data": null, "defaultContent": btns});


        var table = $('#' + tableId).DataTable({
            "proccessing": true,
            "serverSide": true,
            "searching": false,
            "ajax": {
                url: ajaxUrl,
                type: 'POST',
                data: function (d) {
                    $.each(filterObjs, function (i) {
                        d[''+filterObjs[i]+''] = $('#' + filterObjs[i]).val();
                    });
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("Erro - Não foi possível carregar os dados do Grid.");
                }
            },
            "processing": true,
            "language": {
                "search": "",
                "searchPlaceholder": "Pesquisar...",
                "lengthMenu": "Exibir _MENU_ registros por página",
                "zeroRecords": "Sem resultados...",
                "info": "Página _PAGE_ de _PAGES_",
                "infoEmpty": "Sem resultados disponiveis",
                "infoFiltered": "(Filtrados _MAX_ Total)",
                "paginate": {
                    "previous": "Anterior",
                    "next": "Próximo"
                },
                "processing": "Carregando..."
            },
            "columnDefs": columnDefs
        });

        $.fn.dataTable.ext.errMode = 'none';
        $('#' + tableId).on('error.dt', function (e, settings, techNote, message) {
            Helpers.MessageValidation('2', '' + message.split('-')[1] + '', '');
        });

        return table;
    },

    CreateGridColumnDef: function (target, data, visible, defaultContent) {

        var column = {};
        column.targets = target;
        column.data = data;
        column.visible = visible;
        column.defaultContent = defaultContent;

        return column;
    }
};

$(document).on("ready", function () {
    Helpers.Initialize();
});