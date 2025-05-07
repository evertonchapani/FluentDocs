using SkiaSharp;

namespace FluentDocs.Infrastructure;

internal sealed class Image : IDisposable
{
    internal SKImage? SkImage { get; }
    internal long Size { get; }
    internal string Source { get; }
    
    internal Image(SKImage image, long size, string source)
    {
        SkImage = image;
        Size = size;
        Source = source;
    }

    public void Dispose()
    {
        SkImage?.Dispose();
    }
    
    #region public constructors
    /// <summary>
    /// Loads the image from binary data.
    /// </summary>
    public static Image FromBinaryData(byte[] imageBytes)
    {
        using var imageData = SKData.CreateCopy(imageBytes);
        return StaticImageCache.DecodeImage(imageData, isShared: true, imageData.Size);
    }

    /// <summary>
    /// Loads the image from a file with a specified path.
    /// </summary>
    public static Image FromFile(string filePath)
    {
        using var imageData = SKData.Create(filePath);
        return StaticImageCache.DecodeImage(imageData, isShared: true, imageData.Size);
    }

    /// <summary>
    /// Loads the image from a stream.
    /// </summary>
    public static Image FromStream(Stream stream)
    {
        using var imageData = SKData.Create(stream);
        return StaticImageCache.DecodeImage(imageData, isShared: true, imageData.Size);
    }
    #endregion
}