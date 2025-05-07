using FluentDocs.Helpers;
using FluentDocs.Infrastructure;

namespace FluentDocs.Elements;

internal class Page
{
    /// <summary>
    /// The margin top in twips. Defaults to 0.
    /// </summary>
    public float MarginTop { get; set; }

    /// <summary>
    /// The margin right in twips. Defaults to 0.
    /// </summary>
    public float MarginRight { get; set; }
    
    /// <summary>
    /// The margin bottom in twips. Defaults to 0.
    /// </summary>
    public float MarginBottom { get; set; }
    
    /// <summary>
    /// The margin left in twips. Defaults to 0.
    /// </summary>
    public float MarginLeft { get; set; }

    /// <summary>
    /// The page size. Defaults to A4.
    /// </summary>
    public Size Size { get; set; } = PageSizes.A4;
    
    internal TextStyle TextStyle { get; set; } = TextStyle.LibraryDefault with { };
}