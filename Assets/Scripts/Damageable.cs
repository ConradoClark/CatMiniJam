using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Objects;
using Licht.Unity.Physics;
using UnityEngine;

public class Damageable : BaseGameObject
{
    [field:SerializeField]
    public LichtPhysicsObject PhysicsObject { get; private set; }
    public event Action<int> OnDamage;
    public bool IsInvulnerable { get; set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        PhysicsObject.AddCustomObject(this);
    }

    public void Damage(int damage)
    {
        if (IsInvulnerable) return;
        OnDamage?.Invoke(damage);
    }
}
