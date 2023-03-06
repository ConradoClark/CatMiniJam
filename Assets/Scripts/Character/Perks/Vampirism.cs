using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Events;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;
using Random = UnityEngine.Random;

public class Vampirism : BaseGameObject
{
    [field:SerializeField]
    public float ChanceInPercentage { get; private set; }

    [field: SerializeField]
    public TintFlash TintFlash { get; private set; }

    [field: SerializeField]
    public ScriptPrefab Effect { get; private set; }

    [field: SerializeField]
    public Vampirism PrevVamp { get; private set; }

    private Health _health;

    protected override void OnAwake()
    {
        base.OnAwake();
        _health = _health.FromScene();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (PrevVamp != null) PrevVamp.enabled = false;
        this.ObserveEvent<Killable.KillableEvents, Killable>(Killable.KillableEvents.OnDeath, OnEnemyDeath);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        this.StopObservingEvent<Killable.KillableEvents, Killable>(Killable.KillableEvents.OnDeath, OnEnemyDeath);
    }
    private void OnEnemyDeath(Killable obj)
    {
        if (Random.Range(0f, 1f) > ChanceInPercentage * 0.01f) return;

        _health.HP.Value += 1;
        TintFlash.Flash();
        Effect.TrySpawnEffect(transform.position, out _);
    }
}
