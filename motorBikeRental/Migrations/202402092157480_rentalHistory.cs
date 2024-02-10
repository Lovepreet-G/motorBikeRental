namespace motorBikeRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rentalHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.rentalHistories",
                c => new
                    {
                        rentalId = c.Int(nullable: false, identity: true),
                        BikeId = c.Int(nullable: false),
                        customerId = c.Int(nullable: false),
                        from = c.DateTime(nullable: false),
                        to = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.rentalId)
                .ForeignKey("dbo.bikes", t => t.BikeId, cascadeDelete: true)
                .ForeignKey("dbo.customers", t => t.customerId, cascadeDelete: true)
                .Index(t => t.BikeId)
                .Index(t => t.customerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.rentalHistories", "customerId", "dbo.customers");
            DropForeignKey("dbo.rentalHistories", "BikeId", "dbo.bikes");
            DropIndex("dbo.rentalHistories", new[] { "customerId" });
            DropIndex("dbo.rentalHistories", new[] { "BikeId" });
            DropTable("dbo.rentalHistories");
        }
    }
}
