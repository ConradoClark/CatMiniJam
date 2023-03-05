using System;
using System.Collections.Generic;
using System.Linq;
using Licht.Impl.Orchestration;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using Licht.Unity.Physics;
using Licht.Unity.Physics.CollisionDetection;
using UnityEngine;

public class DamageOnContact : BaseGameObject
{
    [field:SerializeField]
    public LichtPhysicsCollisionDetector HitBox { get; private set; }
    [field: SerializeField]
    public float DamageCooldownInMs { get; private set; }
    
    [field: SerializeField]
    public int Damage { get; private set; }

    private LichtPhysics _physics;
    protected override void OnAwake()
    {
        base.OnAwake();
        _physics = this.GetLichtPhysics();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        DefaultMachinery.AddBasicMachine(HandleContact());
    }

    private IEnumerable<IEnumerable<Action>> HandleContact()
    {
        while (ComponentEnabled)
        {
            Damageable damageable = null;
            var trigger = HitBox.Triggers.FirstOrDefault(t => t.TriggeredHit &&
                                                              _physics.TryGetPhysicsObjectByCollider(t.Collider,
                                                                  out var targetObject) &&
                                                              targetObject.TryGetCustomObject(out damageable));

            if (trigger.TriggeredHit)
            {
                damageable.Damage(Damage);
                yield return TimeYields.WaitMilliseconds(GameTimer, DamageCooldownInMs);
            }

            yield return TimeYields.WaitOneFrameX;
        }
    }
}
