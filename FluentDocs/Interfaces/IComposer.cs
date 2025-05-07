using FluentDocs.Infrastructure;

namespace FluentDocs.Interfaces;

internal interface IComposer<out T>
{
    T Compose(DocumentContext context);
}