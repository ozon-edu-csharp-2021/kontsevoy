using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(1)]
    public class MerchRequests : Migration
    {
        private const string TableName = "merch_requests";
        
        public override void Up()
        {
            if (!Schema.Table(TableName).Exists())
            {
                Create
                    .Table(TableName)
                    .WithDescription("Запросы выдачи мерчпаков")
                    .WithColumn("id").AsInt64().Identity().PrimaryKey()
                    .WithColumn("created_at").AsDateTime().NotNullable()
                        .WithDefault(SystemMethods.CurrentDateTime)
                    .WithColumn("employee_email").AsString().NotNullable()
                        .WithColumnDescription("Email сотрудника (для оповещения о готовности мерчпака к выдаче)")
                    .WithColumn("employee_name").AsString().NotNullable()
                        .WithColumnDescription("Имя сотрудника")
                    .WithColumn("employee_clothing_size").AsString().NotNullable()
                        .WithColumnDescription("Размер одежды сотрудника (для выбора состава мерчпака)")
                    .WithColumn("manager_email").AsString().NotNullable()
                        .WithColumnDescription("Email менеджера (для оповещения о готовности мерчпака к выдаче)")
                    .WithColumn("manager_name").AsString().NotNullable()
                        .WithColumnDescription("Имя менеджера")
                    .WithColumn("merch_pack_id").AsInt64().NotNullable()
                    .WithColumn("status").AsString().NotNullable()
                    .WithColumn("try_handout_at").AsDateTime().Nullable()
                        .WithColumnDescription("Дата последней попытки выдать мерч")
                    .WithColumn("handout_at").AsDateTime().Nullable()
                        .WithColumnDescription("Дата выдачи мерчпака")
                    .WithColumn("handout").AsCustom("jsonb").Nullable()
                        .WithColumnDescription("Состав выданного мерчпака");
                
                Create
                    .Index($"idx_{TableName}_employee_email")
                    .OnTable(TableName)
                    .OnColumn("employee_email");
                Create
                    .Index($"idx_{TableName}_status")
                    .OnTable(TableName)
                    .OnColumn("status");
            }
        }

        public override void Down()
        {
            if (Schema.Table(TableName).Exists())
                Delete.Table(TableName);
        }
    }
}