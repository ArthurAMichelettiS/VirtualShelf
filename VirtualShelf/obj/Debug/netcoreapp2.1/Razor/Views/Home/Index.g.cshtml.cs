#pragma checksum "C:\Users\lance\Downloads\VirtualShelf loginKAIAN\VirtualShelf login remaster\VirtualShelf\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e578b4c2a10fdf3f65d8f6f315aaa511f47f73c4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "C:\Users\lance\Downloads\VirtualShelf loginKAIAN\VirtualShelf login remaster\VirtualShelf\Views\_ViewImports.cshtml"
using VirtualShelf;

#line default
#line hidden
#line 2 "C:\Users\lance\Downloads\VirtualShelf loginKAIAN\VirtualShelf login remaster\VirtualShelf\Views\_ViewImports.cshtml"
using VirtualShelf.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e578b4c2a10fdf3f65d8f6f315aaa511f47f73c4", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0b4a676d771f108eb31729bb6ef9ca60c73ded51", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/unknown.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\lance\Downloads\VirtualShelf loginKAIAN\VirtualShelf login remaster\VirtualShelf\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
            BeginContext(45, 211, true);
            WriteLiteral("\r\n\r\n<div class=\"cont-about\">\r\n    <h1>Bem Vindo(a)!</h1>\r\n    <div class=\"cont-about\">Aqui é uma estante virtual de jogos, filmes e livros, onde você pode ver estantes de outros usuarios e fazer amizades</div>\r\n");
            EndContext();
#line 9 "C:\Users\lance\Downloads\VirtualShelf loginKAIAN\VirtualShelf login remaster\VirtualShelf\Views\Home\Index.cshtml"
     if (ViewBag.img3 != null)
    {

#line default
#line hidden
            BeginContext(295, 150, true);
            WriteLiteral("        <div class=\"row\">\r\n            <div class=\"col-md-4\">\r\n                <div class=\"cont-img center\">\r\n                    <img id=\"imgPreview\"");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 445, "\"", 503, 2);
            WriteAttributeValue("", 451, "data:image/jpeg;base64,", 451, 23, true);
#line 14 "C:\Users\lance\Downloads\VirtualShelf loginKAIAN\VirtualShelf login remaster\VirtualShelf\Views\Home\Index.cshtml"
WriteAttributeValue(" ", 474, ViewBag.Img1.ImagemEmBase64, 475, 28, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(504, 246, true);
            WriteLiteral(" class=\"img-responsive\"\r\n                         width=\"244\" height=\"300\">\r\n                </div>\r\n            </div>\r\n\r\n            <div class=\"col-md-4\">\r\n                <div class=\"cont-img center\">\r\n                    <img id=\"imgPreview\"");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 750, "\"", 808, 2);
            WriteAttributeValue("", 756, "data:image/jpeg;base64,", 756, 23, true);
#line 21 "C:\Users\lance\Downloads\VirtualShelf loginKAIAN\VirtualShelf login remaster\VirtualShelf\Views\Home\Index.cshtml"
WriteAttributeValue(" ", 779, ViewBag.Img2.ImagemEmBase64, 780, 28, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(809, 246, true);
            WriteLiteral(" class=\"img-responsive\"\r\n                         width=\"244\" height=\"300\">\r\n                </div>\r\n            </div>\r\n\r\n            <div class=\"col-md-4\">\r\n                <div class=\"cont-img center\">\r\n                    <img id=\"imgPreview\"");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 1055, "\"", 1113, 2);
            WriteAttributeValue("", 1061, "data:image/jpeg;base64,", 1061, 23, true);
#line 28 "C:\Users\lance\Downloads\VirtualShelf loginKAIAN\VirtualShelf login remaster\VirtualShelf\Views\Home\Index.cshtml"
WriteAttributeValue(" ", 1084, ViewBag.Img3.ImagemEmBase64, 1085, 28, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1114, 137, true);
            WriteLiteral(" class=\"img-responsive\"\r\n                         width=\"244\" height=\"300\">\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
            EndContext();
#line 33 "C:\Users\lance\Downloads\VirtualShelf loginKAIAN\VirtualShelf login remaster\VirtualShelf\Views\Home\Index.cshtml"

    }
    else
    {

#line default
#line hidden
            BeginContext(1277, 8, true);
            WriteLiteral("        ");
            EndContext();
            BeginContext(1285, 34, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "983cbd15c2e74e0785ae25c553c2a0f4", async() => {
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
            BeginContext(1319, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 38 "C:\Users\lance\Downloads\VirtualShelf loginKAIAN\VirtualShelf login remaster\VirtualShelf\Views\Home\Index.cshtml"
    }

#line default
#line hidden
            BeginContext(1328, 10, true);
            WriteLiteral("</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
