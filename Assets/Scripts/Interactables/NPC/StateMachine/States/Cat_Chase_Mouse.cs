using DG.Tweening;

namespace Cattac.Interactables.NPC.States
{
    /// <summary>
    /// The cat chases the mouse, attacking it
    /// </summary>
    public class Cat_Chase_Mouse : Cat_State
    {
        /*public override void StartState(Cat_StateManager stateManager)
        {
            stateManager.Mover.transform.DOMove(stateManager.Seeker.Target.gameObject.transform.position, stateManager.Data.ChaseMouseDuration).SetEase(stateManager.Data.ChaseMouseEase);
        }*/

        public override void UpdateState(Cat_StateManager stateManager)
        {
            stateManager.Mover.UpdateInternal(stateManager.Seeker.Target);
        }
        public override void OnNewTarget(Cat_StateManager stateManager, IDetectable newTarget, IDetectable oldTarget)
        {
            var newState = newTarget.OnDetect(stateManager);
            if (newState == this) return;
            stateManager.SwitchState(newState);
        }

        public override void OnTargetLost(Cat_StateManager stateManager, IDetectable oldTarget)
        {
            stateManager.SwitchState(stateManager.Cat_State_Idle);
        }
    }
}