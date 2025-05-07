using FluentDocs.Fluent;
using FluentDocs.Infrastructure;

namespace FluentDocs.Helpers;

/// <summary>
/// Defines the physical dimensions (width and height) of a page.
/// </summary>
/// <remarks>
/// <para>Commonly used page sizes are available in the <see cref="PageSizes"/> class.</para>
/// <para>Change page orientation with the <see cref="PageSizeExtensions.Portrait">Portrait</see> and <see cref="PageSizeExtensions.Landscape">Landscape</see> extension methods.</para>
/// </remarks>
/// <example>
/// <c>PageSizes.A4.Landscape();</c>
/// </example>
public sealed class PageSize
{
    public readonly float Width;
    public readonly float Height;

    public PageSize(float width, float height, Unit unit = Unit.Twip)
    {
        if (width < 0)
            throw new ArgumentOutOfRangeException(nameof(width), "Page width must be greater than 0.");

        if (height < 0)
            throw new ArgumentOutOfRangeException(nameof(height), "Page height must be greater than 0.");

        Width = width.ToTwips(unit);
        Height = height.ToTwips(unit);
    }

    public static implicit operator Size(PageSize pageSize) => new(pageSize.Width, pageSize.Height);
}

/// <summary>
/// Contains a collection of predefined, common and standard page sizes, such as A4 with dimensions of 595 points in width and 842 points in height.
/// </summary>
public static class PageSizes
{
    public const int PointsPerInch = 72;

    public static PageSize A0 => new(2384, 3370, Unit.Point);
    public static PageSize A1 => new(1684, 2384, Unit.Point);
    public static PageSize A2 => new(1191, 1684, Unit.Point);
    public static PageSize A3 => new(842, 1191, Unit.Point);
    public static PageSize A4 => new(595, 842, Unit.Point);
    public static PageSize A5 => new(420, 595, Unit.Point);
    public static PageSize A6 => new(298, 420, Unit.Point);
    public static PageSize A7 => new(210, 298, Unit.Point);
    public static PageSize A8 => new(147, 210, Unit.Point);
    public static PageSize A9 => new(105, 147, Unit.Point);
    public static PageSize A10 => new(74, 105, Unit.Point);

    public static PageSize B0 => new(2835, 4008, Unit.Point);
    public static PageSize B1 => new(2004, 2835, Unit.Point);
    public static PageSize B2 => new(1417, 2004, Unit.Point);
    public static PageSize B3 => new(1001, 1417, Unit.Point);
    public static PageSize B4 => new(709, 1001, Unit.Point);
    public static PageSize B5 => new(499, 709, Unit.Point);
    public static PageSize B6 => new(354, 499, Unit.Point);
    public static PageSize B7 => new(249, 354, Unit.Point);
    public static PageSize B8 => new(176, 249, Unit.Point);
    public static PageSize B9 => new(125, 176, Unit.Point);
    public static PageSize B10 => new(88, 125, Unit.Point);

    public static PageSize C0 => new(2599, 3677, Unit.Point);
    public static PageSize C1 => new(1837, 2599, Unit.Point);
    public static PageSize C2 => new(1298, 1837, Unit.Point);
    public static PageSize C3 => new(918, 1298, Unit.Point);
    public static PageSize C4 => new(649, 918, Unit.Point);
    public static PageSize C5 => new(459, 649, Unit.Point);
    public static PageSize C6 => new(323, 459, Unit.Point);
    public static PageSize C7 => new(230, 323, Unit.Point);
    public static PageSize C8 => new(162, 230, Unit.Point);
    public static PageSize C9 => new(113, 162, Unit.Point);
    public static PageSize C10 => new(79, 113, Unit.Point);

    public static PageSize Env10 => new(297, 684, Unit.Point);
    public static PageSize EnvC4 => new(649, 918, Unit.Point);
    public static PageSize EnvDL => new(312, 624, Unit.Point);

    public static PageSize Postcard => new(284, 419, Unit.Point);
    public static PageSize Executive => new(522, 756, Unit.Point);
    public static PageSize Letter => new(612, 792, Unit.Point);
    public static PageSize Legal => new(612, 1008, Unit.Point);
    public static PageSize Ledger => new(792, 1224, Unit.Point);
    public static PageSize Tabloid => new(1224, 792, Unit.Point);

    public static PageSize ARCH_A => new(648, 864, Unit.Point);
    public static PageSize ARCH_B => new(864, 1296, Unit.Point);
    public static PageSize ARCH_C => new(1296, 1728, Unit.Point);
    public static PageSize ARCH_D => new(1728, 2592, Unit.Point);
    public static PageSize ARCH_E => new(2592, 3456, Unit.Point);
    public static PageSize ARCH_E1 => new(2160, 3024, Unit.Point);
    public static PageSize ARCH_E2 => new(1872, 2736, Unit.Point);
    public static PageSize ARCH_E3 => new(1944, 2808, Unit.Point);
}