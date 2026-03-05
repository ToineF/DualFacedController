using Cattac.Interactables.NPC;
using Cattac.Interactables.NPC.States;
using UnityEngine;

public class NPC_Animator : MonoBehaviour
{
    [SerializeField] private Cat_StateManager _catStateManager;
    [SerializeField] private Animator _animator;

    [Header("Animations")]
    [SerializeField] private string _idleStateName;
    [SerializeField] private string _chaseMouseStateName;
    [SerializeField] private string _lookAtDivinityStateName;

    private void Start()
    {
        _catStateManager.OnStateSwitch += OnStateSwitch;
    }

    private void OnStateSwitch(Cat_State state)
    {
        if (state is Cat_State_Idle)
        {
            _animator.SetTrigger(_idleStateName);
        }
        else if (state is Cat_Chase_Mouse)
        {
            _animator.SetTrigger(_chaseMouseStateName);
        }
        else if (state is Cat_State_LookAtDivinity)
        {
            _animator.SetTrigger(_lookAtDivinityStateName);
        }
    }
}
