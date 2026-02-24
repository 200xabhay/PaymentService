using PaymentServices.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServices.Infrastructure.External
{
    public class LoanAccountClient
    {
        private readonly HttpClient _httpClient;
        public LoanAccountClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }
        public async Task<LoanAccountResponseDTO?> GetLoan(int loanId)
        {
            var response = await _httpClient.GetAsync($"/api/LoanAccount/{loanId}");

            if (!response.IsSuccessStatusCode)
                return null;

            var wrapper = await response.Content.ReadFromJsonAsync<ApiResponse<LoanAccountResponseDTO>>();

            return wrapper?.Data;
        }

    }
}
