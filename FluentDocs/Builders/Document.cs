using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentDocs.Descriptors;
using FluentDocs.Infrastructure;
using SectionProperties = DocumentFormat.OpenXml.Wordprocessing.SectionProperties;

namespace FluentDocs.Builders;

public class Document
{
    private readonly DocumentContext _context = new();
    private ContainerDescriptor? _headerContainer;
    private ContainerDescriptor? _contentContainer;
    private ContainerDescriptor? _footerContainer;

    public static Document Create() => new();

    public Document Page(Action<PageDescriptor> config)
    {
        var pageDescriptor = new PageDescriptor(_context);
        config(pageDescriptor);
        return this;
    }
    
    public Document Header(Action<ContainerDescriptor> config)
    {
        _headerContainer = new ContainerDescriptor(RenderingPartType.Header)
        {
            TextStyle = _context.Page.TextStyle with { }
        };
        config(_headerContainer);
        return this;
    }

    public Document Content(Action<ContainerDescriptor> config)
    {
        _contentContainer = new ContainerDescriptor(RenderingPartType.Main)
        {
            TextStyle = _context.Page.TextStyle with { }
        };
        config(_contentContainer);
        return this;
    }

    public Document Footer(Action<ContainerDescriptor> config)
    {
        _footerContainer = new ContainerDescriptor(RenderingPartType.Footer)
        {
            TextStyle = _context.Page.TextStyle with { }
        };
        config(_footerContainer);
        return this;
    }


    public void Save(string filePath)
    {
        using var wordDoc = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document);
        _context.Initialize(wordDoc);

        var mainPart = _context.MainPart!;
        var body = new Body();
        SectionProperties sectionProps = new();

        // Add header reference to section properties
        var headerReference = new HeaderReference
        {
            Type = HeaderFooterValues.Default,
            Id = _context.GetHeaderReferenceId()
        };
        sectionProps.Append(headerReference);

        // Add footer reference to section properties
        var footerReference = new FooterReference()
        {
            Type = HeaderFooterValues.Default,
            Id = _context.GetFooterReferenceId()
        };
        sectionProps.Append(footerReference);

        if (_headerContainer != null)
        {
            var headerElements = _headerContainer.Compose(_context);
            if (headerElements.Count > 0)
            {
                _context.HeaderPart!.Header.Append(headerElements);
                _context.HeaderPart.Header.Save();
            }
            else
            {
                sectionProps.RemoveChild(headerReference);
                mainPart.DeletePart(_context.HeaderPart!);
            }
        }

        if (_contentContainer != null)
        {
            var contentElements = _contentContainer.Compose(_context);
            foreach (var element in contentElements)
                body.Append(element);
        }
        
        if (_footerContainer != null)
        {
            var footerElements = _footerContainer.Compose(_context);
            if (footerElements.Count > 0)
            {
                _context.FooterPart!.Footer.Append(footerElements);
                _context.FooterPart.Footer.Save();
            }
            else
            {
                sectionProps.RemoveChild(footerReference);
                mainPart.DeletePart(_context.FooterPart!);
            }
        }

        sectionProps.Append(new PageSize
        {
            Width = (uint)_context.Page.Size.Width,
            Height = (uint)_context.Page.Size.Height,
            Orient = _context.Page.Size.Width > _context.Page.Size.Height ? PageOrientationValues.Landscape : PageOrientationValues.Portrait
        });
        
        sectionProps.Append(new PageMargin
        {
            Left = (uint)_context.Page.MarginLeft,
            Top = (int)_context.Page.MarginTop,
            Right = (uint)_context.Page.MarginRight,
            Bottom = (int)_context.Page.MarginBottom,
            //Header = (uint)1f.ToTwips(Unit.Centimetre),
            //Footer = (uint)1f.ToTwips(Unit.Centimetre)
        });
        
        body.Append(sectionProps);
        mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document(body);
        mainPart.Document.Save();
    }
}