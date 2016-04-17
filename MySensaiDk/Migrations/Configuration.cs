using System;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySensaiDk.Infrastructure;
using MySensaiDk.Models;

namespace MySensaiDk.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MySensaiDk.Infrastructure.MySensaiDkDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "MySensaiDk.Infrastructure.MySensaiDkDbContext";
        }

        protected override void Seed(MySensaiDk.Infrastructure.MySensaiDkDbContext context)
        {
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));

            string roleName = "Administrators";
            string roleName2 = "Users";
            string userName = "Admin";
            string firstName = "Admin";
            string lastName = "Admin";
            string password = "MySecret";
            string email = "admin@example.com";
            DateTime birthday = DateTime.Now;

            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }
            if (!roleMgr.RoleExists(roleName2))
            {
                roleMgr.Create(new AppRole(roleName2));
            }

            AppUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new AppUser { UserName = email, Email = email, FirstName = firstName, LastName = lastName, Birthday= birthday  }, password);
                user = userMgr.FindByName(email);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }

            if (!userMgr.IsInRole(user.Id, roleName2))
            {
                userMgr.AddToRole(user.Id, roleName2);
            }

            context.SaveChanges();
        }
    }
}
