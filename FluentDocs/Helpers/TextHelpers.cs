using DocumentFormat.OpenXml.Wordprocessing;
using FluentDocs.Infrastructure;

namespace FluentDocs.Helpers;

internal static class TextHelpers
{
    internal static JustificationValues MapAlignment(TextHorizontalAlignment alignment)
    {
        return alignment switch
        {
            TextHorizontalAlignment.Center => JustificationValues.Center,
            TextHorizontalAlignment.Right => JustificationValues.Right,
            TextHorizontalAlignment.Justify => JustificationValues.Both,
            _ => JustificationValues.Left
        };
    }
    
    internal static HighlightColorValues MapHighlight(HighlightColor color)
    {
        return color switch
        {
            HighlightColor.Black => HighlightColorValues.Black,
            HighlightColor.Blue => HighlightColorValues.Blue,
            HighlightColor.Cyan => HighlightColorValues.Cyan,
            HighlightColor.Green => HighlightColorValues.Green,
            HighlightColor.Magenta => HighlightColorValues.Magenta,
            HighlightColor.Red => HighlightColorValues.Red,
            HighlightColor.Yellow => HighlightColorValues.Yellow,
            HighlightColor.White => HighlightColorValues.White,
            HighlightColor.DarkBlue => HighlightColorValues.DarkBlue,
            HighlightColor.DarkCyan => HighlightColorValues.DarkCyan,
            HighlightColor.DarkGreen => HighlightColorValues.DarkGreen,
            HighlightColor.DarkMagenta => HighlightColorValues.DarkMagenta,
            HighlightColor.DarkRed => HighlightColorValues.DarkRed,
            HighlightColor.DarkYellow => HighlightColorValues.DarkYellow,
            HighlightColor.DarkGray => HighlightColorValues.DarkGray,
            HighlightColor.LightGray => HighlightColorValues.LightGray,
            _ => HighlightColorValues.None
        };
    }
}