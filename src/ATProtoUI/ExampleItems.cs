// <copyright file="ExampleItems.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Reflection;
using FishyFlip.Lexicon.App.Bsky.Feed;

namespace ATProtoUI;

/// <summary>
/// Gets examples from the embedded json folder.
/// </summary>
public static class ExampleItems
{
    /// <summary>
    /// Get a post from the embedded json folder.
    /// </summary>
    /// <param name="item">Post name.</param>
    /// <returns><see cref="GetPostThreadOutput"/>.</returns>
    /// <exception cref="Exception">Post not found.</exception>
    public static GetPostThreadOutput GetPost(string item = "post")
    {
        var postJson = GetResourceFileContentAsString("json." + item + ".json");
        if (string.IsNullOrEmpty(postJson))
        {
            throw new Exception("Post not found.");
        }

        return GetPostThreadOutput.FromJson(postJson);
    }

    private static string GetResourceFileContentAsString(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "ATProtoUI." + fileName;

        string? resource = null;
        using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream is null)
            {
                return string.Empty;
            }

            using StreamReader reader = new StreamReader(stream);
            resource = reader.ReadToEnd();
        }

        return resource ?? string.Empty;
    }
}