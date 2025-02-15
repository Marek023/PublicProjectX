using System.Text;
using MarketDataService.Models.Asset;
using MarketDataService.Models.User;
using MarketDataService.Repositories.AssetRepositories.Interfaces;
using MarketDataService.Repositories.UserRepositories.Interfaces;
using MarketDataService.Services.HTMLTemplateServices.Interfaces;
using MarketDataService.Services.NotificationServices.Interfaces;
using ProjectX.Services.EmailService.Interface;

namespace MarketDataService.Services.NotificationServices.Implemenetations;

public class NotificationChangedAssetsService : INotificationChangedAssetsService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHtmlTemplateService _htmlTemplateService;
    private readonly IEmailService _emailService;

    public NotificationChangedAssetsService(IServiceScopeFactory serviceScopeFactory,
        IHtmlTemplateService htmlTemplateService,
        IEmailService emailService)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _htmlTemplateService = htmlTemplateService;
        _emailService = emailService;
    }

    public async Task SendMailWithChangedAssetsAsync()
    {
        var changedInAssetModel = CheckingChangesInAssets();
        if (changedInAssetModel.IsChange)
        {
            var htmlContent = GetHtmlTemplate(changedInAssetModel.NewAssets, changedInAssetModel.ExcludedAssets);
            SaveChangesToNotificationQueue(htmlContent);
            SetNotificationCreated(changedInAssetModel.NewAssets, changedInAssetModel.ExcludedAssets);

            var usersSubscribed = GetUsersSubscribedToAssetChanges();
            var lastRecord = GetLastRecordFromNotificationQueue();

            CreatingRelationUserAndNotification(usersSubscribed, lastRecord);
            
            await SendEmailsAsync(lastRecord);
        }
    }

    private ChangesInAssetsModel CheckingChangesInAssets()
    {
        var changedInAssetModel = new ChangesInAssetsModel();

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var newAssetRepository = scope.ServiceProvider.GetService<INewAssetRepository>()
                                     ?? throw new NullReferenceException("NewAssetRepository is null");

            var excludedAssetRepository = scope.ServiceProvider.GetService<IExcludedAssetRepository>()
                                          ?? throw new NullReferenceException("ExcludedAssetRepository is null");

            var newAssets = newAssetRepository.GetNewAssets();
            var excludedAssets = excludedAssetRepository.GetExcludedAssets();

            if (newAssets.Any() || excludedAssets.Any())
            {
                changedInAssetModel.IsChange = true;
                changedInAssetModel.NewAssets = newAssets;
                changedInAssetModel.ExcludedAssets = excludedAssets;
            }
        }

        return changedInAssetModel;
    }


    // private string GetChangedAssets()
    // {
    //     using (var scope = _serviceScopeFactory.CreateScope())
    //     {
    //         var newAssetRepository = scope.ServiceProvider.GetService<NewAssetRepository>();
    //         var excludedAssetRepository = scope.ServiceProvider.GetService<ExcludedAssetRepository>();
    //
    //         if (newAssetRepository is null || excludedAssetRepository is null)
    //         {
    //             throw new NullReferenceException("newAssetRepository or excludedAssetRepository is null");
    //         }
    //
    //         var newAssets = newAssetRepository.GetNewAssets();
    //         var excludedAssets = excludedAssetRepository.GetExcludedAssets();
    //
    //         var htmlTamplate = GetHtmlTemplate(newAssets, excludedAssets);
    //
    //         return htmlTamplate;
    //     }
    // }

    private string GetHtmlTemplate(List<AssetModel> newAssets, List<AssetModel> excludedAssets)
    {
        var editHtmlTemplate = string.Empty;
        switch ((newAssets.Any(), excludedAssets.Any()))
        {
            case (true, true):
                var newAndExcludedhtmlTemplate = _htmlTemplateService.GetNewAndExcludedAssetHtmlTemplate();
                editHtmlTemplate = SetNewAndExcludedDataToHtmlTemplate(
                    newAssets,
                    excludedAssets,
                    newAndExcludedhtmlTemplate);
                break;

            case (true, false):
                var newHtmlTemplate = _htmlTemplateService.GetOnlyAssetHtmlTemplate();
                editHtmlTemplate = SetDataToHtmlTemplate(newAssets, newHtmlTemplate);
                break;

            case (false, true):
                var excludedHtmlTemplate = _htmlTemplateService.GetOnlyExcludedAssetHtmlTemplate();
                editHtmlTemplate = SetDataToHtmlTemplate(excludedAssets, excludedHtmlTemplate);
                break;
        }

        return editHtmlTemplate;
    }

    private string SetNewAndExcludedDataToHtmlTemplate(
        List<AssetModel> newAssets,
        List<AssetModel> excludedAssets,
        string htmlTemplate)
    {
        var newAssetForHtmlTemplate = new StringBuilder();
        foreach (var asset in newAssets.OrderBy(asset => asset.AssetCategoryId).ThenBy(asset => asset.Symbol))
        {
            newAssetForHtmlTemplate.AppendLine(
                $"<li>Burza: {asset.AssetCategoryName?.Replace("_", " ")}, symbol: {asset.Symbol}, název: {asset.Name}.</li>");
        }

        var excludedAssetForHtmlTemplate = new StringBuilder();
        foreach (var asset in excludedAssets.OrderBy(asset => asset.AssetCategoryId).ThenBy(asset => asset.Symbol))
        {
            excludedAssetForHtmlTemplate.AppendLine(
                $"<li>Burza: {asset.AssetCategoryName?.Replace("_", " ")}, symbol: {asset.Symbol}, název: {asset.Name}.</li>");
        }

        var newHtmlTemplate = htmlTemplate.Replace("{newAssetsContent}", newAssetForHtmlTemplate.ToString());
        var excludedHtmlTemplate =
            newHtmlTemplate.Replace("{excludedAssetsContent}", excludedAssetForHtmlTemplate.ToString());

        return excludedHtmlTemplate;
    }

    private string SetDataToHtmlTemplate(List<AssetModel> assets, string htmlTemplate)
    {
        var contentForHtmlTemplate = new StringBuilder();
        foreach (var asset in assets.OrderBy(asset => asset.AssetCategoryId).ThenBy(asset => asset.Symbol))
        {
            contentForHtmlTemplate.AppendLine(
                $"<li>Burza: {asset.AssetCategoryName?.Replace("_", " ")}, symbol: {asset.Symbol}, název: {asset.Name}.</li>");
        }

        var newHtmlTemplate = htmlTemplate.Replace("{assetsContent}", contentForHtmlTemplate.ToString());

        return newHtmlTemplate;
    }


    private void SaveChangesToNotificationQueue(string htmlContent)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var assetNotificationQueueRepository =
                scope.ServiceProvider.GetService<IAssetNotificationQueueRepository>()
                ?? throw new NullReferenceException("AssetNotificationQueueRepository is null");

            assetNotificationQueueRepository.SaveNotificationContent(htmlContent);
        }
    }

    private void SetNotificationCreated(List<AssetModel> newAssets, List<AssetModel> excludedAssets)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var newAssetRepostiry = scope.ServiceProvider.GetService<INewAssetRepository>()
                                    ?? throw new NullReferenceException("NewAssetRepository is null");

            var excludeAssetRepostiry = scope.ServiceProvider.GetService<IExcludedAssetRepository>()
                                        ?? throw new NullReferenceException("ExcludedAssetRepository is null");

            newAssetRepostiry.SetNotificationCreated(newAssets);
            excludeAssetRepostiry.SetNotificationCreated(excludedAssets);
        }
    }

    private List<UserSettingModel> GetUsersSubscribedToAssetChanges()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var userSettingRepository = scope.ServiceProvider.GetService<IUserSettingRepository>()
                                        ?? throw new NullReferenceException("UserSetting repository is null");

            return userSettingRepository.GetUserSettings()
                .Where(user => user.SendAssetChanges)
                .ToList();
        }
    }

    private AssetNotificationQueueModel GetLastRecordFromNotificationQueue()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var assetNotificationQueueRepository =
                scope.ServiceProvider.GetService<IAssetNotificationQueueRepository>()
                ?? throw new NullReferenceException("Asset notification queue repository is null");

            return assetNotificationQueueRepository.GetLastNotificationContent();
        }
    }

    private void CreatingRelationUserAndNotification(List<UserSettingModel> usersSubscribed,
        AssetNotificationQueueModel lastRecord)
    {
        foreach (var userSubscribed in usersSubscribed)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var userSettingAssetNotificationQueueRepository =
                    scope.ServiceProvider.GetService<IUserSettingAssetNotificationQueueRepository>()
                    ?? throw new NullReferenceException("userSettingAssetNotificationQueueRepository is null");
                

                userSettingAssetNotificationQueueRepository.CreateRelation(userSubscribed.Id, lastRecord.Id);
            }
        }
    }

    private async Task SendEmailsAsync(AssetNotificationQueueModel lastRecord)
    {
        var relations = GetRelations();

        string subject = "Změna v aktivech";
        foreach (var relation in relations)
        {

            if (await _emailService.SendEmailAsync(relation.UserEmail, subject, lastRecord.NotificationContent))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var userSettingAssetNotificationQueueRepository =
                        scope.ServiceProvider.GetService<IUserSettingAssetNotificationQueueRepository>()
                        ?? throw new NullReferenceException("userSettingAssetNotificationQueueRepository is null");
                    
                    userSettingAssetNotificationQueueRepository.SetRelation(relation.Id); 
                }
            }
            else
            {
                throw new Exception($"SendEmailsAsync failed, {relation.UserEmail} not sent");
            }
        }
    }

    private List<UserSettingAssetNotificationQueueModel> GetRelations()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var userSettingAssetNotificationQueueRepository =
                scope.ServiceProvider.GetService<IUserSettingAssetNotificationQueueRepository>()
                ?? throw new NullReferenceException("userSettingAssetNotificationQueueRepository is null");

            return userSettingAssetNotificationQueueRepository.GetRelations();
        }
    }
}