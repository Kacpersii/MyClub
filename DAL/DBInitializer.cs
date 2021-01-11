using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyClub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyClub.DAL
{
    public class DBInitializer : DropCreateDatabaseIfModelChanges<MyClubContext>
    {
        protected override void Seed(MyClubContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("Trainer"));
            roleManager.Create(new IdentityRole("User"));

            var user0 = new ApplicationUser { UserName = "admin@admin.com" };
            string password0 = "Admin.123";
            userManager.Create(user0, password0);
            userManager.AddToRole(user0.Id, "ADMIN");

            var user1 = new ApplicationUser { UserName = "qwer@qwer.com" };
            string password1 = "Qwer.123";
            userManager.Create(user1, password1);
            userManager.AddToRole(user1.Id, "Trainer");

            var user2 = new ApplicationUser { UserName = "asdf@asdf.com" };
            string password2 = "Asdf.123";
            userManager.Create(user2, password2);
            userManager.AddToRole(user2.Id, "Trainer");

            var user3 = new ApplicationUser { UserName = "zxcv@zxcv.com" };
            string password3 = "Zxcv.123";
            userManager.Create(user3, password3);
            userManager.AddToRole(user3.Id, "User");

            var user4 = new ApplicationUser { UserName = "mnbv@mnbv.com" };
            string password4 = "Mnbv.123";
            userManager.Create(user4, password4);
            userManager.AddToRole(user4.Id, "User");

            var users = new List<User>
            {
                new User { UserName = user0.UserName, Name = "Jan", Surname = "Nowak", BirthDate = DateTime.Parse("08-04-1994"), Phone = 658965965 },
                new User { UserName = user1.UserName, Name = "Artur", Surname = "Kowalczyk", BirthDate = DateTime.Parse("24-04-1990"), Phone = 555111666 },
                new User { UserName = user2.UserName, Name = "Michał", Surname = "Marciniak", BirthDate = DateTime.Parse("03-04-1984"), Phone = 666777999 },
                new User { UserName = user3.UserName, Name = "Filip", Surname = "Woźny", BirthDate = DateTime.Parse("12-04-1997"), Phone = 222444555 },
                new User { UserName = user4.UserName, Name = "Piotr", Surname = "Kozak", BirthDate = DateTime.Parse("20-04-1993"), Phone = 333555444 },
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();


            var articles = new List<Article>
            {
                new Article { UserID = users[1].ID, Subject = "temat 1", Content = "Jakaś treść" },
                new Article { UserID = users[2].ID, Subject = "temat 2", Content = "Jakaś treść 2." },
            };
            articles.ForEach(a => context.Articles.Add(a));
            context.SaveChanges();

        }
    }
}