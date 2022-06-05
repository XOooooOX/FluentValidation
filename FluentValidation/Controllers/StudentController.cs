using Microsoft.AspNetCore.Mvc;
using Models.Repositories;
using Models.ViewModels;

namespace FluentValidationApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ApplicationController
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentController(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    => OK(await _unitOfWork.StudentRepository.GetAll());

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await _unitOfWork.StudentRepository.Get(id);

        if (result is null)
            return NotFound();
        else
            return OK(result);
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] RegisterStudent registerStudent)
    {
        List<Models.DomainModels.Address> addresses = new();

        if (registerStudent.RegisterAddress is not null)
            addresses = registerStudent.RegisterAddress
                .Select(o => new Models.DomainModels.Address(
                    Guid.NewGuid(),
                    o.Name,
                    o.PostalCode,
                    o.City,
                    o.State,
                    o.CompleteAddress))
                .ToList();

        var student = new Models.DomainModels.Student()
        {
            FirstName = Models.ValueObjects.FirstName.Create(registerStudent.FirstName).Value,
            LastName = registerStudent.LastName,
            Email = registerStudent.Email,
            Gender = registerStudent.Gender,
            NationalCode = registerStudent.NationalCode,
            Phone = registerStudent.Phone,
            Age = registerStudent.Age,
            Id = Guid.NewGuid(),
            Addresses = addresses
        };

        await _unitOfWork.StudentRepository.Add(student);

        _unitOfWork.Complete();

        return OK();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Models.DomainModels.Student? result =
            await _unitOfWork.StudentRepository.Get(id);

        if (result == null)
            return NotFound();
        else
            _unitOfWork.StudentRepository.Delete(result);

        _unitOfWork.Complete();

        return OK();
    }
}

