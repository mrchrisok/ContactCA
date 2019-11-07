namespace ContactCA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ContactID = c.Guid(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 40),
                        LastName = c.String(nullable: false, maxLength: 40),
                        EmailAddress = c.String(nullable: false, maxLength: 80),
                        Telephone = c.String(nullable: false, maxLength: 16),
                        Message = c.String(maxLength: 500),
                        BestTimeToCall = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ContactID);
            
            CreateTable(
                "dbo.MetaSetting",
                c => new
                    {
                        MetaSettingID = c.Guid(nullable: false),
                        Environment = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                        Code = c.String(nullable: false, maxLength: 50),
                        Value = c.String(),
                        SortOrder = c.Int(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                        EntityID = c.Guid(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.MetaSettingID)
                .Index(t => new { t.Environment, t.Type, t.Code }, unique: true, name: "IX_NU_EnvironmentTypeCode");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MetaSetting", "IX_NU_EnvironmentTypeCode");
            DropTable("dbo.MetaSetting");
            DropTable("dbo.Contact");
        }
    }
}
