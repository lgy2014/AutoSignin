using AutoSignin.EntityFramework;
using AutoSignin.People;

namespace AutoSignin.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutoSigninDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AutoSignin";
        }

        protected override void Seed(AutoSigninDbContext context)
        {
            context.People.AddOrUpdate(
                p => p.Name,
                new Person {Name = "Halil"},
                new Person {Name = "Emre"}
                );
        }
    }
}
