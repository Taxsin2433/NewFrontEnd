using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Dtos;
using MVC.Models.DTO;
using MVC.Models.Enums;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services;

public class BasketService : IBasketService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<BasketService> _logger;

    public BasketService(IHttpClientService httpClient, ILogger<BasketService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task ReadUserId()
    {
        var result = await _httpClient.SendAsync<PaginatedItemsRequest<BasketTypeFilter>>($"{_settings.Value.BasketUrl}/defended",
           HttpMethod.Get, 
           new PaginatedItemsRequest<BasketTypeFilter>()
            {
                PageIndex = page,
                PageSize = take,
                Filters = filters
            });

        return result;
    }

}
