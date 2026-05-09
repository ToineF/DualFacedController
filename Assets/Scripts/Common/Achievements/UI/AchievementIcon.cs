using UnityEngine;
using UnityEngine.UI;

namespace AntoineFoucault.Utilities.Achievements
{
    public class AchievementIcon : MonoBehaviour
    {
        public System.Action<AchievementData> OnSelect;
        
        public AchievementData Data { get; private set; }

        [field: SerializeField] public Button Button { get; private set; }
        [SerializeField] private Image _icon;

        public void Initialize(AchievementData achievementData)
        {
            AchievementsManager.OnAddAchievement += OnAddAchievement;
            AchievementsManager.OnResetAchievements += OnResetAchievements;
            Data = achievementData;
            _icon.sprite = achievementData.Image;
            UpdateColor();
        }

        private void OnDestroy()
        {
            AchievementsManager.OnAddAchievement -= OnAddAchievement;
            AchievementsManager.OnResetAchievements -= OnResetAchievements;
        }

        public void Select()
        {
            OnSelect?.Invoke(Data);
        }

        private void UpdateColor()
        {
            _icon.color = AchievementsManager.IsAchievementCompleted(Data) ? Color.white : Color.black;
        }
        
        private void OnAddAchievement(AchievementData data)
        {
            if (data != Data) return;
            
            UpdateColor();
        }

        private void OnResetAchievements()
        {
            UpdateColor();
        }
    }
}