using Sources.Components;
using Sources.Extensions;
using UnityEngine;

namespace Sources.Tickable
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private InputHandler _button;
        
        private Unit _unit;

        public void Construct(Unit unit, UnitConfig config)
        {
            _unit = unit;
            _unit.OnDestroy += HandleDestroy;
            _renderer.color = config.Color;
        }

        private void HandleDestroy()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            transform.position = _unit.Position.ToVector3();
        }
    }
}