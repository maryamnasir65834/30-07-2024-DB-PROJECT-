using BookStoreApi.DataAccessLayer;
using BookStoreApi.Services; // Add this using directive if not already present

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register services with DI
builder.Services.AddSingleton<BookDAL>(); // Ensure BookDAL is registered
builder.Services.AddSingleton<BookBL>();  // Ensure BookBL is registered
builder.Services.AddScoped<CommentDAL>(); // Register CommentDAL
builder.Services.AddScoped<CommentBL>();  // Register CommentBL

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://127.0.0.1:5500") // Replace with your frontend origin
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();
app.MapControllers();
app.Run();
