namespace FormalisQuiz.DataLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Utilities;

    internal sealed class Configuration : DbMigrationsConfiguration<FormalisQuiz.DataLayer.FormalisQuizContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FormalisQuiz.DataLayer.FormalisQuizContext context)
        {
            var user = new Models.User { Name = "John", Surname = "Doe", UserName = "testUser", Password = CryptoHelper.ConvertToMd5Hash("password") };
            context.Roles.AddOrUpdate(new Models.Role { Name = "Admin", User = user });
        }
    }
}
