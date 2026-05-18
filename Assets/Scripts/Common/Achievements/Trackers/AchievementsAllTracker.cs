using AntoineFoucault.Utilities.Achievements;
public class AchievementAllTracker
{
    private AchievementData _allAchievement;

    public void Initialize(AchievementData all)
    {
        _allAchievement = all;
        AchievementsManager.OnAllAchievements += OnAll;
    }

    private void OnAll()
    {
        AchievementsManager.AddAchievement(_allAchievement);
    }

    public void Destroy()
    {
        AchievementsManager.OnAllAchievements -= OnAll;
    }

}