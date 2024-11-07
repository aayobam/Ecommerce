using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContexts;

namespace Persistence.Seeders;

public static class DatabaseSeeder
{
    //public static WebApplication SeedRoles(this WebApplication app)
    //{
    //    string[] initialRoles = new string[] { "vendor", "customer", "admin", "superadmin" };

    //    using (var scope = app.Services.CreateAsyncScope())
    //    {
    //        var context = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
    //        var rolesToSeed = new List<ApplicationRole>();

    //        if (rolesToSeed.Any())
    //        {
    //            foreach (var initialRole in initialRoles)
    //            {
    //                if (!context.Roles.Any(r => r.Name == initialRole.ToLower()))
    //                {
    //                    var role = new ApplicationRole()
    //                    {
    //                        Name = initialRole.ToLower(),
    //                        NormalizedName = initialRole.ToUpper(),
    //                    };
    //                    rolesToSeed.Add(role);
    //                    Console.WriteLine($"\n Added {initialRole.ToUpper()} \n");
    //                }
    //                else
    //                {
    //                    Console.WriteLine($"\n Role {initialRole.ToUpper()} already exists. Skipping... \n");
    //                }
    //            }
    //            context.Roles.AddRange(rolesToSeed);
    //            context.SaveChanges();
    //            Console.WriteLine("\n All Roles Seeded Successfully \n");
    //        }
    //        else
    //        {
    //            Console.WriteLine("\n No new roles to seed.\n");
    //        }
    //        context.Database.EnsureCreatedAsync();
    //    }
    //    return app;
    //}
}
