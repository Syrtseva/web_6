using System;
using System.Net.Http;
using System.Threading.Tasks;
using Lab6.Controllers;
using Lab6.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args); //Налаштовуємо веб-додаток

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddEndpointsApiExplorer(); //генерація метаданих для АРІ
            builder.Services.AddSwaggerGen(); //Додаємо Свагер для документування АРІ
        }

        builder.Services.AddControllers(); //Додаємо контролер
        builder.Services.AddHttpClient<HttpClientService>(); //Реєструємо HttpClientService для роботи з запитами

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection(); 
        app.MapControllers(); //Налаштування маршрутів для контролерів

        app.Run();
    }
}
