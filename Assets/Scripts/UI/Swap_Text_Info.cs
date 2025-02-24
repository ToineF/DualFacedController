using UnityEngine;
using TMPro;

public class Swap_Text_Info : MonoBehaviour
{
    public GameObject area;

    private void OnTriggerStay(Collider other)
    {
        area.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        area.SetActive(false);
    }
}
