using Sources.Components;
using Sources.Extensions;
using Sources.StateManagement;
using UnityEngine;

namespace Sources.Core
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private InputHandler _button;
        [SerializeField] private float _speed;

        private Unit _unit;
        private StateMachine _stateMachine;

        public void Construct(Unit unit, UnitConfig config)
        {
            _unit = unit;
            _unit.OnDestroy += HandleDestroy;
            _renderer.color = config.Color;
            _stateMachine = new StateMachine();
            _stateMachine.AddState(new IdleState(_stateMachine, this, _unit));
            _stateMachine.AddState(new FallDelayState(_stateMachine, this, _unit));
            _stateMachine.AddState(new FallState(_stateMachine, this, _unit, _speed));
            _stateMachine.Enter<IdleState>();
        }

        private void HandleDestroy()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _stateMachine.CurrentState switch
            {
                IdleState _ => Color.red,
                FallDelayState _ => Color.yellow,
                FallState _ => Color.green,
            };
            Gizmos.DrawWireCube(_unit.GridPosition.ToVector3(), Vector3.one);
        }
    }
}