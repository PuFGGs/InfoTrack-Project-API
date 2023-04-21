using WebScrapper.API;
using WebScrapper.BLL;
using WebScrapper.BLL.Interface;
using WebScrapper.DL;
using WebScrapper.DL.Interface;
using WebScrapper.DL.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebScrapperContext>();
builder.Services.AddScoped<ISearchDL, SearchService>();
builder.Services.AddScoped<ISearchBLL, SearchBLL>();


builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
    .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();
