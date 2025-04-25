namespace Cattac.Interactables.NPC
{
    public abstract class Cat_State
    {
        public virtual void StartState(Cat_StateManager stateManager) { }
        public virtual void UpdateState(Cat_StateManager stateManager) { }
        public virtual void ExitState(Cat_StateManager stateManager) { }
        public virtual void OnTargetLost(Cat_StateManager stateManager,IDetectable oldTarget) { }
        public virtual void OnNewTarget(Cat_StateManager stateManager,IDetectable newTarget, IDetectable oldTarget) { }
    }
}