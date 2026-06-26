using System;
using UnityEngine;

namespace ControllerInputs
{
    [Serializable]
    public class SpriteTextPair
    {
        [field:SerializeField] public string Text { get; set; }
        [field:SerializeField] public Sprite Sprite { get; set; }
    }
}