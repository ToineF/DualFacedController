using System;
using AntoineFoucault.Utilities.Achievements;
using Cattac.Character;
using UnityEngine;

public class AchievementCharacterTracker : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private CharacterHead _characterHead;
    
    [Header("Squeak")]
    [SerializeField] private AchievementData _squeakAchievement;
    [SerializeField] private int _targetSqueakCount = 10;
    
    [Header("Respawn")]
    [SerializeField] private AchievementData _respawnAchievement;

    private static int _squeaksCounter;

    private void Start()
    {
        _characterHead.OnSqueak += OnSqueak;
        _characterHead.CharacterManager.OnTeleport += OnRepawn;
    }

    private void OnSqueak()
    {
        _squeaksCounter++;
        if (_squeaksCounter >= _targetSqueakCount)
        {
            AchievementsManager.AddAchievement(_squeakAchievement);
        }
    }
    
    private void OnRepawn()
    {
        AchievementsManager.AddAchievement(_respawnAchievement);
    }
}