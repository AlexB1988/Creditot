using Creditot;
using Creditot.Commands;
using Creditot.Domain;
using Creditot.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("Db3");
builder.Services.AddDbContext<DataContext>(options=>options.UseNpgsql(connection),ServiceLifetime.Singleton);
builder.Services.AddSingleton<TelegramBot>();
builder.Services.AddSingleton<ICommandExecutor, CommandExecutor>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IOperationService,OperationService>();
builder.Services.AddSingleton<BaseCommand, StartCommand>();
builder.Services.AddSingleton<BaseCommand, GetCategoriesCommand>();
builder.Services.AddSingleton<KeyboardBase, CategoriesKeyboard>();
builder.Services.AddSingleton<BaseCommand, AddCategoryCommand>();
builder.Services.AddSingleton<BaseCommand, NewCategoryCommand>();
builder.Services.AddSingleton<BaseCommand, AddCategoryCreditCommand>();
builder.Services.AddSingleton<BaseCommand, FinishCreditCommand>();
builder.Services.AddSingleton<BaseCommand, GetDayRangeCommand>();
builder.Services.AddSingleton<BaseCommand, AdminSendMessagesCommand>();
builder.Services.AddSingleton<BaseCommand, MailingListCommand>();
builder.Services.AddSingleton<BaseCommand, SupportCommand>();
builder.Services.AddSingleton<BaseCommand, DeleteStaticticsCommand>();

//builder.Services.Configure<ForwardedHeadersOptions>(options =>
//{
//    options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//serviceProvider.GetRequiredService<TelegramBot>().GetBot().Wait();
//app.UseForwardedHeaders(new ForwardedHeadersOptions
//{
//    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
//});


app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
