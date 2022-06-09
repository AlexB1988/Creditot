using Creditot;
using Creditot.Commands;
using Creditot.Domain;
using Creditot.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<DataContext>(options=>options.UseSqlServer(connection));
builder.Services.AddSingleton<TelegramBot>();
builder.Services.AddScoped<ICommandExecutor, CommandExecutor>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOperationService,OperationService>();
builder.Services.AddScoped<BaseCommand, StartCommand>();
builder.Services.AddScoped<BaseCommand, GetCategoriesCommand>();
builder.Services.AddScoped<KeyboardBase, CategoriesKeyboard>();
builder.Services.AddScoped<BaseCommand, AddCategoryCommand>();
builder.Services.AddScoped<BaseCommand, NewCategoryCommand>();
builder.Services.AddScoped<BaseCommand, AddCategoryCreditCommand>();
builder.Services.AddScoped<BaseCommand, FinishCreditCommand>();
builder.Services.AddScoped<BaseCommand, GetDayRangeCommand>();
builder.Services.AddScoped<BaseCommand, AdminSendMessagesCommand>();
builder.Services.AddScoped<BaseCommand, MailingListCommand>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//serviceProvider.GetRequiredService<TelegramBot>().GetBot().Wait();
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
