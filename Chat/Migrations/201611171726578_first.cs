namespace Chat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menssagems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        De = c.Int(nullable: false),
                        Para = c.Int(nullable: false),
                        Conteudo = c.String(),
                        SendDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EstaOnline = c.Boolean(nullable: false),
                        Nome = c.String(),
                        SignalrId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuarios");
            DropTable("dbo.Menssagems");
        }
    }
}
