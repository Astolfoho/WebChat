namespace Chat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menssagem : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Menssagems");
        }
    }
}
