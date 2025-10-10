using Microsoft.AspNetCore.Identity;

namespace Lab08_Andreboza.Data;

public static class DbSeeder
{
    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Crear roles si no existen
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            // Crear usuario Admin si no existe
            var adminUserEmail = "admin@example.com"; 
            if (await userManager.FindByEmailAsync(adminUserEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = "Andreboza", 
                    Email = adminUserEmail,
                    EmailConfirmed = true
                };
                
                // Contraseña personalizada
                var result = await userManager.CreateAsync(adminUser, "PasswordAndre123!"); 
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}