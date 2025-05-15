using System;
using System.Collections.Generic;
using System.Linq;
using Cattac.Interactables;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace Cattac.Collectibles
{
    public class LevelCollectiblesData : MonoBehaviour
    {
        public Action<int> OnMouseGain;
        
        [field:SerializeField] public List<Cage> Mice { get; private set; }
        
        public void SaveMouse(Cage mouse)
        {
            var index = Mice.FindIndex(e => e == mouse);
            if (index == -1) return;

            OnMouseGain?.Invoke(index);
        }

        [Button("Find Mice in Scene")]
        public void AssignMice()
        {
            Mice = GameObject.FindObjectsOfType<Cage>().Reverse().ToList();
            EditorUtility.SetDirty(gameObject);
        }
    }
}