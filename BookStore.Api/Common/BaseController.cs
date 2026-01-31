using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Common;

[ApiController]
[Route("/[controller]")]
public abstract class BaseController : ControllerBase
{
}