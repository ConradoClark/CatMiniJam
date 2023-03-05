using System;
using System.Collections.Generic;
using System.Linq;
using Licht.Impl.Orchestration;
using Licht.Unity.Objects;
using Licht.Unity.Physics.CollisionDetection;
using UnityEngine;


internal class KillableByObjectContact : BaseGameObject
{
    [field: SerializeField]
    public Killable Killable { get; private set; }

    [field: SerializeField]
    public LichtPhysicsCollisionDetector CollisionDetector { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        DefaultMachinery.AddBasicMachine(HandleContact());
    }

    private IEnumerable<IEnumerable<Action>> HandleContact()
    {
        while (ComponentEnabled)
        {
            if (CollisionDetector.Triggers.Any(t=>t.TriggeredHit))
            {
                Killable.Kill();
                yield break;
            }

            yield return TimeYields.WaitOneFrameX;
        }
    }
}

