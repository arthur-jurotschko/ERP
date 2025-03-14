using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Authorization;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Data;
using GHOST.TalentosCortes.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace GHOST.TalentosCortes.Services.API.Configuration
{
    public static class MvcConfiguration
    {
        public static void AddMvcSecurity(this IServiceCollection services, IConfigurationRoot configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var tokenConfigurations = new TokenDescriptor();
            new ConfigureFromConfigurationOptions<TokenDescriptor>(
                    configuration.GetSection("JwtTokenOptions"))
                .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.SaveToken = true;

                var paramsValidation = bearerOptions.TokenValidationParameters;

                paramsValidation.IssuerSigningKey = SigningCredentialsConfiguration.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;

            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeLer", policy => policy.RequireClaim("Administrador", "Ler"));
                options.AddPolicy("PodeGravar", policy => policy.RequireClaim("Administrador", "Gravar"));

                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}
//    public static class MvcConfiguration
//    {
//        public static void AddMvcSecurity(this IServiceCollection services, IConfigurationRoot configuration)
//        {
//            if (services == null) throw new ArgumentNullException(nameof(services));

//            var tokenConfigurations = new TokenDescriptor();
//            new ConfigureFromConfigurationOptions<TokenDescriptor>(
//                    configuration.GetSection("JwtTokenOptions"))
//                .Configure(tokenConfigurations);
//            services.AddSingleton(tokenConfigurations);

//            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//            {
//                options.ClaimsIdentity.UserIdClaimType = "UserID";
//                options.SignIn.RequireConfirmedEmail = true;
//            })
//                .AddErrorDescriber<PortugueseIdentityErrorDescriber>()
//                .AddEntityFrameworkStores<ApplicationDbContext>()
//                .AddDefaultTokenProviders();

//            services.Configure<IdentityOptions>(options =>
//            {
//                options.Lockout.AllowedForNewUsers = true;
//                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//                options.Lockout.MaxFailedAccessAttempts = 5;

//                options.Password.RequireDigit = true;
//                options.Password.RequiredLength = 8;
//                options.Password.RequiredUniqueChars = 1;
//                options.Password.RequireLowercase = true;
//                options.Password.RequireUppercase = true;
//                options.Password.RequireNonAlphanumeric = true;

//                options.SignIn.RequireConfirmedEmail = false;
//                options.SignIn.RequireConfirmedPhoneNumber = false;

//                // options.Tokens.AuthenticatorTokenProvider = 
//                // options.Tokens.ChangeEmailTokenProvider = 
//                // options.Tokens.ChangePhoneNumberTokenProvider =
//                // options.Tokens.EmailConfirmationTokenProvider =
//                // options.Tokens.PasswordResetTokenProvider =
//                // options.Tokens.ProviderMap = 

//                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//                options.User.RequireUniqueEmail = false;
//            });

//            services.ConfigureApplicationCookie(options => {
//                //options.AccessDeniedPath = "/Account/AcessDenied";
//                //options.ClaimsIssuer = "";
//                //options.Cookie.Domain = "";
//                //options.Cookie.Expiration = "";
//                options.Cookie.HttpOnly = true;
//                options.Cookie.Name = ".AspNetCore.Cookies";
//                //options.Cookie.Path = "";
//                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
//                options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
//                //options.CookieManager = 
//                //options.DataProtectionProvider = 
//                //options.Events =
//                //options.EventsType = 
//                options.ExpireTimeSpan = TimeSpan.FromDays(5);
//                //options.LoginPath = "/Account/Login";
//                //options.LogoutPath = "/Account/Logout";
//                options.ReturnUrlParameter = "ReturnUrl";
//                //options.SessionStore = ""
//                //options.SlidingExpiration = true;
//                //options.TicketDataFormat = 
//            });

//            services.AddAuthentication(options =>
//            {
//                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


//            })

//           .AddJwtBearer(bearerOptions =>
//           {
//               bearerOptions.RequireHttpsMetadata = false;
//               bearerOptions.SaveToken = true;

//               var paramsValidation = bearerOptions.TokenValidationParameters;

//               paramsValidation.IssuerSigningKey = SigningCredentialsConfiguration.Key;
//               paramsValidation.ValidAudience = tokenConfigurations.Audience;
//               paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

//               paramsValidation.ValidateIssuerSigningKey = true;
//               paramsValidation.ValidateLifetime = true;
//               paramsValidation.ClockSkew = TimeSpan.Zero;


//           });


//            services.AddAuthorization(options =>
//            {
//                options.AddPolicy("PodeLer", policy => policy.RequireClaim("Administrador", "Ler"));
//                options.AddPolicy("PodeGravar", policy => policy.RequireClaim("Administrador", "Gravar"));

//                options.AddPolicy("PodeLer", policy => policy.RequireClaim("Estagiario", "Ler"));
//                options.AddPolicy("PodeGravar", policy => policy.RequireClaim("Estagiario", "Gravar"));

//                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
//                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
//                    .RequireAuthenticatedUser().Build());
//            });
//        }
//    }
//}
