using System.Diagnostics;
using System.Reflection;

namespace FluentDocs.Infrastructure;

internal enum TextStyleProperty
{
    Family,
    Size,
    LineHeight,
    Alignment,
    ParagraphSpacing,
    FirstLineIndent,
    FontColor,
    Highlight,
    Bold,
    Italic,
    Underline
}

internal static class TextStyleManager
{
    public static TextStyle Mutate(this TextStyle origin, TextStyleProperty targetProperty, object value)
    {
        var property = typeof(TextStyle).GetProperty(targetProperty.ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        Debug.Assert(property != null);
        
        var oldValue = property.GetValue(origin);
                
        if (oldValue == value) 
            return origin;
        
        var newTextStyle = origin with { };
        property.SetValue(newTextStyle, value);

        return newTextStyle;
    }
}