using UnityEngine;

namespace Cattac.Character
{
    /// <summary>
    /// The main Manager for the character
    /// </summary>
    public class CharacterManager : MonoBehaviour
    {
        public System.Action OnTeleport;
        
        [SerializeField] private CharacterBody _body;
        [SerializeField] private Vector3 _bodyPartRespawnOffset;

        public void TeleportPlayer(Vector3 point, bool sendEvent = true)
        {
            int length = _body.AllRigidbodies.Length;
            for (int i = 0; i < length; i++)
            {
                var rb = _body.AllRigidbodies[i];
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.position = point + _bodyPartRespawnOffset * (i- (length - 1f)/2f);
            }
            
            if (sendEvent) OnTeleport?.Invoke();
        }
    }
}