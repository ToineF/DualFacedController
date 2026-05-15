using AntoineFoucault.Utilities.Achievements;
using Cattac.Interactables.MouseCollection;
using DG.Tweening;

public class AchievementMouseTracker
{
    private AchievementData _firstMouse;
    private AchievementData _halfMice;
    private AchievementData _allMice;
    
    private static int _maxMice;
    private static int _miceCount;

    private bool _quarterUnlocked;
    private bool _halfUnlocked;
    private bool _allUnlocked;

    public void Initialize(AchievementData quarter, AchievementData half, AchievementData all)
    {
        _firstMouse = quarter;
        _halfMice = half;
        _allMice = all;
        
        _maxMice = MainGame.Instance.CollectiblesFactory.Mice.Length;
        MainGame.Instance.CollectiblesManager.MouseCollectibleManager.OnMouseGet += OnMouseGain;
    }

    private void OnMouseGain(SavedMouseData data, float a, Ease b)
    {
        _miceCount++;

        CheckAchievement(_firstMouse, _maxMice, ref _quarterUnlocked);
        CheckAchievement(_halfMice, 2, ref _halfUnlocked);
        CheckAchievement(_allMice, 1, ref _allUnlocked);
    }

    private void CheckAchievement(AchievementData data, int division, ref bool condition)
    {
        if (condition == false && _miceCount >= _maxMice / division)
        {
            condition = true;
            AchievementsManager.AddAchievement(data);
        }
    }
}