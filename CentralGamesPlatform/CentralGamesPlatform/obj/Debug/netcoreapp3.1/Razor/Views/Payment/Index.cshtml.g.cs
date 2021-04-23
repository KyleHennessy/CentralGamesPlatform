#pragma checksum "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "48dc39ad0aa0b572b03801ac041fa85df9f8df84"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Payment_Index), @"mvc.1.0.view", @"/Views/Payment/Index.cshtml")]
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
#nullable restore
#line 1 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\_ViewImports.cshtml"
using CentralGamesPlatform;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\_ViewImports.cshtml"
using CentralGamesPlatform.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\_ViewImports.cshtml"
using CentralGamesPlatform.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"48dc39ad0aa0b572b03801ac041fa85df9f8df84", @"/Views/Payment/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1bf453c22fb51f4ac0c332dd56ab74a36d5de64b", @"/Views/_ViewImports.cshtml")]
    public class Views_Payment_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PaymentSummaryViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("checkout-button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Payment", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreateCheckoutSession", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
  
    ViewData["Title"] = "Payment";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<script src=""https://js.stripe.com/v3/""></script>
<h1>Order Summary</h1>
<table class=""table table-bordered table-striped table-light"">
    <thead>
        <tr>
            <th>Selected Amount</th>
            <th>Game</th>
            <th class=""text-right"">Price</th>
            <th class=""text-right"">Sub Total</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 17 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
         foreach (var item in Model.ShoppingCart.ShoppingCartItems)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td class=\"text-center\">");
#nullable restore
#line 20 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
                                   Write(item.Amount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"text-left\">");
#nullable restore
#line 21 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
                                 Write(item.Game.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"text-right\">");
#nullable restore
#line 22 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
                                  Write(item.Game.Price.ToString("c"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td class=\"text-right\">\r\n                    ");
#nullable restore
#line 24 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
                Write((item.Amount * item.Game.Price).ToString("c"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 27 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n    <tfoot>\r\n        <tr>\r\n            <td colspan=\"3\" class=\"text-right\">Total</td>\r\n            <td class=\"text-right\">");
#nullable restore
#line 32 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
                              Write(Model.ShoppingCartTotal.ToString("c"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n    </tfoot>\r\n</table>\r\n\r\n\r\n<div class=\"card\">\r\n    <div class=\"card-body\">\r\n        <h5 class=\"card-title text-dark\">Billing Details</h5>\r\n        <p class=\"text-sm-left text-dark\">\r\n            First Name: ");
#nullable restore
#line 42 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
                   Write(Model.Order.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </p>\r\n        <p class=\"text-sm-left text-dark\">\r\n            Last Name: ");
#nullable restore
#line 45 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
                  Write(Model.Order.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </p>\r\n        <p class=\"text-sm-left text-dark\">\r\n            Phone Number: ");
#nullable restore
#line 48 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
                     Write(Model.Order.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </p>\r\n        <p class=\"text-sm-left text-dark\">\r\n            Address: ");
#nullable restore
#line 51 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
                Write(Model.Order.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </p>\r\n        <p class=\"text-sm-left text-dark\">\r\n            County: ");
#nullable restore
#line 54 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
               Write(Model.Order.County);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </p>\r\n        <p class=\"text-sm-left text-dark\">\r\n            EirCode: ");
#nullable restore
#line 57 "C:\Users\kyle-\source\repos\CentralGamesPlatform\CentralGamesPlatform\CentralGamesPlatform\Views\Payment\Index.cshtml"
                Write(Model.Order.EirCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </p>\r\n    </div>\r\n</div>\r\n<br/>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "48dc39ad0aa0b572b03801ac041fa85df9f8df8410656", async() => {
                WriteLiteral("Checkout");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

<script type=""text/javascript"">
    // Create an instance of the Stripe object with your publishable API key
    var stripe = Stripe('pk_test_51IB0Z4BTwx1LYfRREoDEBBJOh4HQM4tCzZgvmqRqnutsLFWU2rzPvaUT0pa7vTVp5WqKMMPxyx1nFzeeLjW4o7Eo00umgSxWfI');
    var checkoutButton = document.getElementById('checkout-button');

    checkoutButton.addEventListener('click', function () {
        // Create a new Checkout Session using the server-side endpoint you
        // created in step 3.
        fetch('/create-checkout-session', {
            method: 'POST',
        })
            .then(function (response) {
                return response.json();
            })
            .then(function (session) {
                return stripe.redirectToCheckout({ sessionId: session.id });
            })
            .then(function (result) {
                // If `redirectToCheckout` fails due to a browser or network
                // error, you should display the localized error message to your
               ");
            WriteLiteral(@" // customer using `error.message`.
                if (result.error) {
                    alert(result.error.message);
                }
            })
            .catch(function (error) {
                console.error('Error:', error);
            });
    });
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PaymentSummaryViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
