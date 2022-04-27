using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Core
{
    [CreateAssetMenu]
    public class Combinations : ScriptableObject, IEnumerable<Combination>
    {
        [SerializeField] private List<Combination> _list;

        IEnumerator<Combination> IEnumerable<Combination>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}