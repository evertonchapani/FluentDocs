using System.Diagnostics;
using FluentDocs.Elements;
using FluentDocs.Infrastructure;

namespace FluentDocs.Descriptors;

public class TextSpanDescriptor
{
    internal TextBlockSpan? Span { get; init; }
    
    internal void MutateTextStyle<T>(Func<TextStyle, T, TextStyle> handler, T argument)
    {
        Debug.Assert(Span != null, "Span should not be null");
        
        Span.TextStyle = handler(Span.TextStyle, argument);
    }
    
    internal void MutateTextStyle(Func<TextStyle, TextStyle> handler)
    {
        Debug.Assert(Span != null, "Span should not be null");
        
        Span.TextStyle = handler(Span.TextStyle);
    }
}