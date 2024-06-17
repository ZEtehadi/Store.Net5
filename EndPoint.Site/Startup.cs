using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Application.Interface.Facadpatter;
using Clean_Architecture.Application.Interface.Facadpatter.CommonFacad;
using Clean_Architecture.Application.Interface.Facadpatter.DisCountsFacad;
using Clean_Architecture.Application.Interface.Facadpatter.FinancesFacad;
using Clean_Architecture.Application.Interface.Facadpatter.HomePageFacad;
using Clean_Architecture.Application.Interface.Facadpatter.OrdersFacad;
using Clean_Architecture.Application.Services.Carts;
using Clean_Architecture.Application.Services.Common.FacadPatern;
using Clean_Architecture.Application.Services.DisCounts.FacadPattern;
using Clean_Architecture.Application.Services.Finances.Facad.FinancesFacad;
using Clean_Architecture.Application.Services.HomePage.FacadPatern;
using Clean_Architecture.Application.Services.Orders.Facad;
using Clean_Architecture.Application.Services.Products.FacadPatern;
using Clean_Architecture.Application.Services.Users.Command.EditUser;
using Clean_Architecture.Application.Services.Users.Command.EditUserForAdmin;
using Clean_Architecture.Application.Services.Users.Command.RegisterUser;
using Clean_Architecture.Application.Services.Users.Command.RemoveUser;
using Clean_Architecture.Application.Services.Users.Command.UserLogin;
using Clean_Architecture.Application.Services.Users.Command.UserStatusChange;
using Clean_Architecture.Application.Services.Users.Queries.GetRoles;
using Clean_Architecture.Application.Services.Users.Queries.GetUseDetailById;
using Clean_Architecture.Application.Services.Users.Queries.GetUsers;
using Clean_Architecture.Common.Roles;
using Clean_Architecture.Common.UploadFile;
using Clean_Architecture.Persistance.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EndPoint.Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Set Claim Setting
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/Authentication/signin");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
            });


            //Set Interface & Class
            services.AddScoped<IDataBaseContext, DataBaseContext>();
            services.AddScoped<IGetUsersService, GetUsersService>();
            services.AddScoped<IGetRolesService, GetRolesService>();
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<IRemoveUserService, RemoveUserService>();
            services.AddScoped<IUserStatusChangeService, UserStatusChangeService>();
            services.AddScoped<IEditUserService,EditUserService>() ;
            services .AddScoped<IUserLoginService,UserLoginService>() ;

            services.AddScoped<IProductFacadAdmin, ProductFacadAdmin>();//Ineject Product Facad Admin
            services.AddScoped<IProductFacadSite, ProductFacadSite>();//Ineject Product Facad Site
            services.AddScoped<ICommonFacad, CommonFacad>();//Inject Get Menui Facad
            services.AddScoped<IHomePageFacad, HomePageFacad>();//Inject HomePge facad
            services.AddScoped<IFinancesFacad,FinancesFacad>();
            services.AddScoped<IOrderFacad, OrdersFacad>();
            services.AddScoped<UploadFilee>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IGetUserDetailByIdService, GetUserDetailByIdService>();
            services.AddScoped<IDisCountFacad, DisCountFacad>();
            services.AddScoped<IEditUserForAdminService, EditUserForAdminService>();


            //set Authorization setting
            services.AddAuthorization(options =>
            {
                options.AddPolicy(UserRoles.Customer, policy => policy.RequireRole(UserRoles.Customer));
                options.AddPolicy(UserRoles.Admin, policy => policy.RequireRole(UserRoles.Admin));
                options.AddPolicy(UserRoles.Operator, policy => policy.RequireRole(UserRoles.Operator));
            });








            //Creating Connection to DataBase

            string ConnectionString = "Data Source=Z_E\\MSSQLSERVER_2022; Initial Catalog=DotnetCore_Bugeto_5_EFCore_3_Clean_Architecture; Integrated Security=true;";
            //Data Source=. OR  Z_E\MSSQLSERVER_2022
            services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(ConnectionString));




            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//Must First "Authentication"
            app.UseAuthorization();//Then "Authorization"
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //For Areas

                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
