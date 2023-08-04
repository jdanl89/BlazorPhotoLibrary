// <copyright file="PhotoServiceTests.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace BlazorPhotoLibraryTests.Services;

using BlazorPhotoLibrary.Models;
using BlazorPhotoLibrary.Services;
using Microsoft.Extensions.Logging;
using Moq;

/// <summary>
/// Unit tests for <see cref="PhotoService"/>.
/// </summary>
public class PhotoServiceTests
{
    private readonly HttpClient _httpClient = new();
    private readonly Mock<ILogger<PhotoService>> _loggerMock = new();
    private readonly PhotoService _sut;

    public PhotoServiceTests()
    {
        this._httpClient.BaseAddress = new("https://jsonplaceholder.typicode.com/");
        Mock<IHttpClientFactory> _httpClientFactoryMock = new();
        _ = _httpClientFactoryMock
            .Setup(m => m.CreateClient("PhotoClient"))
            .Returns(this._httpClient);

        this._sut = new(this._loggerMock.Object, _httpClientFactoryMock.Object);
    }

    [Fact]
    public async Task GetAlbumIdsAsync_WhenResponseIsValid_ReturnAlbumIds()
    {
        // Execute SUT.
        HashSet<int> _result = await this._sut.GetAlbumIdsAsync();

        // Verify Results.
        Assert.NotEmpty(_result);

        this.VerifyDebugLog("Photo Service: Retrieving the album IDs.");
        this.VerifyDebugLog($"Photo Service: Successfully retrieved {_result.Count} album IDs.");
    }

    [Fact]
    public async Task GetPhotosByAlbumIdAsync_WhenResponseIsValid_ReturnPhotos()
    {
        // Setup Fixtures.
        const int albumId = 1;

        // Execute SUT.
        List<Photo> _result = await this._sut.GetPhotosByAlbumIdAsync(albumId);

        // Verify Results.
        Assert.NotEmpty(_result);
        Assert.All(_result, r => r.AlbumId.Equals(albumId));

        this.VerifyDebugLog($"Photo Service: Retrieving photos for album {albumId}.");
        this.VerifyDebugLog($"Photo Service: Successfully retrieved {_result.Count} photos for album {albumId}.");
    }

    private void VerifyDebugLog(string message) => this._loggerMock.Verify(
            m => m.Log(
                LogLevel.Debug,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString() !.Contains(message)),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
}