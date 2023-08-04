// <copyright file="PhotoTests.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace BlazorPhotoLibraryTests.Models;

using System.Text.Json;
using BlazorPhotoLibrary.Models;

/// <summary>
/// Unit tests for <see cref="Photo"/>.
/// </summary>
public class PhotoTests
{
    /// <summary>
    /// Tests that the JSON properties are being properly formatted upon deserialization.
    /// </summary>
    [Fact]
    public void Photo_Deserialization()
    {
        // Setup Fixtures.
        Photo _expected = new()
        {
            AlbumId = 1,
            PhotoId = 2,
            ThumbnailUrl = "test_thumbnail_url",
            Title = "test_title",
            Url = "test_url",
        };
        string _serialized = "{" +
                                $"\"albumId\":{_expected.AlbumId}," +
                                 $"\"id\":{_expected.PhotoId}," +
                                 $"\"title\":\"{_expected.Title}\"," +
                                 $"\"thumbnailUrl\":\"{_expected.ThumbnailUrl}\"," +
                                 $"\"url\":\"{_expected.Url}\"" +
                             "}";

        // Execute SUT.
        Photo _result = JsonSerializer.Deserialize<Photo>(_serialized) !;

        // Verify Results.
        Assert.Equal(_expected.AlbumId, _result.AlbumId);
        Assert.Equal(_expected.PhotoId, _result.PhotoId);
        Assert.Equal(_expected.ThumbnailUrl, _result.ThumbnailUrl);
        Assert.Equal(_expected.Title, _result.Title);
        Assert.Equal(_expected.Url, _result.Url);
    }

    /// <summary>
    /// Tests that the JSON properties are being properly formatted upon serialization.
    /// </summary>
    [Fact]
    public void Photo_Serialization()
    {
        // Setup Fixtures.
        Photo _photo = new()
        {
            AlbumId = 1,
            PhotoId = 2,
            ThumbnailUrl = "test_thumbnail_url",
            Title = "test_title",
            Url = "test_url",
        };

        // Execute SUT.
        string _result = JsonSerializer.Serialize(_photo);

        // Verify Results.
        Assert.StartsWith("{", _result);
        Assert.EndsWith("}", _result);
        Assert.Contains($"\"albumId\":{_photo.AlbumId}", _result);
        Assert.Contains($"\"id\":{_photo.PhotoId}", _result);
        Assert.Contains($"\"title\":\"{_photo.Title}\"", _result);
        Assert.Contains($"\"thumbnailUrl\":\"{_photo.ThumbnailUrl}\"", _result);
        Assert.Contains($"\"url\":\"{_photo.Url}\"", _result);
    }
}