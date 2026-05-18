using UnityEngine;
using UnityEngine.Events;


public class WaterElement : MonoBehaviour
{
    [field: SerializeField] public UnityEvent OnWater { get; set; }
}