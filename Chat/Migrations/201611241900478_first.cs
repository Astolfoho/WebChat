namespace Chat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageFiles",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Bytes = c.Binary(),
                        MessageId = c.Guid(nullable: false),
                        Name = c.String(),
                        Content = c.String(),
                        Type = c.String(),
                        Extension = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        From = c.Int(nullable: false),
                        To = c.Int(nullable: false),
                        Content = c.String(),
                        SendDateTime = c.DateTime(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsOnline = c.Boolean(nullable: false),
                        Name = c.String(),
                        SignalrId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
            DropTable("dbo.MessageFiles");
        }
    }
}
