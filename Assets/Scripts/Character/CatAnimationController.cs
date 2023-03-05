using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.CharacterControllers;
using Licht.Unity.Objects;
using UnityEngine;

public class CatAnimationController : BaseGameObject
{
    [field:SerializeField]
    public Animator Animator { get; private set; }

    [field: SerializeField]
    public LichtPlatformerMoveController MoveController { get; private set; }

    [field: SerializeField]
    public SpriteRenderer SpriteRenderer { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Update()
    {
        if (MoveController.LatestDirection != 0)
        {
            SpriteRenderer.flipX = MoveController.LatestDirection < 0;
        }
    }
}
