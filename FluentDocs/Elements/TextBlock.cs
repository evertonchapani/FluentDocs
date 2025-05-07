using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentDocs.Helpers;
using FluentDocs.Infrastructure;
using FluentDocs.Interfaces;

namespace FluentDocs.Elements;

internal class TextBlock : IComposer<OpenXmlElement>, IDocumentElement
{
    public List<TextBlockSpan> Items { get; } = [];
    internal TextStyle TextStyle { get; set; } = TextStyle.LibraryDefault with { };

    public OpenXmlElement Compose(DocumentContext context)
    {
        var paragraph = new Paragraph(GetParagraphProperties());
        
        foreach (var descriptor in Items)
        {
            var run = descriptor.Compose(context);
            paragraph.Append(run);
        }
        
        return paragraph;
    }

    public ParagraphProperties GetParagraphProperties()
    {
        return new ParagraphProperties
        {
            Justification = new Justification { Val = TextHelpers.MapAlignment(TextStyle.Alignment) },
            SpacingBetweenLines = new SpacingBetweenLines
            {
                Line = TextStyle.LineHeight.ToString(CultureInfo.InvariantCulture),
                LineRule = LineSpacingRuleValues.Auto,
                After = TextStyle.ParagraphSpacing.ToString(CultureInfo.InvariantCulture)
            },
            Indentation = new Indentation
            {
                FirstLine = TextStyle.FirstLineIndent.ToString(CultureInfo.InvariantCulture)
            }
        };
    }
}