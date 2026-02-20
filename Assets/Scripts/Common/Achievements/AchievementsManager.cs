using UnityEngine;

namespace AntoineFoucault.Utilities.Achievements
{
    public class AchievementsManager : MonoBehaviour
    {
        public static System.Action<AchievementData> OnGetAchievement;
        
        private static AchievementsManager _achievementsManager;
        
        private void Awake()
        {
            _achievementsManager = this;
        }

        public static void GetAchievement(AchievementData achievement)
        {
            Debug.Log("Get Achievement : " + achievement.Title);
            OnGetAchievement?.Invoke(achievement);
        }
    }
}