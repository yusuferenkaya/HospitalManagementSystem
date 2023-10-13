namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_appfee : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Appointments", "appFee");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "appFee", c => c.Int(nullable: false));
        }
    }
}
