namespace Cattac.Interactables.NPC.States
{
    /// <summary>
    /// The cat doesn't move, doesn't see anything
    /// </summary>
    public class Cat_State_Idle : Cat_State
    {
        public override void OnNewTarget(Cat_StateManager stateManager, IDetectable newTarget, IDetectable oldTarget)
        {
            var newState = newTarget.OnDetect(stateManager);
            stateManager.SwitchState(newState);
        }
    }
}