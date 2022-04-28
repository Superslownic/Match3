using UnityEngine;

namespace Sources.Core
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        public void Construct(UnitConfig config)
        {
            _renderer.color = config.Color;
        }
    }
}