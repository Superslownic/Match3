using TMPro;
using UnityEngine;

namespace Sources.Core
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _text;

        private Cell _cell;

        public void Construct(Cell cell)
        {
            _cell = cell;
        }

        private void Update()
        {
            if(_cell.Unit == null)
            {
                _text.text = "n";
                return;
            }

            _text.text = _cell.Unit.ID.ToString();
        }
    }
}