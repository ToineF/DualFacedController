using System;
using UnityEngine;

public class LagCauser : MonoBehaviour
{
    [SerializeField] private int _lagAmount;
    private void Update()
    {
        for (int i = 0; i < _lagAmount; i++)
        {
            var a = Vector3.Distance(Vector3.zero, Vector3.zero);
        }
    }
}
