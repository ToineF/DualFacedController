using AntoineFoucault.Utilities;
using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerLevelPair[] _levels;

    private void Start()
    {
        LoadLevel(LevelCountDebug.Instance?.CurrentLevel ?? 0);
    }

    public void LoadLevel(int currentLevel)
    {
        currentLevel = currentLevel.Modulo(_levels.Length);
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].Player.SetActive(false);
            _levels[i].Level.SetActive(false);
        }
        _levels[currentLevel].Player.SetActive(true);
        _levels[currentLevel].Level.SetActive(true);
    }
}

[Serializable]
public class PlayerLevelPair
{
    public GameObject Player;
    public GameObject Level;
}
