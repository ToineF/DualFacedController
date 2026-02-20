using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AntoineFoucault.Utilities.Achievements
{
    public class AchievementsUIManager : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        
        private void Start()
        {
            AchievementsManager.OnGetAchievement += OnGetAchievement;
        }

        private void OnDestroy()
        {
            AchievementsManager.OnGetAchievement -= OnGetAchievement;
        }

        private void OnGetAchievement(AchievementData achievement)
        {
            Debug.Log("achivement ui");
            _image.sprite = achievement.Image;
            _title.text = achievement.Title;
            _description.text = achievement.Description;
        }
    }
}