
using MarketDataService.Models.User;

namespace MarketDataService.Repositories.UserRepositories.Interfaces;

public interface IUserSettingRepository
{
    List<UserSettingModel> GetUserSettings();
}