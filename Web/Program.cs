using ApplicationCore.Interfaces.Repository;
using BackendLab01;
using Infrastructure.Memory;
using Infrastructure.Memory.Repository;

var builder = WebApplication.CreateBuilder(args);

// Rejestrowanie 
builder.Services.AddTransient<IntGenerator>();

builder.Services.AddSingleton<IGenericRepository<Quiz, int>,MemoryGenericRepository<Quiz, int>>() ;
builder.Services.AddSingleton<IGenericRepository<QuizItem, int>,MemoryGenericRepository<QuizItem, int>>();
builder.Services.AddSingleton<IGenericRepository<QuizItemUserAnswer, string>, MemoryGenericRepository<QuizItemUserAnswer, string>>(); 

// Rejestrowanie serwisów
builder.Services.AddSingleton<IQuizAdminService, QuizAdminService>();
builder.Services.AddSingleton<IQuizUserService, QuizUserService>();

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

await app.SeedAsync(); // Tylko jeśli masz asynchroniczną metodę SeedAsync. Jeśli nie, zignoruj tę linię lub użyj metody synchronicznej.

app.Run();