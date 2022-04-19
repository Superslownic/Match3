namespace Sources.StateManagement
{
    public abstract class State
    {
        public virtual void OnEnter() { }
        public virtual void OnUpdate(float delta) { }
        public virtual void OnExit() { }
    }
}