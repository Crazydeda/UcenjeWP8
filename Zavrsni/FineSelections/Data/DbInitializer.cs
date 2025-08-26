
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FineSelections.Models;

namespace FineSelections.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider services, ILogger logger)
        {
            using var scope = services.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await ctx.Database.MigrateAsync();

            // Create roles
            var roles = new[] { "Admin", "Customer" };
            foreach (var r in roles)
            {
                if (!await roleMgr.RoleExistsAsync(r))
                {
                    await roleMgr.CreateAsync(new IdentityRole(r));
                }
            }

            // Seed default accounts
            string adminEmail = "admin@fineselections.local";
            string adminPass = "Admin!2345";
            string userEmail = "user@fineselections.local";
            string userPass = "User!2345";

            if (await userMgr.FindByEmailAsync(adminEmail) is null)
            {
                var admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var res = await userMgr.CreateAsync(admin, adminPass);
                if (res.Succeeded) await userMgr.AddToRoleAsync(admin, "Admin");
                else logger.LogError("Admin seed failed: {0}", string.Join(",", res.Errors.Select(e => e.Description)));
            }

            if (await userMgr.FindByEmailAsync(userEmail) is null)
            {
                var user = new IdentityUser { UserName = userEmail, Email = userEmail, EmailConfirmed = true };
                var res = await userMgr.CreateAsync(user, userPass);
                if (res.Succeeded) await userMgr.AddToRoleAsync(user, "Customer");
                else logger.LogError("User seed failed: {0}", string.Join(",", res.Errors.Select(e => e.Description)));
            }

            // Seed products by executing provided SQL inserts when table is empty
            if (!await ctx.Products.AnyAsync())
            {
                var sqlPath = Path.Combine(AppContext.BaseDirectory, "Data", "proizvodi_insert.sql");
                if (!File.Exists(sqlPath))
                {
                    logger.LogWarning("Insert SQL file not found at {path}", sqlPath);
                    return;
                }

                var all = await File.ReadAllTextAsync(sqlPath);
                var statements = all.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                using var trx = await ctx.Database.BeginTransactionAsync();
                try
                {
                    foreach (var s in statements)
                    {
                        await ctx.Database.ExecuteSqlRawAsync(s);
                    }
                    await trx.CommitAsync();
                }
                catch (Exception ex)
                {
                    await trx.RollbackAsync();
                    logger.LogError(ex, "Seeding proizvodi failed");
                }
            }
        }
    }
}
