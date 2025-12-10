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
        public Action<int, bool> OnMouseGain { get; set; }

        // Here instead of having reference to cages, have references to SavedMouseData (the Data) to support multiple scenes
        [field: SerializeField] public List<Cage> Mice { get; private set; }

        public void SaveMouse(Cage mouse)
        {
            for (int i = 0; i < Mice.Count; i++)
            {
                if (Mice[i].Data == mouse.Data)
                {
                    OnMouseGain?.Invoke(i, true);
                    return;
                }
            }
        }

        #if UNITY_EDITOR
        [Button("Find Mice in Scene")]
        public void AssignMice()
        {
            Mice = GameObject.FindObjectsByType<Cage>(FindObjectsSortMode.InstanceID).Reverse().ToList();
            EditorUtility.SetDirty(gameObject);
        }
        #endif
    }
}