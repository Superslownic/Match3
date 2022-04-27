using UnityEngine;

namespace Sources.Tickable
{
    [CreateAssetMenu]
    public class Combination : ScriptableObject
    {
        [SerializeField] private Variation[] _variations;

        public bool Validate(Cell target, Grid grid, out Cell[] cells)
        {
            foreach (Variation variation in _variations)
            {
                if (variation.Validate(target, grid))
                {
                    cells = variation.GetCells(target, grid);
                    return true;
                }
            }
            
            cells = null;
            return false;
        }
    }
}