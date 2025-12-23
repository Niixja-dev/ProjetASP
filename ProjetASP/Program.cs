using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetASP.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuration MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GRHContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configuration Identity
builder.Services.AddIdentity<Utilisateur, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<GRHContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed des roles et admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<GRHContext>();
    var userManager = services.GetRequiredService<UserManager<Utilisateur>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    context.Database.EnsureCreated();

    // Créer les rôles
    string[] roles = { "Admin", "RH" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Créer ou mettre à jour l'admin
    // ========== MODIFIER ICI LES INFOS ADMIN ==========
    var adminEmail = "admin@grh.com";      // Changer l'email ici
    var adminPassword = "Admin123";         // Changer le mot de passe ici
    var adminNom = "Administrateur";        // Changer le nom ici
    var adminPrenom = "Systeme";            // Changer le prénom ici
    // ==================================================
    
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        // Créer le nouvel admin
        var admin = new Utilisateur
        {
            UserName = adminEmail,
            Email = adminEmail,
            Nom = adminNom,
            Prenom = adminPrenom,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
            Console.WriteLine("Admin créé avec succès!");
        }
        else
        {
            Console.WriteLine("Erreur création admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
    else
    {
        // Mettre à jour les infos de l'admin existant
        adminUser.Nom = adminNom;
        adminUser.Prenom = adminPrenom;
        await userManager.UpdateAsync(adminUser);
        
        // Réinitialiser le mot de passe de l'admin existant
        var token = await userManager.GeneratePasswordResetTokenAsync(adminUser);
        var result = await userManager.ResetPasswordAsync(adminUser, token, adminPassword);
        
        if (result.Succeeded)
        {
            Console.WriteLine("Mot de passe admin réinitialisé!");
        }
        
        // S'assurer que l'admin a le rôle Admin
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
