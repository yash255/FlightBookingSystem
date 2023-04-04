namespace FlightBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBook1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "NoOfTicket", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "NoOfTicket");
        }
    }
}
