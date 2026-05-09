using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AntoineFoucault.Utilities.Achievements
{
    public class AchievementsUIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        
        [Header("Animation")]
        [SerializeField] private Animator _animator;
        [SerializeField] private float _stayTime;

        private readonly string _appearParameter = "Appear";
        
        private void Start()
        {
            _animator.gameObject.SetActive(false);
            
            AchievementsManager.OnAddAchievement += OnGetAchievement;
        }

        private void OnDestroy()
        {
            AchievementsManager.OnAddAchievement -= OnGetAchievement;
        }

        private void OnGetAchievement(AchievementData achievement)
        {
            Debug.Log("achivement ui");
            _image.sprite = achievement.Image;
            _title.text = achievement.Title;
            _description.text = achievement.Description;
            
            StopAllCoroutines();
            StartCoroutine(Stay());
        }

        private IEnumerator Stay()
        {
            _animator.gameObject.SetActive(true);
            _animator.SetBool(_appearParameter, true);
            
            yield return new WaitForSeconds(_stayTime);
            
            _animator.SetBool(_appearParameter, false);
            
            yield return new WaitForSeconds(10f); // Hides it if it's somehow still visible
            _animator.gameObject.SetActive(false);
            
        }
    }
}