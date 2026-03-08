using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace GameExchange.Infrastructe.Migrations.Versions
{
    public abstract class VersionBase : ForwardOnlyMigration
    {


        protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string tableName)
        {
            return Create.Table(tableName)
                     .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                     .WithColumn("CreatedOn").AsDateTime().NotNullable();
        }
    }
}
