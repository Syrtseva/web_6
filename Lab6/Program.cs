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
        var builder = WebApplication.CreateBuilder(args); //����������� ���-�������

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddEndpointsApiExplorer(); //��������� ��������� ��� �в
            builder.Services.AddSwaggerGen(); //������ ������ ��� �������������� �в
        }

        builder.Services.AddControllers(); //������ ���������
        builder.Services.AddHttpClient<HttpClientService>(); //�������� HttpClientService ��� ������ � ��������

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection(); 
        app.MapControllers(); //������������ �������� ��� ����������

        app.Run();
    }
}
