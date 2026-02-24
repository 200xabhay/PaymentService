using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaymentServices.Application.DTO;
using PaymentServices.Application.Interfaces;
using PaymentServices.Domain.Entities;
using PaymentServices.Domain.Enums;
using PaymentServices.Infrastructure.Data;
using PaymentServices.Infrastructure.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServices.Infrastructure.Repository
{
    public class PaymentServiceRepo : IPaymentService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly LoanAccountClient loanAccountClient;
        private readonly EmiScheduleClient emiClient;
        public PaymentServiceRepo(ApplicationDbContext db,IMapper mapper,LoanAccountClient loanAccountClient, EmiScheduleClient emiClient)
        {
            this.db = db;
            this.mapper = mapper;
            this.loanAccountClient = loanAccountClient;
            this.emiClient = emiClient;


        }

        public async Task<PaymentResponseDTO> CreatePayment(CreatePaymentDTO dto)
        {
            var loan=await loanAccountClient.GetLoan(dto.LoanId);
            if (loan == null)
            {
                throw new Exception("Loan not found");
            }
            var nextEmi = await emiClient.GetNextEmi(dto.LoanId);
            if(nextEmi == null)
            {
                throw new Exception("No Pending Emi Found...");
            }
            decimal previousAdvance = await db.LoanPayments
            .Where(p => p.LoanId == dto.LoanId && p.PaymentStatus == PaymentStatus.Success)
            .SumAsync(p => p.AdvancePayment);
            decimal emiAmount = nextEmi.EmiAmount;
            decimal remainingAmount = emiAmount+previousAdvance;

            var payment = new LoanPayment
            {
                LoanId = dto.LoanId,
                PaymentAmount = emiAmount,
                PaymentDate=DateTime.UtcNow,
                PaymentMethod=PaymentMethod.AutoDebit,
                PaymentStatus=PaymentStatus.Success,
                CreatedAt=DateTime.UtcNow,
                ScheduleId=nextEmi.ScheduleId,
                AccountNumber= "1111111111111",
                BankName = "APNA BANK",
                TransactionId = $"TID-{Guid.NewGuid().ToString("N")[..8 ].ToUpper()}",
                TransactionReference = $"TRX-{DateTime.UtcNow:yyyyMMddHHmmssfff}",
                ReceiptNumber = $"REC-{DateTime.UtcNow.Year}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}"

            };
           


            payment.PenaltyPaid = 0;

            payment.InterestPaid = Math.Min(remainingAmount, nextEmi.InterestComponent);
            remainingAmount =remainingAmount- payment.InterestPaid;

            payment.PrincipalPaid = Math.Min(remainingAmount, nextEmi.PrincipalComponent);
            remainingAmount = remainingAmount-payment.PrincipalPaid;



            payment.AdvancePayment = remainingAmount;
            await db.LoanPayments.AddAsync(payment);
            await db.SaveChangesAsync();
            await emiClient.UpdateEmiAfterPayment(new UpdateEmiPaymentDTO
            {
                ScheduleId=nextEmi.ScheduleId,
                PaidAmount=dto.PaymentAmount+previousAdvance,
                PaymentDate=DateTime.UtcNow

            });
           
            return mapper.Map<PaymentResponseDTO>(payment);


        }

        public async Task<PaymentResponseDTO> GetPaymentById(int paymentId)
        {
            var payment =await  db.LoanPayments.FirstOrDefaultAsync(p => p.PaymentId == paymentId);
            return mapper.Map<PaymentResponseDTO>(payment);
        }

        public async Task<List<PaymentResponseDTO>> GetPaymentsByLoanId(int loanId)
        {
            var payments =  await db.LoanPayments.Where(p => p.LoanId == loanId && p.PaymentStatus == PaymentStatus.Success ).ToListAsync();
            return mapper.Map<List<PaymentResponseDTO>>(payments);
        }
    }
}
