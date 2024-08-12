using Microsoft.EntityFrameworkCore;
using TreeJournalApi.Data;
using TreeJournalApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IRepository<ExceptionJournal>, ExceptionJournalRepository>();
builder.Services.AddScoped<IRepository<TreeNode>, TreeRepository>();

// Register services
builder.Services.AddScoped<IExceptionJournalService, ExceptionJournalService>();
builder.Services.AddScoped<ITreeService, TreeService>();

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
