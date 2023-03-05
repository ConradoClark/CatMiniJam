using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;

public class AuraDecreaseOnDamage : BaseGameObject
{
    [field:SerializeField]
    public Damageable Damageable { get; private set; }

    private Aura _aura;

    protected override void OnAwake()
    {
        base.OnAwake();
        _aura = _aura.FromScene();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Damageable.OnDamage += Damageable_OnDamage;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Damageable.OnDamage -= Damageable_OnDamage;
    }

    private void Damageable_OnDamage(int obj)
    {
        if (_aura.Radius>10) _aura.Decrease(_aura.Radius*0.5f);
        else _aura.Decrease(2);
    }
}
