using PaymentServices.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServices.Domain.Entities
{
    public class LoanPayment
    {
        public int PaymentId { get; set; }   

        public int LoanId { get; set; }
        public int? ScheduleId { get; set; }

        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }

        public decimal PrincipalPaid { get; set; }
        public decimal InterestPaid { get; set; }
        public decimal PenaltyPaid { get; set; }
        public decimal AdvancePayment { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public string? TransactionReference { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
            
        public string BankName { get; set; }
        public string AccountNumber { get; set; }

        public string ReceiptNumber { get; set; }
        public string? Remarks { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public string? CreatedBy { get; set; }
        public string? DeletedBy { get; set; }
    }
}
