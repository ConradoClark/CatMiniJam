using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Orchestration;
using Licht.Unity.CharacterControllers;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using Licht.Unity.Physics;
using UnityEngine;

public class KnockBackOnDamage : BaseGameObject
{
    [field: SerializeField]
    public Damageable Damageable { get; private set; }

    [field: SerializeField]
    public LichtPlatformerMoveController MoveController { get; private set; }

    [field: SerializeField]
    public LichtPhysicsObject PhysicsObject { get; private set; }

    [field: SerializeField]
    public Vector2 KnockBackSpeed { get; private set; }

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
        DefaultMachinery.AddUniqueMachine($"knockBackOnDamage_{gameObject.GetInstanceID()}",
            UniqueMachine.UniqueMachineBehaviour.Cancel, HandleKnockBack());
    }

    private IEnumerable<IEnumerable<Action>> HandleKnockBack()
    {
        MoveController.BlockMovement(this);

        DefaultMachinery.AddBasicMachine(PhysicsObject.GetSpeedAccessor(new Vector2(0, KnockBackSpeed.y))
            .ToSpeed(Vector2.zero)
            .Over(0.5f)
            .UsingTimer(GameTimer)
            .Easing(EasingYields.EasingFunction.QuadraticEaseOut)
            .Build());

        yield return PhysicsObject.GetSpeedAccessor(new Vector2(-KnockBackSpeed.x * MoveController.LatestDirection, 0))
            .ToSpeed(Vector2.zero)
            .Over(0.5f)
            .UsingTimer(GameTimer)
            .Easing(EasingYields.EasingFunction.QuadraticEaseOut)
            .Build();

        MoveController.UnblockMovement(this);
    }
}
