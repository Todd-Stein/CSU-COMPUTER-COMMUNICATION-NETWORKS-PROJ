using IssueTracker.Data;
using IssueTracker.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/*string DbPath = "issuedb.db";
var db = new IssueContext();
db.Database.EnsureCreated();
db.Add(new IssueModel { Name = "issue test", Description = "test description", Id = 0 });
await db.SaveChangesAsync();*/

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IssueContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

/*using (var scope = app.Services.CreateScope())
{
    var dbCtx = scope.ServiceProvider.GetRequiredService<IssueContext>();
    dbCtx.Database.Migrate();
}*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "IssueMaker",
    pattern: "{controller=Issue}/{action=Index}/");
app.MapControllerRoute(
    name: "CreateIssue",
    pattern: "{cotroller=Issue}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "ProjectMaker",
    pattern: "{controller=Project}/{action=Index}/");

app.Run();
