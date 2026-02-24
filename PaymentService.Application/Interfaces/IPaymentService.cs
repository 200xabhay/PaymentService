using PaymentServices.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServices.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseDTO> CreatePayment (CreatePaymentDTO dto);
        Task<List<PaymentResponseDTO>> GetPaymentsByLoanId(int  loanId);
        Task<PaymentResponseDTO> GetPaymentById(int paymentId);

    }
}
