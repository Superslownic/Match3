using UnityEngine;

namespace Sources.Animation
{
    public interface ITimeline
    {
        Coroutine Run();
        void Stop();
    }
}