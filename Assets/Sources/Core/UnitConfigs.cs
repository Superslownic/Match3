using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sources
{
    [CreateAssetMenu]
    public class UnitConfigs : ScriptableObject, IEnumerable<UnitConfig>
    {
        [SerializeField] private List<UnitConfig> _list;

        IEnumerator<UnitConfig> IEnumerable<UnitConfig>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}