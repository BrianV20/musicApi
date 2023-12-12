using Microsoft.EntityFrameworkCore;
using musicApi2.Config;
using musicApi2.Models.User;
using musicApi2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<musicApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// automapper
builder.Services.AddAutoMapper(typeof(Mapping));

builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<IArtistInterface, ArtistService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IReleaseService, ReleaseService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IRatingInterface, RatingService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
