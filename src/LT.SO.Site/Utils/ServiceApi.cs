using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using LT.SO.Site.Models;
using LT.SO.Site.Models.Enum;

namespace LT.SO.Site.Utils
{
    public static class ServiceApi
    {
        private static IConfiguration _configuration;

        public static ServiceResult Call(IConfiguration configuration, HttpContext httpContext, 
                                         string urlService, ServiceType metodo, object parametros = null, 
                                         bool queryString = false, string token = null, string queryStringParamName = "model")
        {
            _configuration = configuration;

            string HtmlResult = string.Empty;
            string url = _configuration.GetSection("ApiAddress").Value + "/api/v1/" + urlService; //TODO: Quando tiver só um tipo de chamada na API, deixar o trexo /api/v1 no json config
            string parametro = JsonConvert.SerializeObject(parametros);

            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("Content-Type", "application/json");
                    wc.Headers[HttpRequestHeader.Authorization] = string.Format("bearer {0}", 
                                                                    string.IsNullOrEmpty(token) 
                                                                    ? Convert.ToString(httpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault()?.Value)
                                                                    : token);

                    if (queryString)
                        wc.QueryString.Add(queryStringParamName, parametro);

                    switch (metodo)
                    {
                        case ServiceType.POST:
                            HtmlResult = wc.UploadString(url, "POST", parametro);
                            break;
                        case ServiceType.PUT:
                            HtmlResult = wc.UploadString(url, "PUT", parametro);
                            break;
                        case ServiceType.GET:
                            HtmlResult = wc.DownloadString(url);
                            break;
                        case ServiceType.DELETE:
                            HtmlResult = wc.UploadString(url, "DELETE", "");
                            break;
                    }

                }
                catch (Exception e)
                {
                    var resultError = new ServiceResult();
                    resultError.Erros = new List<string>();
                    resultError.Erros.Add("Ocorreu um erro enquanto contatava o servidor.");
                    resultError.Success = false;
                    return resultError;
                }
            }

            if (metodo == ServiceType.GET)
            {
                var obj = JsonConvert.DeserializeObject<ServiceResult>(Convert.ToString(HtmlResult));

                if (obj.Success)
                {
                    if (obj.Data == null || Convert.ToString(obj.Data) == "[]")
                    {
                        var resultError = new ServiceResult();
                        resultError.Erros = new List<string>();
                        resultError.Erros.Add(obj.Data == null ? "O Registro não foi encontrado." : "Ocorreu ao realizar a pesquisa no servidor.");
                        resultError.Success = false;
                        return resultError;
                    }
                }
            }

            return JsonConvert.DeserializeObject<ServiceResult>(Convert.ToString(HtmlResult/*Encoding.UTF8.GetString(Encoding.GetEncoding("iso-8859-1").GetBytes(HtmlResult))*/));
        }
    }
}