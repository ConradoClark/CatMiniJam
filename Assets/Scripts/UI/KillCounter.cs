using Licht.Impl.Events;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;

public class KillCounter : BaseGameObject
{
    [field:SerializeField]
    public CounterStat Counter { get; private set; }

    [field:SerializeField]
    public LayerMask Layers { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.ObserveEvent<Killable.KillableEvents,Killable>(Killable.KillableEvents.OnDeath, OnEnemyDeath);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        this.StopObservingEvent<Killable.KillableEvents, Killable>(Killable.KillableEvents.OnDeath, OnEnemyDeath);
    }

    private void OnEnemyDeath(Killable obj)
    {
        if (!Layers.Contains(obj.gameObject.layer)) return;
        Counter.Value--;
    }
}
