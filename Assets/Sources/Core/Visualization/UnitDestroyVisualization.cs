using System.Collections;
using Sources.Behaviour;
using Sources.GlobalEvents;
using UnityEngine;

namespace Sources.Core
{
    public class UnitDestroyVisualization
    {
        private readonly GridVisualization _gridVisualization;

        public UnitDestroyVisualization(GridVisualization gridVisualization)
        {
            _gridVisualization = gridVisualization;
            EventManager.GetEvent<OnUnitReadyToDestroy>().AddListener(HandleUnitDestroyStart);
        }

        private void HandleUnitDestroyStart(Unit unit)
        {
            // UnitView view = _gridVisualization.GetView(unit);
            // Object.Destroy(view.gameObject);
            // EventManager.GetEvent<OnUnitDestroyed>().Invoke(unit);
            TickHandler.Instance.StartCoroutine(Destroy(unit));
        }

        private IEnumerator Destroy(Unit unit)
        {
            UnitView view = _gridVisualization.GetView(unit);
            
            const float time = 0.5f;
            float timer = 0;
            Vector3 previousScale = view.transform.localScale;

            while (timer < time)
            {
                timer += Time.deltaTime;
                view.transform.localScale = Vector3.Lerp(previousScale, previousScale * 1.3f, timer / time);
                yield return null;
            }
            
            Object.Destroy(view.gameObject);
            EventManager.GetEvent<OnUnitDestroyed>().Invoke(unit);
        }
    }
}