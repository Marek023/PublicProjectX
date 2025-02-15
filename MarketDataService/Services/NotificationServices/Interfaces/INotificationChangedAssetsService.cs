namespace MarketDataService.Services.NotificationServices.Interfaces;

public interface INotificationChangedAssetsService
{
    Task SendMailWithChangedAssetsAsync();
}