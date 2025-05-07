using static FluentDocs.Infrastructure.Unit;

namespace FluentDocs.Infrastructure;

public enum Unit
{
    /// <summary>
    /// TWIP (twentieth of a point) is the standard unit for Office Open XML documents.
    /// 20 TWIP equals 1 point.
    /// </summary>
    Twip,
    Point,
    Meter,
    Centimetre,
    Millimetre,
    Feet,
    Inch,
    Emu
}

internal static class UnitExtensions
{
    private const float InchToCentimetre = 2.54f;
    private const float InchToPoints = 72;
    private const float InchToTwips = 1440f;
        
    public static float ToTwips(this float value, Unit unit)
    {
        return value * GetConversionFactor();
            
        float GetConversionFactor()
        {
            return unit switch
            {
                Twip => 1,
                Point => 20,
                Meter => (1f / 0.0254f) * InchToTwips,
                Centimetre => (1f / InchToCentimetre) * InchToTwips,
                Millimetre => (0.1f / InchToCentimetre) * InchToTwips,
                Feet => 12f * InchToTwips,
                Inch => InchToTwips,
                Emu => 1f / 635f,
                _ => throw new ArgumentOutOfRangeException(nameof(unit), unit, null)
            };
        }
    }
    
    public static float ToPoints(this float value, Unit unit)
    {
        return value * GetConversionFactor();
            
        float GetConversionFactor()
        {
            return unit switch
            {
                Twip => 1 / 20f,
                Point => 1,
                Meter => 100 / InchToCentimetre * InchToPoints,
                Centimetre => 1 / InchToCentimetre * InchToPoints,
                Millimetre => 0.1f / InchToCentimetre * InchToPoints,
                Feet => 12 * InchToPoints,
                Inch => InchToPoints,
                Emu => 1f / 12700f,
                _ => throw new ArgumentOutOfRangeException(nameof(unit), unit, null)
            };
        }
    }
    
    public static float ToEmu(this float value, Unit unit)
    {
        return value * GetConversionFactor();
            
        float GetConversionFactor()
        {
            return unit switch
            {
                Twip => 635f,
                Point => 12700f,
                Meter => (1f / 0.0254f) * 914400f,
                Centimetre => 1f / InchToCentimetre * 914400f,
                Millimetre => 0.1f / InchToCentimetre * 914400f,
                Feet => 12f * 914400f,
                Inch => 914400f,
                Emu => 1f,
                _ => throw new ArgumentOutOfRangeException(nameof(unit), unit, null)
            };
        }
    }
}