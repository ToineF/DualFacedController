using Cattac.Collectibles;
using UnityEngine;

/// <summary>
/// Singleton that references and centralises the other game managers
/// </summary>
public class MainGame : MonoBehaviour
{
    [field:SerializeField] public CollectiblesManager CollectiblesManager { get; private set; }
}