using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;

public class DropCatBombOnBounce : BaseGameObject

{
    [field: SerializeField]
    public ScriptPrefab Bomb { get; private set; }

    [field: SerializeField]
    public Bounce Bounce { get; private set; }

    [field: SerializeField]
    public Vector3 Offset { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        Bounce.OnBounce += Bounce_OnBounce;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Bounce.OnBounce -= Bounce_OnBounce;
    }

    private void Bounce_OnBounce()
    {
        Bomb.TrySpawnEffect(transform.position + Offset, out _);
    }
}
