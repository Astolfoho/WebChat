namespace Chat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Usuariosignlr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "SignalrId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "SignalrId");
        }
    }
}
