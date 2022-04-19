using System;
using System.Collections;
using UnityEngine;

namespace Sources.Animation
{
    public class Timeline : ITimeline
    {
        private readonly MonoBehaviour _context;
        private readonly float _duration;

        private Coroutine _current;
        private float _time;

        public float Progress => _time / _duration;
        public Action StartCallback { get; set; }
        public Action<float> UpdateCallback { get; set; }
        public Action EndCallback { get; set; }

        public Timeline(MonoBehaviour context, float duration)
        {
            if (duration < 0)
                throw new InvalidOperationException();

            _context = context;
            _duration = duration;
        }

        public Coroutine Run()
        {
            Stop();
            return _current = _context.StartCoroutine(Body());
        }

        public void Stop()
        {
            if (_current != null)
                _context.StopCoroutine(_current);
        }

        private IEnumerator Body()
        {
            StartCallback?.Invoke();
            
            while (Progress < 1)
            {
                _time += Time.deltaTime;
                Debug.Log(_time);
                UpdateCallback?.Invoke(Progress);
                yield return null;
            }

            EndCallback?.Invoke();
        }
    }
}
