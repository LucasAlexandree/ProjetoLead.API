using Microsoft.EntityFrameworkCore;
using ProjetoLead.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjetoLeadDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ViaCepService>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
