using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using app.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using app.Repositorios.Abstratos;
using app.Repositorios.Concretos;
using app.Contexto;
using System.Globalization;
using Microsoft.AspNetCore.HttpOverrides;
using VueCliMiddleware;

namespace app
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy",
            builder =>
            {
                builder.WithOrigins("http://localhost:8080", "http://192.168.0.103:8080", "http://leobo.heliohost.org", "https://leobo.heliohost.org")
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader().WithMethods("POST", "PUT", "DELETE", "GET");
            }));

            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "clientapp/dist";
            });

            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<SecurityStampValidatorOptions>(options =>
                options.ValidationInterval = TimeSpan.FromSeconds(10));

            services.AddDbContext<DemoDbContext>(options =>
            options.UseSqlServer("server=mlmsistemas.mssql.somee.com;database=mlmsistemas;uid=leozinx_SQLLogin_1;password=plmfnchybi"));

            services.AddDbContext<AuthenticationContext>(options =>
            options.UseSqlServer("server=mlmsistemas.mssql.somee.com;database=mlmsistemas;uid=leozinx_SQLLogin_1;password=plmfnchybi"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDefaultIdentity<ApplicationUser>()
            //3 - Adicionar Roles baseado no Identity Role
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            });

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParameters);
            // Configuração de Autenticação com JWTBEARER
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });
            services.AddScoped<ICategoria, EFCategoria>();
            services.AddScoped<IMovimentacao, EFMovimentacao>();
            services.AddScoped<IPeriodo, EFPeriodo>();
            services.AddScoped<ITokenFactory, TokenFactory>();
            services.AddScoped<IMenu, EFMenu>();
            services.AddScoped<IMenuRoles, EFMenuRoles>();
            services.AddScoped<ITokenFactory, TokenFactory>();

            /* services.AddTransient<ApplicationInitializer>(); */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
        IHostingEnvironment env)
        {
            app.UseCors("MyPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    // run npm process with client app
                    spa.UseVueCli(npmScript: "serve", port: 8080);
                    // if you just prefer to proxy requests from client app, use proxy to SPA dev server instead:
                    // app should be already running before starting a .NET client
                    // spa.UseProxyToSpaDevelopmentServer("http://localhost:8080"); // your Vue app port
                }
            });

            app.UseRequestLocalization();
        }
    }
}
