using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentServices.Application.DTO
{
    public class LoanAccountResponseDTO
    {
        public int LoanId { get; set; }
        public decimal OutstandingPrincipal { get; set; }
        public decimal OutstandingInterest { get; set; }
        public decimal OutstandingPenalty { get; set; }
        public int RemainingTenure { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AccountStatus AccountStatus { get; set; }


    }
    public enum AccountStatus
    {
        Active   = 1,
        Closed = 2,
        Foreclosed = 3,
        WrittenOff = 4
    }
}
