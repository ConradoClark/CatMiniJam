using System;
using System.Collections;
using System.Collections.Generic;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using Licht.Unity.Physics;
using UnityEngine;

public class Bounceable : BaseGameObject
{
    [field: SerializeField]
    public LichtPhysicsObject PhysicsObject { get; private set; }

    public event Action<LichtPhysicsObject> OnBouncedBy;


    protected override void OnEnable()
    {
        base.OnEnable();
        PhysicsObject.AddCustomObject(this);
    }

    public void Bounce(LichtPhysicsObject source)
    {
        OnBouncedBy?.Invoke(source);
    }
}
