namespace FluentDocs.Infrastructure;

public readonly struct Size(float width, float height, Unit unit = Unit.Twip)
{
    private const float Epsilon = 0.01f;
    private const float Infinity = 14_400;

    internal readonly float Width = width.ToTwips(unit);
    internal readonly float Height = height.ToTwips(unit);
        
    public static Size Zero { get; } = new(0, 0);
    public static Size Max { get; } = new(Infinity, Infinity);

    internal static bool Equal(Size first, Size second)
    {
        if (Math.Abs(first.Width - second.Width) > Epsilon)
            return false;
            
        if (Math.Abs(first.Height - second.Height) > Epsilon)
            return false;

        return true;
    }
        
    public override string ToString() => $"(Width: {Width:N3}, Height: {Height:N3})";
}