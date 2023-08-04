// <copyright file="Error.cshtml.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace BlazorPhotoLibrary.Pages;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/// <summary>
/// The model for the Error page.
/// </summary>
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    /// <summary>
    /// Gets or sets the request ID.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Gets a value indicating whether to show the request ID.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<ErrorModel> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorModel"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public ErrorModel(ILogger<ErrorModel> logger)
    {
        this._logger = logger;
    }

    /// <summary>
    /// The method called upon receiving an HTTP GET request.
    /// </summary>
    public void OnGet() => this.RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;
}
