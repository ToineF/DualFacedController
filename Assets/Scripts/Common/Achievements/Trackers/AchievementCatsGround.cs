using AntoineFoucault.Utilities.Achievements;
using Cattac.Collectibles.Save;
using UnityEngine;

public class AchievementCatsGround : MonoBehaviour
{
    [SerializeField] private AchievementData _oneCatFall;
    [SerializeField] private AchievementData _allCatsFallLocation;
    [SerializeField] private CatGround[] _cats;

    private static int _catsInLocationCount;
    
    private void Start()
    {
        CatGround.OnFallInternal += OneCatFall;
        foreach (var cat in _cats)
        {
            if (cat != null) cat.OnFall += AllCatsLocationFall;
        }
    }

    private void OnDestroy()
    {
        CatGround.OnFallInternal -= OneCatFall;
        foreach (var cat in _cats)
        {
            if (cat != null) cat.OnFall += AllCatsLocationFall;
        }
    }

    private void OneCatFall()
    {
        AchievementsManager.AddAchievement(_oneCatFall);
    }
    
    private void AllCatsLocationFall()
    {
        _catsInLocationCount++;
        if (_catsInLocationCount == _cats.Length) AchievementsManager.AddAchievement(_allCatsFallLocation);
    }
}