using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentServices.Application.DTO;
using PaymentServices.Application.Interfaces;

namespace PaymentServices.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;

        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentDTO dto)
        {
            var result = await paymentService.CreatePayment(dto);
            return Ok(ApiResponse<PaymentResponseDTO>.SuccessResponse(result,"Payment Added Successfully"));
        }
        [HttpGet("loan/{loanId}")]
        public async Task<IActionResult> GetPaymentsByLoanId(int loanId)
        {
            var result = await paymentService.GetPaymentsByLoanId(loanId);
            return Ok(ApiResponse<List<PaymentResponseDTO>>.SuccessResponse(result, "Payment Fetch Successfully"));
        }
        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPaymentById(int paymentId)
        {
            var result = await paymentService.GetPaymentById(paymentId);
            return Ok(ApiResponse<PaymentResponseDTO>.SuccessResponse(result,"Payment Fetch Successfully"));
        }
    }
}
