using PaymentServices.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServices.Application.DTO
{
    
        public class CreatePaymentDTO
        {
            [Required]
            public int LoanId { get; set; }


            [Required]
            [Range(0.01, double.MaxValue, ErrorMessage = "PaymentAmount must be greater than 0")]
            public decimal PaymentAmount { get; set; }

            //[Required]
            //public PaymentMethod PaymentMethod { get; set; }    

            
        }
        public class PaymentResponseDTO
        {
            public int PaymentId { get; set; }
            public int LoanId { get; set; }

            public decimal PaymentAmount { get; set; }

            public decimal PrincipalPaid { get; set; }
            public decimal InterestPaid { get; set; }
            public decimal PenaltyPaid { get; set; }
            public decimal AdvancePayment { get; set; }

            public PaymentStatus PaymentStatus { get; set; }
            public DateTime PaymentDate { get; set; }
        }
    }

