namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class service_table : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Services", "Title", c => c.String());
            AlterColumn("dbo.Services", "Description", c => c.String());
            AlterColumn("dbo.Services", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Services", "Icon", c => c.Int(nullable: false));
            AlterColumn("dbo.Services", "Description", c => c.Int(nullable: false));
            AlterColumn("dbo.Services", "Title", c => c.Int(nullable: false));
        }
    }
}
