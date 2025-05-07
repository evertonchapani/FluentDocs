using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentDocs.Elements;

namespace FluentDocs.Infrastructure;

public class DocumentContext
{
    internal WordprocessingDocument? Document { get; private set; }
    internal MainDocumentPart? MainPart { get; private set; }
    internal HeaderPart? HeaderPart { get; private set; }
    internal FooterPart? FooterPart { get; private set; }

    internal RenderingPartType CurrentRenderingPart { get; set; } = RenderingPartType.Main;
    
    internal Page Page { get; } = new();
    
    internal void Initialize(WordprocessingDocument document)
    {
        Document = document;
        MainPart = document.AddMainDocumentPart();
        HeaderPart = MainPart.AddNewPart<HeaderPart>();
        HeaderPart.Header = new Header();
        HeaderPart.Header.Save();
        
        FooterPart = MainPart.AddNewPart<FooterPart>();
        FooterPart.Footer = new Footer();
        FooterPart.Footer.Save();
    }
    
    internal string GetHeaderReferenceId() => MainPart!.GetIdOfPart(HeaderPart!);
    internal string GetFooterReferenceId() => MainPart!.GetIdOfPart(FooterPart!);
    
    internal OpenXmlPartContainer GetCurrentPart()
    {
        return CurrentRenderingPart switch
        {
            RenderingPartType.Header => HeaderPart!,
            RenderingPartType.Footer => FooterPart!,
            _ => MainPart!
        };
    }
}