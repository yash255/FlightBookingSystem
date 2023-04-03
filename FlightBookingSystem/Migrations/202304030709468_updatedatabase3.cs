namespace FlightBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Flights", "UserId", "dbo.Users");
            DropIndex("dbo.Flights", new[] { "UserId" });
            DropColumn("dbo.Flights", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Flights", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Flights", "UserId");
            AddForeignKey("dbo.Flights", "UserId", "dbo.Users", "Id");
        }
    }
}
