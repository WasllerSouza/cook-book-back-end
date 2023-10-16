using FluentMigrator.Builders.Create.Table;

namespace CookBook.Infrastructure.Migrations;

public static class VersionBase
{
    public static ICreateTableColumnOptionOrWithColumnSyntax InsertColumnBase(ICreateTableWithColumnOrSchemaOrDescriptionSyntax table)
    {
        return table
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("LastUpdate").AsDateTime().NotNullable();
    }
}
