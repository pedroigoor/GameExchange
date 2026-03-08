using GameExchange.Infrastructe.Migrations;
using GameExchange.Infrastructe.Extensions;
using GameExchange.Infrastructe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfrastructe(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.OperationFilter<IdsFilter>();

//    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
//    {
//        Description = @"JWT Authorization header using the Bearer scheme.
//                      Enter 'Bearer' [space] and then your token in the text input below.
//                      Example: 'Bearer 12345abcdef'",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "bearer",


//    });

//    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
//    {
//        [new OpenApiSecuritySchemeReference("bearer", document)] = []
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

app.Run();
void MigrateDatabase()
{
    if (builder.Configuration.IsUnitTestEnviroment())
    {
        return;
    }

    var connetionString = builder.Configuration.ConnectionString();
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    DataBaseMigration.Migrate(connetionString, serviceScope.ServiceProvider);
}