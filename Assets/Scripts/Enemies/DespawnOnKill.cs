using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Orchestration;
using Licht.Unity.Objects;
using Licht.Unity.Pooling;
using UnityEngine;

public class DespawnOnKill : BaseGameObject
{
    [field:SerializeField]
    public Killable Killable { get; private set; }
    
    [field: SerializeField]
    public EffectPoolable PooledObject { get; private set; }

    [field: SerializeField]
    public float TimeToDespawnInMs { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        Killable.OnDeath += OnDeath;
    }
    private void OnDeath()
    {
        DefaultMachinery.AddBasicMachine(Despawn());
    }

    private IEnumerable<IEnumerable<Action>> Despawn()
    {
        yield return TimeYields.WaitMilliseconds(GameTimer, TimeToDespawnInMs);
        PooledObject.EndEffect();
    }
}
