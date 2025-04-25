namespace Cattac.Interactables.NPC.States
{
    /// <summary>
    /// The cat goes to the fish
    /// </summary>
    public class Cat_MoveToFish : Cat_State
    {
        public override void UpdateState(Cat_StateManager stateManager)
        {
            stateManager.Mover.UpdateInternal(stateManager.Seeker.Target);
            if (stateManager.Mover.DistanceToTarget < 80)
                stateManager.SwitchState(stateManager.Cat_Eat_Fish);
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