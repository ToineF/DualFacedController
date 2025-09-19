using Cattac.Character.Multiplayer;
using Cattac.Collectibles;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Singleton that references and centralises the other game managers
/// </summary>
public class MainGame : MonoBehaviour
{
    public static MainGame Instance;
    [field:SerializeField] public CollectiblesManager CollectiblesManager { get; private set; }
    [field:SerializeField] public LevelCollectiblesData LevelCollectiblesData { get; private set; }
    [field:SerializeField] public PlayersManager PlayersManager { get; private set; }
    [field:SerializeField] public PlayerInput PlayerInputPrefab { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

}