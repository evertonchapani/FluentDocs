﻿using System.Collections.Concurrent;
using SkiaSharp;

namespace FluentDocs.Infrastructure;

static class StaticImageCache
{
    private static bool CacheIsEnabled { get; set; } = true;
    private static ConcurrentDictionary<string, Image> Items { get; set; } = new();

    private const int MaxCacheSize = 25_000_000;
    private const int MaxItemSize = 1_000_000;
    
    public static Image Load(string filePath)
    {
        var isPathRooted = Path.IsPathRooted(filePath);
        
        // check fallback path
        if (!File.Exists(filePath))
        {
            var fallbackPath = Path.Combine(Helpers.Helpers.ApplicationFilesPath, filePath);

            if (!File.Exists(fallbackPath))
                throw new Exception($"Cannot load provided image, file not found: {filePath}");   

            filePath = fallbackPath;
        }
        
        if (isPathRooted)
            return LoadImage(filePath, false);
        
        // check file size
        var fileInfo = new FileInfo(filePath);
        
        if (fileInfo.Length > MaxItemSize)
            return LoadImage(filePath, false);
        
        // check if the image is already in the cache
        if (Items.TryGetValue(filePath, out var cacheItem))
            return cacheItem;
        
        // if the cache is larger than expected, the usage might be different from loading static images
        if (!CacheIsEnabled)
            return LoadImage(filePath, false);
        
        // create a new cache item and add it to the cache
        var image = LoadImage(filePath, true);
        Items.TryAdd(filePath, image);
        
        // check cache size
        CacheIsEnabled = Items.Values.Sum(x => x.Size) < MaxCacheSize;
        
        // return cached value
        return image;
    }
    
    private static Image LoadImage(string filePath, bool isShared)
    {
        using var imageData = SKData.Create(filePath);
        return DecodeImage(imageData, isShared, imageData.Size, filePath);
    }
    
    public static Image DecodeImage(SKData imageData, bool isShared, long size, string source = "")
    {
        try
        {
            var skImage = SKImage.FromEncodedData(imageData);
            var image = new Image(skImage, size, source);
            
            return image;
        }
        catch
        {
            throw new Exception("Cannot decode the provided image.");
        }
    }
}