using System.Diagnostics;
using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentDocs.Helpers;
using FluentDocs.Infrastructure;
using FluentDocs.Interfaces;
using Color = DocumentFormat.OpenXml.Wordprocessing.Color;

namespace FluentDocs.Elements;

internal class TextBlockSpan(string text) : IComposer<OpenXmlCompositeElement>
{
    private string Text { get; } = text;
    internal TextStyle TextStyle { get; set; } = TextStyle.LibraryDefault with { };

    public OpenXmlCompositeElement Compose(DocumentContext context)
    {
        var runProps = new RunProperties();
        
        if (TextStyle.Bold)
            runProps.Bold = new Bold();
        
        if (TextStyle.Italic)
            runProps.Italic = new Italic();
        
        if (TextStyle.Underline)
            runProps.Underline = new Underline { Val = UnderlineValues.Single };

        runProps.Color = new Color { Val = TextStyle.FontColor.ToString() };
        runProps.FontSize = new FontSize { Val = (TextStyle.Size * 2).ToString(CultureInfo.InvariantCulture) };
        runProps.RunFonts = new RunFonts
        {
            Ascii = TextStyle.Family,
            HighAnsi = TextStyle.Family,
            ComplexScript = TextStyle.Family,
            EastAsia = TextStyle.Family
        };
        runProps.Highlight = new Highlight { Val = TextHelpers.MapHighlight(TextStyle.Highlight) };

        var text = new Text(Text) { Space = SpaceProcessingModeValues.Preserve };
        var run = new Run(text)
        {
            RunProperties = runProps
        };

        var part = context.GetCurrentPart();
        if (this is TextBlockHyperlink hyperlink)
        {
            Debug.Assert(!string.IsNullOrEmpty(hyperlink.Url), "Hyperlink URL should not be null");
            
            var rel = part.AddHyperlinkRelationship(new Uri(hyperlink.Url), true);

            var wordLink = new Hyperlink(run)
            {
                History = OnOffValue.FromBoolean(true),
                Id = rel.Id
            };

            run.RunProperties.RunStyle = new RunStyle { Val = "Hyperlink" };

            return wordLink;
        }

        return run;
    }
}