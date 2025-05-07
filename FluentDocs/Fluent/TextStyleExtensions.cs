using FluentDocs.Infrastructure;

namespace FluentDocs.Fluent;

public static class TextStyleExtensions
{
    public static TextStyle AlignLeft(this TextStyle style) => style.Mutate(TextStyleProperty.Alignment, TextHorizontalAlignment.Left);
    public static TextStyle AlignCenter(this TextStyle style) => style.Mutate(TextStyleProperty.Alignment, TextHorizontalAlignment.Center);
    public static TextStyle AlignRight(this TextStyle style) => style.Mutate(TextStyleProperty.Alignment, TextHorizontalAlignment.Right);
    public static TextStyle Justify(this TextStyle style) => style.Mutate(TextStyleProperty.Alignment, TextHorizontalAlignment.Justify);
    
    public static TextStyle Bold(this TextStyle style, bool bold = true) => style.Mutate(TextStyleProperty.Bold, bold);
    public static TextStyle Italic(this TextStyle style, bool italic = true) => style.Mutate(TextStyleProperty.Italic, italic);
    public static TextStyle Underline(this TextStyle style, bool underline = true) => style.Mutate(TextStyleProperty.Underline, underline);
    
    public static TextStyle FontFamily(this TextStyle style, string family)
    {
        if (string.IsNullOrEmpty(family))
            throw new ArgumentException("Font family must be informed.");
            
        return style.Mutate(TextStyleProperty.Family, family);
    }
    
    public static TextStyle FontSize(this TextStyle style, float value)
    {
        if (value <= 0)
            throw new ArgumentException("Font size must be greater than 0.");
            
        return style.Mutate(TextStyleProperty.Size, value);
    }
    
    public static TextStyle FontColor(this TextStyle style, Color color)
    {
        return style
            .Mutate(TextStyleProperty.FontColor, color);
    }
    
    public static TextStyle Highlight(this TextStyle style, HighlightColor color)
    {
        return style
            .Mutate(TextStyleProperty.Highlight, color);
    }
    
    public static TextStyle LineHeight(this TextStyle style, float lines)
    {
        if (lines < 0)
            throw new ArgumentException("Line height must be greater or equal 0.");
            
        return style.Mutate(TextStyleProperty.LineHeight, lines * 240);
    }
    
    public static TextStyle ParagraphSpacing(this TextStyle style, float spacing)
    {
        if (spacing < 0)
            throw new ArgumentException("Paragraph spacing must be greater or equal 0.");
            
        return style.Mutate(TextStyleProperty.ParagraphSpacing, spacing);
    }
    
    public static TextStyle FirstLineIndent(this TextStyle style, float indent)
    {
        if (indent < 0)
            throw new ArgumentException("First line indent must be greater or equal 0.");
            
        return style.Mutate(TextStyleProperty.FirstLineIndent, indent);
    }
}