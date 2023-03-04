using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Licht.Impl.Orchestration;
using Licht.Unity.Objects;
using Licht.Unity.Physics.CollisionDetection;
using UnityEngine;

public class Shadow : BaseGameObject
{
    [field:SerializeField]
    public SpriteRenderer SpriteRenderer { get; private set; }

    [field: SerializeField]
    public LichtPhysicsCollisionDetector ShadowRayCast { get; private set; }

    [field:SerializeField]
    public float YOffset { get; private set; }

    [field: SerializeField]
    public float ScaleFactor { get; private set; }

    private float _latestPoint = 0f;

    private void LateUpdate()
    {

        var trigger = ShadowRayCast.Triggers.FirstOrDefault(t => t.Detected);
        if (trigger.Detected)
        {
            var scale = 1 - Mathf.Lerp(0, 1, trigger.Hit.distance * ScaleFactor);
            var offset = YOffset - YOffset * Mathf.Lerp(0, 1, trigger.Hit.distance * ScaleFactor);
            var point = Mathf.Abs(_latestPoint - trigger.Hit.point.y) > 0.05f ? trigger.Hit.point.y : _latestPoint;
            SpriteRenderer.enabled = true;
            transform.position = new Vector3(transform.position.x, trigger.Hit.point.y + offset, transform.position.z);
            transform.localScale = new Vector3(scale, scale, 1);
            _latestPoint = point;
        }
        else SpriteRenderer.enabled = false;
    }
}
