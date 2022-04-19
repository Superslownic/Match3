using System;
using System.Collections;
using Sources.Extensions;
using Sources.StateManagement;
using UnityEngine;

namespace Sources.UnitStates
{
    public class Fall : State
    {
        private readonly StateMachine _stateMachine;
        private readonly Unit _model;
        private readonly AnimationCurve _speed;
        private readonly UnitView _self;
        private readonly Func<UnitView> _next;

        private int _targetWaypointIndex;
        private bool _canFall;
        private float _fallTime;

        public Fall(StateMachine stateMachine, Unit model, AnimationCurve speed, UnitView self, Func<UnitView> next)
        {
            _stateMachine = stateMachine;
            _next = next;
            _self = self;
            _model = model;
            _speed = speed;
            _targetWaypointIndex = 0;
        }
        
        public override void OnEnter()
        {
            _canFall = false;
            _fallTime = 0;
            _self.StartCoroutine(Wait());
        }

        public override void OnUpdate(float delta)
        {
            if(_canFall == false)
                return;

            _fallTime += delta;
            
            Vector3 targetPosition = _model.GetWaypoint(_targetWaypointIndex).ToVector3();
            Vector3 destination = _self.transform.position.ChangeY(-delta * _speed.Evaluate(_fallTime));

            var next = _next.Invoke();
            if (next)
            {
                Vector3 limit = next.transform.position.ChangeY(1);

                if (destination.y < limit.y)
                    destination.y = limit.y;
            }
            
            if (destination.y <= targetPosition.y)
            {
                destination.y = targetPosition.y;
                _targetWaypointIndex++;
            }
            
            _self.transform.position = destination;
            
            if(_targetWaypointIndex > _model.GetLastWaypointIndex())
                _stateMachine.Enter<Idle>();
        }

        private IEnumerator Wait()
        {
            yield return new WaitWhile(() =>
                _next.Invoke() != null && Vector3.Distance(_self.transform.position, _next.Invoke().transform.position) <= 1);
            
            yield return new WaitForSeconds(0.05f);
            
            _canFall = true;
        }
    }
}