namespace CategoryNewProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class p_categoryname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CategoryName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CategoryName");
        }
    }
}
