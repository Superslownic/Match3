using System;
using System.Collections.Generic;

namespace Sources.StateManagement
{
    public class StateMachine
    {
        private readonly Dictionary<Type, State> _states = new Dictionary<Type, State>();

        public State CurrentState { get; private set; }
        
        public void AddState(State state) =>
            _states.Add(state.GetType(), state);

        public void Update(float delta) =>
            CurrentState?.OnUpdate(delta);

        public void Enter<T>() where T : State
        {
            if(CurrentState is T)
                return;
            
            CurrentState?.OnExit();
            CurrentState = _states[typeof(T)];
            CurrentState.OnEnter();
        }
    }
}
