namespace CategoryNewProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class p_removecategoryname : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "CategoryName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CategoryName", c => c.String());
        }
    }
}
