using System;
using UnityEngine;

namespace Cattac.Interactables.ChaseSequence
{
    public class ChasedCat : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.position += transform.forward * (_speed * Time.deltaTime);
        }
    }
}