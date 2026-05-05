using System.Collections.Generic;
using UnityEngine;

namespace AntoineFoucault.Utilities.Achievements
{
    public class AchievementsManager : MonoBehaviour
    {
        public static System.Action<AchievementData> OnGetAchievement;
        public static System.Action<AchievementData> OnGetAllAchievements;
        
        private static AchievementsManager _achievementsManager;

        [SerializeField] private List<AchievementData> _allAchievements;
        
        private Dictionary<AchievementData, bool> _completedAchievements = new();

        private void Awake()
        {
            _completedAchievements = new Dictionary<AchievementData, bool>();
            foreach (var achievement in _allAchievements)
            {
                _completedAchievements.Add(achievement, false);
            }
        }

        public static void GetAchievement(AchievementData achievement)
        {
            Debug.Log("Get Achievement : " + achievement.Title);
            OnGetAchievement?.Invoke(achievement);
        }
        
        // TODO : How to save ? Use static ? Player Prefs ? JSON File ?
        // Maybe JSON would be easier because my entire save system is already working with player prefs.
    }
}