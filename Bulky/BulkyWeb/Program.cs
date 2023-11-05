using Bulky.DataAccess.Data;
using Bulky.DataAccess.DbInitializer;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); //we are using MVC hence the service
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddRazorPages();
//Adding EntityFrameworkcore service(DbContext); Dependency injection innit
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe")); //inject the stripe api keys from the appsetting.json to the properties defined in the StripeSettings.cs

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>      //this services must strictly be added after the above Identity Service
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddAuthentication().AddFacebook(option =>
{
    option.AppId = "302558212712088";
    option.AppSecret = "fcb7ff45c4923be5878066da947fd460";
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) //environmental variable in appsettings.Development.json
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); //wwwroot static file are accessed
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
SeedDatabase();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}

/*One of the features provided by the Identity system is token-based authentication. Tokens are used for various features such as:

Confirming a user's email address
Resetting a user's password
Implementing two-factor authentication
These features require the generation and management of tokens. The AddDefaultTokenProviders() method is used to configure Identity to use the default token providers for these features.

When you call AddDefaultTokenProviders(), you are essentially telling Identity to use its built-in token providers. These providers handle the creation, validation, and storage of the tokens required for the aforementioned features.

Session in web applications are used to store user-specific data persists btn HTTP requests
A "session" refers to a mechanism for storing and managing usr-specific data while  a user intercts with a web application. It is a way to maintain state or context
for individual users btn their various interactions with a webistes or web application.Session are critical for building dynamic and personalized web experiences.
HERE IS HOW SESSIONS WORK
User Identification - When a user visits a website or logs into an application a unique session identifier often called session ID or token is generated and associated with that user.This identifier is usually stored as a cookie on the 
user's device or sent with each request as a URL parameter
Data Storage - The server uses this session ID to associate subsequent requests from the same user. The server can create a data container, typically in memory or a db to store use specific info. Ths data an include things like user preferencs, shopping cart contents, login, status
Persistence - sessions are temporary and typically have an expiration time. The session data remains available as long as the user is active within a certain tijme frame, which can be configured by the application.When the user reamins inactive for a defined period(session timeout),
the session data is cleared or marked as expired
User Tracking - Sessions are commonly used for user tracking authentication. They allow the server to recognize and server content specific to a particular user, which is crucial for features like personalization, user accounts and shopping carts
Security - Sessions can be used to enhance security by storing info that should not be accessible to the client-side(broswer).E.g authentication tokesn, user roles and sensitive data are often stored in server-side sessions.Additionally, setting session cookies as HTTP-only helps prevent client
-side scripts from accessing them, making it more difficult for attackers to steal session data

builder.Services.AddSession(options => {
This line is adding a session service to the application's dependency injection container. The builder variable  represents an IServiceCollection, and AddSession is an extension method for configuring session services in an ASP.NET Core application. The code inside the lambda expression (options => { ... }) 
specifies the configuration for the session service.
options.IdleTimeout = TimeSpan.FromMinutes(100);
This line sets the session timeout or the duration of inactivity that can occur before a session is considered idle and eligible for expiration. In this case, the IdleTimeout is set to 100 minutes. This means that if a user is inactive for 100 minutes, their session will expire, and the data stored in the session will be lost.
options.Cookie.HttpOnly = true;
This line configures the session cookie associated with the session. Setting HttpOnly to true indicates that the session cookie should be marked as "HTTP-only." An HTTP-only cookie can't be accessed via client-side JavaScript, enhancing the security of the session data.
options.Cookie.IsEssential = true;
This line sets the IsEssential property of the session cookie to true. An essential session cookie is typically used for scenarios where the application cannot function without the session cookie. If the client's browser does not send back an essential cookie, the session might be reinitialized.
We used this package microsoft.aspnetcore.authentication.facebook to configure facebook login option, after creating an app in the facebook.developers site
*/

