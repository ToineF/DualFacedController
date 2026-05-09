using UnityEngine;

namespace AntoineFoucault.Utilities.Achievements
{
    public class AchievementAddMethod :MonoBehaviour
    {
        public void AddAchievement(AchievementData achievement)
        {
            AchievementsManager.AddAchievement(achievement);
        }
    }
}