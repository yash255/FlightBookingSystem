namespace FlightBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Flights", "DepartureCity", c => c.String(nullable: false));
            AlterColumn("dbo.Flights", "ArrivalCity", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Flights", "ArrivalCity", c => c.String());
            AlterColumn("dbo.Flights", "DepartureCity", c => c.String());
        }
    }
}
