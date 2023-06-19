using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMonitoringSystem.Api.Controllers;
public class ApiBaseController : Controller
{
    private IMediator? _mediator;
    public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    public IAppCache? _appCache;

    public IConfiguration? _configuration;


}

