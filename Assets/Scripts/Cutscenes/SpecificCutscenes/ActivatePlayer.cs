using UnityEngine;


public class ActivatePlayer : MonoBehaviour
{
    private void OnEnable()
    {
        MainGame.Instance?.PlayerController?.gameObject?.SetActive(true);
    }

    private void OnDisable()
    {
        MainGame.Instance?.PlayerController?.gameObject?.SetActive(false);
    }

    private void OnDestroy()
    {
        MainGame.Instance?.PlayerController?.gameObject?.SetActive(true);
    }
}