using AntoineFoucault.Utilities.Achievements;
using NaughtyAttributes;
using UnityEngine;


public class AchievementCatsInWater : MonoBehaviour
{
    [SerializeField] private AchievementData _oneCatInWater;
    [SerializeField] private AchievementData _allCatsInWater;
    [SerializeField] private int _allCatsCount;

    private static int _catsInWater;

    private void Start()
    {
        CatWater.OnEnterWaterInternal += AddCatInWater;
    }

    private void OnDestroy()
    {
        CatWater.OnEnterWaterInternal -= AddCatInWater;
    }

#if UNITY_EDITOR
    [Button]
    public void GetCats()
    {
        _allCatsCount = FindObjectsByType<CatWater>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID).Length;
    }
    #endif

    public void AddCatInWater()
    {
        _catsInWater++;
        if (_catsInWater == 1) AchievementsManager.AddAchievement(_oneCatInWater);
        if (_catsInWater == _allCatsCount) AchievementsManager.AddAchievement(_allCatsInWater);
    }
}