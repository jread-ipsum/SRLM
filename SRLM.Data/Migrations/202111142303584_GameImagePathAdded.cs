namespace SRLM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameImagePathAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Game", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Game", "ImagePath");
        }
    }
}
