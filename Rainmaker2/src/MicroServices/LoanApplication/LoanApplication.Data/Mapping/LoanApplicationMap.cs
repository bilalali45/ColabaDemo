// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2.1
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace LoanApplicationDb.Data.Mapping
{
    using LoanApplicationDb.Entity.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    // LoanApplication
    
    public partial class LoanApplicationMap : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<LoanApplication>
    {
        public void Configure(EntityTypeBuilder<LoanApplication> builder)
        {
            builder.ToTable("LoanApplication", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.LoanNumber).HasColumnName(@"LoanNumber").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.AgencyNumber).HasColumnName(@"AgencyNumber").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.VisitorId).HasColumnName(@"VisitorId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OpportunityId).HasColumnName(@"OpportunityId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.BusinessUnitId).HasColumnName(@"BusinessUnitId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CustomerId).HasColumnName(@"CustomerId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LoanOriginatorId).HasColumnName(@"LoanOriginatorId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LoanGoalId).HasColumnName(@"LoanGoalId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LoanPurposeId).HasColumnName(@"LoanPurposeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.OtherLoanPurpose).HasColumnName(@"OtherLoanPurpose").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.LoanPurposeProgramId).HasColumnName(@"LoanPurposeProgramId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreditScoreNo).HasColumnName(@"CreditScoreNo").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ProductId).HasColumnName(@"ProductId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LoanTypeId).HasColumnName(@"LoanTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OtherLoanType).HasColumnName(@"OtherLoanType").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.StatusId).HasColumnName(@"StatusId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LockPeriodDays).HasColumnName(@"LockPeriodDays").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.NoteRate).HasColumnName(@"NoteRate").HasColumnType("decimal(18,3)").IsRequired(false);
            builder.Property(x => x.QualifyingRate).HasColumnName(@"QualifyingRate").HasColumnType("decimal(18,3)").IsRequired(false);
            builder.Property(x => x.Price).HasColumnName(@"Price").HasColumnType("decimal(18,3)").IsRequired(false);
            builder.Property(x => x.LoanTermMonths).HasColumnName(@"LoanTermMonths").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ProductAmortizationTypeId).HasColumnName(@"ProductAmortizationTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OtherAmortization).HasColumnName(@"OtherAmortization").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.EscrowWaiver).HasColumnName(@"EscrowWaiver").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.LoanAmount).HasColumnName(@"LoanAmount").HasColumnType("decimal(18,0)").IsRequired(false);
            builder.Property(x => x.CashOutAmount).HasColumnName(@"CashOutAmount").HasColumnType("decimal(18,0)").IsRequired(false);
            builder.Property(x => x.Deposit).HasColumnName(@"Deposit").HasColumnType("decimal(18,0)").IsRequired(false);
            builder.Property(x => x.MonthlyPi).HasColumnName(@"MonthlyPi").HasColumnType("decimal(18,0)").IsRequired(false);
            builder.Property(x => x.MonthlyMi).HasColumnName(@"MonthlyMi").HasColumnType("decimal(18,0)").IsRequired(false);
            builder.Property(x => x.MonthlyEscrow).HasColumnName(@"MonthlyEscrow").HasColumnType("decimal(18,0)").IsRequired(false);
            builder.Property(x => x.ExpectedClosingDate).HasColumnName(@"ExpectedClosingDate").HasColumnType("date").IsRequired(false);
            builder.Property(x => x.EverHadAVaLoan).HasColumnName(@"EverHadAVaLoan").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.VaLoanStatusId).HasColumnName(@"VaLoanStatusId").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.FirstTimeHomeBuyer).HasColumnName(@"FirstTimeHomeBuyer").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.DtiHousing).HasColumnName(@"DtiHousing").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DtiTotal).HasColumnName(@"DtiTotal").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.DocTypeId).HasColumnName(@"DocTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CompletedById).HasColumnName(@"CompletedById").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.SearchDateUtc).HasColumnName(@"SearchDateUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.SessionId).HasColumnName(@"SessionId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.FinalXml).HasColumnName(@"FinalXml").HasColumnType("ntext").IsRequired(false);
            builder.Property(x => x.EntityTypeId).HasColumnName(@"EntityTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName(@"IsDeleted").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.SubjectPropertyDetailId).HasColumnName(@"SubjectPropertyDetailId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.NoOfPeopleLiveIn).HasColumnName(@"NoOfPeopleLiveIn").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.AssetsUseForDownpayment).HasColumnName(@"AssetsUseForDownpayment").HasColumnType("decimal(18,0)").IsRequired(false);
            builder.Property(x => x.DownPaymentExplanation).HasColumnName(@"DownPaymentExplanation").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(3000);
            builder.Property(x => x.InformationMediumId).HasColumnName(@"InformationMediumId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.IsApplyingJointCredit).HasColumnName(@"IsApplyingJointCredit").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.NoOfBorrower).HasColumnName(@"NoOfBorrower").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ProjectTypeId).HasColumnName(@"ProjectTypeId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LoanApplicationFlowState).HasColumnName(@"LoanApplicationFlowState").HasColumnType("varbinary(max)").IsRequired(false);
            builder.Property(x => x.EncompassId).HasColumnName(@"EncompassId").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.EncompassNumber).HasColumnName(@"EncompassNumber").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.ByteLoanNumber).HasColumnName(@"ByteLoanNumber").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.ByteFileName).HasColumnName(@"ByteFileName").HasColumnType("nvarchar").IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.EncompassPostDateUtc).HasColumnName(@"EncompassPostDateUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.BytePostDateUtc).HasColumnName(@"BytePostDateUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.ModifiedBy).HasColumnName(@"ModifiedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ModifiedOnUtc).HasColumnName(@"ModifiedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CreatedOnUtc).HasColumnName(@"CreatedOnUtc").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.MilestoneId).HasColumnName(@"MilestoneId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LosMilestoneId).HasColumnName(@"LosMilestoneId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.TenantId).HasColumnName(@"TenantId").HasColumnType("int").IsRequired();
            builder.Property(x => x.BranchId).HasColumnName(@"BranchId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.LoanOfficerId).HasColumnName(@"LoanOfficerId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.SettingHash).HasColumnName(@"SettingHash").HasColumnType("varchar").IsRequired(false).IsUnicode(false).HasMaxLength(200);
            builder.Property(x => x.State).HasColumnName(@"State").HasColumnType("varchar(max)").IsRequired(false).IsUnicode(false);
            builder.Property(x => x.IsPropertyIdentified).HasColumnName(@"IsPropertyIdentified").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.IsEarnestMoneyProvided).HasColumnName(@"IsEarnestMoneyProvided").HasColumnType("bit").IsRequired(false);
            builder.Property(x => x.Comments).HasColumnName(@"Comments").HasColumnType("varchar(max)").IsRequired(false).IsUnicode(false);

            // Foreign keys
            builder.HasOne(a => a.LoanGoal).WithMany(b => b.LoanApplications).HasForeignKey(c => c.LoanGoalId).OnDelete(DeleteBehavior.SetNull); // FK_LoanApplication_LoanGoal
            builder.HasOne(a => a.LoanPurpose).WithMany(b => b.LoanApplications).HasForeignKey(c => c.LoanPurposeId).OnDelete(DeleteBehavior.SetNull); // FK_LoanApplication_LoanPurpose
            builder.HasOne(a => a.LoanPurposeProgram).WithMany(b => b.LoanApplications).HasForeignKey(c => c.LoanPurposeProgramId).OnDelete(DeleteBehavior.SetNull); // FK_LoanApplication_LoanPurposeProgram
            builder.HasOne(a => a.LoanType).WithMany(b => b.LoanApplications).HasForeignKey(c => c.LoanTypeId).OnDelete(DeleteBehavior.SetNull); // FK_LoanApplication_LoanType
            builder.HasOne(a => a.ProductAmortizationType).WithMany(b => b.LoanApplications).HasForeignKey(c => c.ProductAmortizationTypeId).OnDelete(DeleteBehavior.SetNull); // FK_LoanApplication_ProductAmortizationType
            builder.HasOne(a => a.ProjectType).WithMany(b => b.LoanApplications).HasForeignKey(c => c.ProjectTypeId).OnDelete(DeleteBehavior.SetNull); // FK_LoanApplication_ProjectType
            builder.HasOne(a => a.PropertyInfo).WithMany(b => b.LoanApplications).HasForeignKey(c => c.SubjectPropertyDetailId).OnDelete(DeleteBehavior.SetNull); // FK_LoanApplication_PropertyInfo
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
