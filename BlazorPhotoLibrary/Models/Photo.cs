// <copyright file="Photo.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace BlazorPhotoLibrary.Models;

using System.Text.Json.Serialization;

/// <summary>
/// The model for photos retrieved from the repository.
/// </summary>
public class Photo
{
    /// <summary>
    /// Gets or sets the album ID.
    /// </summary>
    [JsonPropertyName("albumId")]
    public int AlbumId { get; set; }

    /// <summary>
    /// Gets or sets the photo's ID.
    /// </summary>
    [JsonPropertyName("id")]
    public int PhotoId { get; set; }

    /// <summary>
    /// Gets or sets the photo's title.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL for the image.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL for the thumbnail image.
    /// </summary>
    [JsonPropertyName("thumbnailUrl")]
    public string ThumbnailUrl { get; set; } = string.Empty;
}
