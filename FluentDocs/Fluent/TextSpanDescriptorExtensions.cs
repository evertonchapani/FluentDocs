using FluentDocs.Descriptors;
using FluentDocs.Infrastructure;

namespace FluentDocs.Fluent;

public static class TextSpanDescriptorExtensions
{
    public static T Bold<T>(this T descriptor, bool bold = true) where T : TextSpanDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.Bold, bold);
        return descriptor;
    }
    
    public static T Italic<T>(this T descriptor, bool italic = true) where T : TextSpanDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.Italic, italic);
        return descriptor;
    }
    
    public static T Underline<T>(this T descriptor, bool underline = true) where T : TextSpanDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.Underline, underline);
        return descriptor;
    }
    
    public static T FontFamily<T>(this T descriptor, string family) where T : TextSpanDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.FontFamily, family);
        return descriptor;
    }
    
    public static T FontSize<T>(this T descriptor, float value) where T : TextSpanDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.FontSize, value);
        return descriptor;
    }
    
    public static T FontColor<T>(this T descriptor, Color color) where T : TextSpanDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.FontColor, color);
        return descriptor;
    }
    
    public static T Highlight<T>(this T descriptor, HighlightColor color) where T : TextSpanDescriptor
    {
        descriptor.MutateTextStyle(TextStyleExtensions.Highlight, color);
        return descriptor;
    }
}