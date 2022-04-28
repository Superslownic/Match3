using Sources.Core;

namespace Sources.GlobalEvents
{
    public sealed class OnUnitCreated : Event<Unit> { }
    public sealed class OnUnitReadyToFall : Event<Unit> { }
    public sealed class OnUnitStartFall : Event<Unit> { }
    public sealed class OnUnitEndFall : Event<Unit> { }
    public sealed class OnUnitPositionChanged : Event<Unit> { }
    public sealed class OnUnitReadyToDestroy : Event<Unit> { }
    public sealed class OnUnitDestroyed : Event<Unit> { }
    public sealed class OnSwapStart : Event<Unit, Unit> { }
    public sealed class OnSwapEnd : Event<Unit, Unit> { }
}