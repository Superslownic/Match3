using Sources.Extensions;
using Sources.StateManagement;
using UnityEngine;

namespace Sources.Core
{
    public class FallState : State
    {
        private readonly Unit _unit;
        private readonly StateMachine _stateMachine;
        private readonly UnitView _self;
        private readonly float _speed;

        private Vector3 _previousPosition;

        public FallState(StateMachine stateMachine, UnitView self, Unit unit, float speed)
        {
            _speed = speed;
            _stateMachine = stateMachine;
            _self = self;
            _unit = unit;
        }

        public override void OnEnter()
        {
            _previousPosition = _self.transform.position;
        }

        public override void OnUpdate(float delta)
        {
            Vector3 deltaPosition = Vector3.Normalize(_unit.GridPosition.ToVector3() - _previousPosition) * (_speed * Time.deltaTime);
            Vector3 newPosition = _self.transform.position + deltaPosition;
            Vector3 lastDirection = _unit.GridPosition.ToVector3() - _previousPosition;
            Vector3 newDirection = _unit.GridPosition.ToVector3() - newPosition;
            
            if (Vector3.Dot(lastDirection, newDirection) >= 0)
            {
                _self.transform.position = newPosition;
            }
            else
            {
                _self.transform.position = _unit.GridPosition.ToVector3();
                _stateMachine.Enter<IdleState>();
            }
            
            // if(Selection.activeGameObject == _self.gameObject)
            // {
            //     Debug.Log(Vector3.Dot(lastDirection, newDirection));
            // }
        }
    }
}