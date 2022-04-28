using System.Collections;
using Sources.Behaviour;
using Sources.GlobalEvents;
using UnityEngine;

namespace Sources.Core
{
    public class SwapVisualization
    {
        private readonly GridVisualization _gridVisualization;

        public SwapVisualization(GridVisualization gridVisualization)
        {
            _gridVisualization = gridVisualization;
            EventManager.GetEvent<OnSwapStart>().AddListener(HandleSwap);
        }

        private void HandleSwap(Unit unit1, Unit unit2)
        {
            TickHandler.Instance.StartCoroutine(Swap(unit1, unit2));
        }
        
        private IEnumerator Swap(Unit unit1, Unit unit2)
        {
            UnitView view1 = _gridVisualization.GetView(unit1);
            UnitView view2 = _gridVisualization.GetView(unit2);

            Vector3 position1 = view1.transform.position;
            Vector3 position2 = view2.transform.position;

            const float time = 0.2f;
            float timer = 0;

            while (timer < time)
            {
                timer += Time.deltaTime;
                view1.transform.position = Vector3.Lerp(position1, position2, timer / time);
                view2.transform.position = Vector3.Lerp(position2, position1, timer / time);
                yield return null;
            }
            
            EventManager.GetEvent<OnSwapEnd>().Invoke(unit1, unit2);
        }
    }
}