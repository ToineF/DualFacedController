using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;

    public void Enter()
    {

        animator.SetBool("return", true);
    }
    public void Exit()
    {
        animator.SetBool("return", false);

    }
}
