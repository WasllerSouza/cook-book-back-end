using FluentMigrator;

namespace CookBook.Infrastructure.Migrations.Version;
[Migration((long) VersionNumber.CreateUserTable, "Cria tabela usuário versionado")]
public class Version0000001 : Migration
{
    public override void Down()
    {
    }


    public override void Up()
    {
        var table = VersionBase.InsertColumnBase(Create.Table("Usuarios"));

        table
            .WithColumn("Nome").AsString(100).NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("Senha").AsString(2000).NotNullable()
            .WithColumn("Telefone").AsString(20).NotNullable();
    }
}