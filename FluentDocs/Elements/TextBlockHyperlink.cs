namespace FluentDocs.Elements;

internal class TextBlockHyperlink(string text) : TextBlockSpan(text)
{
    public string? Url { get; init; }
}