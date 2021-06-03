using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {

            //GenerateSetupTableUdfScripts();

            //GenerateScriptsForStateCityCountyAndZipCode();

             //GeneratePartialModelForSetupTables();

            //GenerateEfWrappers();

            GenerateDbFunctionServiceMembers();

            //GenerateScriptsforaudittrigger();




        }

        private static void GeneratePartialModelForSetupTables()
        {
            string path = $"D:\\Projects\\Rainmaker2\\Rainmaker2\\src\\MicroServices\\LoanApplication\\LoanApplication.Entity\\Models\\Partials\\SetupTablePartials.cs";
            if (File.Exists(path))
                File.Delete(path);

            using (StreamWriter writer = File.CreateText(path))
            {
                writer.WriteLine($"namespace LoanApplicationDb.Entity.Models");
                writer.WriteLine($"{{");
                foreach (var tableName in _setupTables)
                {
                    writer.WriteLine($"    public partial class {tableName}");
                    writer.WriteLine($"    {{");
                    writer.WriteLine($"        public string DisplayName {{ get; set; }}");
                    writer.WriteLine($"        public string TenantAlternateName {{ get; set; }}");
                    writer.WriteLine($"    }}");
                }
                writer.WriteLine($"");
                writer.WriteLine($"}}");
            }
        }

        private static void GenerateScriptsForStateCityCountyAndZipCode()
        {


            List<string> setupTables2 = new List<string>
                                             {

                                                "StateCountyCity",

                                                "ZipCode",

                                             };

            foreach (var tableName in setupTables2)
            {
                Console.WriteLine($"DROP FUNCTION dbo.[udf{tableName}]");
                Console.WriteLine($"Go");
                Console.WriteLine($"CREATE FUNCTION [dbo].[udf{tableName}] (@tenantId INT)");
                Console.WriteLine($"RETURNS TABLE");
                Console.WriteLine($"AS");
                Console.WriteLine($"RETURN(SELECT * FROM dbo.{tableName} x WHERE NOT EXISTS(SELECT EntityRefId FROM dbo.InActiveSetupItems iasi WHERE(TenantId=@tenantId AND iasi.EntityRefId=x.Id)AND EntityName='{tableName}'));");
                Console.WriteLine($"Go");
                Console.WriteLine($"\n\n");
            }

        }

        private static void GenerateDbFunctionServiceMembers()
        {

            foreach (var tableName in _setupTables)
            {
                Console.WriteLine($"public IQueryable<{tableName}> Udf{tableName}(int tenantId,int? sectionId=null) {{ return Uow.DataContext.Udf{tableName}(tenantId,sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }}");

            }
        }



        private static void GenerateEfWrappers()
        {

            foreach (var tableName in _setupTables)
            {
                Console.WriteLine($"public IQueryable<{tableName}> Udf{tableName}(int tenantId, int? sectionId=null) =>  Set<{tableName}>().FromSqlInterpolated($\"select * from udf{tableName} ({{tenantId}},{{sectionId}})\");");

            }
        }


        private static void GenerateSetupTableUdfScripts()
        {
            List<string> _setupTables2 = new List<string>();

            foreach (var tableName in _setupTables)
            {
                Console.WriteLine($"DROP FUNCTION [dbo].[udf{tableName}]");
                Console.WriteLine($"GO");
                Console.WriteLine($"");
                Console.WriteLine($"SET ANSI_NULLS ON");
                Console.WriteLine($"GO");
                Console.WriteLine($"");
                Console.WriteLine($"SET QUOTED_IDENTIFIER ON");
                Console.WriteLine($"GO");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"");
                Console.WriteLine($"CREATE FUNCTION [dbo].[udf{tableName}]");
                Console.WriteLine($"(");
                Console.WriteLine($"    @tenantId INT,");
                Console.WriteLine($"    @sectionId INT = NULL");
                Console.WriteLine($")");
                Console.WriteLine($"RETURNS TABLE");
                Console.WriteLine($"AS");
                Console.WriteLine($"RETURN");
                Console.WriteLine($"(");
                Console.WriteLine($"    SELECT x.*,");
                Console.WriteLine($"           sitn.AlternateName TenantAlternateName,");
                Console.WriteLine($"           COALESCE (sitn.AlternateName, x.Name) DisplayName");
                Console.WriteLine($"    FROM dbo.{tableName} x");
                Console.WriteLine($"        LEFT JOIN dbo.SetupItemsTenantName sitn ON sitn.TenantId = @tenantId");
                Console.WriteLine($"                                                   AND sitn.EntityName = '{tableName}'");
                Console.WriteLine($"                                                   AND sitn.EntityRefId = x.Id");
                Console.WriteLine($"        LEFT JOIN dbo.InActiveSetupItems iasi ON iasi.EntityRefId = x.Id");
                Console.WriteLine($"                                                 AND iasi.TenantId = @tenantId");
                Console.WriteLine($"                                                 AND iasi.EntityName = '{tableName}'");
                Console.WriteLine($"        LEFT JOIN dbo.InActiveSetupItemsSectionWise iasisw ON iasisw.EntityRefId = x.Id");
                Console.WriteLine($"                                                              AND iasisw.SectionId = @sectionId");
                Console.WriteLine($"                                                              AND iasisw.EntityName = '{tableName}'");
                Console.WriteLine($"    WHERE iasi.Id IS NULL");
                Console.WriteLine($"          AND iasisw.Id IS NULL");
                Console.WriteLine($");");
                Console.WriteLine($"");
                Console.WriteLine($"GO");
            }

        }

        private static void GenerateScriptsforaudittrigger()
        {
            string path = $"C:\\auditTriggers.sql";
            if (File.Exists(path))
                File.Delete(path);

            using (StreamWriter writer = File.CreateText(path))
            {
                foreach (var tableName in _loanAppAuditLogTables)
                {

                    //Insert
                    writer.WriteLine($"DROP TRIGGER IF EXISTS [dbo].[TR_{tableName}_AuditLog_Insert]");
                    writer.WriteLine($"GO");
                    writer.WriteLine($"Create TRIGGER [dbo].[TR_{tableName}_AuditLog_Insert]");
                    writer.WriteLine($"ON [dbo].[{tableName}]");
                    writer.WriteLine($"AFTER Insert AS");
                    writer.WriteLine($"BEGIN");
                    writer.WriteLine($"DECLARE @loggedUser int");
                    writer.WriteLine($"SELECT @loggedUser=convert(int,SESSION_CONTEXT(N'loggedUser'))");
                    writer.WriteLine($"DECLARE @correlationId varchar(50)");
                    writer.WriteLine($"SELECT @correlationId=convert(varchar(50),SESSION_CONTEXT(N'correlationId'))");
                    writer.WriteLine($"DECLARE @transactionId varchar(50)");
                    writer.WriteLine($"SELECT @transactionId=convert(varchar(50),SESSION_CONTEXT(N'transactionId'))");
                    writer.WriteLine($"DECLARE @tenantId int");
                    writer.WriteLine($"SELECT @tenantId=convert(int,SESSION_CONTEXT(N'tenantId'))");

                    writer.WriteLine($"DECLARE @id bigint");
                    writer.WriteLine($"DECLARE new_item CURSOR FOR");
                    writer.WriteLine($"SELECT Id FROM inserted");
                    writer.WriteLine($"OPEN new_item");
                    writer.WriteLine($"fetch next from new_item into @id");
                    writer.WriteLine($"while @@FETCH_STATUS=0");
                    writer.WriteLine($"begin");

                    writer.WriteLine($"INSERT INTO ColabaLog..AuditLog(");
                    writer.WriteLine($"PrimaryKey,");
                    writer.WriteLine($"TableName,");
                    writer.WriteLine($"OldRowData,");
                    writer.WriteLine($"NewRowData,");
                    writer.WriteLine($"DmlType,");
                    writer.WriteLine($"TenantId,");
                    writer.WriteLine($"CorrelationId,");
                    writer.WriteLine($"TransactionId,");
                    writer.WriteLine($"ModifiedBy,");
                    writer.WriteLine($"ModifiedOnUtc");
                    writer.WriteLine($")");
                    writer.WriteLine($"VALUES(");

                    writer.WriteLine($"@id,");
                    writer.WriteLine($"'{tableName}',");

                    writer.WriteLine($"null,");
                    writer.WriteLine($"(SELECT * FROM Inserted where id=@id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),");
                    writer.WriteLine($"'Insert',");
                    writer.WriteLine($"@tenantId,");
                    writer.WriteLine($"@correlationId,");
                    writer.WriteLine($"@transactionId,");
                    writer.WriteLine($"@loggedUser,");
                    writer.WriteLine($"GETUTCDATE()");
                    writer.WriteLine($");");

                    writer.WriteLine($"fetch next from new_item into @id;");
                    writer.WriteLine($"end");
                    writer.WriteLine($"close new_item;");
                    writer.WriteLine($"deallocate new_item;");

                    writer.WriteLine($"END");
                    writer.WriteLine($"go");

                    //update
                    writer.WriteLine($"DROP TRIGGER IF EXISTS [dbo].[TR_{tableName}_AuditLog_Update]");
                    writer.WriteLine($"GO");
                    writer.WriteLine($"Create TRIGGER [dbo].[TR_{tableName}_AuditLog_Update]");
                    writer.WriteLine($"ON [dbo].[{tableName}]");
                    writer.WriteLine($"AFTER Update AS");
                    writer.WriteLine($"BEGIN");
                    writer.WriteLine($"DECLARE @loggedUser int");
                    writer.WriteLine($"SELECT @loggedUser=convert(int,SESSION_CONTEXT(N'loggedUser'))");
                    writer.WriteLine($"DECLARE @correlationId varchar(50)");
                    writer.WriteLine($"SELECT @correlationId=convert(varchar(50),SESSION_CONTEXT(N'correlationId'))");
                    writer.WriteLine($"DECLARE @transactionId varchar(50)");
                    writer.WriteLine($"SELECT @transactionId=convert(varchar(50),SESSION_CONTEXT(N'transactionId'))");
                    writer.WriteLine($"DECLARE @tenantId int");
                    writer.WriteLine($"SELECT @tenantId=convert(int,SESSION_CONTEXT(N'tenantId'))");

                    writer.WriteLine($"DECLARE @id bigint");
                    writer.WriteLine($"DECLARE new_item CURSOR FOR");
                    writer.WriteLine($"SELECT Id FROM inserted");
                    writer.WriteLine($"OPEN new_item;");
                    writer.WriteLine($"fetch next from new_item into @id;");
                    writer.WriteLine($"while @@FETCH_STATUS=0");
                    writer.WriteLine($"begin");

                    writer.WriteLine($"INSERT INTO ColabaLog..AuditLog(");
                    writer.WriteLine($"PrimaryKey,");
                    writer.WriteLine($"TableName,");
                    writer.WriteLine($"OldRowData,");
                    writer.WriteLine($"NewRowData,");
                    writer.WriteLine($"DmlType,");
                    writer.WriteLine($"TenantId,");
                    writer.WriteLine($"CorrelationId,");
                    writer.WriteLine($"TransactionId,");
                    writer.WriteLine($"ModifiedBy,");
                    writer.WriteLine($"ModifiedOnUtc");
                    writer.WriteLine($")");
                    writer.WriteLine($"VALUES(");

                    writer.WriteLine($"@id,");
                    writer.WriteLine($"'{tableName}',");
                    writer.WriteLine($"(SELECT * FROM Deleted where id=@id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),");
                    writer.WriteLine($"(SELECT * FROM Inserted where id=@id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),");


                    writer.WriteLine($"'Update',");
                    writer.WriteLine($"@tenantId,");
                    writer.WriteLine($"@correlationId,");
                    writer.WriteLine($"@transactionId,");
                    writer.WriteLine($"@loggedUser,");
                    writer.WriteLine($"GETUTCDATE()");
                    writer.WriteLine($");");

                    writer.WriteLine($"fetch next from new_item into @id;");
                    writer.WriteLine($"end");
                    writer.WriteLine($"close new_item;");
                    writer.WriteLine($"deallocate new_item;");

                    writer.WriteLine($"END");
                    writer.WriteLine($"go");

                    //Delete
                    writer.WriteLine($"DROP TRIGGER IF EXISTS [dbo].[TR_{tableName}_AuditLog_Delete]");
                    writer.WriteLine($"GO");
                    writer.WriteLine($"Create TRIGGER [dbo].[TR_{tableName}_AuditLog_Delete]");
                    writer.WriteLine($"ON [dbo].[{tableName}]");
                    writer.WriteLine($"AFTER Delete AS");
                    writer.WriteLine($"BEGIN");
                    writer.WriteLine($"DECLARE @loggedUser int");
                    writer.WriteLine($"SELECT @loggedUser=convert(int,SESSION_CONTEXT(N'loggedUser'))");
                    writer.WriteLine($"DECLARE @correlationId varchar(50)");
                    writer.WriteLine($"SELECT @correlationId=convert(varchar(50),SESSION_CONTEXT(N'correlationId'))");
                    writer.WriteLine($"DECLARE @transactionId varchar(50)");
                    writer.WriteLine($"SELECT @transactionId=convert(varchar(50),SESSION_CONTEXT(N'transactionId'))");
                    writer.WriteLine($"DECLARE @tenantId int");
                    writer.WriteLine($"SELECT @tenantId=convert(int,SESSION_CONTEXT(N'tenantId'))");

                    writer.WriteLine($"DECLARE @id bigint");
                    writer.WriteLine($"DECLARE new_item CURSOR FOR");
                    writer.WriteLine($"SELECT Id FROM deleted");
                    writer.WriteLine($"OPEN new_item;");
                    writer.WriteLine($"fetch next from new_item into @id;");
                    writer.WriteLine($"while @@FETCH_STATUS=0");
                    writer.WriteLine($"begin");

                    writer.WriteLine($"INSERT INTO ColabaLog..AuditLog(");
                    writer.WriteLine($"PrimaryKey,");
                    writer.WriteLine($"TableName,");
                    writer.WriteLine($"OldRowData,");
                    writer.WriteLine($"NewRowData,");
                    writer.WriteLine($"DmlType,");
                    writer.WriteLine($"TenantId,");
                    writer.WriteLine($"CorrelationId,");
                    writer.WriteLine($"TransactionId,");
                    writer.WriteLine($"ModifiedBy,");
                    writer.WriteLine($"ModifiedOnUtc");
                    writer.WriteLine($")");
                    writer.WriteLine($"VALUES(");

                    writer.WriteLine($"@id,");

                    writer.WriteLine($"'{tableName}',");
                    writer.WriteLine($"(SELECT * FROM Deleted where id=@id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),");
                    writer.WriteLine($"null,");
                    writer.WriteLine($"'Deleted',");
                    writer.WriteLine($"@tenantId,");
                    writer.WriteLine($"@correlationId,");
                    writer.WriteLine($"@transactionId,");
                    writer.WriteLine($"@loggedUser,");
                    writer.WriteLine($"GETUTCDATE()");
                    writer.WriteLine($");");

                    writer.WriteLine($"fetch next from new_item into @id;");
                    writer.WriteLine($"end");
                    writer.WriteLine($"close new_item;");
                    writer.WriteLine($"deallocate new_item;");

                    writer.WriteLine($"END");
                    writer.WriteLine($"go");

                }

            }




        }


      private  static readonly List<string> _loanAppAuditLogTables = new List<string>
                    {
                    "EthnicityDetail",
                    "MilitaryAffiliation",
                    "AssetType",
                    "VaDetails",
                    "QuestionGroup",
                    "MaritalStatusType",
                    "LoanContactEthnicityBinder",
                    "IncomeCategory",
                    "OwnerShipInterest",
                    "EscrowEntityType",
                    "QuestionSection",
                    "AssetTypeGiftSourceBinder",
                    "Config",
                    "CountyType",
                    "IncomeInfo",
                    "Question",
                    "LoanType",
                    "IncomeType",
                    "OtherIncomeInfo",
                    "Country",
                    "LoanPurposeProgram",
                    "LoanContactEncryption",
                    "QuestionOption",
                    "VaOccupancy",
                    "BorrowerSupportPayment",
                    "IncomeGroup",
                    "State",
                    "BorrowerResidence",
                    "MilitaryStatusList",
                    "LoanPurpose",
                    "QuestionResponse",
                    "ConsentType",
                    "PropertyUsage",
                    "BorrowerQuestionResponse",
                    "PropertyTax",
                     "QuestionPurposeBinder",
                    "BorrowerConsent",
                    "TitleTrustInfo",
                    "BorrowerProperty",
                    "LoanGoal",
                    "BorrowerLiability",
                    "TitleManner",
                    "StateCountyCity",
                    "LiabilityType",
                    "ProjectType",
                    "ZipCode",
                    "County",
                    "TitleLandTenure",
                    "JobType",
                    "ProductFamily",
                    "City",
                    "TitleHeldWith",
                    "BorrowerConsentLog-toberemoved",
                    "BankRuptcy",
                    "ProductAmortizationType",
                    "ConfigSelection",
                    "AddressInfo",
                    "TitleEstate",
                    "ConfigSelectionItem",
                    "BorrowerBankRuptcy",
                    "GiftSource",
                    "PaidBy",
                    "Entity",
                    "EntityType",
                    "PropertyInfo",
                    "SecondLienType",
                    "Gender",
                    "OwnType",
                    "BorrowerAsset",
                    "ResidencyType",
                    "PropertyTaxEscrow",
                    "MaritalStatusList",
                    "AssetBorrowerBinder",
                    "LoanContact",
                    "OwnershipType",
                    "AssetCategory",
                    "MortgageOnProperty",
                    "ResidencyState",
                    "LoanApplication",
                    "OtherIncomeType",
                    "PropertyType",
                    "LoanContactRaceBinder",
                    "Race",
                    "SetupItemsTenantName",
                    "FamilyRelationType",
                    "CollateralAssetType",
                    "InActiveSetupItems",
                    "RaceDetail",
                    "Ethnicity",
                    "MilitaryBranch",
                    "QuestionType",
                    "Borrower"

        };


        static readonly List<string> _setupTables = new List<string>
                                             {
                                                 "AssetCategory",
                                                 "AssetType",
                                                 "AssetTypeGiftSourceBinder",
                                                 "BankRuptcy",
                                                 "City",
                                                 "CollateralAssetType",
                                                 "Config",
                                                 "ConfigSelectionItem",
                                                 "Country",
                                                 "County",
                                                 "CountyType",
                                                 "Entity",
                                                 "EntityType",
                                                 "EscrowEntityType",
                                                 "Ethnicity",
                                                 "EthnicityDetail",
                                                 "FamilyRelationType",
                                                 "Gender",
                                                 "GiftSource",
                                                 "IncomeCategory",
                                                 "IncomeGroup",
                                                 "IncomeType",
                                                 "JobType",
                                                 "LiabilityType",
                                                 "LoanGoal",
                                                 "LoanPurpose",
                                                 "LoanPurposeProgram",
                                                 "LoanType",
                                                 "MaritalStatusList",
                                                 "MaritalStatusType",
                                                 "MilitaryAffiliation",
                                                 "MilitaryBranch",
                                                 "MilitaryStatusList",
                                                 "OtherIncomeType",
                                                 "OwnershipType",
                                                 "OwnType",
                                                 "PaidBy",
                                                 "ProductAmortizationType",
                                                 "ProductFamily",
                                                 "ProjectType",
                                                 "PropertyType",
                                                 "PropertyUsage",
                                                 "Question",
                                                 "QuestionGroup",
                                                 "QuestionOption",
                                                 "QuestionPurposeBinder",
                                                 "QuestionSection",
                                                 "QuestionType",
                                                 "Race",
                                                 "RaceDetail",
                                                 "ResidencyState",
                                                 "ResidencyType",
                                                 "MortgageType",
                                                 "State",
                                                 //"StateCountyCity",
                                                 "TitleEstate",
                                                 "TitleHeldWith",
                                                 "TitleLandTenure",
                                                 "TitleManner",
                                                 "TitleTrustInfo",
                                                 //"ZipCode",


                                             };
    }
}
