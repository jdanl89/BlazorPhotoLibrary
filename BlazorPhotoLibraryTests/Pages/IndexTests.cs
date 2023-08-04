// <copyright file="IndexTests.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace BlazorPhotoLibraryTests.Pages;

using BlazorPhotoLibrary.Models;
using BlazorPhotoLibrary.Pages;
using BlazorPhotoLibrary.Services;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

/// <summary>
/// Unit tests for <see cref="Index"/>.
/// </summary>
public class IndexTests
{
    private readonly Mock<ILogger<Index>> _loggerMock = new();
    private readonly Mock<IPhotoService> _photoServiceMock = new();
    private IRenderedComponent<Index>? _sut;

    [Fact]
    public void Index_WhenSelectAlbumId_GetRelatedPhotos()
    {
        // Setup Fixtures.
        const int albumId = 1;
        Photo _photo = new()
        {
            AlbumId = 1,
            PhotoId = 2,
            ThumbnailUrl = "test_thumbnail_url",
            Title = "test_title",
            Url = "test_url",
        };

        // Setup Mocks.
        this._photoServiceMock
            .Setup(m => m.GetAlbumIdsAsync())
            .ReturnsAsync(new HashSet<int> { albumId })
            .Verifiable();

        this._photoServiceMock
            .Setup(m => m.GetPhotosByAlbumIdAsync(albumId))
            .ReturnsAsync(new List<Photo> { _photo })
            .Verifiable();

        using TestContext _ctx = new();
        _ = _ctx.Services.AddScoped(_ => this._photoServiceMock.Object);
        _ = _ctx.Services.AddSingleton(_ => this._loggerMock.Object);
        this._sut = _ctx.RenderComponent<Index>();

        // Execute SUT.
        this._sut.Find("#albumId").Change(albumId);

        // Verify Results.
        this._photoServiceMock.Verify();
        this.VerifyDebugLog("Index: Initializing page.");
        this.VerifyDebugLog("Index: Page initialized.");
        this.VerifyDebugLog($"Index: Album {albumId} selected. Retrieving photos.");
        this.VerifyDebugLog($"Index: Photos for album {albumId} retrieved.");
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