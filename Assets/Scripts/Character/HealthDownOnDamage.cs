using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using UnityEngine;

public class HealthDownOnDamage : BaseGameObject
{
    [field:SerializeField]
    public Damageable Damageable { get; private set; }

    private Health _health;
    protected override void OnAwake()
    {
        base.OnAwake();
        _health = _health.FromScene();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Damageable.OnDamage += Damageable_OnDamage;
    }

    private void Damageable_OnDamage(int obj)
    {
        _health.HP.Value -= obj;
    }
}
