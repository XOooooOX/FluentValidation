using Microsoft.AspNetCore.Mvc;
using Models.Repositories;
using Models.Validator;
using Models.ViewModels;
using System.Text.RegularExpressions;
using FluentValidation;

namespace FluentValidationApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private IUnitOfWork _unitOfWork;

    public StudentController(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    => Ok(await _unitOfWork.StudentRepository.GetAll());

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        Models.DomainModels.Student? result =
            await _unitOfWork.StudentRepository.Get(id);

        if (result == null)
            return NotFound();
        else
            return Ok(result);
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] RegisterStudent registerStudent)
    {
        //var validator = new RegisterStudentValidator();

        //var result = await validator.ValidateAsync(registerStudent);

        //if (!result.IsValid)
        //    return BadRequest(result.Errors[0].ErrorMessage);

        List<Models.DomainModels.Address> addresses = registerStudent.RegisterAddress
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
            FirstName = Models.DomainModels.FirstName.Create(registerStudent.FirstName).Value,
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

        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        Models.DomainModels.Student? result =
            await _unitOfWork.StudentRepository.Get(id);

        if (result == null)
            return NotFound();
        else
            _unitOfWork.StudentRepository.Delete(result);

        _unitOfWork.Complete();

        return Ok();
    }
}

