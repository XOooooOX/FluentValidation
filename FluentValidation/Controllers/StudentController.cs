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
        var validator = new RegisterStudentValidator();

        var result = await validator.ValidateAsync(registerStudent, o =>
            o.IncludeRuleSets(Models.ActionCrud.Add.ToString())
        );

        if (!result.IsValid)
            return BadRequest(result.Errors[0].ErrorMessage);


        //if (string.IsNullOrWhiteSpace(registerStudent.NationalCode))
        //    return BadRequest("کد ملی را وارد کنید");

        //if (registerStudent.NationalCode?.Length != 10)
        //    return BadRequest("کد ملی صحیح نیست");

        //if (string.IsNullOrWhiteSpace(registerStudent.FirstName))
        //    return BadRequest("نام را وارد کنید");

        //if (string.IsNullOrWhiteSpace(registerStudent.LastName))
        //    return BadRequest("نام فامیلی را وارد کنید");

        //if (string.IsNullOrWhiteSpace(registerStudent.Email))
        //    return BadRequest("ایمیل را وارد کنید");

        //if (!Regex.IsMatch(registerStudent.Email, @"^0(9\d{9})$"))
        //    return BadRequest("فرمت ایمیل معتبر نیست");

        //if (string.IsNullOrWhiteSpace(registerStudent.Phone))
        //    return BadRequest("شماره تلفن را وارد کنید");

        //if (!Regex.IsMatch(registerStudent.Phone, @"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$"))
        //    return BadRequest("شماره تلفن صحیح نیست");

        //if (registerStudent.RegisterAddress == null || registerStudent.RegisterAddress.Count <= 0)
        //    return BadRequest("آدرس را وارد کنید");

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
            FirstName = registerStudent.FirstName,
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

