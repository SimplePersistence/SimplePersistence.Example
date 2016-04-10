namespace SimplePersistence.Example.UoW.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Logging.Applications",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(nullable: false, maxLength: 512),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        UpdatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedBy = c.String(nullable: false, maxLength: 128),
                        DeletedOn = c.DateTimeOffset(precision: 7),
                        DeletedBy = c.String(maxLength: 128),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Logging.Logs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Logger = c.String(nullable: false, maxLength: 256),
                        Message = c.String(nullable: false),
                        Exception = c.String(),
                        Thread = c.String(maxLength: 128),
                        MachineName = c.String(nullable: false, maxLength: 128),
                        AssemblyVersion = c.String(nullable: false, maxLength: 32),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        Application = c.String(nullable: false, maxLength: 128),
                        Level = c.String(nullable: false, maxLength: 8),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Logging.Applications", t => t.Application, cascadeDelete: true)
                .ForeignKey("Logging.Levels", t => t.Level, cascadeDelete: true)
                .Index(t => t.MachineName)
                .Index(t => t.AssemblyVersion)
                .Index(t => t.CreatedOn)
                .Index(t => t.Application)
                .Index(t => t.Level);
            
            CreateTable(
                "Logging.Levels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 8),
                        Description = c.String(nullable: false, maxLength: 512),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        UpdatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedBy = c.String(nullable: false, maxLength: 128),
                        DeletedOn = c.DateTimeOffset(precision: 7),
                        DeletedBy = c.String(maxLength: 128),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Logging.Logs", "Level", "Logging.Levels");
            DropForeignKey("Logging.Logs", "Application", "Logging.Applications");
            DropIndex("Logging.Logs", new[] { "Level" });
            DropIndex("Logging.Logs", new[] { "Application" });
            DropIndex("Logging.Logs", new[] { "CreatedOn" });
            DropIndex("Logging.Logs", new[] { "AssemblyVersion" });
            DropIndex("Logging.Logs", new[] { "MachineName" });
            DropTable("Logging.Levels");
            DropTable("Logging.Logs");
            DropTable("Logging.Applications");
        }
    }
}
