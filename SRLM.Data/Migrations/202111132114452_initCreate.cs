namespace SRLM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Car",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        RaceClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.RaceClass", t => t.RaceClassId, cascadeDelete: true)
                .Index(t => t.RaceClassId);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(nullable: false),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.Platform",
                c => new
                    {
                        PlatformId = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PlatformId);
            
            CreateTable(
                "dbo.Track",
                c => new
                    {
                        TrackId = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.TrackId);
            
            CreateTable(
                "dbo.RaceClass",
                c => new
                    {
                        RaceClassId = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RaceClassId);
            
            CreateTable(
                "dbo.League",
                c => new
                    {
                        LeagueId = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Country = c.String(),
                        LobbySettings = c.String(),
                        StartDate = c.DateTimeOffset(nullable: false, precision: 7),
                        EndDate = c.DateTimeOffset(nullable: false, precision: 7),
                        GameId = c.Int(nullable: false),
                        RaceClassId = c.Int(nullable: false),
                        PlatformId = c.Int(nullable: false),
                        MaxDriverCount = c.Int(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.LeagueId)
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Platform", t => t.PlatformId, cascadeDelete: true)
                .ForeignKey("dbo.RaceClass", t => t.RaceClassId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.RaceClassId)
                .Index(t => t.PlatformId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        GameTag = c.String(),
                        DiscordName = c.String(nullable: false),
                        Country = c.String(),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(precision: 7),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameCar",
                c => new
                    {
                        Game_GameId = c.Int(nullable: false),
                        Car_CarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Game_GameId, t.Car_CarId })
                .ForeignKey("dbo.Game", t => t.Game_GameId, cascadeDelete: true)
                .ForeignKey("dbo.Car", t => t.Car_CarId, cascadeDelete: true)
                .Index(t => t.Game_GameId)
                .Index(t => t.Car_CarId);
            
            CreateTable(
                "dbo.PlatformGame",
                c => new
                    {
                        Platform_PlatformId = c.Int(nullable: false),
                        Game_GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Platform_PlatformId, t.Game_GameId })
                .ForeignKey("dbo.Platform", t => t.Platform_PlatformId, cascadeDelete: true)
                .ForeignKey("dbo.Game", t => t.Game_GameId, cascadeDelete: true)
                .Index(t => t.Platform_PlatformId)
                .Index(t => t.Game_GameId);
            
            CreateTable(
                "dbo.TrackGame",
                c => new
                    {
                        Track_TrackId = c.Int(nullable: false),
                        Game_GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Track_TrackId, t.Game_GameId })
                .ForeignKey("dbo.Track", t => t.Track_TrackId, cascadeDelete: true)
                .ForeignKey("dbo.Game", t => t.Game_GameId, cascadeDelete: true)
                .Index(t => t.Track_TrackId)
                .Index(t => t.Game_GameId);
            
            CreateTable(
                "dbo.ApplicationUserLeague",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        League_LeagueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.League_LeagueId })
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.League", t => t.League_LeagueId, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.League_LeagueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.League", "RaceClassId", "dbo.RaceClass");
            DropForeignKey("dbo.League", "PlatformId", "dbo.Platform");
            DropForeignKey("dbo.League", "GameId", "dbo.Game");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.ApplicationUserLeague", "League_LeagueId", "dbo.League");
            DropForeignKey("dbo.ApplicationUserLeague", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Car", "RaceClassId", "dbo.RaceClass");
            DropForeignKey("dbo.TrackGame", "Game_GameId", "dbo.Game");
            DropForeignKey("dbo.TrackGame", "Track_TrackId", "dbo.Track");
            DropForeignKey("dbo.PlatformGame", "Game_GameId", "dbo.Game");
            DropForeignKey("dbo.PlatformGame", "Platform_PlatformId", "dbo.Platform");
            DropForeignKey("dbo.GameCar", "Car_CarId", "dbo.Car");
            DropForeignKey("dbo.GameCar", "Game_GameId", "dbo.Game");
            DropIndex("dbo.ApplicationUserLeague", new[] { "League_LeagueId" });
            DropIndex("dbo.ApplicationUserLeague", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TrackGame", new[] { "Game_GameId" });
            DropIndex("dbo.TrackGame", new[] { "Track_TrackId" });
            DropIndex("dbo.PlatformGame", new[] { "Game_GameId" });
            DropIndex("dbo.PlatformGame", new[] { "Platform_PlatformId" });
            DropIndex("dbo.GameCar", new[] { "Car_CarId" });
            DropIndex("dbo.GameCar", new[] { "Game_GameId" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.League", new[] { "PlatformId" });
            DropIndex("dbo.League", new[] { "RaceClassId" });
            DropIndex("dbo.League", new[] { "GameId" });
            DropIndex("dbo.Car", new[] { "RaceClassId" });
            DropTable("dbo.ApplicationUserLeague");
            DropTable("dbo.TrackGame");
            DropTable("dbo.PlatformGame");
            DropTable("dbo.GameCar");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.League");
            DropTable("dbo.RaceClass");
            DropTable("dbo.Track");
            DropTable("dbo.Platform");
            DropTable("dbo.Game");
            DropTable("dbo.Car");
        }
    }
}
