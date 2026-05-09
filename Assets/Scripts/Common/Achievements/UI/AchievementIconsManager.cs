using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AntoineFoucault.Utilities.Achievements
{
    public class AchievementIconsManager : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private AchievementIcon _achievementIconPrefab;
        [SerializeField] private GridLayoutGroup _grid;
        
        [Header("UI")]
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        
        [Header("Navigation")]
        [SerializeField] private SubMenu _subMenu;

        private AchievementIcon[] _icons;

        private void Start()
        {
            SpawnAchievements();
            FixNavigation();
        }

        private void SpawnAchievements()
        {
            var allAchievements = AchievementsManager.AllAchievements;
            _icons = new AchievementIcon[allAchievements.Count];
            
            for (int i = 0; i < allAchievements.Count; i++)
            {
                var newIcon = Instantiate(_achievementIconPrefab, _grid.transform);
                newIcon.Initialize(allAchievements[i]);
                newIcon.OnSelect += OnSelect;
                _icons[i] = newIcon;
            }
        }

        private void OnDestroy()
        {
            foreach (var icon in _icons)
            {
                icon.OnSelect -= OnSelect;
            }
        }

        private void FixNavigation()
        {
            // Set first selected button
            _subMenu.FirstSelectedButton = _icons[0].gameObject;
            
            // Set navigation automatically (explicit)
            int lineLength = _grid.constraintCount;
            int columnLength = Mathf.CeilToInt((float)_icons.Length / _grid.constraintCount);
            for (int i = 0; i < _icons.Length; i++)
            {
                var currentButton = _icons[i].Button;
                Navigation navigation = new Navigation();
                navigation.mode = Navigation.Mode.Explicit;

                var currentColumn =  Mathf.FloorToInt((float)i / lineLength);
                navigation.selectOnLeft = _icons[(i - 1 + lineLength) % lineLength + currentColumn * lineLength].Button;
                navigation.selectOnRight = _icons[(i + 1) % lineLength + currentColumn * lineLength].Button;
                navigation.selectOnDown = _icons[(i + lineLength) % _icons.Length].Button;
                navigation.selectOnUp = _icons[(i - lineLength + _icons.Length) % _icons.Length].Button;
                
                currentButton.navigation = navigation;
            }
        }
        

        private void OnSelect(AchievementData data)
        {
            _title.text = data.Title;
            _description.text = data.Description;
        }
    }
}