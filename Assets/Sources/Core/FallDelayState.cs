using System.Collections;
using Sources.StateManagement;
using UnityEngine;

namespace Sources.Core
{
    public class FallDelayState : State
    {
        private readonly Unit _unit;
        private readonly StateMachine _stateMachine;
        private readonly UnitView _self;

        public FallDelayState(StateMachine stateMachine, UnitView self, Unit unit)
        {
            _stateMachine = stateMachine;
            _self = self;
            _unit = unit;
        }
        
        public override void OnEnter()
        {
            _self.StartCoroutine(Delay());
        }
        
        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.05f);
            _unit.TakeNextCell();
            _stateMachine.Enter<FallState>();
        }
    }
}