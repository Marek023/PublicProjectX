using MarketDataService.Models.User;
using ProjectX.Data.Entities;

namespace MarketDataService.Extensions.User;

public static class UserSettingMappingExtensions
{
    public static List<UserSettingModel> ToModelList(this List<UserSetting> userSettings)
    {
        return userSettings.Select(userSetting => new UserSettingModel
        {
            Id = userSetting.Id,
            UserId = userSetting.UserId,
            SendAssetChanges = userSetting.SendAssetChanges ?? false,
        }).ToList();
    }
}