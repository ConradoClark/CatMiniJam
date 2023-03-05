using System;
using System.Collections.Generic;
using System.Linq;
using Licht.Impl.Orchestration;
using Licht.Unity.CharacterControllers;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using Licht.Unity.Physics;
using Licht.Unity.Physics.CollisionDetection;
using UnityEngine;

public class Bounce : BaseGameObject
{
    [field: SerializeField]
    public LichtPhysicsCollisionDetector BounceDetector { get; private set; }
    [field: SerializeField]
    public LichtPlatformerJumpController JumpController { get; private set; }
    [field: SerializeField]
    public LichtPlatformerJumpController.CustomJumpParams BounceParams { get; private set; }

    private LichtPhysics _physics;

    public event Action OnBounce;
    protected override void OnAwake()
    {
        base.OnAwake();
        _physics = this.GetLichtPhysics();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        DefaultMachinery.AddBasicMachine(HandleBounce());
    }
    private IEnumerable<IEnumerable<Action>> HandleBounce()
    {
        while (ComponentEnabled)
        {
            Bounceable target = null;
            var trigger = BounceDetector.Triggers.FirstOrDefault(t => t.TriggeredHit &&
                                                                      _physics.TryGetPhysicsObjectByCollider(t.Collider,
                                                                          out var targetObject) &&
                                                                      targetObject.TryGetCustomObject(out target));

            if (trigger.TriggeredHit && !BounceDetector.PhysicsObject.GetPhysicsTrigger(JumpController.GroundedTrigger))
            {
                target.Bounce(BounceDetector.PhysicsObject);
                OnBounce?.Invoke();
                yield return JumpController.ExecuteJump(customParams: BounceParams).AsCoroutine()
                    .Combine(TimeYields.WaitOneFrameX);
            }
            else yield return TimeYields.WaitOneFrameX;
        }
    }
}
