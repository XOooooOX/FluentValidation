using Microsoft.AspNetCore.Mvc;
using Models.Repositories;
using Models.ViewModels;
using System.Text.RegularExpressions;

namespace FluentValidation.Controllers;

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

    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] AddStudent addStudent)
    {
        if (string.IsNullOrWhiteSpace(addStudent.NationalCode))
            return BadRequest("کد ملی را وارد کنید");

        if (addStudent.NationalCode?.Length != 10)
            return BadRequest("کد ملی صحیح نیست");

        if (string.IsNullOrWhiteSpace(addStudent.NationalCode))
            return BadRequest("کد ملی را وارد کنید");

        if (string.IsNullOrWhiteSpace(addStudent.FirstName))
            return BadRequest("نام را وارد کنید");

        if (string.IsNullOrWhiteSpace(addStudent.LastName))
            return BadRequest("نام فامیلی را وارد کنید");

        if (string.IsNullOrWhiteSpace(addStudent.Email))
            return BadRequest("ایمیل را وارد کنید");

        if (!Regex.IsMatch(addStudent.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            return BadRequest("فرمت ایمیل معتبر نیست");

        if (string.IsNullOrWhiteSpace(addStudent.Phone))
            return BadRequest("شماره تلفن را وارد کنید");

        if (!Regex.IsMatch(addStudent.Phone, @"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$"))
            return BadRequest("شماره تلفن صحیح نیست");

        if (addStudent.AddAddresses == null || addStudent.AddAddresses.Count >= 0)
            return BadRequest("آدرس را وارد کنید");

        List<Models.DomainModels.Address> addresses = addStudent.AddAddresses
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
            FirstName = addStudent.FirstName,
            LastName = addStudent.LastName,
            Email = addStudent.Email,
            Gender = addStudent.Gender,
            NationalCode = addStudent.NationalCode,
            Phone = addStudent.Phone,
            Id = Guid.NewGuid(),
            Addresses = addresses
        };

        await _unitOfWork.StudentRepository.Add(student);

        _unitOfWork.Complete();
        return Ok();
    }
}

