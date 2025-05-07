using DocumentFormat.OpenXml;
using FluentDocs.Infrastructure;
using FluentDocs.Interfaces;

namespace FluentDocs.Descriptors;

public class ContainerDescriptor(RenderingPartType partType)
{
    private readonly List<IComposer<OpenXmlElement>> _builders = [];
    internal TextStyle TextStyle = TextStyle.LibraryDefault with { };

    public void Element(IDocumentElement element)
    {
        _builders.Add((IComposer<OpenXmlElement>)element);
    }
    
    private ContainerDescriptor DefaultTextStyle(TextStyle textStyle)
    {
        TextStyle = textStyle;
        return this;
    }
    
    public ContainerDescriptor DefaultTextStyle(Func<TextStyle, TextStyle> handler)
    {
        return DefaultTextStyle(handler(TextStyle.Default));
    }

    internal List<OpenXmlElement> Compose(DocumentContext context)
    {
        context.CurrentRenderingPart = partType;
        
        List<OpenXmlElement> elements = [];

        foreach (var builder in _builders)
            elements.Add(builder.Compose(context));

        return elements;
    }
}