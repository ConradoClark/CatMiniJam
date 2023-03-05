using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.CharacterControllers;
using Licht.Unity.Objects;
using UnityEngine;

public class SpeedModifier : BaseGameObject
{
    [field:SerializeField]
    public float NewSpeed { get; private set; }

    [field: SerializeField]
    public LichtPlatformerMoveController MoveController { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        MoveController.MaxSpeed = NewSpeed;
    }
}
