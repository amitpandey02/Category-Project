namespace CategoryNewProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class credential : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        UserRole = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Credentials", "RoleId", "dbo.Roles");
            DropIndex("dbo.Credentials", new[] { "RoleId" });
            DropTable("dbo.Roles");
            DropTable("dbo.Credentials");
        }
    }
}
