using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudyFlow.Web.Helpers;

/// <summary>
/// Razor görünümlerinde kullanılan özel HtmlHelper genişletme metotları.
/// Yerleşik submit butonu yardımcısı bulunmadığından TagBuilder ile güvenli ve
/// metni otomatik HTML-encode eden bir yardımcı sağlar.
/// </summary>
public static class StudyFlowHtmlHelperExtensions
{
    /// <summary>
    /// TagBuilder kullanarak güvenli bir &lt;button type="submit"&gt; öğesi üretir.
    /// <paramref name="buttonText"/> değeri InnerHtml.Append ile eklendiğinden otomatik olarak HTML-encode edilir.
    /// </summary>
    /// <param name="htmlHelper">Mevcut IHtmlHelper örneği.</param>
    /// <param name="buttonText">Buton üzerinde gösterilecek metin (otomatik encode edilir).</param>
    /// <param name="cssClass">Butona uygulanacak CSS sınıfları.</param>
    public static IHtmlContent SubmitButton(
        this IHtmlHelper htmlHelper,
        string buttonText,
        string cssClass = "sf-btn sf-btn-primary")
    {
        ArgumentNullException.ThrowIfNull(htmlHelper);

        var buttonTag = new TagBuilder("button");
        buttonTag.Attributes["type"] = "submit";

        if (!string.IsNullOrWhiteSpace(cssClass))
        {
            buttonTag.AddCssClass(cssClass);
        }

        // InnerHtml.Append(string) metni otomatik olarak HTML-encode eder (XSS koruması).
        buttonTag.InnerHtml.Append(buttonText ?? string.Empty);

        return buttonTag;
    }
}
