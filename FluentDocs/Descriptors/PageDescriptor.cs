using FluentDocs.Helpers;
using FluentDocs.Infrastructure;

namespace FluentDocs.Descriptors;

public class PageDescriptor(DocumentContext context)
{
    /// <summary>
    /// Configures the dimensions of the page.
    /// </summary>
    public PageDescriptor Size(float width, float height, Unit unit = Unit.Twip)
    {
        var size = new PageSize(width, height, unit);
        context.Page.Size = size;
        return this;
    }
    
    /// <summary>
    /// Configures the dimensions of the page.
    /// </summary>
    public PageDescriptor Size(Size size)
    {
        context.Page.Size = size;
        return this;
    }

    /// <summary>
    /// Configures the margin of the page.
    /// </summary>
    public PageDescriptor Margin(float margin, Unit unit = Unit.Twip)
    {
        margin = margin.ToTwips(unit);
        context.Page.MarginTop = margin;
        context.Page.MarginRight = margin;
        context.Page.MarginLeft = margin;
        context.Page.MarginBottom = margin;
        
        return this;
    }
    
    /// <summary>
    /// Configures the top margin of the page.
    /// </summary>
    public PageDescriptor MarginTop(float margin, Unit unit = Unit.Twip)
    {
        context.Page.MarginTop = margin.ToTwips(unit);
        return this;
    }
    
    /// <summary>
    /// Configures the right margin of the page.
    /// </summary>
    public PageDescriptor MarginRight(float margin, Unit unit = Unit.Twip)
    {
        context.Page.MarginRight = margin.ToTwips(unit);
        return this;
    }
    
    /// <summary>
    /// Configures the bottom margin of the page.
    /// </summary>
    public PageDescriptor MarginBottom(float margin, Unit unit = Unit.Twip)
    {
        context.Page.MarginBottom = margin.ToTwips(unit);
        return this;
    }
    
    /// <summary>
    /// Configures the left margin of the page.
    /// </summary>
    public PageDescriptor MarginLeft(float margin, Unit unit = Unit.Twip)
    {
        context.Page.MarginLeft = margin.ToTwips(unit);
        return this;
    }
    
    /// <summary>
    /// Configures the vertical margin (top and bottom) of the page.
    /// </summary>
    public PageDescriptor MarginVertical(float margin, Unit unit = Unit.Twip)
    {
        margin = margin.ToTwips(unit);
        
        context.Page.MarginTop = margin;
        context.Page.MarginBottom = margin;
        
        return this;
    }
    
    /// <summary>
    /// Configures the horizontal margin (left and right) of the page.
    /// </summary>
    public PageDescriptor MarginHorizontal(float margin, Unit unit = Unit.Twip)
    {
        margin = margin.ToTwips(unit);
        
        context.Page.MarginLeft = margin;
        context.Page.MarginRight = margin;
        
        return this;
    }

    private PageDescriptor DefaultTextStyle(TextStyle textStyle)
    {
        context.Page.TextStyle = textStyle;
        return this;
    }
    
    public PageDescriptor DefaultTextStyle(Func<TextStyle, TextStyle> handler)
    {
        DefaultTextStyle(handler(TextStyle.Default));
        return this;
    }
}