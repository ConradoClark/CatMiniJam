using System;
using System.Collections.Generic;
using Licht.Impl.Events;
using Licht.Impl.Orchestration;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;

public class KillCounter : BaseGameObject
{
    [field:SerializeField]
    public CounterStat Counter { get; private set; }

    [field:SerializeField]
    public LayerMask Layers { get; private set; }

    private LevelUpSpawner _levelUpSpawner;
    private Level _level;

    protected override void OnAwake()
    {
        base.OnAwake();
        _levelUpSpawner = _levelUpSpawner.FromScene();
        _level = _level.FromScene();
    }

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

        if (Counter.Value == 0)
        {
            DefaultMachinery.AddUniqueMachine($"levelUp_{this.GetInstanceID()}", UniqueMachine.UniqueMachineBehaviour.Cancel, LevelUp());
        }
    }

    private IEnumerable<IEnumerable<Action>> LevelUp()
    {
        _level.LevelStat.Value++;
        yield return _levelUpSpawner.Spawn().AsCoroutine();
        SetNewTarget();
    }

    private void SetNewTarget()
    {
        // Hardcoded Level Up Formula
        Counter.Value = 10 + 2*(_level.LevelStat.Value-1) 
                           + Mathf.RoundToInt(Mathf.Pow(1.3f, _level.LevelStat.Value));
    }
}
