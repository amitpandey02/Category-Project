namespace CategoryNewProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class p_createdby : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CreatedBy");
        }
    }
}
