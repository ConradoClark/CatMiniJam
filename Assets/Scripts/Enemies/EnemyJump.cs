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
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyJump : BaseGameObject
{
    [field:SerializeField]
    public LichtPhysicsObject PhysicsObject { get; private set; }
    [field: SerializeField]
    public float JumpFrequencyInSeconds { get; private set; }

    [field: SerializeField]
    public float JumpHeightMin { get; private set; }

    [field: SerializeField]
    public float JumpHeightMax { get; private set; }

    private Gravity _gravity;
    private float _jumpHeight;
    protected override void OnAwake()
    {
        base.OnAwake();
        _gravity = _gravity.FromScene();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _jumpHeight = Random.Range(JumpHeightMin, JumpHeightMax);
        DefaultMachinery.AddBasicMachine(HandleJumps());
    }

    private IEnumerable<IEnumerable<Action>> HandleJumps()
    {
        while (ComponentEnabled)
        {
            yield return TimeYields.WaitSeconds(GameTimer, Mathf.Max(0.1f, JumpFrequencyInSeconds));

            while (!PhysicsObject.GetPhysicsTrigger(_gravity.GroundedIdentifier) && ComponentEnabled) yield return TimeYields.WaitOneFrameX;

            if (!ComponentEnabled) yield break;

            _gravity.BlockForceFor(this, PhysicsObject);
            yield return PhysicsObject.GetSpeedAccessor(new Vector2(0, _jumpHeight))
                .ToSpeed(Vector2.zero)
                .Easing(EasingYields.EasingFunction.QuadraticEaseOut)
                .Over(0.5f)
                .UsingTimer(GameTimer)
                .Build();

            _gravity.UnblockForceFor(this, PhysicsObject);
        }
    }
}
