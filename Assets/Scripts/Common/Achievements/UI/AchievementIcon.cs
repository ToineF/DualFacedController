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
            Data = achievementData;
            _icon.sprite = achievementData.Image;
        }

        public void Select()
        {
            OnSelect?.Invoke(Data);
        }
    }
}