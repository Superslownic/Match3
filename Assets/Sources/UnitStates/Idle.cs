using System;
using Sources.Components;
using Sources.StateManagement;
using Object = UnityEngine.Object;

namespace Sources.UnitStates
{
    public class Idle : State
    {
        private readonly StateMachine _stateMachine;
        private readonly Unit _model;
        private readonly WorldSpaceButton _button;
        private readonly UnitView _self;
        private readonly Func<UnitView> _prev;
        private readonly Func<UnitView> _next;

        public Idle(StateMachine stateMachine, Unit model, WorldSpaceButton button, UnitView self, Func<UnitView> prev, Func<UnitView> next)
        {
            _stateMachine = stateMachine;
            _next = next;
            _prev = prev;
            _self = self;
            _model = model;
            _button = button;
        }

        public override void OnEnter()
        {
            _button.OnClick += HandleButtonClick;
            _model.OnPositionChanged += HandlePositionChanged;
        }

        public override void OnExit()
        {
            _button.OnClick -= HandleButtonClick;
            _model.OnPositionChanged -= HandlePositionChanged;
        }

        private void HandlePositionChanged()
        {
            _stateMachine.Enter<Fall>();
        }

        private void HandleButtonClick()
        {
            _model.Destroy();
            var prev = _prev.Invoke();
            var next = _next.Invoke();
            if(prev) prev.SetNext(next);
            if(next) next.SetPrev(prev);
            Object.Destroy(_self.gameObject);
        }
    }
}