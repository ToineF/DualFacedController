using System;
using AntoineFoucault.Utilities.Achievements;
using Cattac.Character;
using UnityEngine;

public class AchievementSqueakTracker : MonoBehaviour
{
    [SerializeField] private CharacterHead _characterHead;
    [SerializeField] private AchievementData _squeakAchievement;
    [SerializeField] private int _targetSqueakCount = 10;

    private static int _squeaksCounter;

    private void Start()
    {
        _characterHead.OnSqueak += OnSqueak;
    }

    private void OnSqueak()
    {
        _squeaksCounter++;
        if (_squeaksCounter >= _targetSqueakCount)
        {
            AchievementsManager.AddAchievement(_squeakAchievement);
        }
    }
}