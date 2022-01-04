using MediatR;
using Microsoft.EntityFrameworkCore;
using TransnationalLanka.Rms.Mobile.Dal;
using TransnationalLanka.Rms.Mobile.Services.Customer;
using TransnationalLanka.Rms.Mobile.Services.Item;
using TransnationalLanka.Rms.Mobile.Services.Location;
using TransnationalLanka.Rms.Mobile.Services.MetaData;
using TransnationalLanka.Rms.Mobile.Services.MobileDevice;
using TransnationalLanka.Rms.Mobile.Services.PickList;
using TransnationalLanka.Rms.Mobile.Services.Request;
using TransnationalLanka.Rms.Mobile.Services.User;
using TransnationalLanka.Rms.Mobile.WebApi.Location;
using TransnationalLanka.Rms.Mobile.WebApi.MetaData;
using TransnationalLanka.Rms.Mobile.WebApi.Middleware;
using TransnationalLanka.Rms.Mobile.WebApi.MobileDevice;
using TransnationalLanka.Rms.Mobile.WebApi.PickList;
using TransnationalLanka.Rms.Mobile.WebApi.Request;
using TransnationalLanka.Rms.Mobile.WebApi.User;

var builder = WebApplication.CreateBuilder(args);

//Initialize the Database Context
builder.Services.AddDbContext<RmsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

//Add Project Services
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IMetaDataService, MetaDataService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IPickListService, PickListService>();
builder.Services.AddScoped<IMobileDeviceService, MobileDeviceService>();


builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName.Contains("Uat"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware(typeof(ErrorHandlingMiddleware));

//Application Routes
LocationRoutes.Register(app);
MetaDataRoutes.Register(app);
UserRoutes.Register(app);
PickListRoutes.Register(app);
MobileDeviceRoutes.Register(app);
RequestRoute.Register(app);

app.UseHttpsRedirection();

app.Run();