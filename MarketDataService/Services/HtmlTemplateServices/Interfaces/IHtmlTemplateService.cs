namespace MarketDataService.Services.HTMLTemplateServices.Interfaces;

public interface IHtmlTemplateService
{
    string GetNewAndExcludedAssetHtmlTemplate();
    string GetOnlyAssetHtmlTemplate();
    string GetOnlyExcludedAssetHtmlTemplate();
}