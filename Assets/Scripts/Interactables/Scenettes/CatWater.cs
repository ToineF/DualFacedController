using UnityEngine;

public class CatWater : MonoBehaviour
{
    public static System.Action OnEnterWaterInternal { get; set; }
    public System.Action OnEnterWater { get; set; }

    public void EnterWater()
    {
        OnEnterWater.Invoke();
        OnEnterWaterInternal?.Invoke();
    }
}