namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feature_ımageurl_required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Features", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Features", "ImageUrl", c => c.String(nullable: false));
        }
    }
}
