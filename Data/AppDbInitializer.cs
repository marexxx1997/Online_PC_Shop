using Microsoft.AspNetCore.Identity;
using OnlineShop.Data.Static;
using OnlineShop.Models;

namespace OnlineShop.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbCContext>();
                context.Database.EnsureCreated();
                if (!context.Brands.Any())
                {
                    context.Brands.AddRange(new List<Brand>()
                    {
                        new Brand() { Name = "HP" },
                        new Brand() { Name = "Dell" },
                        new Brand() { Name = "Acer" },
                        

                    });
                    context.SaveChanges();

                }
                //else 
                //{
                //    context.Brands.AddRange(new List<Brand>()
                //    {
                //        new Brand() { Name = "Asus" }

                //    });
                //    context.SaveChanges();
                //}
                if (!context.Laptops.Any())
                {
                    context.Laptops.AddRange(new List<Laptop>()
                    {
                        new Laptop() {Description="First laptop", GraphicCard=Enums.GraphicCard.NVIDIA, HardDisc="256 GB",
                        OS="Windows 10",
                        RAM="12 GB",
                        ScreenSize="15'",
                        Model="XYZ",
                        Processor="Intel i5",
                        Warranty="25 months",
                        ProfilePictureUrl="",
                       Manufacturer=Enums.Manufacturer.HP,
                        Quantity=5,
                        Price=1054.50,
                        LaptopsTypesId = 1
                        }
                    }

                        );
                    context.SaveChanges();
                }

                if(!context.Components.Any())
                {
                    context.Components.AddRange(new List<Components>()
                    {
                        new Components()
                        {
                            Manufacturer="DualTech",
                            Model="XYZEFGH",
                            ComponentsTypesId=1,
                            Warranty="12 mjeseci",
                            Description="Bezveze",
                            Quantity=5,
                            Price=100                        }
                    }
                        );
                    context.SaveChanges();
                }
                //else
                //{
                //    context.Laptops.AddRange(new List<Laptop>()
                //    {
                //         new Laptop() {Description="Fifth Laptop", GraphicCard=Enums.GraphicCard.AMDRadeon, HardDisc="512 GB",
                //        OS="Windows 11",
                //        RAM="8 GB",
                //        ScreenSize="14.9'",
                //        Model="HGRTZH9524",
                //        Processor="AMD Ryzer",
                //        Warranty="12 months",
                //        ProfilePictureUrl="https://www.univerzalno.com/wp-content/uploads/2022/11/1-1-470.jpg",
                //       Manufacturer=Enums.Manufacturer.ACER,
                //       Price=1000.00,
                //       Quantity=3,
                //       LaptopsTypes = "test"

                //         }
                //    }

                //        );
                //    context.SaveChanges();

                //}
                //if (!context.LaptopBrands.Any())
                //{
                //    context.LaptopBrands.AddRange(new List<LaptopBrand>()
                //    { new LaptopBrand()
                //    {
                //        BrandId=1,
                //        LaptopId=1
                //    }


                //    }
                //        );
                //    context.SaveChanges();

                //}

            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope=applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string adminUserEmail = "admin@pcshop.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if(adminUser==null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Administrator",
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true

                    };
                    await userManager.CreateAsync(newAdminUser, "Admin1234!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@pcshop.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Korisnik",
                        UserName = "korisnik",
                        Email = appUserEmail,
                        EmailConfirmed = true

                    };
                    await userManager.CreateAsync(newAppUser, "Korisnik1234!");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
