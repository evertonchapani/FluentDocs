using FluentDocs.Helpers;

namespace FluentDocs.Fluent;

public static class PageSizeExtensions
{
    /// <summary>
    /// Sets page size to a portrait orientation, making the width smaller than the height.
    /// </summary>
    public static PageSize Portrait(this PageSize size)
    {
        return new PageSize(Math.Min(size.Width, size.Height), Math.Max(size.Width, size.Height));
    }

    /// <summary>
    /// Sets page size to a landscape orientation, making the width bigger than the height.
    /// </summary>
    public static PageSize Landscape(this PageSize size)
    {
        return new PageSize(Math.Max(size.Width, size.Height), Math.Min(size.Width, size.Height));
    }
}