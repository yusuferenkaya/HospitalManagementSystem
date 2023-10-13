namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropAgeFromDoctors : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Doctors", "age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "age", c => c.Int(nullable: false));
        }
    }
}
