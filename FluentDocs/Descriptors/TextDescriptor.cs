using FluentDocs.Elements;
using FluentDocs.Infrastructure;

namespace FluentDocs.Descriptors;

public class TextDescriptor
{
    internal TextBlock TextBlock { get; } = new();

    public TextSpanDescriptor Span(string text)
    {
        var textSpan = new TextBlockSpan(text)
        {
            TextStyle = TextBlock.TextStyle with { }
        };

        TextBlock.Items.Add(textSpan);
        return new TextSpanDescriptor
        {
            Span = textSpan
        };
    }
    
    internal void MutateTextStyle<T>(Func<TextStyle, T, TextStyle> handler, T argument)
    {
        TextBlock.TextStyle = handler(TextBlock.TextStyle, argument);
    }
    
    internal void MutateTextStyle(Func<TextStyle, TextStyle> handler)
    {
        TextBlock.TextStyle = handler(TextBlock.TextStyle);
    }
}