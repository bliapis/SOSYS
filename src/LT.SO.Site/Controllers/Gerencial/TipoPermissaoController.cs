using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using LT.SO.Site.Utils;
using LT.SO.Site.Models;
using LT.SO.Site.Services;
using LT.SO.Site.Models.Enum;
using LT.SO.Site.Models.Gerencial.TipoPermissao;
using Refit;
using Newtonsoft.Json;

namespace LT.SO.Site.Controllers.Gerencial
{
    public class TipoPermissaoController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IViewRenderService _viewRenderService;

        public TipoPermissaoController(IConfiguration configuration,
            IViewRenderService viewRenderService) : base(configuration)
        {
            _configuration = configuration;
            _viewRenderService = viewRenderService;
        }

        public IActionResult Index(ValidationMessage validationMessage = null)
        {
            if (validationMessage != null)
                ViewBag.Result = validationMessage;

            return View("~/Views/Gerencial/TipoPermissao/Index.cshtml");
        }

        [HttpPost]
        public JsonResult RenderGrid(TipoPermissaoFilter model)
        {
            int totalResultsCount = 0;

            var tipoPermissaoService = RestService.For<ITipoPermissaoService>(ApiAddress);
            ServiceResult resultService;
            List<TipoPermissao> permissoesLst = new List<TipoPermissao>();

            try
            {
                resultService = tipoPermissaoService.Pesquisar(model).Result;

                if (!resultService.Success)
                {
                    ViewBag.ErrorMsgs = resultService.Erros;
                }

                var paginatedResult = JsonConvert.DeserializeObject<PaginatedResult>(Convert.ToString(resultService.Data));
                totalResultsCount = paginatedResult.TotalRegistros;
                permissoesLst = JsonConvert.DeserializeObject<List<TipoPermissao>>(Convert.ToString(paginatedResult.LstRetorno));
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    error = "Ocorreu um erro, não foi possível carregar os dados do Grid."
                });
            }

            return Json(new
            {
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = totalResultsCount,
                data = permissoesLst.OrderBy(t => t.Nome).ToList()
            });
        }

        public ActionResult Cadastro(string id)
        {
            var model = new TipoPermissao();

            if (!string.IsNullOrEmpty(id))
            {
                var callResult = ServiceApi.Call(_configuration, HttpContext, "permissoes/tipo-permissao/" + id, ServiceType.GET, id, true);

                if (!callResult.Success)
                {
                    ViewBag.Result = new ValidationMessage(callResult);
                    return View("~/Views/Gerencial/TipoPermissao/Index.cshtml");
                }

                model = JsonConvert.DeserializeObject<TipoPermissao>(Convert.ToString(callResult.Data));
            }

            return View("~/Views/Gerencial/TipoPermissao/Cadastro.cshtml", model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Save(TipoPermissao model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Gerencial/TipoPermissao/Cadastro.cshtml", model);
            }

            bool flEditar = (model.Id != null && model.Id != Guid.NewGuid()) ? true : false;
            string url = flEditar ? "permissoes/tipo-permissao/editar" : "permissoes/tipo-permissao/adicionar";

            var callResult = ServiceApi.Call(_configuration, HttpContext, url, flEditar ? ServiceType.PUT : ServiceType.POST, model, flEditar);
            var resultApi = new ValidationMessage(callResult, flEditar ? "Tipo Permissão editado com sucesso!!" : "Tipo Permissão adicionado com sucesso!!");

            //resultApi.CallBackUrl = "/TipoPermissao/Index";
            ViewBag.Result = resultApi;

            if (resultApi.MsgType == 1)
            {
                return RedirectToAction("Index", resultApi);
                //return View("~/Views/Gerencial/TipoPermissao/Index.cshtml", new TipoPermissaoFilter());
            }
            else
            {
                return View("~/Views/Gerencial/TipoPermissao/Cadastro.cshtml", model);
            }
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            var callResult = ServiceApi.Call(_configuration, HttpContext, "permissoes/tipo-permissao/remover/"+id, ServiceType.DELETE, id, true, null, "id");
            var resultApi = new ValidationMessage(callResult, "Tipo Permissão deletado com sucesso!!");

            return Json(new { resultApi });
        }

        [HttpPost]
        public JsonResult Detalhes(string id)
        {
            var callResult = ServiceApi.Call(_configuration, HttpContext, "permissoes/tipo-permissao/" + id, ServiceType.GET, id, true);

            if (!callResult.Success)
            {
                var resultApi = new ValidationMessage(callResult);
                return Json(new { success = false, data = resultApi });
            }

            var model = JsonConvert.DeserializeObject<TipoPermissao>(Convert.ToString(callResult.Data));
            var result = _viewRenderService.RenderToStringAsync("~/Views/Gerencial/TipoPermissao/_Detalhes.cshtml", model).Result;

            return Json(new { success = true, data = result });
        }
    }
}