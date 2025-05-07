using FluentDocs.Descriptors;
using FluentDocs.Elements;
using FluentDocs.Infrastructure;

namespace FluentDocs.Fluent;

public static class TextDescriptorExtensions
{
    /// <summary>
    /// Creates a simple text element
    /// </summary>
    public static void Text(this ContainerDescriptor container, Action<TextDescriptor>? configure)
    {
        var descriptor = new TextDescriptor
        {
            TextBlock =
            {
                TextStyle = container.TextStyle with { }
            }
        };

        configure?.Invoke(descriptor);
        container.Element(descriptor.TextBlock);
    }
    
    /// <summary>
    /// Creates a clickable text that redirects the user to a specific webpage.
    /// </summary>
    public static TextSpanDescriptor Hyperlink(this TextDescriptor descriptor, string text, string url)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Text cannot be null or empty", nameof(text));
        
        if (string.IsNullOrEmpty(url))
            throw new ArgumentException("Url cannot be null or empty", nameof(url));
    
        var textBlockItem = new TextBlockHyperlink(text)
        {
            Url = url
        };

        descriptor.TextBlock.Items.Add(textBlockItem);
        return new TextSpanDescriptor
        {
            Span = textBlockItem
        };
    }
    
    #region Alignment
    public static T AlignLeft<T>(this T descriptor) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.AlignLeft);
        return descriptor;
    }
    
    public static T AlignCenter<T>(this T descriptor) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.AlignCenter);
        return descriptor;
    }
    
    public static T AlignRight<T>(this T descriptor) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.AlignRight);
        return descriptor;
    }
    
    public static T Justify<T>(this T descriptor) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.Justify);
        return descriptor;
    }
    #endregion
    
    #region Text style
    public static T Bold<T>(this T descriptor, bool bold = true) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.Bold, bold);
        return descriptor;
    }
    
    public static T Italic<T>(this T descriptor, bool italic = true) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.Italic, italic);
        return descriptor;
    }
    
    public static T Underline<T>(this T descriptor, bool underline = true) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.Underline, underline);
        return descriptor;
    }
    
    public static T FontFamily<T>(this T descriptor, string family) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.FontFamily, family);
        return descriptor;
    }
    
    public static T FontSize<T>(this T descriptor, float value) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.FontSize, value);
        return descriptor;
    }
    
    public static T FontColor<T>(this T descriptor, Color color) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.FontColor, color);
        return descriptor;
    }
    
    public static T Highlight<T>(this T descriptor, HighlightColor color) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.Highlight, color);
        return descriptor;
    }
    
    public static T LineSpacing<T>(this T descriptor, float lines) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.LineHeight, lines);
        return descriptor;
    }
    
    public static T ParagraphSpacing<T>(this T descriptor, float spacing, Unit unit = Unit.Twip) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.ParagraphSpacing, spacing.ToTwips(unit));
        return descriptor;
    }
    
    public static T FirstLineIndent<T>(this T descriptor, float indent, Unit unit = Unit.Twip) where T : TextDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.FirstLineIndent, indent.ToTwips(unit));
        return descriptor;
    }
    #endregion
}