// <copyright file="PhotoService.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace BlazorPhotoLibrary.Services;

using System.Text.Json;
using BlazorPhotoLibrary.Models;

/// <inheritdoc />
public class PhotoService : IPhotoService
{
    /// <summary>
    /// The URL for retrieving photos for a specific album.
    /// </summary>
    private const string _albumUrl = "photos?albumId={0}";

    /// <summary>
    /// The URL for retrieving all photos.
    /// </summary>
    private const string _photosUrl = "photos";

    /// <summary>
    /// The <see cref="IHttpClientFactory"/>.
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// The <see cref="ILogger"/>.
    /// </summary>
    private readonly ILogger<PhotoService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PhotoService"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/>.</param>
    /// <param name="httpClientFactory">The <see cref="IHttpClientFactory"/>.</param>
    public PhotoService(
        ILogger<PhotoService> logger,
        IHttpClientFactory httpClientFactory)
    {
        this._logger = logger;
        this._httpClient = httpClientFactory.CreateClient("PhotoClient");
    }

    /// <inheritdoc />
    public async Task<HashSet<int>> GetAlbumIdsAsync()
    {
        this._logger.LogDebug("Retrieving the album IDs.");

        try
        {
            HttpRequestMessage _request = new(HttpMethod.Get, _photosUrl);
            HttpResponseMessage _response = await this._httpClient.SendAsync(_request);

            if (_response.IsSuccessStatusCode)
            {
                await using Stream _contentStream = await _response.Content.ReadAsStreamAsync();
                List<Photo> _photos = await JsonSerializer.DeserializeAsync<List<Photo>>(_contentStream) ?? new();
                HashSet<int> _albumIds = _photos.Select(p => p.AlbumId).ToHashSet();

                this._logger.LogDebug($"Successfully retrieved {_albumIds.Count} album IDs.");

                return _albumIds;
            }
        }
        catch (Exception _ex)
        {
            // TODO: properly handle exception.
            this._logger.LogError(_ex, "Failed to retrieve the album IDs.");

            throw;
        }

        return new(0);
    }

    /// <inheritdoc />
    public async Task<List<Photo>> GetPhotosByAlbumIdAsync(int albumId)
    {
        this._logger.LogDebug($"Retrieving photos for album {albumId}.");

        HttpRequestMessage _request = new(HttpMethod.Get, string.Format(_albumUrl, albumId));
        HttpResponseMessage _response = await this._httpClient.SendAsync(_request);
        try
        {
            if (_response.IsSuccessStatusCode)
            {
                await using Stream _contentStream = await _response.Content.ReadAsStreamAsync();
                List<Photo> _photos = await JsonSerializer.DeserializeAsync<List<Photo>>(_contentStream) ?? new();

                this._logger.LogDebug($"Successfully retrieved {_photos.Count} photos for album {albumId}.");

                return _photos;
            }
        }
        catch (Exception _ex)
        {
            // TODO: properly handle exception.
            this._logger.LogError(_ex, $"Failed to retrieve photos for the album {albumId}.");
            throw;
        }

        return new(0);
    }
}
