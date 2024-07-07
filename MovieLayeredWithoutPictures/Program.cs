using MovieLayeredWithoutPictures.BLL.Infrastructure;
using MovieLayeredWithoutPictures.BLL.Interfaces;
using MovieLayeredWithoutPictures.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddMovieContext(connection);

builder.Services.AddUnitOfWorkService(); // ???

builder.Services.AddTransient<IMovieService,MovieService>(); // ???

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseRouting();
//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movie}/{action=Index}/{id?}");

app.Run();
