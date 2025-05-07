using System.Reflection;

namespace FluentDocs.Helpers;

internal static class Helpers
{
    internal static string ApplicationFilesPath
    {
        get
        {
            var baseDirectory = AppContext.BaseDirectory;

            if (string.IsNullOrWhiteSpace(baseDirectory) || baseDirectory == "/")
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;

            return baseDirectory;
        }
    }
}