using System;
using Cattac.Collectibles;
using UnityEngine;

/// <summary>
/// Singleton that references and centralises the other game managers
/// </summary>
public class MainGame : MonoBehaviour
{
    public static MainGame Instance;
    [field:SerializeField] public CollectiblesManager CollectiblesManager { get; private set; }
    [field:SerializeField] public LevelCollectiblesData LevelCollectiblesData { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

}