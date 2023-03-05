using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Licht.Unity.Objects;
using UnityEngine;

public class AlignParticleToAnimator : BaseGameObject
{
    [field:SerializeField]
    public CatAnimationController AnimController { get; private set; }

    [field:SerializeField]
    public ParticleSystem ParticleSystem { get; private set; }

    private ParticleSystemRenderer _particleRenderer;
    protected override void OnAwake()
    {
        base.OnAwake();
        _particleRenderer = ParticleSystem.GetComponent<ParticleSystemRenderer>();

    }

    private void LateUpdate()
    {
        var main = ParticleSystem.main;
        main.startRotationY =
            new ParticleSystem.MinMaxCurve(AnimController.SpriteRenderer.flipX ? Mathf.Deg2Rad*180 : 0);
        //_particleRenderer.material.SetTexture("_MainTex", AnimController.SpriteRenderer.);
        //_particleRenderer.flip = new Vector3(AnimController.SpriteRenderer.flipX ? 1 : 0, 0, 0);
    }
}
