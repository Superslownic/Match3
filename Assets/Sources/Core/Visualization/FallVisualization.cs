using System.Collections;
using Sources.Behaviour;
using Sources.Extensions;
using Sources.GlobalEvents;
using UnityEngine;

namespace Sources.Core
{
    public class FallVisualization
    {
        private readonly GridVisualization _gridVisualization;

        public FallVisualization(GridVisualization gridVisualization)
        {
            _gridVisualization = gridVisualization;
            EventManager.GetEvent<OnUnitReadyToFall>().AddListener(HandleUnitReadyToFall);
            EventManager.GetEvent<OnUnitPositionChanged>().AddListener(HandleUnitPositionChanged);
        }

        private void HandleUnitReadyToFall(Unit unit)
        {
            TickHandler.Instance.StartCoroutine(Delay(unit));
        }
        
        private void HandleUnitPositionChanged(Unit unit)
        {
            TickHandler.Instance.StartCoroutine(Fall(unit));
        }

        private IEnumerator Delay(Unit unit)
        {
            yield return new WaitForSeconds(0.05f);
            EventManager.GetEvent<OnUnitStartFall>()?.Invoke(unit);
        }

        private IEnumerator Fall(Unit unit)
        {
            UnitView view = _gridVisualization.GetView(unit);

            const float time = 0.1f;
            float timer = 0;
            Vector3 previousPosition = view.transform.position;

            while (timer < time)
            {
                timer += Time.deltaTime;
                view.transform.position = Vector3.Lerp(previousPosition, unit.Position.ToVector3(), timer / time);
                yield return null;
            }
            
            EventManager.GetEvent<OnUnitEndFall>()?.Invoke(unit);
        }
    }
}