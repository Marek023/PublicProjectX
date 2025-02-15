using MarketDataService.Extensions.User;
using MarketDataService.Models.User;
using MarketDataService.Repositories.UserRepositories.Interfaces;
using ProjectX.Data.Contexts;

namespace MarketDataService.Repositories.UserRepositories.Implementations;

public class UserSettingRepository : IUserSettingRepository
{
    private readonly IApplicationDbContext _dbContext;

    public UserSettingRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<UserSettingModel> GetUserSettings()
    {
        return _dbContext.UserSetting.ToList().ToModelList();
    }
}