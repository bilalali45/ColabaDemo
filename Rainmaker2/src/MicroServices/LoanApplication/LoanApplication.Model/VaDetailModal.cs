using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoanApplication.Model
{
    public class BorrowerVaDetailGetModel
    {
        public bool? IsVaEligible { get; set; }
        //public List<VaDetailModal> VaDetails { get; set; }
        //public List<MilitaryAffiliation> VaDetails { get; set; }
        public ArrayList VaDetails { get; set; }
    }

    public class VaDetailModal
    {
        public int? MilitaryAffiliationId { get; set; }
        public DateTime? ExpirationDateUtc { get; set; }
        public bool? ReserveEverActivated { get; set; }
    }

    public class VaDetailAddOrUpdate
    {
        public bool IsVaEligible { get; set; }
        public int[] MilitaryAffiliationIds { get; set; }
        public DateTime? ExpirationDateUtc { get; set; }
        public bool? ReserveEverActivated { get; set; }

        [Required]
        [Range(minimum: 1,
                  maximum: int.MaxValue,
                  ErrorMessage = "Invalid borrower id.")]
        public int BorrowerId { get; set; }
        public string State { get; set; }
    }

    public class BorrowerVaStatusModel
    {
        public bool IsVaEligible { get; set; }

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        public string State { get; set; }
    }

    public class MilitaryAffiliation
    {
        public int? MilitaryAffiliationId { get; set; }
    }

    public class ActiveDutyPersonnel : MilitaryAffiliation
    {
        public DateTime? ExpirationDateUtc { get; set; }
    }

    public class ReserveNationalGuard : MilitaryAffiliation
    {
        public bool? ReserveEverActivated { get; set; }
    }
}