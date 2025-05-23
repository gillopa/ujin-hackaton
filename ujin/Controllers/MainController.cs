using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using ujin.Models;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

[ApiController]
[Route("api")]
public class BadSomethingController : ControllerBase
{
    private readonly string _apiToken;

    public BadSomethingController(IConfiguration configuration)
    {
        _apiToken = configuration["ApiToken"];
    }

    [HttpPost("insidents")]
    public async Task<IActionResult> GetBadSomething([FromForm] string? objectType)
    {
        var client = new HttpClient();

        var allTickets = await Helper.GetAllTickets(_apiToken);
        List<Ticket>? buffer = new();
        if (string.IsNullOrEmpty(objectType))
            buffer = allTickets;
        else
            buffer = allTickets.Where(x => x.objects.First().Type == objectType).ToList();
        var mapObjects = JsonSerializer.SerializeToNode(buffer.Select(x => x.objects.First()));

        var apiUrl2 = "https://api-uae-test.ujin.tech/api/v1/tck/objects/detailed/";
        var requestBodyForEnd = new JsonObject
        { {"token" , _apiToken }, {"objects" , mapObjects} };

        var bob = await client.PostAsync(apiUrl2, JsonContent.Create(requestBodyForEnd));
        var alise = await bob.Content.ReadAsStringAsync();
        var detailedObject = JsonSerializer.Deserialize<DetalasedObject>(alise);

        foreach (var item in buffer)
        {
            var itemParts = item.objects.First();
            var parts = detailedObject?.Data.FirstOrDefault(x => x.Object.Id == itemParts.Id);
            if (parts != null)
            {
                item.parts = parts.Parts;
            }
        }
        return Ok(buffer.Where(x => x.Status == "assigned"));
    }
}
public class Helper
{
    public static async Task<List<Ticket>> GetAllTickets(string apiToken)
    {
        var apiUrl = "https://api-uae-test.ujin.tech/api/v1/tck/bms/tickets/list/";
        var client = new HttpClient();
        var allTickets = new List<Ticket>();
        int currentPage = 1;
        int totalPages = 1; // Изначально предполагаем одну страницу

        do
        {
            var requestBody = new { token = apiToken, page = currentPage };
            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, new MediaTypeHeaderValue("application/json"));

            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ticketResponse = JsonSerializer.Deserialize<TicketResponse>(jsonResponse);

                if (ticketResponse != null && ticketResponse.Data != null)
                {
                    if (currentPage == 1 && ticketResponse.Data.PerPage > 0)
                    {
                        // Рассчитываем общее количество страниц после первого запроса
                        totalPages = (int)Math.Ceiling((double)ticketResponse.Data.Total / ticketResponse.Data.PerPage);
                    }

                    if (ticketResponse.Data.Tickets != null)
                    {
                        allTickets.AddRange(ticketResponse.Data.Tickets);
                    }
                }
                else
                {
                    throw new Exception("Something went wrong");
                    // Обработка случая, когда десериализация не удалась или данные пусты
                }
            }
            else
            {
                // Обработка ошибок HTTP
                throw new Exception("Something went wrong");
            }

            currentPage++;

        } while (currentPage <= totalPages);
        return allTickets;
    }
}