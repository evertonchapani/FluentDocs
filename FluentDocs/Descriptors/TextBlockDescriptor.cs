using System.Diagnostics;
using FluentDocs.Elements;
using FluentDocs.Infrastructure;

namespace FluentDocs.Descriptors;

public class TextBlockDescriptor : TextSpanDescriptor
{
    internal TextBlock? TextBlock { get; init; }
    
    internal new void MutateTextStyle<T>(Func<TextStyle, T, TextStyle> handler, T argument)
    {
        Debug.Assert(TextBlock != null, "TextBlock should not be null");
        
        TextBlock.TextStyle = handler(TextBlock.TextStyle, argument);
    }
    
    internal new void MutateTextStyle(Func<TextStyle, TextStyle> handler)
    {
        Debug.Assert(TextBlock != null, "TextBlock should not be null");
        
        TextBlock.TextStyle = handler(TextBlock.TextStyle);
    }
}