namespace FlightBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBook2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "BookingTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "BookingTime", c => c.DateTime(nullable: false));
        }
    }
}
