namespace LocalizaAmigos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovaBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_amigos",
                c => new
                    {
                        amigoID = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 30),
                        latitude = c.Double(nullable: false),
                        longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.amigoID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tb_amigos");
        }
    }
}
