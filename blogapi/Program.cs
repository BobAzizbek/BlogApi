var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogConnection")));
builder.Services.AddTransient<IMediaService, MediaService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddCors(options =>
options.AddPolicy("OnlyGetPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:7006");
        builder.WithMethods("POST", "GET", "PUT", "DELETE");
    }));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("OnlyGetPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
