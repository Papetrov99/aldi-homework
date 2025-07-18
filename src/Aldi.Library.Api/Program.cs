using Aldi.Library.Api.Middlewares;
using Aldi.Library.Api.Models.Data;
using Aldi.Library.Api.Models.DTOs;
using Aldi.Library.Api.Repositories;
using Aldi.Library.Api.Repositories.Interfaces;
using Aldi.Library.Api.Services;
using Aldi.Library.Api.Services.Interfaces;
using Aldi.Library.Api.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Aldi.Library.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<LibraryDbContext>(options =>
            options.UseInMemoryDatabase("LibraryDb"));
        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

        builder.Services.AddLogging();
        builder.Services.AddHttpLogging(x => x.CombineLogs = true);
        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<ILoanRepository, LoanRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ILoanService, LoanService>();
        builder.Services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserValidator>();

        var app = builder.Build();

        app.UseExceptionHandler();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpLogging();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseStatusCodePages();

        app.Run();
    }
}
