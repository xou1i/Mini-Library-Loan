using BookStore.Api.Extensions;
using BookStore.Api.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o => o.DisplayRequestDuration());
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestTimingMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();
