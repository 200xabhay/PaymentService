using PaymentServices.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServices.Infrastructure.External
{
    public class EmiScheduleClient
    {
        private readonly HttpClient _client;
        public EmiScheduleClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<EmiResponseDTO> GetNextEmi(int loandId)
        {
            var response = await _client.GetAsync($"api/Emi/{loandId}/upcoming");
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("EmiSchedule Service Error");
            }
            var apiResonse=await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<EmiResponseDTO>>>();
            return apiResonse?.Data?.FirstOrDefault();
            
        }
         public async Task UpdateEmiAfterPayment(UpdateEmiPaymentDTO dto)
        {
            var response = await _client.PutAsJsonAsync("api/Emi/update-payment", dto);
        }
    }
}
