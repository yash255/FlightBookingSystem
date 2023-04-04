namespace FlightBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBook2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bookings", "BookingTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "BookingTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
