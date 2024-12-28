namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migyumadmintable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YumAdmins",
                c => new
                    {
                        YumAdminId = c.Int(nullable: false, identity: true),
                        NameSurname = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.YumAdminId);
            
            DropTable("dbo.Admins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        NameSurname = c.Int(nullable: false),
                        UserName = c.Int(nullable: false),
                        Password = c.Int(nullable: false),
                        ImageUrl = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId);
            
            DropTable("dbo.YumAdmins");
        }
    }
}
