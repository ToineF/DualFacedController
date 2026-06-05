using Cattac.Character;
using UnityEngine;

/// <summary>
/// Teleports a CharacterManager at the gameObject's position
/// </summary>
public class CharacterTeleporter : MonoBehaviour
{
    [SerializeField] private CharacterManager _characterManager;

    public void Teleport()
    {
        _characterManager.TeleportPlayer(transform.position, false);
    }
}