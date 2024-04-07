using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

builder.Services.AddAuthentication(options => { options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; })
    .AddJwtBearer(jwtOptions =>
    {
        jwtOptions.Authority =
            $"{builder.Configuration["AzureAdB2C:Instance"]}/{builder.Configuration["AzureAdB2C:TenantId"]}/{builder.Configuration["AzureAdB2C:SignUpSignInPolicyId"]}/v2.0/";
        jwtOptions.Audience = builder.Configuration["AzureAdB2C:ClientId"];
    });

// Add services to the container.
/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(options =>
    {
        builder.Configuration.Bind("AzureAdB2C", options);
    }, options => { builder.Configuration.Bind("AzureAdB2C", options); });*/

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();