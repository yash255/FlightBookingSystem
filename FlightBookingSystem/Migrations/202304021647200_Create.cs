namespace FlightBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PassengerName = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false),
                        NoOfTickets = c.Int(nullable: false),
                        CabinClass = c.Int(nullable: false),
                        BookingTime = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FlightId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Flights", t => t.FlightId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.FlightId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FlightNumber = c.String(nullable: false, maxLength: 50),
                        DepartureTime = c.DateTime(nullable: false),
                        ArrivalTime = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        City_Id = c.Int(),
                        City_Id1 = c.Int(),
                        ArrivalCity_Id = c.Int(),
                        DepartureCity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .ForeignKey("dbo.Cities", t => t.City_Id1)
                .ForeignKey("dbo.Cities", t => t.ArrivalCity_Id)
                .ForeignKey("dbo.Cities", t => t.DepartureCity_Id)
                .Index(t => t.City_Id)
                .Index(t => t.City_Id1)
                .Index(t => t.ArrivalCity_Id)
                .Index(t => t.DepartureCity_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "FlightId", "dbo.Flights");
            DropForeignKey("dbo.Flights", "DepartureCity_Id", "dbo.Cities");
            DropForeignKey("dbo.Flights", "ArrivalCity_Id", "dbo.Cities");
            DropForeignKey("dbo.Flights", "City_Id1", "dbo.Cities");
            DropForeignKey("dbo.Flights", "City_Id", "dbo.Cities");
            DropIndex("dbo.Flights", new[] { "DepartureCity_Id" });
            DropIndex("dbo.Flights", new[] { "ArrivalCity_Id" });
            DropIndex("dbo.Flights", new[] { "City_Id1" });
            DropIndex("dbo.Flights", new[] { "City_Id" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.Bookings", new[] { "FlightId" });
            DropTable("dbo.Users");
            DropTable("dbo.Cities");
            DropTable("dbo.Flights");
            DropTable("dbo.Bookings");
        }
    }
}
