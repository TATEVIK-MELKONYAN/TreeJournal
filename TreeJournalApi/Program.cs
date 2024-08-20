using Microsoft.EntityFrameworkCore;
using TreeJournalApi.Data;
using TreeJournalApi.Middleware;
using TreeJournalApi.Models;
using TreeJournalApi.Repositories;
using TreeJournalApi.Repositories.Interfaces;
using TreeJournalApi.Services;
using TreeJournalApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped<IRepository<ExceptionJournal>, ExceptionJournalRepository>();
builder.Services.AddScoped<IRepository<TreeNode>, TreeNodeRepository>();
// Register Services
builder.Services.AddScoped<ITreeNodeService, TreeNodeService>();
builder.Services.AddScoped<IExceptionJournalService, ExceptionJournalService>();

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
