namespace MarketDataService.Models.User;

public class UserSettingModel
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public bool SendAssetChanges { get; set; }
}