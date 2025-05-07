using DocumentFormat.OpenXml.Wordprocessing;
using FluentDocs.Helpers;

namespace FluentDocs.Infrastructure;

public record TextStyle
{
    public static TextStyle Default { get; } = new();
    internal static TextStyle LibraryDefault { get; } = new();

    /// <summary>
    /// The font family. Defaults to Arial.
    /// </summary>
    internal string Family { get; set; } = "Arial";
    
    /// <summary>
    /// The font size in points. Defaults to 12 points.
    /// </summary>
    internal float Size { get; set; } = 12;
    
    /// <summary>
    /// The line height in twips. Defaults to 1 line (240 twips).
    /// </summary>
    internal float LineHeight { get; set; } = 240;
    
    /// <summary>
    /// The paragraph spacing in twips. Defaults to 0.
    /// </summary>
    internal float ParagraphSpacing { get; set; } = 0;
    
    /// <summary>
    /// The paragraph alignment. Defaults to left.
    /// </summary>
    internal TextHorizontalAlignment Alignment { get; set; } = TextHorizontalAlignment.Left;
    
    /// <summary>
    /// The first line indentation in twips. Defaults to 0.
    /// </summary>
    internal float FirstLineIndent { get; set; } = 0;
    
    /// <summary>
    /// The font color. Defaults to black.
    /// </summary>
    internal Color FontColor { get; set; } = Colors.Black;
    
    /// <summary>
    /// The text highlight color. Defaults to none.
    /// </summary>
    internal HighlightColor Highlight { get; set; } = HighlightColor.None;
    
    /// <summary>
    /// Whether the text should be bold. Defaults to false.
    /// </summary>
    internal bool Bold { get; set; }
    
    /// <summary>
    /// Whether the text should be italic. Defaults to false.
    /// </summary>
    internal bool Italic { get; set; }
    
    /// <summary>
    /// Whether the text should be underlined. Defaults to false.
    /// </summary>
    internal bool Underline { get; set; }
}