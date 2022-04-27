using Sources.StateManagement;

namespace Sources.Core
{
    public class IdleState : State
    {
        private readonly Unit _unit;
        private readonly StateMachine _stateMachine;
        private readonly UnitView _self;

        public IdleState(StateMachine stateMachine, UnitView self, Unit unit)
        {
            _self = self;
            _stateMachine = stateMachine;
            _unit = unit;
        }

        public override void OnEnter()
        {
            if(_unit.CanFall)
            {
                _unit.TakeNextCell();
                _stateMachine.Enter<FallState>();
            }
        }

        public override void OnUpdate(float delta)
        {
            if(_unit.CanFall)
                _stateMachine.Enter<FallDelayState>();
            
            // if(Selection.activeGameObject == _self.gameObject)
            // {
            //     Debug.Log(_unit.CanFall);
            // }
        }
    }
}