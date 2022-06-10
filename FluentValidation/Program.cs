using FluentValidation.AspNetCore;
using FluentValidationApp;
using FluentValidationApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Context;
using Models.Repositories;
using Models.Validator;
using Models.ValueObjects;
using System.Net;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


//builder.Services.AddControllers();

//builder.Services.AddControllers()
//    .AddFluentValidation();

//builder.Services.AddTransient<IValidator<RegisterStudent>, RegisterStudentValidator>();

builder.Services.AddControllers()
    .AddJsonOptions(o
    => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
    .ConfigureApiBehaviorOptions(option =>
    {
        option.InvalidModelStateResponseFactory = ModelStateValidator.ValidateModelState;
    })
    .AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<RegisterStudentValidator>());


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FluentValidationDbContext>(options
    => options.UseSqlServer(configuration.GetConnectionString("FluentValidationDatabase")));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//FluentValidation.ValidatorOptions.Global.CascadeMode = FluentValidation.CascadeMode.Stop;


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public class ModelStateValidator
{
    public static IActionResult ValidateModelState(ActionContext actionContext)
    {
        List<Error> errors = new();

        var modelState = actionContext.ModelState.ToList();

        errors.AddRange(modelState
            .Where(o => o.Value is not null && o.Value.Errors is not null)
            .Select(o => Error.Deserialize(o.Value!.Errors[0].ErrorMessage)));

        ApiResult result = ApiResult.Error(errors);
        var apiActionResult = new ApiActionResult(result, HttpStatusCode.BadRequest);
        return apiActionResult;
    }
}