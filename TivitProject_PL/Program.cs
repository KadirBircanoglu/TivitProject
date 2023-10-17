using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Globalization;
using TivitProject_BL.EmailSenderManager;
using TivitProject_BL.ImplementationofManagers;
using TivitProject_BL.InterfaceofManagers;
using TivitProject_DL.ContextInfo;
using TivitProject_DL.ImplementationofRepos;
using TivitProject_DL.InterfaceofRepos;
using TivitProject_EL.IdentityModels;
using TivitProject_EL.Mappings;
using TivitProject_EL.ViewModels;
using TivitProject_PL.CreateDefaultData;
using TivitProject_PL.Models;

namespace TivitProject_PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //culture info  ---> Türkçe karakterlerde hata almamak için
            var cultureInfo = new CultureInfo("tr-TR");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;



            // serilog logger ayarlari

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);



            //contexti ayarliyoruz.
            builder.Services.AddDbContext<MyContext>(options =>
            {
                //klasik mvcde connection string web configte yer alir.
                //core mvcde connection string appsetting.json dosyasindan alinir.
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyLocal"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            });


            //appuser ve approle identity ayari
            builder.Services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireDigit = true;
                opt.User.RequireUniqueEmail = true;
                //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+&%";

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<MyContext>();



            //automapper ayari 
            builder.Services.AddAutoMapper(a =>
            {
                a.AddExpressionMapping();
                a.AddProfile(typeof(Maps));
                a.CreateMap<TivitIndexViewModel, UserTivitDTO>().ReverseMap();
                a.CreateMap<AppUser, ProfileViewModel>().ReverseMap(); ;

            });


            //interfacelerin DI yasam dongusu
            builder.Services.AddScoped<IEmailManager, EmailManager>();

            builder.Services.AddScoped<IUserTivitRepo, UserTivitRepo>();
            builder.Services.AddScoped<IUserTivitManager, UserTivitManager>();

            builder.Services.AddScoped<ITivitTagsRepo, TivitTagsRepo>();
            builder.Services.AddScoped<ITivitTagsManager, TivitTagsManager>();

            builder.Services.AddScoped<ITivitPhotoRepo, TivitPhotoRepo>();
            builder.Services.AddScoped<ITivitPhotoManager, TivitPhotoManager>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            // Authentication, Authorization ayarý
            app.UseAuthentication(); // login logout
            app.UseAuthorization();  // yetki

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //Sistem ilk ayaga kalktiginnda rolleri ekleyelim
            //ADMIN, MEMBER, WAITINGFORACTIVATION, PASSIVE

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                CreateData c = new CreateData(logger);
              //  c.CreateRoles(serviceProvider);

            }

            app.Run();
        }
    }
}