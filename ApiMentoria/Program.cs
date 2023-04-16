using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ApiMentoria",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Francisco Neto",
            Email = "fnetho.si@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/francisco-neto-3b910513a/")
        }
    });

    //var xmlFile = "ApiMentoria.xml";
    //var xmlPatch = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPatch);

});

var app = builder.Build();

// Configure the HTTP request pipeline.

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
