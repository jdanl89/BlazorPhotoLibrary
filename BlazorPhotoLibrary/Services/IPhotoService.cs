// <copyright file="IPhotoService.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace BlazorPhotoLibrary.Services;

using BlazorPhotoLibrary.Models;

/// <summary>
/// The service for handling photos from the external repository.
/// </summary>
public interface IPhotoService
{
    /// <summary>
    /// Gets a list of album ID.
    /// </summary>
    /// <returns>The unique album IDs.</returns>
    public Task<HashSet<int>> GetAlbumIdsAsync();

    /// <summary>
    /// Gets photos belonging to a specific album.
    /// </summary>
    /// <param name="albumId">The album ID.</param>
    /// <returns>The photos.</returns>
    public Task<List<Photo>> GetPhotosByAlbumIdAsync(int albumId);
}
