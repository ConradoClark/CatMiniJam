using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Objects;
using UnityEngine;

public class KillableByBouncing : BaseGameObject
{
    [field:SerializeField]
    public Killable Killable { get; private set; }

    [field: SerializeField]
    public Bounceable Bounceable { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        Bounceable.OnBouncedBy += OnBouncedBy;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Bounceable.OnBouncedBy -= OnBouncedBy;
    }

    private void OnBouncedBy(Licht.Unity.Physics.LichtPhysicsObject obj)
    {
        Killable.Kill();
    }
}
