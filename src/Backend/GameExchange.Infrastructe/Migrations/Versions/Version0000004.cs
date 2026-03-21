using FluentMigrator;

namespace GameExchange.Infrastructe.Migrations.Versions
{
    [Migration(DataBaseVersion.TABLE_LISTING, "Create table to save the Listing")]
    public class Version0000004 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Listings")
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Description").AsCustom("TEXT").NotNullable()
                .WithColumn("Price").AsDecimal(8, 2).NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("GameId").AsInt32().NotNullable().ForeignKey("FK_Listings_Game_Id", "Games", "Id")
                .WithColumn("SellerId").AsInt64().NotNullable().ForeignKey("FK_Listings_User_Id", "Users", "Id");

          
        }
    }
}
