using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Models.Context;
using Models.Repositories;
using Models.Validator;
using Models.ViewModels;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


//builder.Services.AddControllers();

//builder.Services.AddControllers()
//    .AddFluentValidation();

//builder.Services.AddTransient<IValidator<RegisterStudent>, RegisterStudentValidator>();

builder.Services.AddControllers()
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
