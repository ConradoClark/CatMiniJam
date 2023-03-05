using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Orchestration;
using Licht.Unity.Objects;
using Licht.Unity.Physics.CollisionDetection;
using Licht.Unity.Pooling;
using UnityEngine;

public class EndEffectOnContact : BaseGameObject

{
    [field:SerializeField]
    public EffectPoolable EffectPoolable { get; private set; }

    [field: SerializeField]
    public LichtPhysicsCollisionDetector CollisionDetector { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        DefaultMachinery.AddBasicMachine(HandleDetection());
    }

    private IEnumerable<IEnumerable<Action>> HandleDetection()
    {
        while (ComponentEnabled)
        { 
            if (CollisionDetector.Triggers.Any(t => t.TriggeredHit))
            {
                yield return TimeYields.WaitOneFrameX;
                EffectPoolable.EndEffect();
                yield break;
            }
            yield return TimeYields.WaitOneFrameX;
        }
    }
}
