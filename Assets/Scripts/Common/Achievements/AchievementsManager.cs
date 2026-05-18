using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace AntoineFoucault.Utilities.Achievements
{
    public class AchievementsManager : MonoBehaviour
    {
        
        public static System.Action<AchievementData> OnAddAchievement;
        public static System.Action OnAllAchievements;
        public static System.Action OnResetAchievements;

        public static List<AchievementData> AllAchievements { get; set; }
        private static AchievementsManager _achievementsManager;

        [SerializeField] private List<AchievementData> _allAchievements;
        [SerializeField] private bool _resetInEditor = true;

        private static Dictionary<AchievementData, bool> _completedAchievements = new();

        private const string _savePath = "/achievements.txt";

        private void Awake()
        {
            AllAchievements = _allAchievements;
            
            #if UNITY_EDITOR
            if (_resetInEditor)
            {
                ResetAchievements();
            }
            #endif
            
            _completedAchievements = new Dictionary<AchievementData, bool>();
            foreach (var achievement in _allAchievements)
            {
                _completedAchievements.Add(achievement, false);
            }

            Load();
        }

        public static void AddAchievement(AchievementData achievement)
        {
            if (_completedAchievements.TryGetValue(achievement, out bool completed) == false)
            {
                Debug.LogWarning("Achievement " + achievement.Title + " not found");
                return;
            }

            // Check if achievement already unlocked (and don't send the event if it is)
            if (completed)
            {
                Debug.Log("Achievement " + achievement.Title + " is already completed");
                return;
            }
            
            Debug.Log("Get New Achievement : " + achievement.Title);
            _completedAchievements[achievement] = true;
            OnAddAchievement?.Invoke(achievement);
            Save(achievement);
            CheckAllAchievements();
        }

        public static bool IsAchievementCompleted(AchievementData achievement)
        {
            return _completedAchievements[achievement];
        }

        private static void Save(AchievementData achievement)
        {
            var path = Application.persistentDataPath + _savePath;
            
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(achievement.Title);
            }

            Debug.Log("Saved achievements at " + path);
        }

        private void Load()
        {
            var path = Application.persistentDataPath + _savePath;
            if (File.Exists(path) == false)
            {
                Debug.LogWarning("No file existing at " + path);
                return;
            }

            Debug.Log("Load achievements at " + path);
            var lines = File.ReadAllLines(Application.persistentDataPath + _savePath);

            foreach (var line in lines)
            {
                foreach (var achievement in _allAchievements)
                {
                    if (line == achievement.Title)
                    {
                        _completedAchievements[achievement] = true;
                        Debug.Log("Got achievement " + achievement.Title +" with value " + _completedAchievements[achievement]);
                    }
                }
            }
        }

        public static void ResetAchievements()
        {
            var path = Application.persistentDataPath + _savePath;
            if (File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
                Debug.Log("Reset all achievements");
            }
            foreach (var key in _completedAchievements.Keys.ToList())
            {
                _completedAchievements[key] = false;
            }

            OnResetAchievements?.Invoke();
        }

        private static void CheckAllAchievements()
        {
            int count = 0;
            foreach (var key in _completedAchievements.Keys.ToList())
            {
                if (_completedAchievements[key] == false) count++;
            }
            if (count <= 1) OnAllAchievements?.Invoke();
        }
    }
}