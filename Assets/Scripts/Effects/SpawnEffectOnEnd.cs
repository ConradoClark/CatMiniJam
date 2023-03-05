using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using Licht.Unity.Pooling;
using UnityEngine;

public class SpawnEffectOnEnd : BaseGameObject
{
    [field:SerializeField]
    public Vector3 Offset { get; private set; }
    [field: SerializeField]
    public EffectPoolable EffectPoolable { get; private set; }

    [field: SerializeField]
    public ScriptPrefab Effect { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        EffectPoolable.OnEffectOver += Effect_OnEffectOver;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EffectPoolable.OnEffectOver -= Effect_OnEffectOver;
    }

    private void Effect_OnEffectOver()
    {
        Effect.TrySpawnEffect(transform.position + Offset, out _);
    }
}
