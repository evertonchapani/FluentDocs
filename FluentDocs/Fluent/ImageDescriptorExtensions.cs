using System.Diagnostics;
using FluentDocs.Descriptors;
using FluentDocs.Infrastructure;

namespace FluentDocs.Fluent;

public static class ImageDescriptorExtensions
{
    /// <summary>
    /// Draws an image by decoding it from a provided byte array.
    /// </summary>
    public static ImageDescriptor Image(this ContainerDescriptor parent, byte[] imageData)
    {
        var image = Infrastructure.Image.FromBinaryData(imageData);
        return parent.Image(image);
    }
    
    /// <summary>
    /// Draws the image loaded from a file located at the provided path.
    /// </summary>
    public static ImageDescriptor Image(this ContainerDescriptor parent, string filePath)
    {
        var image = StaticImageCache.Load(filePath);
        return parent.Image(image);
    }
    
    /// <summary>
    /// Draws the image loaded from a stream.
    /// </summary>
    public static ImageDescriptor Image(this ContainerDescriptor parent, Stream fileStream)
    {
        var image = Infrastructure.Image.FromStream(fileStream);
        return parent.Image(image);
    }

    private static ImageDescriptor Image(this ContainerDescriptor parent, Image image)
    {
        if (image == null)
            throw new Exception("Cannot load or decode provided image.");
            
        var imageElement = new Elements.Image
        {
            DocumentImage = image
        };

        parent.Element(imageElement);

        return new ImageDescriptor
        {
            ImageElement = imageElement
        };
    }


    #region Alignment
    public static ImageDescriptor AlignLeft(this ImageDescriptor descriptor)
    {
        Debug.Assert(descriptor.ImageElement != null, "Image element should not be null");
        
        descriptor.ImageElement.Alignment = TextHorizontalAlignment.Left;
        return descriptor;
    }
    
    public static ImageDescriptor AlignCenter(this ImageDescriptor descriptor)
    {
        Debug.Assert(descriptor.ImageElement != null, "Image element should not be null");
        
        descriptor.ImageElement.Alignment = TextHorizontalAlignment.Center;
        return descriptor;
    }
    
    public static ImageDescriptor AlignRight(this ImageDescriptor descriptor)
    {
        Debug.Assert(descriptor.ImageElement != null, "Image element should not be null");
        
        descriptor.ImageElement.Alignment = TextHorizontalAlignment.Right;
        return descriptor;
    }
    #endregion
    
    #region Size
    public static ImageDescriptor Width(this ImageDescriptor descriptor, float width, Unit unit = Unit.Twip)
    {
        Debug.Assert(descriptor.ImageElement != null, "Image element should not be null");
        
        descriptor.ImageElement.Width = (long)width.ToEmu(unit);
        
        return descriptor;
    }
    
    public static ImageDescriptor Height(this ImageDescriptor descriptor, float height, Unit unit = Unit.Twip)
    {
        Debug.Assert(descriptor.ImageElement != null, "Image element should not be null");
        
        descriptor.ImageElement.Height = (long)height.ToEmu(unit);
        
        return descriptor;
    }
    #endregion
}