using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AntoineFoucault.Utilities.Achievements
{
    public class AchievementsManager : MonoBehaviour
    {
        
        // Current problems left :
        // Option to reset save system
        
        public static System.Action<AchievementData> OnGetAchievement;
        public static System.Action<AchievementData> OnGetAllAchievements;

        private static AchievementsManager _achievementsManager;

        [SerializeField] private List<AchievementData> _allAchievements;
        [SerializeField] private bool _resetInEditor = true;

        private static Dictionary<AchievementData, bool> _completedAchievements = new();

        private const string _savePath = "/achievements.txt";

        private void Awake()
        {
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

        public static void GetAchievement(AchievementData achievement)
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
            OnGetAchievement?.Invoke(achievement);
            Save(achievement);
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

        public void ResetAchievements()
        {
            var path = Application.persistentDataPath + _savePath;
            if (File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
                Debug.Log("Reset all achievements");
            }
        }
    }
}