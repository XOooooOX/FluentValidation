using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Models.ValueObjects;
using System.Net;

namespace FluentValidationApp.Controllers;

[ApiController]
public class ApplicationController : ControllerBase
{
  protected IActionResult OK(object? result = null)
      => new ApiActionResult(ApiResult.OK(result), HttpStatusCode.OK);

  protected IActionResult OK()
      => new ApiActionResult(ApiResult.OK(), HttpStatusCode.OK);

  protected IActionResult Error(List<Error> errors)
      => new ApiActionResult(ApiResult.Error(errors), HttpStatusCode.BadRequest);

  protected new IActionResult NotFound(object? id)
      => new ApiActionResult(ApiResult.Error(Errors.General.NotFound(id)), HttpStatusCode.NotFound);

  protected IActionResult FromResult<T>(Result<T, List<Error>> result)
  {
    if (result.IsSuccess)
      return OK();

    return Error(result.Error);
  }
}

public class ApiActionResult : IActionResult
{
  private readonly ApiResult _apiResult;
  private readonly int? _statusCode;

  public ApiActionResult(ApiResult apiResult, HttpStatusCode statusCode)
  {
    _apiResult = apiResult;
    _statusCode = (int)statusCode;
  }

  public Task ExecuteResultAsync(ActionContext context)
      => new ObjectResult(_apiResult) { StatusCode = _statusCode }.ExecuteResultAsync(context);
}

