namespace UnitTests.DataAccess.EntityFramework.Migrations
{
    using SocialDb;
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomeData : DbMigration
    {
        public override void Up()
        {
            //заполнение

            //генераторы
            SqlEntityGenerator.GenerateUserInfo(20);
            SqlEntityGenerator.GenerateMessageInfo(40);
            SqlEntityGenerator.GenerateFriendsInfo(40);
            SqlEntityGenerator.GenerateUserLikes(15);

            //конкретные пользователи
            string cs = ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString;
            SqlQueryLogic.MakeAndExecuteSQLQuery(new NewNameIsEnterEventArgs("add one user Lars, male, 26.12.1963, 18.08.2016, n"), cs);
            SqlQueryLogic.MakeAndExecuteSQLQuery(new NewNameIsEnterEventArgs("add one user James, male, 03.08.1963, 18.08.2016, n"), cs);
            SqlQueryLogic.MakeAndExecuteSQLQuery(new NewNameIsEnterEventArgs("add one user Kirk, male, 18.11.1962, 18.08.2016, n"), cs);
            SqlQueryLogic.MakeAndExecuteSQLQuery(new NewNameIsEnterEventArgs("add one user Robert, male, 23.10.1964, 18.08.2016, n"), cs);
            SqlQueryLogic.MakeAndExecuteSQLQuery(new NewNameIsEnterEventArgs("add one user Dave, male, 13.09.1961, 22.01.2016, n"), cs);

            //добавляем предложения дружбы
            SqlQueryLogic.MakeAndExecuteSQLQuery(new NewNameIsEnterEventArgs("add one offer Lars, James, 2"), cs);
            SqlQueryLogic.MakeAndExecuteSQLQuery(new NewNameIsEnterEventArgs("add one offer James, Lars, 2"), cs);
            SqlQueryLogic.MakeAndExecuteSQLQuery(new NewNameIsEnterEventArgs("add one offer Lars, Kirk, 1"), cs);
            SqlQueryLogic.MakeAndExecuteSQLQuery(new NewNameIsEnterEventArgs("add one offer Kirk, Lars, 0"), cs);
            SqlQueryLogic.MakeAndExecuteSQLQuery(new NewNameIsEnterEventArgs("add one offer Lars, Robert, 2"), cs);
            SqlQueryLogic.MakeAndExecuteSQLQuery(new NewNameIsEnterEventArgs("add one offer Dave, Lars, 1"), cs);
        }

        public override void Down()
        {
        }
    }
}
