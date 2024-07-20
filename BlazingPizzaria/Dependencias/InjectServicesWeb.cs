
using BlazingPizza.Repositories.Interface.LocalCache.StorageCahceServices;
using BlazingPizzaria.Models.DTOs;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using Radzen;

namespace BlazingPizza.Dependencias
{
    /// <summary>
    ///  Todas dependências de serviços do projeto 
    /// </summary>
    public partial class InjectServicesWeb
    {
       
        public readonly IJSRuntime _jSRuntime;

        public readonly ILogger<ProdutoDtos> _logger;

        public HttpClient _httpClient { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [Inject]
        public DialogService _dialogService { get; set; }

        [Inject]
        public TooltipService _tooltipService { get; set; }

        [Inject]
        public ContextMenuService _contextMenuService { get; set; }

        [Inject]
        public NotificationService _notificationService { get; set; }


        public InjectServicesWeb(IJSRuntime IJSRuntime,ILogger<ProdutoDtos> Logger, HttpClient HttpClient,
            NavigationManager NavigationManager)
        {
            _jSRuntime = IJSRuntime;
            _logger = Logger;
            _httpClient = HttpClient;
            _navigationManager = NavigationManager;

        }
    }
}
