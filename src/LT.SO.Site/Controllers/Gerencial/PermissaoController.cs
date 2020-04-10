using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using LT.SO.Site.Utils;
using LT.SO.Site.Models;
using LT.SO.Site.Models.Enum;
using LT.SO.Site.Models.Gerencial.Permissao;
using LT.SO.Site.Models.Gerencial.TipoPermissao;

namespace LT.SO.Site.Controllers.Gerencial
{
    public class PermissaoController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IViewRenderService _viewRenderService;
        private readonly string _permissaoPath = "permissoes/permissao/";

        public PermissaoController(IConfiguration configuration,
            IViewRenderService viewRenderService) : base(configuration)
        {
            _configuration = configuration;
            _viewRenderService = viewRenderService;
        }

        public IActionResult Index(ValidationMessage validationMessage = null)
        {
            if (validationMessage != null)
                ViewBag.Result = validationMessage;

            ViewBag.SLTipoPermissao = LoadTipoPermissaoSL();

            return View("~/Views/Gerencial/Permissao/Index.cshtml");
        }

        [HttpPost]
        public JsonResult RenderGrid(PermissaoFilter model)
        {
            int totalResultsCount = 0;

            var callResult = ServiceApi.Call(_configuration, HttpContext, _permissaoPath + "pesquisar", ServiceType.POST, model);
            List<PermissaoViewModel> permissoesLst = new List<PermissaoViewModel>();

            if (!callResult.Success)
            {
                return Json(new
                {
                    error = "Ocorreu um erro, não foi possível carregar os dados do Grid.",
                    draw = model.draw,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new List<PermissaoViewModel>()
                });
            }

            var paginatedResult = JsonConvert.DeserializeObject<PaginatedResult>(Convert.ToString(callResult.Data));
            totalResultsCount = paginatedResult.TotalRegistros;
            permissoesLst = JsonConvert.DeserializeObject<List<PermissaoViewModel>>(Convert.ToString(paginatedResult.LstRetorno));

            return Json(new
            {
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = totalResultsCount,
                data = permissoesLst.OrderBy(t => t.TipoNome).ToList()
            });
        }

        public ActionResult Cadastro(string id)
        {
            var model = new PermissaoViewModel();

            if (!string.IsNullOrEmpty(id))
            {
                var callResult = ServiceApi.Call(_configuration, HttpContext, _permissaoPath + id, ServiceType.GET, id, true);

                if (!callResult.Success)
                {
                    ViewBag.Result = new ValidationMessage(callResult);
                    return View("~/Views/Gerencial/Permissao/Index.cshtml");
                }

                model = JsonConvert.DeserializeObject<PermissaoViewModel>(Convert.ToString(callResult.Data));
            }

            ViewBag.SLTipoPermissao = LoadTipoPermissaoSL(model.TipoId);
            return View("~/Views/Gerencial/Permissao/Cadastro.cshtml", model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Save(PermissaoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Gerencial/Permissao/Cadastro.cshtml", model);
            }

            bool flEditar = (model.Id != null && model.Id != Guid.NewGuid()) ? true : false;
            string url = flEditar ? _permissaoPath + "editar" : _permissaoPath + "adicionar";

            var callResult = ServiceApi.Call(_configuration, HttpContext, url, flEditar ? ServiceType.PUT : ServiceType.POST, model, flEditar);
            var resultApi = new ValidationMessage(callResult, flEditar ? "Permissão editada com sucesso!!" : "Permissão adicionada com sucesso!!");

            ViewBag.Result = resultApi;

            if (resultApi.MsgType == 1)
            {
                return RedirectToAction("Index", resultApi);
            }
            else
            {
                return View("~/Views/Gerencial/Permissao/Cadastro.cshtml", model);
            }
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            var callResult = ServiceApi.Call(_configuration, HttpContext, _permissaoPath + "remover/" + id, ServiceType.DELETE, id, true, null, "id");
            var resultApi = new ValidationMessage(callResult, "Permissão deletada com sucesso!!");

            return Json(new { resultApi });
        }

        [HttpPost]
        public JsonResult Detalhes(string id)
        {
            var callResult = ServiceApi.Call(_configuration, HttpContext, _permissaoPath + id, ServiceType.GET, id, true);

            if (!callResult.Success)
            {
                var resultApi = new ValidationMessage(callResult);
                return Json(new { success = false, data = resultApi });
            }

            var model = JsonConvert.DeserializeObject<PermissaoViewModel>(Convert.ToString(callResult.Data));
            var result = _viewRenderService.RenderToStringAsync("~/Views/Gerencial/Permissao/_Detalhes.cshtml", model).Result;

            return Json(new { success = true, data = result });
        }


        private SelectList LoadTipoPermissaoSL(Guid? selectedVal = null)
        {
            var callResult = ServiceApi.Call(_configuration, HttpContext, "permissoes/tipo-permissao/todos", ServiceType.GET);
            var tipoPermissaoLst = new List<TipoPermissao>();

            if (!callResult.Success)
            {
                var resultApi = new ValidationMessage(callResult);
                return new SelectList(tipoPermissaoLst, "Id", "Nome");
            }

            tipoPermissaoLst.Add(new TipoPermissao() { Id = null, Nome = "Selecione..." });
            tipoPermissaoLst.AddRange(JsonConvert.DeserializeObject<List<TipoPermissao>>(Convert.ToString(callResult.Data)));

            return new SelectList(tipoPermissaoLst, "Id", "Nome", selectedValue: selectedVal);
        }
    }
}