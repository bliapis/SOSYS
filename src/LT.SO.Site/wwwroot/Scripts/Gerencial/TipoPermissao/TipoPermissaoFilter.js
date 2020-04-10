/// <reference path="../../Shared/Helpers.js" />

var TipoPermissaoFilter = {

    Init: function () {
        Helpers.Initialize();
    },

    Methods: {
        PesquisarTipoPermissao: function () {
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
            window.location = LT_Path.GetUrl("/TipoPermissao/Cadastro");
        },

        Voltar: function () {
            Helpers.ShowLoader();
            location.href = LT_Path.GetUrl("/TipoPermissao/Index");
        },

        BtnEditar: function (elemento) {
            Helpers.ShowLoader();
            var data = table.row($(elemento).parents('tr')).data();
            var elementoId = data['id'];

            window.location = LT_Path.GetUrl("/TipoPermissao/Cadastro/" + elementoId);
        },

        BtnExcluir: function (elemento) {

            var data = table.row($(elemento).parents('tr')).data();
            gridLastElement = elemento;

            LT_Modal.ShowModalDelete(
                "O tipo de permissão " + data['nome'] + " será deletado!",
                "TipoPermissaoFilter.Methods.GridExcluir()");
        },

        GridExcluir: function () {
            Helpers.ShowLoader();

            var elemento = gridLastElement;
            var data = table.row($(elemento).parents('tr')).data();
            var id = data['id'];

            LT_Ajax.AjaxNoData(LT_Path.GetUrl("/TipoPermissao/Delete/" + id), 'json', function (response) {

                Helpers.MessageValidation(response.resultApi.msgType, response.resultApi.messages, '');
                
                if (response.resultApi.msgType == 1) {
                    table.row($(elemento).parents('tr')).remove().draw();
                    $('#layoutModal').modal('toggle');
                }
            });

            Helpers.HideLoader();
        },

        BtnDetalhes: function (elemento) {

            var data = table.row($(elemento).parents('tr')).data();
            var id = data['id'];

            LT_Ajax.AjaxNoData(LT_Path.GetUrl("/TipoPermissao/Detalhes/" + id), 'json', function (response) {

                if (response.success === true) {

                    LT_Modal.ShowPopDetalhes(response.data);
                } else {
            
                    var resultApi = JSON.parse(response.data);
                    Helpers.MessageValidation(resultApi.msgType, resultApi.messages, '');
                }
            });
        },

        Salvar: function () {

            Helpers.ShowLoader();
            $("#formCadastroTipoPermissao").submit();
        }
    }
};

$(document).ready(function () {

    TipoPermissaoFilter.Init();

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
});


var columnDefs = [];
columnDefs.push(LT_Grid.CreateGridColumnDef(0, "id", false, null));
columnDefs.push(LT_Grid.CreateGridColumnDef(1, "nome", true, null));

var btnsGrid = [
                ['detail', true, 'TipoPermissaoFilter.Methods.BtnDetalhes(this);'],
                ['edit', true, 'TipoPermissaoFilter.Methods.BtnEditar(this);'], 
                ['del', true, 'TipoPermissaoFilter.Methods.BtnExcluir(this);']
               ];

var table = LT_Grid.CreateGrid('SearchResultTable', "/TipoPermissao/RenderGrid", ['Nome'], columnDefs, btnsGrid);