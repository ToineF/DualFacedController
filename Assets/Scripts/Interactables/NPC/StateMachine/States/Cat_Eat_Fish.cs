using UnityEngine;

namespace Cattac.Interactables.NPC.States
{
    /// <summary>
    /// The cat eats the fish
    /// </summary>
    public class Cat_Eat_Fish : Cat_State
    {
        private float _timer;
        private IDetectable _target;

        public override void StartState(Cat_StateManager stateManager)
        {
            _timer = 5;
            _target = stateManager.Seeker.Target;
        }

        public override void UpdateState(Cat_StateManager stateManager)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0 && stateManager.Seeker.Target == _target)
            {
                GameObject.Destroy(_target.gameObject);
                stateManager.SwitchState(stateManager.Cat_State_Idle);
            }
        }
    }
}