namespace FlightBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "FlightId", "dbo.Flights");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            AlterColumn("dbo.Bookings", "BookingTime", c => c.DateTime(nullable: false));
            AddForeignKey("dbo.Bookings", "FlightId", "dbo.Flights", "FlightId", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            DropColumn("dbo.Bookings", "NumberOfTickets");
            DropColumn("dbo.Bookings", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Bookings", "NumberOfTickets", c => c.Int(nullable: false));
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "FlightId", "dbo.Flights");
            AlterColumn("dbo.Bookings", "BookingTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddForeignKey("dbo.Bookings", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Bookings", "FlightId", "dbo.Flights", "FlightId");
        }
    }
}
