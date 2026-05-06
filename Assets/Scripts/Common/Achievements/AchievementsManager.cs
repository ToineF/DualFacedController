using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace AntoineFoucault.Utilities.Achievements
{
    public class AchievementsManager : MonoBehaviour
    {
        
        // Current problems left :
        // Option to reset save system
        // Save good lines (never the same two lines) but don't erase already saved lines (maybe only save the achievements that are "true")
        
        public static System.Action<AchievementData> OnGetAchievement;
        public static System.Action<AchievementData> OnGetAllAchievements;

        private static AchievementsManager _achievementsManager;

        [SerializeField] private List<AchievementData> _allAchievements;

        private static Dictionary<AchievementData, bool> _completedAchievements = new();

        private const string _savePath = "/achievements.txt";
        private const char _separatorChar = '$';

        private void Awake()
        {
            _completedAchievements = new Dictionary<AchievementData, bool>();
            foreach (var achievement in _allAchievements)
            {
                _completedAchievements.Add(achievement, false);
            }

            Load();
        }

        public static void GetAchievement(AchievementData achievement)
        {
            Debug.Log("Get Achievement : " + achievement.Title);
            OnGetAchievement?.Invoke(achievement);
            Save();
        }

        private static void Save()
        {
            var path = Application.persistentDataPath + _savePath;
            StringBuilder achievementsBuilder = new StringBuilder();

            foreach (var achievement in _completedAchievements)
            {
                achievementsBuilder.AppendLine(achievement.Key.Title + _separatorChar + achievement.Value);
            }

            File.WriteAllText(path, achievementsBuilder.ToString());
            Debug.Log("Saved achievements at " + path);
        }

        private void Load()
        {
            var path = Application.persistentDataPath + _savePath;
            if (File.Exists(Application.persistentDataPath + _savePath) == false)
            {
                Debug.LogError("No file existing at " + path);
                return;
            }

            Debug.Log("Load achievements at " + path);
            var lines = File.ReadAllLines(Application.persistentDataPath + _savePath);

            foreach (var line in lines)
            {
                var lineContents = line.Split(_separatorChar);
                foreach (var achievement in _allAchievements)
                {
                    if (lineContents[0] == achievement.Title)
                    {
                        _completedAchievements[achievement] = lineContents[1].ToLower() == "true";
                        Debug.Log("Got achievement " + achievement.Title +" with value " + _completedAchievements[achievement]);
                    }
                }
            }
        }
    }
}