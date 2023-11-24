using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GlobalSolution.Views.Shared.TagHelpers
{
    [HtmlTargetElement("imagem-redonda", Attributes = "caminho")]
    public class ImagemRedondaTagHelper : TagHelper
    {
        public string Caminho { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.Attributes.SetAttribute("src", Caminho);
            output.Attributes.SetAttribute("alt", "Imagem Redonda");
            output.Attributes.SetAttribute("class", "img-fluid rounded-circle mb-3");
            output.Attributes.SetAttribute("style", "max-width: 300px;");
        }
    }
}
