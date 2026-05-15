using AntoineFoucault.Utilities.Achievements;

public class AchievementCheeseTracker
{
    private AchievementData _quarterCheeseAchievement;
    private AchievementData _halfCheeseAchievement;
    private AchievementData _allCheeseAchievement;
    
    private static int _maxCheeses;
    private static int _cheeseCount;

    private bool _quarterUnlocked;
    private bool _halfUnlocked;
    private bool _allUnlocked;

    public void Initialize(AchievementData quarter, AchievementData half, AchievementData all)
    {
        _quarterCheeseAchievement = quarter;
        _halfCheeseAchievement = half;
        _allCheeseAchievement = all;
        
        _maxCheeses = MainGame.Instance.CollectiblesManager.CheeseCollectiblesManager.MaxCheeses;
        MainGame.Instance.CollectiblesManager.CheeseCollectiblesManager.OnCheeseGain += OnCheeseGain;
    }

    private void OnCheeseGain(bool a)
    {
        _cheeseCount++;

        CheckAchievement(_quarterCheeseAchievement, 4, ref _quarterUnlocked);
        CheckAchievement(_halfCheeseAchievement, 2, ref _halfUnlocked);
        CheckAchievement(_allCheeseAchievement, 1, ref _allUnlocked);
    }

    private void CheckAchievement(AchievementData data, int division, ref bool condition)
    {
        if (condition == false && _cheeseCount >= _maxCheeses / division)
        {
            condition = true;
            AchievementsManager.AddAchievement(data);
        }
    }
}