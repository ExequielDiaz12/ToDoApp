using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using mvc.Models;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;


namespace mvc.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly HttpClient _httpClient;
    public UserController(ILogger<UserController> logger,
    HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
        httpClient.BaseAddress = new Uri("http://localhost:5184");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("/User");
        if(response.IsSuccessStatusCode){
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<IEnumerable<UserViewModel>>(content);
            return View("Index", users);
        }
        return View(new List<UserViewModel>());
    }

    
}
