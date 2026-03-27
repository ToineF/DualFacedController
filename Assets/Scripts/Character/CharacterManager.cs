using System;
using UnityEngine;

namespace Cattac.Character
{
    /// <summary>
    /// The main Manager for the character
    /// </summary>
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private CharacterBody _body;

        public void TeleportPlayer(Vector3 point)
        {
            foreach (var rb in _body.AllRigidbodies)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.position = point;
            }
        }
    }
}