using FluentMigrator;

namespace GameExchange.Infrastructe.Migrations.Versions
{
    [Migration(DataBaseVersion.TABLE_PLATAFORM_GAME_CATEGORY, "Create table to save the plataform, game, category ")]
    public class Version0000003 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Platforms")
                .WithColumn("Name").AsString().NotNullable();

            CreateTable("Categories")
            .WithColumn("Name").AsString().NotNullable();

            CreateTable("Games")
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("CategoryId").AsInt64().NotNullable().ForeignKey("FK_RefreshTokens_Category_Id", "Categories", "Id") 
            .WithColumn("PlatformId").AsInt64().NotNullable().ForeignKey("FK_RefreshTokens_Plataform_Id", "Platforms ", "Id");
        }
    }
}
