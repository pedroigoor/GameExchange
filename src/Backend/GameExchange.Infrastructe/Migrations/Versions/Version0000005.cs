using FluentMigrator;

namespace GameExchange.Infrastructe.Migrations.Versions
{
    [Migration(DataBaseVersion.TABLE_ORDER, "Create table to save the order")]
    public class Version0000005 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Orders")
                .WithColumn("BuyerId").AsInt64().NotNullable().ForeignKey("FK_Order_User_Id", "Users", "Id")
                .WithColumn("ListingId").AsInt64().NotNullable().ForeignKey("FK_Order_Listing_Id", "Listings", "Id")
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Price").AsDecimal(8, 2).NotNullable();

          
        }
    }
}
