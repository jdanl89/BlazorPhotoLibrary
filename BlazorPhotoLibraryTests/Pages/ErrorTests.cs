// <copyright file="ErrorTests.cs" company="Jason Danley">
// Copyright (c) Jason Danley. All rights reserved.
// </copyright>

namespace BlazorPhotoLibraryTests.Pages;

using System.Diagnostics;
using BlazorPhotoLibrary.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;

/// <summary>
/// Unit tests for <see cref="ErrorModel"/>.
/// </summary>
public class ErrorTests
{
    private readonly Mock<ILogger<ErrorModel>> _loggerMock = new();
    private readonly ErrorModel _sut;

    public ErrorTests()
    {
        DefaultHttpContext _httpContext = new();
        ModelStateDictionary _modelState = new();
        ActionContext _actionContext = new(_httpContext, new(), new PageActionDescriptor(), _modelState);
        EmptyModelMetadataProvider _modelMetadataProvider = new();
        ViewDataDictionary _viewData = new(_modelMetadataProvider, _modelState);
        PageContext _pageContext = new(_actionContext)
        {
            ViewData = _viewData,
        };
        this._sut = new(this._loggerMock.Object)
        {
            PageContext = _pageContext,
        };
    }

    [Theory]
    [InlineData("unit_test_request_id", true)]
    [InlineData(null, false)]
    public void ErrorModel_WhenRequestIdIsPopulated_ShowRequestIdIsTrue(string? requestId, bool showRequestId)
    {
        // Execute SUT.
        this._sut.RequestId = requestId;

        // Verify Results.
        Assert.Equal(showRequestId, this._sut.ShowRequestId);
    }

    [Fact]
    public void OnGet_WhenCurrentActivityIsNull_SetRequestIdFromHttpContext()
    {
        // Setup Fixtures.
        Activity.Current = new("unit_test_activity");

        // Execute SUT.
        this._sut.OnGet();

        // Verify Results.
        Assert.Equal(this._sut.HttpContext.TraceIdentifier, this._sut.RequestId);
    }
}