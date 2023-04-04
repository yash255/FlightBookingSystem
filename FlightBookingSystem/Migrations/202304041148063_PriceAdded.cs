namespace FlightBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "Price");
        }
    }
}
