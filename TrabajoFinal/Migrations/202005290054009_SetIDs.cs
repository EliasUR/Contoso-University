namespace TrabajoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetIDs : DbMigration
    {
        public override void Up()
        {
            //Setea los id a partir de 1000
            Sql("DBCC CHECKIDENT ('Department', RESEED, 1000)");
            Sql("DBCC CHECKIDENT ('Instructor', RESEED, 1000)");
            Sql("DBCC CHECKIDENT ('Course', RESEED, 1000)");
            Sql("DBCC CHECKIDENT ('Student', RESEED, 1000)");
        }

        public override void Down()
        {
        }
    }
}
