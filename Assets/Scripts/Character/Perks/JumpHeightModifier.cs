using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.CharacterControllers;
using Licht.Unity.Objects;
using UnityEngine;

public class JumpHeightModifier : BaseGameObject
{
    [field:SerializeField]
    public LichtPlatformerJumpController JumpController { get; private set; }

    [field: SerializeField]
    public float JumpSpeed { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        JumpController.JumpSpeed = JumpSpeed;
    }
}
