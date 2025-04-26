using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace colectare_deseuri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ORSController : ControllerBase
    {
        private readonly HttpClient _client;

        public ORSController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
        }

        [HttpPost("segment")]
        public async Task<IActionResult> GetSegment([FromBody] List<List<double>> coords)
        {
            var payload = new { coordinates = coords };
            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openrouteservice.org/v2/directions/driving-car/geojson")
            {
                Content = content
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "5b3ce3597851110001cf6248fe3fef57b4dd4e818839ee3bc8a70e10");

            var response = await _client.SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return Content(jsonResponse, "application/json");
        }
    }
}
