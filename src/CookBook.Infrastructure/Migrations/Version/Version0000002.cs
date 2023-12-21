using FluentMigrator;

namespace CookBook.Infrastructure.Migrations.Version;
[Migration((long)VersionNumber.CreateRevenueTable, "Cria tabela usuário versionado")]
public class Version0000002 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        CreateRevenueTable();
        CreateIngredientTable();
    }

    private void CreateRevenueTable()
    {
        var table = VersionBase.InsertColumnBase(Create.Table("Receitas"));

        table
            .WithColumn("Titulo").AsString(100).NotNullable()
            .WithColumn("Categoria").AsInt16().NotNullable()
            .WithColumn("ModoPreparo").AsString(5000).NotNullable()
            .WithColumn("UsuarioId").AsGuid().ForeignKey("FK_Receita_Usuario_Id", "Usuarios", "Id");

    }
    private void CreateIngredientTable()
    {

        var table = VersionBase.InsertColumnBase(Create.Table("Ingredientes"));
        table
            .WithColumn("Produto").AsString(2000).NotNullable()
            .WithColumn("Quantidade").AsString(2000).NotNullable()
            .WithColumn("ReceitaId").AsGuid().ForeignKey("FK_Ingrediente_Receita_Id", "Receitas", "Id");

    }
}
