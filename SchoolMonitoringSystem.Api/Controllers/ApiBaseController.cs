using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMonitoringSystem.Api.Filters;

namespace SchoolMonitoringSystem.Api.Controllers;
[Authorize]
[GlobalExceptionFilter]
public class ApiBaseController : Controller
{
    private IMediator? _mediator;
    public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    public IAppCache? _appCache;

    public IConfiguration? _configuration;


}

