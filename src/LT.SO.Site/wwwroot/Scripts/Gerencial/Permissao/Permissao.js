/// <reference path="../../Shared/Helpers.js" />

var Permissao = {

    Init: function () {
        Helpers.Initialize();
    },

    Methods: {
        PesquisarPermissao: function () {
            table.draw();
        },

        MostrarEsconderFiltros: function () {

            var x = document.getElementById("filtros");

            if (x.style.display === "none") {
                x.style.display = "block";
                $('#iconHSFiltros').attr('class', 'fa fa-sort-down');
                $('#spanSHFiltros').text('Esconder filtros');
            } else {
                x.style.display = "none";
                $('#iconHSFiltros').attr('class', 'fa fa-sort-up');
                $('#spanSHFiltros').text('Mostrar filtros');
            }
        },

        CriarNovo: function () {
            Helpers.ShowLoader();
            window.location = LT_Path.GetUrl("/Permissao/Cadastro");
        },

        Salvar: function () {

            Helpers.ShowLoader();
            $("#formCadastroPermissao").submit();
        },

        BtnDetalhes: function (elemento) {

            var data = table.row($(elemento).parents('tr')).data();
            var id = data['id'];

            LT_Ajax.AjaxNoData(LT_Path.GetUrl("/Permissao/Detalhes/" + id), 'json', function (response) {

                if (response.success === true) {

                    LT_Modal.ShowPopDetalhes(response.data);
                } else {

                    var resultApi = JSON.parse(response.data);
                    Helpers.MessageValidation(resultApi.msgType, resultApi.messages, '');
                }
            });
        },

        BtnEditar: function (elemento) {
            Helpers.ShowLoader();
            var data = table.row($(elemento).parents('tr')).data();
            var elementoId = data['id'];

            window.location = LT_Path.GetUrl("/Permissao/Cadastro/" + elementoId);
        },

        BtnExcluir: function (elemento) {

            var data = table.row($(elemento).parents('tr')).data();
            gridLastElement = elemento;

            LT_Modal.ShowModalDelete(
                "A permissão " + data['nome'] + " será deletada!",
                "Permissao.Methods.GridExcluir()");
        },

        GridExcluir: function () {

            Helpers.ShowLoader();

            var elemento = gridLastElement;
            var data = table.row($(elemento).parents('tr')).data();
            var id = data['id'];

            LT_Ajax.AjaxNoData(LT_Path.GetUrl("/Permissao/Delete/" + id), 'json', function (response) {

                Helpers.MessageValidation(response.resultApi.msgType, response.resultApi.messages, '');

                if (response.resultApi.msgType == 1) {
                    table.row($(elemento).parents('tr')).remove().draw();
                    $('#layoutModal').modal('toggle');
                }
            });

            Helpers.HideLoader();
        },

        Voltar: function () {
            Helpers.ShowLoader();
            location.href = LT_Path.GetUrl("/Permissao/Index");
        }
    }
};

$(document).ready(function () {

    Permissao.Init();

    $body = $("body");

    $(document).on({
        ajaxStart: function () { $body.addClass("loading"); },
        ajaxStop: function () { $body.removeClass("loading"); }
    });

    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

    var gridLastElement;

    Helpers.HideLoader();

    $('#TipoId').select2();
});


var columnDefs = [];
columnDefs.push(LT_Grid.CreateGridColumnDef(0, "id", false, null));
columnDefs.push(LT_Grid.CreateGridColumnDef(1, "tipoId", false, null));
columnDefs.push(LT_Grid.CreateGridColumnDef(2, "valor", true, null));
columnDefs.push(LT_Grid.CreateGridColumnDef(3, "tipoNome", true, null));

var btnsGrid = [
    ['detail', true, 'Permissao.Methods.BtnDetalhes(this);'],
    ['edit', true, 'Permissao.Methods.BtnEditar(this);'],
    ['del', true, 'Permissao.Methods.BtnExcluir(this);']
];

var table = LT_Grid.CreateGrid('SearchResultTable', "/Permissao/RenderGrid", ['Valor', 'TipoId'], columnDefs, btnsGrid);