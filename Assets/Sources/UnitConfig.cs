using UnityEngine;

namespace Sources
{
    [CreateAssetMenu]
    public class UnitConfig : ScriptableObject
    {
        [field: SerializeField] public int Type { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
    }
}