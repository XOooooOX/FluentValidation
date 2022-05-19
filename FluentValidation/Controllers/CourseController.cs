using Microsoft.AspNetCore.Mvc;
using Models.Repositories;

namespace FluentValidation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private IUnitOfWork _unitOfWork;

    public CourseController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}

