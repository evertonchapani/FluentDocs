using FluentDocs.Descriptors;
using FluentDocs.Elements;
using FluentDocs.Infrastructure;

namespace FluentDocs.Fluent;

public static class TextBlockDescriptorExtensions
{
    /// <summary>
    /// Creates a simple text element
    /// </summary>
    public static TextBlockDescriptor Text(this ContainerDescriptor container, string value)
    {
        var textBlock = new TextBlock
        {
            TextStyle = container.TextStyle with { }
        };

        var textSpan = new TextBlockSpan(value)
        {
            TextStyle = textBlock.TextStyle
        };
        textBlock.Items.Add(textSpan);
        
        var textBlockDescriptor = new TextBlockDescriptor
        {
            TextBlock = textBlock,
            Span = textSpan
        };
        
        container.Element(textBlock);
        return textBlockDescriptor;
    }
    
    public static void EmptyLine(this ContainerDescriptor container, int quantity = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 1, nameof(quantity));
        
        foreach (var _ in Enumerable.Range(0, quantity))
        {
            var textBlock = new TextBlock
            {
                TextStyle = container.TextStyle with { }
            };

            var textSpan = new TextBlockSpan("")
            {
                TextStyle = textBlock.TextStyle
            };
            textBlock.Items.Add(textSpan);

            container.Element(textBlock);
        }
    }

    /// <summary>
    /// Creates a clickable text that redirects the user to a specific webpage.
    /// </summary>
    public static TextBlockDescriptor Hyperlink(this ContainerDescriptor container, string text, string url)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Text cannot be null or empty", nameof(text));
        
        if (string.IsNullOrEmpty(url))
            throw new ArgumentException("Url cannot be null or empty", nameof(url));
        
        var textBlock = new TextBlock
        {
            TextStyle = container.TextStyle with { }
        };

        var textSpan = new TextBlockHyperlink(text)
        {
            TextStyle = textBlock.TextStyle,
            Url = url
        };
        textBlock.Items.Add(textSpan);
        
        var textBlockDescriptor = new TextBlockDescriptor
        {
            TextBlock = textBlock,
            Span = textSpan
        };
        
        container.Element(textBlock);
        return textBlockDescriptor;
    }
    
    #region Alignment
    public static T AlignLeft<T>(this T descriptor) where T : TextBlockDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.AlignLeft);
        return descriptor;
    }
    
    public static T AlignCenter<T>(this T descriptor) where T : TextBlockDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.AlignCenter);
        return descriptor;
    }
    
    public static T AlignRight<T>(this T descriptor) where T : TextBlockDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.AlignRight);
        return descriptor;
    }
    
    public static T Justify<T>(this T descriptor) where T : TextBlockDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.Justify);
        return descriptor;
    }
    #endregion
    
    #region Text style
    public static T LineSpacing<T>(this T descriptor, float lines) where T : TextBlockDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.LineHeight, lines);
        return descriptor;
    }
    
    public static T ParagraphSpacing<T>(this T descriptor, float spacing, Unit unit = Unit.Twip) where T : TextBlockDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.ParagraphSpacing, spacing.ToTwips(unit));
        return descriptor;
    }
    
    public static T FirstLineIndent<T>(this T descriptor, float indent, Unit unit = Unit.Twip) where T : TextBlockDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.FirstLineIndent, indent.ToTwips(unit));
        return descriptor;
    }
    #endregion
}