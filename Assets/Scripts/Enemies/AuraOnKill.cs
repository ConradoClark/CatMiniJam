using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;

public class AuraOnKill : BaseGameObject
{
    [field: SerializeField]
    public float Amount { get; private set; }

    [field: SerializeField]
    public Killable Killable { get; private set; }

    private Aura _aura;
    protected override void OnAwake()
    {
        base.OnAwake();
        _aura = _aura.FromScene();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Killable.OnDeath += Killable_OnDeath;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Killable.OnDeath -= Killable_OnDeath;
    }

    private void Killable_OnDeath()
    {
        _aura.Increase(Amount);
    }
}
