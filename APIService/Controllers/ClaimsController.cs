using APIService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIService.Controllers
{
    [Route("Claims")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        [HttpPost("CreateClaim")]
        public async Task<ResponseModel> CreateClaim(ClaimModel requestDetail)
        {
            var _httpClient = new HttpClient();

            var jsonPayload = JsonConvert.SerializeObject(requestDetail.ClaimsBody);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            // TODO: Add logging

            // TODO: Get client's Claim URL here
            var claimUrl = "";

            var response = await _httpClient.PostAsync(claimUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                // TODO: log failure in error table
                throw new Exception($"Failed to send claim payload. Status code: {response.StatusCode}");
            }
            return new ResponseModel();
        }
    }
}
