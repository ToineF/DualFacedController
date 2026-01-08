using Cattac.Interactables.NPC.States;
using UnityEngine;

namespace Cattac.Interactables.NPC
{
    public class Cat_StateManager : MonoBehaviour
    {
        public System.Action<Cat_State> OnStateSwitch { get; set; }
        
        [field:Header("Listeners")]
        [field:SerializeField] public NPC_Seeker Seeker { get; private set; }
        [field:Header("Components")]
        [field:SerializeField] public NPC_MoveTo Mover { get; private set; }
        [field:Header("Data")]
        [field:SerializeField] public NPC_Data Data { get; private set; }
        
        
        public Cat_State CurrentState { get; private set; }

        public Cat_Chase_Mouse Cat_Chase_Mouse { get; private set; } = new();
        public Cat_Eat_Fish Cat_Eat_Fish { get; private set; } = new();
        public Cat_MoveToFish CatMoveToFish { get; private set; } = new();
        public Cat_Return_Idle Cat_Return_Idle { get; private set; } = new();
        public Cat_State_Idle Cat_State_Idle { get; private set; } = new();
        public Cat_State_LookAtDivinity Cat_State_LookAtDivinity { get; private set; } = new();

        private void Start()
        {
            Seeker.OnTargetChange += OnTargetChange;
            
            CurrentState = Cat_State_Idle;
            CurrentState.StartState(this);
        }

        private void OnTargetChange(IDetectable oldTarget, IDetectable newTarget)
        {
            if (newTarget == null) CurrentState.OnTargetLost(this, oldTarget);
            else CurrentState.OnNewTarget(this, newTarget, oldTarget);
        }

        private void Update()
        {
            CurrentState.UpdateState(this);
        }

        public void SwitchState(Cat_State newState)
        {
            //Debug.Log($"From state {CurrentState.ToString()} to new state : {newState.ToString()}");
            CurrentState.ExitState(this);
            CurrentState = newState;
            newState.StartState(this);
            OnStateSwitch?.Invoke(newState);
        }
    }
}