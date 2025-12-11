using Cattac.Character.Multiplayer;
using Cattac.Collectibles;
using Cattac.Collectibles.Save;
using Cattac.Interactables.MouseCollection;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Singleton that references and centralises the other game managers
/// </summary>
public class MainGame : MonoBehaviour
{
    public static MainGame Instance;
    [field:SerializeField] public PlayersManager PlayersManager { get; private set; }
    [field:SerializeField] public GameObject PlayerController { get; private set; }
    [field:SerializeField] public PlayerInput PlayerInputPrefab { get; private set; }
    [field:SerializeField] public CollectiblesManager CollectiblesManager { get; private set; }
    [field:SerializeField] public LevelCollectiblesData LevelCollectiblesData { get; private set; }
    [field:SerializeField] public SavedMouseFactory CollectiblesFactory { get; private set; }
    [field:SerializeField] public CollectiblesSaveSystem CollectiblesSaveSystem { get; private set; }
    
    private void Awake()
    {
        if (Instance != null) Debug.LogError("Multiples instances of MainGame!");
        Instance = this;
    }

}