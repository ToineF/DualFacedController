using UnityEngine;

/// <summary>
/// Manages all achievements trackers in one MonoBehaviour
/// </summary>
public class AchievementTrackersManager : MonoBehaviour
{
    [Header("Cheese")]
    [SerializeField] private AchievementData _quarterCheeseAchievement;
    [SerializeField] private AchievementData _halfCheeseAchievement;
    [SerializeField] private AchievementData _allCheeseAchievement;
    
    [Header("Mouse")]
    [SerializeField] private AchievementData _firstMouse;
    [SerializeField] private AchievementData _halfMice;
    [SerializeField] private AchievementData _allMice;
    
    [Header("All")]
    [SerializeField] private AchievementData _achievementsAll;
    
    private AchievementAllTracker _allTracker;
    
    private void Start()
    {
        // Cheese
        AchievementCheeseTracker cheeseTracker = new AchievementCheeseTracker();
        cheeseTracker.Initialize(_quarterCheeseAchievement, _halfCheeseAchievement, _allCheeseAchievement);
        
        // Mice
        AchievementMouseTracker miceTracker = new AchievementMouseTracker();
        miceTracker.Initialize(_firstMouse, _halfMice, _allMice);
        
        // All
        _allTracker = new AchievementAllTracker();
        _allTracker.Initialize(_achievementsAll);
    }

    private void OnDestroy()
    {
        _allTracker.Destroy();
    }
}