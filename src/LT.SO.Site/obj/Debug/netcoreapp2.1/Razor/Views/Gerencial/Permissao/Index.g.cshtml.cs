#pragma checksum "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a0f6906e5ac74427469b2e1a743d59bad7c97da8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Gerencial_Permissao_Index), @"mvc.1.0.view", @"/Views/Gerencial/Permissao/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Gerencial/Permissao/Index.cshtml", typeof(AspNetCore.Views_Gerencial_Permissao_Index))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\Projects\SOSYS\src\LT.SO.Site\Views\_ViewImports.cshtml"
using LT.SO.Site;

#line default
#line hidden
#line 2 "D:\Projects\SOSYS\src\LT.SO.Site\Views\_ViewImports.cshtml"
using LT.SO.Site.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a0f6906e5ac74427469b2e1a743d59bad7c97da8", @"/Views/Gerencial/Permissao/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"318740067bd8aac9da4af0d50bcccf81f825619f", @"/Views/_ViewImports.cshtml")]
    public class Views_Gerencial_Permissao_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LT.SO.Site.Models.Gerencial.Permissao.PermissaoFilter>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("TipoId"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "TipoId", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control input"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Scripts/Gerencial/Permissao/Permissao.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(62, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\Index.cshtml"
  
    ViewData["Title"] = "Permissão";

#line default
#line hidden
            BeginContext(109, 615, true);
            WriteLiteral(@"
<div id=""content"" class=""panel"">

    <h1 class=""panel panel-heading"">Permissão</h1>

    <div class=""panel-body"">
        <div class=""row"">
            <div class=""col-md-12"">
                <div style=""float: right;"">
                    <span id=""spanSHFiltros"" class=""btn btn-white btn-xs"" onclick=""Permissao.Methods.MostrarEsconderFiltros();"">Esconder filtros</span><i id=""iconHSFiltros"" class=""fa fa-sort-down"" aria-hidden=""true""></i>
                </div>
            </div>

            <div id=""filtros"" class=""card col-md-12"">
                <div class=""card-body"">
                    ");
            EndContext();
            BeginContext(725, 23, false);
#line 21 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\Index.cshtml"
               Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(748, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(771, 28, false);
#line 22 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\Index.cshtml"
               Write(Html.ValidationSummary(true));

#line default
#line hidden
            EndContext();
            BeginContext(799, 158, true);
            WriteLiteral("\r\n\r\n                    <div class=\"row\">\r\n                        <div class=\"form-group col-xl-3 col-lg-6 col-md-8 col-sm-12\">\r\n                            ");
            EndContext();
            BeginContext(958, 27, false);
#line 26 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\Index.cshtml"
                       Write(Html.LabelFor(m => m.Valor));

#line default
#line hidden
            EndContext();
            BeginContext(985, 30, true);
            WriteLiteral("\r\n                            ");
            EndContext();
            BeginContext(1016, 97, false);
#line 27 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\Index.cshtml"
                       Write(Html.TextBoxFor(m => m.Valor, null, new { @class = "form-control input", placeholder = "Valor" }));

#line default
#line hidden
            EndContext();
            BeginContext(1113, 151, true);
            WriteLiteral("\r\n                        </div>\r\n\r\n                        <div class=\"form-group col-xl-3 col-lg-6 col-md-8 col-sm-12\">\r\n                            ");
            EndContext();
            BeginContext(1265, 28, false);
#line 31 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\Index.cshtml"
                       Write(Html.LabelFor(m => m.TipoId));

#line default
#line hidden
            EndContext();
            BeginContext(1293, 30, true);
            WriteLiteral("\r\n                            ");
            EndContext();
            BeginContext(1323, 124, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f94afc6db54e46c885294a40d30ada7f", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
#line 32 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.TipoId);

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 32 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = ViewBag.SLTipoPermissao;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1447, 1276, true);
            WriteLiteral(@"
                        </div>
                    </div>

                    <div class=""row"">
                        <div class=""col-lg-12"">
                            <button id='btnPesquisarPermissao' onclick=""Permissao.Methods.PesquisarPermissao();"" class='btn btn-success'><i class='fa fa-search'> Pesquisar</i></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <br /><br />

        <div class=""col-md-12"">
            <div style=""float: right;"">
                <span id=""btnNovo"" class=""btn btn-success m-b-5"" onclick=""Permissao.Methods.CriarNovo();""><i class=""fa fa-plus-square"" aria-hidden=""true""> Criar Novo</i></span>
            </div>
        </div>

        <div class=""row col-md-12"">
            <table id=""SearchResultTable"" class=""display responsive"" style=""width:100%"">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>TipoId<");
            WriteLiteral("/th>\r\n                        <th>Permissão</th>\r\n                        <th>Tipo Permissão</th>\r\n                        <th>Ações</th>\r\n                    </tr>\r\n                </thead>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
            BeginContext(2724, 29, false);
#line 69 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\Index.cshtml"
Write(Html.Partial("_MostrarErros"));

#line default
#line hidden
            EndContext();
            BeginContext(2753, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            BeginContext(2757, 66, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "70f05b3f73f743c4bca200f9f6ca95a3", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LT.SO.Site.Models.Gerencial.Permissao.PermissaoFilter> Html { get; private set; }
    }
}
#pragma warning restore 1591
