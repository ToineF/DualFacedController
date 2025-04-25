namespace Cattac.Interactables.NPC.States
{
    /// <summary>
    /// The cat is looking at the divinity
    /// </summary>
    public class Cat_State_LookAtDivinity : Cat_State
    {
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