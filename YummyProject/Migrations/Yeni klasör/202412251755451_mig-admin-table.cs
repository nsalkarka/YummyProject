namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migadmintable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "NameSurname", c => c.String());
            AlterColumn("dbo.Admins", "UserName", c => c.String());
            AlterColumn("dbo.Admins", "Password", c => c.String());
            AlterColumn("dbo.Admins", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "ImageUrl", c => c.Int(nullable: false));
            AlterColumn("dbo.Admins", "Password", c => c.Int(nullable: false));
            AlterColumn("dbo.Admins", "UserName", c => c.Int(nullable: false));
            AlterColumn("dbo.Admins", "NameSurname", c => c.Int(nullable: false));
        }
    }
}
