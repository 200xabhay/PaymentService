using PaymentServices.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentServices.Infrastructure.External
{
    public class LoanAccountClient
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public LoanAccountClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoanAccountResponseDTO?> GetLoan(int loanId)
        {
            var response = await _httpClient.GetAsync($"/api/LoanAccount/{loanId}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var wrapper = JsonSerializer.Deserialize<ApiResponse<LoanAccountResponseDTO>>(json, _jsonOptions);
            return wrapper?.Data;
        }
    }
}