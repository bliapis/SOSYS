#pragma checksum "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\_Detalhes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ceb4d105d4a53db85aa8bf7b8be2c1c559e54b63"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Gerencial_Permissao__Detalhes), @"mvc.1.0.view", @"/Views/Gerencial/Permissao/_Detalhes.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Gerencial/Permissao/_Detalhes.cshtml", typeof(AspNetCore.Views_Gerencial_Permissao__Detalhes))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ceb4d105d4a53db85aa8bf7b8be2c1c559e54b63", @"/Views/Gerencial/Permissao/_Detalhes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"318740067bd8aac9da4af0d50bcccf81f825619f", @"/Views/_ViewImports.cshtml")]
    public class Views_Gerencial_Permissao__Detalhes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LT.SO.Site.Models.Gerencial.Permissao.PermissaoViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(65, 59, true);
            WriteLiteral("\r\n<div class=\"form-group\">\r\n    <div class=\"row\">\r\n        ");
            EndContext();
            BeginContext(125, 27, false);
#line 5 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\_Detalhes.cshtml"
   Write(Html.LabelFor(m => m.Valor));

#line default
#line hidden
            EndContext();
            BeginContext(152, 2, true);
            WriteLiteral(": ");
            EndContext();
            BeginContext(155, 33, false);
#line 5 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\_Detalhes.cshtml"
                                 Write(Html.DisplayTextFor(m => m.Valor));

#line default
#line hidden
            EndContext();
            BeginContext(188, 45, true);
            WriteLiteral("\r\n    </div>\r\n    <div class=\"row\">\r\n        ");
            EndContext();
            BeginContext(234, 30, false);
#line 8 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\_Detalhes.cshtml"
   Write(Html.LabelFor(m => m.TipoNome));

#line default
#line hidden
            EndContext();
            BeginContext(264, 2, true);
            WriteLiteral(": ");
            EndContext();
            BeginContext(267, 36, false);
#line 8 "D:\Projects\SOSYS\src\LT.SO.Site\Views\Gerencial\Permissao\_Detalhes.cshtml"
                                    Write(Html.DisplayTextFor(m => m.TipoNome));

#line default
#line hidden
            EndContext();
            BeginContext(303, 20, true);
            WriteLiteral("\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LT.SO.Site.Models.Gerencial.Permissao.PermissaoViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
