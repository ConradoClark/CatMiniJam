using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Impl.Orchestration;
using Licht.Unity.Extensions;
using Licht.Unity.Objects;
using Licht.Unity.Physics;
using Licht.Unity.Physics.Forces;
using Licht.Unity.Pooling;
using UnityEngine;

public class SnowCrystal : EffectPoolable
{

    [field: SerializeField]
    public LichtPhysicsObject PhysicsObject { get; private set; }

    [field: SerializeField]
    public float Speed { get; private set; }

    public event Action OnMaxHeight;

    private Gravity _gravity;
    protected override void OnAwake()
    {
        base.OnAwake();
        _gravity = _gravity.FromScene();
    }

    public override void OnActivation()
    {
        DefaultMachinery.AddBasicMachine(HandleCrystal());
    }

    protected virtual IEnumerable<IEnumerable<Action>> HandleCrystal()
    {
        var direction = new Vector2(CustomProps.ContainsKey("DirX") ? CustomProps["DirX"] : 0f,
            CustomProps.ContainsKey("DirY") ? CustomProps["DirY"] : 0f);

        _gravity.BlockForceFor(this, PhysicsObject);

        yield return PhysicsObject.GetSpeedAccessor(direction * Speed)
            .ToSpeed(new Vector2(direction.x * Speed * 0.5f, 0))
            .Over(0.5f)
            .Easing(EasingYields.EasingFunction.QuadraticEaseOut)
            .BreakIf(() => IsEffectOver)
            .UsingTimer(GameTimer)
            .Build();

        _gravity.UnblockForceFor(this, PhysicsObject);

        OnMaxHeight?.Invoke();

        while (!IsEffectOver)
        {
            PhysicsObject.ApplySpeed(new Vector2(direction.x * Speed * 0.5f, 0));
            yield return TimeYields.WaitOneFrameX;
        }
    }
}
