using Aldi.Library.Api.Models.Data;
using Aldi.Library.Api.Repositories;
using Aldi.Library.Api.Repositories.Interfaces;
using Aldi.Library.Api.Services;
using Aldi.Library.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aldi.Library.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<LibraryDbContext>(options =>
            options.UseInMemoryDatabase("LibraryDb"));
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<ILoanRepository, LoanRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ILoanService, LoanService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
