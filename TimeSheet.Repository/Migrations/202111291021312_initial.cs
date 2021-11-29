namespace TimeSheet.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TimeSheetEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        Time = c.Double(nullable: false),
                        OverTime = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.UserOnProjects", t => new { t.UserId, t.ProjectId }, cascadeDelete: true)
                .Index(t => new { t.UserId, t.ProjectId })
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.UserOnProjects",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProjectId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        ZipCode = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        HoursPerWeek = c.Double(nullable: false),
                        Status = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamLeaders",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.UserOnProjects", t => new { t.UserId, t.ProjectId }, cascadeDelete: true)
                .Index(t => new { t.UserId, t.ProjectId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamLeaders", new[] { "UserId", "ProjectId" }, "dbo.UserOnProjects");
            DropForeignKey("dbo.TimeSheetEntries", new[] { "UserId", "ProjectId" }, "dbo.UserOnProjects");
            DropForeignKey("dbo.UserOnProjects", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserOnProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.TimeSheetEntries", "CategoryId", "dbo.Categories");
            DropIndex("dbo.TeamLeaders", new[] { "UserId", "ProjectId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Clients", new[] { "CountryId" });
            DropIndex("dbo.Projects", new[] { "ClientId" });
            DropIndex("dbo.UserOnProjects", new[] { "ProjectId" });
            DropIndex("dbo.UserOnProjects", new[] { "UserId" });
            DropIndex("dbo.TimeSheetEntries", new[] { "CategoryId" });
            DropIndex("dbo.TimeSheetEntries", new[] { "UserId", "ProjectId" });
            DropTable("dbo.TeamLeaders");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Countries");
            DropTable("dbo.Clients");
            DropTable("dbo.Projects");
            DropTable("dbo.UserOnProjects");
            DropTable("dbo.TimeSheetEntries");
            DropTable("dbo.Categories");
        }
    }
}
