using BooksApi.Data;
using Microsoft.EntityFrameworkCore;
using BooksApi.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
/* builder.Services.AddDbContext<RepositoryContext>(opt =>
    opt.UseInMemoryDatabase("BookList")); */
builder.Services.AddDbContext<RepositoryContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Repository Wrapper
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
// Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("http://localhost:3000/").AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();