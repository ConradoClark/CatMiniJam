using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using Licht.Unity.Physics;
using Licht.Unity.Physics.Forces;
using UnityEngine;

public class DisableGravity : BaseGameObject
{
    [field: SerializeField]
    public LichtPhysicsObject PhysicsObject { get; private set; }

    private Gravity _gravity;

    protected override void OnAwake()
    {
        base.OnAwake();
        _gravity = _gravity.FromScene();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _gravity.BlockForceFor(this, PhysicsObject);
    }
}
