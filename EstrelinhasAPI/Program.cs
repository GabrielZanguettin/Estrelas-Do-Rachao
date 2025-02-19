using EstrelinhasAPI.Services;
using NHibernate;
using NHibernate.Cfg;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configuration = new Configuration();

configuration.SetProperty("cache.use_second_level_cache", "true");
configuration.SetProperty("cache.provider_class", "NHibernate.Cache.HashtableCacheProvider");

builder.Services.AddSingleton<ISessionFactory>((s) =>
{
    var sessionFactory = new Configuration().Configure().BuildSessionFactory();
    return sessionFactory;
});
builder.Services.AddTransient<UserService>();

builder.Services.AddAuthorization();
builder.Services.AddCors(
    b => b.AddDefaultPolicy(c =>
        c.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
    )
);


var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();
app.Run();
