using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ujin.Models;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api")]
public class ComplexController : ControllerBase
{
    private readonly string _apiToken;

    public ComplexController(IConfiguration configuration)
    {
        _apiToken = configuration["ApiToken"];
    }

    [HttpPost("complexList")]
    public async Task<IActionResult> GetList()
    {
        var client = new HttpClient();

        var allTickets = await Helper.GetAllTickets(_apiToken);
        var mapObjects = JsonSerializer.SerializeToNode(allTickets.Select(x => x.objects.First()));

        var apiUrl2 = "https://api-uae-test.ujin.tech/api/v1/tck/objects/detailed/";
        var requestBodyForEnd = new JsonObject
        { {"token" , _apiToken }, {"objects" , mapObjects} };

        var bob = await client.PostAsync(apiUrl2, JsonContent.Create(requestBodyForEnd));
        var alise = await bob.Content.ReadAsStringAsync();
        var detailedObject = JsonSerializer.Deserialize<DetalasedObject>(alise);

        var complexNames = new HashSet<string>();
        foreach (var item in allTickets)
        {
            var itemParts = item.objects.First();
            var parts = detailedObject?.Data.FirstOrDefault(x => x.Object.Id == itemParts.Id);
            var complexName = parts?.Parts.FirstOrDefault(x => x.Type == "complex")?.Title;
            if (complexName != null)
                complexNames.Add(complexName);
        }

        return Ok(complexNames);
    }
    [HttpPost("complex")]
    public async Task<IActionResult> GetComplex([FromForm] string complexName)
    {
        var client = new HttpClient();

        var allTickets = await Helper.GetAllTickets(_apiToken);
        var mapObjects = JsonSerializer.SerializeToNode(allTickets.Select(x => x.objects.First()));

        var apiUrl2 = "https://api-uae-test.ujin.tech/api/v1/tck/objects/detailed/";
        var requestBodyForEnd = new JsonObject
        { {"token" , _apiToken }, {"objects" , mapObjects} };

        var bob = await client.PostAsync(apiUrl2, JsonContent.Create(requestBodyForEnd));
        var alise = await bob.Content.ReadAsStringAsync();
        var detailedObject = JsonSerializer.Deserialize<DetalasedObject>(alise);

        foreach (var item in allTickets)
        {
            var itemParts = item.objects.First();
            var parts = detailedObject?.Data.FirstOrDefault(x => x.Object.Id == itemParts.Id);
            if (parts != null)
            {
                item.parts = parts.Parts;
            }
        }
        var complex = allTickets.Where(x => x.parts.FirstOrDefault(x => x.Type == "complex")?.Title == complexName).ToList();
        return Ok(complex.Where(x => x.Status == "assigned"));
    }
}