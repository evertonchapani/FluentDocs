using System.Diagnostics;
using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentDocs.Helpers;
using FluentDocs.Infrastructure;
using FluentDocs.Interfaces;
using SkiaSharp;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Path = System.IO.Path;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace FluentDocs.Elements;

internal class Image : IComposer<OpenXmlElement>, IDocumentElement
{
    public Infrastructure.Image? DocumentImage { get; init; }
    public long? Width { get; set; }
    public long? Height { get; set; }
    public TextHorizontalAlignment Alignment { get; set; } = TextHorizontalAlignment.Left;

    public OpenXmlElement Compose(DocumentContext context)
    {
        Debug.Assert(DocumentImage != null, "Document image should not be null");
        Debug.Assert(DocumentImage.SkImage != null, "SkImage should not be null");
        
        var part = context.GetCurrentPart();
        
        ImagePart imagePart;
        var imageFormat = GetImageFormat(DocumentImage.Source);
        
        if (part is MainDocumentPart mainPart)
            imagePart = mainPart.AddImagePart(GetImagePartType(imageFormat));
        else if (part is HeaderPart headerPart)
            imagePart = headerPart.AddImagePart(GetImagePartType(imageFormat));
        else if (part is FooterPart footerPart)
            imagePart = footerPart.AddImagePart(GetImagePartType(imageFormat));
        else
            throw new Exception("Can't add image to this part");

        using var data = DocumentImage.SkImage.Encode(imageFormat, 100).AsStream();
        imagePart.FeedData(data);

        string relationshipId = part.GetIdOfPart(imagePart);
        var drawing = CreateDrawingElement(relationshipId, DocumentImage.Source, context.Page);

        ParagraphProperties properties = new ParagraphProperties
        {
            Justification = new Justification { Val = TextHelpers.MapAlignment(Alignment) },
            SpacingBetweenLines = new SpacingBetweenLines
            {
                Line = TextStyle.LibraryDefault.LineHeight.ToString(CultureInfo.InvariantCulture),
                LineRule = LineSpacingRuleValues.Auto,
                After = TextStyle.LibraryDefault.ParagraphSpacing.ToString(CultureInfo.InvariantCulture)
            }
        };
        
        return new Paragraph(properties, new Run(drawing));
    }

    /// <summary>
    /// Gets the appropriate image format based on the image name/extension
    /// </summary>
    private static SKEncodedImageFormat GetImageFormat(string imageName)
    {
        if (string.IsNullOrEmpty(imageName))
            return SKEncodedImageFormat.Png; // Default to PNG

        var extension = Path.GetExtension(imageName).ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" => SKEncodedImageFormat.Jpeg,
            ".gif" => SKEncodedImageFormat.Gif,
            ".bmp" => SKEncodedImageFormat.Bmp,
            ".webp" => SKEncodedImageFormat.Webp,
            _ => SKEncodedImageFormat.Png // Default to PNG for unknown formats
        };
    }
    
    /// <summary>
    /// Gets the appropriate image part type based on the image format
    /// </summary>
    private static string GetImagePartType(SKEncodedImageFormat format)
    {
        return format switch
        {
            SKEncodedImageFormat.Jpeg => "image/jpeg",
            SKEncodedImageFormat.Gif => "image/gif",
            SKEncodedImageFormat.Bmp => "image/bmp",
            _ => "image/png" // Default to PNG for other formats
        };
    }

    private Drawing CreateDrawingElement(string relationshipId, string imagePath, Page pageSettings)
    {
        long widthEmu;
        long heightEmu;

        if (Width.HasValue || Height.HasValue)
        {
            if (Width.HasValue && Height.HasValue)
            {
                widthEmu = Width.Value;
                heightEmu = Height.Value;
            }
            else if (Width.HasValue)
            {
                widthEmu = Width.Value;
                
                // Keep aspect ratio
                float aspectRatio = (float)DocumentImage!.SkImage!.Height / DocumentImage!.SkImage!.Width;
                heightEmu = (long)(widthEmu * aspectRatio);
            }
            else
            {
                heightEmu = Height!.Value;

                // Keep aspect ratio
                float aspectRatio = (float)DocumentImage!.SkImage!.Width / DocumentImage!.SkImage!.Height;
                widthEmu = (int)(heightEmu * aspectRatio);
            }
        }
        else
        {
            // Use original image dimensions
            float dpiAdjustment = 96f / 72f; // Convert from 96 DPI to 72 DPI (points)
            widthEmu = (int)(DocumentImage!.SkImage!.Width * dpiAdjustment * 12700);
            heightEmu = (int)(DocumentImage!.SkImage!.Height * dpiAdjustment * 12700);

            // Apply max width/height constraints if needed
            long maxWidthEmu = (long)(pageSettings.Size.Width - pageSettings.MarginLeft - pageSettings.MarginRight).ToEmu(Unit.Twip);
            if (widthEmu > maxWidthEmu)
            {
                float scale = (float)maxWidthEmu / widthEmu;
                widthEmu = maxWidthEmu;
                heightEmu = (int)(heightEmu * scale);
            }
        }
        
        var inline = new DW.Inline(
            new DW.Extent() { Cx = widthEmu, Cy = heightEmu },
            new DW.EffectExtent()
            {
                LeftEdge = 0,
                TopEdge = 0,
                RightEdge = 0,
                BottomEdge = 0
            },
            new DW.DocProperties()
            {
                Id = 1,
                Name = Path.GetFileNameWithoutExtension(imagePath)
            },
            new DW.NonVisualGraphicFrameDrawingProperties(
                new A.GraphicFrameLocks() { NoChangeAspect = true }),
            new A.Graphic(
                new A.GraphicData(
                    new PIC.Picture(
                        new PIC.NonVisualPictureProperties(
                            new PIC.NonVisualDrawingProperties()
                            {
                                Id = 0,
                                Name = Path.GetFileName(imagePath)
                            },
                            new PIC.NonVisualPictureDrawingProperties()),
                        new PIC.BlipFill(
                            new A.Blip()
                            {
                                Embed = relationshipId,
                                CompressionState = A.BlipCompressionValues.Print
                            },
                            new A.Stretch(new A.FillRectangle())),
                        new PIC.ShapeProperties(
                            new A.Transform2D(
                                new A.Offset() { X = 0, Y = 0 },
                                new A.Extents() { Cx = widthEmu, Cy = heightEmu }),
                            new A.PresetGeometry(
                                new A.AdjustValueList()
                            ) { Preset = A.ShapeTypeValues.Rectangle }))
                ) { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
        )
        {
            DistanceFromTop = 0,
            DistanceFromBottom = 0,
            DistanceFromLeft = 0,
            DistanceFromRight = 0
        };

        return new Drawing(inline);
    }
}