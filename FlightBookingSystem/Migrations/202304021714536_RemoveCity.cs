namespace FlightBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Flights", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Flights", "City_Id1", "dbo.Cities");
            DropForeignKey("dbo.Flights", "ArrivalCity_Id", "dbo.Cities");
            DropForeignKey("dbo.Flights", "DepartureCity_Id", "dbo.Cities");
            DropIndex("dbo.Flights", new[] { "City_Id" });
            DropIndex("dbo.Flights", new[] { "City_Id1" });
            DropIndex("dbo.Flights", new[] { "ArrivalCity_Id" });
            DropIndex("dbo.Flights", new[] { "DepartureCity_Id" });
            AddColumn("dbo.Flights", "DepartureCity", c => c.String());
            AddColumn("dbo.Flights", "ArrivalCity", c => c.String());
            DropColumn("dbo.Flights", "City_Id");
            DropColumn("dbo.Flights", "City_Id1");
            DropColumn("dbo.Flights", "ArrivalCity_Id");
            DropColumn("dbo.Flights", "DepartureCity_Id");
            DropTable("dbo.Cities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Flights", "DepartureCity_Id", c => c.Int());
            AddColumn("dbo.Flights", "ArrivalCity_Id", c => c.Int());
            AddColumn("dbo.Flights", "City_Id1", c => c.Int());
            AddColumn("dbo.Flights", "City_Id", c => c.Int());
            DropColumn("dbo.Flights", "ArrivalCity");
            DropColumn("dbo.Flights", "DepartureCity");
            CreateIndex("dbo.Flights", "DepartureCity_Id");
            CreateIndex("dbo.Flights", "ArrivalCity_Id");
            CreateIndex("dbo.Flights", "City_Id1");
            CreateIndex("dbo.Flights", "City_Id");
            AddForeignKey("dbo.Flights", "DepartureCity_Id", "dbo.Cities", "Id");
            AddForeignKey("dbo.Flights", "ArrivalCity_Id", "dbo.Cities", "Id");
            AddForeignKey("dbo.Flights", "City_Id1", "dbo.Cities", "Id");
            AddForeignKey("dbo.Flights", "City_Id", "dbo.Cities", "Id");
        }
    }
}
