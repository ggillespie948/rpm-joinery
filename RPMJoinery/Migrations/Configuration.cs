namespace RPMJoinery.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RPMJoinery.Models.ProjectsWebContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "RPMJoinery.Models.ProjectsWebContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(RPMJoinery.Models.ProjectsWebContext context)
        {
            //
            // d261ed9e-bf95-4666-958c-4b011dc0a30e - userID

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
