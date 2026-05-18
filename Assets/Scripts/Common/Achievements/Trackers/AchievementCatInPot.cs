using AntoineFoucault.Utilities.Achievements;
using UnityEngine;


public class AchievementCatInPot : MonoBehaviour
{
    [SerializeField] private AchievementData _achievement;
    [SerializeField] private DestructibleObject _destructibleObject;

    private void Start()
    {
        _destructibleObject.OnDestroyObject += OnDestroyObject;
    }

    private void OnDestroy()
    {
        _destructibleObject.OnDestroyObject += OnDestroyObject;
    }

    private void OnDestroyObject()
    {
        AchievementsManager.AddAchievement(_achievement);
    }
}