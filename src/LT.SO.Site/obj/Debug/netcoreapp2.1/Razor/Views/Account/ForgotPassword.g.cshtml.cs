#pragma checksum "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "67e70ba0e961eba78f8f9c9e1a79acca073828d6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_ForgotPassword), @"mvc.1.0.view", @"/Views/Account/ForgotPassword.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/ForgotPassword.cshtml", typeof(AspNetCore.Views_Account_ForgotPassword))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67e70ba0e961eba78f8f9c9e1a79acca073828d6", @"/Views/Account/ForgotPassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"318740067bd8aac9da4af0d50bcccf81f825619f", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_ForgotPassword : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LT.SO.Site.Models.Account.ForgotPasswordModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Scripts/Shared/Helpers.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Scripts/Account/ForgotPassword.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("pace-top"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(54, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml"
  
    Layout = null;
    ViewData["Title"] = "Recuperar senha";

#line default
#line hidden
            BeginContext(127, 29, true);
            WriteLiteral("\r\n<!DOCTYPE html>\r\n\r\n<html>\r\n");
            EndContext();
            BeginContext(156, 2451, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "51304a501191440ab49f96c65df54dd9", async() => {
                BeginContext(162, 1754, true);
                WriteLiteral(@"
    <meta charset=""utf-8"" />
    <title>LTSO | Página de Login</title>
    <meta content=""width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"" name=""viewport"" />
    <meta content="""" name=""description"" />
    <meta content="""" name=""author"" />

    <!-- ================== BEGIN BASE CSS STYLE ================== -->
    <link href=""http://fonts.googleapis.com/css?family=Nunito:400,300,700"" rel=""stylesheet"" id=""fontFamilySrc"" />
    <link href=""../assets/plugins/jquery-ui/jquery-ui.min.css"" rel=""stylesheet"" />
    <link href=""../assets/plugins/bootstrap/bootstrap-4.1.1/css/bootstrap.min.css"" rel=""stylesheet"" />
    <link href=""../assets/plugins/font-awesome/5.1/css/all.css"" rel=""stylesheet"" />
    <link href=""../assets/css/animate.min.css"" rel=""stylesheet"" />
    <link href=""../assets/css/style.min.css"" rel=""stylesheet"" />
    <!-- ================== END BASE CSS STYLE ================== -->
    <!-- ================== BEGIN BASE JS ================== -->
    <script src=""");
                WriteLiteral(@"../assets/plugins/pace/pace.min.js""></script>
    <!-- ================== END BASE JS ================== -->
    <!--[if lt IE 9]>
        <script src=""../assets/crossbrowserjs/excanvas.min.js""></script>
    <![endif]-->

    <script src=""http://code.jquery.com/jquery-1.10.2.min.js""></script>
    <link href=""https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.0.1/css/toastr.css"" rel=""stylesheet"" />
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.0.1/js/toastr.js""></script>

    <script>
        function getBaseUrl() {
            var re = new RegExp(/^.*\//);
            return re.exec(window.location.href);
        }
        var baseUrl = 'http://localhost:54384';
    </script>

    ");
                EndContext();
                BeginContext(1916, 51, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "12657c5b366349799e432899ef3299ae", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1967, 633, true);
                WriteLiteral(@"

    <style>
        .pgLoadModal {
            display: none;
            position: fixed;
            z-index: 1000;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            background: rgba( 255, 255, 255, .8 ) url('http://localhost:54384/images/loading32x32.gif') 50% 50% no-repeat;
        }

        body.loading {
            overflow: hidden;
        }

            /* a partir do momento em que o body estiver com a classe loading,  o modal aparecerá */
            body.loading .pgLoadModal {
                display: block;
            }
    </style>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2607, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(2609, 3430, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6c8eafd0444a407bab66e545a8d2457b", async() => {
                BeginContext(2632, 727, true);
                WriteLiteral(@"
    <!-- begin #page-loader -->
    <div class=""pgLoadModal"">Carregando...</div>
    <div id=""page-loader"" class=""page-loader fade in""><span class=""spinner"">Loading...</span></div>
    <!-- end #page-loader -->
    <!-- begin #page-container -->
    <div id=""page-container"" class=""fade page-container"">
        <!-- begin login -->
        <div class=""login"">
            <!-- begin login-brand -->
            <div class=""login-brand bg-inverse text-white"">
                <img src=""../assets/img/logo-white.png"" height=""36"" class=""pull-right"" alt="""" /> Recuperar Senha
            </div>
            <!-- end login-brand -->
            <!-- begin login-content -->
            <div class=""login-content"">
");
                EndContext();
#line 85 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml"
                 using (Html.BeginForm("RecuperarSenha", "Account", FormMethod.Post, new { @class = "form-input-flat", @id = "ForgotPasswordForm" }))
                {
                    

#line default
#line hidden
                BeginContext(3550, 23, false);
#line 87 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml"
               Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
#line 87 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml"
                                            ;
                    

#line default
#line hidden
                BeginContext(3597, 28, false);
#line 88 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml"
               Write(Html.ValidationSummary(true));

#line default
#line hidden
                EndContext();
#line 88 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml"
                                                 ;

#line default
#line hidden
                BeginContext(3628, 70, true);
                WriteLiteral("                    <div class=\"form-group\">\r\n                        ");
                EndContext();
                BeginContext(3699, 26, false);
#line 90 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml"
                   Write(Html.LabelFor(m => m.Name));

#line default
#line hidden
                EndContext();
                BeginContext(3725, 26, true);
                WriteLiteral("\r\n                        ");
                EndContext();
                BeginContext(3752, 101, false);
#line 91 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml"
                   Write(Html.TextBoxFor(m => m.Name, null, new { @class = "form-control input-lg", placeholder = "Usuário" }));

#line default
#line hidden
                EndContext();
                BeginContext(3853, 26, true);
                WriteLiteral("\r\n                        ");
                EndContext();
                BeginContext(3880, 74, false);
#line 92 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml"
                   Write(Html.ValidationMessageFor(m => m.Name, "", new { @class = "text-danger" }));

#line default
#line hidden
                EndContext();
                BeginContext(3954, 344, true);
                WriteLiteral(@"
                    </div>
                    <div class=""row m-b-20"">
                        <div class=""col-lg-12"">
                            <input type=""button"" value=""Enviar e-mail"" onclick=""ForgotPassword.Methods.EnviarEmail();"" class=""btn btn-lime btn-lg btn-block"">
                        </div>
                    </div>
");
                EndContext();
#line 99 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml"
                }

#line default
#line hidden
                BeginContext(4317, 1299, true);
                WriteLiteral(@"            </div>
            <!-- end login-content -->
        </div>
        <!-- end login -->
    </div>
    <!-- end page container -->
    <!-- ================== BEGIN BASE JS ================== -->
    <script src=""../assets/plugins/jquery/jquery-3.3.1.min.js""></script>
    <script src=""../assets/plugins/jquery-ui/jquery-ui.min.js""></script>
    <script src=""../assets/plugins/bootstrap/bootstrap-4.1.1/js/bootstrap.bundle.min.js""></script>
    <!--[if lt IE 9]>
        <script src=""../assets/crossbrowserjs/html5shiv.js""></script>
        <script src=""../assets/crossbrowserjs/respond.min.js""></script>
    <![endif]-->
    <script src=""../assets/plugins/slimscroll/jquery.slimscroll.min.js""></script>
    <script src=""../assets/plugins/jquery-cookie/jquery.cookie.js""></script>
    <!-- ================== END BASE JS ================== -->
    <!-- ================== BEGIN PAGE LEVEL JS ================== -->
    <script src=""../assets/js/demo.min.js""></script>
    <script src=""../asset");
                WriteLiteral(@"s/js/apps.min.js""></script>
    <!-- ================== END PAGE LEVEL JS ================== -->

    <script>
        $(document).ready(function () {
            App.init();
            Demo.initThemePanel();
        });

        $(document).ready(function () {

");
                EndContext();
#line 130 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml"
             if (ViewBag.Result != null)
            {
                foreach(var msg in ViewBag.Result?.Messages)
                {
                    

#line default
#line hidden
                BeginContext(5775, 123, false);
#line 134 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml"
               Write(Html.Raw("Helpers.MessageValidation('"+ ViewBag.Result.MsgType + "', '"+ msg + "', '" + ViewBag.Result.CallBackUrl + "');"));

#line default
#line hidden
                EndContext();
#line 134 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Account\ForgotPassword.cshtml"
                                                                                                                                                ;
                }
            }

#line default
#line hidden
                BeginContext(5935, 34, true);
                WriteLiteral("        });\r\n    </script>\r\n\r\n    ");
                EndContext();
                BeginContext(5969, 59, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2268b4254452439ca3bc6d90f9476b61", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(6028, 4, true);
                WriteLiteral("\r\n\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(6039, 9, true);
            WriteLiteral("\r\n</html>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LT.SO.Site.Models.Account.ForgotPasswordModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
