using UnityEngine;

namespace Sources.Core
{
    [CreateAssetMenu]
    public class UnitConfig : ScriptableObject
    {
        [field: SerializeField] public int Type { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
    }
}