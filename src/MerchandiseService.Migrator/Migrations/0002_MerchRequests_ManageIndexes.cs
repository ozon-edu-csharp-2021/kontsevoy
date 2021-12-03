using FluentMigrator;

namespace MerchandiseService.Migrator.Migrations
{
    [Migration(2)]
    public class MerchRequestsManageIndexes : Migration
    {
        private const string TableName = "merch_requests";
        
        public override void Up()
        {
            if (!Schema.Table(TableName).Exists())
            {
                Delete.Index($"idx_{TableName}_employee_name");
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
            Delete.Index($"idx_{TableName}_status");
            Delete.Index($"idx_{TableName}_employee_email");
            
            Create
                .Index($"idx_{TableName}_employee_name")
                .OnTable(TableName)
                .OnColumn("employee_name");
        }
    }
}