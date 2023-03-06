using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.CharacterControllers;
using Licht.Unity.Objects;
using UnityEngine;

public class BounceModifier : BaseGameObject
{
    [field:SerializeField]
    public Bounce Bounce { get; private set; }

    [field: SerializeField]
    public float JumpSpeed { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        Bounce.BounceParams = new LichtPlatformerJumpController.CustomJumpParams
        {
            AccelerationTime = Bounce.BounceParams.AccelerationTime,
            DecelerationTime = Bounce.BounceParams.DecelerationTime,
            JumpSpeed = JumpSpeed,
            Identifier = Bounce.BounceParams.Identifier,
            MinJumpDelay = Bounce.BounceParams.MinJumpDelay,
            MovementStartEasing = Bounce.BounceParams.MovementStartEasing,
            MovementEndEasing = Bounce.BounceParams.MovementEndEasing,
        };
    }
}
