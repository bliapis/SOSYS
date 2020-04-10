using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;

namespace LT.SO.Site.Controllers
{
    public class BaseController : Controller
    {
        private IConfiguration _configuration;

        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;

            ApiAddress = _configuration.GetSection("ApiAddress").Value;
        }

        protected string ApiAddress { get; set; }


        protected void SetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        protected string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }
    }
}